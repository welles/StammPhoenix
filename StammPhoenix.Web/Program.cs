using AspNetCore.SEOHelper;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption.ConfigurationModel;
using Microsoft.AspNetCore.StaticFiles;
using StammPhoenix.Persistence;
using StammPhoenix.Util.Interfaces;
using StammPhoenix.Util.Services;
using StammPhoenix.Web.Core;
using StammPhoenix.Web.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRouting(options =>
{
    options.LowercaseUrls = true;
    options.LowercaseQueryStrings = false;
});
builder.Services.AddControllersWithViews();

var environmentVariableService = new EnvironmentVariables();
builder.Services.AddSingleton<IEnvironmentVariables>(environmentVariableService);
environmentVariableService.Validate();

builder.Services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddTransient<IMapper, WebMapper>();
builder.Services.AddTransient<IPasswordHasher, BCryptPasswordHasher>();

builder.Services.AddTransient<IDatabaseConnection, DatabaseConnection>();
builder.Services.AddTransient<IDatabaseContext, DatabaseContext>();
builder.Services.AddSingleton<IDatabaseValidator, DatabaseValidator>();

builder.Services.AddDataProtection()
    .UseCryptographicAlgorithms(new AuthenticatedEncryptorConfiguration
    {
        EncryptionAlgorithm = EncryptionAlgorithm.AES_256_CBC,
        ValidationAlgorithm = ValidationAlgorithm.HMACSHA256
    })
    .PersistKeysToFileSystem(new DirectoryInfo(environmentVariableService.DataProtectionPath));
builder.Services.AddSingleton<ITempCookieService, TempCookieService>();

builder.Services.AddTransient<IContentTypeProvider, FileExtensionContentTypeProvider>();
builder.Services.AddSingleton<IDownloadFilesService, DownloadFilesService>();
builder.Services.AddTransient<IAssetPipelineHelper, AssetPipelineHelper>();

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

builder.Services.AddTransient<IAuth, Auth>();

builder.Services.AddTransient<CheckSecurityStampMiddleware>();
builder.Services.AddTransient<NeedsPasswordChangeMiddleware>();

builder.Services.AddWebOptimizer(pipeline =>
{
    pipeline.AddCssBundle(builder.Environment, "/css/bundle.base.css",
        builder.Environment.GetMinified("/css/bootstrap.css"),
        "/css/material-icons.css");

    pipeline.AddCssBundle(builder.Environment, "/css/bundle.site.css",
        "/css/site.css");

    pipeline.AddJsBundle(builder.Environment, "/js/bundle.base.js",
        builder.Environment.GetMinified("/js/jquery-3.6.0.js"),
        builder.Environment.GetMinified("/js/bootstrap.bundle.js"));

    pipeline.AddJsBundle(builder.Environment, "/js/bundle.site.js",
        "/js/site.js");
    
    pipeline.AddJsBundle(builder.Environment, "/bundle.serviceworker.js",
        "/serviceworker.js");

    pipeline.AddJsBundle(builder.Environment, "/js/bundle.pdf.js",
        builder.Environment.GetMinified("/js/pdf.js"));

    pipeline.AddJsBundle(builder.Environment, "/js/bundle.pdf.worker.js",
        builder.Environment.GetMinified("/js/pdf.worker.js"));

    pipeline.AddJsBundle(builder.Environment, "/js/bundle.pages.downloads.js",
        "/js/pages/downloads.js");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
    app.UseStatusCodePagesWithRedirects("/Home/Error/{0}");
}

app.UseHttpsRedirection();
app.UseWebOptimizer();
app.UseStaticFiles();

SitemapBuilder.BuildSitemap(builder.Environment.WebRootPath);
app.UseXMLSitemap(builder.Environment.WebRootPath);

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<CheckSecurityStampMiddleware>();
app.UseMiddleware<NeedsPasswordChangeMiddleware>();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

await app.Services.GetRequiredService<IDatabaseValidator>().Validate();

app.Run();
