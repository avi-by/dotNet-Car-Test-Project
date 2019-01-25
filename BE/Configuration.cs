using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BE
{
    public class Configuration
    {
        public static string DALType = "List";
        public static string BLType = "myBL";
        public static int MaxAgeTester = 40;
        public static int MinAgeTester = 20;
        public static int MinNumLessons = 20;
        public static int MinAgeTrainee = 18;
        public static TimeSpan IntervalBetweenTest = new TimeSpan(7, 0, 0, 0, 0);
        public static int test_id = 0;
        public static double minGradeForPassTest = 0.5;
        public static Dictionary<string,List<string>> street;
        public static List<string> city;
     static Configuration()
        {
            XElement streetName = XElement.Load(@"dataXML\streetWithoutSynonimXML.xml");
            street = (from item in streetName.Elements()
                      group item by item.Element("שם_ישוב").Value.Trim()
                                  into groupXml
                      select new { groupXml.Key,lis =groupXml.Select(i=>i.Element("שם_רחוב").Value.Trim() )}).ToDictionary(group=>group.Key,group=>group.lis.ToList());
            XElement cityXml = XElement.Load(@"dataXML\cityList.xml");
            city = (from i in cityXml.Elements()
                    let iElement = i.Element("שם_ישוב").Value.Trim()
                    where !iElement.Contains("שבט(") && !iElement.Contains("איחוד(") && !iElement.Contains("מאוחד(")
                    select iElement).ToList();
            List<string> temp = new List<string>();
            foreach (var item in city)
            {
                if (!street.ContainsKey(item))
                    temp.Add(item);
            }
            foreach (var item in temp)
            {
                city.Remove(item);
            }
        }
    }
    //check
}
