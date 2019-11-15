using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2_1_5_SA
{
    class Program
    {
        public const string Ans = "12345678901234567890"; //密碼鎖的解

        static void Main(string[] args)
        {
            Program p = new Program();
            StreamWriter sw = new StreamWriter(@"2-1-5-SA.txt");

            string start = "00000000000000000000"; //初始值
            string best = null; //目前最佳解
            int value = p.Distance(start); //差距值
            int times = 10000; //次數
            double T = 100; //從100度開始降溫

            for (int i = 0; i < 10000; i++) //猜10000次
            {
                //隨機挑位置
                Random rnd = new Random(Guid.NewGuid().GetHashCode());
                int pos = rnd.Next(0, 20);
                //Console.WriteLine(pos);

                string betterStr = null;
                int betterValue = 200;

                //取得鄰近解
                string addStr = p.Add(start, pos);
                string minusStr = p.Minus(start, pos);
                int addValue = p.Distance(addStr);
                int minusValue = p.Distance(minusStr);

                if (addValue < minusValue) //+1較小
                {
                    betterStr = addStr;
                    betterValue = addValue;
                }
                else //-1較小
                {
                    betterStr = minusStr;
                    betterValue = minusValue;
                }

                if (p.P(value, betterValue, T) > rnd.NextDouble())
                    start = betterStr;

                if (betterValue < value)
                {
                    best = betterStr;
                    value = betterValue;
                }

                Console.WriteLine("目前最佳解={0}", start);
                Console.WriteLine("差距值{0}", value);
                sw.WriteLine(value);

                if (value == 0)//找到了
                {
                    times = i + 1;
                    break;
                }

                T = T * 0.8; //降溫
            }

            Console.WriteLine("猜測次數:{0}", times);
            sw.Close();
            Console.ReadLine();
        }

        /// <summary>
        /// 模擬退火法的機率函數
        /// </summary>
        /// <param name="oldValue"></param>
        /// <param name="newValue"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public double P(int oldValue, int newValue, double t)
        {
            if (newValue < oldValue)
                return 1;
            else
                return Math.Exp((oldValue - newValue) / t);
        }

        /// <summary>
        /// 將指定位數數字加一
        /// </summary>
        /// <param name="number"></param>
        /// <param name="pos">位數 0:千 1:百 2:十 3:個</param>
        /// <returns></returns>
        public string Add(string number, int pos)
        {
            string s = Convert.ToString(number[pos]);
            int temp = Convert.ToInt32(s);
            temp = temp + 1;
            if (temp == 10)
                temp = 0;

            number = number.Insert(pos, Convert.ToString(temp));
            number = number.Remove(pos + 1, 1);
            return number;
        }

        /// <summary>
        /// 將指定位數數字減一
        /// </summary>
        /// <param name="number"></param>
        /// <param name="pos">位數 0:千 1:百 2:十 3:個</param>
        /// <returns></returns>
        public string Minus(string number, int pos)
        {
            string s = Convert.ToString(number[pos]);
            int temp = Convert.ToInt32(s);
            temp = temp - 1;
            if (temp == -1)
                temp = 9;

            number = number.Insert(pos, Convert.ToString(temp));
            number = number.Remove(pos + 1, 1);
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
