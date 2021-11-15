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
            else if(input == "2"){
                string file = "data.txt";

                if(!File.Exists(file)){
                    Console.WriteLine($"Error: {file} does not exist");
                }
                StreamReader reader = new StreamReader(file);

                    double netTotal = 0;
                    double netCount = 0;
                    double netAvg = 0;
                    while (!reader.EndOfStream)
                    {
                        
                        double totalHours = 0;
                        double average = 0;
                        double avgFormat = 0;
                        input = reader.ReadLine();

                        
                        string[] wk = input.Split(',');

                        
                        DateTime date;
                        DateTime.TryParse(wk[0], out date);

                        double[] hr = Array.ConvertAll(wk[1].Split('|'), Double.Parse);

                        
                        foreach (var value in hr)
                        {
                            totalHours += value;
                            netCount++;
                        }

                        average = totalHours / 7;
                        netTotal += totalHours;
                        netAvg = netTotal / netCount;
                        avgFormat = Math.Round(average, 2);

                        Console.WriteLine($"Week of {date:MMM}, {date:dd}, {date:yyyy}");

                        Console.WriteLine(
                            $"{"Sun",4}{"Mon",4}{"Tue",4}{"Wed",4}{"Thu",4}{"Fri",4}{"Sat",4}{"Total",6}{"Average",8}");
                        Console.WriteLine(
                            $"{"---",4}{"---",4}{"---",4}{"---",4}{"---",4}{"---",4}{"---",4}{"-----",6}{"-------",8}");
                        
                        Console.WriteLine(
                            $"{hr[0],4}{hr[1],4}{hr[2],4}{hr[3],4}{hr[4],4}{hr[5],4}{hr[6],4}{totalHours,6}{avgFormat,8}");
                        Console.WriteLine();
                        Console.WriteLine($"Net Total: {netTotal} hours");
                        Console.WriteLine($"Net Average: {netAvg:N2} hours");
                        Console.WriteLine();
                }
            }
            logger.Info("Program ended");
        }
    }
}
