using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StammPhoenix.Persistence;
using StammPhoenix.Persistence.Constants;
using StammPhoenix.Web.Areas.Leiter.Models.Teams;

namespace StammPhoenix.Web.Areas.Leiter.Controllers;

[Authorize]
[Area("Leiter")]
public class TeamsController : Controller
{
    private IDatabaseContext DatabaseContext { get; }

    private IMapper Mapper { get; }

    public TeamsController(IDatabaseContext databaseContext, IMapper mapper)
    {
        this.DatabaseContext = databaseContext;
        this.Mapper = mapper;
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> Index()
    {
        var teams = await this.DatabaseContext.GetTeams();

        var viewModel = new TeamsViewModel
        {
            Vorstand = this.Mapper.Map<TeamModel>(teams.SingleOrDefault(x => x.Rank == Rank.Vorstand)),
            Leitendenrunde = this.Mapper.Map<TeamModel>(teams.SingleOrDefault(x => x.Rank == Rank.Leiter)),
            Rover = this.Mapper.Map<TeamModel>(teams.SingleOrDefault(x => x.Rank == Rank.Rover)),
            Pfadfinder = this.Mapper.Map<TeamModel>(teams.SingleOrDefault(x => x.Rank == Rank.Pfadfinder)),
            Jungpfadfinder = this.Mapper.Map<TeamModel>(teams.SingleOrDefault(x => x.Rank == Rank.Jungpfadfinder)),
            Woelflinge = this.Mapper.Map<TeamModel>(teams.SingleOrDefault(x => x.Rank == Rank.Woelflinge))
        };

        return this.View(viewModel);
    }
}
