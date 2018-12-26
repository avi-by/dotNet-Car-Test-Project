using System;
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
                if (tester.Age<BE.Configuration.MinAgeTrainee)
                {
                    throw new Exception("too young, cant be a tester");
                }
                MyDal.AddTester(tester);
            
        }

        public void updateTester(Tester tester, string id)
        {
            MyDal.UpdateTester(tester,id);
        }

        public void deleteTester(Tester tester)
        {
            MyDal.DeleteTester(tester);
        }

        public List<Tester> getAllTester()
        {
            return MyDal.getAllTesters();
        }

        //public void updateTester(Tester tester, string name)
        //{
        //    throw new NotImplementedException();
        //}

        //void updateTester(Tester tester, Tester newtester)
        //{
        //    MyDal.UpdateTester(tester, newtester);
        //}
        //public void updateTester(Tester tester, Tester newtester)
        //{
        //    throw new NotImplementedException();
        //}

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

        //public void updateTrainee(Trainee trainee, Trainee newTrainee)
        //{
        //    throw new NotImplementedException();
        //}

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
                List<Test> arr = MyDal.GetTestList(item => ((Test)item).Date == test.Date && (((Test)item).TraineeId == test.TraineeId));
            
            if (arr != null)
                    throw new Exception("cant regist, the traniee do another test at the same time");
                arr = MyDal.GetTestList(item => ((Test)item).Date == test.Date && ((Test)item).TesterId == test.TesterId);
                if (arr != null)
                    throw new Exception("cant regist, the tester do another test at the same time");
            if (trainee.AmountOfTests>0)
            {
                if ((test.Date - MyDal.GetTestList(item=>item.TraineeId==trainee.Id&&test.GearBox==trainee.GearBox).Max(item=>item.Date)).Days<Configuration.IntervalBetweenTest.Days) //get all the test of this trainee on this gearbox and select the test with the later date and check if past enough days from this test 
                {
                    throw new Exception("cant regist, the trainee did his last test not long ago");
                }
            }
            if (trainee.NumberOfLesson<Configuration.MinNumLessons)
            {
                throw new Exception("cant regist, the trainee didnt learn enough lessons");
            }
            
            if (tester.MaxTestInWeek>=MyDal.GetTestList(item=>item.TesterId==tester.Id&&item.Date.AddDays(-1*(int)item.Date.DayOfWeek)== test.Date.AddDays(-1 * (int)test.Date.DayOfWeek)).Count)//get all the test that the tester was the same as our test and their date was at the same week (by check their day in the week and subtract it from their date) and count it and check if it is more then the max tests of this tester  
            {
                throw new Exception("cant regist, the tester did his max test this week");
            }
            arr = MyDal.GetTestList(item => ((Test)item).GearBox == trainee.GearBox && ((Test)item).TraineeId == trainee.Id&&((Test)item).Succeeded); //all the test of this trainee on this kind of gear box that he pass 
            if (arr != null)
            {
                throw new Exception("cant regist, this trainee have licence on this kind of gearbox");
            }
            if (trainee.GearBox != tester.GearBox)
            {
                throw new Exception("cant regist, this tester cant test on this kind of gearbox");
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

      
        #endregion
    }
}