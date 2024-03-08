

namespace Student_Registrationsystem.Models

{
    public class Students
    {
        public int Id { get; set; }
        public string std_Fullname { get; set; }    
        public string std_Phone { get; set; } 
        public int std_age { get; set;}
        internal static void Add(Students students)
        {
            throw new NotImplementedException();
        }

    }
}
