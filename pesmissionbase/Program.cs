using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using pesmissionbase.Data;
using pesmissionbase.Helpers;
using pesmissionbase.Models;
using pesmissionbase.Services.AddCategory;
using pesmissionbase.Services.AddPermissions;
using pesmissionbase.Services.GetCategory;
using pesmissionbase.Services.IChangePlace;
using pesmissionbase.Services.IRemoveNode;
using pesmissionbase.Services.IRenameNode;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.



string conectinString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<DataBaseContext>(
    p => p.UseSqlServer(conectinString));

builder.Services.AddIdentity<Users, Roles>()
                .AddEntityFrameworkStores<DataBaseContext>()
                .AddDefaultTokenProviders();



builder.Services.AddAuthorization(options =>
{

    options.AddPolicy("acsessAuthorizationHandler", policy =>
    {
        policy.AddRequirements(new testRequirment());
    });

}
);

//builder.Services.AddMvc(config =>
//{
//    var policy = new AuthorizationPolicyBuilder()
//                 .RequireAuthenticatedUser()
//                 .AddRequirements(new testRequirment())
//                 .Build();
//    config.Filters.Add(new AuthorizeFilter(policy));

//});
builder.Services.AddControllers(config =>
{
    var policy = new AuthorizationPolicyBuilder()
                 .RequireAuthenticatedUser()
                 .AddRequirements(new testRequirment())
                 .Build();
    config.Filters.Add(new AuthorizeFilter(policy));

});
builder.Services.AddMemoryCache();
builder.Services.AddControllersWithViews();


builder.Services.AddControllersWithViews()
    .AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);



builder.Services.AddTransient<IRemoveNode, RemoveNode>();
builder.Services.AddTransient<IRenameNode, RenameNode>();
builder.Services.AddTransient<IChangePlace, ChangePlace>();
builder.Services.AddTransient<IGetCategory, GetCategory>();
builder.Services.AddTransient<IAddCategory, AddCategory>();
builder.Services.AddTransient<IAddPermission, AddPermission>();
builder.Services.AddTransient<IAuthorizationHandler, acsessAuthorizationHandler>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
