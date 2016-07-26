using BLEUevaluator;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using TheTranslator.Common;
using TheTranslator.DataManager;
using TheTranslator.Extractors;
using TheTranslator.ImproveMethods;

namespace TheTranslator.GUI
{
    public partial class Main : Form
    {
        public static Extractor m_extractor;
        ImprovementMethod m_subtitutionLogic;
        ImprovementMethod m_subtitutionLogic_backup;

        List<PostTranData> m_OutputTranslations;

        public Main()
        {
            InitializeComponent();
        }

        private void LoadToMemory()
        {
            try
            {

                CheckInputFiles();

                string trainEnPath = txtExperimentPath.Text + txtTrainingNames.Text + ".en";
                string trainHePath = txtExperimentPath.Text + txtTrainingNames.Text + ".he";
                string auxDictionaryPath = txtExperimentPath.Text + txtAuxDic.Text;

                m_extractor = new ExtractorDirect();

                int limitLearning = txtLimit.Text == "" ? 0 : int.Parse(txtLimit.Text);

                bool status = m_extractor.ScanWords(trainHePath, trainEnPath, auxDictionaryPath, limitLearning);

                if (status)
                    rtbMemoryStatus.Text = "Loading ended successfully";
                else
                    throw new Exception();
            }
            catch
            {
                rtbMemoryStatus.Text = "Loading Failed";
            }
        }
        private void ResetRedis()
        {
            if (m_extractor != null)
            {
                m_extractor.ResetDB();
            }
        }
        private void LoadToRedis()
        {
            try
            {
                CheckInputFiles();

                string trainEnPath = txtExperimentPath.Text + txtTrainingNames.Text + ".en";
                string trainHePath = txtExperimentPath.Text + txtTrainingNames.Text + ".he";
                string auxDictionaryPath = txtExperimentPath.Text + txtAuxDic.Text;

                m_extractor = new ExtractorDirect();

                int limitLearning = txtLimit.Text == "" ? 0 : int.Parse(txtLimit.Text);


                bool status = m_extractor.build(trainHePath, trainEnPath, auxDictionaryPath, limitLearning);

                if (status)
                    rtbMemoryStatus.Text = "Loading ended successfully";
                else
                    throw new Exception();
            }
            catch
            {
                rtbMemoryStatus.Text = "Loading Failed";
            }
        }
        private void CheckInputFiles()
        {

            if (txtTestNames.Text == "" || txtTrainingNames.Text == "" ||
                    txtTuningNames.Text == "" || txtExperimentPath.Text == "" || txtAuxDic.Text == "")
                throw new Exception();
            if (!Directory.Exists(txtExperimentPath.Text))
                throw new Exception();

            string testEnPath = txtExperimentPath.Text + txtTestNames.Text + ".en";
            string testHePath = txtExperimentPath.Text + txtTestNames.Text + ".he";

            string tuneEnPath = txtExperimentPath.Text + txtTuningNames.Text + ".en";
            string tuneHePath = txtExperimentPath.Text + txtTuningNames.Text + ".he";

            string trainEnPath = txtExperimentPath.Text + txtTrainingNames.Text + ".en";
            string trainHePath = txtExperimentPath.Text + txtTrainingNames.Text + ".he";

            string auxDictionaryPath = txtExperimentPath.Text + txtAuxDic.Text;

            if (!File.Exists(tuneHePath) || !File.Exists(testHePath) ||
                !File.Exists(tuneEnPath) || !File.Exists(tuneHePath) ||
                !File.Exists(trainEnPath) || !File.Exists(trainHePath) || !File.Exists(auxDictionaryPath))
                throw new Exception();
        }
        private void EnhanceMoses()
        {
            if (m_extractor != null)
            {
                m_extractor.Enhance();
            }
        }
        private void playMario()
        {
            Console.Beep(659, 125);
            Console.Beep(659, 125);
            Thread.Sleep(125);
            Console.Beep(659, 125);
            Thread.Sleep(167);
            Console.Beep(523, 125);
            Console.Beep(659, 125);
            Thread.Sleep(125);
            Console.Beep(784, 125);
            Thread.Sleep(375);
            Console.Beep(392, 125);
            Thread.Sleep(375);
            Console.Beep(523, 125);
            Thread.Sleep(250);
            Console.Beep(392, 125);
            Thread.Sleep(250);
            Console.Beep(330, 125);
            Thread.Sleep(250);
            Console.Beep(440, 125);
            Thread.Sleep(125);
            Console.Beep(494, 125);
            Thread.Sleep(125);
            Console.Beep(466, 125);
            Thread.Sleep(42);
            Console.Beep(440, 125);
            Thread.Sleep(125);
            Console.Beep(392, 125);
            Thread.Sleep(125);
            Console.Beep(659, 125);
            Thread.Sleep(125);
            Console.Beep(784, 125);
            Thread.Sleep(125);
            Console.Beep(880, 125);
            Thread.Sleep(125);
            Console.Beep(698, 125);
            Console.Beep(784, 125);
            Thread.Sleep(125);
            Console.Beep(659, 125);
            Thread.Sleep(125);
            Console.Beep(523, 125);
            Thread.Sleep(125);
            Console.Beep(587, 125);
            Console.Beep(494, 125);
            Thread.Sleep(125);
            Console.Beep(523, 125);
            Thread.Sleep(250);
            Console.Beep(392, 125);
            Thread.Sleep(250);
            Console.Beep(330, 125);
            Thread.Sleep(250);
            Console.Beep(440, 125);
            Thread.Sleep(125);
            Console.Beep(494, 125);
            Thread.Sleep(125);
            Console.Beep(466, 125);
            Thread.Sleep(42);
            Console.Beep(440, 125);
            Thread.Sleep(125);
            Console.Beep(392, 125);
            Thread.Sleep(125);
            Console.Beep(659, 125);
            Thread.Sleep(125);
            Console.Beep(784, 125);
            Thread.Sleep(125);
            Console.Beep(880, 125);
            Thread.Sleep(125);
            Console.Beep(698, 125);
            Console.Beep(784, 125);
            Thread.Sleep(125);
            Console.Beep(659, 125);
            Thread.Sleep(125);
            Console.Beep(523, 125);
            Thread.Sleep(125);
            Console.Beep(587, 125);
            Console.Beep(494, 125);
            Thread.Sleep(375);
            Console.Beep(784, 125);
            Console.Beep(740, 125);
            Console.Beep(698, 125);
            Thread.Sleep(42);
            Console.Beep(622, 125);
            Thread.Sleep(125);
            Console.Beep(659, 125);
            Thread.Sleep(167);
            Console.Beep(415, 125);
            Console.Beep(440, 125);
            Console.Beep(523, 125);
            Thread.Sleep(125);
            Console.Beep(440, 125);
            Console.Beep(523, 125);
            Console.Beep(587, 125);
            Thread.Sleep(250);
            Console.Beep(784, 125);
            Console.Beep(740, 125);
            Console.Beep(698, 125);
            Thread.Sleep(42);
            Console.Beep(622, 125);
            Thread.Sleep(125);
            Console.Beep(659, 125);
            Thread.Sleep(167);
            Console.Beep(698, 125);
            Thread.Sleep(125);
            Console.Beep(698, 125);
            Console.Beep(698, 125);
            Thread.Sleep(625);
            Console.Beep(784, 125);
            Console.Beep(740, 125);
            Console.Beep(698, 125);
            Thread.Sleep(42);
            Console.Beep(622, 125);
            Thread.Sleep(125);
            Console.Beep(659, 125);
            Thread.Sleep(167);
            Console.Beep(415, 125);
            Console.Beep(440, 125);
            Console.Beep(523, 125);
            Thread.Sleep(125);
            Console.Beep(440, 125);
            Console.Beep(523, 125);
            Console.Beep(587, 125);
            Thread.Sleep(250);
            Console.Beep(622, 125);
            Thread.Sleep(250);
            Console.Beep(587, 125);
            Thread.Sleep(250);
            Console.Beep(523, 125);
            Thread.Sleep(1125);
            Console.Beep(784, 125);
            Console.Beep(740, 125);
            Console.Beep(698, 125);
            Thread.Sleep(42);
            Console.Beep(622, 125);
            Thread.Sleep(125);
            Console.Beep(659, 125);
            Thread.Sleep(167);
            Console.Beep(415, 125);
            Console.Beep(440, 125);
            Console.Beep(523, 125);
            Thread.Sleep(125);
            Console.Beep(440, 125);
            Console.Beep(523, 125);
            Console.Beep(587, 125);
            Thread.Sleep(250);
            Console.Beep(784, 125);
            Console.Beep(740, 125);
            Console.Beep(698, 125);
            Thread.Sleep(42);
            Console.Beep(622, 125);
            Thread.Sleep(125);
            Console.Beep(659, 125);
            Thread.Sleep(167);
            Console.Beep(698, 125);
            Thread.Sleep(125);
            Console.Beep(698, 125);
            Console.Beep(698, 125);
            Thread.Sleep(625);
            Console.Beep(784, 125);
            Console.Beep(740, 125);
            Console.Beep(698, 125);
            Thread.Sleep(42);
            Console.Beep(622, 125);
            Thread.Sleep(125);
            Console.Beep(659, 125);
            Thread.Sleep(167);
            Console.Beep(415, 125);
            Console.Beep(440, 125);
            Console.Beep(523, 125);
            Thread.Sleep(125);
            Console.Beep(440, 125);
            Console.Beep(523, 125);
            Console.Beep(587, 125);
            Thread.Sleep(250);
            Console.Beep(622, 125);
            Thread.Sleep(250);
            Console.Beep(587, 125);
            Thread.Sleep(250);
            Console.Beep(523, 125);
            Thread.Sleep(625);
        }

