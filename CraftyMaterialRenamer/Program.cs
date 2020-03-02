using System;
using System.Diagnostics;
using System.IO;

namespace CraftyMaterialRenamer
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Crafty Material Renamer\n");
            
            if (args.Length < 1)
            {
                Console.WriteLine("Error - No files given.");
                Console.ReadKey();
                return;
            }
            
            Console.WriteLine($"Got {args.Length} files.");
            
            foreach (string s in args)
            {
                string name = Path.GetFileName(s);
                Console.WriteLine($"Renaming {s}...");
                
            }
            
            Console.WriteLine("All files modified.");
            Console.ReadKey();
        }
    }
}