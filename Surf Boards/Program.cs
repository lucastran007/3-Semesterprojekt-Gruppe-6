using Surf_Boards.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<Surf_BoardsContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Surf_BoardsContext") ?? throw new InvalidOperationException("Connection string 'Surf_BoardsContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();
//builder.Services.AddDbContext<SurfBoardDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("MvcSurfBoardConnectionString")));

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
