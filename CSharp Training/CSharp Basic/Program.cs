using System.Collections;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace CSharp_Basic
{

    enum Days
    {
        Monday,
        Tuesday,
        Wednesday
    }


    class Program  : Program3
    {
        public static string test = "test";

        //method overriding 
        //public void A()
        //{
        //    Debug.WriteLine("we are in program");
        //}

       
        public static void GetSum(int a, int b)
        {
            int sum;
            sum = a + b;
            Debug.WriteLine($"sum is {sum}");
        }

        private static void Main(string[] args)
        {
            Program p = new Program();
            //p.GetSum(5, 10);
            p.CheckCount();
            //p.sort();
            p.A();
            GetSum(5, 10);

            int x = 8;
            int y = 10;
            p.swap<int>(ref x, ref y);


            p.readWriteFile();

            Program1 p1 = new Program1();
            p1.findLargestNumber();

            Program3 p3 = new Program();
            p3.A();


            p3.showMatch("A Thousand Splendid Sun",@"\bS\S*");

            ITest1 i1 = new Program1();
            i1.getTest1();
            int r = 10;
            const double pi = 3.14;
            double area = pi * r * r;
            //Debug.WriteLine($"area of circle is {area}");
        }


        public void CheckCount()
        {
            string str = "abbbcccd";
            var count = 0;
            for (int i = 0; i < str.Length; i++)
            {

                if (str[i] == 'b') {
                    count += 1;
                }


            }
            Debug.WriteLine($"count of b {count}");
        }


        public void sort()
        {
            int[] intArray = { 1, 7, 9, 15, 8, 2 };
            int temp = 0;
            //ArrayList sortArray = new ArrayList();

            for (int i = 0; i < intArray.Length; i++)
            {

                //Debug.Write(temp);
                for (int j = 0; j < intArray.Length; j++) {
                    if (intArray[i] < intArray[j])
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
            foreach (int i in intArray) {
                Debug.Write(i);
            }



        }

        // read write in file
        public void readWriteFile()
        {
            string[] data = { "I", "am", "Ankit", "Bhavsar" };

            using (StreamWriter sw = new StreamWriter("/Users/ankit/BeyondKey/Local/QAAutomation/CSharp Training/CSharp Basic/abc.txt"))
            {
                foreach (string str in data)
                {
                    sw.WriteLine(str);
                }
            }

            string line = "";
            using (StreamReader sr = new StreamReader("/Users/ankit/BeyondKey/Local/QAAutomation/CSharp Training/CSharp Basic/abc.txt"))
            {
                while ((line = sr.ReadLine()) != null)
                {
                    Debug.WriteLine(line);
                }
            }
        }

        //Switch and Enum
        public void enumMethod()
        {
            Days day = Days.Monday;
            switch (day)
            {
                case Days.Monday:
                    Debug.WriteLine(day);
                    break;
                case Days.Tuesday:
                    Debug.WriteLine(day);
                    break;
                case Days.Wednesday:
                    Debug.WriteLine(day);
                    break; 

                default:
                    Debug.WriteLine("Defaul "+day);
                    break;
            }

            int dayNum = (int)Days.Monday;
            Debug.WriteLine(dayNum);
            Debug.WriteLine((Days)dayNum);
        }

        //Genric Method
        public void swap<T>(ref T a, ref T b)
        {
            T temp;
            temp = a;
            a = b;
            b = temp;

            Debug.WriteLine("after swaping {0}, {1}", a, b);
        }


        public void divison(int a, int b)
        {
            int result = 0;

            try
            {
                result = a / b;

            }catch(Exception e)
            {
                Debug.WriteLine("exception" + e);
                result = 0;
            }
            finally
            {
                Debug.WriteLine("result" + result);
            }
        }
        
    }

    internal class Program3
    {
        public void A()
        {
            Debug.WriteLine("we are in program 3");
        }

        public void showMatch(string text, string pattern)
        {
            MatchCollection mc = Regex.Matches(text, pattern);

            foreach (Match m in mc)
            {
                Debug.WriteLine(m);
            }
        }
    }


}