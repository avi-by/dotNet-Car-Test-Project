using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DS;
using System.Collections.Specialized;
using System.IO;

namespace DAL
{



    public class DAL_imp : IDAL  
    {
#region Event
        private EventHandler<EventArgs> testerEvent = delegate { }; //to prevent exeption of null event 
        public event EventHandler<EventArgs> TesterEvent { add { testerEvent += value; } remove { testerEvent -= value; } }

        private EventHandler<EventArgs> traineeEvent = delegate { };
        public event EventHandler<EventArgs> TraineeEvent { add { traineeEvent += value; } remove { traineeEvent -= value; } }

        private EventHandler<EventArgs> testEvent = delegate { };
        public event EventHandler<EventArgs> TestEvent { add { testEvent += value; } remove { testEvent -= value; } }
#endregion


        #region Singleton
        private static readonly DAL_imp instance = new DAL_imp();



        public static DAL_imp Instance
        {
            get { return instance; }
        }


        private DAL_imp() {
            if (File.Exists(Dal_XML_imp.TesterPath))
                DataSource.testers = Dal_XML_imp.LoadFromXML<List<Tester>>(Dal_XML_imp.TesterPath);
            if (File.Exists(Dal_XML_imp.TraineePath))
                DataSource.trainees = Dal_XML_imp.LoadFromXML<List<Trainee>>(Dal_XML_imp.TraineePath);
            if (File.Exists(Dal_XML_imp.TestPath))
              DataSource.tests = Dal_XML_imp.Instance.GetAllTests();


        }
        static DAL_imp() { }
        

       

        #endregion
        

        #region Tester

        /// <summary>
        /// delete one tester
        /// </summary>
        /// <param name="tester"></param>
        public void DeleteTester(Tester tester)
        {
            if (DataSource.testers.RemoveAll(item => item.Id == tester.Id) == 0)
                throw new Exception("failed to remove, tester with the same ID not found");
            Dal_XML_imp.DeleteTester(tester);
            testerEvent(this, null);
        }

        /// <summary>
        /// return all the tester by the prdicate
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public List<Tester> GetTestersList(Predicate<Tester> p)
        {
            var x = from item in DataSource.testers
                    where p(item)
                    select item;
            var y = x.ToList<Tester>();
            if (y == null) return null;
            return (y.Clone().ToList());
        }

        /// <summary>
        /// update one tester
        /// </summary>
        /// <param name="tester"></param>
        public void UpdateTester(Tester tester)
        {

            int index = 0;
            foreach (Tester item in DataSource.testers)
            {
                if (item.Id == tester.Id)
                {
                    DataSource.testers[index] = tester;
                    Dal_XML_imp.SaveToXML<List<Tester>>(DataSource.testers, Dal_XML_imp.TesterPath);
                    testerEvent(this, null);
                                      return;
                }
                index++;
            }
            throw new Exception("failed to update, tester with the same ID not found");
        }


        /// <summary>
        /// add one tester
        /// </summary>
        /// <param name="tester"></param>
        public void AddTester(Tester tester)
        {
            if (DataSource.testers.Find(item => item.Id == tester.Id) != null)
                throw new Exception("cant add the tester, there are another tester with the same id");
            DataSource.testers.Add(tester);
           Dal_XML_imp.SaveToXML<List<Tester>>(DataSource.testers, Dal_XML_imp.TesterPath);
            testerEvent(DAL_imp.Instance, null);

        }
        public List<Tester> getAllTesters()
        {
            if (DataSource.testers == null) return null;
            return DataSource.testers.Clone().ToList();
        }


      
        /// <summary>
        /// update one tester, include id update
        /// </summary>
        /// <param name="tester">the new tester</param>
        /// <param name="oldId">the old id</param>
        public void UpdateTester(Tester tester, string oldId)
        {
            int index = 0;
            foreach (Tester item in DataSource.testers)
            {
                if (item.Id == oldId)
                {
                    DataSource.testers[index] = tester;
                    Dal_XML_imp.SaveToXML<List<Tester>>(DataSource.testers, Dal_XML_imp.TesterPath);
                    testerEvent(this, null);
                    return;
                }
                index++;
            }
            throw new Exception("failed to update, the tester old ID not found");

        }

        /// <summary>
        /// return the tester with this id
        /// </summary>
        /// <param name="testerId"></param>
        /// <returns></returns>
        public Tester findTester(string testerId) => DataSource.testers.Find(item => item.Id == testerId);


        #endregion

        #region trainee
        /// <summary>
        /// add one trainee
        /// </summary>
        /// <param name="trainee"></param>
        public void AddTrainee(Trainee trainee)
        {
            if (DataSource.trainees.Find(item => item.Id == trainee.Id) != null)
                throw new Exception("cant add the trainee, there are another trainee with the same id");
            DataSource.trainees.Add(trainee);
            Dal_XML_imp.SaveToXML<List<Trainee>>(DataSource.trainees, Dal_XML_imp.TraineePath);
            traineeEvent(this, null);
        }

