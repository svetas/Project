using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransltrProject
{
    public class Sentence
    {
        public int Index { get; set; }
        public string Text { get; set; }
        public int Start { get; set; }
        public int TimeToNext { get; set; }
        public int End { get; set; }
    
        public Sentence(Sentence Other)
        {
            this.Text=Other.Text;
            this.Start = Other.Start;
            this.TimeToNext=Other.TimeToNext;
            this.End=Other.End;
        }

        public Sentence(){}


        
        public override string ToString()
        {
            return Text;
        }
        
        public string Translate()
        {
            return Dictionary.Translate(Text.ToLower());
        }
    }
}

