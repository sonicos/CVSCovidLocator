using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CVSCovidLocator
{
    
    static class Program
    {
        static Random rand = new Random();
        static Config config;
        static Ui interactiveForm = new Ui();


        [STAThread]
        static void Main()
        {

            config = JsonConvert.DeserializeObject<Config>(File.ReadAllText("config.json"));
            var Zips = config.searchZips;
            Globals.BasePayload = File.ReadAllText("payload.json").Replace("___APIKEY___",config.apiKey);
            Globals.CurrentAvailability = new Dictionary<string, List<Availability>>();
            
            

            foreach (var zip in Zips)
            {
                var timer = new Timer();
                timer.Tick += (object s, EventArgs a) => CheckAvailability(s,a,timer,zip);
                // fire first "tick" in 500ms
                timer.Interval = 500;
                timer.Start();
            }
            Application.Run(interactiveForm);

        }

        private static void CheckAvailability( object sender, EventArgs e, Timer timer, string zip)
        {
            string myJson = Globals.BasePayload.Replace(Globals.ZIP_REPLACE,zip);

            try
            {
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(config.endpoint);
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    streamWriter.Write(myJson);
                }

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(result);
                    var availability = new List<Availability>();
                    if (myDeserializedClass.responseMetaData == null)
                    {
                        // Bad API key causes this
                        timer.Stop();
                        interactiveForm.SetError("Invalid Response. Check API key and/or Endpoint");

                    }
                    else
                    {
                        if (myDeserializedClass.responseMetaData.statusCode == "0000")
                        {

                            // Success
                            // Get List of Locations
                            // Get list of dates per location
                            foreach (var location in myDeserializedClass.responsePayloadData.locations)
                            {
                                var av = new Availability(location, zip);
                                availability.Add(av);
                                Debug.WriteLine($"{av.City} - {string.Join(", ", av.Dates)} - {av.Address} ---");
                            }



                        }
                        else
                        {
                            if (myDeserializedClass.responseMetaData.statusCode != "1010")
                            {
                                // what other error codes are there!
                            }
                            // Fail

                        }
                        lock (Globals.LOCK)
                        {
                            Globals.CurrentAvailability[zip] = availability;
                            Debug.Print($"{zip} - {myDeserializedClass.responseMetaData.statusCode} - {myDeserializedClass.responseMetaData.statusDesc}");
                            interactiveForm.UpdateAvailability();
                            Console.WriteLine($"{zip} - {myDeserializedClass.responseMetaData.statusCode} - {myDeserializedClass.responseMetaData.statusDesc}");
                        }
                        var nextInterval = rand.Next(config.minDelay * 1000, config.maxDelay * 1000);
                        timer.Interval = nextInterval;
                        //Debug.WriteLine($"{zip} - inverval -- {nextInterval}");
                    }
                }
            } catch (Exception ex)
            {
                // network error probably?
                Debug.WriteLine(ex);
            }

        }
    }
}
