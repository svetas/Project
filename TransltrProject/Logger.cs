using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransltrProject
{
    class Logger
    {
        private static Logger logger;
        private static StreamWriter generalOutput;
        private static StreamWriter informationOutput;
        private static StreamWriter debuger;

        private Logger()
        {
            generalOutput = new StreamWriter("MsgLog.csv");
            string time = DateTime.Now.ToShortDateString().Replace('/','-');
            informationOutput = new StreamWriter(time + "DataLog.csv");
            debuger = new StreamWriter(time + "debuger.txt");

            generalOutput.WriteLine("Time, Total Movies, Sync Count, Avg precision");
            generalOutput.Flush();
        }
        public static void WriteLine(string text)
        {
            if (logger == null)
                logger = new Logger();

            text =DateTime.Now.ToLongDateString() + "," + text;
            generalOutput.WriteLine(text);
        }


        public static void WriteDebug(string text)
        {
            if (logger == null)
                logger = new Logger();
            debuger.WriteLine(text);
        }

        public static void WriteError(string filePath,string errorReason)
        {
            if (logger == null)
                logger = new Logger();

            informationOutput.WriteLine(filePath+","+errorReason);
        }

        ~Logger()
        {
            //generalOutput.Close();
            //informationOutput.Close();
        }

        public static void Flush()
        {
            generalOutput.Flush();
            informationOutput.Flush();
            debuger.Flush();
        }
    }
}
