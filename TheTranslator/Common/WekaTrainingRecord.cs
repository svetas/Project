using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheTranslator.Common
{
    public class WekaTrainingRecord
    {
        private int totalSeen;
        private int totalGood;

        public WekaTrainingRecord(string source,string target)
        {
            Source = source;
            Target = target;
        }


        public void AddGood()
        {
            totalGood++;
            totalSeen++;
        }
        public void AddBad()
        {
            totalSeen++;
        }

        public string Source { get; set; }
        public string Target { get; set; }

        public int OurLength { get; set; }
        public int SourceLength { get; set; }

    }
}
