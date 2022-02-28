using NUglify;
using NUglify.JavaScript;
using WebOptimizer;

namespace StammPhoenix.Web.Core;

internal class JavaScriptMinifier : Processor
{
    public JavaScriptMinifier(CodeSettings settings) => this.Settings = settings;

    private CodeSettings Settings { get; set; }

    public override Task ExecuteAsync(IAssetContext config)
    {
        var dictionary = new Dictionary<string, byte[]>();
        foreach (var key in config.Content.Keys)
        {
            if (key.EndsWith(".min"))
            {
                dictionary[key] = config.Content[key];
            }
            else
            {
                var source = config.Content[key].AsString();
                var uglifyResult = Uglify.Js(source, this.Settings);
                var text = uglifyResult.Code;
                if (uglifyResult.HasErrors)
                {
                    text = "/* " + string.Join("\r\n", uglifyResult.Errors) + " */\r\n" + source;
                }
                dictionary[key] = text.AsByteArray();
            }
        }
        config.Content = dictionary;
        return Task.CompletedTask;
    }
}