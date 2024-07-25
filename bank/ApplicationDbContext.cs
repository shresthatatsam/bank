
    using bank.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

    namespace bank
    {
        public class ApplicationDbContext : DbContext
        {
            private readonly IConfiguration _configuration;

            public ApplicationDbContext(IConfiguration configuration)
            {
                _configuration = configuration;
            }
            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                optionsBuilder.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));
            }

     

            public DbSet<UserInformationViewModel> UserInformations { get; set; }
            public DbSet<GroupNameViewModel> GroupNames { get; set; }
            public DbSet<UserGroupViewModel> UserGroups { get; set; }
            public DbSet<MessageInfoViewModel> MessageInfos { get; set; }

            public DbSet<MessageRecipentViewModel> MessageRecipents { get; set; }

        //second slide


        public DbSet<CurrencyViewModel> currencies { get; set; }

        public DbSet<BankDetailsViewModel> bankdetails { get; set; }

        public DbSet<PartyViewModel> parties { get; set; }
        public DbSet<DealerViewModel> dealers { get; set; }

        public DbSet<PartyBankDetailsViewModel> partybankdetails { get; set; }

        public DbSet<FiscalYearViewModel> fiscalyear { get; set; }

        public DbSet<ModeOfDealViewModel> modeofdeals { get; set; }

        public DbSet<ForexButSellDealsViewModel> forexbutselldeals { get; set; }


        public DbSet<RoleViewModel> roles { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
            {



                modelBuilder.Entity<UserInformationViewModel>(x =>
                {
                    x.ToTable("UserInformations");
                    x.HasKey(e => e.user_id);
                    x.Property(e => e.user_name).IsRequired();
                    x.Property(e => e.password).IsRequired();
                    x.Property(e => e.remarks).IsRequired(false);
                    x.Property(e => e.is_active).HasDefaultValue(true);
                    x.Property(e=>e.role).IsRequired(true);

                });
            modelBuilder.Entity<RoleViewModel>(x =>
            {
                x.ToTable("roles");
                x.HasKey(e => e.role_id);
                x.Property(e => e.role_name).IsRequired(true);
                x.Property(e => e.is_active).HasDefaultValue(true);

            });

            modelBuilder.Entity<GroupNameViewModel>(x =>
                {
                    x.ToTable("GroupNames");
                    x.HasKey(e => e.Id);
                    x.Property(e => e.group_name).IsRequired();
                    x.Property(e => e.created_date).IsRequired(false);
                    x.Property(e => e.is_active).HasDefaultValue(true);
                });

                modelBuilder.Entity<UserGroupViewModel>(entity =>
                {
                    entity.ToTable("UserGroups");
                    entity.HasKey(e => e.Id);
                    entity.Property(e => e.created_date).IsRequired(false);
                    entity.Property(e => e.is_active).HasDefaultValue(true);

                    entity.HasOne(ug => ug.userInformation)
                          .WithMany(u => u.UserGroups)
                          .HasForeignKey(ug => ug.user_id)
                          .OnDelete(DeleteBehavior.Restrict)
                          .IsRequired();

                    entity.HasOne(ug => ug.groupName)
                          .WithMany(g => g.UserGroups)
                          .HasForeignKey(ug => ug.group_id)
                          .OnDelete(DeleteBehavior.Restrict)
                          .IsRequired();
                });


                modelBuilder.Entity<MessageInfoViewModel>(x =>
                {
                    x.ToTable("MessageInfos");
                    x.HasKey(e => e.Id);
                    x.Property(e => e.subject).IsRequired();
                    x.Property(e => e.message_body).IsRequired(false);
                    x.Property(e => e.creator_id).IsRequired(true);
                    x.Property(e => e.created_date).IsRequired(false);
                    x.Property(e => e.parent_message_id).IsRequired(false);
                });

                modelBuilder.Entity<MessageRecipentViewModel>(entity =>
                {
                    entity.ToTable("MessageRecipents");
                    entity.HasKey(e => e.Id);

                    entity.HasOne(ug => ug.userInformation)
                          .WithMany(u => u.MessageRecipents)
                          .HasForeignKey(ug => ug.recipent_id)
                          .OnDelete(DeleteBehavior.Restrict)
                          .IsRequired();

                    entity.HasOne(ug => ug.groupName)
                          .WithMany(g => g.MessageRecipents)
                          .HasForeignKey(ug => ug.recipent_group_id)
                          .OnDelete(DeleteBehavior.Restrict)
                          .IsRequired();

                    entity.HasOne(ug => ug.messageInfo)
                        .WithMany(g => g.MessageRecipents)
                        .HasForeignKey(ug => ug.message_info_id)
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            //second slide

            modelBuilder.Entity<CurrencyViewModel>(x =>
            {
                x.ToTable("currencies");
                x.HasKey(e => e.currency_id);
                x.Property(e => e.currency_name).IsRequired();
            });

            modelBuilder.Entity<BankDetailsViewModel>(entity =>
            {
                entity.ToTable("bankdetails");
                entity.HasKey(e => e.bank_id);
                entity.Property(e => e.bank_name).IsRequired(false);
                entity.Property(e => e.account_number).IsRequired(false);

                entity.HasOne(ug => ug.currency)
                      .WithMany(u => u.bankdetails)
                      .HasForeignKey(ug => ug.currency_id)
                      .OnDelete(DeleteBehavior.Restrict)
                      .IsRequired();

            });


            modelBuilder.Entity<PartyViewModel>(x =>
            {
                x.ToTable("parties");
                x.HasKey(e => e.party_id);
                x.Property(e => e.party_name).IsRequired(false);
                x.Property(e => e.remarks).IsRequired(false);
            });

            modelBuilder.Entity<DealerViewModel>(entity =>
            {
                entity.ToTable("dealers");
                entity.HasKey(e => e.dealer_id);
                entity.Property(e => e.dealer_name).IsRequired(false);

                entity.HasOne(ug => ug.party)
                      .WithMany(u => u.dealer)
                      .HasForeignKey(ug => ug.party_id)
                      .OnDelete(DeleteBehavior.Restrict)
                      .IsRequired();

            });


            modelBuilder.Entity<PartyBankDetailsViewModel>(entity =>
            {
                entity.ToTable("partybankdetails");
                entity.HasKey(e => e.party_bank_id);

                entity.HasOne(ug => ug.party)
                      .WithMany(u => u.party_bank)
                      .HasForeignKey(ug => ug.party_id)
                      .OnDelete(DeleteBehavior.Restrict)
                      .IsRequired();

                entity.HasOne(ug => ug.bank)
                 .WithMany(u => u.party_bank)
                 .HasForeignKey(ug => ug.bank_id)
                 .OnDelete(DeleteBehavior.Restrict)
                 .IsRequired();

            });

            modelBuilder.Entity<FiscalYearViewModel>(x =>
            {
                x.ToTable("fiscalyear");
                x.HasKey(e => e.fiscal_year_id);

                x.Property(e => e.start_date).IsRequired(false);
                x.Property(e => e.end_date).IsRequired(false);
                x.Property(e => e.fs_code).IsRequired(false);
                x.Property(e => e.fs_code).IsRequired(false);
            });

            modelBuilder.Entity<ModeOfDealViewModel>(x =>
            {
                x.ToTable("modeofdeals");
                x.HasKey(e => e.mod_id);

                x.Property(e => e.deal_name).IsRequired(false);
            });

            modelBuilder.Entity<ForexButSellDealsViewModel>(x =>
            {
                x.ToTable("forexbutselldeals");
                x.HasKey(e => e.forex_id);

                x.HasOne(ug => ug.party)
               .WithMany(u => u.ForexBuySellDeals)
               .HasForeignKey(ug => ug.party_id)
               .OnDelete(DeleteBehavior.Restrict)
               .IsRequired();


                x.Property(e => e.reference_no).IsRequired(false);
                x.Property(e => e.counter_party_dealer_name).IsRequired(false);
                x.Property(e => e.our_dealer_name).IsRequired(false);
                x.Property(e => e.creator).IsRequired(true);
                x.HasOne(ug => ug.modeofdeal)
                .WithMany(u => u.ForexBuySellDeals)
                .HasForeignKey(ug => ug.mod_id)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();
                
                x.HasOne(ug => ug.currency)
                .WithMany(u => u.ForexBuySellDeals)
                .HasForeignKey(ug => ug.seller_currency_id)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

                x.HasOne(ug => ug.currency)
                .WithMany(u => u.ForexBuySellDeals)
                .HasForeignKey(ug => ug.buyer_currency_id)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

                x.Property(e => e.buyer_currency_amount).IsRequired(true);
                x.Property(e => e.seller_currency_amount).IsRequired(true);

                x.Property(e => e.exchange_rate).IsRequired(true);

                x.Property(e => e.buyer_banker_id).IsRequired(false);

                x.Property(e => e.seller_banker_id).IsRequired(false);

                x.Property(e => e.buyer_send_bank_id).IsRequired(false);

                x.Property(e => e.deal_date_time).IsRequired(false);

                x.Property(e => e.value_date_time).IsRequired(false);

                x.HasOne(ug => ug.FiscalYear)
                .WithMany(u => u.ForexBuySellDeals)
                .HasForeignKey(ug => ug.fiscal_year_id)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

                x.HasOne(ug => ug.Dealer)
                .WithMany(u => u.ForexBuySellDeals)
                .HasForeignKey(ug => ug.dealer_id)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

                x.Property(e => e.dealer_signature).IsRequired(false);

                x.HasOne(ug => ug.userinformation)
                .WithMany(u => u.ForexBuySellDeals)
                .HasForeignKey(ug => ug.verified_by)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired(false);

                x.Property(e => e.verified_signature).IsRequired(false);

                x.HasOne(ug => ug.userinformation)
                .WithMany(u => u.ForexBuySellDeals)
                .HasForeignKey(ug => ug.authorized_by)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired(false);

                x.Property(e => e.authorized_signature).IsRequired(false);

                x.Property(e => e.mid_office_id).IsRequired(false);

                x.Property(e => e.mid_office_signature).IsRequired(false);

                x.Property(e => e.mid_office_date_time).IsRequired(false);

                x.Property(e => e.mid_office_remarks).IsRequired(false);


                x.Property(e => e.back_office_id).IsRequired(false);

                x.Property(e => e.back_office_remarks).IsRequired(false);

                x.Property(e => e.back_office_signature).IsRequired(false);


            });


            base.OnModelCreating(modelBuilder);
            }

        }
    }


