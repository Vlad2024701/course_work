using AutoParking.Services;

using System;
using System.ComponentModel.DataAnnotations;

namespace AutoParking.Model
{
	public class Booking
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public Auto Auto { get; set; }
        [Required]
        public DateTime StartTime { get; set; }
        [Required]
        public DateTime EndTime { get; set; }

        public double Price { get; set; }

        public User User { get => SqlClient.GetUserByAuto(Auto); }

        public bool IsEnded { get => DateTime.Now > EndTime; }

        public Booking() { Id = 0; }
    }
}
