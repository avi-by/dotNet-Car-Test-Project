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
       
      

        static IDAL MyDal = DAL.FactoryDAL.getDAL("List");

        #region Constructor

        private MyBL() { }

        static MyBL()
        {
            MyDal = FactoryDAL.getDAL(Configuration.DALType);
        }

        public int getMinimumAge()
        {
           
            return Configuration.MinAgeTrainee;
        }

        public int getMaximumAge()
        {
            return Configuration.MaxAgeTester;
        }

        #endregion

       
        #region Tester
        

        public void AddTester(Tester t)
        {
            
            DateTime fourtyYearsOld = t.BirthDay.AddYears(40);
            DateTime currentDate = DateTime.Now;
            if (currentDate < fourtyYearsOld)
                MyDal.AddTester(t);
            else
                throw new Exception ("cannot add a tester over the age of 40");
        }

        public List<Tester> getAllTesters()
        {
            return MyDal.getAllTesters();
        }


        public void DeleteTester(Tester tester)
        {
            MyDal.DeleteTester(tester);
        }

        public void UpdateTester(Tester tester)
        {
            MyDal.UpdateTester(tester);
        }

        #endregion
        #region trainee
        public void AddTrainee(Trainee trainee)
        {
            DateTime eighteenYearsOld = trainee.BirthDay.AddYears(18);
            DateTime currentDate = DateTime.Now;
            if (currentDate > eighteenYearsOld)
                MyDal.AddTrainee(trainee);
            else
                throw new Exception("cannot add a trainee under the age of 40");
        }

        public void DeleteTrainee(Trainee trainee)
        {
            MyDal.DeleteTrainee(trainee);
        }

        public void UpdateTrainee(Trainee trainee)
        {
            MyDal.UpdateTrainee(trainee);
        }


        public List<Trainee> getAllTrainees()
        {
            return MyDal.getAllTrainees();
        }

        #endregion 

        public void AddTest(Test test)
        {
          
        }

        public void DeleteTest(Test test)
        {
            foreach (Test item in DataSource.tests)
            {
                if (item.Id == test.Id)
                {
                    DataSource.tests.Remove(item);
                    return;
                }
                throw new Exception("failed to remove, test with the same ID not found");

            }
        }

        public void UpdateTest(Test test)
        {
            int index = 0; ;
            foreach (Test item in DataSource.tests)
            {
                if (item.Id == test.Id)
                {
                    DataSource.tests[index] = test;
                    break;
                }
                index++;
            }
            throw new Exception("failed to update, test with the same ID not found");
        }

        public List<Test> getAllTests()
        {
            throw new NotImplementedException();
        }
    }
}