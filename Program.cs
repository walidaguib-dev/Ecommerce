
using Ecommerce.Data;
using Ecommerce.Extensions;
using Ecommerce.Hubs;
using Ecommerce.Models;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;



var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddStripe(builder);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCorsConfig();
builder.Services.AddSwaggerConfig();
builder.Services.AddRedis(builder);
builder.Services.AddRateLimit();
builder.Services.AddDB(builder);
builder.Services.AddCustomServices();
builder.Services.AddMailing(builder);
builder.Services.AddValidationServices();
builder.Services.AddIdentityProvider();
builder.Services.AddAuthentication(builder);
builder.Services.AddSignalR();

builder.Services.AddAuthorization(
   o =>
   {
       o.AddPolicy("admin", p => p.RequireRole("admin"));
       o.AddPolicy("user", p => p.RequireRole("user"));
   }
);


builder.Services.AddDirectoryBrowser();
builder.Logging.AddConsole();
var app = builder.Build();



if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

var fileProvider = new PhysicalFileProvider(Path.Combine(builder.Environment.ContentRootPath, "Media"));
var requestPath = "/MyImages";



// Enable displaying browser links.
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = fileProvider,
    RequestPath = requestPath
});

app.UseDirectoryBrowser(new DirectoryBrowserOptions
{
    FileProvider = fileProvider,
    RequestPath = requestPath
});

app.UseCors("AllowSpecificOrigin");

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.UseRateLimiter();
app.MapControllers();

app.MapHub<MessagesHub>("/chat");
app.Run();
