
using System;
using System.ComponentModel.DataAnnotations;

namespace AutoParking.Models
{
	public class Booking
    {
        public const double pricePerHour = 1;

        [Key]
        public int Id { get; set; }

        [Required]
        public User User { get; set; }
        [Required]
        public Place Place { get; set; }
        [Required]
        public DateTime StartTime { get; set; }
        [Required]
        public DateTime EndTime { get; set; }


		public double Price => (EndTime - StartTime).TotalHours * pricePerHour;

		public bool IsEnded => DateTime.Now > EndTime;

        public Booking() { Id = 0; }

		public Booking(User user, Place place, DateTime startTime, DateTime endTime)
		{
            Id = 0;
			User = user;
            Place = place;
			StartTime = startTime;
			EndTime = endTime;
		}
	}
}
