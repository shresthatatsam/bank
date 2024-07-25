using bank.Models;
using bank.Models.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using System.Web.Mvc;

namespace bank.Models
{

    public class UserGroup : IUserGroup
    {
        public UserGroupViewModel Model { get; set; }
        public readonly ApplicationDbContext _context;

        public UserGroup(ApplicationDbContext context)
        {
            Model = new UserGroupViewModel();
            _context = context;
        }


        public async Task<List<UserGroupViewModel>> GetAllViewModelsAsync()
        {
            var entities = await _context.UserGroups
                .Include(ug => ug.userInformation)
                .Include(ug => ug.groupName)
                .ToListAsync();

            var viewModels = entities
     .Where(entity => entity.is_active == true) // Check for "true" string
     .Select(entity => new UserGroupViewModel
     {
         Id = entity.Id,
         created_date = entity.created_date,
         is_active =entity.is_active, // Convert string to boolean
         user_id = entity.user_id,
         group_id = entity.group_id,
         userInformation = new UserInformationViewModel
         {
             user_id = entity.userInformation.user_id,
             user_name = entity.userInformation.user_name,
             password = entity.userInformation.password,
             remarks = entity.userInformation.remarks,
             is_active = entity.userInformation.is_active,
             // Assuming other properties need mapping
         },
         groupName = new GroupNameViewModel
         {
             Id = entity.groupName.Id,
             group_name = entity.groupName.group_name,
             created_date = entity.groupName.created_date,
             is_active = entity.groupName.is_active,
             // Assuming other properties need mapping
         }
     }).ToList();


            return viewModels;
        }




        public List<UserInformationViewModel> GetUserItems()
        {
            List<UserInformationViewModel> users = _context.UserInformations.ToList();

            List<UserInformationViewModel> userItems = users.Select(u => new UserInformationViewModel
            {
                user_id = u.user_id,
                user_name = u.user_name
            }).ToList();

            userItems.Insert(0, new UserInformationViewModel { user_id = Guid.Empty, user_name = "Select User" });

            return userItems;
        }

        public List<GroupNameViewModel> GetGroupItems()
        {
            List<GroupNameViewModel> group = _context.GroupNames.ToList();

            List<GroupNameViewModel> groupItems = group.Select(u => new GroupNameViewModel
            {
                Id = u.Id,
                group_name = u.group_name
            }).ToList();

            groupItems.Insert(0, new GroupNameViewModel { Id = Guid.Empty, group_name = "Select User" });

            return groupItems;
        }



        public IActionResult Create()
        {
            _context.UserGroups.Add(Model);
            _context.SaveChanges();
            return new OkResult();
        }


        public IActionResult Edit(Guid id, Guid newNameId, Guid newGroupId, bool is_active)
        {
            var usergroup = _context.UserGroups.FirstOrDefault(g => g.Id == id);

            if (usergroup == null)
            {
                return null;
            }

            usergroup.group_id = newGroupId;
            usergroup.user_id = newNameId;
            usergroup.created_date = DateTime.Now.ToString();
            usergroup.is_active = Convert.ToBoolean(is_active);

            _context.UserGroups.Update(usergroup);
            _context.SaveChanges();

            return new OkResult();
        }

        public IActionResult Delete(Guid id)
        {
            var group = _context.UserGroups.FirstOrDefault(g => g.Id == id);
            group.is_active = false;
            _context.UserGroups.Update(group);
            _context.SaveChanges();
            return new OkResult();
        }


    }

    public class UserGroupViewModel
    {
        public Guid Id { get; set; }
        public string created_date { get; set; }
        public bool? is_active { get; set; }

        //foreignkey properties
        public Guid user_id { get; set; }
        public Guid group_id { get; set; }

        // Navigation properties 
        public virtual UserInformationViewModel userInformation { get; set; }
        public virtual GroupNameViewModel groupName { get; set; }


    }
}