        private void GenerateTranslations()
        {
            m_subtitutionLogic = null;
            m_subtitutionLogic_backup = null;
            Logger.GetInstance().Open();
            try {
                if (radioSeenLength.Checked)
                {
                    m_subtitutionLogic = new SureAndLong();
                }
                else if (radioCombine.Checked)
                {
                    m_subtitutionLogic = new LongCombiner(m_extractor);
                }
                else if (radioWeka1.Checked)
                {
                    m_subtitutionLogic = new WekaKnowladge();
                }
                else if (radioWeka2.Checked)
                {
                    m_subtitutionLogic = new WekaKnowladge2();
                }
                else if (radioWeka3.Checked)
                {
                    m_subtitutionLogic = new WekaKnowladge3();
                }
                else if (radioWeka4.Checked)
                {
                    m_subtitutionLogic = new WekaKnowladge4();
                }
                else if (radioWeka5.Checked)
                {
                    m_subtitutionLogic = new WekaKnowladge5();
                }
                else if (radioWeka6.Checked)
                {
                    m_subtitutionLogic = new WekaKnowladge5();
                    m_subtitutionLogic_backup = new LongCombiner(m_extractor);
                }
                else if (radioWeka7.Checked)
                {
                    m_subtitutionLogic = new WekaKnowladge6();
                }
                else if (radioWeka8.Checked)
                {
                    m_subtitutionLogic = new WekaKnowladge7();
                }
                else if (radioLongerSure.Checked)
                {
                    m_subtitutionLogic = new SureAndLong();
                }else if (radioGoogle.Checked)
                {
                    m_subtitutionLogic = new WekaKnowladgeGoogle2();
                }
                if (m_subtitutionLogic == null)
                {
                    return;
                }

                string testHePath = txtExperimentPath.Text + txtTestNames.Text + ".he";
                string testEnPath = txtExperimentPath.Text + txtTestNames.Text + ".en";
                string mosesTranslationPath = txtExperimentPath.Text + txtMosesPath.Text;

                if (!File.Exists(testHePath) || !File.Exists(mosesTranslationPath))
                    return;


                StreamReader srHe = new StreamReader(testHePath);
                StreamReader srEn = new StreamReader(testEnPath);
                StreamReader srM = new StreamReader(mosesTranslationPath);
                //StreamWriter srLogger = new StreamWriter(txtExperimentPath.Text+"TranslationLog.txt");
                m_OutputTranslations = new List<PostTranData>();

                string lineOur;
                int counter = 0, total = 0;

                while (!srHe.EndOfStream)
                {
                    if (total % 1000 == 0)
                    {
                        Console.WriteLine(total + " updated: " + counter);
                    }
                    string lineMoses = srM.ReadLine();
                    string lineSource = srHe.ReadLine();
                    string lineReference = srEn.ReadLine();
                    lineOur = m_extractor.ExtractExactTranslation(lineSource, 0,1)[0].Trim(' ');

                    if (lineMoses.Trim(' ')!=lineMoses || lineSource.Trim(' ')!= lineSource ||
                        lineOur.Trim(' ')!= lineOur)
                    {
                        lineMoses = lineMoses.Trim(' ');
                        lineOur = lineOur.Trim(' ');
                        lineSource = lineSource.Trim(' ');
                    }

                    total++;

                    

                    int selectedMachine;
                    int selectedMachine2 = 0;

                    string chosen = m_subtitutionLogic.ChooseBetter(lineOur, lineMoses,lineSource, out selectedMachine);
                    string chosen2;
                    if (m_subtitutionLogic_backup != null)
                    {
                        chosen2 = m_subtitutionLogic_backup.ChooseBetter(lineOur, lineMoses, lineSource, out selectedMachine2);
                        if (selectedMachine2 == 1)
                        {
                            selectedMachine = 1;
                            chosen = chosen2;
                        }
                    }
                    

                    if (selectedMachine==1)
                        counter++;

                    m_OutputTranslations.Add(new PostTranData(lineSource, lineReference, chosen, lineMoses,lineOur, selectedMachine));
                    WekaOutput.Add(lineSource, lineReference, lineMoses,lineOur);
                }

                srEn.Close();
                srHe.Close();
                srM.Close();

                Logger.GetInstance().Close();

                rtbTranslationProcedure.Text = "\r\n Finished loading.";
                rtbTranslationProcedure.Text += "\r\n We improved " + counter + "/" + total + ", thats " + counter / (double)total + "!";
                rtbTranslationProcedure.Text += "\r\n" + m_subtitutionLogic.ToString();
            }
            catch (Exception e)
            {
                rtbTranslationProcedure.Text = "Failed to Translate";
            }
        }

