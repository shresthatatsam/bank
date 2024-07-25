using bank.Models;
using bank.Models.Interface;
using Microsoft.AspNetCore.Mvc;

namespace bank.Controllers
{
    public class DealerController : Controller
    {
        private readonly IDealer _dealer;
        public DealerController(IDealer dealer)
        {
          _dealer = dealer;

        }

        public async Task<IActionResult> Index()
        {
            var partyItems = _dealer.GetPartyItems();
            ViewBag.PartyItems = partyItems;
            var partyViewModels = await _dealer.GetAllViewModelsAsync();

            return View(partyViewModels);
        }

        [HttpPost]
        public IActionResult Create(IFormCollection frm)
        {
            var idString = frm["edit_id"].ToString();
            var dealer_name = frm["dealer_name"].ToString();
            var party_idString = frm["party_name"].ToString();

            if (!Guid.TryParse(party_idString, out Guid party_id))
            {
                throw new ArgumentException("Invalid user_id format");
            }

          

            if (!string.IsNullOrEmpty(idString) && Guid.TryParse(idString, out Guid id))
            {
                _dealer.Edit(id, dealer_name, party_id);
            }
            else
            {
                _dealer.Model.dealer_name = dealer_name;
                _dealer.Model.party_id = party_id;
                _dealer.Create();
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public bool Delete(Guid id)
        {
            _dealer.Delete(id);

            return true;
        }
    }
}
