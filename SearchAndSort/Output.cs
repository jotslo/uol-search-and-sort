using System;
using System.Collections.Generic;
using System.Text;

namespace SearchAndSort
{
    class Output
    {
        public void Sort(List<int> list, int increment, int steps)
        {
            // iterate through sorted list to display ascending dataset
            // skips every *increment* places, i.e. 10 or 50
            Console.Write("\nAscending: ");
            for (int i = increment - 1; i < list.Count; i += increment)
            {
                Console.Write(Convert.ToString(list[i]) + " ");
            }

            // iterate in reverse through list to display same for descending
            Console.Write("\nDescending: ");
            for (int i = list.Count - 1; i >= 0; i -= increment)
            {
                Console.Write(Convert.ToString(list[i]) + " ");
            }

            // show number of steps taken to complete task
            Console.WriteLine($"\n\nNumber of steps: {steps}");
        }

        public void Search(List<int> list, List<int> indexes, int steps)
        {
            // for every index in the index list, display the closest match value and index
            foreach (int index in indexes)
            {
                Console.WriteLine($"Closest match {list[index]}, found at index {index}.");
            }

            // show number of steps taken to complete task
            Console.WriteLine($"Number of steps: {steps}");
        }
    }
}
