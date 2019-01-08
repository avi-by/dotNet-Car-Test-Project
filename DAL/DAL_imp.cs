using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DS;
using System.Collections.Specialized;

namespace DAL
{

    public class DAL_imp : IDAL  //, INotifyCollectionChanged
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
        private EventHandler<EventArgs> testerEvent = delegate { }; //to prevent exeption of null event 
        public event EventHandler<EventArgs> TesterEvent { add { testerEvent += value; } remove { testerEvent -= value; } }

        private EventHandler<EventArgs> traineeEvent = delegate { };
        public event EventHandler<EventArgs> TraineeEvent { add { traineeEvent += value; } remove { traineeEvent -= value; } }

        private EventHandler<EventArgs> testEvent = delegate { };
        public event EventHandler<EventArgs> TestEvent { add { testEvent += value; } remove { testEvent -= value; } }

        //private void OnNotifyCollectionChanged(NotifyCollectionChangedEventArgs args)
        //{
        //    if (this.CollectionChanged != null)
        //    {
        //        this.CollectionChanged(this, args);
        //    }
        //}

        #region Tester

        public void DeleteTester(Tester tester)
        {
            if (DataSource.testers.RemoveAll(item => item.Id == tester.Id) == 0)
                throw new Exception("failed to remove, tester with the same ID not found");
            testerEvent(this, null);
        }


        public void UpdateTester(Tester tester)
        {

            int index = 0;
            foreach (Tester item in DataSource.testers)
            {
                if (item.Id == tester.Id)
                {
                    DataSource.testers[index] = tester;
                    testerEvent(this, null);
                    //            this.OnNotifyCollectionChanged(
                    //new NotifyCollectionChangedEventArgs(
                    //  NotifyCollectionChangedAction.Add, tester));
                    return;
                }
                index++;
            }
            throw new Exception("failed to update, tester with the same ID not found");
        }



        public void AddTester(Tester tester)
        {
            if (DataSource.testers.Find(item => item.Id == tester.Id) != null)
                throw new Exception("cant add the tester, there are another tester with the same id");
            DataSource.testers.Add(tester);
           Dal_XML_imp.SaveToXML<List<Tester>>(DataSource.testers, Dal_XML_imp.TesterPath);
            testerEvent(DAL_imp.Instance, null);

            //    this.OnNotifyCollectionChanged(
            //new NotifyCollectionChangedEventArgs(
            //  NotifyCollectionChangedAction.Add, tester)); 

        }
        public List<Tester> getAllTesters()
        {
            return DataSource.testers.Clone().ToList();
        }

        public IEnumerable<object> testerGrouping(string property)
        {
            IEnumerable<object> groupOfTesters = null;
            List<Tester> listOfTesters = null;
            switch (property)
            {
                case "carType":

                    groupOfTesters = from tester in DataSource.testers
                                     group tester by tester.CarType;
                    listOfTesters = (groupOfTesters as List<Tester>).Clone();
                    break;



            }
            return listOfTesters;

        }

        public void UpdateTester(Tester tester, string id)
        {
            int index = 0;
            foreach (Tester item in DataSource.testers)
            {
                if (item.Id == id)
                {
                    DataSource.testers[index] = tester;
                    testerEvent(this, null);

                    //            this.OnNotifyCollectionChanged(
                    //new NotifyCollectionChangedEventArgs(
                    //  NotifyCollectionChangedAction.Add, tester));
                    return;
                }
                index++;
            }
            throw new Exception("failed to update, the tester old ID not found");

        }

        public Tester findTester(string testerId) => DataSource.testers.Find(item => item.Id == testerId);


        #endregion

        #region trainee
        public void AddTrainee(Trainee trainee)
        {
            if (DataSource.trainees.Find(item => item.Id == trainee.Id) != null)
                throw new Exception("cant add the trainee, there are another trainee with the same id");
            DataSource.trainees.Add(trainee);
            Dal_XML_imp.SaveToXML<List<Trainee>>(DataSource.trainees, Dal_XML_imp.TraineePath);
            traineeEvent(this, null);
        }

        public void DeleteTrainee(Trainee trainee)
        {
            if (DataSource.trainees.RemoveAll(item => item.Id == trainee.Id) == 0)
                throw new Exception("failed to remove, trainee with the same ID not found");
            traineeEvent(this, null);

        }

        public void UpdateTrainee(Trainee trainee)
        {
            int index = 0; ;
            foreach (Trainee item in DataSource.trainees)
            {
                if (item.Id == trainee.Id)
                {
                    DataSource.trainees[index] = trainee;
                    traineeEvent(this, null);
                    return;
                }
                index++;
            }
            throw new Exception("failed to update, trainee with the same ID not found");
        }

        public void UpdateTrainee(Trainee trainee, string id)
        {
            int index = 0; ;
            foreach (Trainee item in DataSource.trainees)
            {
                if (item.Id == id)
                {
                    DataSource.trainees[index] = trainee;
                    traineeEvent(this, null);
                    return;
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
                            group trainee by GetTestList(item => item.Car == trainee.CarType && item.GearBox == trainee.GearBox && item.TraineeId == trainee.Id).Count;
                    listOfTrainees = (group as List<Trainee>).Clone();
                    break;

            }
            return listOfTrainees;

        }

        public Trainee findTrainee(string traineeId) => DataSource.trainees.Find(item => item.Id == traineeId);



        #endregion

        #region test

        public List<Test> GetTestList(Func<Test, bool> p)
        {
            var x = from item in DataSource.tests
                    where p(item)
                    select item;
            var y = x.ToList<Test>();
            //if (y.Count==0)
            //{
            //    return null;
            //}
            return (y.Clone().ToList());
        }

        public void AddTest(Test test)
        {
            test.Id = Configuration.test_id.ToString();
            //add 0 at the start of the id antil there is 8 digits
            StringBuilder builder = new StringBuilder();
            for (int i = test.Id.Length; i < 8; i++)
                builder.Append(0);
            builder.Append(test.Id);
            test.Id = builder.ToString();
            Configuration.test_id++;
            DataSource.tests.Add(test);
            testEvent(this, null);
            //            this.OnNotifyCollectionChanged(
            //new NotifyCollectionChangedEventArgs(
            //NotifyCollectionChangedAction.Add, test));
        }

        public void DeleteTest(Test test)
        {
            foreach (Test item in DataSource.tests)
            {
                if (item.Id == test.Id)
                {
                    DataSource.tests.Remove(item);
                    testEvent(this, null);
                    //           new NotifyCollectionChangedEventArgs(
                    //NotifyCollectionChangedAction.Remove, test);
                    return;
                }
                throw new Exception("failed to remove, test with the same ID not found");

            }
        }

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
                    testEvent(this, null);
                    return;
                }
                index++;
            }
            throw new Exception("failed to update, test with the same ID not found");
        }

        public List<Test> getAllTests()
        {
            return DataSource.tests.Clone().ToList();
        }

        public List<Tester> GetTestersList(Predicate<Tester> p)
        {
            var x = from item in DataSource.testers
                    where p(item)
                    select item;
            var y = x.ToList<Tester>();
           
            return (y.Clone().ToList());
        }

        public object findTest(Test test)
        {
            return DataSource.tests.Find(i => i.Id == test.Id);
        }




        #endregion test

    }
}