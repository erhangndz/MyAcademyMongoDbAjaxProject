using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using MyAcademyMongoDbAjaxProject.DAL.Entities;
using MyAcademyMongoDbAjaxProject.DAL.Settings;

namespace MyAcademyMongoDbAjaxProject.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IMongoCollection<Category> _categoryCollection;
     

        public CategoryController(IDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);
            _categoryCollection = database.GetCollection<Category>(databaseSettings.CategoryCollectionName);
            
        }

        public IActionResult Index()
        {
            var values = _categoryCollection.Find(category => true).ToList();
            return View(values);
        }

        public IActionResult CreateCategory()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateCategory(Category category)
        {
            _categoryCollection.InsertOne(category);
            return RedirectToAction("Index");
        }

        public IActionResult DeleteCategory(string id)
        {
            _categoryCollection.DeleteOne(x=>x.CategoryID==id);
            return RedirectToAction("Index");
        }

        public IActionResult UpdateCategory(string id)
        {
            var value = _categoryCollection.Find(x => x.CategoryID == id).FirstOrDefault();
            return View(value);

        }

        [HttpPost]
        public IActionResult UpdateCategory(Category category)
        {
            _categoryCollection.FindOneAndReplace(x => x.CategoryID == category.CategoryID, category);
            return RedirectToAction("Index");
        }
    }
}
