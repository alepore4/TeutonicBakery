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
                //CurrentPrice = new Money(
                //    System.Enum.Parse<Currency>(Convert.ToString(pastryRow["CurrentPrice_Currency"])),
                //    Convert.ToDecimal(pastryRow["CurrentPrice_Amount"])
                //),
                
            };
            return pastryViewModel;
        }
    }
}
