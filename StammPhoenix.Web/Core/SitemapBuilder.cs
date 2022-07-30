using AspNetCore.SEOHelper.Sitemap;

namespace StammPhoenix.Web.Core;

public static class SitemapBuilder
{
    public static void BuildSitemap(string path)
    {
        var list = new[]
        {
            new SitemapNode() { LastModified = DateTime.UtcNow, Frequency = SitemapFrequency.Always, Priority = 1, Url = "/" },
            new SitemapNode() { LastModified = DateTime.UtcNow, Frequency = SitemapFrequency.Always, Priority = 1, Url = "/gruppenstunden" },
            new SitemapNode() { LastModified = DateTime.UtcNow, Frequency = SitemapFrequency.Always, Priority = 1, Url = "/stufe/woelflinge" },
            new SitemapNode() { LastModified = DateTime.UtcNow, Frequency = SitemapFrequency.Always, Priority = 1, Url = "/stufe/jungpfadfinder" },
            new SitemapNode() { LastModified = DateTime.UtcNow, Frequency = SitemapFrequency.Always, Priority = 1, Url = "/stufe/pfadfinder" },
            new SitemapNode() { LastModified = DateTime.UtcNow, Frequency = SitemapFrequency.Always, Priority = 1, Url = "/stufe/rover" },
            new SitemapNode() { LastModified = DateTime.UtcNow, Frequency = SitemapFrequency.Always, Priority = 1, Url = "/verband" },
            new SitemapNode() { LastModified = DateTime.UtcNow, Frequency = SitemapFrequency.Always, Priority = 1, Url = "/downloads" },
            new SitemapNode() { LastModified = DateTime.UtcNow, Frequency = SitemapFrequency.Always, Priority = 1, Url = "/kontakt" },
            new SitemapNode() { LastModified = DateTime.UtcNow, Frequency = SitemapFrequency.Always, Priority = 1, Url = "/datenschutz" },
            new SitemapNode() { LastModified = DateTime.UtcNow, Frequency = SitemapFrequency.Always, Priority = 1, Url = "/impressum" },
        };
        
        new SitemapDocument().CreateSitemapXML(list, path);
    }
}