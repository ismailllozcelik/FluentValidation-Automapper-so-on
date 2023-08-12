namespace FluentValidationApp.Web.DTOs
{
    public class CustomerDTO
    {
        public int Id { get; set; }
        public string Isim { get; set; }
        public string Eposta { get; set; }
        public int Yas { get; set; }

        public string FullName { get; set; }
    }
}
