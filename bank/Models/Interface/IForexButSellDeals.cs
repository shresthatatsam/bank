using Microsoft.AspNetCore.Mvc;

namespace bank.Models.Interface
{
    public interface IForexButSellDeals
    {
        public ForexButSellDealsViewModel Model { get; set; }
        //List<PartyViewModel> GetPartyItems();
        List<FiscalYearViewModel> GetFiscalYear();
        List<DealerViewModel> GetDealer();
        List<CurrencyViewModel> GetCurrencyDetails();
        List<ModeOfDealViewModel> GetModeOfDeal();
        IActionResult Create();
        List<BankDetailsViewModelDto> GetBankDetails();
        //Task<List<ForexButSellDealsViewModel>> GetAllViewModelsAsync();
        Task<List<ForexButSellDealsViewModelDto>> GetAllViewModelsAsync();
        Task<List<ForexButSellDealsViewModel>> GetDataById(Guid id);
        //IActionResult Edit(Guid id);
        IActionResult UpdateAuthorizer(Guid id);
    }
}
