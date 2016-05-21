using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheTranslator.Common
{
    class PostTranData
    {
        public PostTranData(string source,string reference,string hypo,string otherMachineOutput,int selectedTranslation)
        {
            Source = source;
            Reference = reference;
            Hypo = hypo;
            OtherMachineOutput = otherMachineOutput;
            SelectedTranslation = selectedTranslation;
        }

        public string Source { get; set; }
        public string Reference { get; set; } 
        public string Hypo { get; set; }
        public string OtherMachineOutput { get; set; }
        public int BetterBLEU { get; set; }
        public int SelectedTranslation { get; set; }



    }
}
