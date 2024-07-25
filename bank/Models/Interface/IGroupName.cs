using Microsoft.AspNetCore.Mvc;

namespace bank.Models.Interface
{
    public interface IGroupName
    {
        GroupNameViewModel Model { get; set; }
        Task<List<GroupNameViewModel>> GetAllViewModelsAsync();
        IActionResult Create();
        IActionResult Edit(Guid id, string newName, bool is_active);

        IActionResult Delete(Guid id);
    }
}
