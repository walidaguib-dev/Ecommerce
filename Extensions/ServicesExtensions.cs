using System.Threading.RateLimiting;
using Ecommerce.Data;
using Ecommerce.Dtos.Blocking;
using Ecommerce.Dtos.Category;
using Ecommerce.Dtos.Comments;
using Ecommerce.Dtos.Comments.CommentLikes;
using Ecommerce.Dtos.Followers;
using Ecommerce.Dtos.MediaDtos;
using Ecommerce.Dtos.Payments;
using Ecommerce.Dtos.Products;
using Ecommerce.Dtos.Products.Variants;
using Ecommerce.Dtos.Profile;
using Ecommerce.Dtos.ProuctFiles;
using Ecommerce.Dtos.Reviews;
using Ecommerce.Dtos.User;
using Ecommerce.Dtos.WishLists;
using Ecommerce.Helpers;
using Ecommerce.Models;
using Ecommerce.Repositories;
using Ecommerce.Services;
using Ecommerce.Validations.Blocking;
using Ecommerce.Validations.Category;
using Ecommerce.Validations.Comments;
using Ecommerce.Validations.Comments.CommentLikes;
using Ecommerce.Validations.Following;
using Ecommerce.Validations.Media;
using Ecommerce.Validations.Payments;
using Ecommerce.Validations.ProductFiles;
using Ecommerce.Validations.Products;
using Ecommerce.Validations.Products.Variants;
using Ecommerce.Validations.Profile;
using Ecommerce.Validations.Reviews;
using Ecommerce.Validations.User;
using Ecommerce.Validations.WishLists;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using StackExchange.Redis;
using Stripe;


namespace Ecommerce.Extensions
{
    public static class ServicesExtensions
    {
        public static void AddCustomServices(this IServiceCollection services)
        {
            services.AddScoped<ICache, RedisCacheRepo>();
            services.AddTransient<IVerification, UserVerificationRepo>();
            services.AddScoped<IUser, UserRepo>();
            services.AddSingleton<IToken, TokenRepo>();
            services.AddScoped<ICategory, CategoryRepo>();
            services.AddTransient<IMedia, MediaRepo>();
            services.AddScoped<IMessages, MessagesRepo>();
            services.AddScoped<IFollowing, FollowingRepo>();
            services.AddScoped<IBlocking, blockingRepo>();
            services.AddScoped<IProfile, ProfilesRepo>();
            services.AddTransient<IProduct, ProductsRepo>();
            services.AddScoped<IProductFiles, ProductFilesRepo>();
            services.AddScoped<IVariants, ProductsVariantsRepo>();
            services.AddScoped<IComments, CommentsRepo>();
            services.AddScoped<ICommentLikes, CommentlikesRepo>();
            services.AddScoped<IWishList, WishListsRepo>();
            services.AddScoped<IReviews, ReviewsRepo>();
            services.AddScoped<IPayment, PaymentsRepo>();
        }

        public static void AddValidationServices(this IServiceCollection services)
        {
            services.AddKeyedScoped<IValidator<RegisterDto>, RegisterValidation>("register");
            services.AddKeyedScoped<IValidator<LoginDto>, LoginValidation>("login");
            services.AddKeyedScoped<IValidator<UpdateUserDto>, UpdateUserValidation>("updateUser");

            services.AddKeyedScoped<IValidator<CreateCategoryDto>, CategoryValidation>("category");
            services.AddKeyedScoped<IValidator<CreateFile>, MediaValidation>("media");

            services.AddKeyedScoped<IValidator<FollowDto>, FollowingValidation>("following");
            services.AddKeyedScoped<IValidator<BlockUserDto>, BlockingValidation>("blocking");

            services.AddKeyedScoped<IValidator<CreateProfileDto>, CreateProfileValidation>("createProfile");
            services.AddKeyedScoped<IValidator<UpdateProfileDto>, UpdateProfileValidation>("UpdateProfile");

            services.AddKeyedScoped<IValidator<CreateProductDto>, CreateProductValidation>("createProduct");
            services.AddKeyedScoped<IValidator<UpdateProductDto>, UpdateProductValidation>("updateProduct");

            services.AddKeyedScoped<IValidator<CreateProductFile>, ProductFilesValidation>("productFile");
            services.AddKeyedSingleton<IValidator<CreateCommentLike>, CommentLikesValidation>("commentLike");

            services.AddKeyedScoped<IValidator<CreateCommentDto>, CreateCommentValidation>("createComment");
            services.AddKeyedScoped<IValidator<UpdateCommentDto>, UpdateCommentValidation>("updateComment");

            services.AddKeyedScoped<IValidator<AddItemDto>, AddToWishListsValidation>("addItem");

            services.AddKeyedScoped<IValidator<CreateReviewDto>, CreateReviewValidation>("createReview");
            services.AddKeyedScoped<IValidator<UpdateReviewDto>, UpdateReviewValidation>("updateReview");

            services.AddKeyedScoped<IValidator<CreateProductVariant>, CreateProductVariantValidation>("createVariant");
            services.AddKeyedScoped<IValidator<UpdateProductVariant>, UpdateProductVariantValidation>("updateVariant");

            services.AddKeyedScoped<IValidator<InitiatePaymentDto>, InitiatePaymentValidation>("initiatePayment");
        }

