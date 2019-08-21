using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace GitTableauCleanFilter
{
    public static class TalbaeuWorkbookCleanFilter
    {
        public static string CleanTwbXml(string tableauxml, List<Tuple<string, string>> pathConvertions)
        {
            var xdoc = XDocument.Parse(tableauxml);

            var xmldec = BuildXmlDeclaration(xdoc);

            // Remove all BASE64 thumbnail images.
            var thumbnails_elems = xdoc.Root.Elements("thumbnails");
            if (thumbnails_elems.Any())
            {
                thumbnails_elems.First().Remove();
            }

            // Revert sepcial charactors to original file as same as possible, just in case.
            var xmlbody = xdoc.ToSingleQuoteString();
            xmlbody = xmlbody.Replace("\"", "&quot;");
            xmlbody = xmlbody.Replace("&#xD;&#xA;", "&#13;&#10;");
            xmlbody = xmlbody.Replace("Æ\n", "Æ&#10;");

            // Convert absolute path to relative path based on dir where tableau file exists.
            foreach (var t in pathConvertions)
            {
                xmlbody = xmlbody.Replace(t.Item1, t.Item2);
            }

            var outputxml =
      $@"{xmldec}

{xmlbody}";

            return outputxml;
        }

        private static string BuildXmlDeclaration(XDocument xdoc)
        {
            xdoc.Declaration = new XDeclaration("1.0", "utf-8", null);
            var xmldec = xdoc.Declaration.ToString();
            xmldec = xmldec.Replace('"', '\'');
            return xmldec;
        }
    }
}