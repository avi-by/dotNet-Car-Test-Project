using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Xml;
using BE;

namespace DAL
{
    public class distance_calculation
    {
        
        public static double distanceCalculation(Address trainee_Adress, Address tester_Adress)
        {
            try
            {
                string traineeAddress = trainee_Adress.street + " " + trainee_Adress.houseNumber + " " + trainee_Adress.city;
                string testerAddress = tester_Adress.street + " " + tester_Adress.houseNumber + " " + tester_Adress.city;

                //string origin = "pisga 45 st. jerusalem";//or "תקווה פתח 100 העם אחד "etc.
                //string destination = "gilgal 78 st. ramat-gan";//or "גן רמת 10 בוטינסקי'ז "etc.
                string KEY = @"qJohA5KvfKXcnOlya1sL0tGCyDwArh2A";
                string url = @"https://www.mapquestapi.com/directions/v2/route" +
                 @"?key=" + KEY +
                 @"&from=" + testerAddress +
                 @"&to=" + traineeAddress +
                 @"&outFormat=xml" +
                 @"&ambiguities=ignore&routeType=fastest&doReverseGeocode=false" +
                 @"&enhancedNarrative=false&avoidTimedConditions=false";
                //request from MapQuest service the distance between the 2 addresses
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                WebResponse response = request.GetResponse();
                Stream dataStream = response.GetResponseStream();
                StreamReader sreader = new StreamReader(dataStream);
                string responsereader = sreader.ReadToEnd();
                response.Close();
                //the response is given in an XML format
                XmlDocument xmldoc = new XmlDocument();
                xmldoc.LoadXml(responsereader);

                if (xmldoc.GetElementsByTagName("statusCode")[0].ChildNodes[0].InnerText == "0")
                //we have the expected answer
                {
                    //display the returned distance
                    XmlNodeList distance = xmldoc.GetElementsByTagName("distance");
                    double distInMiles = Convert.ToDouble(distance[0].ChildNodes[0].InnerText);
                    //Console.WriteLine("Distance In KM: " + distInMiles * 1.609344);
                    //display the returned driving time 
                    //there is no use in the drive time
                    //XmlNodeList formattedTime = xmldoc.GetElementsByTagName("formattedTime");
                    //string fTime = formattedTime[0].ChildNodes[0].InnerText;

                    return distInMiles * 1.609344;
                    //return Convert.ToDouble(fTime);
                    //return fTime;
                    //Console.WriteLine("Driving Time: " + fTime);
                }
                else if (xmldoc.GetElementsByTagName("statusCode")[0].ChildNodes[0].InnerText == "402")
                //we have an answer that an error occurred, one of the addresses is not found
                {
                    return -1;
                    //  return "an error occurred, one of the addresses is not found. try again.";
                    //Console.WriteLine("an error occurred, one of the addresses is not found. try again.");
                }
                else //busy network or other error...
                {
                    //at the Instructions they say that if there are any problem with the return value use defult value so:
                    return 13;
                    //   return distanceCalculation(trainee_Adress, tester_Adress);
                    // return "We have'nt got an answer, maybe the net is busy...";
                    //Console.WriteLine("We have'nt got an answer, maybe the net is busy...");
                }
            }
            catch  
            {
                return 13;
            }
            
        }

    }
}
