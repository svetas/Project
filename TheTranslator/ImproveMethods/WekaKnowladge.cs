using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheTranslator.DataManager;

namespace TheTranslator.ImproveMethods
{
    class WekaKnowladge : ImprovementMethod
    {

        // This returns 31.66 , moses is 31.51

        public override string ChooseBetter(string ourTrans, string otherTrans, string source, out int selected)
        {
            bool takeOur = false;

            if (ourTrans == null || ourTrans.Length == 0)
            {
                selected = -1;
                IncrementCounter("Picke Moses");
                return otherTrans;
            } else
            {
                int ourLength = ourTrans.Split(' ').Length;
                int mosesLength = otherTrans.Split(' ').Length;
                int sourceLength = source.Split(' ').Length;
                char containsShort = ourTrans.Contains("&apos;") ? 't' : 'f';
                int timesSeen = (int)DBManager.GetInstance().GetSet(source, ourTrans);



                if (ourLength <= 2)
                {
                    if (mosesLength <= 4)
                    {
                        if (ourLength <= 1)
                        {
                            if (mosesLength <= 2) takeOur = true;
                            else takeOur = false;
                        }
                        else
                        {
                            if (containsShort == 'f')
                            {
                                if (mosesLength <= 2) takeOur = false;
                                else {
                                    if (timesSeen <= 3) takeOur = false;
                                    else takeOur = true;
                                }
                            }
                            else
                            {
                                takeOur = false;
                            }
                        }
                    } else
                    {
                        if (sourceLength <= 1) takeOur = true;
                        else
                        {
                            if (ourLength <= 1) takeOur = false;
                            else takeOur = true;
                        }
                    }
                } else // our length >2
                {
                    if (mosesLength <= 2) takeOur = false;
                    else
                    {
                        if (ourLength <= 3)
                        {
                            if (mosesLength <= 4)
                            {
                                if (timesSeen <= 1) takeOur = false;
                                else
                                {
                                    if (sourceLength <= 1)
                                    {
                                        if (timesSeen <= 2) takeOur = false;
                                        else takeOur = true;
                                    }
                                }
                            } else
                            {
                                takeOur = true;
                            }
                        } else // our length >3
                        {
                            if (mosesLength <= 3) takeOur = false;
                            else
                            {
                                if (ourLength <= 5)
                                {
                                    if (sourceLength <= 1) takeOur = true;
                                    else
                                    {
                                        if (containsShort == 'f') takeOur = false;
                                        else
                                        {
                                            if (ourLength <= 4)
                                            {
                                                if (mosesLength <= 4) takeOur = true;
                                                else takeOur = false;
                                            } else
                                            {
                                                if (mosesLength <= 4) takeOur = false;
                                                else takeOur = true;
                                            }
                                        }
                                    }
                                } else
                                {
                                    takeOur = false;
                                }
                            }
                        }
                    }
                }
            }

            if (takeOur)
            {
                selected = 1;
                IncrementCounter("weka");
                return ourTrans;
            }
            else
            {
                selected = -1;
                IncrementCounter("Picked moses");
                return otherTrans;
            }
        }


    }
}
