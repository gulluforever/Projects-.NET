using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkUnSpinner
{
    class NewSpin
    {
        public static Random rad = new Random();
        public static string Spin(string content)
        {
            int index = content.IndexOf('{');
            int length = content.IndexOf('}');
            if ((index == -1) && (length == -1))
            {
                return content;
            }
            if ((index == -1) || (length < index))
            {
                return content;
            }
            string str2 = Spin(content.Substring(index + 1, content.Length - (index + 1)));
            length = str2.IndexOf('}');
            string[] strArray = str2.Substring(0, length).Split(new char[] { '|' });
            string str3 = strArray[rad.Next(0, strArray.Length)];
            return (content.Substring(0, index) + str3 + Spin(str2.Substring(length + 1, str2.Length - (length + 1))));
        }
    }
}
