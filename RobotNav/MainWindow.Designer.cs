namespace RobotNav
{
    partial class MainWindow
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnSearch = new System.Windows.Forms.Button();
            this.tbx_Output = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.rad_BFS = new System.Windows.Forms.RadioButton();
            this.rad_AStar = new System.Windows.Forms.RadioButton();
            this.btn_Reset = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rad_Goal2 = new System.Windows.Forms.RadioButton();
            this.rad_Goal1 = new System.Windows.Forms.RadioButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.rad_IDFS = new System.Windows.Forms.RadioButton();
            this.rad_Djikstra = new System.Windows.Forms.RadioButton();
            this.rad_GBFS = new System.Windows.Forms.RadioButton();
            this.rad_DFS = new System.Windows.Forms.RadioButton();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.Silver;
            this.btnSearch.FlatAppearance.BorderSize = 0;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnSearch.Location = new System.Drawing.Point(610, 310);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(137, 61);
            this.btnSearch.TabIndex = 0;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // tbx_Output
            // 
            this.tbx_Output.BackColor = System.Drawing.Color.DarkGray;
            this.tbx_Output.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbx_Output.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.tbx_Output.Location = new System.Drawing.Point(12, 250);
            this.tbx_Output.Multiline = true;
            this.tbx_Output.Name = "tbx_Output";
            this.tbx_Output.ReadOnly = true;
            this.tbx_Output.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbx_Output.Size = new System.Drawing.Size(534, 188);
            this.tbx_Output.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(228, 212);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 25);
            this.label1.TabIndex = 2;
            this.label1.Text = "Output";
            // 
            // rad_BFS
            // 
            this.rad_BFS.AutoSize = true;
            this.rad_BFS.Checked = true;
            this.rad_BFS.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rad_BFS.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.rad_BFS.Location = new System.Drawing.Point(14, 18);
            this.rad_BFS.Name = "rad_BFS";
            this.rad_BFS.Size = new System.Drawing.Size(182, 25);
            this.rad_BFS.TabIndex = 3;
            this.rad_BFS.TabStop = true;
            this.rad_BFS.Text = "Breadth-First Search";
            this.rad_BFS.UseVisualStyleBackColor = true;
            // 
            // rad_AStar
            // 
            this.rad_AStar.AutoSize = true;
            this.rad_AStar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rad_AStar.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.rad_AStar.Location = new System.Drawing.Point(14, 96);
            this.rad_AStar.Name = "rad_AStar";
            this.rad_AStar.Size = new System.Drawing.Size(104, 25);
            this.rad_AStar.TabIndex = 4;
            this.rad_AStar.Text = "A* Search";
            this.rad_AStar.UseVisualStyleBackColor = true;
            // 
            // btn_Reset
            // 
            this.btn_Reset.BackColor = System.Drawing.Color.Silver;
            this.btn_Reset.FlatAppearance.BorderSize = 0;
            this.btn_Reset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Reset.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btn_Reset.Location = new System.Drawing.Point(610, 377);
            this.btn_Reset.Name = "btn_Reset";
            this.btn_Reset.Size = new System.Drawing.Size(137, 61);
            this.btn_Reset.TabIndex = 5;
            this.btn_Reset.Text = "Reset";
            this.btn_Reset.UseVisualStyleBackColor = false;
            this.btn_Reset.Click += new System.EventHandler(this.btn_Reset_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rad_Goal2);
            this.panel1.Controls.Add(this.rad_Goal1);
            this.panel1.Location = new System.Drawing.Point(552, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(236, 91);
            this.panel1.TabIndex = 9;
            // 
            // rad_Goal2
            // 
            this.rad_Goal2.AutoSize = true;
            this.rad_Goal2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rad_Goal2.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.rad_Goal2.Location = new System.Drawing.Point(17, 48);
            this.rad_Goal2.Name = "rad_Goal2";
            this.rad_Goal2.Size = new System.Drawing.Size(78, 25);
            this.rad_Goal2.TabIndex = 1;
            this.rad_Goal2.Text = "Goal 2";
            this.rad_Goal2.UseVisualStyleBackColor = true;
            // 
            // rad_Goal1
            // 
            this.rad_Goal1.AutoSize = true;
            this.rad_Goal1.Checked = true;
            this.rad_Goal1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rad_Goal1.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.rad_Goal1.Location = new System.Drawing.Point(17, 17);
            this.rad_Goal1.Name = "rad_Goal1";
            this.rad_Goal1.Size = new System.Drawing.Size(78, 25);
            this.rad_Goal1.TabIndex = 0;
            this.rad_Goal1.TabStop = true;
            this.rad_Goal1.Text = "Goal 1";
            this.rad_Goal1.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.rad_IDFS);
            this.panel2.Controls.Add(this.rad_Djikstra);
            this.panel2.Controls.Add(this.rad_GBFS);
            this.panel2.Controls.Add(this.rad_DFS);
            this.panel2.Controls.Add(this.rad_BFS);
            this.panel2.Controls.Add(this.rad_AStar);
            this.panel2.Location = new System.Drawing.Point(552, 109);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(236, 195);
            this.panel2.TabIndex = 10;
            // 
            // rad_IDFS
            // 
            this.rad_IDFS.AutoSize = true;
            this.rad_IDFS.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rad_IDFS.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.rad_IDFS.Location = new System.Drawing.Point(14, 150);
            this.rad_IDFS.Name = "rad_IDFS";
            this.rad_IDFS.Size = new System.Drawing.Size(127, 25);
            this.rad_IDFS.TabIndex = 8;
            this.rad_IDFS.TabStop = true;
            this.rad_IDFS.Text = "Iterative DFS";
            this.rad_IDFS.UseVisualStyleBackColor = true;
            // 
            // rad_Djikstra
            // 
            this.rad_Djikstra.AutoSize = true;
            this.rad_Djikstra.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rad_Djikstra.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.rad_Djikstra.Location = new System.Drawing.Point(14, 123);
            this.rad_Djikstra.Name = "rad_Djikstra";
            this.rad_Djikstra.Size = new System.Drawing.Size(172, 25);
            this.rad_Djikstra.TabIndex = 7;
            this.rad_Djikstra.TabStop = true;
            this.rad_Djikstra.Text = "Dijkstra\'s Algorithm";
            this.rad_Djikstra.UseVisualStyleBackColor = true;
            // 
            // rad_GBFS
            // 
            this.rad_GBFS.AutoSize = true;
            this.rad_GBFS.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rad_GBFS.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.rad_GBFS.Location = new System.Drawing.Point(14, 70);
            this.rad_GBFS.Name = "rad_GBFS";
            this.rad_GBFS.Size = new System.Drawing.Size(213, 25);
            this.rad_GBFS.TabIndex = 6;
            this.rad_GBFS.Text = "Greedy Best-First Search";
            this.rad_GBFS.UseVisualStyleBackColor = true;
            // 
            // rad_DFS
            // 
            this.rad_DFS.AutoSize = true;
            this.rad_DFS.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rad_DFS.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.rad_DFS.Location = new System.Drawing.Point(14, 44);
            this.rad_DFS.Name = "rad_DFS";
            this.rad_DFS.Size = new System.Drawing.Size(169, 25);
            this.rad_DFS.TabIndex = 5;
            this.rad_DFS.Text = "Depth-First Search";
            this.rad_DFS.UseVisualStyleBackColor = true;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DimGray;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btn_Reset);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbx_Output);
            this.Controls.Add(this.btnSearch);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "MainWindow";
            this.Text = "RoboNav";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.MainWindow_Paint);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button btnSearch;
        private TextBox tbx_Output;
        private Label label1;
        private RadioButton rad_BFS;
        private RadioButton rad_AStar;
        private Button btn_Reset;
        private Panel panel1;
        private RadioButton rad_Goal1;
        private RadioButton rad_Goal2;
        private Panel panel2;
        private RadioButton rad_DFS;
        private RadioButton rad_GBFS;
        private RadioButton rad_IDFS;
        private RadioButton rad_Djikstra;
    }
}