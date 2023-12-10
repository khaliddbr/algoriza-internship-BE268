using AutoMapper;
using core.Models;
using core.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repository.data;
using Service.Services;
using VezeetaProject.Dtos;

namespace VezeetaProject.Controllers
{

    public class AdminController : BaseController
    {
        private readonly IDashboardService _dashboardService;
        private readonly IGenericRepository<Doctor> _doctorRepository;
        private readonly IGenericRepository<Patient> _patientRepository;
        private readonly IMapper _mapper;

        public AdminController(IDashboardService dashboardService, IGenericRepository<Doctor> doctorRepository, IGenericRepository<Patient> PatientRepository, IMapper mapper)
        {
            _dashboardService = dashboardService;
            _doctorRepository = doctorRepository;
            _patientRepository = PatientRepository;
            _mapper = mapper;
        }

        [HttpGet("statistics")]
        public ActionResult<DashboardStatistics> GetStatistics()
        {
            var statistics = _dashboardService.GetOverallStatistics();
            return Ok(statistics);
        }

        [HttpGet("statistics/timeframe")]
        public ActionResult<DashboardStatistics> GetStatisticsByTimeframe([FromQuery] string timeframe)
        {
            var statistics = _dashboardService.GetStatisticsByTimeframe(timeframe);
            return Ok(statistics);
        }

        [HttpGet("Get All Doctors")]
        public async Task<ActionResult<IEnumerable<DoctorDto>>> GetAll(int page = 1, int pageSize = 10, string? search = null)
        {
            if (string.IsNullOrEmpty(search))
            {
                var Doctors = await _doctorRepository.GetAllAsync(page, pageSize);
                var DoctorDto = _mapper.Map<IEnumerable<DoctorDto>>(Doctors);
                return Ok(DoctorDto);
            }
            else
            {
                var Doctors = await _doctorRepository.GetAllAsync(page, pageSize, search);
                var DoctorDto = _mapper.Map<IEnumerable<PatientDto>>(Doctors);
                return Ok(DoctorDto);
            }
        }
        [HttpGet("GetDoctorById/{id}")]
        public async Task<ActionResult<DoctorDto>> GetById(int id)
        {
            var doctor = await _doctorRepository.GetByIdAsync(id);
            if (doctor == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<Doctor, DoctorDto>(doctor));
        }
        [HttpPost("Add Doctor")]
        public async Task<IActionResult> AddDoctor([FromBody] DoctorDto doctorDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var doctor = _mapper.Map<Doctor>(doctorDto);
                _doctorRepository.AddAsync(doctor);

                var addedDoctorDto = _mapper.Map<DoctorDto>(doctor);
                return Ok(addedDoctorDto); 
            }
              
                
            catch (Exception ex)
            {
               
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
        [HttpPut("EditDoctor/{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] DoctorDto doctorDto)
        {
            if (id == 0)
            {
                return BadRequest("The provided ID does not match the doctor ID.");
            }

            var existingDoctor = await _doctorRepository.GetByIdAsync(id);

            if (existingDoctor == null)
            {
                return NotFound("Doctor not found.");
            }
            
            existingDoctor.FullName = doctorDto.FullName;
            existingDoctor.Email = doctorDto.Email;
            existingDoctor.phone = doctorDto.phone;
            existingDoctor.specializations.Name = doctorDto.Specialization;
            existingDoctor.dateOfBirth = doctorDto.dateOfBirth;
            existingDoctor.ImageUrl = doctorDto.ImageUrl;

            try
            {
                await _doctorRepository.UpdateAsync(existingDoctor);
                return NoContent(); 
            }
            catch (Exception ex)
            {
                
                return StatusCode(500, "Internal server error");
            }
        }


        [HttpDelete("DeleteDoctor/{id}")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            var doctor = await _doctorRepository.GetByIdAsync(id);
            if (doctor == null)
            {
                return NotFound();
            }
            await _doctorRepository.RemoveAsync(doctor);
            return Ok(true);
        }

        [HttpGet("GetAllPatient")]
        public async Task<ActionResult<IEnumerable<PatientDto>>> GetAllPatients(int page = 1, int pageSize = 10, string? search = null)
        {
            if (string.IsNullOrEmpty(search))
            {
                var patients = await _patientRepository.GetAllAsync(page, pageSize);
                var patientDto = _mapper.Map<IEnumerable<PatientDto>>(patients);
                return Ok(patientDto);
            }
            else
            {
                var patients = await _patientRepository.GetAllAsync(page, pageSize, search);
                var patientDto = _mapper.Map<IEnumerable<PatientDto>>(patients);
                return Ok(patientDto);
            }
        }

        [HttpGet("GetPatientById/{id}")]
        public async Task<ActionResult<PatientDto>> GetPatientById(int id)
        {
            var patient = await _patientRepository.GetByIdAsync(id);

            if (patient == null)
            {
                return NotFound();
            }
            var patientDto = _mapper.Map<PatientDto>(patient);
            return Ok(patientDto);
        }

    }

}



    



