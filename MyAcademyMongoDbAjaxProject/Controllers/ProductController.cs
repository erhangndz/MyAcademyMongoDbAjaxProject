using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using MyAcademyMongoDbAjaxProject.DAL.Entities;
using MyAcademyMongoDbAjaxProject.DAL.Settings;

namespace MyAcademyMongoDbAjaxProject.Controllers
{
    public class ProductController : Controller
    {
        private readonly IMongoCollection<Product> _productCollection;

        public ProductController(IDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);
            _productCollection = database.GetCollection<Product>(databaseSettings.ProductCollectionName);


         
        }

        public IActionResult Index()
        {
            var values = _productCollection.Find(category => true).ToList();
            return View(values);
        }

        public IActionResult CreateProduct()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateProduct(Product product)
        {
            _productCollection.InsertOne(product);
            return RedirectToAction("Index");
        }

        public IActionResult DeleteProduct(string id)
        {
            _productCollection.DeleteOne(x => x.ProductID == id);
            return RedirectToAction("Index");
        }

        public IActionResult UpdateProduct(string id)
        {
            var value = _productCollection.Find(x => x.CategoryID == id).FirstOrDefault();
            return View(value);

        }

        [HttpPost]
        public IActionResult UpdateProduct(Product product)
        {
            _productCollection.FindOneAndReplace(x => x.ProductID == product.ProductID, product);
            return RedirectToAction("Index");
        }
    }
}
