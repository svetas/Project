using BLEUevaluator;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using TheTranslator.Common;
using TheTranslator.Extractors;
using TheTranslator.ImproveMethods;

namespace TheTranslator.GUI
{
    public partial class Main : Form
    {
        Extractor m_extractor;
        ImprovementMethod m_subtitutionLogic;
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

                m_extractor = new ExtractorDirect(txtExperimentPath.Text);

                bool status = m_extractor.ScanWords(trainHePath, trainEnPath, auxDictionaryPath);

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

                m_extractor = new ExtractorDirect(txtExperimentPath.Text);

                bool status = m_extractor.build(trainHePath, trainEnPath, auxDictionaryPath);

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
            try {
                if (radioSeenLength.Checked)
                {
                    m_subtitutionLogic = new SureAndLong();
                }
                else
                {

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
                    total++;

                    lineOur = m_extractor.ExtractExactTranslation(lineSource, 0);

                    int selectedMachine;

                    string chosen = m_subtitutionLogic.ChooseBetter(lineOur, lineMoses,out selectedMachine);

                    if (selectedMachine==1)
                        counter++;

                    m_OutputTranslations.Add(new PostTranData(lineSource, lineReference, chosen, lineMoses, selectedMachine));
                }

                srEn.Close();
                srHe.Close();
                srM.Close();

                rtbTranslationProcedure.Text = "\r\n Finished loading.";
                rtbTranslationProcedure.Text += "\r\n We improved " + counter + "/" + total + ", thats " + counter / (double)total + "!";
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


    }
}
