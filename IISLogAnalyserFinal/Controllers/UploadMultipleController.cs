using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;
using System.IO;
using IISLogAnalyserFinal.Model;
using Newtonsoft.Json;

namespace IISLogAnalyserFinal.Controllers
{
    public class UploadMultipleController : Controller
    {
        readonly UserDataContext context;
        

        List<UserData> listUserData = new List<UserData>();

        //private IHostingEnvironment _hostingEnvironment;
        //public UploadMultipleController(IHostingEnvironment hostingEnvironment)
        //{
        //    _hostingEnvironment = hostingEnvironment;
        //}

        public UploadMultipleController(UserDataContext context)
        {
            this.context = context;
        }

        [HttpPost]
        public IActionResult Index(IList<IFormFile> files)
        {
            foreach (IFormFile file in files)
            {

                string filename = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');

                if (filename.EndsWith(".log") || filename.EndsWith(".text"))
                {


                    //var fileContents = file.OpenReadStream();

                    // var result = string.Empty;
                    // using (var reader = new StreamReader(file.OpenReadStream()))
                    // {
                    //     string[] lines = reader.ReadToEnd();
                    //     result = reader.ReadToEnd();
                    // }

                    var lines = new List<string>();
                    using (var reader = new StreamReader(file.OpenReadStream()))
                    {
                        while (reader.Peek() >= 0)
                            lines.Add(reader.ReadLine());
                    }


                    Dictionary<DateTime, List<string>> linesInDictionary = new Dictionary<DateTime, List<string>>();
                    Dictionary<DateTime, List<IISLogProperties>> linesInDictionaryReal = new Dictionary<DateTime, List<IISLogProperties>>();
                    DateTime currentKeyDate = new DateTime();
                    List<string> lineValues = new List<string>();
                    List<IISLogProperties> lineValuesReal = new List<IISLogProperties>();
                    bool dateChanged = false;
                    string lastLine = "";
                    bool isLastItem = false;

                    var k = 0;
                    var count = lines.Count;
                    var fieldMap = (IISLogPropertiesFieldMap)null;
                    foreach (string line in lines)
                    {

                        // Use a tab to indent each line of the file.
                        if (line.StartsWith("#Fields"))
                        {
                            string lineTimmed = line.Replace("#Fields: ", "");

                            fieldMap = new HelperMethods().Parse(lineTimmed);
                        }

                        if (lastLine.StartsWith("#") && !line.StartsWith("#"))
                        {
                            lineValues = new List<string>();
                            lineValuesReal = new List<IISLogProperties>();
                            lineValues.Add(line);
                            var returnValue = new IISLogProperties();
                            returnValue = new HelperMethods().Parse(line, fieldMap);
                            lineValuesReal.Add(returnValue);
                        }

                        else if (!line.StartsWith("#"))
                        {
                            lineValues.Add(line);
                            var returnValue = new IISLogProperties();
                            returnValue = new HelperMethods().Parse(line, fieldMap);
                            lineValuesReal.Add(returnValue);
                        }

                        if (!lastLine.StartsWith("#") && line.StartsWith("#") && !string.IsNullOrEmpty(lastLine))
                        {
                            isLastItem = true;
                        }

                        if (dateChanged && isLastItem)
                        {
                            dateChanged = false;
                            linesInDictionary.Add(currentKeyDate, lineValues);
                            linesInDictionaryReal.Add(currentKeyDate, lineValuesReal);
                            Console.WriteLine(lineValues.Count);
                            isLastItem = false;
                            // lineValues.Clear();
                        }

                        if (line.StartsWith("#Date"))
                        {
                            string dateString = line.Replace("#Date:", "").Trim();
                            DateTime newDate = new DateTime();
                            newDate = Convert.ToDateTime(dateString);

                            if (currentKeyDate != newDate)
                            {
                                dateChanged = true;
                                currentKeyDate = Convert.ToDateTime(dateString);
                            }
                        }



                        lastLine = line;

                        //if its the last item
                        if (++k == count)
                        {
                            linesInDictionary.Add(currentKeyDate, lineValues);
                            linesInDictionaryReal.Add(currentKeyDate, lineValuesReal);
                            Console.WriteLine(lineValues.Count);
                        }

                        //Console.WriteLine("\t" + line);

                    }

                    // Keep the console window open in debug mode.

                    //string IPAdd = "217.170.202.120";
                    //IPHostEntry hostEntry = Dns.GetHostEntry(IPAdd);
                    //Console.WriteLine(hostEntry.HostName);


                    Console.WriteLine("Hello World!");



                    foreach (var itemDict in linesInDictionaryReal)
                    {
                        foreach (IISLogProperties item in itemDict.Value)
                        {
                            UserData result = listUserData.Find(x => x.IP == item.ClientIpAddress);
                            //int index = listUserData.FindIndex(x => x.IP == item.ClientIpAddress);
                            // if (index >= 0)
                            if (result != null)
                            {
                                // element exists, do what you need
                                result.Hits++;
                            }
                            else
                            {
                                UserData user = new UserData();
                                user.IP = item.ClientIpAddress;
                                user.Hits = 1;
                                user.FQDN = "TODO";
                                listUserData.Add(user);
                            }
                        }
                    }

                    //string filename = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    //filename = this.EnsureFilename(filename);
                    //using (FileStream filestream = System.IO.File.Create(this.GetPath(filename)))
                    //{
                    //}
                    var json = JsonConvert.SerializeObject(listUserData);

                    context.UserData.Add(new UserDataTest() { JSONText = json });
                    context.SaveChanges();

                }
                else
                {
                    context.UserData.RemoveRange(context.UserData);
                    context.SaveChanges();
                }
            }
            return this.Content("Success");


        }
        [HttpGet("[action]")]
        public string UserDatas()
        {
            var json = JsonConvert.SerializeObject(listUserData);
            return json;
        }
    }
}