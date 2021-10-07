using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ConsoleApp2
{
    class Program
    {
        static Dictionary<string, List<string>> folders = new Dictionary<string, List<string>> {
           { "Text", new List<string> { ".txt", ".doc",".pdf"} },
           { "Images", new List<string> { ".png", ".jpeg",".ico",".jpg" } },
           { "Programs", new List<string> { ".exe" } },
           { "Videos", new List<string> { ".avi", ".mp4",".gif" } },
           { "Music", new List<string> { ".mp3", ".wav",".ogg" } },
           { "Archives", new List<string> { ".zip", ".rar" } },
           { "Code", new List<string> { ".cs", ".html",".php",".cpp",".js" } }
        };
        static string path = String.Empty;
        static void Main(string[] args)
        {
            string next = String.Empty;
            List<string> elements = new List<string>();
            do
            {
                path = args[0];
                path += @"\";
                if (Directory.Exists(path))
                {
                    elements.AddRange(Directory.GetFiles(path).Select(Path.GetFileName).ToList());
                    Console.Clear();
                    for (int i = 0; i < elements.Count; i++)
                        Console.WriteLine($"Element [{i}]:" + elements[i]);

                    if (elements.Count <= 0)
                        Console.WriteLine("Doesn't exist files");
                    else
                    {
                        Console.WriteLine("Sort files?\n1 - yes\t0 - no");
                        if (int.Parse(Console.ReadLine()) == 1)
                            SortFiles(elements);
                    }
                }
                else
                    Console.WriteLine("Doesn't exists this directory");

                Console.WriteLine("Do you want to continue?");
                Console.WriteLine("1 - yes\t0 - exit");
                next = Console.ReadLine();
                Console.Clear();

            } while (int.Parse(next) != 0);
            Environment.Exit(0);
        }
        static void SortFiles(List<string> allApps)
        {
            CreateDirectory();
            foreach (var item in folders)
                foreach (var format in item.Value)
                    allApps.Where(x => x.Contains(format)).ToList().ForEach(f => { File.Move($"{path + f}", $"{path + item.Key}\\{f}"); });

            allApps = Directory.GetFiles(path).Select(Path.GetFileName).ToList();
            allApps.ToList().ForEach(y => { File.Move($"{path + y}", $"{path}Other\\{y}"); });

            DeleteEmptyDir();
        }
        static void CreateDirectory()
        {
            for (int i = 0; i < folders.Count; i++)
                if (Directory.Exists($"{path + folders.ElementAt(i).Key}") == false)
                    Directory.CreateDirectory($"{path + folders.ElementAt(i).Key}");
            Directory.CreateDirectory($"{path}Other");
        }
        static void DeleteEmptyDir()
        {
            List<string> dirs = Directory.GetDirectories(path).ToList();
            foreach (var item in dirs)
                if (Directory.GetFiles(item).Length == 0)
                    Directory.Delete(item);
        }
    }
}