        private void RunSignTest()
        {
            int counter = 0;
            double firstBetter = 0;
            double secondBetter = 0;

            foreach (var translation in m_OutputTranslations)
            {
                string refe = translation.Reference;
                double bleuFirst = BLEU.Evaluate(translation.Hypo, new List<string> { refe });
                double bleuSecond = BLEU.Evaluate(translation.OtherMachineOutput, new List<string> { refe });

                counter++;

                if (bleuFirst > bleuSecond)
                {
                    translation.BetterBLEU = 1;
                    firstBetter++;
                }
                else if (bleuFirst < bleuSecond)
                {
                    translation.BetterBLEU = -1;
                    secondBetter++;
                }
                else
                {
                    translation.BetterBLEU = 0;
                    firstBetter += 0.5;
                    secondBetter += 0.5;
                }
            }


            rtbSignTestResults.Text = "Our <> Mosese\r\n";
            rtbSignTestResults.Text += firstBetter + " <> " + secondBetter+"\r\n";
            rtbSignTestResults.Text += "Different with confidence of "+SignTest.CalcConfidence((int)firstBetter, counter);
        }

        Dictionary<string, Dictionary<string, int>> memory = new Dictionary<string, Dictionary<string, int>>();

        public void ExtractReferences(string refSource, string refTarget)
        {
            StreamReader readerRefFileSource = new StreamReader(refSource);
            StreamReader readerRefFileTarget = new StreamReader(refTarget);
            int loaderCounter = 0;

            while (!readerRefFileSource.EndOfStream)
            {

                string lineSource = readerRefFileSource.ReadLine();
                string lineTarget = readerRefFileTarget.ReadLine();

                if (!memory.ContainsKey(lineSource))
                    memory.Add(lineSource, new Dictionary<string, int>());

                if (!memory[lineSource].ContainsKey(lineTarget))
                    memory[lineSource].Add(lineTarget, 0);

                memory[lineSource][lineTarget] += 1;
                loaderCounter++;
                if (loaderCounter % 1000 == 0)
                    Console.WriteLine(loaderCounter + "/900000");
            }
        }

