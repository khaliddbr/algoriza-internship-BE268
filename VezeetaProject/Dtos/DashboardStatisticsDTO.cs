namespace VezeetaProject.Dtos
{
    public class DashboardStatisticsDTO
    {
        public int NumOfDoctors { get; set; }
        public int NumOfPations { get; set; }
        public DashboardStatusRequestDTO NumOfRequests { get; set; }
        public List<SpecializationDTO> TopSpecializations { get; set; }
        public List<TopDoctorDTO> TopDoctors { get; set; }
    }
}

