using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using TeutonicBakery.Models.Exceptions.Application;
using TeutonicBakery.Models.Services.Infrastructure;
using TeutonicBakery.Models.ViewModels.Pastries;

namespace TeutonicBakery.Models.Services.Application.Pastries
{
    public class PastryService : IPastryService
    {
        private readonly ILogger<PastryService> logger;
        private readonly IDatabaseAccessor db;
        //private readonly IOptionsMonitor<CoursesOptions> coursesOptions;
        private readonly IImagePersister imagePersister;
        public PastryService(ILogger<PastryService> logger, IDatabaseAccessor db, IImagePersister imagePersister/*, IOptionsMonitor<CoursesOptions> coursesOptions*/)
        {
            this.imagePersister = imagePersister;
            //this.coursesOptions = coursesOptions;
            this.logger = logger;
            this.db = db;
        }
        //public async Task<PastryDetailViewModel> GetCourseAsync(int id)
        //{
        //    logger.LogInformation($"Pastry {id} requested");

        //    FormattableString query = $@"SELECT Id, Title, Description, ImagePath, Author, Rating, FullPrice_Amount, FullPrice_Currency, CurrentPrice_Amount, CurrentPrice_Currency FROM Courses WHERE Id={id} AND Status<>{nameof(CourseStatus.Deleted)}
        //    ; SELECT Id, Title, Description, Duration FROM Lessons WHERE CourseId={id} ORDER BY [Order], Id";

        //    DataSet dataSet = await db.QueryAsync(query);

        //    //Course
        //    var courseTable = dataSet.Tables[0];
        //    if (courseTable.Rows.Count != 1)
        //    {
        //        logger.LogWarning($"Pastry {id} not found");
        //        throw new PastryNotFoundException(id);
        //    }
        //    var courseRow = courseTable.Rows[0];
        //    var courseDetailViewModel = CourseDetailViewModel.FromDataRow(courseRow);

        //    //Course lessons
        //    var lessonDataTable = dataSet.Tables[1];

