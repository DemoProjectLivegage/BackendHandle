namespace Application.DTO
{
    public class GL_DTO
    {
        public int account_no { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public string operation { get; set; }
        public string description { get; set; }
        public decimal value { get; set; } = 0;
    }
}