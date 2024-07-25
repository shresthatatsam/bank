using bank.Models.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Web.Helpers;

namespace bank.Models
{

    public class ForexButSellDeals : IForexButSellDeals
    {
        public ForexButSellDealsViewModel Model { get; set; }
        public readonly ApplicationDbContext _context;

        private readonly IHttpContextAccessor _httpContextAccessor;


        public ForexButSellDeals(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            Model = new ForexButSellDealsViewModel();
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            
        }


        //public List<PartyViewModel> GetPartyItems()
        //{
        //    List<PartyViewModel> party = _context.parties.ToList();

        //    List<PartyViewModel> partyItems = party.Select(u => new PartyViewModel
        //    {
        //        party_id = u.party_id,
        //        party_name = u.party_name,

        //    }).ToList();

        //    partyItems.Insert(0, new PartyViewModel { party_id = Guid.Empty, party_name = "Select User" });

        //    return partyItems;
        //}

        public List<UserInformationViewModel> GetUserDetails()
        {
            List<UserInformationViewModel> userinfo = _context.UserInformations.ToList();

            List<UserInformationViewModel> userinfoItems = userinfo.Select(u => new UserInformationViewModel
            {
                user_id = u.user_id,
                user_name = u.user_name,
               
            }).ToList();

            userinfoItems.Insert(0, new UserInformationViewModel { user_id = Guid.Empty, user_name = "Select code"});

            return userinfoItems;
        }


        public List<FiscalYearViewModel> GetFiscalYear()
        {
            List<FiscalYearViewModel> fiscalyear = _context.fiscalyear.ToList();

            List<FiscalYearViewModel> fiscalyearItems = fiscalyear.Select(u => new FiscalYearViewModel
            {
                fiscal_year_id = u.fiscal_year_id,
                start_date = u.start_date,
                end_date = u.end_date,
                fs_code = u.fs_code,
                fs_year = u.fs_year,
            }).ToList();

            fiscalyearItems.Insert(0, new FiscalYearViewModel { fiscal_year_id = Guid.Empty, fs_code = "Select code" , fs_year ="select year" });

            return fiscalyearItems;
        }

        public List<DealerViewModel> GetDealer()
        {
            List<DealerViewModel> dealer = _context.dealers.ToList();

            List<DealerViewModel> dealerItems = dealer.Select(u => new DealerViewModel
            {
                dealer_id = u.dealer_id,    
                dealer_name = u.dealer_name,

            }).ToList();

            dealerItems.Insert(0, new DealerViewModel { dealer_id = Guid.Empty, dealer_name = "Select dealer name"});

            return dealerItems;
        }

        public List<CurrencyViewModel> GetCurrencyDetails()
        {
            List<CurrencyViewModel> currency = _context.currencies.ToList();

            List<CurrencyViewModel> currencyItems = currency.Select(u => new CurrencyViewModel
            {
                currency_id = u.currency_id,
                currency_name = u.currency_name,

            }).ToList();

            currencyItems.Insert(0, new CurrencyViewModel { currency_id = Guid.Empty, currency_name = "Select currency name" });

            return currencyItems;
        }

        public List<ModeOfDealViewModel> GetModeOfDeal()
        {
            List<ModeOfDealViewModel> modeofdeal = _context.modeofdeals.ToList();

            List<ModeOfDealViewModel> modeofdealItems = modeofdeal.Select(u => new ModeOfDealViewModel
            {
                mod_id = u.mod_id,
                deal_name = u.deal_name,

            }).ToList();

            modeofdealItems.Insert(0, new ModeOfDealViewModel { mod_id = Guid.Empty, deal_name = "Select deal name" });

            return modeofdealItems;
        }


        public List<BankDetailsViewModelDto> GetBankDetails()
        {
            List<BankDetailsViewModel> bankdetail = _context.bankdetails
        .Include(b => b.currency) 
        .ToList();

            List<BankDetailsViewModelDto> bankdetailItems = bankdetail.Select(u => new BankDetailsViewModelDto
            {
                bank_id = u.bank_id,
                bank_name = u.bank_name,
                currency_name = u.currency.currency_name,
                currency_id = u.currency_id,    

            }).ToList();

            bankdetailItems.Insert(0, new BankDetailsViewModelDto { bank_id = Guid.Empty, bank_name = "Select bank name" });

            return bankdetailItems;
        }




