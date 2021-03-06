﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using BE;
using DAL;
using System.Collections.Specialized;
using System.Threading;

namespace BL
{
    public class MyBL : IBL
    {
        #region event
        public event EventHandler<EventArgs> TraineeEvent { add { MyDal.TraineeEvent += value; } remove { MyDal.TraineeEvent -= value; } }

        public event EventHandler<EventArgs> TesterEvent { add { MyDal.TesterEvent += value; } remove { MyDal.TesterEvent -= value; } }

        public event EventHandler<EventArgs> TestEvent { add { MyDal.TestEvent += value; } remove { MyDal.TestEvent -= value; } }
#endregion

        #region Singleton
        private static readonly MyBL instance = new MyBL();

        public static MyBL Instance
        {
            get { return instance; }
        }
        #endregion

        static IDAL MyDal;

        #region Constructor

        private MyBL() { }

        static MyBL()
        {
            MyDal = FactoryDAL.getDAL(Configuration.DALType);
        }

        #endregion
      
        #region Tester

        /// <summary>
        /// return the minimun age for a tester
        /// </summary>
        /// <returns></returns>
        public int getMinimumAgeOfTester()
        {

            return Configuration.MinAgeTester;
        }

        /// <summary>
        /// return the maximun age for a tester
        /// </summary>
        /// <returns></returns>
        public int getMaximumAge()
        {
            return Configuration.MaxAgeTester;
        }

        /// <summary>
        /// add a tester to the data source
        /// </summary>
        /// <param name="tester"></param>
        public void addTester(Tester tester)
        {
            if (tester.Age < BE.Configuration.MinAgeTrainee)
            {
                throw new Exception("too young, can't be a tester");
            }
            MyDal.AddTester(tester);

        }

        /// <summary>
        /// update tester, include update id. need the old id
        /// </summary>
        /// <param name="tester"></param>
        /// <param name="old_Id"></param>
        public void updateTester(Tester tester, string old_Id)
        {
            if (tester.Age < BE.Configuration.MinAgeTrainee)
            {
                throw new Exception("too young, can't be a tester");
            }
            MyDal.UpdateTester(tester, old_Id);
        }

        /// <summary>
        /// delete a tester from the data source
        /// </summary>
        /// <param name="tester"></param>
        public void deleteTester(Tester tester)
        {
            MyDal.DeleteTester(tester);
        }

        /// <summary>
        /// return list of all the tester 
        /// </summary>
        /// <returns></returns>
        public List<Tester> getAllTester()
        {
            return MyDal.getAllTesters();
        }

        /// <summary>
        /// update a tester
        /// </summary>
        /// <param name="tester"></param>
        public void updateTester(Tester tester)
        {
            if (tester.Age < BE.Configuration.MinAgeTrainee)
            {
                throw new Exception("too young, can't be a tester");
            }
            MyDal.UpdateTester(tester);
        }

        /// <summary>
        /// return true if the tester Available at this date and hour
        /// </summary>
        /// <param name="testerId"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public bool isAvailableAtDate(string testerId, DateTime date)
        {
            return testersAvailableAtDateAndHour(date).Find(item => item.Id == testerId) != null;
        }

        /// <summary>
        /// return true if the tester with this id can do a test at this address 
        /// </summary>
        /// <param name="testerId"></param>
        /// <param name="address"></param>
        /// <returns></returns>
        public bool atAvailbleDistance(string testerId, Address address)
        {
            Tester tester = MyDal.findTester(testerId);
            return distanceFromAddress(tester.Address, address) <= tester.Distance;
        }

