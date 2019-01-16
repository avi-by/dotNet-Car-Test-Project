using BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public interface IDAL
    {
                #region Tester

        void AddTester(Tester t);
        void DeleteTester(Tester t);
        void UpdateTester(Tester t);
        void UpdateTester(Tester tester, string id);
        Tester findTester(string testerId);
        List<Tester> getAllTesters();
        event EventHandler<EventArgs> TesterEvent;
        List<Tester> GetTestersList(Predicate<Tester> p);

        #endregion



        #region Trainee

        void AddTrainee(Trainee t);
        void DeleteTrainee(Trainee t);
        void UpdateTrainee(Trainee t);
        List<Trainee> getAllTrainees();
        Trainee findTrainee(string traineeId);
        void UpdateTrainee(Trainee tester, string id);
        event EventHandler<EventArgs> TraineeEvent;
        IEnumerable<object> traineeGrouping(string property);

        #endregion



        #region Test

        void AddTest(Test t);
        void DeleteTest(Test t);
        void UpdateTest(Test t);
        List<Test> getAllTests();
        List<Test> GetTestList(Func<Test, bool> p);
        event EventHandler<EventArgs> TestEvent;

        Test findTest(Test test);

        #endregion
    }
}