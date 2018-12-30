using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using BE;
using DAL;
using System.Collections.Specialized;

namespace BL
{
    public class MyBL : IBL
    {

        #region Singleton
        private static readonly MyBL instance = new MyBL();

        public static MyBL Instance
        {
            get { return instance; }
        }
        #endregion

        static IDAL MyDal;

        public event EventHandler<EventArgs> TraineeEvent { add { MyDal.TraineeEvent += value; } remove { MyDal.TraineeEvent -= value; } }

        public event EventHandler<EventArgs> TesterEvent { add { MyDal.TesterEvent += value; } remove { MyDal.TesterEvent -= value; } }

        public event EventHandler<EventArgs> TestEvent { add { MyDal.TestEvent += value; } remove { MyDal.TestEvent -= value; } }

        #region Constructor

        private MyBL() { }

        static MyBL()
        {
            MyDal = FactoryDAL.getDAL(Configuration.DALType);
        }

        #endregion
        public int getMinimumAge()
        {

            return Configuration.MinAgeTrainee;
        }

        public int getMinimumAgeOfTester()
        {

            return Configuration.MinAgeTester;
        }


        public int getMaximumAge()
        {
            return Configuration.MaxAgeTester;
        }



        #region Tester
        public void addTester(Tester tester)
        {
            if (tester.Age < BE.Configuration.MinAgeTrainee)
            {
                throw new Exception("too young, cant be a tester");
            }
            MyDal.AddTester(tester);

        }

        public void updateTester(Tester tester, string id)
        {
            MyDal.UpdateTester(tester, id);
        }

        public void deleteTester(Tester tester)
        {
            MyDal.DeleteTester(tester);
        }

        public List<Tester> getAllTester()
        {
            return MyDal.getAllTesters();
        }






        public void updateTester(Tester tester)
        {
            MyDal.UpdateTester(tester);
        }
        #endregion

        #region trainee
        public void addTrainee(Trainee trainee)
        {

            if (trainee.Age > Configuration.MinAgeTrainee)
                MyDal.AddTrainee(trainee);
            else
                throw new Exception("cannot add a trainee too young");
        }

        public void deleteTrainee(Trainee trainee)
        {
            MyDal.DeleteTrainee(trainee);
        }

        public void updateTrainee(Trainee trainee)
        {
            MyDal.UpdateTrainee(trainee);
        }



        public List<Trainee> getAllTrainees()
        {
            return MyDal.getAllTrainees();
        }



        #endregion trainee
        #region test
        public void AddTest(Test test)
        {
            checkIfValidTest(test);

            MyDal.AddTest(test);
        }

        private void checkIfValidTest(Test test)
        {
            Tester tester = MyDal.findTester(test.TesterId);
            Trainee trainee = MyDal.findTrainee(test.TraineeId);
            if (trainee == null)
            {
                throw new Exception("cant regist, the trainee not exist at the data base");
            }

            if (tester == null)
            {
                throw new Exception("cant regist, the tester not exist at the data base");
            }

            if (MyDal.GetTestList(item => item.Date == test.Date && item.TraineeId == test.TraineeId).Count != 0)//if their id another test of this trainee at the same time
                throw new Exception("cant regist, the traniee do another test at the same time");

            if (!isAvailableAtDate(test.TesterId, test.Date))
                throw new Exception("cant regist, the tester do another test at the same time");
            var temp = MyDal.GetTestList(item => item.TraineeId == trainee.Id && item.GearBox == trainee.GearBox && item.Car == trainee.CarType);
            if (temp.Count > 0 && (test.Date - temp.Max(item => item.Date)).Days < Configuration.IntervalBetweenTest.Days) //get all the test of this trainee on this gearbox and select the test with the later date and check if past enough days from this test (first check if there are any test records and after search in them, max cant work on empty list) 
            {
                throw new Exception("cant regist, the trainee did his last test not long ago");
            }

            if (trainee.NumberOfLesson < Configuration.MinNumLessons)
            {
                throw new Exception("cant regist, the trainee didnt learn enough lessons");
            }

            if (tester.MaxTestInWeek <= MyDal.GetTestList(item => item.TesterId == tester.Id && item.Date.AddDays(-1 * (int)item.Date.DayOfWeek) == test.Date.AddDays(-1 * (int)test.Date.DayOfWeek)).Count)//get all the test that the tester was the same as our test and their date was at the same week (by check their day in the week and subtract it from their date) and count it and check if it is more then the max tests of this tester  
            {
                throw new Exception("cant regist, the tester did his max test this week");
            }

            if (isPassed(trainee.Id))
            {
                throw new Exception("cant regist, this trainee have licence on this kind of gecararbox");
            }
            if (trainee.GearBox != tester.GearBox || trainee.CarType != tester.CarType)
            {
                throw new Exception("cant regist, this tester cant test on this kind of car");
            }

            if (!atAvailbleDistance(tester.Id, test.Address))
            {
                throw new Exception("cant regist, the address is too far to the tester");
            }

            if (test.Succeeded != null && (test.Test1_ReverseParking == null || test.Test2_KeepingSafeDistance == null || test.Test3_UsingMirrors == null || test.Test4_UsingTurnSignals == null || test.Test5_LegalSpeed == null))//the test can not be insert as a finished test if any of the test's check not insert
                throw new Exception("the test can not be insert as a finished test if any of the test's check not insert");
            if (test.Succeeded == true && test.Grade < Configuration.minGradeForPassTest)
            {
                throw new Exception("cant pass the test with grade of " + 100 * test.Grade + " the min grade is: " + Configuration.minGradeForPassTest);
            }
        }

