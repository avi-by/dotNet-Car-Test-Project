using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;


namespace BE
{
    public class Tester :ICloneable
    {
        private string id;
        private string firstName;
        private string lastName;
        private DateTime birthDate;
        private Gender gender;
        private string phoneNumber;
        private Address address;
        private int expYears;
        private int maxTestInWeek;
        private CarType carType;
        private GearBox gearBox;
        private bool[][] workHour;
        private double distance;
        
       
        

        public Tester(string name, int age, Gender gender, string phoneNumber, Address address, int expYears, int maxTestInWeek, CarType carType, GearBox gearBox, bool[][] workHour, double distance)
        {
            //make from one string first and last name, if there are only one word enter " " to the last name
            string[] seperator = { " " };
            string[] names = name.Split(seperator, 2, StringSplitOptions.RemoveEmptyEntries);
            FirstName = names[0];
            LastName = names[1] != null ? names[1] : " ";
            Age = age;
            Gender = gender;
            PhoneNumber = phoneNumber;
            Address = address;
            ExpYears = expYears;
            MaxTestInWeek = maxTestInWeek;
            CarType = carType;
            GearBox = gearBox;
            WorkHour = workHour;
            Distance = distance;
   
        }

        private void initilazeSchedule()
        {
            for (var day = DayOfWeek.Sunday; day < DayOfWeek.Friday; day++)
                for (int hour = 0; hour < 6; hour++)
                    WorkHour[(int)day][hour] = false;
        }

        //public Tester(string name, DateTime dateTime)
        //{
        //    //make from one string first and last name, if there are only one word enter " " to the last name
        //    string[] seperator = { " " };
        //    string[] names = name.Split(seperator, 2, StringSplitOptions.RemoveEmptyEntries);
        //    FirstName = names[0];
        //    LastName = names[1] != null ? names[1] : " ";
        //    BirthDate = dateTime;
        //    initilazeSchedule();
        //}

        public Tester(string id, string firstName, string lastName, DateTime birthDay, Address address, Gender gender=Gender.MALE, string phoneNumber=" ", int expYears=0, int maxTestInWeek=0, CarType carType=CarType.PrivetCar, GearBox gearBox=GearBox.Manual, bool[][] workHour=null, double distance=0)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            BirthDate = birthDay;
            Gender = gender;
            PhoneNumber = phoneNumber;
            Address = address;
            ExpYears = expYears;
            MaxTestInWeek = maxTestInWeek;
            CarType = carType;
            GearBox = gearBox;
            //judjed array (2 dimensional array is difficult to binding)
            this.workHour = new bool[5][];
            for (int i = 0; i < this.workHour.Length; i++)
            {
                this.workHour[i] = new bool[6];
            }

            if (workHour == null)
                initilazeSchedule();
            else
            {
                for (var day = DayOfWeek.Sunday; day < DayOfWeek.Friday; day++)
                    for (int hour = 0; hour < 6; hour++)
                        WorkHour[(int)day][hour] = workHour[(int)day][hour];
            }

            Distance = distance;
        }

        public Tester(string firstName, DateTime birthDay, string lastName)
        {
            
            FirstName = firstName;
            LastName = lastName;
            BirthDate = birthDay;
            
        }

        public string Id { get => id; set => id = value; }
        public string FirstName { get => firstName; set => firstName = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public DateTime BirthDate { get => birthDate; set => birthDate = value; }
        public Gender Gender { get => gender; set => gender = value; }
        public string PhoneNumber { get => phoneNumber; set => phoneNumber = value; }
        public Address Address { get => address; set => address = value; }
        public int ExpYears { get => expYears; set => expYears = value; }
        public int MaxTestInWeek { get => maxTestInWeek; set => maxTestInWeek = value; }
        public CarType CarType { get => carType; set => carType = value; }
        public GearBox GearBox { get => gearBox; set => gearBox = value; }
        public bool[][] WorkHour { get => workHour; set => workHour = value; }
        public double Distance { get => distance; set => distance = value; }
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

        public object Clone()
        {
            Tester temp = (Tester)MemberwiseClone();
            temp.Address = new Address(address.street, address.houseNumber, address.city);
            temp.BirthDate = new DateTime(BirthDate.Year, BirthDate.Month, BirthDate.Day);
            return temp;
        }

        public override string ToString()
        {
            return FirstName + " " + LastName + " id:" + Id;
        }
    }
}
