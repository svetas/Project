using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheTranslator.Common
{
    class Logger
    {
        private Logger()
        {
            string CurrentDate = DateTime.Now.ToFileTime().ToString();
            string fileName = "SwitchedSentences_"+ CurrentDate + ".csv";
            sw = new StreamWriter(fileName);
        }

        private static Logger m_instance;
        private static StreamWriter sw;
        public static Logger GetInstance()
        {
            if (m_instance == null)
                m_instance = new Logger();
            return m_instance;
        }
        public void Open()
        {
            m_instance = new Logger();
        }
        public void WriteLine(string line)
        {
            sw.WriteLine(line);
        }

        public void Close()
        {
            sw.Close();
        }
    }
}
