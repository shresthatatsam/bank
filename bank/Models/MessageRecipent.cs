

namespace bank.Models
{
    public class MessageRecipentViewModel
    {
        public Guid Id { get; set; }
 

        //foreignkey properties
        public Guid recipent_group_id { get; set; }
        public Guid message_info_id { get; set; }
        public Guid recipent_id { get; set; }

        // Navigation properties 
        public virtual UserInformationViewModel userInformation { get; set; }
        public virtual GroupNameViewModel groupName { get; set; }
        public virtual MessageInfoViewModel messageInfo { get; set; }
      
    }
}
