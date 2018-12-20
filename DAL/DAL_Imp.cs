using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DS;

namespace DAL
{
    public class DAL_imp : IDAL
    {
        #region Singleton
        private static readonly DAL_imp instance = new DAL_imp();
        public static DAL_imp Instance
        {
            get { return instance; }
        }

        private DAL_imp() { }
        static DAL_imp() { }

        #endregion

        #region Tester



        public void AddTester(Tester t)
        {
            DataSource.testers.Add(t);
        }

        public List<Tester> getAllTesters()
        {
            throw new NotImplementedException();
        //    return DataSource.testers.Clone().ToList();
        }
      

        public void DeleteTester(Tester tester)
        {
            foreach (Tester item in DataSource.testers)
            {
                if (item.Id == tester.Id)
                {
    
                    DataSource.testers.Remove(item);
                    return;
                }
                throw new Exception("failed to remove, tester with the same ID not found");
                
            }

        }

        public void UpdateTester(Tester tester)
        {
            int index = 0; ;
            foreach (Tester item in DataSource.testers)
            {
                if (item.Id == tester.Id)
                {
                    DataSource.testers[index] = tester;
                    break;
                }
                index++;
            }
            throw new Exception("failed to update, tester with the same ID not found");
        }

        #endregion
        #region trainee
        public void AddTrainee(Trainee t)
        {
            DataSource.trainees.Add(t);
        }

        public void DeleteTrainee(Trainee trainee)
        {
            foreach (Trainee item in DataSource.trainees)
            {
                if (item.Id == trainee.Id)
                {
                    DataSource.trainees.Remove(item);
                    return;
                }
                throw new Exception("failed to remove, trainee with the same ID not found");

            }
        }

        public void UpdateTrainee(Trainee trainee)
        {
            int index = 0; ;
            foreach (Trainee item in DataSource.trainees)
            {
                if (item.Id == trainee.Id)
                {
                    DataSource.trainees[index] = trainee;
                    break;
                }
                index++;
            }
            throw new Exception("failed to update, trainee with the same ID not found");
        }
    

        public List<Trainee> getAllTrainees()
        {
            throw new NotImplementedException();
        }

        #endregion 

        public void AddTest(Test test)
        {
            test.Id = Configuration.test_id.ToString();
            Configuration.test_id++;
            DataSource.tests.Add(test);
        }

        public void DeleteTest(Test test)
        {
            if(DataSource.tests.RemoveAll(item => item.Id == test.Id)==0)
                throw new Exception("failed to remove, test with the same ID not found");

            //}
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