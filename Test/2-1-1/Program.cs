using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2_1_1
{
    class Program
    {
        public const string Ans = "3456"; //密碼鎖的解

        static void Main(string[] args)
        {
            Program p = new Program();
            StreamWriter sw = new StreamWriter(@"2-1-1.txt");
                     
            string start = "0000"; //初始值
            string best = null; //目前最佳解
            int value = p.Distance(start); //差距值
            int times = 10000; //次數

            for (int i = 0; i < 10000; i++) //猜10000次
            {
                start = p.Add(start);
                int newValue = p.Distance(start);
                if (newValue < value)
                {
                    value = newValue;
                    best = start;
                }

                Console.WriteLine("目前最佳解={0}", best);
                Console.WriteLine("差距值={0}", value);
                sw.WriteLine(value);

                if (value == 0)//找到了
                {
                    times = i + 1;
                    break;
                }
            }

            Console.WriteLine("猜測次數:{0}", times);
            sw.Close();
            Console.ReadLine();
        }

        /// <summary>
        /// 將數字加一
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public string Add(string number)
        {
            int temp = Convert.ToInt32(number);
            temp = temp + 1;
            if (temp == 10000)
                temp = 0;

            string str = Convert.ToString(temp);

            if (temp < 10)
                str = str.Insert(0, "000");
            else if (temp < 100)
                str = str.Insert(0, "00");
            else if (temp < 1000)
                str = str.Insert(0, "0");

            return str;
        }

        /// <summary>
        /// 計算與解答的差距值
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public int Distance(string number)
        {
            int result = 0;
            for (int i = 0; i < 4; i++)
                result = result + Math.Abs(Convert.ToInt32(number[i]) - Convert.ToInt32(Ans[i]));
            return result;
        }
    }
}
