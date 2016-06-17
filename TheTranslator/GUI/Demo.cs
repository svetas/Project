using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;
using TheTranslator.ImproveMethods;

namespace TheTranslator.GUI
{
    public partial class Demo : Form
    {
        public Demo()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ImprovementMethod m_subtitutionLogic = new WekaKnowladgeGoogle();

            if (txtSource.Text == "")
                return;
            string yandexLine = "https://translate.yandex.net/api/v1.5/tr.json/translate?key=trnsl.1.1.20160119T221704Z.84fe7a44b8b13085.e8a5f9bac79f034936320506a54b7606b5a22f51&lang=he-en&text=";
            string googleLine = "https://translate.googleapis.com/translate_a/single?client=gtx&sl=he&tl=en&dt=t&q=";
            txtYandex.Text = GetYandexTranslation(yandexLine + txtSource.Text);
            txtGoogle.Text = GoogleTranslate(txtSource.Text);

            if (Main.m_extractor!= null)
            {
                string[] translations = Main.m_extractor.ExtractExactTranslation(txtSource.Text, 0, 10);
                if (translations.Length <= 0) return;
                txtMovies.Text = "";
                foreach (var item in translations)
                {
                    txtMovies.Text += item.Trim(' ').Replace(" &apos;", "'")+"\r\n";
                }
                string otherTrans="";
                
                if (radioButton1.Checked)
                {
                    otherTrans = txtGoogle.Text;
                }
                else {
                    otherTrans = txtYandex.Text;
                }
                int selectedMachine;
                m_subtitutionLogic.ChooseBetter(translations[0], otherTrans, txtSource.Text, out selectedMachine);
                if (selectedMachine == 1)
                {
                    txtOutput.Text = translations[0];
                } else
                {
                    txtOutput.Text = otherTrans;
                }
            }
        }

        private string GetGoogleTranslation(string text)
        {
            WebClient wc = new WebClient();
            wc.Encoding = Encoding.UTF8;
            Uri path = new Uri(text);
            byte[] googleStr = wc.DownloadData(path);

            using (MemoryStream stream = new MemoryStream(wc.DownloadData(path)))
            {

            }
                Stream strm = new MemoryStream(googleStr);
            StreamReader sr = new StreamReader(strm);
            string google = sr.ReadToEnd();
            int indexBegin = google.IndexOf(@""",""");
            return google.Substring(3, indexBegin - 3);
        
        }

        private string GoogleTranslate(string sourceText)
        {
            string output = "";
            try
            {
                // Download translation
                string url = string.Format("https://translate.googleapis.com/translate_a/single?client=gtx&sl={0}&tl={1}&dt=t&q={2}",
                                            "he",
                                            "en",
                                            HttpUtility.UrlEncode(sourceText));
                string outputFile = Path.GetTempFileName();
                using (WebClient wc = new WebClient())
                {
                    wc.Headers.Add("user-agent", "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.36 " +
                                                  "(KHTML, like Gecko) Chrome/41.0.2228.0 Safari/537.36");
                    wc.DownloadFile(url, outputFile);
                }

                // Get translated text
                if (File.Exists(outputFile))
                {

                    // Get phrase collection
                    string text = File.ReadAllText(outputFile);
                    output = text.Substring(4, text.IndexOf(@""",""")-4);
                }
            }
            catch (Exception ex)
            {
                
            }

            return output;
        }

        private string GetYandexTranslation(string text)
        {
            WebClient wc = new WebClient();
            wc.Encoding = Encoding.UTF8;
            string strJson = wc.DownloadString(text);
            System.Web.Script.Serialization.JavaScriptSerializer js = new System.Web.Script.Serialization.JavaScriptSerializer();
            RootObject ro = js.Deserialize<RootObject>(strJson);
            return ro.text[0];
        }

        public class RootObject
        {
            public int code { get; set; }
            public string lang { get; set; }
            public List<string> text { get; set; }
        }
    }
}
