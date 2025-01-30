using System;
using System.Collections;
using System.Diagnostics;

namespace CSharp_Basic
{


    interface ITest1
    {
        void getTest1();
    } 
    public class Program1: ITest1
	{
		public void findLargestNumber()
		{
            int[] intArray = { 1, 7, 9, 15, 8, 2 };
            int temp = 0;
            //ArrayList sortArray = new ArrayList();

            for (int i = 0; i < intArray.Length; i++)
            {

                //Debug.Write(temp);
                for (int j = 0; j < intArray.Length; j++)
                {
                    if (intArray[i] > intArray[j])
                    {
                        temp = intArray[i];
                        //Debug.Write("i"+i);
                        //Debug.Write("j"+j);
                        intArray[i] = intArray[j];
                        intArray[j] = temp;

                        //Debug.Write("i" + intArray[i]);
                        //Debug.Write("j" + intArray[j]);

                    }
                }


            }
            Debug.Write("largest number" + intArray[0]);


        }

        public void collection()
        {
            ArrayList a = new ArrayList();
            a.Add(1);
            a.Add(2);
            a.Add(3);


            Dictionary<string, int> dict = new Dictionary<string, int>();
            dict.Add("A", 1);
            dict.Add("B", 2);
            Debug.WriteLine(dict.GetValueOrDefault("A"));

            foreach(KeyValuePair<string, int> pair in dict)
            {
                Debug.WriteLine("pair value {0}, {1}", pair.Key, pair.Value);
            }
            Hashtable ht = new Hashtable();
            ht.Add("A", 1);
            ht.Add(2, 3);

            foreach (DictionaryEntry i in ht)
            {
                Debug.WriteLine("pair value {0}, {1}", i.Key, i.Value);
            }
        }

        public void getTest1()
        {
            Debug.WriteLine("test ");
        }
    }

   
}

