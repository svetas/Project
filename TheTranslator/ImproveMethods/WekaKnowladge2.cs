using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheTranslator.DataManager;

namespace TheTranslator.ImproveMethods
{
    class WekaKnowladge2 : ImprovementMethod
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
            }
            else
            {
                int ourLength = ourTrans.Split(' ').Length;
                int mosesLength = otherTrans.Split(' ').Length;
                int sourceLength = source.Split(' ').Length;
                char containsShort = ourTrans.Contains("&apos;") ? 't' : 'f';
                int timesSeen = (int)DBManager.GetInstance().GetSet(source, ourTrans);


                if (timesSeen <= 1)
                {
                    if (ourLength <= 6)
                    {
                        if (ourLength <= 1)
                        {
                            if (sourceLength <= 1) takeOur = true;
                            else takeOur = false;
                        } else
                        {
                            if (ourLength<=4)
                            {
                                if (mosesLength<=5)
                                {
                                    if (mosesLength <= 3)
                                    {
                                        if (ourLength <= 2) takeOur = true;
                                        else takeOur = false;
                                    } else
                                    {
                                        takeOur = true;
                                    }
                                } else
                                {
                                    if (sourceLength <= 3) takeOur = false;
                                    else takeOur = true;
                                }
                            } else
                            {
                                if (mosesLength <= 5) takeOur = false;
                                else
                                {
                                    if (ourLength <= 5) takeOur = true;
                                    else
                                    {
                                        if (mosesLength <= 6) takeOur = false;
                                        else takeOur = true;
                                    }
                                }
                            }
                        }                       
                    } else
                    {
                        if (mosesLength <= 7) takeOur = false;
                        else takeOur = true;
                    }
                } else
                {
                    if (ourLength <= 1)
                    {
                        if (mosesLength <= 2)
                        {
                            if (timesSeen <= 9933)
                            {
                                if (timesSeen <= 3297)
                                {
                                    if (timesSeen <= 3058)
                                    {
                                        if (timesSeen <= 498) takeOur = true;
                                        else
                                        {
                                            if (timesSeen <= 1081)
                                            {
                                                if (timesSeen <= 1059) takeOur = true;
                                                else takeOur = false;
                                            }
                                            else takeOur = true;
                                        }
                                    }
                                    else takeOur = false;
                                }
                                else takeOur = true;
                            } else
                            {
                                if (timesSeen <= 10826) takeOur = false;
                                else takeOur = true;
                            }
                        }else
                        {
                            if (mosesLength <= 3)
                            {
                                if (sourceLength <= 1) takeOur = true;
                                else takeOur = false;
                            }
                            else takeOur = true;
                        }
                    } else
                    {
                        takeOur = true;
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
}
