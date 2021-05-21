using System.ComponentModel.DataAnnotations;

namespace AutoParking.Model
{
	public class Car
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Number { get; set; }
        public string Model { get; set; }

		public Car() => Id = 0;

		public Car(string number, string model)
		{
			Id = 0;
			Number = number;
			Model = model;
		}
	}
}
