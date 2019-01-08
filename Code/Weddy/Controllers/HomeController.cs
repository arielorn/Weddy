using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Weddy.Mailers;
using Weddy.Models;
using Weddy.Services;
using Weddy.ViewModels;

namespace Weddy.Controllers
{
    public class HomeController : Controller
    {
        private readonly WeddyDbContext _dbContext;
        private readonly ICalculationService _calculationService;
        private readonly IUniqueIdentifierProvider _uniqueIdentifierProvider;
        private readonly IWeddyMailer _mailer;

        public HomeController()
            : this(new WeddyDbContext(), new CalculationService(), 
            new UniqueIdentifierProvider(new WeddyDbContext()), new WeddyMailer())
        {
        }

        public HomeController(WeddyDbContext dbContext, ICalculationService calculationService,
            IUniqueIdentifierProvider uniqueIdentifierProvider, IWeddyMailer mailer)
        {
            _dbContext = dbContext;
            _calculationService = calculationService;
            _uniqueIdentifierProvider = uniqueIdentifierProvider;
            _mailer = mailer;
        }

        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View();
        }

        public ActionResult Create()
        {
            return View(new CreatePoolViewModel());
        }

        [HttpPost]
        public ActionResult Create(CreatePoolViewModel cpvm)
        {
            var pool = new Pool
                           {
                               EventName = cpvm.EventName,
                               EventType = cpvm.EventType,
                               Entries = new List<Entry>()
                           };


            foreach (var participant in cpvm.Participants)
            {
                var entry = new Entry
                                {
                                    Email = participant.Email,
                                    Name = participant.Name
                                };

                pool.Entries.Add(entry);
            }
            pool.UniqueIdentifier = _uniqueIdentifierProvider.GenerateIdentifier();
            _dbContext.Pools.Add(pool);
            _dbContext.SaveChanges();

            return RedirectToAction("Details", new {id = pool.UniqueIdentifier});
        }

        public ActionResult Details(string id)
        {
            var pool = _dbContext.Pools
                .FirstOrDefault(p => p.UniqueIdentifier == id);

            if (pool != null)
            {
                var poolViewModel = new PoolViewModel
                                        {
                                            UniqueIdentifier = pool.UniqueIdentifier,
                                            EventType = pool.EventType,
                                            EventName = pool.EventName,
                                        };

                return View(poolViewModel);
            }

            return View("FindEvent", new FindEventViewModel
                                         {
                                             IsError = true
                                         });
        }

        public PartialViewResult FillIn(string id)
        {
            var viewModel = new ParticipantFillInViewModel
                                {
                                    UniqueIdentifier = id
                                };

            return PartialView("FillIn", viewModel);
        }

        [HttpPost]
        public PartialViewResult FillIn(ParticipantFillInViewModel pfvm)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("FillIn", pfvm);
            }
            var pool = _dbContext.Pools
                .FirstOrDefault(p => p.UniqueIdentifier == pfvm.UniqueIdentifier);

            if (pool != null)
            {
                var entry = pool.Entries
                    .FirstOrDefault(e => e.Email == pfvm.Email);
                if (entry == null)
                {
                    pool.Entries.Add(new Entry
                                         {
                                             Name = pfvm.Name,
                                             Amount = pfvm.Amount,
                                             Email = pfvm.Email,
                                         });
                }
                else
                {
                    entry.Amount = pfvm.Amount;
                }

                _calculationService.UpdatePool(pool);
                _dbContext.SaveChanges();
            }
            return PartialView("ThankYou");
        }

        public ActionResult FindEvent()
        {
            return View("FindEvent", new FindEventViewModel
                                         {
                                             IsError = false
                                         });
        }

        [HttpPost]
        public ActionResult FindEvent(FindEventViewModel fevm)
        {

            return RedirectToAction("Details", new { id = fevm.UniqueIdentifier});
        }

    }
}
