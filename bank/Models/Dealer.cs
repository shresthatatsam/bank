using bank.Models.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace bank.Models
{
    public class Dealer :IDealer
    {
        public DealerViewModel Model { get; set; }
        public readonly ApplicationDbContext _context;

        public Dealer(ApplicationDbContext context)
        {
            Model = new DealerViewModel();
            _context = context;
        }




        public async Task<List<DealerViewModelDto>> GetAllViewModelsAsync()
        {
            var entities = await _context.dealers
                .ToListAsync();

            var viewModels = entities
     .Select(entity => new DealerViewModelDto
     {
         dealer_id = entity.dealer_id,
          dealer_name= entity.dealer_name,
         party_name = _context.parties.Where(y => y.party_id == entity.party_id).Select(x => x.party_name).FirstOrDefault(),
         party = new PartyViewModel
         {
             party_id = entity.party.party_id,
             
         }
         
     }).ToList();


            return viewModels;
        }




        public IActionResult Create()
        {
            _context.dealers.Add(Model);
            _context.SaveChanges();
            return new OkResult();
        }


        public List<PartyViewModel> GetPartyItems()
        {
            List<PartyViewModel> party = _context.parties.ToList();

            List<PartyViewModel> partyItems = party.Select(u => new PartyViewModel
            {
              party_id = u.party_id,
              party_name = u.party_name,
             
            }).ToList();

            partyItems.Insert(0, new PartyViewModel { party_id = Guid.Empty, party_name = "Select User" });

            return partyItems;
        }


        public IActionResult Edit(Guid id, string dealer_name, Guid party_id)
        {
            var dealer = _context.dealers.FirstOrDefault(g => g.dealer_id == id);

            if (dealer == null)
            {
                return null;
            }

            dealer.dealer_name = dealer_name;
            dealer.party_id = party_id;

            _context.dealers.Update(dealer);
            _context.SaveChanges();

            return new OkResult();
        }

        public IActionResult Delete(Guid id)
        {
            var dealer = _context.dealers.FirstOrDefault(g => g.dealer_id == id);
            _context.dealers.Remove(dealer);
            _context.SaveChanges();
            return new OkResult();
        }


    }

    public class DealerViewModelDto
    {
        public Guid dealer_id { get; set; }
        public string dealer_name { get; set; }

        public string party_name {  get; set; } 
        //foreignkey properties
        public Guid party_id { get; set; }

        // Navigation properties 
        public virtual PartyViewModel party { get; set; }
        public ICollection<ForexButSellDealsViewModel> ForexBuySellDeals { get; set; }


    }

    public class DealerViewModel
    {
        public Guid dealer_id { get; set; }
        public string dealer_name { get; set; }

        //foreignkey properties
        public Guid party_id { get; set; }

        // Navigation properties 
        public virtual PartyViewModel party { get; set; }
        public ICollection<ForexButSellDealsViewModel> ForexBuySellDeals { get; set; }


    }
}
