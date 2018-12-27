using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Trainee :ICloneable
    {
        private string id;
        private string firstName;
        private string lastName;
        private DateTime birthDate;
        private Gender gender;
        private string phoneNumber;
        private Address address;
        private CarType carType;
        private GearBox gearBox;
        private string schoolName;
        private string teacherName;
        private int numberOfLesson;
     //   private int amountOfTests;

        public Trainee(string id, string name, int age, Address address, string schoolName="", string teacherName="", int numberOfLesson=0, string phoneNumber="00000", Gender gender = Gender.MALE, CarType carType = CarType.PrivetCar, GearBox gearBox = GearBox.Manual)
        {
            this.id = id;
            //make from one string first and last name, if there are only one word enter " " to the last name
            string[] seperator = { " " };
            string[] names = name.Split(seperator, 2, StringSplitOptions.RemoveEmptyEntries);
            FirstName = names[0];
            LastName = names[1] != null ? names[1] : " ";
            Age = age;
            Gender = gender;
            PhoneNumber = phoneNumber;
            Address = address;
            CarType = carType;
            GearBox = gearBox;
            SchoolName = schoolName;
            TeacherName = teacherName;
            NumberOfLesson = numberOfLesson;
     //       AmountOfTests = 0;
        }

        public Trainee(string id, string firstName, string lastName, DateTime birthDay, string schoolName, string teacherName, int numberOfLesson, string phoneNumber, Address address, Gender gender=Gender.MALE,  CarType carType=CarType.PrivetCar, GearBox gearBox=GearBox.Manual)
        {
           Id = id;
           FirstName = firstName;
           LastName = lastName;
           BirthDate = birthDay;
           Gender = gender;
           PhoneNumber = phoneNumber;
           Address = address;
           CarType = carType;
           GearBox = gearBox;
           SchoolName = schoolName;
           TeacherName = teacherName;
           NumberOfLesson = numberOfLesson;
        }

        public string Id { get => id; set => id = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public DateTime BirthDate { get => birthDate; set => birthDate = value; }
        public Gender Gender { get => gender; set => gender = value; }
        public string PhoneNumber { get => phoneNumber; set => phoneNumber = value; }
        public Address Address { get => address; set => address = value; }
        public CarType CarType { get => carType; set => carType = value; }
        public GearBox GearBox { get => gearBox; set => gearBox = value; }
        public string SchoolName { get => schoolName; set => schoolName = value; }
        public string TeacherName { get => teacherName; set => teacherName = value; }
        public int NumberOfLesson { get => numberOfLesson; set => numberOfLesson = value; }
        public string FirstName { get => firstName; set => firstName = value; }
        public int Age
        {
            get
            {
                // Save today's date.
                var today = DateTime.Today;
                // Calculate the age.
                var age = today.Year - BirthDate.Year;
                // Go back to the year the person was born in case of a leap year
                if (BirthDate > today.AddYears(-age)) age--;
                return age;
            }
            set => BirthDate = new DateTime(DateTime.Now.Year - value, 1, 1);//default month and day if enter only age
        }

    //    public int AmountOfTests { get => amountOfTests; set => amountOfTests = value; }

        public object Clone()
        {
            Trainee temp = (Trainee)MemberwiseClone();
            temp.Address = new Address(address.street, address.houseNumber, address.city);
            temp.BirthDate = new DateTime(BirthDate.Year, BirthDate.Month, BirthDate.Day);
            return temp;
        }

        public override string ToString()
        {
            return FirstName + " " + LastName + " " + " id:" + Id;
        }
    }
}
