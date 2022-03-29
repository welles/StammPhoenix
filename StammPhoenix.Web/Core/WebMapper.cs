using AutoMapper;
using StammPhoenix.Persistence.Models;
using StammPhoenix.Web.Models.Kontakt;

namespace StammPhoenix.Web.Core;

public class WebMapper : Mapper
{
    private static readonly AutoMapper.IConfigurationProvider Configuration = new MapperConfiguration(cfg =>
    {
        cfg.CreateMap<PageContact, KontaktModel>();
    });

    public WebMapper() : base(WebMapper.Configuration) { }
}
