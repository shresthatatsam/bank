using Microsoft.AspNetCore.Mvc;

namespace bank.Models.Interface
{
    public interface IUserGroup
    {
        UserGroupViewModel Model { get; set; }
        Task<List<UserGroupViewModel>> GetAllViewModelsAsync();
        List<UserInformationViewModel> GetUserItems();
        List<GroupNameViewModel> GetGroupItems();
        IActionResult Create();
        IActionResult Edit(Guid id, Guid newNameId, Guid newGroupId, bool is_active);
        IActionResult Delete(Guid id);
    }
}
