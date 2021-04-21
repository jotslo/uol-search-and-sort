using System;
using System.Collections.Generic;
using System.Text;

namespace SearchAndSort
{
    class Input
    {
        public List<int> Read(int roadNumber, bool isLarge)
        {
            // generate road list, create length int based on whether large file
            List<int> road = new List<int>();
            string number = Convert.ToString(roadNumber);
            int length = isLarge ? 2048 : 256;

            // get relevant road data file
            System.IO.StreamReader input =
                new System.IO.StreamReader($"Road_{number}_{length}.txt");

            // get file length based on road number and length
            // iterate through every line in file and add to list
            int fileLength = roadNumber == 4 ? length * 2 : length;
            for (int i = 0; i < fileLength; i++)
            {
                road.Add(Convert.ToInt32(input.ReadLine()));
            }

            return road;
        }
    }
}
