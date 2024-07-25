using bank.Models.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace bank.Models
{

    public class BankDetails : IBankDetails
    {
        public BankDetailsViewModel Model { get; set; }
        public readonly ApplicationDbContext _context;

        public BankDetails(ApplicationDbContext context)
        {
            Model = new BankDetailsViewModel();
            _context = context;
        }



        public async Task<List<BankDetailsViewModel>> GetAllViewModelsAsync()
        {
            var entities = await _context.bankdetails
                .ToListAsync();

            var viewModels = entities
     .Select(entity => new BankDetailsViewModel
     {
         bank_id = entity.bank_id,
         bank_name = entity.bank_name,
         account_number = entity.account_number,

         currency = new CurrencyViewModel
         {
             currency_name = entity.currency.currency_name,
             currency_id = entity.currency.currency_id,
         }

     }).ToList();


            return viewModels;
        }




        public IActionResult Create()
        {
            _context.bankdetails.Add(Model);
            _context.SaveChanges();
            return new OkResult();
        }


        public List<CurrencyViewModel> GetCurrencyName()
        {
            List<CurrencyViewModel> currency = _context.currencies.ToList();

            List<CurrencyViewModel> currencyItems = currency.Select(u => new CurrencyViewModel
            {
                currency_id = u.currency_id,
                currency_name = u.currency_name,

            }).ToList();

            currencyItems.Insert(0, new CurrencyViewModel { currency_id = Guid.Empty, currency_name = "Select User" });

            return currencyItems;
        }


        public IActionResult Edit(Guid id, string bank_name, string account_number ,Guid currency_id)
        {
            var bankdetail = _context.bankdetails.FirstOrDefault(g => g.bank_id == id);

            if (bankdetail == null)
            {
                return null;
            }

            bankdetail.bank_name = bank_name;
            bankdetail.account_number = account_number;

            _context.bankdetails.Update(bankdetail);
            _context.SaveChanges();

            return new OkResult();
        }

        public IActionResult Delete(Guid id)
        {
            var bankdetail = _context.bankdetails.FirstOrDefault(g => g.bank_id == id);
            _context.bankdetails.Remove(bankdetail);
            _context.SaveChanges();
            return new OkResult();
        }


    }



    public class BankDetailsViewModelDto
    {
        public Guid bank_id { get; set; }
        public string bank_name { get; set; }
        public string account_number { get; set; }

       public string currency_name { get; set; }

        public Guid currency_id { get; set; }
    }


    public class BankDetailsViewModel
    {
        public Guid bank_id { get; set; }
        public string bank_name { get; set;}
        public string account_number { get; set;} 

        //foreignkey properties
        public Guid currency_id { get; set; }

        // Navigation properties 
        public virtual CurrencyViewModel currency { get; set; }


        public ICollection<PartyBankDetailsViewModel> party_bank { get; set; }
    }
}
