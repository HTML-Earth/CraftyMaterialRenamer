using System;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;

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
                        // Cut off "# "
                        realTextureName = lines[i].Substring(2, lines[i].Length - 2);

                        // Fix texture names that look like "maps/d1_canals_02/stone/stonewall051b_-477_1583_-795"
                        if (realTextureName.StartsWith("maps/"))
                        {
                            Regex rx = new Regex(@"(?:^maps/[0-z]+/)(.*)(?:(_-?[0-9]+){3}$)", RegexOptions.Compiled);
                            MatchCollection matches = rx.Matches(realTextureName);

                            if (matches.Count == 1)
                                realTextureName = matches[0].Result("$1");
                            else
                                Console.WriteLine($"Regex error: Found {matches.Count} matches for {realTextureName}");
                        }
                        else // Lowercase for texture names that look like "CONCRETE/CONCRETEWALL071E"
                        {
                            realTextureName = lines[i].ToLower();
                        }
                    }
                }
                
                File.WriteAllLines(file, newLines);
            }
            
            Console.WriteLine("All files modified.");
            Console.ReadKey();
        }
    }
}