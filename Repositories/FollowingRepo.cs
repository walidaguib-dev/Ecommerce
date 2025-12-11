using Ecommerce.Data;
using Ecommerce.Dtos.Followers;
using Ecommerce.Filters;
using Ecommerce.Helpers;
using Ecommerce.Mappers;
using Ecommerce.Models;
using Ecommerce.Services;
using FluentValidation;

using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Ecommerce.Repositories
{
    public class FollowingRepo(
        ApplicationDBContext _context,
        [FromKeyedServices("following")] IValidator<FollowDto> _validator,
        ICache _cache
        ) : IFollowing
    {
        private readonly ApplicationDBContext context = _context;
        private readonly IValidator<FollowDto> validator = _validator;
        private readonly ICache cache = _cache;

        public async Task<Follower> FollowUser(FollowDto dto)
        {
            var result = validator.Validate(dto);
            if (result.IsValid)
            {
                var follow = dto.ToModel();
                await context.followers.AddAsync(follow);
                await context.followings.AddAsync(new Following { followerId = dto.followerId, followingId = dto.userId });
                await context.SaveChangesAsync();
                await cache.RemoveByPattern("followers");
                await cache.RemoveByPattern("followings");
                return follow;
            }
            else
            {
                throw new ValidationException(result.Errors);
            }

        }

        public async Task<Follower> getFollower(string userId, string followerId)
        {



            var follower = await context.followers
                .Include(f => f.follower)
                .Include(f => f.follower.Profile)
                .Where(f => f.UserId == userId && f.FollowerId == followerId)
                .FirstAsync();

            return follower;
        }

        public async Task<ICollection<Follower>> GetFollowers(string userId, QueryFilters query)
        {
            string key = $"followers_{query.PageNumber}_{query.Limit}_{query.SortBy}_{query.IsDescending}_{query.Name}";
            var cachedFollowers = await cache.GetFromCacheAsync<ICollection<Follower>>(key);
            if (!cachedFollowers.IsNullOrEmpty()) return cachedFollowers;
            var followers = context.followers
                .Include(f => f.follower)
                .Include(f => f.follower.Profile)
                .Where(f => f.UserId == userId).AsQueryable();

            if (!string.IsNullOrEmpty(query.Name) || !string.IsNullOrWhiteSpace(query.Name))
            {
                followers = followers.Where(f => f.follower.UserName.Contains(query.Name));
            }

            if (!string.IsNullOrEmpty(query.SortBy) || !string.IsNullOrWhiteSpace(query.SortBy))
            {


                if (query.SortBy.Equals("Id", StringComparison.OrdinalIgnoreCase))
                {

                    followers = query.IsDescending ?
                                 followers.OrderByDescending(f => f.FollowerId)
                                 : followers.OrderBy(f => f.FollowerId);



                }

                if (query.SortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {

                    followers = query.IsDescending ?
                                 followers.OrderByDescending(f => f.follower.UserName)
                                 : followers.OrderBy(f => f.follower.UserName);


                }
            }
            var skipNumber = (query.PageNumber - 1) * query.Limit;
            var pagedFollowers = await followers.Skip(skipNumber).Take(query.Limit).ToListAsync();
            await cache.SetAsync(key, pagedFollowers);
            return pagedFollowers;

        }

        public async Task<Following> GetFollowing(string followerId, string followingId)
        {
            var followingUser = await context.followings
                .Include(f => f.following)
                .Include(f => f.follower.Profile)
                .Where(f => f.followerId == followerId && f.followingId == followingId)
                .FirstAsync();
            return followingUser;
        }

        public async Task<ICollection<Following>> GetFollowings(string followerId, QueryFilters query)
        {
            string key = $"followings_{query.PageNumber}_{query.Limit}_{query.SortBy}_{query.IsDescending}_{query.Name}";
            var cachedFollowings = await cache.GetFromCacheAsync<ICollection<Following>>(key);
            if (!cachedFollowings.IsNullOrEmpty()) return cachedFollowings;
            var followings = context.followings
                .Include(f => f.following)
                .Where(f => f.followerId == followerId).AsQueryable();

            if (!string.IsNullOrEmpty(query.Name) || !string.IsNullOrWhiteSpace(query.Name))
            {
                followings = followings.Where(f => f.following.UserName.Contains(query.Name));
            }

            if (!string.IsNullOrEmpty(query.SortBy) || !string.IsNullOrWhiteSpace(query.SortBy))
            {
                if (query.SortBy.Equals("Id", StringComparison.OrdinalIgnoreCase))
                {
                    followings = query.IsDescending ?
                                 followings.OrderByDescending(f => f.followingId)
                                 : followings.OrderBy(f => f.followingId);
                }

                if (query.SortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    followings = query.IsDescending ?
                                 followings.OrderByDescending(f => f.following.UserName)
                                 : followings.OrderBy(f => f.following.UserName);
                }
            }

            var skipNumber = (query.PageNumber - 1) * query.Limit;
            var pagedFollowings = await followings.Skip(skipNumber).Take(query.Limit).ToListAsync();
            await cache.SetAsync(key, pagedFollowings);
            return pagedFollowings;
        }

        public async Task<Follower> Unfollow(FollowDto dto)
        {

            var result = validator.Validate(dto);
            if (result.IsValid)
            {
                var user = await context.followers
                                .Where(f => f.UserId == dto.userId && f.FollowerId == dto.followerId).FirstAsync();
                var followingUser = await context.followings
                    .Where(f => f.followerId == dto.followerId && f.followingId == dto.userId)
                    .FirstAsync();
                context.followers.Remove(user);
                context.followings.Remove(followingUser);
                await context.SaveChangesAsync();
                await cache.RemoveByPattern("followers");
                await cache.RemoveByPattern("followings");
                return user;
            }
            else
            {
                throw new ValidationException(result.Errors);
            }


        }
    }
}