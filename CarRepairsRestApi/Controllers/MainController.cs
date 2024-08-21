using CarRepairsRestApi.Models;
using CarRepairsRestApi.Repositories.Interfaces;
using CarRepairsRestApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CarRepairsRestApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MainController : ControllerBase
    {
        private IRepairService RepairService { get; set; }
        private IBaseRepository<Document> Documents { get; set; }

        public MainController(IRepairService repairService, IBaseRepository<Document> document)
        {
            RepairService = repairService;
            Documents = document;
        }

        // Получение списка всех документов
        [HttpGet]
        public JsonResult Get()
        {
            return new JsonResult(Documents.GetAll());
        }

        // Вызов сервиса ремонта и возвращение сообщения об успехе
        [HttpPost]
        public JsonResult Post()
        {
            RepairService.Work();
            return new JsonResult("Work was successfully done");
        }

        // Обновление документа
        [HttpPut]
        public JsonResult Put(Document doc)
        {
            bool success = true;
            var document = Documents.Get(doc.Id);

            try
            {
                if (document != null)
                {
                    document = Documents.Update(doc);
                }
                else
                {
                    success = false;
                }
            }
            catch (Exception)
            {
                success = false;
            }

            return success ? new JsonResult($"Update successful {document.Id}") : new JsonResult("Update was not successful");
        }

        // Удаление документа по ID
        [HttpDelete]
        public JsonResult Delete(Guid id)
        {
            bool success = true;
            var document = Documents.Get(id);

            try
            {
                if (document != null)
                {
                    Documents.Delete(document.Id);
                }
                else
                {
                    success = false;
                }
            }
            catch (Exception)
            {
                success = false;
            }

            return success ? new JsonResult("Delete successful") : new JsonResult("Delete was not successful");
        }
    }
}
