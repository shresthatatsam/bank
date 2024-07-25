using bank.Models;
using bank.Models.Interface;
using Microsoft.AspNetCore.Mvc;

namespace bank.Controllers
{
    public class PartyController : Controller
    {

        private readonly IParty _party;
        public PartyController(IParty party)
        {
            _party = party;

        }
        public async Task<IActionResult> Index()
        {
            var party = await _party.GetAllViewModelsAsync();
            return View(party);
        }









        [HttpPost]
        public IActionResult Create(IFormCollection frm)
        {
            var idString = frm["edit_id"].ToString();
            var party_name = frm["party_name"].ToString();
            var remarks = frm["remarks"].ToString();

            if (!string.IsNullOrEmpty(idString) && Guid.TryParse(idString, out Guid id))
            {
                _party.Edit(id, party_name, remarks);
            }
            else
            {
                _party.Model.party_name = party_name;
                _party.Model.remarks = remarks;
                _party.Create();
            }

            return RedirectToAction("Index");
        }


        [HttpPost]
        public bool Delete(Guid id)
        {
            _party.Delete(id);

            return true;
        }
    }
}
