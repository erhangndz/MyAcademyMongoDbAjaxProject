using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using MyAcademyMongoDbAjaxProject.DAL.Entities;
using MyAcademyMongoDbAjaxProject.DAL.Settings;
using Newtonsoft.Json;

namespace MyAcademyMongoDbAjaxProject.Controllers
{
    public class EmployeeController : Controller
    {

        private readonly IMongoCollection<Employee> _employeeCollection;
        public EmployeeController(IDatabaseSettings _databaseSettings)
        {
            var client = new MongoClient(_databaseSettings.ConnectionString);
            var database = client.GetDatabase(_databaseSettings.DatabaseName);
            _employeeCollection = database.GetCollection<Employee>(_databaseSettings.EmployeeCollectionName);
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> EmployeeList()
        {
            var values = await _employeeCollection.Find(x => true).ToListAsync();
            var jsonEmployee = JsonConvert.SerializeObject(values);
            return Json(jsonEmployee);
        }

        public async Task<IActionResult> CreateEmployee(Employee employee)
        {
            await _employeeCollection.InsertOneAsync(employee);
            var values = JsonConvert.SerializeObject(employee);
            return Json(values);
        }

        public async Task<IActionResult> GetEmployee(string employeeId)
        {
            var value = await _employeeCollection.Find(x => x.EmployeeId == employeeId).FirstOrDefaultAsync();
            var jsonValue = JsonConvert.SerializeObject(value);
            return Json(jsonValue);
        }

   
        public async Task<IActionResult> DeleteEmployee(string id)
        {
           
              var value=  await _employeeCollection.DeleteOneAsync(x=>x.EmployeeId==id);
            return NoContent();
         
        }

        public async Task<IActionResult> UpdateEmployee(Employee employee)
        {
            await _employeeCollection.FindOneAndReplaceAsync(x=>x.EmployeeId==employee.EmployeeId, employee);
            return NoContent();
        }


    }
}
