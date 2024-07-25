namespace bank.Models
{
    public class PartyBankDetailsViewModel
    {
        public Guid party_bank_id { get; set; }

        //foreignkey properties
        public Guid party_id { get; set; }
        public Guid bank_id { get; set; }

        // Navigation properties 
        public virtual PartyViewModel party { get; set; }

        public virtual BankDetailsViewModel bank { get; set; }
    }
}
