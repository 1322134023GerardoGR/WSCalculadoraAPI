using System.ComponentModel.DataAnnotations.Schema;

namespace WSCalculadoraAPI.Models
{
    [Table("Calculadora")]
    public class Aritmetica
    {
        [Column("id")] 
        public int Id { get; set; }

        [Column("num1")]
        public double Num1 { get; set; }

        [Column("num2")]
        public double Num2 { get; set; }

        [Column("operation")]
        public string? Operation { get; set; }

        [Column("result")]
        public double Result { get; set; }

        [Column("createdat")]
        public DateTime CreatedAt { get; set; };
    }
}
