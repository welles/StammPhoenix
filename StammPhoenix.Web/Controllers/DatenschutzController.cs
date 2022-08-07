using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StammPhoenix.Persistence;
using StammPhoenix.Web.Models.Kontakt;

namespace StammPhoenix.Web.Controllers
{
    public class DatenschutzController : Controller
    {
        private IDatabaseContext DatabaseContext { get; }

        private IMapper Mapper { get; }

        public DatenschutzController(IDatabaseContext databaseContext, IMapper mapper)
        {
            this.DatabaseContext = databaseContext;
            this.Mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var kontakte = await this.DatabaseContext.GetPageContacts();

            var viewModel = new KontaktViewModel
            {
                Kontakte = kontakte.Select(this.Mapper.Map<KontaktModel>).ToArray()
            };

            return this.View(viewModel);
        }
    }
}
