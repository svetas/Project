using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransltrProject
{
    public class AlignmentSetting
    {
        //point 1 first movie, point 2 first movie, point 1 second movie, point 2 second movie
        public AlignmentSetting(int firstS, int firstE, int secondS, int secondE)
        {
            FirstS = firstS;
            FirstE = firstE;
            SecondS = secondS;
            SecondE = secondE;
        }
        public int FirstS { get; set; }
        public int FirstE { get; set; }
        public int SecondS { get; set; }
        public int SecondE { get; set; }
    }
}
