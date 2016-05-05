using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheTranslator
{
    public class DistanceStatistics : Statistics
    {
        //
        //
        Dictionary<string, Dictionary<string, int>[]> m_Statistics = new Dictionary<string, Dictionary<string, int>[]>();
        //      
        //
        public override void Insert(string target)
        {
            string[] linePartsTa = target.Split(new string[] { " ", "," }, StringSplitOptions.RemoveEmptyEntries);

            if (linePartsTa.Length == 0)
                return;

            // count 
            string currentWord;
            string otherWord;
            for (int i = 0; i < linePartsTa.Length; i++)
            {
                currentWord = linePartsTa[i];
                if (m_Statistics.ContainsKey(currentWord))
                {
                    Dictionary<string, int>[] followings = new Dictionary<string, int>[3];
                    m_Statistics.Add(currentWord, followings);
                }
                if (i + 1 < linePartsTa.Length)
                {
                    otherWord = linePartsTa[i + 1];
                    if (!m_Statistics[currentWord][0].ContainsKey(otherWord))
                    {
                        m_Statistics[currentWord][0].Add(otherWord, 1);
                    }else
                    {
                        m_Statistics[currentWord][0][otherWord]++;
                    }
                }
                if (i + 2 < linePartsTa.Length)
                {
                    otherWord = linePartsTa[i + 2];
                    if (!m_Statistics[currentWord][0].ContainsKey(otherWord))
                    {
                        m_Statistics[currentWord][0].Add(otherWord, 1);
                    }
                    else
                    {
                        m_Statistics[currentWord][0][otherWord]++;
                    }
                }
                if (i + 3 < linePartsTa.Length)
                {
                    otherWord = linePartsTa[i + 3];
                    if (!m_Statistics[currentWord][0].ContainsKey(otherWord))
                    {
                        m_Statistics[currentWord][0].Add(otherWord, 1);
                    }
                    else
                    {
                        m_Statistics[currentWord][0][otherWord]++;
                    }
                }
            }
        }
    }
}
