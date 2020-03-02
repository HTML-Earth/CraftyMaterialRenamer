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
            
            foreach (string arg in args)
            {
                string file = Path.GetFileName(arg);
                Console.WriteLine($"Renaming {file}");

                string realTextureName = "";
                
                string[] lines = File.ReadAllLines(file);
                string[] newLines = new string[lines.Length];
                
                for (int i = 0; i < lines.Length; i++)
                {
                    if (lines[i].StartsWith("map_Kd"))
                    {
                        if (realTextureName.Equals(""))
                            Console.WriteLine("realTextureName has not been set!");
                        else
                        {
                            newLines[i] = $"map_Kd textures/{realTextureName}.tga";
                        }
                    }
                    else
                        newLines[i] = lines[i];
                    
                    if (lines[i].StartsWith("#"))
                    {
                        realTextureName = lines[i].Substring(2, lines[i].Length - 2).ToLower();
                        //if (realTextureName.StartsWith("maps/"))
                            // fix it
                    }
                }
                
                File.WriteAllLines(file, newLines);
            }
            
            Console.WriteLine("All files modified.");
            Console.ReadKey();
        }
    }
}