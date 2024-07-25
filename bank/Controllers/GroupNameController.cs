using bank.Models;
using bank.Models.Interface;
using Microsoft.AspNetCore.Mvc;
using System;

namespace bank.Controllers
{
    public class GroupNameController : Controller
    {
        private readonly IGroupName _groupname;
        public GroupNameController(IGroupName groupname)
        {
            _groupname = groupname;

        }

        public async Task<IActionResult> Index()
        {
            var groupNames = await _groupname.GetAllViewModelsAsync();
            return View(groupNames);
        }


        [HttpPost]
        public IActionResult Create(IFormCollection frm)
        {
            var idString = frm["edit_id"].ToString();
            var group_name =frm["group_name"].ToString();
            var is_active = Convert.ToBoolean(frm["is_active"]);
           
            if (idString != string.Empty)
            {
                if (Guid.TryParse(idString, out Guid id))
                {
                    _groupname.Edit(id, group_name, is_active);
                }
            }
            else
            {

                _groupname.Model.group_name = group_name;
                _groupname.Model.created_date = DateTime.Now.ToString();
                _groupname.Model.is_active = true;
                _groupname.Create();
            }
            
            return RedirectToAction("Index");
        }


        [HttpPost]
        public bool Delete(Guid id)
        {
            _groupname.Delete(id);

            return true;
            //return RedirectToAction("Index");
        }

    }
}
