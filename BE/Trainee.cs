using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Trainee
    {
        private string id;
        private string firstName;
        private string lastName;
        private DateTime birthDay;
        private Gender gender;
        private string phoneNumber;
        private Address address;
        private CarType carType;
        private GearBox gearBox;
        private string schoolName;
        private string teacherName;
        private int numberOfLesson;
        private string name;
        private int age;

        public Trainee(string id, string name, int age)
        {
            this.id = id;
            this.name = name;
            this.age = age;
        }

        public Trainee(string id, string firstName, string lastName, DateTime birthDay, Gender gender, string phoneNumber, Address address, CarType carType, GearBox gearBox, string schoolName, string teacherName, int numberOfLesson)
        {
            this.id = id;
            this.firstName = firstName;
            this.lastName = lastName;
            this.birthDay = birthDay;
            this.gender = gender;
            this.phoneNumber = phoneNumber;
            this.address = address;
            this.carType = carType;
            this.gearBox = gearBox;
            this.schoolName = schoolName;
            this.teacherName = teacherName;
            this.numberOfLesson = numberOfLesson;
        }

        public string Id { get => id; set => id = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public DateTime BirthDay { get => birthDay; set => birthDay = value; }
        public Gender Gender { get => gender; set => gender = value; }
        public string PhoneNumber { get => phoneNumber; set => phoneNumber = value; }
        public Address Address { get => address; set => address = value; }
        public CarType CarType { get => carType; set => carType = value; }
        public GearBox GearBox { get => gearBox; set => gearBox = value; }
        public string SchoolName { get => schoolName; set => schoolName = value; }
        public string TeacherName { get => teacherName; set => teacherName = value; }
        public int NumberOfLesson { get => numberOfLesson; set => numberOfLesson = value; }
        public string FirstName { get => firstName; set => firstName = value; }
        public override string ToString()
        {
            return FirstName+" "+LastName+" "+" id:"+Id;
        }
    }
}
