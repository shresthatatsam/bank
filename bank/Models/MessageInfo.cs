using bank.Models.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace bank.Models
{

    public class MessageInfo : IMessageInfo
    {
        public MessageInfoViewModel Model { get; set; }
        public readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public MessageInfo(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            Model = new MessageInfoViewModel();
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }


        public async Task<List<MessageInfoViewModel>> GetAllViewModelsAsync()
        {
            var entities = await _context.MessageInfos
                .Include(ug => ug.MessageRecipents)
                .ToListAsync();

            var viewModels = entities
                .Select(entity => new MessageInfoViewModel
                {
                    Id = entity.Id,
                    created_date = entity.created_date,
                    subject = entity.subject,
                    message_body = entity.message_body,
                    creator_id = entity.creator_id,    
                    parent_message_id = entity.parent_message_id,
                }).ToList();
         return viewModels;
        }


        public IActionResult Create()
        {
            _context.MessageInfos.Add(Model);
            _context.SaveChanges();
            return new OkResult();
        }


        public IActionResult Edit(Guid id, string message_body, string subject, string parent_message_id)
        {
            var userIdString = _httpContextAccessor.HttpContext?.Session.GetString("UserId");
            if (!Guid.TryParse(userIdString, out Guid user_id))
            {
                throw new ArgumentException("Invalid user_id format");
            }
            var MessageInfo = _context.MessageInfos.FirstOrDefault(g => g.Id == id);

            if (MessageInfo == null)
            {
                return null;
            }

            MessageInfo.message_body = message_body;
            MessageInfo.subject = subject;
            MessageInfo.created_date = DateTime.Now.ToString();
            MessageInfo.creator_id = user_id;
            MessageInfo.parent_message_id = parent_message_id;
            _context.MessageInfos.Update(MessageInfo);
            _context.SaveChanges();

            return new OkResult();
        }

        public IActionResult Delete(Guid id)
        {
            var group = _context.MessageInfos.FirstOrDefault(g => g.Id == id);
            _context.MessageInfos.Remove(group);
            _context.SaveChanges();
            return new OkResult();
        }


        public List<MessageInfoViewModel> GetMessageItems()
        {
            List<MessageInfoViewModel> message = _context.MessageInfos.ToList();

            List<MessageInfoViewModel> messageItems = message.Select(u => new MessageInfoViewModel
            {
                Id = u.Id,
                subject = u.subject
            }).ToList();

            messageItems.Insert(0, new MessageInfoViewModel { Id = Guid.Empty, subject = "Select User" });

            return messageItems;
        }


    }


    public class MessageInfoViewModel
    {
        public Guid Id { get; set; }
        public string subject { get; set; }
        public string message_body { get; set; }
        public Guid creator_id { get; set; }
        public string created_date { get; set; }
        public string parent_message_id { get; set; }
        public ICollection<MessageRecipentViewModel> MessageRecipents { get; set; }
    }
}
