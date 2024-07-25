using bank.Models.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace bank.Models
{
    public class Currency :ICurrency
    {
        public CurrencyViewModel Model { get; set; }
        public readonly ApplicationDbContext _context;

        public Currency(ApplicationDbContext context)
        {
            Model = new CurrencyViewModel();
            _context = context;
        }



        public async Task<List<CurrencyViewModel>> GetAllViewModelsAsync()
        {
            var entities = await _context.currencies
                .ToListAsync();

            var viewModels = entities
     .Select(entity => new CurrencyViewModel
     {
         currency_id = entity.currency_id,
         currency_name = entity.currency_name,

         

     }).ToList();


            return viewModels;
        }

        public IActionResult Create()
        {
            _context.currencies.Add(Model);
            _context.SaveChanges();
            return new OkResult();
        }

    }



    public class CurrencyViewModel
    {
        public Guid currency_id { get; set; }
        public string currency_name { get; set; }


        public ICollection<BankDetailsViewModel> bankdetails { get; set; }

        public ICollection<ForexButSellDealsViewModel> ForexBuySellDeals { get; set; }
    }
}
