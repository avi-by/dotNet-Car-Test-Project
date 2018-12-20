﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace BL
{
    public interface IBL
    {

        {
            #region Tester

            void AddTester(Tester t);
        void DeleteTester(Tester t);
        void UpdateTester(Tester t);
        List<Tester> getAllTesters();
        #endregion

        #region Trainee

        void AddTrainee(Trainee t);
        void DeleteTrainee(Trainee t);
        void UpdateTrainee(Trainee t);
        List<Trainee> getAllTrainees();
        #endregion

        #region Test

        void AddTest(Test t);
        void DeleteTest(Test t);
        void UpdateTest(Test t);
        List<Test> getAllTests();
        #endregion

    }
}
