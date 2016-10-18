using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;
 
namespace Project1
{
    static class CreateCheckCodeUtility
    {
        /// <summary>
        /// 生成随机数
        /// </summary>
        /// <returns></returns>
        public static int CreateRandomNumber()
        {
            byte[] num = new byte[4];
            RNGCryptoServiceProvider irgnum = new RNGCryptoServiceProvider();
            irgnum.GetBytes(num);//对num进行处理，返回值void，改变了num本身的值（num本身为空，现在为四个随机数）
 
            int randomNumber = BitConverter.ToInt32(num,0);
            return Math.Abs(randomNumber);
        }
       /// <summary>
       /// 生成随机code
       /// </summary>
       /// <param name="randomNumber"></param>
       /// <returns></returns>
        public static string CreateCode()
        {
            int randomNumber;
            char code;
            string checkcode = String.Empty;
 
            while (checkcode.Length < 5)
            {
                randomNumber = CreateCheckCodeUtility.CreateRandomNumber();
 
                if (randomNumber % 2 == 0)
                {
                    //生成1-9中随机的数字
                    code = (char)('0' + (char)(randomNumber % 10));
                }
                else
                {
                    //生成A-Z中随机的数字
                    code = (char)('A' + (char)(randomNumber % 26));
                }
 
                checkcode += code.ToString();
            }
 
            return checkcode;
        }
        /// <summary>
        /// 生成中文汉字码
        /// </summary>
        /// <returns></returns>
        public static string CreateChineseCode()
        {
            string[] s = new String[16] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "A", "B", "C", "D", "E", "F" };
            object[] chineseCodeNumber = new object[4];
            Random random = new Random();
            for (int i = 0; i < 4; i++)
            {
                //生成四位区位码
                int r1 = random.Next(0, 16);
                int r2 = random.Next(0, 16);
                int r3 = random.Next(0, 16);
                int r4 = random.Next(0, 16);
 
                string r1_1 = s[r1].Trim();
                string r1_2 = s[r2].Trim();
                string r1_3 = s[r3].Trim();
                string r1_4 = s[r4].Trim();
 
                byte byte1 = Convert.ToByte(r1_1 + r1_2, 16);
                byte byte2 = Convert.ToByte(r1_3 + r1_4, 16);
                byte[] str = new byte[] { byte1, byte2 };
 
                chineseCodeNumber.SetValue(str, i);
            }
 
            Encoding gb = Encoding.GetEncoding("unicode");
            string code_1 = gb.GetString((byte[])Convert.ChangeType(chineseCodeNumber[0], typeof(byte[])));
            string code_2 = gb.GetString((byte[])Convert.ChangeType(chineseCodeNumber[1], typeof(byte[])));
            string code_3 = gb.GetString((byte[])Convert.ChangeType(chineseCodeNumber[2], typeof(byte[])));
            string code_4 = gb.GetString((byte[])Convert.ChangeType(chineseCodeNumber[3], typeof(byte[])));
            string code = code_1 + code_2 + code_3 + code_4;
 
            return code;
 
        }
    }
}
 
