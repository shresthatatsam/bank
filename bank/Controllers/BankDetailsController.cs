using bank.Models.Interface;
using Microsoft.AspNetCore.Mvc;

namespace bank.Controllers
{
    public class BankDetailsController : Controller
    {
        private readonly IBankDetails _bankdetails;
        public BankDetailsController(IBankDetails bankdetails)
        {
            _bankdetails = bankdetails;

        }

        public async Task<IActionResult> Index()
        {
            var GetCurrencyName = _bankdetails.GetCurrencyName();
            ViewBag.GetCurrencyName = GetCurrencyName;
            var GetAllViewModels = await _bankdetails.GetAllViewModelsAsync();

            return View(GetAllViewModels);
        }

        [HttpPost]
        public IActionResult Create(IFormCollection frm)
        {
            var idString = frm["edit_id"].ToString();
            var bank_name = frm["bank_name"].ToString();
            var account_number = frm["account_number"].ToString();

            var currency_idstring = frm["currency_id"].ToString();

            if (!Guid.TryParse(currency_idstring, out Guid currency_id))
            {
                throw new ArgumentException("Invalid user_id format");
            }



            if (!string.IsNullOrEmpty(idString) && Guid.TryParse(idString, out Guid id))
            {
                _bankdetails.Edit(id, bank_name, account_number, currency_id);
            }
            else
            {
                _bankdetails.Model.bank_name = bank_name;
                _bankdetails.Model.account_number = account_number;
                _bankdetails.Model.currency_id = currency_id;
                _bankdetails.Create();
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public bool Delete(Guid id)
        {
            _bankdetails.Delete(id);

            return true;
        }
      
    }
}
