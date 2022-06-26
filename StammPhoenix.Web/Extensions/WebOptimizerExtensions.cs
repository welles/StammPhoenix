using NUglify.Css;
using NUglify.JavaScript;
using WebOptimizer;

namespace StammPhoenix.Web.Extensions;

public static class WebOptimizerExtensions
{
    public static IAsset AddJsBundle(this IAssetPipeline pipeline, IHostEnvironment environment, string route, params string[] sourceFiles)
    {
        var bundle = pipeline.AddBundle(route,
                "text/javascript; charset=UTF-8",
                sourceFiles)
            .EnforceFileExtensions(".js")
            .Concatenate()
            .AddResponseHeader("Cache-Control", "max-age=31536000")
            .AddResponseHeader("X-Content-Type-Options", "nosniff");

        if (!environment.IsDevelopment())
        {
            bundle.Processors.Add(new Core.JavaScriptMinifier(new CodeSettings { PreserveImportantComments = false }));
        }

        return bundle;
    }

    public static IAsset AddCssBundle(this IAssetPipeline pipeline, IHostEnvironment environment, string route, params string[] sourceFiles)
    {
        var bundle = pipeline.AddBundle(route,
                "text/css; charset=UTF-8",
                sourceFiles)
            .EnforceFileExtensions(".css")
            .AdjustRelativePaths()
            .Concatenate()
            .FingerprintUrls()
            .AddResponseHeader("Cache-Control", "max-age=31536000")
            .AddResponseHeader("X-Content-Type-Options", "nosniff");

        if (!environment.IsDevelopment())
        {
            bundle.Processors.Add(new Core.CssMinifier(new CssSettings { CommentMode = CssComment.None }));
        }

        return bundle;
    }

    public static string GetMinified(this IHostEnvironment environment, string file)
    {
        if (environment.IsDevelopment())
        {
            return file;
        }

        return file.Replace(".css", ".min.css").Replace(".js", ".min.js");
    }
}
