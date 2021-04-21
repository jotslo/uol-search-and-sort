using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace SearchAndSort
{
    class Algorithm
    {
        private static (List<int>, int) Merge(List<int> left, List<int> right, int steps)
        {
            List<int> result = new List<int>();
            
            // while anything in left or right list
            while (left.Any() || right.Any())
            {
                steps++;
                // if values in left+right, compare values and add lowest to new list
                if (left.Any() && right.Any())
                {
                    if (left.First() <= right.First())
                    {
                        result.Add(left.First());
                        left.Remove(left.First());
                    }
                    else
                    {
                        result.Add(right.First());
                        right.Remove(right.First());
                    }
                }

                // otherwise, if values in only one, add all L/R values to list
                else if (left.Any())
                {
                    result.Add(left.First());
                    left.Remove(left.First());
                }
                else if (right.Any())
                {
                    result.Add(right.First());
                    right.Remove(right.First());
                }
            }

            // return final merged list and amount of steps taken
            return (result, steps);
        }

        public Tuple<List<int>, int> MergeSort(params object[] args)
        {
            List<int> list = (List<int>)args[0];
            int steps = 0;
            
            // set steps value if found
            if (args.Length > 1)
            {
                steps = (int)args[1];
            } 

            // if list is empty, return empty list and step count
            if (list.Count <= 1)
            {
                return Tuple.Create(list, steps);
            }

            List<int> left = new List<int>();
            List<int> right = new List<int>();

            // get median value in list, split into left and right list
            int median = list.Count / 2;
            for (int i = 0; i < median; i++)
            {
                left.Add(list[i]);
            }
            for (int i = median; i < list.Count; i++)
            {
                right.Add(list[i]);
            }

            // get sorted left list and sorted right list
            Tuple<List<int>, int> leftResult = MergeSort(left, steps);
            Tuple<List<int>, int> rightResult = MergeSort(right, leftResult.Item2);

            left = leftResult.Item1;
            right = rightResult.Item1;
            steps = rightResult.Item2;

            // merge sorted lists into one and return final sorted list
            (List<int> result, int newSteps) = Merge(left, right, steps);
            return Tuple.Create(result, newSteps);
        }

        private static (int, int) Partition(List<int> list, int low, int high, int steps)
        {
            int pivot = list[high];
            int lowIndex = (low - 1);

            // for every value from low to high, if list lower than pivot value,
            // then switch values in list and increase step count
            for (int j = low; j < high; j++)
            {
                if (list[j] <= pivot)
                {
                    lowIndex++;

                    int temp = list[lowIndex];
                    list[lowIndex] = list[j];
                    list[j] = temp;
                    steps++;
                }
            }

            // switch values in list at high index
            int temp1 = list[lowIndex + 1];
            list[lowIndex + 1] = list[high];
            list[high] = temp1;

            // return lowest index and step count
            return (lowIndex + 1, steps);
        }
        
        private static int QuickRecurse(List<int> list, int low, int high, int steps)
        {
            // if low var is less than high var, continue to recurse
            if (low < high)
            {
                (int partitionIndex, int newSteps) = Partition(list, low, high, steps);

                steps = newSteps;
                steps = QuickRecurse(list, low, partitionIndex - 1, steps);
                steps = QuickRecurse(list, partitionIndex + 1, high, steps);
            }

            // otherwise, return step count
            return steps;
        }

        public Tuple<List<int>, int> QuickSort(params object[] args)
        {
            // call recurse function
            // return new list and step count when complete
            List<int> list = (List<int>)args[0];
            int steps = QuickRecurse(list, 0, list.Count - 1, 0);
            return Tuple.Create(list, steps);
        }

        public Tuple<List<int>, int> BubbleSort(params object[] args)
        {
            List<int> list = (List<int>)args[0];
            int steps = 0;

            // for every value from 1 to list len, and repeated per iteration
            for (int i = 0; i < list.Count - 1; i++)
            {
                for (int j = 0; j < list.Count - 1; j++)
                {
                    // if value is greater than, value to the right
                    // then switch values and increase step counter
                    if (list[j] > list[j + 1])
                    {
                        int temp = list[j + 1];
                        list[j + 1] = list[j];
                        list[j] = temp;
                        steps++;
                    }
                }
            }

            // return new list and step count
            return Tuple.Create(list, steps);
        }

        public Tuple<List<int>, int> InsertionSort(params object[] args)
        {
            List<int> list = (List<int>)args[0];
            int steps = 0;

            // for every value from 1 to list len
            for (int i = 1; i < list.Count; i++)
            {
                int key = list[i];
                int j = i - 1;

                // while j var is positive and list value higher than key
                // switch value in list with value to left
                // deduct j and increment step counter
                while (j >= 0 && list[j] > key)
                {
                    list[j + 1] = list[j];
                    j--;
                    steps++;
                }
                list[j + 1] = key;
            }

            // return sorted list and step count
            return Tuple.Create(list, steps);
        }

        public Tuple<List<int>, int> LinearSearch(List<int> list, int value)
        {
            int lastValue = -9999;
            int index = 0;
            int firstOccurence = -1;
            int steps = 0;

            // for every value in list, inc. step count
            foreach (int localValue in list)
            {
                steps++;
                
                // if code has passed the value we're searching for
                if (localValue > value)
                {
                    // if current value is closer to query than previous value,
                    // return current value
                    if (localValue - value < value - lastValue)
                        return Tuple.Create(new List<int> { index }, steps);

                    // otherwise, return previous value
                    else if (firstOccurence == -1)
                        return Tuple.Create(new List<int> { index - 1 }, steps);

                    // if value is correct then return list containing all exact matches
                    else
                        return Tuple.Create(
                            Enumerable.Range(firstOccurence, index - firstOccurence).ToList(),
                            steps
                        );
                }

                // set first occurence variable to generate list when last occurence found
                else if (localValue == value && firstOccurence == -1)
                {
                    firstOccurence = index;
                }
                
                // increase index value and set last value for reference
                lastValue = localValue;
                index++;
            }
            
            // if value not found, return highest possible index as value is too high
            return Tuple.Create(new List<int> { list.Count - 1 }, steps);
        }

        private List<int> GetAllIndexes(List<int> list, int index, int value)
        {
            List<int> indexes = new List<int>();

            // for every value from index to 0, if value is also exact match
            // then add to index list, otherwise end loop
            for (int i = index; i >= 0; i--)
            {
                if (list[i] == value)
                    indexes.Add(i);
                else
                    break;
            }

            // for every value from index to list max, if value is exact match
            // then also add to index list, otherwise end loop
            for (int i = index + 1; i < list.Count; i++)
            {
                if (list[i] == value)
                    indexes.Add(i);
                else
                    break;
            }

            // return list of exact matching indexes
            return indexes;
        }

        public Tuple<List<int>, int> BinarySearch(List<int> list, int value)
        {
            int minNum = 0;
            int maxNum = list.Count - 1;
            int midNum = 0;
            int steps = 0;

            while (minNum <= maxNum)
            {
                // set midnum to median value between min and max values
                // increment step counter
                midNum = (minNum + maxNum) / 2;
                steps++;

                // if value is found, find all nearby indexes of exact matches and return
                if (value == list[midNum])
                    return Tuple.Create(GetAllIndexes(list, ++midNum, value), steps);
                
                // otherwise if value is too low, halve the range by setting max num to current mid
                else if (value < list[midNum])
                    maxNum = midNum - 1;
                
                // if value too high, halve range by setting min num to current mid
                else
                    minNum = midNum + 1;
            }

            // if no exact match found, return current mid number as index
            // which represents the closest match to given input
            return Tuple.Create(new List<int> { midNum }, steps);
        }
    }
}
