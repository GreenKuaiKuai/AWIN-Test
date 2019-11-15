using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2_1_2
{
    class Program
    {
        public const string Ans = "3456"; //密碼鎖的解

        static void Main(string[] args)
        {
            Program p = new Program();

            string start = "0000"; //初始值
            int value = p.Distance(start); //差距值
            int times = 10000; //次數

            for (int i = 0; i < 10000; i++) //猜10000次
            {
                //隨機挑位置
                Random rnd = new Random(Guid.NewGuid().GetHashCode());
                int pos = rnd.Next(0, 4);
                //Console.WriteLine(pos);

                string addStr = p.Add(start, pos);
                string minusStr = p.Minus(start, pos);
                int addValue = p.Distance(addStr);
                int minusValue = p.Distance(minusStr);

                if (!(addValue < value || minusValue < value)) //有沒有比目前更好
                {
                    Console.WriteLine("目前最佳解={0}", start);
                    Console.WriteLine("差距值{0}", value);
                    continue; //沒有的話就跳過
                }

                if (addValue < minusValue) //+1較小
                {
                    start = addStr;
                    value = addValue;
                }
                else //-1較小
                {
                    start = minusStr;
                    value = minusValue;
                }

                Console.WriteLine("目前最佳解={0}", start);
                Console.WriteLine("差距值{0}", value);

                if (value == 0)//找到了
                {
                    times = i + 1;
                    break;
                }
            }

            Console.WriteLine("猜測次數:{0}", times);
            Console.ReadLine();
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
            for (int i = 0; i < 4; i++)
                result = result + Math.Abs(Convert.ToInt32(number[i]) - Convert.ToInt32(Ans[i]));
            return result;
        }
    }
}
