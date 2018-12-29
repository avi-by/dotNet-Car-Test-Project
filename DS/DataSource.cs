using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using System.Collections.Specialized;


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
            
        }

    }
}