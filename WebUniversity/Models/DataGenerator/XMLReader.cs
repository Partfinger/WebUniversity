using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace WebUniversity.Models
{
    static class XMLReader
    {
        static XMLReader()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }

        public static string[] GetNamesArray(string path)
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(path);
            return ToStringArray(xDoc.DocumentElement.ChildNodes);
        }

        static string[] ToStringArray(XmlNodeList nodeList)
        {
            string[] names = new string[nodeList.Count];
            if (names.Length == 0)
            {
                throw new Exception();
            }
            int index = 0;
            foreach (XmlElement currentXMLElement in nodeList)
            {
                names[index] = currentXMLElement.InnerText;
                index++;
            }
            return names;
        }
    }
}