        /// <summary>
        /// delete one trainee
        /// </summary>
        /// <param name="trainee"></param>
        public void DeleteTrainee(Trainee trainee)
        {
            if (DataSource.trainees.RemoveAll(item => item.Id == trainee.Id) == 0)
                throw new Exception("failed to remove, trainee with the same ID not found");
            Dal_XML_imp.DeleteTrainee(trainee);
            traineeEvent(this, null);

        }

        /// <summary>
        /// update one trainee.
        /// </summary>
        /// <param name="trainee"></param>
        public void UpdateTrainee(Trainee trainee)
        {
            int index = 0; ;
            foreach (Trainee item in DataSource.trainees)
            {
                if (item.Id == trainee.Id)
                {
                    DataSource.trainees[index] = trainee;
                    Dal_XML_imp.SaveToXML<List<Trainee>>(DataSource.trainees, Dal_XML_imp.TraineePath);
                    traineeEvent(this, null);
                    return;
                }
                index++;
            }
            throw new Exception("failed to update, trainee with the same ID not found");
        }

        /// <summary>
        /// update on trainee, include id update
        /// </summary>
        /// <param name="trainee"></param>
        /// <param name="old_ID"></param>
        public void UpdateTrainee(Trainee trainee, string old_ID)
        {
            int index = 0; ;
            foreach (Trainee item in DataSource.trainees)
            {
                if (item.Id == old_ID)
                {
                    DataSource.trainees[index] = trainee;
                    Dal_XML_imp.SaveToXML<List<Trainee>>(DataSource.trainees, Dal_XML_imp.TraineePath);
                    traineeEvent(this, null);
                    return;
                }
                index++;
            }
            throw new Exception("failed to update, trainee with the same ID not found");

        }

        /// <summary>
        /// return list of all the trainee
        /// </summary>
        /// <returns></returns>
        public List<Trainee> getAllTrainees()
        {
            if (DataSource.trainees == null) return null;
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
                            group trainee by GetTestList(item => item.Car == trainee.CarType && item.GearBox == trainee.GearBox && item.TraineeId == trainee.Id).Count;
                    listOfTrainees = (group as List<Trainee>).Clone();
                    break;

            }
            return listOfTrainees;

        }

        /// <summary>
        /// return the trainee with this id
        /// </summary>
        /// <param name="traineeId"></param>
        /// <returns></returns>
        public Trainee findTrainee(string traineeId) => DataSource.trainees.Find(item => item.Id == traineeId);



        #endregion

        #region test

        /// <summary>
        /// return all the test by the predicate
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public List<Test> GetTestList(Func<Test, bool> p)
        {
            var x = from item in DataSource.tests
                    where p(item)
                    select item;
            var y = x.ToList<Test>();
            if (y == null) return null;
            return (y.Clone().ToList());
        }

        /// <summary>
        /// add new test. it also give the test new id
        /// </summary>
        /// <param name="test"></param>
        public void AddTest(Test test)
        {
            test.Id = Dal_XML_imp.ID_FromConfigXML().ToString("00000000");

            ////add 0 at the start of the id antil there is 8 digits
            //StringBuilder builder = new StringBuilder();
            //for (int i = test.Id.Length; i < 8; i++)
            //    builder.Append(0);
            //builder.Append(test.Id);
            //test.Id = builder.ToString();
            //Configuration.test_id++;
            DataSource.tests.Add(test);

            Dal_XML_imp.AddTest_PartialDetails(test);
            testEvent(this, null);
          
        }

        /// <summary>
        /// delete one test
        /// </summary>
        /// <param name="test"></param>
        public void DeleteTest(Test test)
        {
            foreach (Test item in DataSource.tests)
            {
                if (item.Id == test.Id)
                {
                    DataSource.tests.Remove(item);
                    Dal_XML_imp.DeleteTest(item);
                    testEvent(this, null);
                    return;
                }

               

            }
            throw new Exception("failed to remove, test with the same ID not found");

        }

        /// <summary>
        /// update one test. test id mustn't change
        /// </summary>
        /// <param name="test"></param>
        public void UpdateTest(Test test)
        {
            if (test.Id == null)
            {
                throw new Exception("failed to update, cant make test without ID");
            }
            int index = 0; ;
            foreach (Test item in DataSource.tests)
            {
                if (item.Id == test.Id)
                {
                    DataSource.tests[index] = test;
                    Dal_XML_imp.UpdateTest(test);
                    testEvent(this, null);
                    return;
                }
                index++;
            }
            throw new Exception("failed to update, test with the same ID not found");
        }

        /// <summary>
        /// return list of all the tests
        /// </summary>
        /// <returns></returns>
        public List<Test> getAllTests()
        {
            if (DataSource.tests == null) return null;
            return DataSource.tests.Clone().ToList();
        }

       
        /// <summary>
        /// return the test with the same id from the data source
        /// </summary>
        /// <param name="test"></param>
        /// <returns></returns>
        public Test findTest(Test test)
        {
            return (Test)DataSource.tests.Find(i => i.Id == test.Id).Clone();
        }




        #endregion test

    }
}