        //    foreach (DataRow lessonRow in lessonDataTable.Rows)
        //    {
        //        LessonViewModel lessonViewModel = LessonViewModel.FromDataRow(lessonRow);
        //        courseDetailViewModel.Lessons.Add(lessonViewModel);
        //    }
        //    return courseDetailViewModel;
        //}
        public async Task<List<PastryViewModel>> GetPastriesAsync()
        {
            //string orderby = model.OrderBy == "CurrentPrice" ? "CurrentPrice_Amount" : model.OrderBy;
            //string direction = model.Ascending ? "ASC" : "DESC";

            FormattableString query = $@"SELECT Id, Name, Description, Price, Currency, ImagePath, InsertDateTime 
                                        FROM Pastries";
                                        //WHERE Name LIKE {"%" + model.Search + "%"} 
                                        //ORDER BY {(Sql)orderby} {(Sql)direction}";
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
        //public async Task<CourseEditInputModel> GetCourseForEditingAsync(int id)
        //{
        //    FormattableString query = $@"SELECT Id, Title, Description, ImagePath, Email, FullPrice_Amount, FullPrice_Currency, CurrentPrice_Amount, CurrentPrice_Currency, RowVersion FROM Courses WHERE Id={id} AND Status<>{nameof(CourseStatus.Deleted)}";

        //    DataSet dataSet = await db.QueryAsync(query);

        //    DataTable courseTable = dataSet.Tables[0];
        //    if (courseTable.Rows.Count != 1)
        //    {
        //        logger.LogWarning("Course {id} not found", id);
        //        throw new CourseNotFoundException(id);
        //    }
        //    DataRow courseRow = courseTable.Rows[0];
        //    CourseEditInputModel courseEditInputModel = CourseEditInputModel.FromDataRow(courseRow);
        //    return courseEditInputModel;
        //}
        public async Task<List<PastryViewModel>> GetAvailablePastriesAsync()
        {
            List<PastryViewModel> result = await GetPastriesAsync();
            //ListViewModel<CourseViewModel> result = await GetCoursesAsync(inputModel);
            //return result.Results;
            return result;
        }
        //public async Task<List<CourseViewModel>> GetMostRecentCoursesAsync()
        //{
        //    CourseListInputModel inputModel = new(
        //        search: "",
        //        page: 1,
        //        orderby: "Id",
        //        ascending: false,
        //        limit: coursesOptions.CurrentValue.InHome,
        //        orderOptions: coursesOptions.CurrentValue.Order);

        //    ListViewModel<CourseViewModel> result = await GetCoursesAsync(inputModel);
        //    return result.Results;
        //}
        //public async Task<CourseDetailViewModel> CreateCourseAsync(CourseCreateInputModel inputModel)
        //{
        //    string title = inputModel.Title;
        //    string author = "Mario Rossi";

        //    try
        //    {
        //        int courseId = await db.QueryScalarAsync<int>($@"INSERT INTO Courses (Title, Author, ImagePath, Rating, CurrentPrice_Currency, CurrentPrice_Amount, FullPrice_Currency, FullPrice_Amount, Status) VALUES ({title}, {author}, '/Courses/default.png', 0, 'EUR', 0, 'EUR', 0, {nameof(CourseStatus.Draft)});
        //                                         SELECT last_insert_rowid();");

        //        CourseDetailViewModel course = await GetCourseAsync(courseId);
        //        return course;
        //    }
        //    catch (ConstraintViolationException exc)
        //    {
        //        throw new CourseTitleUnavailableException(inputModel.Title, exc);
        //    }
        //}
        //public async Task<bool> IsTitleAvailableAsync(string title, int id)
        //{
        //    bool titleExists = await db.QueryScalarAsync<bool>($"SELECT COUNT(*) FROM Courses WHERE Title LIKE {title} AND id<>{id}");
        //    return !titleExists;
        //}
        //public async Task<CourseDetailViewModel> EditCourseAsync(CourseEditInputModel inputModel)
        //{
        //    try
        //    {
        //        string imagePath = null;
        //        if (inputModel.Image != null)
        //        {
        //            imagePath = await imagePersister.SaveCourseImageAsync(inputModel.Id, inputModel.Image);
        //        }
        //        int affectedRows = await db.CommandAsync($"UPDATE Courses SET ImagePath=COALESCE({imagePath}, ImagePath), Title={inputModel.Title}, Description={inputModel.Description}, Email={inputModel.Email}, CurrentPrice_Currency={inputModel.CurrentPrice.Currency.ToString()}, CurrentPrice_Amount={inputModel.CurrentPrice.Amount}, FullPrice_Currency={inputModel.FullPrice.Currency.ToString()}, FullPrice_Amount={inputModel.FullPrice.Amount} WHERE Id={inputModel.Id} AND Status<>{nameof(CourseStatus.Deleted)} AND RowVersion={inputModel.RowVersion}");
        //        if (affectedRows == 0)
        //        {
        //            bool courseExists = await db.QueryScalarAsync<bool>($"SELECT COUNT(*) FROM Courses WHERE Id={inputModel.Id} AND Status<>{nameof(CourseStatus.Deleted)}");
        //            if (courseExists)
        //            {
        //                throw new OptimisticConcurrencyException();
        //            }
        //            else
        //            {
        //                throw new CourseNotFoundException(inputModel.Id);
        //            }
        //        }
        //    }
        //    catch (ConstraintViolationException exc)
        //    {
        //        throw new CourseTitleUnavailableException(inputModel.Title, exc);
        //    }
        //    catch (ImagePersistenceException exc)
        //    {
        //        throw new CourseImageInvalidException(inputModel.Id, exc);
        //    }

        //    CourseDetailViewModel course = await GetCourseAsync(inputModel.Id);
        //    return course;
        //}

        //public async Task DeleteCourseAsync(CourseDeleteInputModel inputModel)
        //{
        //    int affectedRows = await this.db.CommandAsync($"UPDATE Courses SET Status={nameof(CourseStatus.Deleted)} WHERE Id={inputModel.Id} AND Status<>{nameof(CourseStatus.Deleted)}");
        //    if (affectedRows == 0)
        //    {
        //        throw new CourseNotFoundException(inputModel.Id);
        //    }
        //}
    }
}
