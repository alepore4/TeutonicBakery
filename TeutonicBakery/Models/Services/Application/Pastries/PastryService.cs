using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using TeutonicBakery.Models.Exceptions.Application;
using TeutonicBakery.Models.Services.Infrastructure;
using TeutonicBakery.Models.ViewModels.Ingredients;
using TeutonicBakery.Models.ViewModels.Pastries;

namespace TeutonicBakery.Models.Services.Application.Pastries
{
    public class PastryService : IPastryService
    {
        private readonly ILogger<PastryService> logger;
        private readonly IDatabaseAccessor db;
        private readonly IImagePersister imagePersister;
        public PastryService(ILogger<PastryService> logger, IDatabaseAccessor db, IImagePersister imagePersister)
        {
            this.imagePersister = imagePersister;
            this.logger = logger;
            this.db = db;
        }

        public async Task<PastryDetailViewModel> GetPastryAsync(int id)
        {
            logger.LogInformation("Pastry {id} requested", id);

            FormattableString query = $@"SELECT Id, Name, Description, Price, Currency, ImagePath, date(InsertDateTime) as InsertDateTime
                                         FROM Pastries 
                                         WHERE Id={id};
                                         SELECT i.Id, i.Name, c.Quantity, c.UoM
                                         FROM Ingredients i 
                                         INNER JOIN Compositions c ON i.Id = c.IdI
                                         WHERE c.IdP = {id}";

            DataSet dataSet = await db.QueryAsync(query);

            var pastryTable = dataSet.Tables[0];
            if (pastryTable.Rows.Count != 1)
            {
                logger.LogWarning("Pastry {id} not found", id);
                throw new PastryNotFoundException(id);
            }
            var pastryRow = pastryTable.Rows[0];
            var pastryDetailViewModel = PastryDetailViewModel.FromDataRow(pastryRow);

            var ingredientsDataTable = dataSet.Tables[1];

            foreach (DataRow ingredientRow in ingredientsDataTable.Rows)
            {
                IngredientViewModel ingredientViewModel = IngredientViewModel.FromDataRow(ingredientRow);
                pastryDetailViewModel.Ingredients.Add(ingredientViewModel);
            }
            return pastryDetailViewModel;
        }

        public async Task<List<PastryViewModel>> GetPastriesAsync()
        {
            FormattableString query = $@"SELECT Id, Name, Description, Price, Currency, ImagePath, date(InsertDateTime) as InsertDateTime
                                        FROM Pastries";

            DataSet dataSet = await db.QueryAsync(query);
            DataTable dataTable = dataSet.Tables[0];
            List<PastryViewModel> pastryList = new();
            foreach (DataRow pastryRow in dataTable.Rows)
            {
                PastryViewModel pastryViewModel = PastryViewModel.FromDataRow(pastryRow);
                pastryList.Add(pastryViewModel);
            }

            return pastryList;
        }

        public async Task<List<PastryViewModel>> GetAvailablePastriesAsync()
        {
            List<PastryViewModel> result = await GetPastriesAsync();
            return result;
        }

    }
}
