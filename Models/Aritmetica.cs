namespace WSCalculadoraAPI.Models
{
    public class Aritmetica
    {
        public int Id { get; set; }
        public double Num1 { get; set; }
        public double Num2 { get; set; }
        public string Operation { get; set; }
        public double Result { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? DeletedAt { get; set; }
    }
}
