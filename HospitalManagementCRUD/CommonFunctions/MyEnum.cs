namespace HospitalManagementCRUD.CommonFunctions
{
    public class MyEnum
    {
        public enum Role
        {
            Admin = 29,
            Doctor = 30,
            Patient = 31
        }

        public enum Gender
        {
            Male = 1,
            Female = 2,
        }

        public enum Status
        {
            Booked = 3,
            Completed = 4,
            Confirmed = 5,
            Cancelled = 6,
            Rescheduled = 7,
            NoShow = 8
        }

        public enum Specialization
        {
            GeneralPhysician = 9,
            Cardiologist = 10,
            Dermatologist = 11,
            Neurologist = 12,
            OrthopedicSurgeon = 13,
            Pediatrician = 14,
            Gynecologist = 15,
            Psychiatrist = 16,
            ENT_Specialist = 17,
            Ophthalmologist= 18,
            Dentist = 19,
            Urologist = 20,
            Nephrologist = 21,
            Gastroenterologist = 22,
            Pulmonologist = 23,
            Oncologist = 24,
            Endocrinologist = 25,
            Radiologist = 26,
            Anesthesiologist = 27,
            Emergency_Medicine = 28
        }
    }
}
