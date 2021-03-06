using AutoMapper;
using StammPhoenix.Persistence.Models;
using StammPhoenix.Web.Areas.Leiter.Models.Vorstand;

namespace StammPhoenix.Web.Core;

public class WebMapper : Mapper
{
    private static readonly AutoMapper.IConfigurationProvider Configuration = new MapperConfiguration(cfg =>
    {
        cfg.CreateMap<PageContact, VorstandModel>();
        cfg.CreateMap<PageContact, EditVorstandViewModel>();
        cfg.CreateMap<EditVorstandFormModel, EditVorstandViewModel>();
        cfg.CreateMap<CreateVorstandFormModel, CreateVorstandViewModel>();
    });

    public WebMapper() : base(WebMapper.Configuration) { }
}
