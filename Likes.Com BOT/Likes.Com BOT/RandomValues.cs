using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Likes.Com_BOT
{
    class RandomValues
    {
        public static string UppercaseFirst(string s)
        {
            // Check for empty string.
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }
            // Return char and concat substring.
            return char.ToUpper(s[0]) + s.Substring(1);
        }
        public static string GetName()
        {
            String[] randFirst = Properties.Resources.names.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            return randFirst[rnd.Next(randFirst.Length)];
        }
        public static string GenerateEmailandUserName(int type)
        {
            string strPwdchar = "abcdefghijklmnopqrstuvwxyz";
            string strPwd = "";

            for (int i = 0; i <= 7; i++)
            {
                int iRandom = rnd.Next(0, strPwdchar.Length - 1);
                strPwd += strPwdchar.Substring(iRandom, 1);
            }
            if (type == 1)
            {
                return strPwd + "@gmail.com";
            }
            else
            {
                return strPwd;
            }

        }
        public static string MixedPassowrd()
        {
            string strPwdchar = "abcdefghijklmnopqrstuvwxyz";
            string strPwdHigh = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string strpwdnum = "1234567890";
            string strPwd = "";

            for (int i = 0; i <= 3; i++)
            {
                int iRandom = rnd.Next(0, strPwdchar.Length - 1);
                int sRandom = rnd.Next(0, strPwdHigh.Length - 1);
                int qRandom = rnd.Next(0, strpwdnum.Length - 1);
                strPwd += strPwdchar.Substring(iRandom, 1) + strPwdHigh.Substring(sRandom, 1) + strpwdnum.Substring(qRandom, 1);
            }

            return strPwd;


        }
        public static Random rnd = new Random();
    }
}
