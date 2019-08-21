using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;

namespace GitTableauCleanFilter
{
    public static class XmlExtensions
    {
        //original: https://blog.swiftsoftwaregroup.com/save-xdocument-xml-use-single-quotes-attribute-values
        public static string ToSingleQuoteString(this XDocument xdoc)
        {
            using (var sw = new StringWriter())
            using (var writer = new XmlTextWriter(sw))
            {
                writer.Formatting = Formatting.Indented;
                writer.QuoteChar = '\'';
                writer.WriteNode(xdoc.CreateNavigator(), false);
                writer.Flush();
                return sw.ToString();
            }
        }
    }
}