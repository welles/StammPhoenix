using NUglify.Css;
using NUglify.JavaScript;
using StammPhoenix.Web.Core;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddControllersWithViews();

if (!builder.Environment.IsDevelopment())
{
    // PRODUCTION

    builder.Services.AddWebOptimizer(pipeline =>
    {
        var css = pipeline.AddBundle("/css/bundle.css",
                "text/css; charset=UTF-8",
                "css/bootstrap.min.css",
                "css/bootstrap-grid.min.css",
                "css/bootstrap-reboot.min.css",
                "css/site.css")
            .EnforceFileExtensions(".css")
            .AdjustRelativePaths()
            .Concatenate()
            .FingerprintUrls()
            .AddResponseHeader("X-Content-Type-Options", "nosniff");
        css.Processors.Add(new CssMinifier(new CssSettings {CommentMode = CssComment.None}));

        var javascript = pipeline.AddBundle("/js/bundle.js",
                "text/javascript; charset=UTF-8",
                "js/jquery-3.6.0.min.js",
                "js/popper.min.js",
                "js/bootstrap.min.js",
                "js/site.js")
            .EnforceFileExtensions(".js")
            .Concatenate()
            .AddResponseHeader("X-Content-Type-Options", "nosniff");
        javascript.Processors.Add(new JavaScriptMinifier(new CodeSettings {PreserveImportantComments = false}));
    });
}
else
{
    // DEVELOPMENT

    builder.Services.AddWebOptimizer(pipeline =>
    {
        pipeline.AddBundle("/css/bundle.css",
                "text/css; charset=UTF-8",
                "css/bootstrap.css",
                "css/bootstrap-grid.css",
                "css/bootstrap-reboot.css",
                "css/site.css")
            .EnforceFileExtensions(".css")
            .AdjustRelativePaths()
            .Concatenate()
            .FingerprintUrls()
            .AddResponseHeader("X-Content-Type-Options", "nosniff");

        pipeline.AddBundle("/js/bundle.js",
                "text/javascript; charset=UTF-8",
                "js/jquery-3.6.0.js",
                "js/popper.js",
                "js/bootstrap.js",
                "js/site.js")
            .EnforceFileExtensions(".js")
            .Concatenate()
            .AddResponseHeader("X-Content-Type-Options", "nosniff");
    });
}

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
