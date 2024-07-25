using bank.Models.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace bank.Models
{
    public class Party :IParty
    {
        public PartyViewModel Model { get; set; }
        public readonly ApplicationDbContext _context;

        public Party(ApplicationDbContext context)
        {
            Model = new PartyViewModel();
            _context = context;
        }

        public IActionResult Create()
        {
            _context.parties.Add(Model);
            _context.SaveChanges();
            return new OkResult();
        }



        public async Task<List<PartyViewModel>> GetAllViewModelsAsync()
        {
            var entities = await _context.parties
                .ToListAsync();

            var viewModels = entities
             .Select(entity => new PartyViewModel
             {
                 party_id = entity.party_id,
                 party_name = entity.party_name,
                 remarks = entity.remarks,
        
             }).ToList();


            return viewModels;
        }

         public IActionResult Edit(Guid id, string party_name, string remarks)
        {
            var party = _context.parties.FirstOrDefault(g => g.party_id == id);

            if (party == null)
            {
                return null;
            }

            party.party_name = party_name;
            party.remarks = remarks;

            _context.parties.Update(party);
            _context.SaveChanges();

            return new OkResult();
        }

        public IActionResult Delete(Guid id)
        {
            var group = _context.parties.FirstOrDefault(g => g.party_id == id);
            _context.parties.Remove(group);
            _context.SaveChanges();
            return new OkResult();
        }


    }


    public class PartyViewModel
    {
        public Guid party_id { get; set; }
        public string party_name { get; set; }

        public string remarks { get; set; }


        public ICollection<DealerViewModel> dealer { get; set; }

        public ICollection<PartyBankDetailsViewModel> party_bank { get; set; }
        public ICollection<ForexButSellDealsViewModel> ForexBuySellDeals { get; set; }

    }
}