        public IActionResult Create()
        {
            _context.forexbutselldeals.Add(Model);
            _context.SaveChanges();
            return new OkResult();
        }

     public IActionResult UpdateAuthorizer(Guid id)
        {
            var forexDetail = _context.forexbutselldeals.FirstOrDefault(f => f.forex_id == id);
            var userIdString = _httpContextAccessor.HttpContext?.Session.GetString("UserId");
            if (!Guid.TryParse(userIdString, out Guid user_id))
            {
                throw new ArgumentException("Invalid user_id format");
            }
            var role = _context.UserInformations.Where(x=>x.user_id == user_id).Select(x => x.role).FirstOrDefault();
            if (role == "CREATOR")
            {
                
            }
            else if (role == "AUTHORIZER")
            {
                forexDetail.authorized_by = user_id;
            _context.forexbutselldeals.Update(forexDetail);
            }
            else if(role == "MID_OFFICE_VERIFIER")
            {
                forexDetail.mid_office_id = user_id;
                forexDetail.mid_office_date_time = DateTime.Now;    
                _context.forexbutselldeals.Update(forexDetail);
            }
            else if (role == "BACK_OFFICE_AUTHORIZER")
            {
                forexDetail.back_office_id = user_id;
                _context.forexbutselldeals.Update(forexDetail);
            }
            // Save the changes to the database
            _context.SaveChanges();
            return new OkResult();
        }

        public async Task<List<ForexButSellDealsViewModelDto>> GetAllViewModelsAsync()
        {
            var entities = await _context.forexbutselldeals
                .ToListAsync();

            var viewModels = entities
     .Select(x => new ForexButSellDealsViewModelDto
     {
         forex_id = x.forex_id,
         dealer_id = x.dealer_id,
         counter_party_dealer_name =  x.counter_party_dealer_name,
         our_dealer_name=x.our_dealer_name,
         buyer_currency_amount = x.buyer_currency_amount,
         seller_currency_amount =x.seller_currency_amount,
         exchange_rate = x.exchange_rate,
         deal_date_time = x.deal_date_time,
         mid_office_date_time =x.mid_office_date_time,  
         value_date_time=x.value_date_time,
         seller_banker_id = x.seller_banker_id,
         buyer_banker_id=x.buyer_banker_id,
         buyer_send_bank_id= x.buyer_send_bank_id,
         authorized_by = x.authorized_by,
         authorized_name = _context.UserInformations.Where(y => y.user_id == x.authorized_by).Select(x=>x.user_name).FirstOrDefault(),

         mid_authorized_name = _context.UserInformations.Where(y => y.user_id == x.mid_office_id).Select(x => x.user_name).FirstOrDefault(),

         back_authorized_name = _context.UserInformations.Where(y => y.user_id == x.back_office_id).Select(x => x.user_name).FirstOrDefault(),


     }).ToList();


            return viewModels;
        }


        public async Task<List<ForexButSellDealsViewModel>> GetDataById(Guid id)
        {
            var entities = await _context.forexbutselldeals
                .ToListAsync();

            var viewModels = entities.Where(x=>x.forex_id == id)
     .Select(x => new ForexButSellDealsViewModel
     {
         forex_id = x.forex_id,
         dealer_id = x.dealer_id,
         counter_party_dealer_name = x.counter_party_dealer_name,
         our_dealer_name = x.our_dealer_name,
         buyer_currency_amount = x.buyer_currency_amount,
         seller_currency_amount = x.seller_currency_amount,
         exchange_rate = x.exchange_rate,
         deal_date_time = x.deal_date_time,
         mid_office_date_time = x.mid_office_date_time,
         value_date_time = x.value_date_time,
         seller_banker_id = x.seller_banker_id,
         buyer_banker_id = x.buyer_banker_id,
         buyer_send_bank_id = x.buyer_send_bank_id,
         fiscal_year_id = x.fiscal_year_id,
         party_id=x.party_id,
         back_office_remarks = x.back_office_remarks,
         mid_office_remarks=x.mid_office_remarks,
         mod_id=x.mod_id,
         




     }).ToList();


            return (viewModels);
        }


    }




