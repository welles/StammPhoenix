using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using SixLabors.ImageSharp;

namespace StammPhoenix.Util;

public class RandomBackgroundService: IRandomBackgroundService
{
    private static Random Random = new Random();

    private IHostingEnvironment Environment { get; }

    public RandomBackgroundService(IHostingEnvironment environment)
    {
        this.Environment = environment;
    }

    public RandomBackgroundData GetRandomBackground()
    {
        var files = this.GetBackgroundImages();

        var randomIndex = RandomBackgroundService.Random.Next(files.Length);

        var selectedImagePath = files[randomIndex];

        using var stream = File.Open(selectedImagePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

        using var image = Image.Load(stream);

        return new RandomBackgroundData
        {
            Path = this.ConvertToUrl(selectedImagePath),
            Height = image.Height,
            Width = image.Width
        };
    }

    private string ConvertToUrl(string path)
    {
        return path.Replace(this.Environment.WebRootPath, string.Empty).Replace("\\", "/");
    }

    private string[] GetBackgroundImages()
    {
        return Directory.GetFiles(Path.Combine(this.Environment.WebRootPath, "image", "bg"));
    }
}
