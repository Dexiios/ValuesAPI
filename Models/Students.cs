using System.ComponentModel.DataAnnotations;

namespace ValuesAPI.Models
{
    public class Students
    {
        [Key]
        public int Id { get; set; }
        public string Grade { get; set; }
    }
}