using Microsoft.AspNetCore.Mvc;

namespace bank.Models.Interface
{
    public interface IMessageInfo
    {
        MessageInfoViewModel Model { get; set; }
        Task<List<MessageInfoViewModel>> GetAllViewModelsAsync();
        IActionResult Edit(Guid id, string message_body, string subject,string parent_message_id);
        IActionResult Create();
        IActionResult Delete(Guid id);

        List<MessageInfoViewModel> GetMessageItems();
    }
}
