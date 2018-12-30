using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace BL
{
   public interface IBL
    {
        #region test
        void AddTest(Test test);
        void DeleteTest(Test test);
        List<Test> getAllTests();
        void updateTest(Test test);
        void completedTest(Test test);
        List<Test> allTheTestAtDate(DateTime date);
        List<Test> allTheTestAtMonth(DateTime date);
        event EventHandler<EventArgs> TestEvent;
        #endregion

        #region trainee
        void addTrainee(Trainee trainee);
        void deleteTrainee(Trainee trainee);
        void updateTrainee(Trainee trainee, string id);
        void updateTrainee(Trainee trainee);
        event EventHandler<EventArgs> TraineeEvent;
        List<Trainee> getAllTrainees();
        bool haveLicense(string id, GearBox gearBox, CarType carType);
        bool isPassed(string id);
        int amountOfTests(string id);
        #endregion

        #region Tester
        void addTester(Tester tester);
        void deleteTester(Tester tester);
        void updateTester(Tester tester, string id);
        void updateTester(Tester tester);
        event EventHandler<EventArgs> TesterEvent;
        bool isWorkAtThisDay(Tester tester, DayOfWeek dayOfWeek);
        List<Tester> getAllTester();
        List<Tester> testersAtDistanceFromAddress(Address address, double distance = 5);
        List<Tester> testersAvailableAtDate(DateTime date);
        bool isAvailableAtDate(string testerId, DateTime date);
        bool atAvailbleDistance(string testerId, Address address);
        DateTime NearestOpenDate(DateTime date);
        DateTime NearestOpenDate();
        #endregion
    }
}