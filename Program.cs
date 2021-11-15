using System;
using System.IO;
using NLog;
using NLog.Web;

namespace StringInterpolation
{
    class Program
    {
         private static NLog.Logger logger = NLogBuilder.ConfigureNLog(Directory.GetCurrentDirectory() + "\\nlog.config").GetCurrentClassLogger();
        static void Main(string[] args)
        {
            logger.Info("Program started");

            System.Console.WriteLine("1) create file");
            System.Console.WriteLine("2) Parse Data");
            System.Console.WriteLine("Enter to Quit");

            System.Console.Write(">");
            string input= Console.ReadLine();
            logger.Info("Input Selected");

            if(input=="1"){
                System.Console.WriteLine("Weeks of info required>");
                int weeks = Int32.Parse(Console.ReadLine());

                DateTime today = DateTime.Now;

                DateTime endDate = today.AddDays(-(int)today.DayOfWeek);

                DateTime dataDate = endDate.AddDays(-(weeks * 7));

                Random rnd = new Random();

                StreamWriter sw = new StreamWriter("data.txt");

                while(dataDate < endDate){
                    int[] hours = new int[7];
                    for (int x = 0; x < hours.Length; x++){
                        hours[x] = rnd.Next(4,13);
                    }

                    sw.WriteLine($"{dataDate:M/d/yyyy},{string.Join("|", hours)}");

                    dataDate = dataDate.AddDays(7);
                }
                sw.Close();
            }
        }
    }
}