        private void SaveReferences(string sourceTest, string targetTest, string refOutputPath)
        {

            string[] testSourceLines = File.ReadAllLines(sourceTest);
            string[] testTargetLines = File.ReadAllLines(targetTest);

            string[] outputRef1 = new string[testSourceLines.Length];
            string[] outputRef2 = new string[testSourceLines.Length];
            string[] outputRef3 = new string[testSourceLines.Length];

            for (int index = 0; index < testSourceLines.Length; index++)
            {
                string item = testSourceLines[index];
                string line = item.Trim(' ');

                outputRef1[index] = testTargetLines[index];

                if (memory.ContainsKey(line))
                {
                    var bestReferences = (from entry in memory[line] orderby entry.Value descending select entry.Key).Take(2).ToList();
                    bestReferences.Remove(testTargetLines[index]);
                    if (bestReferences.Count == 0)
                    {
                        outputRef2[index] = testTargetLines[index];
                        outputRef3[index] = testTargetLines[index];
                    }
                    else
                    {
                        outputRef2[index] = bestReferences[0];

                        if (bestReferences.Count() == 2)
                            outputRef3[index] = bestReferences[1];
                        else
                            outputRef3[index] = bestReferences[0];
                    }
                }
                else
                {
                    outputRef2[index] = testTargetLines[index];
                    outputRef3[index] = testTargetLines[index];
                }
            }
            File.WriteAllLines(refOutputPath + "ref0.txt", outputRef1);
            File.WriteAllLines(refOutputPath + "ref1.txt", outputRef2);
            File.WriteAllLines(refOutputPath + "ref2.txt", outputRef3);
        }
        private void SaveForBleu()
        {            
            if (m_OutputTranslations!=null && m_OutputTranslations.Count>0 && txtOutputToMoses.Text!="")
            {
                string translationPath = txtExperimentPath.Text + txtOutputToMoses.Text;
                StreamWriter swToBleu= new StreamWriter(translationPath);
                foreach (var item in m_OutputTranslations)
                {
                    swToBleu.WriteLine(item.Hypo);
                }
                swToBleu.Close();
            }
        }

