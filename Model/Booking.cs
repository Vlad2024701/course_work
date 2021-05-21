using AutoParking.Services;

using System;
using System.ComponentModel.DataAnnotations;

namespace AutoParking.Model
{
	public class Booking
    {
        const double pricePerHour = 1;

        [Key]
        public int Id { get; set; }

        [Required]
        public User User { get; set; }
        [Required]
        public Car Car { get; set; }
        [Required]
        public Place Place { get; set; }
        [Required]
        public DateTime StartTime { get; set; }
        [Required]
        public DateTime EndTime { get; set; }


        public double Price { get => (EndTime - StartTime).TotalHours * pricePerHour; }

        public bool IsEnded { get => DateTime.Now > EndTime; }

        public Booking() { Id = 0; }

		public Booking(User user, Car car, Place place, DateTime startTime, DateTime endTime)
		{
            Id = 0;
			User = user;
			Car = car;
            Place = place;
			StartTime = startTime;
			EndTime = endTime;
		}
	}
}