    public class ForexButSellDealsViewModelDto
    {
        public Guid forex_id { get; set; }
        public string reference_no { get; set; }
        public string counter_party_dealer_name { get; set; }
        public string our_dealer_name { get; set; }
        public decimal buyer_currency_amount { get; set; }
        public decimal seller_currency_amount { get; set; }
        public decimal exchange_rate { get; set; }
        public string authorized_name { get; set; }
        public string mid_authorized_name { get; set; }
        public string back_authorized_name { get; set; }
        public Guid? buyer_banker_id { get; set; }
        public Guid? seller_banker_id { get; set; }

        public Guid? buyer_send_bank_id { get; set; }
        public DateTime? deal_date_time { get; set; }

        public DateTime? value_date_time { get; set; }

        public Guid creator { get; set; }
        public string dealer_signature { get; set; }
        public Guid? verified_by { get; set; }
        public string verified_signature { get; set; }
        public Guid? authorized_by { get; set; }
        public string authorized_signature { get; set; }
        public Guid? mid_office_id { get; set; }

        public string mid_office_signature { get; set; }

        public DateTime? mid_office_date_time { get; set; }

        public string mid_office_remarks { get; set; }

        public Guid? back_office_id { get; set; }
        public string back_office_signature { get; set; }
        public string back_office_remarks { get; set; }



        public virtual UserInformationViewModel userinformation { get; set; }

        public Guid dealer_id { get; set; }
        public virtual DealerViewModel Dealer { get; set; }
        public Guid fiscal_year_id { get; set; }
        public virtual FiscalYearViewModel FiscalYear { get; set; }
        public Guid buyer_currency_id { get; set; }
        public Guid seller_currency_id { get; set; }
        public virtual CurrencyViewModel currency { get; set; }


        public Guid party_id { get; set; }

        public virtual PartyViewModel party { get; set; }
        public Guid mod_id { get; set; }
        public virtual ModeOfDealViewModel modeofdeal { get; set; }
    }




    public class ForexButSellDealsViewModel
    {
        public Guid forex_id {  get; set; }
        public string reference_no { get; set; }
        public string counter_party_dealer_name { get; set; }
        public string our_dealer_name { get; set; }
        public decimal buyer_currency_amount { get; set; }
        public decimal seller_currency_amount { get; set; }
        public decimal exchange_rate { get; set; }

        public Guid? buyer_banker_id { get; set; }
        public Guid? seller_banker_id { get; set; }

        public Guid? buyer_send_bank_id { get; set; }
        public DateTime? deal_date_time { get; set; }

        public DateTime? value_date_time { get; set; }

        public Guid creator {  get; set; }  
        public string dealer_signature {  get; set; }   
        public Guid? verified_by { get; set; }
        public string verified_signature {  get; set; } 
        public Guid? authorized_by { get; set; }
        public string authorized_signature { get; set; }
        public Guid? mid_office_id { get; set; }

        public string mid_office_signature { get; set; }

        public DateTime? mid_office_date_time { get; set; }

        public string mid_office_remarks { get; set; }

        public Guid? back_office_id { get; set; }
        public string back_office_signature { get; set; }
        public string back_office_remarks { get; set; }



        public virtual UserInformationViewModel userinformation { get; set; }

        public Guid dealer_id { get; set; }
        public virtual DealerViewModel Dealer { get; set; }
        public Guid fiscal_year_id { get; set; }
        public virtual FiscalYearViewModel FiscalYear { get; set; }
        public Guid buyer_currency_id { get; set; }
        public Guid seller_currency_id { get; set; }
        public virtual CurrencyViewModel currency { get; set; }


        public Guid party_id { get; set; }

        public virtual PartyViewModel party { get; set; }
        public Guid mod_id { get;  set; }
        public virtual ModeOfDealViewModel modeofdeal { get; set; }
    }
}