        private void openDialog_Click(object sender, EventArgs e)
        {
            if (folderDialog.ShowDialog() == DialogResult.OK)
            {
                txtExperimentPath.Text = folderDialog.SelectedPath;
            }
        }
        private void btnLoadToMemory_Click(object sender, EventArgs e)
        {
            LoadToMemory();
        }
        private void btnLoadRedis_Click(object sender, EventArgs e)
        {
            LoadToRedis();
        }     
        private void btnCleanDB_Click(object sender, EventArgs e)
        {
            ResetRedis();
        }
        private void btnEnhance_Click(object sender, EventArgs e)
        {
            EnhanceMoses();
        }
        private void btnRunCombined_Click(object sender, EventArgs e)
        {
            GenerateTranslations();
        }

        private void btnSignTest_Click(object sender, EventArgs e)
        {
            RunSignTest();
        }

        private void btnSaveForBleu_Click(object sender, EventArgs e)
        {
            SaveForBleu();
        }

        private void btnSaveStats_Click(object sender, EventArgs e)
        {
            WekaOutput.Print(txtExperimentPath.Text + "wekaTests");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadTrainingOptionsToWeka();
        }

        private void LoadTrainingOptionsToWeka()
        {
            string testHePath = txtExperimentPath.Text + txtTestNames.Text + ".he";
            string testEnPath = txtExperimentPath.Text + txtTestNames.Text + ".en";
            //string mosesTranslationPath = txtExperimentPath.Text + txtMosesPath.Text;
            string mosesGrades = txtExperimentPath.Text + txtMosesGrades.Text;

            //         source            target
            Dictionary<string, Dictionary<string, WekaTrainingRecord>> m_records = new Dictionary<string, Dictionary<string, WekaTrainingRecord>>();

            if (!File.Exists(testHePath) || !File.Exists(testEnPath)/* || !File.Exists(mosesTranslationPath)*/ || !File.Exists(mosesGrades))
            {
                rtbMemoryStatus.Text += "Missing files";
                return;
            }

            string[] mosesGradesData = File.ReadAllLines(mosesGrades);
            string[] sourceData = File.ReadAllLines(testHePath);
            string[] referenceData = File.ReadAllLines(testHePath);

            if (mosesGradesData.Length != sourceData.Length)
            {
                rtbMemoryStatus.Text += "Files have different length";
                return;
            }
            //string[] referenceData = File.ReadAllLines(testEnPath);
            int index = 0;
            foreach (var sourceLine in sourceData)
            {
                double mosesGrade = double.Parse(mosesGradesData[index]);

                Regex rxSpace = new Regex(@"\s\s+");
                string source = rxSpace.Replace(sourceLine, " ");

                HashEntry[] directResults = DBManager.GetInstance().GetSet(source);

                if (directResults.Length == 0)
                    continue;

                foreach (HashEntry entry in directResults)
                {
                    if ((int)entry.Value <= 1)
                        continue;

                    string translation = entry.Name.ToString().Substring(1);
                    double translationBLEU = BLEU.Evaluate(translation, new List<string> { referenceData[index] });

                    bool betterBleu=false;
                    if (translationBLEU * 0.9 > mosesGrade)
                    {
                        betterBleu = true;
                    }


                    if (!m_records.ContainsKey(source))
                        m_records.Add(source, new Dictionary<string, WekaTrainingRecord>());
                    if (!m_records[source].ContainsKey(translation))
                        m_records[source].Add(translation, new WekaTrainingRecord(source, translation));

                    WekaTrainingRecord wr = m_records[source][translation];
                    
                    if (betterBleu)
                    {
                        wr.AddGood();
                    } else
                    {
                        wr.AddBad();
                    }
                }
                index++;
                if (index % 100 == 0) Console.WriteLine(index+"/"+ sourceData.Length);
            }
        // Finished Learning whats good.

    

        }


