namespace bank.Models
{
    public class ModeOfDealViewModel
    {
        public Guid mod_id { get; set; }
        public string deal_name { get; set; }

        public ICollection<ForexButSellDealsViewModel> ForexBuySellDeals { get; set; }
    }
}
