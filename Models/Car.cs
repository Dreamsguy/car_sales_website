using Microsoft.AspNetCore.SignalR;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    public class Car
    {
        public int CarId { get; set; }
        public string Name { get; set; }// Марка
        public int Horsepower { get; set; }
        public string Model { get; set; }
        public double Cost { get; set; }
        public string Color { get; set; }
        public int ordersId { get; set; }
        public List<Orders>? orders { get; set; } = null;

    }
}