        private void btnTrimMoses_Click(object sender, EventArgs e)
        {
            string mosesTranslationPath = txtExperimentPath.Text + txtMosesPath.Text;

            if (!File.Exists(mosesTranslationPath))
                return;

            string[] MosesLines = File.ReadAllLines(mosesTranslationPath);
            for (int i = 0; i < MosesLines.Length; i++)
            {
                MosesLines[i] = MosesLines[i].Trim(' ');    
            }
            File.WriteAllLines(mosesTranslationPath,MosesLines);
        }

        private void button2_Click(object sender, EventArgs e)
        {

            string refEnPath = txtExperimentPath.Text + txtRefLocation.Text + ".en";
            string refHePath = txtExperimentPath.Text + txtRefLocation.Text + ".he";
            if (!File.Exists(refEnPath) || !File.Exists(refHePath))
            {
                rtbMemoryStatus.Text += "Missing files";
                return;
            }
            ExtractReferences(refHePath, refEnPath);
        }
        private void btnMakeRefs_Click(object sender, EventArgs e)
        {
            string testHePath = txtExperimentPath.Text + txtTestNames.Text + ".he";
            string testEnPath = txtExperimentPath.Text + txtTestNames.Text + ".en";
            string refNames = txtExperimentPath.Text + txtRefNAMES.Text;
            if (!File.Exists(testHePath) || !File.Exists(testEnPath))
            {
                rtbMemoryStatus.Text += "Missing files";
                return;
            }
            SaveReferences(testHePath, testEnPath, refNames);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Demo d = new Demo();
            d.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SaveReplacements();
        }

        private void SaveReplacements()
        {
            if (m_OutputTranslations != null && m_OutputTranslations.Count > 0 && txtOutputReplacements.Text != "")
            {
                string replacementsPath = txtExperimentPath.Text + txtOutputReplacements.Text;
                StreamWriter swToHtml = new StreamWriter(replacementsPath);
                swToHtml.WriteLine("<html><body><body><table><tr><td>Source</td><td>Base System</td><td>Examples</td><td>Selected</td><td>Human Translation</td></tr>");
                foreach (var item in m_OutputTranslations)
                {
                    if (item.SelectedTranslation==1) 
                        swToHtml.WriteLine("<tr><td>" + item.Source + "</td><td>" + item.OtherMachineOutput+ "</td><td><b>"+item.ExamplesOutput+ "</b></td><td>"+item.Hypo+ "</td><td>" + item.Reference + "</td></tr>");
                    else
                        swToHtml.WriteLine("<tr><td>" + item.Source + "</td><td><b>" + item.OtherMachineOutput + "</b></td><td>" + item.ExamplesOutput + "</td><td>" + item.Hypo + "</td><td>" + item.Reference + "</td></tr>");
                }
                swToHtml.WriteLine("</table></body></html>");
                swToHtml.Close();
            }
        }
    }
}
