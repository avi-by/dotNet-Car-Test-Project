using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
using BE;
using System.Xml;
using System.Reflection;

namespace DAL
{
    public class Dal_XML_imp
    {
        #region Singleton
        private static readonly Dal_XML_imp instance = new Dal_XML_imp();
        public static Dal_XML_imp Instance { get { return instance; } }
        private Dal_XML_imp() { }
        static Dal_XML_imp() { }
        #endregion

        public static string TesterPath = @"TesterXml.xml";
        public static string TraineePath = @"TraineeXml.xml";
        public static string TestPath = @"TestXml.xml";
        public static string configPath = @"config.xml";


        /// <summary>
        /// save the source as xml document at the xml file in the path
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="path"></param>
        public static void SaveToXML<T>(T source, string path)
        {
            FileStream file = new FileStream(path, FileMode.Create);
            XmlSerializer xmlSerializer = new XmlSerializer(source.GetType());
            xmlSerializer.Serialize(file, source);
            file.Close();
        }

        /// <summary>
        /// load data from xml document in the path
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <returns></returns>
        public static T LoadFromXML<T>(string path)
        {
            FileStream file = new FileStream(path, FileMode.Open);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            T result = (T)xmlSerializer.Deserialize(file);
            file.Close();
            return result;
        }

        /// <summary>
        /// make sure that the test xml file exist, if not creat it.
        /// </summary>
        private static void Tests_xml()
        {
            XElement TestsRoot;

            if (!File.Exists(TestPath)) //if file isn't exist, create file:
            {
                TestsRoot = new XElement("Tests");
                TestsRoot.Save(TestPath);
            }
            else //if file exist, load file:
            {
                try { TestsRoot = XElement.Load(TestPath); }
                catch { throw new Exception("File upload problem"); }
            }
        }

        /// <summary>
        /// delete one tester from the tester xml file at "testerPath"
        /// </summary>
        /// <param name="tester"></param>
        internal static void DeleteTester(Tester tester)
        {

            XElement testerRoot = XElement.Load(TesterPath);
            var x = (from i in testerRoot.Elements()
                     where i.Element("Id").Value == tester.Id
                     select i).FirstOrDefault();
            if (x == null)
                throw new Exception("the tester not found at the xml");
            x.Remove();
            testerRoot.Save(TesterPath);

        }

        /// <summary>
        /// add test to the xml file of test in test path. only new test without grade and result
        /// </summary>
        /// <param name="test"></param>
        public static void AddTest_PartialDetails(Test test)
        {
            Tests_xml();
            XElement ID = new XElement("Id", Dal_XML_imp.ID_FromConfigXML().ToString("00000000"));
            Dal_XML_imp.configXML_advancingID(); //call to function that adavances id

            XElement testerId = new XElement("TesterId", test.TesterId);
            XElement traineeId = new XElement("TraineeId", test.TraineeId);

           XElement date = new XElement("Date", XmlConvert.ToString( test.Date, "yyyy-MM-ddTHH:mm:ss"));

            XElement houseNumber = new XElement("houseNumber", test.Address.houseNumber);
            XElement street = new XElement("street", test.Address.street);
            XElement city = new XElement("city", test.Address.city);
            XElement address = new XElement("Address", houseNumber, street, city);

            XElement GearBox = new XElement("GearBox",  (int)(test.GearBox));
            XElement carType = new XElement("Car", (int)test.Car);
            XElement test1_ReverseParking = new XElement("Test1_ReverseParking");
            XElement test2_KeepingSafeDistance = new XElement("Test2_KeepingSafeDistance");
            XElement test3_UsingMirrors = new XElement("Test3_UsingMirrors");
            XElement test4_UsingTurnSignals = new XElement("Test4_UsingTurnSignals");
            XElement test5_LegalSpeed = new XElement("Test5_LegalSpeed");
            XElement succeeded = new XElement("Succeeded");
            XElement notes = new XElement("Notes");

            XElement Test = new XElement("test", ID, testerId, traineeId, date, address, GearBox,carType,test1_ReverseParking,test2_KeepingSafeDistance,test3_UsingMirrors,test4_UsingTurnSignals,test5_LegalSpeed,succeeded,notes);

            XElement test_root = XElement.Load(TestPath);

            test_root.Add(Test);
            test_root.Save(TestPath);
        }

       
        /// <summary>
        /// delete trainee from the xml file at trainee path
        /// </summary>
        /// <param name="trainee"></param>
        internal static void DeleteTrainee(Trainee trainee)
        {

            XElement traineeRoot = XElement.Load(TraineePath);
            var x = (from i in traineeRoot.Elements()
                     where i.Element("Id").Value == trainee.Id
                     select i).FirstOrDefault();
            if (x == null)
                throw new Exception("the trainee not found at the xml");
            x.Remove();
            traineeRoot.Save(TraineePath);

        }

