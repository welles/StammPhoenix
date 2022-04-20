using AutoMapper;
namespace StammPhoenix.Web.Core;

public class WebMapper : Mapper
{
    private static readonly AutoMapper.IConfigurationProvider Configuration = new MapperConfiguration(cfg =>
    {

    });

    public WebMapper() : base(WebMapper.Configuration) { }
}
