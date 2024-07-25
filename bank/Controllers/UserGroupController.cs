using bank.Models;
using bank.Models.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace bank.Controllers
{
    public class UserGroupController : Controller
    {
        private readonly IUserGroup _usergroup;
        public UserGroupController(IUserGroup usergroup)
        {
            _usergroup = usergroup; 

        }


        public async Task<IActionResult> Index()
        {
            var userItems =  _usergroup.GetUserItems();
            ViewBag.UserItems = userItems;
            var groupItems = _usergroup.GetGroupItems();    
            ViewBag.GroupItems = groupItems;
            var userGroupViewModels = await _usergroup.GetAllViewModelsAsync();

            return View(userGroupViewModels);
        }

        [HttpPost]
        public IActionResult Create(IFormCollection frm)
        {
            var idString = frm["edit_id"].ToString();
            var group_idString = frm["group_name"].ToString();
            var user_idString = frm["user_Id"].ToString();
            var is_active = Convert.ToBoolean(frm["is_active"]);

            if (!Guid.TryParse(user_idString, out Guid user_id))
            {
                throw new ArgumentException("Invalid user_id format");
            }

            if (!Guid.TryParse(group_idString, out Guid group_id))
            {
             
                throw new ArgumentException("Invalid user_id format");
            }

           
            if (!string.IsNullOrEmpty(idString) && Guid.TryParse(idString, out Guid id))
            {
                _usergroup.Edit(id, user_id, group_id, is_active);
            }
            else 
            {
                _usergroup.Model.group_id = group_id;
                _usergroup.Model.created_date = DateTime.Now.ToString();
                _usergroup.Model.is_active =Convert.ToBoolean(is_active);
                _usergroup.Model.user_id = user_id;
                _usergroup.Create();
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public bool Delete(Guid id)
        {
            _usergroup.Delete(id);

            return true;
        }


    }
}
