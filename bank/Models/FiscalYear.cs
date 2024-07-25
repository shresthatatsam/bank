namespace bank.Models
{
    public class FiscalYearViewModel
    {
        public Guid fiscal_year_id { get; set; }
        public string start_date { get; set; }
        public string end_date { get; set;}
        public string fs_code { get; set; }
        public string fs_year { get; set;}


        public ICollection<ForexButSellDealsViewModel> ForexBuySellDeals { get; set; }

    }
}
