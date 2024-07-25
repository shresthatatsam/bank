using bank.Models;
using bank.Models.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace bank.Controllers
{
    public class ForexBuySellDealsController : Controller
    {
        private readonly IForexButSellDeals  _forexbuyselleals;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IDealer _dealer;
        public readonly ApplicationDbContext _context;
        public ForexBuySellDealsController(IForexButSellDeals forexbuyselleals, IDealer dealer , IHttpContextAccessor httpContextAccessor, ApplicationDbContext context)
        {
            _forexbuyselleals = forexbuyselleals;
            _dealer = dealer;
            _httpContextAccessor = httpContextAccessor;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var partyItems = _dealer.GetPartyItems();
            ViewBag.PartyItems = partyItems;
            var fiscalyearItems = _forexbuyselleals.GetFiscalYear();
            ViewBag.fiscalyearItems = fiscalyearItems;
            var dealerItems = _forexbuyselleals.GetDealer();
            ViewBag.dealerItems = dealerItems;
            var CurrencyItems = _forexbuyselleals.GetCurrencyDetails();
            ViewBag.CurrencyItems = CurrencyItems;
            var ModeOfDealItems = _forexbuyselleals.GetModeOfDeal();
            ViewBag.ModeOfDealItems = ModeOfDealItems;
            var BankDetailsItems = _forexbuyselleals.GetBankDetails();
            ViewBag.BankDetailsItems = BankDetailsItems;

            var getviewmodel = await _forexbuyselleals.GetAllViewModelsAsync();

            return View(getviewmodel);
        }

        public async Task<IActionResult> CreateIndex()
        {
            var partyItems = _dealer.GetPartyItems();
            ViewBag.PartyItems = partyItems;
            var fiscalyearItems = _forexbuyselleals.GetFiscalYear();
            ViewBag.fiscalyearItems = fiscalyearItems;
            var dealerItems = _forexbuyselleals.GetDealer();
            ViewBag.dealerItems = dealerItems;
            var CurrencyItems = _forexbuyselleals.GetCurrencyDetails();
            ViewBag.CurrencyItems = CurrencyItems;
            var ModeOfDealItems = _forexbuyselleals.GetModeOfDeal();
            ViewBag.ModeOfDealItems = ModeOfDealItems;
            var BankDetailsItems = _forexbuyselleals.GetBankDetails();
            ViewBag.BankDetailsItems = BankDetailsItems;

            var getviewmodel = await _forexbuyselleals.GetAllViewModelsAsync();

            return View(getviewmodel);
        }

        public async Task<IActionResult> getalldata(Guid id)
        {
            var getviewmodel = await _forexbuyselleals.GetDataById(id);
            return Json(getviewmodel);
        }

        //    [HttpPost]
        public IActionResult Create(IFormCollection frm)
        {
            var idString = frm["edit_id"].ToString();


            var userIdString = _httpContextAccessor.HttpContext?.Session.GetString("UserId");
            if (!Guid.TryParse(userIdString, out Guid user_id))
            {
                throw new ArgumentException("Invalid user_id format");
            }
             
           var role = _context.UserInformations.Where(x => x.user_id == user_id).Select(x => x.role).FirstOrDefault();

            if(role == "CREATOR")
            {
                if (!string.IsNullOrEmpty(idString) && Guid.TryParse(idString, out Guid id))
                {
                    var forexdetails = _context.forexbutselldeals.Where(g => g.forex_id == id).FirstOrDefault();

                    forexdetails.counter_party_dealer_name = GetFormValueOrDefault(frm, "counter_party_dealer_name", forexdetails.counter_party_dealer_name);
                    forexdetails.our_dealer_name = GetFormValueOrDefault(frm, "our_dealer_name", forexdetails.our_dealer_name);
                    forexdetails.buyer_currency_amount = GetDecimalValueOrDefault(frm, "buyer_currency_amount", forexdetails.buyer_currency_amount);
                    forexdetails.seller_currency_amount = GetDecimalValueOrDefault(frm, "seller_currency_amount", forexdetails.seller_currency_amount);
                    forexdetails.exchange_rate = GetDecimalValueOrDefault(frm, "exchange_rate", forexdetails.exchange_rate);
                    forexdetails.creator = user_id;
                    forexdetails.buyer_banker_id = GetNullableGuidValueOrDefault(frm, "buyer_banker_id", forexdetails.buyer_banker_id);
                    forexdetails.seller_banker_id = GetNullableGuidValueOrDefault(frm, "seller_banker_id", forexdetails.seller_banker_id);
                    forexdetails.buyer_send_bank_id = GetNullableGuidValueOrDefault(frm, "buyer_send_bank_id", forexdetails.buyer_send_bank_id);
                    forexdetails.deal_date_time = GetNullableDateTimeValueOrDefault(frm, "deal_date_time", forexdetails.deal_date_time);
                    forexdetails.value_date_time = GetNullableDateTimeValueOrDefault(frm, "value_date_time", forexdetails.value_date_time);
                    forexdetails.mid_office_date_time = GetNullableDateTimeValueOrDefault(frm, "mid_office_date_time", forexdetails.mid_office_date_time);
                    forexdetails.mid_office_remarks = GetFormValueOrDefault(frm, "mid_office_remarks", forexdetails.mid_office_remarks);
                    forexdetails.back_office_remarks = GetFormValueOrDefault(frm, "back_office_remarks", forexdetails.back_office_remarks);
                    forexdetails.dealer_id = GetGuidValueOrDefault(frm, "dealer_id", forexdetails.dealer_id);
                    forexdetails.fiscal_year_id = GetGuidValueOrDefault(frm, "fiscal_year_id", forexdetails.fiscal_year_id);
                    forexdetails.buyer_currency_id = GetGuidValueOrDefault(frm, "buyer_currency_id", forexdetails.buyer_currency_id);
                    forexdetails.seller_currency_id = GetGuidValueOrDefault(frm, "seller_currency_id", forexdetails.seller_currency_id);
                    forexdetails.party_id = GetGuidValueOrDefault(frm, "party_id", forexdetails.party_id);
                    forexdetails.mod_id = GetGuidValueOrDefault(frm, "mod_id", forexdetails.mod_id);

                    _context.forexbutselldeals.Update(forexdetails);
                    _context.SaveChanges();
                }
                else
                {



                    _forexbuyselleals.Model.reference_no = RandomStringGenerator.GenerateRandomString(3) + "_" + GenerateRandomRefNumber();
                    _forexbuyselleals.Model.counter_party_dealer_name = frm["counter_party_dealer_name"].ToString();
                    _forexbuyselleals.Model.our_dealer_name = frm["our_dealer_name"].ToString();

                    _forexbuyselleals.Model.buyer_currency_amount = Convert.ToDecimal(frm["buyer_currency_amount"].ToString());
                    _forexbuyselleals.Model.seller_currency_amount = Convert.ToDecimal(frm["seller_currency_amount"].ToString());
                    _forexbuyselleals.Model.exchange_rate = Convert.ToDecimal(frm["exchange_rate"].ToString());

                    _forexbuyselleals.Model.creator = user_id;
                    _forexbuyselleals.Model.buyer_banker_id = Guid.TryParse(frm["buyer_banker_id"].ToString(), out Guid buyer_banker_id) ? buyer_banker_id : (Guid?)null;
                    _forexbuyselleals.Model.seller_banker_id = Guid.TryParse(frm["seller_banker_id"].ToString(), out Guid seller_banker_id) ? seller_banker_id : (Guid?)null;
                    _forexbuyselleals.Model.buyer_send_bank_id = Guid.TryParse(frm["buyer_send_bank_id"].ToString(), out Guid buyer_send_bank_id) ? buyer_send_bank_id : (Guid?)null;

                    _forexbuyselleals.Model.deal_date_time = Convert.ToDateTime(frm["deal_date_time"].ToString());
                    _forexbuyselleals.Model.value_date_time = Convert.ToDateTime(frm["value_date_time"].ToString());


                    //_forexbuyselleals.Model.mid_office_id = Guid.TryParse(frm["mid_office_id"].ToString(), out Guid mid_office_id) ? mid_office_id : (Guid?)null;
                    _forexbuyselleals.Model.mid_office_date_time = Convert.ToDateTime(frm["mid_office_date_time"].ToString());
                    _forexbuyselleals.Model.mid_office_remarks = frm["mid_office_remarks"].ToString();

                    //_forexbuyselleals.Model.back_office_id = Guid.TryParse(frm["back_office_id"], out Guid back_office_id) ? back_office_id : Guid.Empty;
                    _forexbuyselleals.Model.back_office_remarks = frm["back_office_remarks"].ToString();


                    _forexbuyselleals.Model.dealer_id = Guid.TryParse(frm["dealer_id"], out Guid dealer_id) ? dealer_id : Guid.Empty;
                    _forexbuyselleals.Model.fiscal_year_id = Guid.TryParse(frm["fiscal_year_id"], out Guid fiscal_year_id) ? fiscal_year_id : Guid.Empty;
                    _forexbuyselleals.Model.buyer_currency_id = Guid.TryParse(frm["buyer_currency_id"].ToString(), out Guid buyer_currency_id) ? buyer_currency_id : Guid.Empty;

                    _forexbuyselleals.Model.seller_currency_id = Guid.TryParse(frm["seller_currency_id"].ToString(), out Guid seller_currency_id) ? seller_currency_id : Guid.Empty;
                    _forexbuyselleals.Model.party_id = Guid.TryParse(frm["party_id"], out Guid party_id) ? party_id : Guid.Empty;
                    _forexbuyselleals.Model.mod_id = Guid.TryParse(frm["mod_id"], out Guid mod_id) ? mod_id : Guid.Empty;
                    _forexbuyselleals.Create();
                }

            }
                return RedirectToAction("Index");


          
        }

        [HttpPost]
        public IActionResult updateAuthorizer(Guid id)
        {

            _forexbuyselleals.UpdateAuthorizer(id);

            return Json("success");

        }


        private string GenerateRandomRefNumber()
        {
            Random random = new Random();
            int randomNumber = random.Next(100, 1000);
            return randomNumber.ToString();
        }


        public class RandomStringGenerator
        {
            private static Random random = new Random();
            private const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

            public static string GenerateRandomString(int length)
            {
                return new string(Enumerable.Repeat(chars, length)
                  .Select(s => s[random.Next(s.Length)]).ToArray());
            }
        }


        private string GetFormValueOrDefault(IFormCollection frm, string key, string currentValue)
        {
            return !string.IsNullOrEmpty(frm[key].ToString()) ? frm[key].ToString() : currentValue;
        }

        private decimal GetDecimalValueOrDefault(IFormCollection frm, string key, decimal currentValue)
        {
            return decimal.TryParse(frm[key].ToString(), out decimal result) ? result : currentValue;
        }

        private DateTime? GetNullableDateTimeValueOrDefault(IFormCollection frm, string key, DateTime? currentValue)
        {
            return DateTime.TryParse(frm[key].ToString(), out DateTime result) ? (DateTime?)result : currentValue;
        }

        private Guid? GetNullableGuidValueOrDefault(IFormCollection frm, string key, Guid? currentValue)
        {
            return Guid.TryParse(frm[key].ToString(), out Guid result) ? result : currentValue;
        }

        private Guid GetGuidValueOrDefault(IFormCollection frm, string key, Guid currentValue)
        {
            return Guid.TryParse(frm[key].ToString(), out Guid result) ? result : currentValue;
        }

    }
}
