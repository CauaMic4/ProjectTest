using ProjectTest.Model.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectTest.Model
{
    [Table("person")]
    public class Person : BaseEntity
    {
       // public long Id { get; set; }
        public string Address { get; set; } 
        public string FirstName { get; set; } 
        public string LastName { get; set; } 
        public string Gender { get; set; }
        //public bool? Ativo { get; set; }
    }
}
