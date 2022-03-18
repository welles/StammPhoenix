using AutoMapper;
using StammPhoenix.Persistence.Models;
using StammPhoenix.Web.Models.Kontakt;

namespace StammPhoenix.Web.Core;

public static class WebMapper
{
    public static IMapper Create(IServiceProvider serviceProvider)
    {
        var configuration = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<PageContact, KontaktModel>();
        });

        return configuration.CreateMapper();
    }
}