        public static void AddDB(this IServiceCollection services, WebApplicationBuilder builder)
        {
            services.AddDbContext<ApplicationDBContext>(options =>
            {
                options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
        }

        public static void AddRateLimit(this IServiceCollection services)
        {
            services.AddRateLimiter(options =>
            {
                options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
                options.AddFixedWindowLimiter("fixed", _ =>
                {
                    _.Window = TimeSpan.FromSeconds(10);
                    _.PermitLimit = 5;
                    _.QueueLimit = 2;
                    _.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                });
            });
        }

        public static void AddRedis(this IServiceCollection services, WebApplicationBuilder builder)
        {
            services.AddSingleton<IConnectionMultiplexer>
            (
                x => ConnectionMultiplexer.Connect(builder.Configuration.GetValue<string>("RedisConnectionString"))
            )
            ;
        }

        public static void AddMailing(this IServiceCollection services, WebApplicationBuilder builder)
        {
            var smtpSettings = builder.Configuration.GetSection("Smtp");
            services.AddSingleton(smtpSettings);
        }

        public static void AddStripe(this IServiceCollection services, WebApplicationBuilder builder)
        {
            var stripeSettings = builder.Configuration.GetSection("Stripe");
            StripeConfiguration.ApiKey = stripeSettings["SecretKey"];
        }

        public static void AddSwaggerConfig(this IServiceCollection services)
        {
            services.AddSwaggerGen(option =>
                {

                    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
                    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                    {
                        In = ParameterLocation.Header,
                        Description = "Please enter a valid token",
                        Name = "Authorization",
                        Type = SecuritySchemeType.Http,
                        BearerFormat = "JWT",
                        Scheme = "Bearer"
                    });
                    option.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                         new OpenApiSecurityScheme
                        {
                        Reference = new OpenApiReference
                        {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                        }
                    },
                    new string[]{}
        }
    });
                    option.AddSignalRSwaggerGen(options =>
                    {

                    });
                });
        }

        public static void AddCorsConfig(this IServiceCollection services)
        {
            services.AddCors(options =>
                {
                    options.AddPolicy("AllowSpecificOrigin", builder =>
                    {
                        builder.WithOrigins("http://localhost:5173") // Add your front-end URL here
                               .AllowAnyHeader()
                               .AllowCredentials()
                               .AllowAnyMethod();
                        builder.WithOrigins("http://localhost:5174") // Add your front-end URL here
                               .AllowAnyHeader()
                               .AllowCredentials()
                               .AllowAnyMethod();
                    });
                });
        }

        public static void AddAuthentication(this IServiceCollection services, WebApplicationBuilder builder)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme =
                options.DefaultChallengeScheme =
                options.DefaultForbidScheme =
                options.DefaultScheme =
                options.DefaultSignInScheme =
                options.DefaultSignOutScheme = JwtBearerDefaults.AuthenticationScheme;

            })
                .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = builder.Configuration["JWT:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = builder.Configuration["JWT:Audience"],
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        System.Text.Encoding.UTF8.GetBytes(builder.Configuration["JWT:SignInKey"]))
                };
            });
        }

        public static void AddIdentityProvider(this IServiceCollection services)
        {
            services.AddIdentity<User, IdentityRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequiredLength = 12;
                options.User.RequireUniqueEmail = true;
            }).AddEntityFrameworkStores<ApplicationDBContext>()
            .AddRoles<IdentityRole>()
            .AddDefaultTokenProviders();
        }
    }

}
