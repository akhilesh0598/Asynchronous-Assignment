using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace AsyncAssignment
{
    
    class Program
    {
        
        public static void ReadAndWriteFirstApproach()
        {
              var readTask = Task.Run(async() =>
                {
                    string filePath = @"C:\Users\akhil\source\repos\AsyncAssignment\AsyncAssignment\ReadFile.txt";
                    using(var stream=new StreamReader(File.OpenRead(filePath)))
                    {
                        var lines = new List<string>();
                        string line;
                        while((line=await stream.ReadLineAsync())!=null)
                        {
                            lines.Add(line);
                        }
                        return lines;
                    }
                    
                });
                var writeTask = readTask.ContinueWith(async(decendent) =>
                {
                    var lines = decendent.Result;
                    foreach(var line in lines)
                    {
                        Console.WriteLine(line);
                    }
                    string filePath = @"C:\Users\akhil\source\repos\AsyncAssignment\AsyncAssignment\WriteFile.txt";
                    using (var stream = new StreamWriter(File.OpenWrite(filePath)))
                    {
                        
                        foreach (var line in lines)
                        {
                            await stream.WriteLineAsync(line);
                        }
                    }
                });
            writeTask.Wait();
        }
        public static async Task ReadAndWriteSecondApproach()
        {
            string filePath = @"C:\Users\akhil\source\repos\AsyncAssignment\AsyncAssignment\ReadFile.txt";
            string[] text = await File.ReadAllLinesAsync(filePath);
            foreach (var line in text)
            {
                Console.WriteLine(line);
            }
            string filePath2 = @"C:\Users\akhil\source\repos\AsyncAssignment\AsyncAssignment\WriteFile.txt";
            await File.WriteAllLinesAsync(filePath2, text);
        }
        static  void Main(string[] args)
        {
           
            ReadAndWriteFirstApproach();
            //ReadAndWriteSecondApproach().Wait();
            //Console.ReadLine();
            
        }
        
    }
}
