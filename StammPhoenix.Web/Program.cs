using NUglify.Css;
using NUglify.JavaScript;
using StammPhoenix.Persistence;
using StammPhoenix.Web.Core;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IDatabaseContext, DatabaseContext>();

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
