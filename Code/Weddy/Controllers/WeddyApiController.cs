using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Weddy.Mailers;
using Weddy.Models;
using Weddy.Services;
using Weddy.ViewModels;

namespace Weddy.Controllers
{
    public class WeddyApiController : ApiController
    {

        private readonly WeddyDbContext _dbContext;
        private readonly ICalculationService _calculationService;
        private readonly IWeddyMailer _mailer;

        public WeddyApiController()
            : this(new WeddyDbContext(), new CalculationService(), new WeddyMailer())
        {
        }

        public WeddyApiController(WeddyDbContext dbContext, ICalculationService calculationService, IWeddyMailer mailer)
        {
            _dbContext = dbContext;
            _calculationService = calculationService;
            _mailer = mailer;
        }

        [HttpGet]
        public PoolViewModel Details(string id)
        {


            var pool = _dbContext.Pools
                .FirstOrDefault(p => p.UniqueIdentifier == id);


            if (pool != null)
            {
                var activeEntries = pool.Entries.Where(e => e.Amount > 0);

                var entriesVm = new List<EntryViewModel>();
                foreach (var entry in activeEntries)
                {
                    var entryVm = ToEntryViewModel(entry);
                    entriesVm.Add(entryVm);
                }

                var poolViewModel = new PoolViewModel
                                        {
                                            UniqueIdentifier = pool.UniqueIdentifier,
                                            EventType = pool.EventType,
                                            EventName = pool.EventName,
                                            ActiveEntries = entriesVm,
                                            Average = pool.Average
                                        };

                return poolViewModel;
            }

            return null;
        }

        [HttpPost]
        public EntryViewModel FillIn(ParticipantFillInViewModel fillInModel)
        {
            var pool = _dbContext.Pools
                .FirstOrDefault(p => p.UniqueIdentifier == fillInModel.UniqueIdentifier);

            if (pool != null)
            {
                var entry = pool.Entries
                    .FirstOrDefault(e => e.Email == fillInModel.Email);

                if (entry == null)
                {
                    entry = new Entry
                                {
                                    Name = fillInModel.Name,
                                    Amount = fillInModel.Amount,
                                    Email = fillInModel.Email,
                                };
                    pool.Entries.Add(entry);
                }
                else
                {
                    entry.Amount = fillInModel.Amount;
                }


                var entryViewModel = ToEntryViewModel(entry);
                _calculationService.UpdatePool(pool);
                _dbContext.SaveChanges();

                return entryViewModel;
            }

            return null;
        }


        private static EntryViewModel ToEntryViewModel(Entry entry)
        {

                var viewModel = new EntryViewModel
                                    {
                                        Name = entry.Name,
                                        Email = entry.Email,
                                        Amount = entry.Amount,
                                        DeviationMax = entry.DeviationMax,
                                        DeviationMin = entry.DeviationMin,
                                        Id = entry.Id,
                                    };

                return viewModel;
        }

    }
}
