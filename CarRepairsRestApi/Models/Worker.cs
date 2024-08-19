using CarRepairsRestApi.Models.Base;

namespace CarRepairsRestApi.Models
{
    public class Worker : BaseModel
    {
        public string Name { get; set; }
        public string Position { get; set; }
        public string Telephone { get; set; }
    }
}
