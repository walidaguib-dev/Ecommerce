using Ecommerce.Data;
using Ecommerce.Dtos.Blocking;
using Ecommerce.Dtos.Followers;
using Ecommerce.Filters;
using Ecommerce.Mappers;
using Ecommerce.Models;
using Ecommerce.Services;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Ecommerce.Repositories
{
    public class blockingRepo
    (
        ApplicationDBContext _context,
        [FromKeyedServices("blocking")] IValidator<BlockUserDto> _validator,
        ICache _cache,
        IFollowing _userservice
    )
     : IBlocking
    {
        private readonly ApplicationDBContext context = _context;
        private readonly IValidator<BlockUserDto> validator = _validator;
        private readonly ICache cache = _cache;
        private readonly IFollowing userservice = _userservice;
        public async Task<BlockedUsers?> BlockUser(BlockUserDto dto)
        {
            var result = validator.Validate(dto);
            if (result.IsValid)
            {
                using (var transaction = await context.Database.BeginTransactionAsync())
                {
                    try
                    {
                        var follower = await userservice.getFollower(dto.UserId, dto.BlockedUserId);
                        if (follower == null)
                        {
                            return null;
                        }
                        else
                        {
                            await userservice.Unfollow(new FollowDto
                            {
                                userId = dto.UserId,
                                followerId = dto.BlockedUserId
                            });
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    try
                    {
                        var following = await userservice.getFollower(dto.BlockedUserId, dto.UserId);
                        if (following == null)
                        {
                            return null;
                        }
                        else
                        {
                            await userservice.Unfollow(new FollowDto
                            {
                                userId = dto.BlockedUserId,
                                followerId = dto.UserId
                            });
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }

                    var user = dto.ToModel();
                    await context.blockedUsers.AddAsync(user);
                    await context.SaveChangesAsync();
                    await transaction.CommitAsync();
                    await cache.RemoveByPattern("users");
                    return user;
                }


            }
            else
            {
                throw new ValidationException(result.Errors);
            }
        }

        public async Task<List<BlockedUsers>> GetBlockedUsers(string userId, QueryFilters query)
        {
            string key = $"users_{query.PageNumber}_{query.Limit}_{query.SortBy}_{query.IsDescending}_{query.Name}";
            var cachedUsers = await cache.GetFromCacheAsync<List<BlockedUsers>>(key);
            if (!cachedUsers.IsNullOrEmpty()) return cachedUsers;
            var users = context.blockedUsers
            .Include(b => b.blockedUser)
            .Include(b => b.user)
            .Where(b => b.userId == userId).AsQueryable();
            if (!string.IsNullOrEmpty(query.Name) || !string.IsNullOrWhiteSpace(query.Name))
            {
                users = users.Where(f => f.blockedUser.UserName.Contains(query.Name));
            }

            if (!string.IsNullOrEmpty(query.SortBy) || !string.IsNullOrWhiteSpace(query.SortBy))
            {
                if (query.SortBy.Equals("Id", StringComparison.OrdinalIgnoreCase))
                {
                    users = query.IsDescending ?
                                 users.OrderByDescending(f => f.blockedUserId)
                                 : users.OrderBy(f => f.blockedUserId);
                }

                if (query.SortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    users = query.IsDescending ?
                                 users.OrderByDescending(f => f.blockedUser.UserName)
                                 : users.OrderBy(f => f.blockedUser.UserName);
                }
            }

            var skipNumber = (query.PageNumber - 1) * query.Limit;
            var pagedUsers = await users.Skip(skipNumber).Take(query.Limit).ToListAsync();
            await cache.SetAsync(key, pagedUsers);
            return pagedUsers;
        }

        public async Task<BlockedUsers?> UnblockUser(BlockUserDto dto)
        {
            var user = await context.blockedUsers
            .Include(b => b.blockedUser)
            .Where(b => b.userId == dto.UserId && b.blockedUserId == dto.BlockedUserId).FirstAsync();

            if (user == null) return null;

            context.blockedUsers.Remove(user);
            await context.SaveChangesAsync();
            await cache.RemoveByPattern("users");
            return user;
        }
    }
}