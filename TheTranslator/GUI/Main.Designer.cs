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
            this.txtMosesGrades = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtRefPath = new System.Windows.Forms.TextBox();
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
            this.radioGoogle = new System.Windows.Forms.RadioButton();
            this.radioWeka8 = new System.Windows.Forms.RadioButton();
            this.btnTrimMoses = new System.Windows.Forms.Button();
            this.radioLongerSure = new System.Windows.Forms.RadioButton();
            this.radioWeka7 = new System.Windows.Forms.RadioButton();
            this.txtLimitTest = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtMosesPath = new System.Windows.Forms.TextBox();
            this.radioWeka6 = new System.Windows.Forms.RadioButton();
            this.radioWeka5 = new System.Windows.Forms.RadioButton();
            this.radioWeka4 = new System.Windows.Forms.RadioButton();
            this.radioWeka3 = new System.Windows.Forms.RadioButton();
            this.radioWeka2 = new System.Windows.Forms.RadioButton();
            this.radioWeka1 = new System.Windows.Forms.RadioButton();
            this.radioCombine = new System.Windows.Forms.RadioButton();
            this.radioSeenLength = new System.Windows.Forms.RadioButton();
            this.btnRunCombined = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtLimit = new System.Windows.Forms.TextBox();
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
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.button3 = new System.Windows.Forms.Button();
            this.txtRefLocation = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.txtRefNAMES = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.btnMakeRefs = new System.Windows.Forms.Button();
            this.folderDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.saveTranslation = new System.Windows.Forms.SaveFileDialog();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.button4 = new System.Windows.Forms.Button();
            this.txtOutputReplacements = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox8.SuspendLayout();
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
            this.tableLayoutPanel1.Controls.Add(this.groupBox8, 2, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 55.94714F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 44.05286F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1088, 574);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtMosesGrades);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.txtRefPath);
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
            this.groupBox1.Size = new System.Drawing.Size(356, 315);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Paths";
            // 
            // txtMosesGrades
            // 
            this.txtMosesGrades.Location = new System.Drawing.Point(13, 199);
            this.txtMosesGrades.Name = "txtMosesGrades";
            this.txtMosesGrades.Size = new System.Drawing.Size(100, 20);
            this.txtMosesGrades.TabIndex = 19;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(119, 197);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(193, 23);
            this.button1.TabIndex = 15;
            this.button1.Text = "LoadWekaTrainingTries";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(9, 151);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(63, 13);
            this.label12.TabIndex = 18;
            this.label12.Text = "Ref Names:";
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
            // txtRefPath
            // 
            this.txtRefPath.Location = new System.Drawing.Point(78, 148);
            this.txtRefPath.Name = "txtRefPath";
            this.txtRefPath.Size = new System.Drawing.Size(205, 20);
            this.txtRefPath.TabIndex = 0;
            this.txtRefPath.Text = "DownloadedFullRef.en-he.clean";
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
            this.txtTestNames.Text = "DownloadedFullTest.en-he.clean";
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
            this.txtTuningNames.Text = "DownloadedFullTune.en-he.clean";
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
            this.txtTrainingNames.Text = "DownloadedFullTrain.en-he.clean";
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
            this.groupBox2.Size = new System.Drawing.Size(356, 315);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Step 2: Generate Translations";
            // 
            // rtbTranslationProcedure
            // 
            this.rtbTranslationProcedure.Location = new System.Drawing.Point(4, 255);
            this.rtbTranslationProcedure.Name = "rtbTranslationProcedure";
            this.rtbTranslationProcedure.ReadOnly = true;
            this.rtbTranslationProcedure.Size = new System.Drawing.Size(346, 54);
            this.rtbTranslationProcedure.TabIndex = 2;
            this.rtbTranslationProcedure.Text = "";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.radioGoogle);
            this.groupBox6.Controls.Add(this.radioWeka8);
            this.groupBox6.Controls.Add(this.btnTrimMoses);
            this.groupBox6.Controls.Add(this.radioLongerSure);
            this.groupBox6.Controls.Add(this.radioWeka7);
            this.groupBox6.Controls.Add(this.txtLimitTest);
            this.groupBox6.Controls.Add(this.label9);
            this.groupBox6.Controls.Add(this.txtMosesPath);
            this.groupBox6.Controls.Add(this.radioWeka6);
            this.groupBox6.Controls.Add(this.radioWeka5);
            this.groupBox6.Controls.Add(this.radioWeka4);
            this.groupBox6.Controls.Add(this.radioWeka3);
            this.groupBox6.Controls.Add(this.radioWeka2);
            this.groupBox6.Controls.Add(this.radioWeka1);
            this.groupBox6.Controls.Add(this.radioCombine);
            this.groupBox6.Controls.Add(this.radioSeenLength);
            this.groupBox6.Controls.Add(this.btnRunCombined);
            this.groupBox6.Location = new System.Drawing.Point(6, 20);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(344, 229);
            this.groupBox6.TabIndex = 2;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Select Improvement Method";
            // 
            // radioGoogle
            // 
            this.radioGoogle.AutoSize = true;
            this.radioGoogle.Location = new System.Drawing.Point(6, 180);
            this.radioGoogle.Name = "radioGoogle";
            this.radioGoogle.Size = new System.Drawing.Size(90, 17);
            this.radioGoogle.TabIndex = 24;
            this.radioGoogle.TabStop = true;
            this.radioGoogle.Text = "google-Power";
            this.radioGoogle.UseVisualStyleBackColor = true;
            // 
            // radioWeka8
            // 
            this.radioWeka8.AutoSize = true;
            this.radioWeka8.Location = new System.Drawing.Point(197, 182);
            this.radioWeka8.Name = "radioWeka8";
            this.radioWeka8.Size = new System.Drawing.Size(83, 17);
            this.radioWeka8.TabIndex = 23;
            this.radioWeka8.TabStop = true;
            this.radioWeka8.Text = "radioWeka8";
            this.radioWeka8.UseVisualStyleBackColor = true;
            // 
            // btnTrimMoses
            // 
            this.btnTrimMoses.Location = new System.Drawing.Point(258, 56);
            this.btnTrimMoses.Name = "btnTrimMoses";
            this.btnTrimMoses.Size = new System.Drawing.Size(75, 23);
            this.btnTrimMoses.TabIndex = 22;
            this.btnTrimMoses.Text = "Trim moses";
            this.btnTrimMoses.UseVisualStyleBackColor = true;
            this.btnTrimMoses.Click += new System.EventHandler(this.btnTrimMoses_Click);
            // 
            // radioLongerSure
            // 
            this.radioLongerSure.AutoSize = true;
            this.radioLongerSure.Location = new System.Drawing.Point(197, 156);
            this.radioLongerSure.Name = "radioLongerSure";
            this.radioLongerSure.Size = new System.Drawing.Size(122, 17);
            this.radioLongerSure.TabIndex = 14;
            this.radioLongerSure.TabStop = true;
            this.radioLongerSure.Text = "OurTakeLongerSure";
            this.radioLongerSure.UseVisualStyleBackColor = true;
            // 
            // radioWeka7
            // 
            this.radioWeka7.AutoSize = true;
            this.radioWeka7.Location = new System.Drawing.Point(197, 131);
            this.radioWeka7.Name = "radioWeka7";
            this.radioWeka7.Size = new System.Drawing.Size(57, 17);
            this.radioWeka7.TabIndex = 13;
            this.radioWeka7.TabStop = true;
            this.radioWeka7.Text = "weka7";
            this.radioWeka7.UseVisualStyleBackColor = true;
            // 
            // txtLimitTest
            // 
            this.txtLimitTest.Location = new System.Drawing.Point(278, 206);
            this.txtLimitTest.Name = "txtLimitTest";
            this.txtLimitTest.Size = new System.Drawing.Size(55, 20);
            this.txtLimitTest.TabIndex = 12;
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
            // radioWeka6
            // 
            this.radioWeka6.AutoSize = true;
            this.radioWeka6.Location = new System.Drawing.Point(197, 108);
            this.radioWeka6.Name = "radioWeka6";
            this.radioWeka6.Size = new System.Drawing.Size(129, 17);
            this.radioWeka6.TabIndex = 9;
            this.radioWeka6.TabStop = true;
            this.radioWeka6.Text = "weka6+long combiner";
            this.radioWeka6.UseVisualStyleBackColor = true;
            // 
            // radioWeka5
            // 
            this.radioWeka5.AutoSize = true;
            this.radioWeka5.Location = new System.Drawing.Point(197, 85);
            this.radioWeka5.Name = "radioWeka5";
            this.radioWeka5.Size = new System.Drawing.Size(57, 17);
            this.radioWeka5.TabIndex = 8;
            this.radioWeka5.TabStop = true;
            this.radioWeka5.Text = "weka5";
            this.radioWeka5.UseVisualStyleBackColor = true;
            // 
            // radioWeka4
            // 
            this.radioWeka4.AutoSize = true;
            this.radioWeka4.Location = new System.Drawing.Point(197, 62);
            this.radioWeka4.Name = "radioWeka4";
            this.radioWeka4.Size = new System.Drawing.Size(57, 17);
            this.radioWeka4.TabIndex = 7;
            this.radioWeka4.TabStop = true;
            this.radioWeka4.Text = "weka4";
            this.radioWeka4.UseVisualStyleBackColor = true;
            // 
            // radioWeka3
            // 
            this.radioWeka3.AutoSize = true;
            this.radioWeka3.Location = new System.Drawing.Point(6, 156);
            this.radioWeka3.Name = "radioWeka3";
            this.radioWeka3.Size = new System.Drawing.Size(57, 17);
            this.radioWeka3.TabIndex = 6;
            this.radioWeka3.TabStop = true;
            this.radioWeka3.Text = "weka3";
            this.radioWeka3.UseVisualStyleBackColor = true;
            // 
            // radioWeka2
            // 
            this.radioWeka2.AutoSize = true;
            this.radioWeka2.Location = new System.Drawing.Point(7, 133);
            this.radioWeka2.Name = "radioWeka2";
            this.radioWeka2.Size = new System.Drawing.Size(57, 17);
            this.radioWeka2.TabIndex = 5;
            this.radioWeka2.TabStop = true;
            this.radioWeka2.Text = "weka2";
            this.radioWeka2.UseVisualStyleBackColor = true;
            // 
            // radioWeka1
            // 
            this.radioWeka1.AutoSize = true;
            this.radioWeka1.Location = new System.Drawing.Point(7, 110);
            this.radioWeka1.Name = "radioWeka1";
            this.radioWeka1.Size = new System.Drawing.Size(57, 17);
            this.radioWeka1.TabIndex = 4;
            this.radioWeka1.TabStop = true;
            this.radioWeka1.Text = "weka1";
            this.radioWeka1.UseVisualStyleBackColor = true;
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
            this.btnRunCombined.Location = new System.Drawing.Point(6, 206);
            this.btnRunCombined.Name = "btnRunCombined";
            this.btnRunCombined.Size = new System.Drawing.Size(266, 23);
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
            this.groupBox3.Location = new System.Drawing.Point(3, 324);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(356, 247);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Step 1: Load Data";
            // 
            // txtLimit
            // 
            this.txtLimit.Location = new System.Drawing.Point(239, 29);
            this.txtLimit.Name = "txtLimit";
            this.txtLimit.Size = new System.Drawing.Size(100, 20);
            this.txtLimit.TabIndex = 11;
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
            this.groupBox4.Controls.Add(this.txtOutputReplacements);
            this.groupBox4.Controls.Add(this.button4);
            this.groupBox4.Controls.Add(this.txtOutputToMoses);
            this.groupBox4.Controls.Add(this.groupBox5);
            this.groupBox4.Controls.Add(this.btnSaveForBleu);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox4.Location = new System.Drawing.Point(365, 324);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(356, 247);
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
            this.groupBox5.Location = new System.Drawing.Point(6, 127);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(344, 82);
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
            this.btnSaveStats.Location = new System.Drawing.Point(7, 52);
            this.btnSaveStats.Name = "btnSaveStats";
            this.btnSaveStats.Size = new System.Drawing.Size(123, 23);
            this.btnSaveStats.TabIndex = 0;
            this.btnSaveStats.Text = "Save with Stats";
            this.btnSaveStats.UseVisualStyleBackColor = true;
            this.btnSaveStats.Click += new System.EventHandler(this.btnSaveStats_Click);
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
            this.groupBox7.Size = new System.Drawing.Size(358, 315);
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
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.button3);
            this.groupBox8.Controls.Add(this.txtRefLocation);
            this.groupBox8.Controls.Add(this.button2);
            this.groupBox8.Controls.Add(this.label13);
            this.groupBox8.Controls.Add(this.txtRefNAMES);
            this.groupBox8.Controls.Add(this.label11);
            this.groupBox8.Controls.Add(this.btnMakeRefs);
            this.groupBox8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox8.Location = new System.Drawing.Point(727, 324);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(358, 247);
            this.groupBox8.TabIndex = 5;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "groupBox8";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(130, 208);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(219, 30);
            this.button3.TabIndex = 2;
            this.button3.Text = "Open Demo";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // txtRefLocation
            // 
            this.txtRefLocation.Location = new System.Drawing.Point(9, 42);
            this.txtRefLocation.Name = "txtRefLocation";
            this.txtRefLocation.Size = new System.Drawing.Size(197, 20);
            this.txtRefLocation.TabIndex = 23;
            this.txtRefLocation.Text = "DownloadedFullRef.en-he.clean";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(257, 40);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(92, 23);
            this.button2.TabIndex = 22;
            this.button2.Text = "Load Refs";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(6, 83);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(94, 13);
            this.label13.TabIndex = 21;
            this.label13.Text = "Ref output names:";
            // 
            // txtRefNAMES
            // 
            this.txtRefNAMES.Location = new System.Drawing.Point(106, 80);
            this.txtRefNAMES.Name = "txtRefNAMES";
            this.txtRefNAMES.Size = new System.Drawing.Size(100, 20);
            this.txtRefNAMES.TabIndex = 20;
            this.txtRefNAMES.Text = "First";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(209, 45);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(39, 13);
            this.label11.TabIndex = 18;
            this.label11.Text = ".en/he";
            // 
            // btnMakeRefs
            // 
            this.btnMakeRefs.Location = new System.Drawing.Point(212, 78);
            this.btnMakeRefs.Name = "btnMakeRefs";
            this.btnMakeRefs.Size = new System.Drawing.Size(92, 23);
            this.btnMakeRefs.TabIndex = 19;
            this.btnMakeRefs.Text = "Save Refs";
            this.btnMakeRefs.UseVisualStyleBackColor = true;
            this.btnMakeRefs.Click += new System.EventHandler(this.btnMakeRefs_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(203, 57);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(130, 23);
            this.button4.TabIndex = 3;
            this.button4.Text = "Save replacements";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // txtOutputReplacements
            // 
            this.txtOutputReplacements.Location = new System.Drawing.Point(12, 59);
            this.txtOutputReplacements.Name = "txtOutputReplacements";
            this.txtOutputReplacements.Size = new System.Drawing.Size(184, 20);
            this.txtOutputReplacements.TabIndex = 4;
            this.txtOutputReplacements.Text = "Replacements.html";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1088, 574);
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
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
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
        private System.Windows.Forms.RadioButton radioWeka6;
        private System.Windows.Forms.RadioButton radioWeka5;
        private System.Windows.Forms.RadioButton radioWeka4;
        private System.Windows.Forms.RadioButton radioWeka3;
        private System.Windows.Forms.RadioButton radioWeka2;
        private System.Windows.Forms.RadioButton radioWeka1;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.RichTextBox rtbSignTestResults;
        private System.Windows.Forms.Button btnSignTest;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtMosesPath;
        private System.Windows.Forms.TextBox txtOutputToMoses;
        private System.Windows.Forms.TextBox txtLimit;
        private System.Windows.Forms.TextBox txtLimitTest;
        private System.Windows.Forms.RadioButton radioWeka7;
        private System.Windows.Forms.RadioButton radioLongerSure;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.Button btnMakeRefs;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtRefPath;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtRefNAMES;
        private System.Windows.Forms.Button btnTrimMoses;
        private System.Windows.Forms.RadioButton radioWeka8;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox txtRefLocation;
        private System.Windows.Forms.TextBox txtMosesGrades;
        private System.Windows.Forms.RadioButton radioGoogle;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox txtOutputReplacements;
        private System.Windows.Forms.Button button4;
    }
}