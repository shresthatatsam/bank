using Microsoft.AspNetCore.Mvc;

namespace bank.Models.Interface
{
    public interface IParty
    {
        public PartyViewModel Model { get; set; }
        IActionResult Create();
        IActionResult Edit(Guid id, string party_name, string remarks);
        Task<List<PartyViewModel>> GetAllViewModelsAsync();
        IActionResult Delete(Guid id);
    }
}
