namespace VezeetaProject.Dtos
{
    public class DashboardStatusRequestDTO
    {
        public int TotalRequests { get; set; }
        public int PendingRequests { get; set; }
        public int CompletedRequests { get; set; }
        public int CancelledRequests { get; set; }
    }
}
