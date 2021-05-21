using System.ComponentModel.DataAnnotations;

namespace AutoParking.Model
{
	public class Auto
    {
        [Key]
        public string Number { get; set; }

        public string Model { get; set; }

        public Auto() { }
    }
}
