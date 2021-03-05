using System;
using System.Data;
using TeutonicBakery.Models.Enum;

namespace TeutonicBakery.Models.ViewModels.Ingredients
{
    public class IngredientViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Quantity { get; set; } 
        public UoM UoM { get; set; }

        public static IngredientViewModel FromDataRow(DataRow ingredientRow)
        {
            IngredientViewModel ingredientViewModel = new()
            {
                Id = Convert.ToInt32(ingredientRow["Id"]),
                Name = Convert.ToString(ingredientRow["Name"]),
                Quantity = Convert.ToDecimal(ingredientRow["Quantity"]),
                UoM = System.Enum.Parse<UoM>(Convert.ToString(ingredientRow["UoM"]))
            };
            return ingredientViewModel;
        }

    }
}