using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;

namespace SearchAndSort
{
    class Program
    {
        static void Main(string[] args)
        {
            Algorithm algorithm = new Algorithm();
            Input input = new Input();
            Data data = new Data();
            Output output = new Output();

            int number = 1;
            bool isLarge = false;

            // get list for first 256 length road file by default
            List<int> list = input.Read(1, false);

            while (true)
            {
                // get data length by checking whether file is large
                // and whether file contains two merged files or not
                // then get road number+type, clear console and display menu
                int dataLength = (isLarge ? 2048 : 256) * (number == 4 ? 2 : 1);
                string roadNumber = number == 4 ? "1+3" : Convert.ToString(number);
                string roadType = $"{roadNumber} ({dataLength})";
                Console.Clear();
                Console.Write(
                    String.Format(data.Descriptions, roadType)
                    + "\n> "
                );

                // read user input, if empty then reset, otherwise get first char
                string userInput = Console.ReadLine();
                if (userInput == "") { continue; }
                char firstChar = userInput[0];

                // if first char is digit and valid, get relevant dataset
                if (Char.IsDigit(firstChar))
                {
                    number = Convert.ToInt32(firstChar.ToString());
                    isLarge = userInput.Contains("+");

                    if (1 <= number && number <= 4)
                        list = input.Read(number, isLarge);
                }

                // otherwise, check if input is valid algorithm key
                else if (data.InputMap.ContainsKey(userInput))
                {
                    int increment = isLarge ? 50 : 10;
                    string functionName = data.InputMap[userInput];
                    MethodInfo method = algorithm.GetType().GetMethod(functionName);

                    // if function is sort type, call relevant function and output info
                    if (functionName.Contains("Sort"))
                    {
                        Tuple<List<int>, int> result = (Tuple<List<int>, int>)method.Invoke(
                            algorithm,
                            new object[] { new object[] { list } }
                        );
                        output.Sort(result.Item1, increment, result.Item2);
                    }

                    // otherwise, if function is search type, take user input
                    // then call relevant search function and output info
                    else
                    {
                        Console.Write("\nEnter the value you would like to find in the list...\n\n> ");
                        string value = Console.ReadLine();
                        int n;

                        if (int.TryParse(value, out n))
                        {
                            Tuple<List<int>, int> result = (Tuple<List<int>, int>)method.Invoke(
                                algorithm,
                                new object[] { list, n }
                            );
                            output.Search(list, result.Item1, result.Item2);
                        }
                    }

                    Console.Write("\n\nPress enter to return to main menu...\n\n> ");
                    Console.ReadLine();
                }
            }
        }
    }
}
