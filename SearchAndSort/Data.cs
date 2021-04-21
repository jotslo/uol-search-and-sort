using System;
using System.Collections.Generic;
using System.Text;

namespace SearchAndSort
{
    class Data
    {
        public string Descriptions =
@"--------------------------------
Search and Sort Program
--------------------------------
Selected Road: 'Road {0}'

Type '1'     Road 1 (256 values)
Type '2'     Road 2 (256 values)
Type '3'     Road 3 (256 values)
Type '4'   Road 1&3 (512 values)

Type '1+'   Road 1 (2048 values)
Type '2+'   Road 2 (2048 values)
Type '3+'   Road 3 (2048 values)
Type '4+' Road 1&3 (4096 values)
--------------------------------
Sorting Algorithms

Type 'bu'            Bubble Sort
Type 'in'         Insertion Sort
Type 'm'              Merge Sort
Type 'q'              Quick Sort
--------------------------------
Searching Algorithms
Note: Data must be sorted first.

Type 'l'           Linear Search
Type 'bi'          Binary Search
--------------------------------
";

        public Dictionary<string, string> InputMap = new Dictionary<string, string>();

        public Data()
        {
            // input map - keys the user can enter to refer to function names
            InputMap.Add("bu", "BubbleSort");
            InputMap.Add("in", "InsertionSort");
            InputMap.Add("m", "MergeSort");
            InputMap.Add("q", "QuickSort");
            InputMap.Add("l", "LinearSearch");
            InputMap.Add("bi", "BinarySearch");
        }
    }
}
