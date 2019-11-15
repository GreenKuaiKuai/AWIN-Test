using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2_1_4_All
{
    class Program
    {
        public const string Ans = "12345678901234567890"; //密碼鎖的解

        static void Main(string[] args)
        {
            Program p = new Program();
            StreamWriter sw = new StreamWriter(@"2-1-4.txt");

            string start = "00000000000000000000"; //初始值
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
            int nextPos = 19;

            while (nextPos != -1)//進位
            {
                string s = Convert.ToString(number[nextPos]);
                int temp = Convert.ToInt32(s);
                temp = temp + 1;
                if (temp == 10)//要進位
                    temp = 0;

                number = number.Insert(nextPos, Convert.ToString(temp));
                number = number.Remove(nextPos + 1, 1);

                if (temp == 0)//找下一位
                    nextPos = nextPos - 1;
                else
                    nextPos = -1;
            }
            return number;
        }

        /// <summary>
        /// 計算與解答的差距值
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public int Distance(string number)
        {
            int result = 0;
            for (int i = 0; i < 20; i++)
                result = result + Math.Abs(Convert.ToInt32(number[i]) - Convert.ToInt32(Ans[i]));
            return result;
        }
    }
}
