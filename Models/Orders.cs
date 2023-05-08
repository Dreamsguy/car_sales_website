using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Orders
    {
        public int Id { get; set; }
        public string? date { get; set; }
        public string? name { get; set; }
        public string? status { get; set; }
        public List<Car>? Cars { get; set; } = null;
    }
}
