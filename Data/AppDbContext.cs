using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Product_Display.Models;

namespace Product_Display.Data
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<TreasuryBP> Treasuries { get; set; }
        public DbSet<FGNBond> FGNBonds { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<SavingDay> SavingDays { get; set; }
        public DbSet<FXRate> FXRates { get; set; } 
        public DbSet<TextRoller> TextRollers { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<FXRate>().HasData(
                new FXRate
                {
                    Id = 1,
                    CurrencyName = "US Dollar",
                    Symbol = "USD",
                    BuyRate = 2010.52m,
                    SellRate = 2090.34m,
                    IsUpwardTrend = true
                },
                 new FXRate
                 {
                     Id = 2,
                     CurrencyName = "Pound Sterling",
                     Symbol = "GBP",
                     BuyRate = 1758.53m,
                     SellRate = 1812.43m,
                     IsUpwardTrend = true
                 },
                  new FXRate
                  {
                      Id = 3,
                      CurrencyName = "Euro",
                      Symbol = "EUR",
                      BuyRate = 1600,
                      SellRate = 1660,
                      IsUpwardTrend = true
                  },
                   new FXRate
                   {
                       Id = 4,
                       CurrencyName = "Canadian Dollar",
                       Symbol = "CAD",
                       BuyRate = 1116.19m,
                       SellRate = 1174.12m,
                       IsUpwardTrend = true
                   },
                    new FXRate
                    {
                        Id = 5,
                        CurrencyName = "S/African Rand",
                        Symbol = "ZAR",
                        BuyRate = 82.87m,
                        SellRate = 87.14m,
                        IsUpwardTrend = true
                    },
                     new FXRate
                     {
                         Id = 6,
                         CurrencyName = "UAE Dirham",
                         Symbol = "AED",
                         BuyRate = 421.14m,
                         SellRate = 443.48m,
                         IsUpwardTrend = true
                     },
                      new FXRate
                      {
                          Id = 7,
                          CurrencyName = "Chinese Yuan",
                          Symbol = "CNY",
                          BuyRate = 0,
                          SellRate = 0,
                          IsUpwardTrend = true
                      },
                       new FXRate
                       {
                           Id = 8,
                           CurrencyName = "West African",
                           Symbol = "XAF",
                           BuyRate = 0,
                           SellRate = 0,
                           IsUpwardTrend = true
                       },
                        new FXRate
                        {
                            Id = 9,
                            CurrencyName = "Japanese Yen",
                            Symbol = "JPY",
                            BuyRate = 0,
                            SellRate = 0,
                            IsUpwardTrend = true
                        },
                         new FXRate
                         {
                             Id = 10,
                             CurrencyName = "Indian Rupee",
                             Symbol = "RUP",
                             BuyRate = 0,
                             SellRate = 0,
                             IsUpwardTrend = true
                         },
                          new FXRate
                          {
                              Id = 11,
                              CurrencyName = "Hong Kong Dollar",
                              Symbol = "HKD",
                              BuyRate = 0,
                              SellRate = 0,
                              IsUpwardTrend = true
                          },
                           new FXRate
                           {
                               Id = 12,
                               CurrencyName = "Singaporean Dollar",
                               Symbol = "SGD",
                               BuyRate = 0,
                               SellRate = 0,
                               IsUpwardTrend = true
                           }
                );
        }
    }
}
