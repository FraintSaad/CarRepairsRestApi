using CarRepairsRestApi.Models.Base;

namespace CarRepairsRestApi.Models
{
    public class Car : BaseModel
    {
        public string Name { get; set; }
        public string Number { get; set; }
    }
}
