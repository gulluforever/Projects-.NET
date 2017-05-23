using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Article_Copier
{
    class Converters
    {
        public static string FormatHTMLtoWiki(string code)
        {
            code = FormatHtmlIntoBBCode(code);
            code = code.Replace("[url=", "[");
            code = code.Replace("]", " ");
            code = code.Replace("[/url", "]");
            return code;

        }
        public static string FormatHTMLtoPlainUrl(string code)
        {
            code = FormatHtmlIntoBBCode(code);
            code = code.Replace("[url=", "");
            code = code.Replace("]", " ");
            code = code.Replace("[/url", "");
            return code;
        }
        public static string FormatHTMLtoWikkaWiki(string code)
        {
            code = FormatHtmlIntoBBCode(code);

            code = code.Replace("[url=", "[[");
            
            code = code.Replace("]", " ");
            code = code.Replace("[/url", "]]");
            return code;
        }
        public static string FormatHTMLtoDokuWiki(string code)
        {
            code = FormatHtmlIntoBBCode(code);

            code = code.Replace("[url=", "[[");
            code = code.Replace("]", "|");
            code = code.Replace("[/url|", "]]");
            return code;
        }
        public static string FormatHTMLtoTikiWIki(string code)
        {
            code = FormatHtmlIntoBBCode(code);

            code = code.Replace("[url=", "[");
            code = code.Replace("]", "|");
            code = code.Replace("[/url|", "]");
            return code;
        }
        public static string FormatHtmlIntoBBCode(string desc)
        {
            desc = Regex.Replace(desc, @"<br(.*?) />", "[br]", RegexOptions.IgnoreCase);
            desc = Regex.Replace(desc, @"<UL[^>]*>", "[ulist]", RegexOptions.IgnoreCase);
            desc = Regex.Replace(desc, @"<\/UL>", "[/ulist]", RegexOptions.IgnoreCase);
            desc = Regex.Replace(desc, @"<OL[^>]*>", "[olist]", RegexOptions.IgnoreCase);
            desc = Regex.Replace(desc, @"<\/OL>", "[/olist]", RegexOptions.IgnoreCase);
            desc = Regex.Replace(desc, @"<LI>", "[*]", RegexOptions.IgnoreCase);
            desc = Regex.Replace(desc, @"<\/li>", "", RegexOptions.IgnoreCase);
            desc = Regex.Replace(desc, @"<B>", "[b]", RegexOptions.IgnoreCase);
            desc = Regex.Replace(desc, @"<\/B>>", "[/b]", RegexOptions.IgnoreCase);
            desc = Regex.Replace(desc, @"<STRONG>", "[strong]", RegexOptions.IgnoreCase);
            desc = Regex.Replace(desc, @"<\/STRONG>", "[/strong]", RegexOptions.IgnoreCase);
            desc = Regex.Replace(desc, @"<u>", "[u]", RegexOptions.IgnoreCase);
            desc = Regex.Replace(desc, @"<\/u>", "[/u]", RegexOptions.IgnoreCase);
            desc = Regex.Replace(desc, @"<i>", "[i]", RegexOptions.IgnoreCase);
            desc = Regex.Replace(desc, @"<\/i>", "[/i]", RegexOptions.IgnoreCase);
            desc = Regex.Replace(desc, @"<em>", "[em]", RegexOptions.IgnoreCase);
            desc = Regex.Replace(desc, @"<\/em>", "[/em]", RegexOptions.IgnoreCase);
            desc = Regex.Replace(desc, @"<sup>", "[sup]", RegexOptions.IgnoreCase);
            desc = Regex.Replace(desc, @"<\/sup>", "[/sup]", RegexOptions.IgnoreCase);
            desc = Regex.Replace(desc, @"<sub>", "[sub]", RegexOptions.IgnoreCase);
            desc = Regex.Replace(desc, @"<\/sub>", "[/sub]", RegexOptions.IgnoreCase);
            desc = Regex.Replace(desc, @"<HR[^>]*>", "[hr]", RegexOptions.IgnoreCase);
            desc = Regex.Replace(desc, @"<STRIKE>", "[strike]", RegexOptions.IgnoreCase);
            desc = Regex.Replace(desc, @"<\/STRIKE>", "[/strike]", RegexOptions.IgnoreCase);
            desc = Regex.Replace(desc, @"<h1>", "[h1]", RegexOptions.IgnoreCase);
            desc = Regex.Replace(desc, @"<\/h1>", "[/h1]", RegexOptions.IgnoreCase);
            desc = Regex.Replace(desc, @"<h2>", "[h2]", RegexOptions.IgnoreCase);
            desc = Regex.Replace(desc, @"<\/h2>", "[/h2]", RegexOptions.IgnoreCase);
            desc = Regex.Replace(desc, @"<h3>", "[h3]", RegexOptions.IgnoreCase);
            desc = Regex.Replace(desc, @"<\/h3>", "[/h3]", RegexOptions.IgnoreCase);
            desc = Regex.Replace(desc, "<A HREF=\"", "[url=", RegexOptions.IgnoreCase);
            desc = Regex.Replace(desc, "\">", "]", RegexOptions.IgnoreCase);
            desc = Regex.Replace(desc, @"<\/A>", "[/url]", RegexOptions.IgnoreCase);
            desc = Regex.Replace(desc, @"'>", "']", RegexOptions.IgnoreCase);

            //match on image tags
            var match = Regex.Matches(desc, @"<IMG[\s\S]*?SRC=\'([\s\S]*?)\'[\s\S]*?>", RegexOptions.IgnoreCase);
            if (match.Count > 0)
                desc = Regex.Replace(desc, match[0].ToString(), "[img]" + match[0].Groups[1].Value + "[/img]", RegexOptions.IgnoreCase);

            return desc;
        }
    }
}