        public void DeleteTest(Test test)
        {
            MyDal.DeleteTest(test);
        }

        public List<Test> getAllTests()
        {
            return MyDal.getAllTests();
        }


        /// <summary>
        /// return all the testers that  at in "distance" from the "address" 
        /// </summary>
        /// <param name="address"></param>
        /// <param name="distance"></param>
        /// <returns></returns>
        public List<Tester> testersAtDistanceFromAddress(Address address, double distance = 5)
        {
            var lis = MyDal.getAllTesters();
            var l = from x in lis where distanceFromAddress(x.Address, address) <= distance select x;
            return l.ToList();
        }

        private double distanceFromAddress(Address addressSource, Address addressDestination)
        {
            Random random = new Random();
            double e = random.Next() % 8;
            return e;
        }

        /// <summary>
        /// return all the tester that available at this date and this hour
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public List<Tester> testersAvailableAtDateAndHour(DateTime date)
        {
            if (date.Hour > 15 || date.Hour < 9)//testers works hour is between 9:00 -15:00
                return new List<Tester>(); //empty list (not null, becuse the result of the linq is also empty list and not null 
            //all the test at this date
            var testList = MyDal.GetTestList(item => item.Date == date);
            var allTester = MyDal.getAllTesters();
            //select all the testers that not exist at the list of the tester at the same date and work at this day and hour
            var availableTesters = from item in allTester
                                   where testList.Find(element => element.TesterId == item.Id) == null && item.WorkHour[(int)date.DayOfWeek][date.Hour - 9]//9:00 is the first slot and its num is 0
                                   select item;
            return availableTesters.ToList();
        }

        /// <summary>
        ///  return all the tester that available at this date
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public List<Tester> testersAvailableAtDate(DateTime date)
        {
            //all the test at this date
            var testList = MyDal.GetTestList(item => item.Date == date);
            var allTester = MyDal.getAllTesters();
            //select all the testers that not exist at the list of the tester at the same date and work at this day
            var availableTesters = from item in allTester
                                   where testList.Find(element => element.TesterId == item.Id) == null && isWorkAtThisDay(item, date.DayOfWeek)
                                   select item;
            return availableTesters.ToList();
        }

        /// <summary>
        /// return if the tester work at this day
        /// </summary>
        /// <param name="tester"></param>
        /// <param name="dayOfWeek"></param>
        /// <returns></returns>
        public bool isWorkAtThisDay(Tester tester, DayOfWeek dayOfWeek)
        {
            for (int i = 0; i < tester.WorkHour[(int)dayOfWeek].Length; i++)
            {
                if (tester.WorkHour[(int)dayOfWeek][i])
                    return true;
            }
            return false;
        }

