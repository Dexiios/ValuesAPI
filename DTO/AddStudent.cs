using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ValuesAPI.DTO
{
    public class AddStudent
    {
        public string Student_grade { get; set; }
        public int Student_age { get; set; }
        public string Student_first_name { get; set; }
        public string Student_last_name { get; set; }
        public string Student_adress { get; set; }
        public string Student_country { get; set; }
    }
}
