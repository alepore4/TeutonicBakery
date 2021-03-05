using System;
using System.Data;
using TeutonicBakery.Models.Enum;
using TeutonicBakery.Models.ValueTypes;

namespace TeutonicBakery.Models.ViewModels.Pastries
{
    public class PastryViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Money Price { get; set; }
        public Money CurrentPrice { get; set; }
        public string ImagePath { get; set; }
        public DateTime InsertTime { get; set; }
        public PastryStatus Status { get; set; }

        public static PastryViewModel FromDataRow(DataRow pastryRow)
        {
            var pastryViewModel = new PastryViewModel
            {
                Id = Convert.ToInt32(pastryRow["Id"]),
                Name = Convert.ToString(pastryRow["Name"]),
                Description = Convert.ToString(pastryRow["Description"]),
                Price = new Money(
                    System.Enum.Parse<Currency>(Convert.ToString(pastryRow["Currency"])),
                    Convert.ToDecimal(pastryRow["Price"])
                ),
                ImagePath = Convert.ToString(pastryRow["ImagePath"]),
                InsertTime = Convert.ToDateTime(pastryRow["InsertDateTime"])
            };

            int days = Convert.ToInt32(Math.Floor((DateTime.Now - pastryViewModel.InsertTime).TotalDays));

            if (days > 2)
            {
                pastryViewModel.Status = PastryStatus.NotGoodToSell;
            }
            else
            {
                pastryViewModel.Status = PastryStatus.GoodToSell;
            }

            decimal discountPrice = (days switch
            {
                0 => pastryViewModel.Price.Amount,
                1 => (pastryViewModel.Price.Amount * 80) / 100,
                2 => (pastryViewModel.Price.Amount * 20) / 100,
                _ => 0
            });

            pastryViewModel.CurrentPrice = new Money(
                System.Enum.Parse<Currency>(Convert.ToString(pastryRow["Currency"])),
                discountPrice
            );

            return pastryViewModel;
        }
    }
}
