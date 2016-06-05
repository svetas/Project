using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace TransltrProject
{
    static class Program
    {
        static void Main()
        {
            char ch = 'a';
            Extractor ex;
            switch (ch)
            {
                case 's':
                    /*

                      ~~~~~    Extractor    ~~~~~
                      First param: Raw DB Path
                      Second Parma: Sorted DB Path
                      Third Param: Ratio to scan: 1 - scan all, 0.5 - scan first half of folders, 0 - dont scan at all
                      
                    */

                    //ex = new Extractor(@"D:\Users\User\Desktop\manualDownload\sorted", @"D:\DB", 1);
                    //ex.SyncFilesInInputPath();
                    Logger.Flush();
                    break;
                case 'r':
                    /*

                      ~~~~~ Phrases extractor ~~~~

                    */
                    //ex = new Extractor(@"D:\DB", @"D:\DB", 1);
                    //ex.ExtractAsSrtOnlyHebrew();
                    Logger.Flush();                   
                    break;
                case 'a':
                    string[] paths = { @"D:\chromeDownloads\Sorted" , @"D:\data\sdarot", @"D:\DB" };
                    ex = new Extractor(paths, @"D:\AlignedSentencesDB", 1);
                    ex.ExtractAsSrt();
                    Logger.Flush();
                    break;
                case 'b':
                    string[] paths2 = { @"D:\chromeDownloads\SortedSynced" };
                    ex = new Extractor(paths2, @"D:\AlignedSentencesReferences", 1);
                    ex.ExtractAsSrt();
                    Logger.Flush();
                    break;
                default:
                    break;
            }
        }
    }
}
