using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace DS
{
    public class DataSource
    {
        public static List<Tester> testers;
        public static List<Trainee> trainees;
        public static List<Test> tests;
        

        static DataSource()
        {
            testers = new List<Tester>();
            trainees = new List<Trainee>();
            tests = new List<Test>();
            Test test = new Test("12345666", "12345670", new DateTime(2019, 2, 3), new Address("hacotel", 5, "jerusalem"), GearBox.Manual, CarType.PrivetCar);
            test.Notes = "this is an exemple, the code is in DS";
            tests.Add(test);
        }

        

    }
}