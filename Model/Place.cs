using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace AutoParking.Model
{
	public class Place
    {
        [Key]
        public int Id { get; set; }

        public virtual List<Booking> Bookings { get; set; } = new List<Booking>();
       
        public bool IsBooked { get => !Bookings.All(item => item.IsEnded); }

        public Place() { Id = 0; }
    }
}
