using GrowUp.DataAccess.Data;
using GrowUp.DataAccess.DbInitializer;
using GrowUp.DataAccess.Repository;
using GrowUp.DataAccess.Repository.IRepository;
using GrowUp.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection")
    ));




builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddDefaultTokenProviders()
    .AddEntityFrameworkStores<AppDbContext>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IDbInitializer, DbInitializer>();

builder.Services.AddSingleton<IEmailSender, EmailSender>();
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();


//configure default url 
builder.Services.ConfigureApplicationCookie(op =>
{
    op.LoginPath = $"/identity/account/login";
    op.LoginPath = $"/Admin/account/loginAdmin";
 
    op.LogoutPath = $"/Identity/Account/Logout";
    op.AccessDeniedPath = $"/Identity/Account/AccessDenied";

});

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
SeedDataBase();
app.UseAuthentication(); ;

app.UseAuthorization();
app.MapRazorPages();
app.MapControllerRoute(
    name: "default",
    pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}");


app.MapControllerRoute(
        name: "reactube_savevideos",
        pattern: "{area=UserDashboard}/{controller=Content}/{action=SaveVideos}/{id?}"
    );


//app.MapControllerRoute(
//        name: "AddVideoRoute",
//        pattern: "{area=UserDashboard}/{controller=Content}/{action=AddVideoDetails}/{id?}");


app.Run();

void SeedDataBase()
{
    using (var scope = app.Services.CreateScope())
    {
        var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();

        dbInitializer.Initialize();
    }
}