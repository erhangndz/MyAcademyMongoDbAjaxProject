using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using MyAcademyMongoDbAjaxProject.DAL.Entities;
using MyAcademyMongoDbAjaxProject.DAL.Settings;

namespace MyAcademyMongoDbAjaxProject.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IMongoCollection<Category> _categoryCollection;
     

        public CategoryController(IMongoCollection<Category> categoryCollection, IDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);
            _categoryCollection = database.GetCollection<Category>(databaseSettings.CategoryCollectionName);
            
        }

        public IActionResult Index()
        {
            var values = _categoryCollection.AsQueryable().ToList();
            return View();
        }
    }
}
