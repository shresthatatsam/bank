using bank.Models.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Web.Mvc;

namespace bank.Models
{
    public class GroupName : IGroupName
    {
        public GroupNameViewModel Model { get; set; }


        public readonly ApplicationDbContext _context;

        public GroupName(ApplicationDbContext context)
        {
            Model = new GroupNameViewModel();
            _context = context;
        }


       
        public async Task<List<GroupNameViewModel>> GetAllViewModelsAsync()
        {
           
            var entities = await _context.GroupNames.ToListAsync();
            var viewModels = entities.Where(entity=>entity.is_active == true).Select(entity => new GroupNameViewModel
            {
                Id = entity.Id,
                group_name = entity.group_name,
                created_date = entity.created_date,
                is_active = entity.is_active,
            }).ToList();

            return viewModels;
        }


        public IActionResult Create()
        {
            _context.GroupNames.Add(Model);
            _context.SaveChanges();
            return new OkResult();
        }

      
        public IActionResult Edit(Guid id, string newName, bool is_active)
        {
            var group = _context.GroupNames.FirstOrDefault(g =>g.Id == id);

            if (group == null)
            {
                return null; 
            }

            group.group_name = newName;
            group.created_date = DateTime.Now.ToString();
            group.is_active = Convert.ToBoolean(is_active);

            _context.GroupNames.Update(group);
            _context.SaveChanges();

            return new OkResult();
        }

        public IActionResult Delete(Guid id)
        {
            var group = _context.GroupNames.FirstOrDefault(g => g.Id == id);
                group.is_active = false;
            _context.GroupNames.Update(group);
            _context.SaveChanges();
            return new OkResult();
        }
    }


    public class GroupNameViewModel
    {
        public Guid Id { get; set; }
        public string group_name { get; set; }
        public string? created_date { get; set; }
        public bool? is_active { get; set; }
        public ICollection<UserGroupViewModel> UserGroups { get; set; }
        public ICollection<MessageRecipentViewModel> MessageRecipents { get; set; }

    }
}
