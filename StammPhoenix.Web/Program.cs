using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption.ConfigurationModel;
using NUglify.Css;
using NUglify.JavaScript;
using StammPhoenix.Persistence;
using StammPhoenix.Web.Core;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddControllersWithViews();

builder.Services.AddTransient<IDatabaseConnection, DatabaseConnection>();
builder.Services.AddTransient<IDatabaseContext, DatabaseContext>();

builder.Services.AddDataProtection()
    .UseCryptographicAlgorithms(new AuthenticatedEncryptorConfiguration
    {
        EncryptionAlgorithm = EncryptionAlgorithm.AES_256_CBC,
        ValidationAlgorithm = ValidationAlgorithm.HMACSHA256
    })
    .PersistKeysToFileSystem(new DirectoryInfo(Environment.GetEnvironmentVariable("ASPNETCORE_DataProtection__Path")!));
builder.Services.AddSingleton<ITempCookieService, TempCookieService>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.HttpOnly = true;
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30);

        options.LoginPath = "/login";
        options.LogoutPath = "/logout";
        options.AccessDeniedPath = "/login/accessdenied";
        options.SlidingExpiration = true;
        options.ReturnUrlParameter = "redirect";
    });

builder.Services.AddTransient<IMapper, WebMapper>();

builder.Services.AddTransient<IPasswordHasher, BCryptPasswordHasher>();

builder.Services.AddTransient<InitialSetupMiddleware>();
builder.Services.AddTransient<NeedsPasswordChangeMiddleware>();

builder.Services.AddWebOptimizer(pipeline =>
{
    var css = pipeline.AddBundle("/css/bundle.css",
            "text/css; charset=UTF-8",
            "css/bootstrap.css",
            "css/site.css")
        .EnforceFileExtensions(".css")
        .AdjustRelativePaths()
        .Concatenate()
        .FingerprintUrls()
        .AddResponseHeader("X-Content-Type-Options", "nosniff");

    var javascript = pipeline.AddBundle("/js/bundle.js",
            "text/javascript; charset=UTF-8",
            "js/jquery-3.6.0.js",
            "js/bootstrap.bundle.js",
            "js/site.js")
        .EnforceFileExtensions(".js")
        .Concatenate()
        .AddResponseHeader("X-Content-Type-Options", "nosniff");

    if (!builder.Environment.IsDevelopment())
    {
        css.Processors.Add(new CssMinifier(new CssSettings {CommentMode = CssComment.None}));
        javascript.Processors.Add(new JavaScriptMinifier(new CodeSettings {PreserveImportantComments = false}));
    }
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
app.UseWebOptimizer();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<InitialSetupMiddleware>();
app.UseMiddleware<NeedsPasswordChangeMiddleware>();

app.MapControllerRoute(
    name : "areas",
    pattern : "{area:exists}/{controller=Home}/{action=Index}/{id?}");
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
