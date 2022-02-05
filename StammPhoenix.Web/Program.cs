var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddControllersWithViews();

if (!builder.Environment.IsDevelopment())
{
    builder.Services.AddWebOptimizer(pipeline =>
    {
        pipeline.AddCssBundle("/css/bundle.min.css",
            "/css/bootstrap.min.css",
            "/css/bootstrap-grid.min.css",
            "/css/bootstrap-reboot.min.css",
            "/css/site.css");

        pipeline.AddJavaScriptBundle("/js/bundle.min.js",
            "/js/jquery-3.6.0.min.js",
            "/js/popper.min.js",
            "/js/bootstrap.min.js",
            "/js/site.js");
    });
}
else
{
    builder.Services.AddWebOptimizer(pipeline =>
    {
        pipeline.AddCssBundle("/css/bundle.css",
            "/css/bootstrap.css",
            "/css/bootstrap-grid.css",
            "/css/bootstrap-reboot.css",
            "/css/site.css");

        pipeline.AddJavaScriptBundle("/js/bundle.js",
            "/js/jquery-3.6.0.js",
            "/js/popper.js",
            "/js/bootstrap.js",
            "/js/site.js");
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