        /// <summary>
        /// return the tester by his id
        /// </summary>
        /// <param name="testerId"></param>
        /// <returns></returns>
        public Tester findTester(string testerId)
        {
            return MyDal.findTester(testerId);
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

        /// <summary>
        /// calculate the distance from the address. need network connection
        /// </summary>
        /// <param name="addressSource"></param>
        /// <param name="addressDestination"></param>
        /// <returns></returns>
        private double distanceFromAddress(Address addressSource, Address addressDestination)
        {
            double value = 1;
            var thread = new Thread(() =>
            {
                value = (DAL.distance_calculation.distanceCalculation(addressSource, addressDestination));
            });
            thread.Start();
            thread.Join();
            return value;
            // Random random = new Random();
            //double e = random.Next() % 5;
            //return e;
        }

        /// <summary>
        /// return all the tester that available at this date and this hour
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public List<Tester> testersAvailableAtDateAndHour(DateTime date)
        {
            if (date.Hour > 14 || date.Hour < 9)//testers works hour is between 9:00 -15:00
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
        /// return list of the available tester at this date with this Specialization
        /// </summary>
        /// <param name="date"></param>
        /// <param name="car"></param>
        /// <param name="gearBox"></param>
        /// <returns></returns>
        public List<Tester> testersAvailableAtDateBySpecialization(DateTime date, CarType car, GearBox gearBox)
        {
            var availbeleTesters = testersAvailableAtDate(date);
            var specTesters = from item in availbeleTesters
                              where item.CarType == car && item.GearBox == gearBox
                              select item;
            return specTesters.ToList();
        }

        /// <summary>
        /// return list of the available tester at this date and at this hour with this Specialization
        /// </summary>
        /// <param name="dateAndHour"></param>
        /// <param name="trainee"></param>
        /// <returns></returns>
        public List<Tester> testersAvailableAtDateAndHourBySpecialization(DateTime dateAndHour, Trainee trainee)
        {
            var allTestersAtDate = testersAvailableAtDateAndHour(dateAndHour);
            var specTesters = from item in allTestersAtDate
                              where item.GearBox == trainee.GearBox && item.CarType == trainee.CarType
                              select item;
            return specTesters.ToList();
        }

        /// <summary>
        /// return if the tester work at this day
        /// </summary>
        /// <param name="tester"></param>
        /// <param name="dayOfWeek"></param>
        /// <returns></returns>
        public bool isWorkAtThisDay(Tester tester, DayOfWeek dayOfWeek)
        {
            if ((int)dayOfWeek > 4) //Friday or Saturday
                return false;
            for (int i = 0; i < tester.WorkHour[(int)dayOfWeek].Length; i++)
            {
                if (tester.WorkHour[(int)dayOfWeek][i])
                    return true;
            }
            return false;
        }

        /// <summary>
        /// return list of the available tester at this date and at this hour with this Specialization and at this address
        /// </summary>
        /// <param name="dateAndHour"></param>
        /// <param name="trainee"></param>
        /// <param name="address"></param>
        /// <returns></returns>
        public List<Tester> testersAvailableAtDateAndHourBySpecializationAndAddress(DateTime dateAndHour, Trainee trainee, Address address)
        {
            var x = testersAvailableAtDateAndHourBySpecialization(dateAndHour, trainee);
            x = (from i in x where atAvailbleDistance(i.Id, address) select i).ToList();
            return x;
        }

        /// <summary>
        /// return list of the available tester at this date and with this Specialization and at this address
        /// </summary>
        /// <param name="date"></param>
        /// <param name="car"></param>
        /// <param name="gearBox"></param>
        /// <param name="address"></param>
        /// <returns></returns>
        public List<Tester> testersAvailableAtDateBySpecializationAndAddress(DateTime date, CarType car, GearBox gearBox, Address address)
        {
            var x = testersAvailableAtDateBySpecialization(date, car, gearBox);
            x = (from i in x where atAvailbleDistance(i.Id, address) select i).ToList();
            return x;
        }

        #endregion

        #region trainee

        /// <summary>
        /// return the minimun age for a trainee
        /// </summary>
        /// <returns></returns>
        public int getMinimumAge()
        {

            return Configuration.MinAgeTrainee;
        }

        /// <summary>
        /// add new trainee to the data source
        /// </summary>
        /// <param name="trainee"></param>
        public void addTrainee(Trainee trainee)
        {
            checkTrainee(trainee);
            MyDal.AddTrainee(trainee);
        }

        /// <summary>
        /// delete a trainee from the data source
        /// </summary>
        /// <param name="trainee"></param>
        public void deleteTrainee(Trainee trainee)
        {
            MyDal.DeleteTrainee(trainee);
        }

        /// <summary>
        /// update a trainee
        /// </summary>
        /// <param name="trainee"></param>
        public void updateTrainee(Trainee trainee)
        {
            checkTrainee(trainee);
            MyDal.UpdateTrainee(trainee);
        }

        /// <summary>
        /// check if the details of the trainee if acceptable
        /// </summary>
        /// <param name="trainee"></param>
        private void checkTrainee(Trainee trainee)
        {
            if (trainee.Age < Configuration.MinAgeTrainee)
                throw new Exception("cannot add a trainee too young");
            //small truck nedd license in privete car, and truck need license in small truck, the enum value of the car type is in order of the level of the license
            if ((int)(trainee.CarType) >= (int)CarType.SmallTruck && !haveLicense(trainee.Id, trainee.GearBox, (CarType)((int)trainee.CarType - 1)))
                throw new Exception("the trainee does not meet the conditions required for driving license, need license in " + ((CarType)((int)trainee.CarType) - 1));
        }

        /// <summary>
        /// return list of all the trainee
        /// </summary>
        /// <returns></returns>
        public List<Trainee> getAllTrainees()
        {
            return MyDal.getAllTrainees();
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

        /// <summary>
        /// update a trainee, include id. need the old id
        /// </summary>
        /// <param name="trainee"></param>
        /// <param name="old_Id"></param>
        public void updateTrainee(Trainee trainee, string old_Id)
        {
            if (trainee.Age >= Configuration.MinAgeTrainee)
                MyDal.UpdateTrainee(trainee, old_Id);
            else
                throw new Exception("cannot update, the trainee too young");
        }

        /// <summary>
        /// return trainee by his id
        /// </summary>
        /// <param name="traineeId"></param>
        /// <returns></returns>
        public Trainee findTraine(string traineeId)
        {
            return MyDal.findTrainee(traineeId);
        }

        /// <summary>
        /// return if person have a license at this kind of car and gearbox
        /// </summary>
        /// <param name="id"></param>
        /// <param name="gearBox"></param>
        /// <returns></returns>
        public bool haveLicense(string id, GearBox gearBox, CarType carType)
        {
            return (MyDal.GetTestList(item => item.TraineeId == id && (int)(item.GearBox) <= (int)gearBox && item.Car == carType && item.Succeeded == true).Count != 0) ? true : false; //if the trainee have a license in manual gearbox he can drive on auto gearbox but not vice versa
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

        #endregion trainee

        #region test

        /// <summary>
        /// add new test to the data source
        /// </summary>
        /// <param name="test"></param>
        public void AddTest(Test test)
        {
            Trainee trainee = MyDal.findTrainee(test.TraineeId); //in update test and finish test its always false, then check it only in add test
            var temp = MyDal.GetTestList(item => item.TraineeId == trainee.Id && item.GearBox == trainee.GearBox && item.Car == trainee.CarType);
            if (temp.Count > 0 && Math.Abs((temp.Max(item => item.Date) - test.Date).Days) < Configuration.IntervalBetweenTest.Days) //get all the test of this trainee on this gearbox and select the test with the later date and check if past enough days from this test (first check if there are any test records and after search in them, max cant work on empty list) 
            {
                throw new Exception("cant regist, the trainee did his last test not long ago");
            }
            checkIfValidTest(test);

            MyDal.AddTest(test);
        }

        /// <summary>
        /// check if the test is valid and acceptable
        /// </summary>
        /// <param name="test"></param>
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

            if (MyDal.GetTestList(item => item.Date == test.Date && item.TraineeId == test.TraineeId).Count != 0)//if there is another test of this trainee at the same time
                throw new Exception("cant regist, the traniee do another test at the same time");

            if (!isAvailableAtDate(test.TesterId, test.Date))
                throw new Exception("cant regist, the tester do another test at the same time");
          

            if (trainee.NumberOfLesson < Configuration.MinNumLessons)
            {
                throw new Exception("cant regist, the trainee didnt learn enough lessons");
            }

            if (tester.MaxTestInWeek <= MyDal.GetTestList(item => item.TesterId == tester.Id && item.Date.AddDays(-1 * (int)item.Date.DayOfWeek) == test.Date.AddDays(-1 * (int)test.Date.DayOfWeek)).Count)//get all the test that the tester was the same as our test and their date was at the same week (by check their day in the week and subtract it from their date) and count it and check if it is more then the max tests of this tester  
            {
                throw new Exception("cant regist, the tester did his max test this week");
            }

            if (haveLicense(trainee.Id, trainee.GearBox, trainee.CarType))
            {
                throw new Exception("cant regist, this trainee have licence on this kind of car gearbox");
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
     
        /// <summary>
        /// delete a test from the data source
        /// </summary>
        /// <param name="test"></param>
        public void DeleteTest(Test test)
        {
            MyDal.DeleteTest(test);
        }

        /// <summary>
        /// return list of all the test
        /// </summary>
        /// <returns></returns>
        public List<Test> getAllTests()
        {
            return MyDal.getAllTests();
        }

        /// <summary>
        /// return the successes percentage of a trainee from every school, of every teacher, and with every tester. have 3 mod tester school teacher
        /// </summary>
        /// <param name="mod"></param>
        /// <returns></returns>
        public IEnumerable<object> successesPercentage(string mod)
        {
           
            switch (mod)
            {
                //return list of elements. every elemnts have id of tester and percentage of successes in his tests
                case "tester":
                    var tester = from test in MyDal.getAllTests()
                                 where test.Succeeded != null
                                 select test
                                 into tests //list of all the test that was done
                                 group tests by tests.TesterId
                                into groupTesters //group to gruops that every gruop contain the tester id and his tests
                                from i in MyDal.getAllTesters()
                                where i.Id == groupTesters.Key
                                 select new { tester_ID = groupTesters.Key,name = i.FirstName+" "+i.LastName ,p = ((double)groupTesters.Count(x => x.Succeeded == true) / groupTesters.Count()).ToString("0.0%") };//return anonymous element that comtain the tester id and the percentage of successes in his tests in sting fotmat of percentage

                    return tester.ToList();
                //return list of elements that contain schools name and percentage of successes in tests of its trainee
                case "school":
                    var schoolP = from test in MyDal.getAllTests()
                                  where test.Succeeded != null
                                  select test
                    into finshedTest //list of all the test that was done
                                  group finshedTest by new { id = finshedTest.TraineeId, CarType = finshedTest.Car, GearBox = finshedTest.GearBox }
                     into groupTest //group the test by trianees and the kind of car for the test
                                  from trainee in MyDal.getAllTrainees()
                                  where trainee.Id == groupTest.Key.id&&trainee.GearBox==groupTest.Key.GearBox&&trainee.CarType==groupTest.Key.CarType //for every match trianee, car type, gearbox and test-group 
                                  select new { trainee, success = groupTest.Count(i => i.Succeeded == true), count = groupTest.Count() }
                            into traineeList //make element that contain the trainee and the number of success in test (0 or 1) and the number of all the test that this trainee do in this kind of car 
                                  group traineeList by traineeList.trainee.SchoolName 
                            into school
                                  select new { school = school.Key, p = ((double)school.Sum(i => i.success) / school.Sum(i => i.count)).ToString("0.0%"), firstTest = ((double)school.Count(i => i.success == 1 && i.success == i.count) / school.Count()).ToString("0.0%") };
                    //the final element contain the school name, the percentage of suuccess, the percentage of first test success
                    return schoolP.ToList();

                //return list of elements that contain schools name and teachers names and percentage of successes in tests of its trainee
                case "teacher":
                    var teacherP = from test in MyDal.getAllTests()
                                   where test.Succeeded != null
                                   select test
                    into finshedTest //list of all the test that was done
                                   group finshedTest by new { id = finshedTest.TraineeId, CarType = finshedTest.Car, GearBox = finshedTest.GearBox }
                     into groupTest //group the test by trianees and the kind of car for the test
                                   from trainee in MyDal.getAllTrainees()
                                   where trainee.Id == groupTest.Key.id && trainee.GearBox == groupTest.Key.GearBox && trainee.CarType == groupTest.Key.CarType //for every match trianee, car type, gearbox and test-group 
                                   select new { trainee, success = groupTest.Count(i => i.Succeeded == true), count = groupTest.Count() }
                            into traineeList //make element that contain the trainee and the number of success in test (0 or 1) and the number of all the test that this trainee do in this kind of car 
                                   group traineeList by new { school = traineeList.trainee.SchoolName, teacher = traineeList.trainee.TeacherName }
                            into schoolAndTeacher
                                   select new
                                   {
                                       schoolAndTeacher = schoolAndTeacher.Key,
                                       p = ((double)schoolAndTeacher.Sum(i => i.success) / schoolAndTeacher.Sum(i => i.count)).ToString("0.0%"),
                                       firstTest = ((double)schoolAndTeacher.Count(i => i.success == 1 && i.success == i.count) / schoolAndTeacher.Count()).ToString("0.0%")
                                   };
                    //the final element contain the school name,the teacher name (both in key), the percentage of suuccess, the percentage of first test success
                    return teacherP.ToList();

                default:
                    return null;
            }
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
                throw new Exception("cant update test that done, for insert test result use the right function");
            if (test.Test1_ReverseParking != null || test.Test2_KeepingSafeDistance != null || test.Test3_UsingMirrors != null || test.Test4_UsingTurnSignals != null || test.Test5_LegalSpeed != null)
                throw new Exception("cant insert the result of any of the test check before finished");
            Test orginalTest = MyDal.findTest(test);
            DeleteTest(orginalTest); //to update just remove the last one and try to insert new one' if its work good, else return the old one and throw exeption 
            try
            {
                AddTest(test);
            }
            catch (Exception msg)
            {
                AddTest(orginalTest);
                throw msg;
            }
        }

        /// <summary>
        /// use to insert test results, can calculate if pass or not
        /// </summary>
        /// <param name="test"></param>
        public void completedTest(Test test)
        {
            if ((test.Test1_ReverseParking == null || test.Test2_KeepingSafeDistance == null || test.Test3_UsingMirrors == null || test.Test4_UsingTurnSignals == null || test.Test5_LegalSpeed == null))
                throw new Exception("The test did not end until all the tests were over");
            if (MyDal.findTest(test)==null)
                throw new Exception("the test not exist at the data base");
            if (test.Succeeded == null) //if the trainne pass most of the tests
                test.Succeeded = test.Grade > Configuration.minGradeForPassTest ? true : false;
            MyDal.UpdateTest(test);
        }

        /// <summary>
        /// return list of all the test at specific date
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public List<Test> allTheTestAtDate(DateTime date)
        {
            return MyDal.GetTestList(item => item.Date.Date == date.Date);//compare only the date of the test
        }

        /// <summary>
        /// return all the test at specific month
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public List<Test> allTheTestAtMonth(DateTime date)
        {
            return MyDal.GetTestList(item => item.Date.Year == date.Year && item.Date.Month == date.Month);//return all the test in the same year and month
        }

        /// <summary>
        /// return the first open date to do a test from the input date
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public DateTime NearestOpenDate(DateTime date)
        {
            while (testersAvailableAtDate(date).Count == 0)
            {
                date = date.AddDays(1);
            }
            return date.Date;
        }

        /// <summary>
        /// the nearest date available from tommorow to do a test
        /// </summary>
        /// <returns></returns>
        public DateTime NearestOpenDate()
        {
            return NearestOpenDate(DateTime.Now.AddDays(1));
        }

        /// <summary>
        /// return if true there are any tester that can do a test at this date
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public bool isAvailableDate(DateTime date)
        {
            if (testersAvailableAtDate(date).Count == 0)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// return list of all the test at the range of the date
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public List<Test> allTheTestAtRange(DateTime start, DateTime end)
        {
            return MyDal.GetTestList(item => item.Date.Date >= start.Date && item.Date.Date <= end.Date);
        }

        /// <summary>
        ///  return the first open date to do a test in this kind of car and gear box from the input date
        /// </summary>
        /// <param name="carType"></param>
        /// <param name="gearBox"></param>
        /// <param name="inputDate"></param>
        /// <returns></returns>
        public DateTime NearestOpenDateByspecialization(CarType carType, GearBox gearBox, DateTime? inputDate)
        {
            if (MyDal.getAllTesters().Find(i => i.CarType == carType && i.GearBox == gearBox) == null)
                throw new Exception("there are no tester for this type of car and gearbox");
            DateTime date;
            if (inputDate == null)
                date = NearestOpenDate();
            else
                date = (DateTime)inputDate;
            while (testersAvailableAtDateBySpecialization(date, carType, gearBox).Count == 0)
            {
                date = date.AddDays(1);
            }
            return date.Date;

        }

        /// <summary>
        /// return the first open date to do a test in this kind of car and gear box at this address from the input date
        /// </summary>
        /// <param name="carType"></param>
        /// <param name="gearBox"></param>
        /// <param name="inputDate"></param>
        /// <param name="address"></param>
        /// <returns></returns>
        public DateTime NearestOpenDateBySpecializationAndAddress(CarType carType, GearBox gearBox, DateTime? inputDate, Address address)
        {
            //if there are no relevant tester at all, there is no meaning in check the date, and the loop continue forever
            if (MyDal.getAllTesters().Find(i => i.CarType == carType && i.GearBox == gearBox) == null)
                throw new Exception("there are no tester for this type of car and gearbox");
            if (MyDal.getAllTesters().Find(i => i.CarType == carType && i.GearBox == gearBox&&distanceFromAddress( i.Address,address)<=i.Distance) == null) //to minimalize the number of the cull to the web, check only for relevant tester 
                throw new Exception("there are no tester with this type of car and gearbox available at this address");

            DateTime date;
            if (inputDate == null)
                date = NearestOpenDate();
            else
                date = (DateTime)inputDate;
            while (testersAvailableAtDateBySpecializationAndAddress(date, carType, gearBox,address).Count == 0)
            {
                date = date.AddDays(1);
            }
            return date.Date;
        }

        #endregion
    }
}