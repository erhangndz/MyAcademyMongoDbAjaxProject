using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using MyAcademyMongoDbAjaxProject.DAL.Entities;
using MyAcademyMongoDbAjaxProject.DAL.Settings;

namespace MyAcademyMongoDbAjaxProject.Controllers
{
    public class ProductController : Controller
    {
        private readonly IMongoCollection<Product> _productCollection;

        public ProductController(IMongoCollection<Product> productCollection,IDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);
            _productCollection = database.GetCollection<Product>(databaseSettings.ProductCollectionName);


            _productCollection = productCollection;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
