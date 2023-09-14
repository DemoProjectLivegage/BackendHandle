namespace Application.DTO
{
    public class BarDTO
    {
        public DateOnly Due_Date {get; set;}
        public decimal UPB_Amount {get; set;}
        public decimal Principal_Amount {get; set;}
        public decimal Interest_Amount {get; set;}
    }
}