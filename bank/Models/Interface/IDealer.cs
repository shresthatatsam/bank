using Microsoft.AspNetCore.Mvc;

namespace bank.Models.Interface
{
    public interface IDealer
    {
        public DealerViewModel Model { get; set; }
        IActionResult Create();
        IActionResult Edit(Guid id, string dealer_name, Guid party_id);
        IActionResult Delete(Guid id);
        List<PartyViewModel> GetPartyItems();
        Task<List<DealerViewModelDto>> GetAllViewModelsAsync();
    }
}
