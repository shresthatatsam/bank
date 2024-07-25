using bank.Models;
using bank.Models.Interface;
using Microsoft.AspNetCore.Mvc;

namespace bank.Controllers
{
    public class CurrencyController : Controller
    {

        private readonly ICurrency _currency;
        public CurrencyController(ICurrency currency)
        {
            _currency = currency;

        }


        public async Task<IActionResult> Index()
        {
            var currencyViewModels = await _currency.GetAllViewModelsAsync();

            return View(currencyViewModels);
        }

        [HttpPost]
        public IActionResult Create(IFormCollection frm)
        {
            var idString = frm["edit_id"].ToString();
            var currency_name = frm["currency_name"].ToString();
            if (!string.IsNullOrEmpty(idString) && Guid.TryParse(idString, out Guid id))
            {
                //_currency.Edit(id, dealer_name, party_id);
            }
            else
            {
                _currency.Model.currency_name = currency_name;
                _currency.Create();
            }

            return RedirectToAction("Index");
        }
    }
}
