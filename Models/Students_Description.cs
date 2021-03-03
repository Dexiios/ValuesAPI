using System.ComponentModel.DataAnnotations;

namespace ValuesAPI.Models
{
    public class Students_Description
    {
        [Key]
        public int Id { get; set; }
        public int Student_Id { get; set; }
        public int Age { get; set; }
        public string First_name { get; set; }
        public string Last_name { get; set; }
        public string Adress { get; set; }
        public string Country { get; set; }
    }
}