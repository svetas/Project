using RDotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.Distributions;

namespace BLEUevaluator
{
    public class SignTest
    {
        //public static Dictionary<string, Dictionary<string, double>> m_methodsComparison = new Dictionary<string, Dictionary<string, double>>();

        public static double CalcConfidence(int count,int totalTries)//string sFirst, string sSecond)
        {

            REngine engine = REngine.GetInstance();
            string[] a = engine.Evaluate("binom.test("+ count + ", "+ totalTries + ")").AsCharacter().ToArray();
            string pValue = a[2];
            return double.Parse(pValue);          
        }
       
    }
}
