using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.IO; 

namespace CaeserCypherLab3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //for the first file
            string pathGoodFile = @"C:\Visual Studio 2022 Preview\CaeserCypherLab3\TheRaven.txt"; 
            string[] goodText = File.ReadAllLines(pathGoodFile);
            Console.WriteLine("1. Reading input file \"The Raven\".");//1. name of file

            // Join all lines into one string for analysis
            string text1 = string.Join("", goodText);

            Console.WriteLine($"2. The file has {numberOfLines(goodText)} lines and {numberOfCharacters(text1)}");//2. number of lines and number of characters

            //this is for the other file 
            string filePath = @"C:\Visual Studio 2022 Preview\CaeserCypherLab3\Encrypted.txt";
            string[] encryptedFile = File.ReadAllLines(filePath);

            // Join all lines into one string for analysis
            string text2 = string.Join("", encryptedFile);

            Console.WriteLine("3. Reading input file \"Encrypted\".");//3. name of file
            Console.WriteLine($"4. The file has {numberOfLines(encryptedFile)} lines and {numberOfCharacters(text2)} characters.");//4. number of lines and number of characters

            char genMostUsedChar = 'e';
            char textMostUsedChar = GetMostFrequentLetter(text2); 
            Console.WriteLine($"5. The most occurring character is '{GetMostFrequentLetter(text2)}'.");//5. most frequent letter + nb of times it occurs
            Console.WriteLine($"6. A shift factor of {calculateShift(textMostUsedChar, genMostUsedChar)} has been determined."); //6. show the shift calculated
            
            string decryptedText2 = DecryptCaesarCipher(text2, calculateShift(textMostUsedChar, genMostUsedChar));
            WriteOutputFile(@"C:\Visual Studio 2022 Preview\CaeserCypherLab3\Decrypted.txt", decryptedText2);

            Console.WriteLine("8. Display the file? (y/n)");
            string choice = Console.ReadLine();
            while (choice != "y" && choice != "n")
            {
                Console.WriteLine("Invalid input. Please enter 'y' or 'n'.");
                choice = Console.ReadLine()?.ToLower();
            }

            if (choice == "y")
            {
                Console.WriteLine("----- Decrypted File Content -----");
                Console.WriteLine(decryptedText2);
            }
            else // choice == "n"
            {
                Console.WriteLine("Program ended without displaying file.");
            }

        }

        public static int numberOfLines(string[] lines)
        {
            return lines.Length;
        }


        public static int numberOfCharacters(string text)
        {
            return text.Length;
        }

        public static char GetMostFrequentLetter(string text)
        {
            var frequency = new Dictionary<char, int>();
            foreach (char letter in text)
            {
                if (char.IsLetter(letter))
                {
                    char lowerChar = char.ToLower(letter);
                    if (frequency.ContainsKey(lowerChar))
                    {
                        frequency[lowerChar]++;
                    }
                    else
                    {
                        frequency[lowerChar] = 1;
                    }
                }
            }
            return frequency.OrderByDescending(keyValuePair => keyValuePair.Value).First().Key;
        }

        public static int calculateShift(char mostFrequentLetter, char generalMostFrequentLetter)
        {
            int shift = (mostFrequentLetter - generalMostFrequentLetter + 26) % 26;
            return shift;
        }


        public static String DecryptCaesarCipher(string text, int shift)
        {
            StringBuilder result = new StringBuilder();
            foreach (char c in text)
            {
                if (char.IsUpper(c))
                {
                    char decryptedChar = (char)(((c - 'A' - shift + 26) % 26) + 'A');
                    result.Append(decryptedChar);
                }
                else if (char.IsLower(c))
                {
                    char decryptedChar = (char)(((c - 'a' - shift + 26) % 26) + 'a');
                    result.Append(decryptedChar);
                }
                else
                {
                    result.Append(c);
                }

            }

            return result.ToString();

        }



        public static void WriteOutputFile(string outputPath, string decryptedText)
        {
            File.WriteAllText(outputPath, decryptedText);
            Console.WriteLine($"7. Writing output file now to \"{outputPath}\".");
        }



    }

}
