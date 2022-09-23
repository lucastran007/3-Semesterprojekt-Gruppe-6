using Surf_Boards.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using Surf_Boards.Areas.Identity.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.VisualBasic;
using Surf_Boards.Core;
using Surf_Boards.Core.Repository;
using Surf_Boards.Repositories;
using Microsoft.AspNetCore.Localization;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<Surf_BoardsContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Surf_BoardsContext") ?? throw new InvalidOperationException("Connection string 'Surf_BoardsContext' not found.")));

builder.Services.AddDbContext<Surf_BoardsContext1>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Surf_BoardsContext") ?? throw new InvalidOperationException("Connection string 'Surf_BoardsContext' not found.")));


builder.Services.AddDefaultIdentity<Surf_BoardsUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<Surf_BoardsContext1>();

//builder.Services.Configure<AuthMessageSenderOptions>(builder.Configuration.GetSection("SendGrid"));

builder.Services.AddAuthentication()
.AddGoogle(googleOptions =>
{
    googleOptions.ClientId = "885777028144-osaib3k40o2peh97tu1uttec0ut4pqd0.apps.googleusercontent.com";
    googleOptions.ClientSecret = "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";

})
.AddFacebook(facebookOptions =>
{
    facebookOptions.AppId = "2469950299814688";
    facebookOptions.AppSecret = "f6bd6db2667e16056587e9e1e5492487";
}
);

// Add services to the container.
builder.Services.AddControllersWithViews();
//builder.Services.AddDbContext<SurfBoardDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("MvcSurfBoardConnectionString")));


#region Authorizaition

AddAuthorizationPolicies();

#endregion

AddScoped();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// localization, fix number issue with comma vs dot
//var supportedCultures = new string[] { "da-DK", "en-GB" };
//app.UseRequestLocalization(options =>
//            options
//            .AddSupportedCultures(supportedCultures)
//            .AddSupportedUICultures(supportedCultures)
//            .SetDefaultCulture("da-DK")
//            .RequestCultureProviders.Insert(0, new CustomRequestCultureProvider(context =>
//            {
//                return Task.FromResult(new ProviderCultureResult("en-GB"));
//            }))
//);

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication(); ;

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();
app.Run();

void AddAuthorizationPolicies()
{
    builder.Services.AddAuthorization(options =>
    {
        options.AddPolicy(ConstantsRole.Policies.RequireAdmin, policy => policy.RequireRole(ConstantsRole.Roles.Administrator));
        options.AddPolicy(ConstantsRole.Policies.RequireManager, policy => policy.RequireRole(ConstantsRole.Roles.Manager));

    });
}

void AddScoped()
{
    builder.Services.AddScoped<IUserRepository, UserRepository>();
    builder.Services.AddScoped<IRoleRepository, RoleRepository>();
    builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
}