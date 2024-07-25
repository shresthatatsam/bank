using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bank.Migrations
{
    public partial class completetable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "currencies",
                columns: table => new
                {
                    currency_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    currency_name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_currencies", x => x.currency_id);
                });

            migrationBuilder.CreateTable(
                name: "fiscalyear",
                columns: table => new
                {
                    fiscal_year_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    start_date = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    end_date = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    fs_code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    fs_year = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_fiscalyear", x => x.fiscal_year_id);
                });

            migrationBuilder.CreateTable(
                name: "GroupNames",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    group_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    created_date = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    is_active = table.Column<bool>(type: "bit", nullable: true, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupNames", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MessageInfos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    subject = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    message_body = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    creator_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    created_date = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    parent_message_id = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessageInfos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "modeofdeals",
                columns: table => new
                {
                    mod_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    deal_name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_modeofdeals", x => x.mod_id);
                });

            migrationBuilder.CreateTable(
                name: "parties",
                columns: table => new
                {
                    party_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    party_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    remarks = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_parties", x => x.party_id);
                });

            migrationBuilder.CreateTable(
                name: "roles",
                columns: table => new
                {
                    role_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    role_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    is_active = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_roles", x => x.role_id);
                });

            migrationBuilder.CreateTable(
                name: "UserInformations",
                columns: table => new
                {
                    user_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    user_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    is_active = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
                    role = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserInformations", x => x.user_id);
                });

            migrationBuilder.CreateTable(
                name: "bankdetails",
                columns: table => new
                {
                    bank_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    bank_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    account_number = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    currency_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bankdetails", x => x.bank_id);
                    table.ForeignKey(
                        name: "FK_bankdetails_currencies_currency_id",
                        column: x => x.currency_id,
                        principalTable: "currencies",
                        principalColumn: "currency_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "dealers",
                columns: table => new
                {
                    dealer_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    dealer_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    party_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dealers", x => x.dealer_id);
                    table.ForeignKey(
                        name: "FK_dealers_parties_party_id",
                        column: x => x.party_id,
                        principalTable: "parties",
                        principalColumn: "party_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MessageRecipents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    recipent_group_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    message_info_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    recipent_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessageRecipents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MessageRecipents_GroupNames_recipent_group_id",
                        column: x => x.recipent_group_id,
                        principalTable: "GroupNames",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MessageRecipents_MessageInfos_message_info_id",
                        column: x => x.message_info_id,
                        principalTable: "MessageInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MessageRecipents_UserInformations_recipent_id",
                        column: x => x.recipent_id,
                        principalTable: "UserInformations",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserGroups",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    created_date = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    is_active = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
                    user_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    group_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserGroups_GroupNames_group_id",
                        column: x => x.group_id,
                        principalTable: "GroupNames",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserGroups_UserInformations_user_id",
                        column: x => x.user_id,
                        principalTable: "UserInformations",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "partybankdetails",
                columns: table => new
                {
                    party_bank_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    party_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    bank_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_partybankdetails", x => x.party_bank_id);
                    table.ForeignKey(
                        name: "FK_partybankdetails_bankdetails_bank_id",
                        column: x => x.bank_id,
                        principalTable: "bankdetails",
                        principalColumn: "bank_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_partybankdetails_parties_party_id",
                        column: x => x.party_id,
                        principalTable: "parties",
                        principalColumn: "party_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "forexbutselldeals",
                columns: table => new
                {
                    forex_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    reference_no = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    counter_party_dealer_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    our_dealer_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    buyer_currency_amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    seller_currency_amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    exchange_rate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    buyer_banker_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    seller_banker_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    buyer_send_bank_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    deal_date_time = table.Column<DateTime>(type: "datetime2", nullable: true),
                    value_date_time = table.Column<DateTime>(type: "datetime2", nullable: true),
                    creator = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    dealer_signature = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    verified_by = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    verified_signature = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    authorized_by = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    authorized_signature = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    mid_office_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    mid_office_signature = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    mid_office_date_time = table.Column<DateTime>(type: "datetime2", nullable: true),
                    mid_office_remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    back_office_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    back_office_signature = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    back_office_remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    dealer_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    fiscal_year_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    buyer_currency_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    seller_currency_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    party_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    mod_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_forexbutselldeals", x => x.forex_id);
                    table.ForeignKey(
                        name: "FK_forexbutselldeals_currencies_buyer_currency_id",
                        column: x => x.buyer_currency_id,
                        principalTable: "currencies",
                        principalColumn: "currency_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_forexbutselldeals_dealers_dealer_id",
                        column: x => x.dealer_id,
                        principalTable: "dealers",
                        principalColumn: "dealer_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_forexbutselldeals_fiscalyear_fiscal_year_id",
                        column: x => x.fiscal_year_id,
                        principalTable: "fiscalyear",
                        principalColumn: "fiscal_year_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_forexbutselldeals_modeofdeals_mod_id",
                        column: x => x.mod_id,
                        principalTable: "modeofdeals",
                        principalColumn: "mod_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_forexbutselldeals_parties_party_id",
                        column: x => x.party_id,
                        principalTable: "parties",
                        principalColumn: "party_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_forexbutselldeals_UserInformations_authorized_by",
                        column: x => x.authorized_by,
                        principalTable: "UserInformations",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_bankdetails_currency_id",
                table: "bankdetails",
                column: "currency_id");

            migrationBuilder.CreateIndex(
                name: "IX_dealers_party_id",
                table: "dealers",
                column: "party_id");

            migrationBuilder.CreateIndex(
                name: "IX_forexbutselldeals_authorized_by",
                table: "forexbutselldeals",
                column: "authorized_by");

            migrationBuilder.CreateIndex(
                name: "IX_forexbutselldeals_buyer_currency_id",
                table: "forexbutselldeals",
                column: "buyer_currency_id");

            migrationBuilder.CreateIndex(
                name: "IX_forexbutselldeals_dealer_id",
                table: "forexbutselldeals",
                column: "dealer_id");

            migrationBuilder.CreateIndex(
                name: "IX_forexbutselldeals_fiscal_year_id",
                table: "forexbutselldeals",
                column: "fiscal_year_id");

            migrationBuilder.CreateIndex(
                name: "IX_forexbutselldeals_mod_id",
                table: "forexbutselldeals",
                column: "mod_id");

            migrationBuilder.CreateIndex(
                name: "IX_forexbutselldeals_party_id",
                table: "forexbutselldeals",
                column: "party_id");

            migrationBuilder.CreateIndex(
                name: "IX_MessageRecipents_message_info_id",
                table: "MessageRecipents",
                column: "message_info_id");

            migrationBuilder.CreateIndex(
                name: "IX_MessageRecipents_recipent_group_id",
                table: "MessageRecipents",
                column: "recipent_group_id");

            migrationBuilder.CreateIndex(
                name: "IX_MessageRecipents_recipent_id",
                table: "MessageRecipents",
                column: "recipent_id");

            migrationBuilder.CreateIndex(
                name: "IX_partybankdetails_bank_id",
                table: "partybankdetails",
                column: "bank_id");

            migrationBuilder.CreateIndex(
                name: "IX_partybankdetails_party_id",
                table: "partybankdetails",
                column: "party_id");

            migrationBuilder.CreateIndex(
                name: "IX_UserGroups_group_id",
                table: "UserGroups",
                column: "group_id");

            migrationBuilder.CreateIndex(
                name: "IX_UserGroups_user_id",
                table: "UserGroups",
                column: "user_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "forexbutselldeals");

            migrationBuilder.DropTable(
                name: "MessageRecipents");

            migrationBuilder.DropTable(
                name: "partybankdetails");

            migrationBuilder.DropTable(
                name: "roles");

            migrationBuilder.DropTable(
                name: "UserGroups");

            migrationBuilder.DropTable(
                name: "dealers");

            migrationBuilder.DropTable(
                name: "fiscalyear");

            migrationBuilder.DropTable(
                name: "modeofdeals");

            migrationBuilder.DropTable(
                name: "MessageInfos");

            migrationBuilder.DropTable(
                name: "bankdetails");

            migrationBuilder.DropTable(
                name: "GroupNames");

            migrationBuilder.DropTable(
                name: "UserInformations");

            migrationBuilder.DropTable(
                name: "parties");

            migrationBuilder.DropTable(
                name: "currencies");
        }
    }
}
