using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Configuration
    {
        public static string DALType = "List";
        public static string BLType = "myBL";
        public static int MaxAgeTester = 40;
        public static int MinAgeTester = 20;

        public static int MinNumLessons = 20;
        public static int MinAgeTrainee = 18;
        public static TimeSpan IntervalBetweenTest = new TimeSpan(7, 0, 0, 0, 0);
        public static int test_id = 0;
        public static double minGradeForPassTest = 0.5;

    }
    //check
}
