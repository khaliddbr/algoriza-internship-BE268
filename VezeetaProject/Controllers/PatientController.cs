using AutoMapper;
using core.Models;
using core.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Repositories;
using VezeetaProject.Dtos;

namespace VezeetaProject.Controllers
{
    public class PatientController : BaseController
    {
        private readonly IpatientRepository _patientRepository;
        private readonly IBookingRepository _bookingRepository;

        public PatientController(IpatientRepository patientRepository, IBookingRepository bookingRepository)
        {
            _patientRepository = patientRepository;
            _bookingRepository = bookingRepository;
        }

        [HttpPost("register")]
        public IActionResult RegisterPatient([FromBody] Patient patientData)
        {
            bool isRegistered = _patientRepository.RegisterPatient(patientData);
            if (isRegistered)
            {
                return Ok("Patient registered successfully");
            }
            return BadRequest("Failed to register patient");
        }

        [HttpPost("login")]
        public IActionResult LoginPatient([FromBody] User loginData)
        {
            bool isLoggedIn = _patientRepository.LoginPatient(loginData);
            if (isLoggedIn)
            {
                return Ok("Patient logged in successfully");
            }
            return BadRequest("Login failed");
        }

        [HttpGet("search-doctors")]
        public IActionResult SearchDoctors(int page, int pageSize, string search)
        {
            var doctors = _patientRepository.SearchDoctors(page, pageSize, search);
            if (doctors != null && doctors.Any())
            {
                return Ok(doctors);
            }
            return NotFound("No doctors found");
        }

        [HttpPost("book-doctor")]
        public IActionResult BookDoctor([FromBody] Booking bookingData)
        {
            bool isBooked = _bookingRepository.CreateBooking(bookingData);
            if (isBooked)
            {
                return Ok("Doctor booked successfully");
            }
            return BadRequest("Booking failed");
        }

        [HttpGet("bookings/{patientId}")]
        public IActionResult GetPatientBookings(int patientId)
        {
            var bookings = _bookingRepository.GetBookingsByPatient(patientId);
            if (bookings != null && bookings.Any())
            {
                return Ok(bookings);
            }
            return NotFound("No bookings found for this patient");
        }

        [HttpDelete("cancel-booking/{bookingId}")]
        public IActionResult CancelBooking(int bookingId)
        {
            bool isCancelled = _bookingRepository.CancelBooking(bookingId);
            if (isCancelled)
            {
                return Ok("Booking cancelled successfully");
            }
            return BadRequest("Cancellation failed");
        }

    }
}




