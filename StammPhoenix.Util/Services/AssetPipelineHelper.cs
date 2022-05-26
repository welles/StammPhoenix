using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using StammPhoenix.Util.Interfaces;
using WebOptimizer;

namespace StammPhoenix.Util.Services;

public class AssetPipelineHelper : IAssetPipelineHelper
{
    private IHttpContextAccessor HttpContextAccessor { get; }

    private IAssetPipeline AssetPipeline { get; }

    private IOptionsMonitor<WebOptimizerOptions> Options { get; }

    public AssetPipelineHelper(IHttpContextAccessor httpContextAccessor, IAssetPipeline assetPipeline, IOptionsMonitor<WebOptimizerOptions> options)
    {
        this.HttpContextAccessor = httpContextAccessor;
        this.AssetPipeline = assetPipeline;
        this.Options = options;
    }

    public string GetRouteWithVersion(string route)
    {
        this.AssetPipeline.TryGetAssetFromRoute(route, out var asset);

        return asset.Route + "?v=" + asset.GenerateCacheKey(this.HttpContextAccessor.HttpContext, this.Options.CurrentValue);
    }
}
