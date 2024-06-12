using Microsoft.EntityFrameworkCore;
using mvstwo.Model; // пространство имен класса ApplicationContext
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using mvstwo.FileUploadService;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(option =>
    {
        option.LoginPath = "/Access/Main";
        option.ExpireTimeSpan = TimeSpan.FromMinutes(20);
    });

builder.Services.AddAuthorization(opts => {

    opts.AddPolicy("Teacher", policy => {
        policy.RequireClaim("Role", "Преподаватель");
    });
    opts.AddPolicy("IO", policy => {
        policy.RequireClaim("IO", "1");
    });
    opts.AddPolicy("Group", policy => {
        policy.RequireClaim("Group", "1");
    });
	opts.AddPolicy("Id", policy => {
		policy.RequireClaim("Id", "1");
	});
});

// получаем строку подключения из файла конфигурации
string connection = builder.Configuration.GetConnectionString("DefaultConnection");

// добавляем контекст ApplicationContext в качестве сервиса в приложение
builder.Services.AddDbContext<OkeiSiteContext>(options => options.UseSqlServer(connection));

builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IFileUploadService, LocalFileUploadService>();

var app = builder.Build();
app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Access}/{action=Login}/{id?}");

app.Run();