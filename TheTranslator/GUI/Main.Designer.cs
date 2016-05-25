namespace TheTranslator.GUI
{
    partial class Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtAuxDic = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtTestNames = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtTuningNames = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtTrainingNames = new System.Windows.Forms.TextBox();
            this.btnOpenDialog = new System.Windows.Forms.Button();
            this.txtExperimentPath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rtbTranslationProcedure = new System.Windows.Forms.RichTextBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtMosesPath = new System.Windows.Forms.TextBox();
            this.radioButton7 = new System.Windows.Forms.RadioButton();
            this.radioButton6 = new System.Windows.Forms.RadioButton();
            this.radioButton5 = new System.Windows.Forms.RadioButton();
            this.radioButton4 = new System.Windows.Forms.RadioButton();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioCombine = new System.Windows.Forms.RadioButton();
            this.radioSeenLength = new System.Windows.Forms.RadioButton();
            this.btnRunCombined = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.rtbMemoryStatus = new System.Windows.Forms.RichTextBox();
            this.btnCleanDB = new System.Windows.Forms.Button();
            this.btnLoadToMemory = new System.Windows.Forms.Button();
            this.btnEnhance = new System.Windows.Forms.Button();
            this.btnLoadRedis = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.txtOutputToMoses = new System.Windows.Forms.TextBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.chkSign = new System.Windows.Forms.CheckBox();
            this.btnSaveStats = new System.Windows.Forms.Button();
            this.btnSaveForBleu = new System.Windows.Forms.Button();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.rtbSignTestResults = new System.Windows.Forms.RichTextBox();
            this.btnSignTest = new System.Windows.Forms.Button();
            this.folderDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.saveTranslation = new System.Windows.Forms.SaveFileDialog();
            this.txtLimit = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33332F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox2, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox3, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.groupBox4, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.groupBox7, 2, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 55.94714F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 44.05286F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1088, 507);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.txtAuxDic);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtTestNames);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtTuningNames);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtTrainingNames);
            this.groupBox1.Controls.Add(this.btnOpenDialog);
            this.groupBox1.Controls.Add(this.txtExperimentPath);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(356, 277);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Paths";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(10, 124);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(75, 13);
            this.label8.TabIndex = 15;
            this.label8.Text = "Aux Dictionary";
            // 
            // txtAuxDic
            // 
            this.txtAuxDic.Location = new System.Drawing.Point(100, 121);
            this.txtAuxDic.Name = "txtAuxDic";
            this.txtAuxDic.Size = new System.Drawing.Size(210, 20);
            this.txtAuxDic.TabIndex = 14;
            this.txtAuxDic.Text = "GoogleTranslateWords.txt";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(313, 98);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(39, 13);
            this.label7.TabIndex = 13;
            this.label7.Text = ".en/he";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(312, 72);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(39, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = ".en/he";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(312, 46);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(39, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = ".en/he";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 98);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Test Names:";
            // 
            // txtTestNames
            // 
            this.txtTestNames.Location = new System.Drawing.Point(100, 95);
            this.txtTestNames.Name = "txtTestNames";
            this.txtTestNames.Size = new System.Drawing.Size(210, 20);
            this.txtTestNames.TabIndex = 8;
            this.txtTestNames.Text = "DownloadedFullTest.en-he.true";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 72);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Tuning Names:";
            // 
            // txtTuningNames
            // 
            this.txtTuningNames.Location = new System.Drawing.Point(100, 69);
            this.txtTuningNames.Name = "txtTuningNames";
            this.txtTuningNames.Size = new System.Drawing.Size(210, 20);
            this.txtTuningNames.TabIndex = 6;
            this.txtTuningNames.Text = "DownloadedFullTune.en-he.true";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Training Names:";
            // 
            // txtTrainingNames
            // 
            this.txtTrainingNames.Location = new System.Drawing.Point(100, 43);
            this.txtTrainingNames.Name = "txtTrainingNames";
            this.txtTrainingNames.Size = new System.Drawing.Size(210, 20);
            this.txtTrainingNames.TabIndex = 4;
            this.txtTrainingNames.Text = "DownloadedFullTrain.en-he.true";
            // 
            // btnOpenDialog
            // 
            this.btnOpenDialog.Location = new System.Drawing.Point(182, 15);
            this.btnOpenDialog.Name = "btnOpenDialog";
            this.btnOpenDialog.Size = new System.Drawing.Size(168, 23);
            this.btnOpenDialog.TabIndex = 2;
            this.btnOpenDialog.Text = "browse";
            this.btnOpenDialog.UseVisualStyleBackColor = true;
            this.btnOpenDialog.Click += new System.EventHandler(this.openDialog_Click);
            // 
            // txtExperimentPath
            // 
            this.txtExperimentPath.Location = new System.Drawing.Point(85, 17);
            this.txtExperimentPath.Name = "txtExperimentPath";
            this.txtExperimentPath.Size = new System.Drawing.Size(91, 20);
            this.txtExperimentPath.TabIndex = 1;
            this.txtExperimentPath.Text = "Z:\\";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Data\'s Folder";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rtbTranslationProcedure);
            this.groupBox2.Controls.Add(this.groupBox6);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(365, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(356, 277);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Step 2: Generate Translations";
            // 
            // rtbTranslationProcedure
            // 
            this.rtbTranslationProcedure.Location = new System.Drawing.Point(4, 211);
            this.rtbTranslationProcedure.Name = "rtbTranslationProcedure";
            this.rtbTranslationProcedure.ReadOnly = true;
            this.rtbTranslationProcedure.Size = new System.Drawing.Size(346, 54);
            this.rtbTranslationProcedure.TabIndex = 2;
            this.rtbTranslationProcedure.Text = "";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.label9);
            this.groupBox6.Controls.Add(this.txtMosesPath);
            this.groupBox6.Controls.Add(this.radioButton7);
            this.groupBox6.Controls.Add(this.radioButton6);
            this.groupBox6.Controls.Add(this.radioButton5);
            this.groupBox6.Controls.Add(this.radioButton4);
            this.groupBox6.Controls.Add(this.radioButton3);
            this.groupBox6.Controls.Add(this.radioButton1);
            this.groupBox6.Controls.Add(this.radioCombine);
            this.groupBox6.Controls.Add(this.radioSeenLength);
            this.groupBox6.Controls.Add(this.btnRunCombined);
            this.groupBox6.Location = new System.Drawing.Point(6, 20);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(344, 185);
            this.groupBox6.TabIndex = 2;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Select Improvement Method";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(10, 32);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(57, 13);
            this.label9.TabIndex = 11;
            this.label9.Text = "Moses file:";
            // 
            // txtMosesPath
            // 
            this.txtMosesPath.Location = new System.Drawing.Point(82, 29);
            this.txtMosesPath.Name = "txtMosesPath";
            this.txtMosesPath.Size = new System.Drawing.Size(244, 20);
            this.txtMosesPath.TabIndex = 10;
            this.txtMosesPath.Text = "MosesTranslated.en";
            // 
            // radioButton7
            // 
            this.radioButton7.AutoSize = true;
            this.radioButton7.Location = new System.Drawing.Point(197, 133);
            this.radioButton7.Name = "radioButton7";
            this.radioButton7.Size = new System.Drawing.Size(85, 17);
            this.radioButton7.TabIndex = 9;
            this.radioButton7.TabStop = true;
            this.radioButton7.Text = "radioButton7";
            this.radioButton7.UseVisualStyleBackColor = true;
            // 
            // radioButton6
            // 
            this.radioButton6.AutoSize = true;
            this.radioButton6.Location = new System.Drawing.Point(197, 110);
            this.radioButton6.Name = "radioButton6";
            this.radioButton6.Size = new System.Drawing.Size(85, 17);
            this.radioButton6.TabIndex = 8;
            this.radioButton6.TabStop = true;
            this.radioButton6.Text = "radioButton6";
            this.radioButton6.UseVisualStyleBackColor = true;
            // 
            // radioButton5
            // 
            this.radioButton5.AutoSize = true;
            this.radioButton5.Location = new System.Drawing.Point(197, 87);
            this.radioButton5.Name = "radioButton5";
            this.radioButton5.Size = new System.Drawing.Size(85, 17);
            this.radioButton5.TabIndex = 7;
            this.radioButton5.TabStop = true;
            this.radioButton5.Text = "radioButton5";
            this.radioButton5.UseVisualStyleBackColor = true;
            // 
            // radioButton4
            // 
            this.radioButton4.AutoSize = true;
            this.radioButton4.Location = new System.Drawing.Point(197, 64);
            this.radioButton4.Name = "radioButton4";
            this.radioButton4.Size = new System.Drawing.Size(85, 17);
            this.radioButton4.TabIndex = 6;
            this.radioButton4.TabStop = true;
            this.radioButton4.Text = "radioButton4";
            this.radioButton4.UseVisualStyleBackColor = true;
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Location = new System.Drawing.Point(7, 133);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(85, 17);
            this.radioButton3.TabIndex = 5;
            this.radioButton3.TabStop = true;
            this.radioButton3.Text = "radioButton3";
            this.radioButton3.UseVisualStyleBackColor = true;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(7, 110);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(85, 17);
            this.radioButton1.TabIndex = 4;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "radioButton1";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // radioCombine
            // 
            this.radioCombine.AutoSize = true;
            this.radioCombine.Location = new System.Drawing.Point(7, 87);
            this.radioCombine.Name = "radioCombine";
            this.radioCombine.Size = new System.Drawing.Size(145, 17);
            this.radioCombine.TabIndex = 3;
            this.radioCombine.TabStop = true;
            this.radioCombine.Text = "Seen>0 && Combine Large";
            this.radioCombine.UseVisualStyleBackColor = true;
            // 
            // radioSeenLength
            // 
            this.radioSeenLength.AutoSize = true;
            this.radioSeenLength.Location = new System.Drawing.Point(7, 64);
            this.radioSeenLength.Name = "radioSeenLength";
            this.radioSeenLength.Size = new System.Drawing.Size(119, 17);
            this.radioSeenLength.TabIndex = 2;
            this.radioSeenLength.TabStop = true;
            this.radioSeenLength.Text = "Seen>1 && Length>3";
            this.radioSeenLength.UseVisualStyleBackColor = true;
            // 
            // btnRunCombined
            // 
            this.btnRunCombined.Location = new System.Drawing.Point(0, 156);
            this.btnRunCombined.Name = "btnRunCombined";
            this.btnRunCombined.Size = new System.Drawing.Size(325, 23);
            this.btnRunCombined.TabIndex = 1;
            this.btnRunCombined.Text = "Translate";
            this.btnRunCombined.UseVisualStyleBackColor = true;
            this.btnRunCombined.Click += new System.EventHandler(this.btnRunCombined_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txtLimit);
            this.groupBox3.Controls.Add(this.rtbMemoryStatus);
            this.groupBox3.Controls.Add(this.btnCleanDB);
            this.groupBox3.Controls.Add(this.btnLoadToMemory);
            this.groupBox3.Controls.Add(this.btnEnhance);
            this.groupBox3.Controls.Add(this.btnLoadRedis);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(3, 286);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(356, 218);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Step 1: Load Data";
            // 
            // rtbMemoryStatus
            // 
            this.rtbMemoryStatus.Location = new System.Drawing.Point(6, 146);
            this.rtbMemoryStatus.Name = "rtbMemoryStatus";
            this.rtbMemoryStatus.ReadOnly = true;
            this.rtbMemoryStatus.Size = new System.Drawing.Size(341, 63);
            this.rtbMemoryStatus.TabIndex = 10;
            this.rtbMemoryStatus.Text = "Not Loaded..";
            // 
            // btnCleanDB
            // 
            this.btnCleanDB.Location = new System.Drawing.Point(6, 115);
            this.btnCleanDB.Name = "btnCleanDB";
            this.btnCleanDB.Size = new System.Drawing.Size(341, 23);
            this.btnCleanDB.TabIndex = 0;
            this.btnCleanDB.Text = "Clean Redis DB";
            this.btnCleanDB.UseVisualStyleBackColor = true;
            this.btnCleanDB.Click += new System.EventHandler(this.btnCleanDB_Click);
            // 
            // btnLoadToMemory
            // 
            this.btnLoadToMemory.Location = new System.Drawing.Point(6, 28);
            this.btnLoadToMemory.Name = "btnLoadToMemory";
            this.btnLoadToMemory.Size = new System.Drawing.Size(226, 23);
            this.btnLoadToMemory.TabIndex = 3;
            this.btnLoadToMemory.Text = "Load Training Set to Memory";
            this.btnLoadToMemory.UseVisualStyleBackColor = true;
            this.btnLoadToMemory.Click += new System.EventHandler(this.btnLoadToMemory_Click);
            // 
            // btnEnhance
            // 
            this.btnEnhance.Location = new System.Drawing.Point(6, 86);
            this.btnEnhance.Name = "btnEnhance";
            this.btnEnhance.Size = new System.Drawing.Size(341, 23);
            this.btnEnhance.TabIndex = 1;
            this.btnEnhance.Text = "Enhance Redis DB";
            this.btnEnhance.UseVisualStyleBackColor = true;
            this.btnEnhance.Click += new System.EventHandler(this.btnEnhance_Click);
            // 
            // btnLoadRedis
            // 
            this.btnLoadRedis.Location = new System.Drawing.Point(6, 57);
            this.btnLoadRedis.Name = "btnLoadRedis";
            this.btnLoadRedis.Size = new System.Drawing.Size(341, 23);
            this.btnLoadRedis.TabIndex = 2;
            this.btnLoadRedis.Text = "Load Training Set to Memory && Redis";
            this.btnLoadRedis.UseVisualStyleBackColor = true;
            this.btnLoadRedis.Click += new System.EventHandler(this.btnLoadRedis_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.txtOutputToMoses);
            this.groupBox4.Controls.Add(this.groupBox5);
            this.groupBox4.Controls.Add(this.btnSaveForBleu);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox4.Location = new System.Drawing.Point(365, 286);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(356, 218);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Step 3: Save translations";
            // 
            // txtOutputToMoses
            // 
            this.txtOutputToMoses.Location = new System.Drawing.Point(13, 30);
            this.txtOutputToMoses.Name = "txtOutputToMoses";
            this.txtOutputToMoses.Size = new System.Drawing.Size(184, 20);
            this.txtOutputToMoses.TabIndex = 2;
            this.txtOutputToMoses.Text = "MosesAfterImprovedByOur.en";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.chkSign);
            this.groupBox5.Controls.Add(this.btnSaveStats);
            this.groupBox5.Location = new System.Drawing.Point(6, 57);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(344, 152);
            this.groupBox5.TabIndex = 1;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Save with stats";
            // 
            // chkSign
            // 
            this.chkSign.AutoSize = true;
            this.chkSign.Location = new System.Drawing.Point(7, 29);
            this.chkSign.Name = "chkSign";
            this.chkSign.Size = new System.Drawing.Size(84, 17);
            this.chkSign.TabIndex = 1;
            this.chkSign.Text = "Winner Sign";
            this.chkSign.UseVisualStyleBackColor = true;
            // 
            // btnSaveStats
            // 
            this.btnSaveStats.Location = new System.Drawing.Point(7, 123);
            this.btnSaveStats.Name = "btnSaveStats";
            this.btnSaveStats.Size = new System.Drawing.Size(123, 23);
            this.btnSaveStats.TabIndex = 0;
            this.btnSaveStats.Text = "Save with Stats";
            this.btnSaveStats.UseVisualStyleBackColor = true;
            // 
            // btnSaveForBleu
            // 
            this.btnSaveForBleu.Location = new System.Drawing.Point(203, 28);
            this.btnSaveForBleu.Name = "btnSaveForBleu";
            this.btnSaveForBleu.Size = new System.Drawing.Size(130, 23);
            this.btnSaveForBleu.TabIndex = 0;
            this.btnSaveForBleu.Text = "Save for BLEU";
            this.btnSaveForBleu.UseVisualStyleBackColor = true;
            this.btnSaveForBleu.Click += new System.EventHandler(this.btnSaveForBleu_Click);
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.rtbSignTestResults);
            this.groupBox7.Controls.Add(this.btnSignTest);
            this.groupBox7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox7.Location = new System.Drawing.Point(727, 3);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(358, 277);
            this.groupBox7.TabIndex = 4;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Evaluate";
            // 
            // rtbSignTestResults
            // 
            this.rtbSignTestResults.Location = new System.Drawing.Point(6, 46);
            this.rtbSignTestResults.Name = "rtbSignTestResults";
            this.rtbSignTestResults.ReadOnly = true;
            this.rtbSignTestResults.Size = new System.Drawing.Size(346, 96);
            this.rtbSignTestResults.TabIndex = 1;
            this.rtbSignTestResults.Text = "";
            // 
            // btnSignTest
            // 
            this.btnSignTest.Location = new System.Drawing.Point(6, 20);
            this.btnSignTest.Name = "btnSignTest";
            this.btnSignTest.Size = new System.Drawing.Size(105, 23);
            this.btnSignTest.TabIndex = 0;
            this.btnSignTest.Text = "Sign Test";
            this.btnSignTest.UseVisualStyleBackColor = true;
            this.btnSignTest.Click += new System.EventHandler(this.btnSignTest_Click);
            // 
            // txtLimit
            // 
            this.txtLimit.Location = new System.Drawing.Point(239, 29);
            this.txtLimit.Name = "txtLimit";
            this.txtLimit.Size = new System.Drawing.Size(100, 20);
            this.txtLimit.TabIndex = 11;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1088, 507);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "Main";
            this.Text = "Main";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnOpenDialog;
        private System.Windows.Forms.TextBox txtExperimentPath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.FolderBrowserDialog folderDialog;
        private System.Windows.Forms.RichTextBox rtbMemoryStatus;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtTestNames;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtTuningNames;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtTrainingNames;
        private System.Windows.Forms.Button btnLoadToMemory;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnLoadRedis;
        private System.Windows.Forms.Button btnEnhance;
        private System.Windows.Forms.Button btnCleanDB;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtAuxDic;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnRunCombined;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.CheckBox chkSign;
        private System.Windows.Forms.Button btnSaveStats;
        private System.Windows.Forms.Button btnSaveForBleu;
        private System.Windows.Forms.SaveFileDialog saveTranslation;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.RadioButton radioCombine;
        private System.Windows.Forms.RadioButton radioSeenLength;
        private System.Windows.Forms.RichTextBox rtbTranslationProcedure;
        private System.Windows.Forms.RadioButton radioButton7;
        private System.Windows.Forms.RadioButton radioButton6;
        private System.Windows.Forms.RadioButton radioButton5;
        private System.Windows.Forms.RadioButton radioButton4;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.RichTextBox rtbSignTestResults;
        private System.Windows.Forms.Button btnSignTest;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtMosesPath;
        private System.Windows.Forms.TextBox txtOutputToMoses;
        private System.Windows.Forms.TextBox txtLimit;
    }
}