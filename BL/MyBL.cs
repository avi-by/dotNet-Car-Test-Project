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

        static IDAL MyDal=DAL.FactoryDAL.getDAL("List");

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
        public void addTester(Tester tester)
        {
            MyDal.AddTester(tester);
        }

        public void deleteTester(Tester tester)
        {
            throw new NotImplementedException();
        }

        public List<Tester> getTestersList()
        {
            return MyDal.getAllTesters();
        }

        public void updateTester(Tester tester, string name)
        {
            throw new NotImplementedException();
        }

        public void updateTester(Tester tester, Tester newtester)
        {
            throw new NotImplementedException();
        }

        public void updateTester(Tester tester, int cost, string minmax)
        {
            throw new NotImplementedException();
        }

        public void addTrainee(Trainee trainee)
        {
            throw new NotImplementedException();
        }

        public void deleteTrainee(Trainee trainee)
        {
            throw new NotImplementedException();
        }

        public void updateTrainee(Tester tester, string name)
        {
            throw new NotImplementedException();
        }

        public void updateTrainee(Trainee trainee, Trainee newTrainee)
        {
            throw new NotImplementedException();
        }

        public List<Trainee> getTraineesList()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}