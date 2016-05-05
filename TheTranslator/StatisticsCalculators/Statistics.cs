using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheTranslator
{
    public abstract class Statistics
    {
        // Train the languange model by inserting it sentences which can be analyzed
        /// <summary>
        /// Collect statistics about the english sentences
        /// </summary>
        /// <param name="target"></param>
        public abstract void Insert(string target);

        
    }
}
