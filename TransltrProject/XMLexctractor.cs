using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace TransltrProject
{
    class XMLexctractor
    {
        private string path;
        public List<object> data = new List<object>();
        public XMLexctractor(string path)
        {

            StreamReader sr = new StreamReader(path);
            string lines = sr.ReadToEnd();
            lines = lines.Replace("<i>", string.Empty).Replace("</i>", string.Empty);
            sr.Close();
            StreamWriter sw = new StreamWriter(path);
            sw.Write(lines);
            sw.Close();


            using (FileStream xmlStream = new FileStream(path, FileMode.Open))
            {
                using (XmlReader xmlReader = XmlReader.Create(xmlStream))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(document));
                    document deserializedStudents = serializer.Deserialize(xmlReader) as document;
                    foreach (var item in deserializedStudents.s)
                    {
                        data.AddRange(item.Items.ToList());
                    }
                }
            }
        }
    }
}
