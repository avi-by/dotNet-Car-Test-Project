using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Tester
    {
        private string id;
        private string firstName;
        private string lastName;
        private DateTime birthDay;
        private Gender gender;
        private string phoneNumber;
        private Address address;
        private int expYears;
        private int maxTestInWeek;
        private CarType carType;
        private GearBox gearBox;
        private bool[,] workHour;
        private double distance;

        public Tester(string id, string firstName, string lastName, DateTime birthDay, Gender gender, string phoneNumber, Address address, int expYears, int maxTestInWeek, CarType carType, GearBox gearBox, bool[,] workHour, double distance)
        {
            this.id = id;
            this.firstName = firstName;
            this.lastName = lastName;
            this.birthDay = birthDay;
            this.gender = gender;
            this.phoneNumber = phoneNumber;
            this.address = address;
            this.expYears = expYears;
            this.maxTestInWeek = maxTestInWeek;
            this.carType = carType;
            this.gearBox = gearBox;
            this.workHour = workHour;
            this.distance = distance;
        }

        public string Id { get => id; set => id = value; }
        public string FirstName { get => firstName; set => firstName = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public DateTime BirthDay { get => birthDay; set => birthDay = value; }
        public Gender Gender { get => gender; set => gender = value; }
        public string PhoneNumber { get => phoneNumber; set => phoneNumber = value; }
        public Address Address { get => address; set => address = value; }
        public int ExpYears { get => expYears; set => expYears = value; }
        public int MaxTestInWeek { get => maxTestInWeek; set => maxTestInWeek = value; }
        public CarType CarType { get => carType; set => carType = value; }
        public GearBox GearBox { get => gearBox; set => gearBox = value; }
        public bool[,] WorkHour { get => workHour; set => workHour = value; }
        public double Distance { get => distance; set => distance = value; }

        public override string ToString()
        {
            return FirstName + " " + LastName + " id:" + Id;
        }
    }
}
