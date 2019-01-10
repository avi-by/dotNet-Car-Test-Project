using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
using BE;

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



        public static void SaveToXML<T>(T source, string path)
        {
            FileStream file = new FileStream(path, FileMode.Create);
            XmlSerializer xmlSerializer = new XmlSerializer(source.GetType());
            xmlSerializer.Serialize(file, source);
            file.Close();
        }

        public static T LoadFromXML<T>(string path)
        {
            FileStream file = new FileStream(path, FileMode.Open);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            T result = (T)xmlSerializer.Deserialize(file);
            file.Close();
            return result;
        }

        public static void Tests_xml()
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

        public static void AddTest_PartialDetails(Test test)
        {
            Tests_xml();
            XElement ID = new XElement("id", Dal_XML_imp.ID_FromConfigXML());
            Dal_XML_imp.configXML_advancingID(); //call to function that adavances id

            XElement testerId = new XElement("testerID", test.TesterId);
            XElement traineeId = new XElement("traineeID", test.TraineeId);

            //XElement year = new XElement("year", test.Date.Year);
            //XElement month = new XElement("month", test.Date.Month);
            //XElement day = new XElement("day", test.Date.Day);
            //XElement hour = new XElement("hour", test.Date.Hour);
            XElement date = new XElement("date", test.Date.ToShortDateString());

            XElement houseNumber = new XElement("houseNumber", test.Address.houseNumber);
            XElement street = new XElement("street", test.Address.street);
            XElement city = new XElement("city", test.Address.city);
            XElement address = new XElement("address", houseNumber, street, city);

            XElement GearBox = new XElement("GearBox",  (int)(test.GearBox));
            XElement carType = new XElement("CarType", (int)test.Car);
            XElement test1_ReverseParking = new XElement("test1_ReverseParking");
            XElement test2_KeepingSafeDistance = new XElement("test2_KeepingSafeDistance");
            XElement test3_UsingMirrors = new XElement("test3_UsingMirrors");
            XElement test4_UsingTurnSignals = new XElement("test4_UsingTurnSignals");
            XElement test5_LegalSpeed = new XElement("test5_LegalSpeed");
            XElement succeeded = new XElement("succeeded");
            XElement notes = new XElement("notes");

            XElement Test = new XElement("test", ID, testerId, traineeId, date, address, GearBox,carType,test1_ReverseParking,test2_KeepingSafeDistance,test3_UsingMirrors,test4_UsingTurnSignals,test5_LegalSpeed,succeeded,notes);

            XElement test_root = XElement.Load(TestPath);

            test_root.Add(Test);
            test_root.Save(TestPath);
        }

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
                             Id = test.Element("id").Value,
                             TesterId = test.Element("testerID").Value,
                             TraineeId = test.Element("traineeID").Value,
                             Date = Convert.ToDateTime(test.Element("date").Value),
                             Address = new Address(test.Element("address").Element("street").Value,
                           int.Parse(test.Element("address").Element("houseNumber").Value),
                             test.Element("address").Element("city").Value),
                             GearBox = (GearBox)(int.Parse(test.Element("GearBox").Value)),
                             Car = (CarType)(int.Parse(test.Element("CarType").Value)),
                             Test1_ReverseParking = test.Element("test1_ReverseParking").Value == "" ? (bool?)null : (bool.Parse(test.Element("test1_ReverseParking").Value)),
                             Test2_KeepingSafeDistance = test.Element("test2_KeepingSafeDistance").Value == "" ? (bool?)null : (bool.Parse(test.Element("test2_KeepingSafeDistance").Value)),
                             Test3_UsingMirrors = test.Element("test3_UsingMirrors").Value == "" ? (bool?)null : (bool.Parse(test.Element("test3_UsingMirrors").Value)),
                             Test4_UsingTurnSignals = test.Element("test4_UsingTurnSignals").Value == "" ? (bool?)null : (bool.Parse(test.Element("test4_UsingTurnSignals").Value)),
                             Test5_LegalSpeed = test.Element("test5_LegalSpeed").Value == "" ? (bool?)null : (bool.Parse(test.Element("test5_LegalSpeed").Value)),
                             Succeeded = test.Element("succeeded").Value == "" ? (bool?)null : (bool.Parse(test.Element("succeeded").Value)),
                             Notes = test.Element("notes").Value
                         }).ToList().Clone();
            }
            catch
            {
                tests = null;
            }
            return tests;
        }

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

    }
}
