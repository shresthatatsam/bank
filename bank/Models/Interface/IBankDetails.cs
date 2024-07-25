using Microsoft.AspNetCore.Mvc;

namespace bank.Models.Interface
{
    public interface IBankDetails
    {
        public BankDetailsViewModel Model { get; set; }
        Task<List<BankDetailsViewModel>> GetAllViewModelsAsync();
        IActionResult Create();
        List<CurrencyViewModel> GetCurrencyName();
        IActionResult Delete(Guid id);
        IActionResult Edit(Guid id, string bank_name, string account_number, Guid currency_id);
    }
}
