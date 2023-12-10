using core.Models;

namespace VezeetaProject.Dtos
{
    public class DiscontDto
    {
        public int Id { get; set; }
        public string DiscountCode { get; set; }

        public string DiscountType { get; set; }

        public int Value { get; set; }

        public string Doctor { get; set; }

    }
}
