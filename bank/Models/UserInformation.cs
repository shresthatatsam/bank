using bank.Models.Interface;
using Microsoft.AspNetCore.Mvc;

namespace bank.Models
{
    public class UserInformation :IUserInformation
    {
       public UserInformationViewModel Model { get; set; }
        public readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserInformation(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor) 
        {
        Model = new UserInformationViewModel();
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public IActionResult Register()
        {
            _context.UserInformations.Add(Model);
            _context.SaveChanges();
            return new OkResult();
        }





        public bool Login(string userName, string password)
        {
            var user = _context.UserInformations
                .FirstOrDefault(u => u.user_name == userName && u.password == password);

            if (user!=null)
            {
                var userId = user.user_id;
                var user_Name = user.user_name;
                _httpContextAccessor.HttpContext.Session.SetString("UserId", userId.ToString());
                _httpContextAccessor.HttpContext.Session.SetString("UserName", user_Name.ToString());
                return true;
            }
           return false;
        }

    }

    public class UserInformationViewModel
    {
        public Guid user_id { get; set; }
        public string user_name { get; set; }
        public string password { get; set; }
        public string remarks { get; set; }
        public bool? is_active { get; set; }

        public string role {  get; set; }   
        public ICollection<UserGroupViewModel> UserGroups { get; set; }

        public ICollection<MessageRecipentViewModel> MessageRecipents { get; set; }

        public ICollection<ForexButSellDealsViewModel> ForexBuySellDeals { get; set; }
    }
}
