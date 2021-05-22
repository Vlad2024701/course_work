using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

using AutoParking.Services;

namespace AutoParking.Models
{
	public class Place
	{
		[Key]
		public int Id { get; set; }

		public virtual List<Booking> Bookings { get; set; } = new List<Booking>();

		[NotMapped]
		public List<Booking> History => SqlClient.GetInstance().Bookings
			.Where(booking => booking.Place.Id == Id)
			.OrderBy(item => item.StartTime).ToList();

		public Booking CurrentBooking => SqlClient.GetInstance().Bookings.ToList().Where(item => item.Place.Id == Id && !item.IsEnded).FirstOrDefault();

		public bool IsBooked => CurrentBooking != null;

		public Place() { Id = 0; }
	}
}
