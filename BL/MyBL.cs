﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using BE;
using DAL;

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

            if (MyDal.GetTestList(item => item.Date == test.Date && item.TraineeId == test.TraineeId) != null)//if their id another test of this trainee at the same time
                throw new Exception("cant regist, the traniee do another test at the same time");

            if (!isAvailableAtDate(test.TesterId, test.Date))
                throw new Exception("cant regist, the tester do another test at the same time");

            if ((test.Date - MyDal.GetTestList(item => item.TraineeId == trainee.Id && item.GearBox == trainee.GearBox && item.Car == trainee.CarType).Max(item => item.Date)).Days < Configuration.IntervalBetweenTest.Days) //get all the test of this trainee on this gearbox and select the test with the later date and check if past enough days from this test 
            {
                throw new Exception("cant regist, the trainee did his last test not long ago");
            }

            if (trainee.NumberOfLesson < Configuration.MinNumLessons)
            {
                throw new Exception("cant regist, the trainee didnt learn enough lessons");
            }

            if (tester.MaxTestInWeek >= MyDal.GetTestList(item => item.TesterId == tester.Id && item.Date.AddDays(-1 * (int)item.Date.DayOfWeek) == test.Date.AddDays(-1 * (int)test.Date.DayOfWeek)).Count)//get all the test that the tester was the same as our test and their date was at the same week (by check their day in the week and subtract it from their date) and count it and check if it is more then the max tests of this tester  
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

            if (!atAvailbleDistance(tester.Id,test.Address))
            {
                throw new Exception("cant regist, the address is too far to the tester");
            }

            AddTest(test);
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
            var l = from x in lis where distanceFromAddress(x.Address,address) <= distance select x;
            return l.ToList();
        }

        private double distanceFromAddress(Address addressSource,Address addressDestination)
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
        public List<Tester> testersAvailableAtDate(DateTime date)
        {
            //all the test at this date
            var testList = MyDal.GetTestList(item => item.Date == date);
            var allTester = MyDal.getAllTesters();
            //select all the testers that not exist at the list of the tester at the same date and work at this day and hour
            var availableTesters = from item in allTester
                                   where testList.Find(element => element.TesterId == item.Id) == null && item.WorkHour[(int)date.DayOfWeek][date.Hour]
                                   select item;
            return availableTesters.ToList();
        }

        /// <summary>
        /// return if person have a license at this kind of car and gearbox
        /// </summary>
        /// <param name="id"></param>
        /// <param name="gearBox"></param>
        /// <returns></returns>
        public bool haveLicense(string id, GearBox gearBox)
        {
            return (MyDal.GetTestList(item => item.TraineeId == id && item.GearBox == gearBox && item.Succeeded) != null) ? true : false;
        }


        /// <summary>
        /// return true if the trainee pass the test
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool isPassed(string id)
        {
            Trainee trainee = MyDal.findTrainee(id);
            if (MyDal.GetTestList(item => item.Car == trainee.CarType && item.GearBox == trainee.GearBox && item.TraineeId == trainee.Id && item.Succeeded) != null) //all the test of this trainee on this kind of gearbox and car that he pass 
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


        #endregion
    }
}