        /// <summary>
        /// return all the test at the xml file in xml path
        /// </summary>
        /// <returns></returns>
        public List<Test> GetAllTests()
        {
            XElement TestsRoot;
            try { TestsRoot = XElement.Load(TestPath); }
            catch { throw new Exception("File upload problem"); }
           
            List<Test> tests;
            try
            {
                tests = (from test in TestsRoot.Elements()
                         select new Test()
                         {
                             Id = test.Element("Id").Value,
                             TesterId = test.Element("TesterId").Value,
                             TraineeId = test.Element("TraineeId").Value,
                             Date = Convert.ToDateTime(test.Element("Date").Value),
                             Address = new Address(test.Element("Address").Element("street").Value,
                           int.Parse(test.Element("Address").Element("houseNumber").Value),
                             test.Element("Address").Element("city").Value),
                             GearBox = (GearBox)(int.Parse(test.Element("GearBox").Value)),
                             Car = (CarType)(int.Parse(test.Element("Car").Value)),
                             Test1_ReverseParking = test.Element("Test1_ReverseParking").Value == "" ? (bool?)null : (bool.Parse(test.Element("Test1_ReverseParking").Value)),
                             Test2_KeepingSafeDistance = test.Element("Test2_KeepingSafeDistance").Value == "" ? (bool?)null : (bool.Parse(test.Element("Test2_KeepingSafeDistance").Value)),
                             Test3_UsingMirrors = test.Element("Test3_UsingMirrors").Value == "" ? (bool?)null : (bool.Parse(test.Element("Test3_UsingMirrors").Value)),
                             Test4_UsingTurnSignals = test.Element("Test4_UsingTurnSignals").Value == "" ? (bool?)null : (bool.Parse(test.Element("Test4_UsingTurnSignals").Value)),
                             Test5_LegalSpeed = test.Element("Test5_LegalSpeed").Value == "" ? (bool?)null : (bool.Parse(test.Element("Test5_LegalSpeed").Value)),
                             Succeeded = test.Element("Succeeded").Value == "" ? (bool?)null : (bool.Parse(test.Element("Succeeded").Value)),
                             Notes = test.Element("Notes").Value
                         }).ToList().Clone();
            }
            catch //if the load faild
            {
                tests = new List<Test>();
            }
            return tests;
        }

        /// <summary>
        /// retun the id for the new test from the xml
        /// </summary>
        /// <returns></returns>
        public static int ID_FromConfigXML()
        {
            XElement config_root;

            if (!File.Exists(configPath)) //if file isn't exist, create file:
            {

                config_root = new XElement("Test_ID");
                config_root.Add(new XElement("Tester_ID", 0));
                config_root.Save(configPath);
                return 0;
            }
            else //if file exist, load file:
            {
                try
                {
                    config_root = XElement.Load(configPath);
                    int ID = int.Parse(config_root.Element("Tester_ID").Value);
                    return ID;
                }
                catch { throw new Exception("File upload problem"); }

            }
        }


        /// <summary>
        /// advances the test id, in each adding of test
        /// </summary>
        public static void configXML_advancingID()
        {
            XElement config_root;
            try
            {
                config_root = XElement.Load(configPath);
                int ID = int.Parse(config_root.Element("Tester_ID").Value);
                ID++;
                config_root.Element("Tester_ID").Value = ID.ToString();
                config_root.Save(configPath);

            }
            catch { throw new Exception("File upload problem, cannot advance id"); }
        }

        /// <summary>
        /// delete one test fro the xml file at test path
        /// </summary>
        /// <param name="item"></param>
        internal static void DeleteTest(Test item)
        {
             XElement testRoot = XElement.Load(TestPath);
            var x = (from i in testRoot.Elements()
                    where i.Element("Id").Value == item.Id
                    select i).FirstOrDefault();
            if (x == null)
                throw new Exception("the test not found at the xml");
            x.Remove();
            testRoot.Save(TestPath);

        }

        /// <summary>
        /// update the test in the xml file at test path. use it to update the result of the test
        /// </summary>
        /// <param name="test"></param>
        internal static void UpdateTest(Test test)
        {
            XElement testRoot = XElement.Load(TestPath);
            var x = (from i in testRoot.Elements()
                     where i.Element("Id").Value == test.Id
                     select i).FirstOrDefault();
            if (x == null)
                throw new Exception("the test not found at the xml");
            foreach (PropertyInfo item in typeof(BE.Test).GetProperties())
                    switch (item.Name)
                    {
                    case "Date":
                        x.Element("Date").SetValue(XmlConvert.ToString(test.Date, "yyyy-MM-ddTHH:mm:ss"));
                        break;
                    case "Grade":
                        break;
                    case "Address":
                        x.Element("Address").Element("street").SetValue(test.Address.street);
                        x.Element("Address").Element("houseNumber").SetValue(test.Address.houseNumber);
                        x.Element("Address").Element("city").SetValue(test.Address.city);
                        break;
                    case "GearBox":
                        x.Element("GearBox").SetValue((int)test.GearBox);
                        break;
                    case "Car":
                        x.Element("Car").SetValue((int)test.Car);
                        break;
                    case "Notes":
                        x.Element("Notes").SetValue(test.Notes);
                        break;
                    default:
                        x.Element(item.Name).SetValue((item.GetValue(test)).ToString().ToLower()); //true and false need lower letter for bool.parse
                        break;
                }
         
            testRoot.Save(TestPath);
        }
    }
}
