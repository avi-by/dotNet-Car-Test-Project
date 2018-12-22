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
        //public void AddTester(Tester t)
        //{
        //    DataSource.testers.Add(t);
        //}

        //public void AddTester(string name)
        //{
        //    throw new NotImplementedException();
        //}

      
        public void DeleteTester(Tester tester)
        {
            if (DataSource.testers.RemoveAll(item => item.Id == tester.Id) == 0)
                throw new Exception("failed to remove, tester with the same ID not found");

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

        public List<Test> GetTestList(Func<Test, bool> p)
        {
            var x = from item in DataSource.tests
                    where p(item)
                    select item;
            var y = x.ToList<Test>();
            return (y.Clone().ToList());
        }

        public void AddTester(Tester t)
        {
            DataSource.testers.Add(t);
        }
        public List<Tester> getAllTesters()
        {
            return DataSource.testers.Clone().ToList();
        }

        public IEnumerable<object> testerGrouping(string property)
        {
            IEnumerable<object> groupOfTesters = null;
            List<Trainee> listOfTesters = null;
            switch (property)
            {
                case "carType":

                    groupOfTesters = from tester in DataSource.testers
                            group tester by tester.CarType;
                    listOfTesters = (groupOfTesters as List<Trainee>).Clone();
                    break;

                

            }
            return listOfTesters;

        }

        #endregion

        #region trainee
        public void AddTrainee(Trainee t)
        {
            DataSource.trainees.Add(t);
        }

        public void DeleteTrainee(Trainee trainee)
        {
            if (DataSource.trainees.RemoveAll(item => item.Id == trainee.Id) == 0)
                throw new Exception("failed to remove, trainee with the same ID not found");

            
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
            return DataSource.trainees.Clone().ToList();
        }

        public IEnumerable<object> traineeGrouping(string property)
        {
            IEnumerable<object> group = null;
            List<Trainee> listOfTrainees = null;
            switch (property)
            {
                case "schoolName":

                    group = from trainee in DataSource.trainees
                            group trainee by trainee.SchoolName;
                    listOfTrainees = (group as List<Trainee>).Clone();
                    break;

                case "teacherName":
                    group = from trainee in DataSource.trainees
                            group trainee by trainee.TeacherName;
                    listOfTrainees = (group as List<Trainee>).Clone();
                    break;

                case "amountOfTests":
                    group = from trainee in DataSource.trainees
                            group trainee by trainee.AmountOfTests;
                    listOfTrainees = (group as List<Trainee>).Clone();
                    break;

            }
            return listOfTrainees;

        }

        #endregion

        #region test


        public void AddTest(Test test)
        {
            test.Id = Configuration.test_id.ToString();
            Configuration.test_id++;
            DataSource.tests.Add(test);
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
            return DataSource.tests.Clone().ToList();
        }

       
        #endregion test

    }
}