        /// <summary>
        /// return if person have a license at this kind of car and gearbox
        /// </summary>
        /// <param name="id"></param>
        /// <param name="gearBox"></param>
        /// <returns></returns>
        public bool haveLicense(string id, GearBox gearBox, CarType carType)
        {
            return (MyDal.GetTestList(item => item.TraineeId == id && item.GearBox == gearBox && item.Car == carType && item.Succeeded == true).Count != 0) ? true : false;
        }


        /// <summary>
        /// return true if the trainee pass the test
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool isPassed(string id)
        {
            Trainee trainee = MyDal.findTrainee(id);
            if (MyDal.GetTestList(item => item.Car == trainee.CarType && item.GearBox == trainee.GearBox && item.TraineeId == trainee.Id && item.Succeeded == true).Count != 0) //all the test of this trainee on this kind of gearbox and car that he pass 
                return true;
            return false;
        }

        /// <summary>
        /// return the amount of test that this treinee do on the current kind of car
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int amountOfTests(string id)
        {
            Trainee trainee = MyDal.findTrainee(id);
            return MyDal.GetTestList(item => item.Car == trainee.CarType && item.GearBox == trainee.GearBox && item.TraineeId == trainee.Id).Count;
        }

        public void updateTrainee(Trainee trainee, string id)
        {
            MyDal.UpdateTrainee(trainee, id);
        }


        /// <summary>
        /// return true if the tester Available at this date and hour
        /// </summary>
        /// <param name="testerId"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public bool isAvailableAtDate(string testerId, DateTime date)
        {
            return testersAvailableAtDate(date).Find(item => item.Id == testerId) != null;
        }

        public bool atAvailbleDistance(string testerId, Address address)
        {
            Tester tester = MyDal.findTester(testerId);
            return distanceFromAddress(tester.Address, address) <= tester.Distance;
        }

        /// <summary>
        /// Update of a test that has not yet been performed
        /// </summary>
        /// <param name="test"></param>
        public void updateTest(Test test)
        {
            if (test.Id == null)
            {
                throw new Exception("cant update, test without ID, if you update Use only legitimate tests that have previously entered the system");
            }
            if (test.Succeeded != null)
                throw new Exception("cant update test that done, for insert test resukt use the right function");
            if (test.Test1_ReverseParking != null || test.Test2_KeepingSafeDistance != null || test.Test3_UsingMirrors != null || test.Test4_UsingTurnSignals != null || test.Test5_LegalSpeed != null)
                throw new Exception("cant insert the result of any of the test check before finished");
            checkIfValidTest(test);
            MyDal.UpdateTest(test);
        }

        /// <summary>
        /// use to insert test results, can calculate if pass or not
        /// </summary>
        /// <param name="test"></param>
        public void completedTest(Test test)
        {
            if ((test.Test1_ReverseParking == null || test.Test2_KeepingSafeDistance == null || test.Test3_UsingMirrors == null || test.Test4_UsingTurnSignals == null || test.Test5_LegalSpeed == null))
                throw new Exception("The test did not end until all the tests were over");
            checkIfValidTest(test);
            if (test.Succeeded == null) //if the trainne pass most of the tests
                test.Succeeded = test.Grade > Configuration.minGradeForPassTest ? true : false;
            MyDal.UpdateTest(test);
        }


        public List<Test> allTheTestAtDate(DateTime date)
        {
            return MyDal.GetTestList(item => item.Date.Date == date.Date);//compare only the date of the test
        }

        public List<Test> allTheTestAtMonth(DateTime date)
        {
            return MyDal.GetTestList(item => item.Date.Year == date.Year && item.Date.Month == date.Month);//return all the test in the same year and month
        }


        public DateTime NearestOpenDate(DateTime date)
        {
            while (testersAvailableAtDate(date).Count == 0)
            {
                date = date.AddDays(1);
            }
            return date.Date;
        }

        /// <summary>
        /// the nearest date available from tommorow
        /// </summary>
        /// <returns></returns>
        public DateTime NearestOpenDate()
        {
            return NearestOpenDate(DateTime.Now.AddDays(1));
        }


        #endregion
    }
}