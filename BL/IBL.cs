using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace BL
{
    interface IBL
    {
        #region test
        void AddTest(Test test);
        void DeleteTest(Test test);
        List<Test> getAllTests();

        #endregion
        #region trainee
        void addTrainee(Trainee trainee);
        void deleteTrainee(Trainee trainee);
        //void updateTrainee(Tester tester, string name);
        //void updateTrainee(Trainee trainee, Trainee newTrainee);
        List<Trainee> getAllTrainees();

        #endregion

        #region Tester
        void addTester(Tester tester);
        void deleteTester(Tester tester);
        //void updateTester(Tester tester, string name);
        //void updateTester(Tester tester, Tester newtester);
        List<Tester> getAllTester();
        
        #endregion
    }
}