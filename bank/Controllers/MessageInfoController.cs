using bank.Models;
using bank.Models.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace bank.Controllers
{
    //[Authorize]
    public class MessageInfoController : Controller
    {

        private readonly IMessageInfo _messageinfo;
        public MessageInfoController(IMessageInfo messageinfo)
        {
           _messageinfo = messageinfo;

        }
        public async Task<IActionResult> Index()
        {
            var messageItems = _messageinfo.GetMessageItems();
            ViewBag.MessageItems = messageItems;
            var userGroupViewModels = await _messageinfo.GetAllViewModelsAsync();

            return View(userGroupViewModels);
           
        }

        [HttpPost]
        public IActionResult Create(IFormCollection frm)
        {
            var idString = frm["edit_id"].ToString();
            var subject = frm["subject"].ToString();
            var message_body = frm["message_body"].ToString();
            var parent_message_id = frm["parent_messageid"].ToString();
            //Guid.TryParse(parent_message_idstring, out Guid parent_message_id);
            if (!string.IsNullOrEmpty(idString) && Guid.TryParse(idString, out Guid id))
            {
                _messageinfo.Edit(id, message_body,subject, parent_message_id);
            }
            else
            {
                _messageinfo.Model.message_body = message_body;
                _messageinfo.Model.created_date = DateTime.Now.ToString();
                _messageinfo.Model.subject = subject;
                _messageinfo.Model.parent_message_id = parent_message_id ?? null;
                _messageinfo.Create();
            }

            return RedirectToAction("Index");
        }


        [HttpPost]
        public bool Delete(Guid id)
        {
            _messageinfo.Delete(id);

            return true;
        }


    }
}
