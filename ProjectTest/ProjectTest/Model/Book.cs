using ProjectTest.Model.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectTest.Model
{
    [Table("book")]
    public class Book: BaseEntity
    {
        public string Author { get; set; } 
        public string Title { get; set; } 
        public DateTime Launch_date { get; set; } 
        public decimal Price { get; set; }
        
    }
}
