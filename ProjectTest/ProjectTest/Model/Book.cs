using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectTest.Model
{
    [Table("book")]
    public class Book
    {
        public long Id { get; set; }
        public string Author { get; set; } 
        public DateTime Launch_date { get; set; } 
        public decimal Price { get; set; }
        public bool? Ativo { get; set; }
    }
}
