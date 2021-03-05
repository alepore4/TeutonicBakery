using System;
using System.Collections.Generic;
using System.Data;
using TeutonicBakery.Models.Enum;
using TeutonicBakery.Models.ValueTypes;
using TeutonicBakery.Models.ViewModels.Ingredients;

namespace TeutonicBakery.Models.ViewModels.Pastries
{
    public class PastryDetailViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Money Price { get; set; }
        public Money CurrentPrice { get; set; }
        public string ImagePath { get; set; }
        public DateTime InsertTime { get; set; }
        public List<IngredientViewModel> Ingredients { get; set; } = new List<IngredientViewModel>();
        public PastryStatus Status { get; set; }

        public static PastryDetailViewModel FromDataRow(DataRow pastryRow)
        {
            var pastryDetailViewModel = new PastryDetailViewModel
            {
                Id = Convert.ToInt32(pastryRow["Id"]),
                Name = Convert.ToString(pastryRow["Name"]),
                Description = Convert.ToString(pastryRow["Description"]),
                Price = new Money(
                    System.Enum.Parse<Currency>(Convert.ToString(pastryRow["Currency"])),
                    Convert.ToDecimal(pastryRow["Price"])
                ),
                ImagePath = Convert.ToString(pastryRow["ImagePath"]),
                InsertTime = Convert.ToDateTime(pastryRow["InsertDateTime"]),
                Ingredients = new List<IngredientViewModel>()
            };

            int days = Convert.ToInt32(Math.Floor((DateTime.Now - pastryDetailViewModel.InsertTime).TotalDays));

            if (days > 2)
            {
                pastryDetailViewModel.Status = PastryStatus.NotGoodToSell;
            }
            else
            {
                pastryDetailViewModel.Status = PastryStatus.GoodToSell;
            }

            decimal discountPrice = (days switch
            {
                0 => pastryDetailViewModel.Price.Amount,
                1 => (pastryDetailViewModel.Price.Amount * 80) / 100,
                2 => (pastryDetailViewModel.Price.Amount * 20) / 100,
                _ => 0
            });

            pastryDetailViewModel.CurrentPrice = new Money(
                System.Enum.Parse<Currency>(Convert.ToString(pastryRow["Currency"])),
                discountPrice
            );

            return pastryDetailViewModel;
        }

    }
}