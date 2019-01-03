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

        /// <summary>
        /// add test to the data base
        /// </summary>
        /// <param name="test"></param>
        void AddTest(Test test);

        /// <summary>
        /// delete the test from the data base
        /// </summary>
        /// <param name="test"></param>
        void DeleteTest(Test test);

        /// <summary>
        /// return all the tests at the data base
        /// </summary>
        /// <returns></returns>
        List<Test> getAllTests();

        /// <summary>
        /// Update of a test that has not yet been performed
        /// </summary>
        /// <param name="test"></param>
        void updateTest(Test test);

        /// <summary>
        /// use to insert test results, can calculate if pass or not
        /// </summary>
        /// <param name="test"></param>
        void completedTest(Test test);

        /// <summary>
        /// return list of all the test in the input day
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        List<Test> allTheTestAtDate(DateTime date);

        /// <summary>
        /// return list of all the test that happend in the mounth 
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        List<Test> allTheTestAtMonth(DateTime date);

        /// <summary>
        /// return list of all the test from the start date to the end date
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        List<Test> allTheTestAtRange(DateTime start, DateTime end);

        /// <summary>
        /// return true if there are atleast one avialble tester at this ohur and date
        /// </summary>
        /// <param name="testerId"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        bool isAvailableDate(DateTime date);

        /// <summary>
        /// notify if somthing cange at the data base
        /// </summary>
        event EventHandler<EventArgs> TestEvent;
        #endregion

        #region trainee

        /// <summary>
        /// minimum age of trainee
        /// </summary>
        /// <returns></returns>
        int getMinimumAge();

        /// <summary>
        /// add new trainee to the data base
        /// </summary>
        /// <param name="trainee"></param>
        void addTrainee(Trainee trainee);

        /// <summary>
        /// delete trainee from the data base
        /// </summary>
        /// <param name="trainee"></param>
        void deleteTrainee(Trainee trainee);

        /// <summary>
        /// update trainee by new one ,can change ID,need the old ID
        /// </summary>
        /// <param name="trainee"></param>
        /// <param name="id"></param>
        void updateTrainee(Trainee trainee, string id);

        /// <summary>
        /// update trainee by new one, (cant change the ID)
        /// </summary>
        /// <param name="trainee"></param>
        void updateTrainee(Trainee trainee);

        /// <summary>
        /// notify if sonthing change in the trainee data bese
        /// </summary>
        event EventHandler<EventArgs> TraineeEvent;

        /// <summary>
        /// return copy of all the treainee data base
        /// </summary>
        /// <returns></returns>
        List<Trainee> getAllTrainees();

        /// <summary>
        /// return if person have a license at this kind of car and gearbox
        /// </summary>
        /// <param name="id"></param>
        /// <param name="gearBox"></param>
        /// <returns></returns>
        bool haveLicense(string id, GearBox gearBox, CarType carType);

        /// <summary>
        /// return true if the trainee pass the test
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool isPassed(string id);

        /// <summary>
        /// return the amount of test that this treinee do on the current kind of car
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        int amountOfTests(string id);
        #endregion

        #region Tester

        /// <summary>
        /// return the maximum age of a tester
        /// </summary>
        /// <returns></returns>
        int getMaximumAge();

        /// <summary>
        /// return the minimum age of the tester
        /// </summary>
        /// <returns></returns>
        int getMinimumAgeOfTester();



        /// <summary>
        /// add new tester to the data base
        /// </summary>
        /// <param name="tester"></param>
        void addTester(Tester tester);

        /// <summary>
        /// delete the tester from the data base
        /// </summary>
        /// <param name="tester"></param>
        void deleteTester(Tester tester);

        /// <summary>
        /// update exist tester by new one, can change the ID, need the old one
        /// </summary>
        /// <param name="updatedTester"></param>
        /// <param name="oldId"></param>
        void updateTester(Tester tester, string id);

        /// <summary>
        /// update exist tester by new one (need the same ID)
        /// </summary>
        /// <param name="tester"></param>
        void updateTester(Tester tester);

        /// <summary>
        /// notify if somthing change in tester data base
        /// </summary>
        event EventHandler<EventArgs> TesterEvent;

        /// <summary>
        /// return if the tester work at this day
        /// </summary>
        /// <param name="tester"></param>
        /// <param name="dayOfWeek"></param>
        /// <returns></returns>
        bool isWorkAtThisDay(Tester tester, DayOfWeek dayOfWeek);

        /// <summary>
        /// return copy of all the tester in the data  base
        /// </summary>
        /// <returns></returns>
        List<Tester> getAllTester();

        /// <summary>
        /// return all the testers that  at in "distance" from the "address" 
        /// </summary>
        /// <param name="address"></param>
        /// <param name="distance"></param>
        /// <returns></returns>
        List<Tester> testersAtDistanceFromAddress(Address address, double distance = 5);

        /// <summary>
        ///  return all the tester that available at this date
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        List<Tester> testersAvailableAtDate(DateTime date);

        /// <summary>
        /// return all the tester that available at this date and this hour
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        List<Tester> testersAvailableAtDateAndHour(DateTime date);

        /// <summary>
        /// return if this tester can do do a test at this date in any possible hour
        /// </summary>
        /// <param name="testerId"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        bool isAvailableAtDate(string testerId, DateTime date);

        /// <summary>
        /// return if the address is at availble distance for this tester
        /// </summary>
        /// <param name="testerId"></param>
        /// <param name="address"></param>
        /// <returns></returns>
        bool atAvailbleDistance(string testerId, Address address);

        /// <summary>
        ///  the nearest date available for test from the insert date
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        DateTime NearestOpenDate(DateTime date);

        /// <summary>
        /// the nearest date available for test from tommorow
        /// </summary>
        /// <returns></returns> 
        DateTime NearestOpenDate();

        /// <summary>
        /// return all the tester that available at this date and this hour and can test this trainee
        /// </summary>
        /// <param name="dateAndHour"></param>
        /// <param name="trainee"></param>
        /// <returns></returns>
        List<Tester> testersAvailableAtDateAndHourBySpecialization(DateTime dateAndHour, Trainee trainee);

        /// <summary>
        ///  the nearest date available for test of specific type from date, default tommorow
        /// </summary>
        /// <param name="carType"></param>
        /// <param name="gearBox"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        DateTime NearestOpenDateByspecialization(CarType carType, GearBox gearBox, DateTime? date);

        /// <summary>
        /// return all the tester that specialize in a specific type that available at this date
        /// </summary>
        /// <param name="date"></param>
        /// <param name="car"></param>
        /// <param name="gearBox"></param>
        /// <returns></returns>
        List<Tester> testersAvailableAtDateBySpecialization(DateTime date, CarType car, GearBox gearBox);

        #endregion
    }
}


