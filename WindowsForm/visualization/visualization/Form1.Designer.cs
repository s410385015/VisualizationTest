namespace visualization
{
    partial class Form1
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器
        /// 修改這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.LabelView = new MetroFramework.Controls.MetroListView();
            this.updateBtn = new MetroFramework.Controls.MetroButton();
            this.preTime = new MetroFramework.Controls.MetroDateTime();
            this.LabelList = new System.Windows.Forms.ListBox();
            this.curTime = new MetroFramework.Controls.MetroDateTime();
            this.dataNumLabel = new MetroFramework.Controls.MetroLabel();
            this.alphaBar = new MetroFramework.Controls.MetroScrollBar();
            this.mode = new MetroFramework.Controls.MetroLabel();
            this.updateScatterPlot = new MetroFramework.Controls.MetroButton();
            this.LabelView2 = new MetroFramework.Controls.MetroListView();
            this.RightClickMenu = new MetroFramework.Controls.MetroContextMenu(this.components);
            this.graphToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.correlationCoefficientToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.LabelViewMode = new MetroFramework.Controls.MetroContextMenu(this.components);
            this.normalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sortByCCToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.LabelViewCC = new MetroFramework.Controls.MetroListView();
            this.Loading = new System.Windows.Forms.PictureBox();
            this.scatterPlot = new visualization.ScatterPlot();
            this.graph_table = new visualization.Graph();
            this.RightClickMenu.SuspendLayout();
            this.LabelViewMode.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Loading)).BeginInit();
            this.SuspendLayout();
            // 
            // LabelView
            // 
            this.LabelView.AutoArrange = false;
            this.LabelView.CheckBoxes = true;
            this.LabelView.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.LabelView.ForeColor = System.Drawing.SystemColors.WindowText;
            this.LabelView.FullRowSelect = true;
            this.LabelView.Location = new System.Drawing.Point(12, 453);
            this.LabelView.Name = "LabelView";
            this.LabelView.OwnerDraw = true;
            this.LabelView.Size = new System.Drawing.Size(121, 225);
            this.LabelView.TabIndex = 5;
            this.LabelView.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.LabelView.UseCompatibleStateImageBehavior = false;
            this.LabelView.UseCustomBackColor = true;
            this.LabelView.UseCustomForeColor = true;
            this.LabelView.UseSelectable = true;
            this.LabelView.UseStyleColors = true;
            this.LabelView.View = System.Windows.Forms.View.List;
            this.LabelView.SelectedIndexChanged += new System.EventHandler(this.LabelView_SelectedIndexChanged);
            this.LabelView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.LabelView_MouseDown);
            // 
            // updateBtn
            // 
            this.updateBtn.Location = new System.Drawing.Point(1200, 721);
            this.updateBtn.Name = "updateBtn";
            this.updateBtn.Size = new System.Drawing.Size(75, 23);
            this.updateBtn.TabIndex = 6;
            this.updateBtn.Text = "Update";
            this.updateBtn.UseSelectable = true;
            this.updateBtn.Click += new System.EventHandler(this.metroButton1_Click);
            // 
            // preTime
            // 
            this.preTime.Location = new System.Drawing.Point(12, 701);
            this.preTime.MaxDate = new System.DateTime(2018, 12, 31, 0, 0, 0, 0);
            this.preTime.MinDate = new System.DateTime(2007, 1, 1, 0, 0, 0, 0);
            this.preTime.MinimumSize = new System.Drawing.Size(4, 30);
            this.preTime.Name = "preTime";
            this.preTime.Size = new System.Drawing.Size(180, 30);
            this.preTime.TabIndex = 8;
            this.preTime.Value = new System.DateTime(2007, 1, 2, 0, 0, 0, 0);
            // 
            // LabelList
            // 
            this.LabelList.FormattingEnabled = true;
            this.LabelList.ItemHeight = 15;
            this.LabelList.Location = new System.Drawing.Point(813, 712);
            this.LabelList.MultiColumn = true;
            this.LabelList.Name = "LabelList";
            this.LabelList.ScrollAlwaysVisible = true;
            this.LabelList.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.LabelList.Size = new System.Drawing.Size(46, 19);
            this.LabelList.TabIndex = 3;
            this.LabelList.SelectedIndexChanged += new System.EventHandler(this.LabelList_SelectedIndexChanged);
            // 
            // curTime
            // 
            this.curTime.Location = new System.Drawing.Point(264, 701);
            this.curTime.MaxDate = new System.DateTime(2018, 12, 31, 0, 0, 0, 0);
            this.curTime.MinDate = new System.DateTime(2007, 1, 1, 0, 0, 0, 0);
            this.curTime.MinimumSize = new System.Drawing.Size(4, 30);
            this.curTime.Name = "curTime";
            this.curTime.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.curTime.Size = new System.Drawing.Size(180, 30);
            this.curTime.TabIndex = 7;
            this.curTime.Value = new System.DateTime(2007, 1, 2, 0, 0, 0, 0);
            this.curTime.DragOver += new System.Windows.Forms.DragEventHandler(this.curTime_DragOver);
            // 
            // dataNumLabel
            // 
            this.dataNumLabel.AutoSize = true;
            this.dataNumLabel.Location = new System.Drawing.Point(24, 64);
            this.dataNumLabel.Name = "dataNumLabel";
            this.dataNumLabel.Size = new System.Drawing.Size(0, 0);
            this.dataNumLabel.TabIndex = 9;
            // 
            // alphaBar
            // 
            this.alphaBar.LargeChange = 10;
            this.alphaBar.Location = new System.Drawing.Point(198, 49);
            this.alphaBar.Maximum = 100;
            this.alphaBar.Minimum = 0;
            this.alphaBar.MouseWheelBarPartitions = 10;
            this.alphaBar.Name = "alphaBar";
            this.alphaBar.Orientation = MetroFramework.Controls.MetroScrollOrientation.Horizontal;
            this.alphaBar.ScrollbarSize = 15;
            this.alphaBar.Size = new System.Drawing.Size(300, 15);
            this.alphaBar.TabIndex = 255;
            this.alphaBar.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.alphaBar.UseBarColor = true;
            this.alphaBar.UseCustomBackColor = true;
            this.alphaBar.UseSelectable = true;
            this.alphaBar.Scroll += new System.Windows.Forms.ScrollEventHandler(this.alphaBar_Scroll);
            // 
            // mode
            // 
            this.mode.AutoSize = true;
            this.mode.Location = new System.Drawing.Point(530, 711);
            this.mode.Name = "mode";
            this.mode.Size = new System.Drawing.Size(193, 20);
            this.mode.TabIndex = 256;
            this.mode.Text = "Mode : Gradient by two color ";
            this.mode.Click += new System.EventHandler(this.mode_Click);
            // 
            // updateScatterPlot
            // 
            this.updateScatterPlot.Location = new System.Drawing.Point(729, 721);
            this.updateScatterPlot.Name = "updateScatterPlot";
            this.updateScatterPlot.Size = new System.Drawing.Size(75, 23);
            this.updateScatterPlot.TabIndex = 257;
            this.updateScatterPlot.Text = "Update";
            this.updateScatterPlot.UseSelectable = true;
            this.updateScatterPlot.Click += new System.EventHandler(this.metroButton1_Click_1);
            // 
            // LabelView2
            // 
            this.LabelView2.AutoArrange = false;
            this.LabelView2.CheckBoxes = true;
            this.LabelView2.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.LabelView2.ForeColor = System.Drawing.SystemColors.WindowText;
            this.LabelView2.FullRowSelect = true;
            this.LabelView2.Location = new System.Drawing.Point(377, 404);
            this.LabelView2.Name = "LabelView2";
            this.LabelView2.OwnerDraw = true;
            this.LabelView2.Size = new System.Drawing.Size(121, 225);
            this.LabelView2.TabIndex = 259;
            this.LabelView2.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.LabelView2.UseCompatibleStateImageBehavior = false;
            this.LabelView2.UseCustomBackColor = true;
            this.LabelView2.UseCustomForeColor = true;
            this.LabelView2.UseSelectable = true;
            this.LabelView2.UseStyleColors = true;
            this.LabelView2.View = System.Windows.Forms.View.List;
            // 
            // RightClickMenu
            // 
            this.RightClickMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.RightClickMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.graphToolStripMenuItem,
            this.correlationCoefficientToolStripMenuItem});
            this.RightClickMenu.Name = "metroContextMenu1";
            this.RightClickMenu.Size = new System.Drawing.Size(241, 56);
            // 
            // graphToolStripMenuItem
            // 
            this.graphToolStripMenuItem.Name = "graphToolStripMenuItem";
            this.graphToolStripMenuItem.Size = new System.Drawing.Size(240, 26);
            this.graphToolStripMenuItem.Text = "Graph";
            this.graphToolStripMenuItem.Click += new System.EventHandler(this.graphToolStripMenuItem_Click);
            // 
            // correlationCoefficientToolStripMenuItem
            // 
            this.correlationCoefficientToolStripMenuItem.Name = "correlationCoefficientToolStripMenuItem";
            this.correlationCoefficientToolStripMenuItem.Size = new System.Drawing.Size(240, 26);
            this.correlationCoefficientToolStripMenuItem.Text = "Correlation Coefficient";
            this.correlationCoefficientToolStripMenuItem.Click += new System.EventHandler(this.correlationCoefficientToolStripMenuItem_Click);
            // 
            // LabelViewMode
            // 
            this.LabelViewMode.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.LabelViewMode.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.normalToolStripMenuItem,
            this.sortByCCToolStripMenuItem});
            this.LabelViewMode.Name = "LabelViewMode";
            this.LabelViewMode.Size = new System.Drawing.Size(159, 56);
            this.LabelViewMode.Opening += new System.ComponentModel.CancelEventHandler(this.LabelViewMode_Opening);
            // 
            // normalToolStripMenuItem
            // 
            this.normalToolStripMenuItem.Name = "normalToolStripMenuItem";
            this.normalToolStripMenuItem.Size = new System.Drawing.Size(158, 26);
            this.normalToolStripMenuItem.Text = "Normal";
            this.normalToolStripMenuItem.Click += new System.EventHandler(this.normalToolStripMenuItem_Click);
            // 
            // sortByCCToolStripMenuItem
            // 
            this.sortByCCToolStripMenuItem.Name = "sortByCCToolStripMenuItem";
            this.sortByCCToolStripMenuItem.Size = new System.Drawing.Size(158, 26);
            this.sortByCCToolStripMenuItem.Text = "Sort by CC";
            this.sortByCCToolStripMenuItem.Click += new System.EventHandler(this.sortByCCToolStripMenuItem_Click);
            // 
            // LabelViewCC
            // 
            this.LabelViewCC.AutoArrange = false;
            this.LabelViewCC.CheckBoxes = true;
            this.LabelViewCC.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.LabelViewCC.ForeColor = System.Drawing.SystemColors.WindowText;
            this.LabelViewCC.FullRowSelect = true;
            this.LabelViewCC.Location = new System.Drawing.Point(211, 416);
            this.LabelViewCC.Name = "LabelViewCC";
            this.LabelViewCC.OwnerDraw = true;
            this.LabelViewCC.Size = new System.Drawing.Size(121, 225);
            this.LabelViewCC.TabIndex = 263;
            this.LabelViewCC.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.LabelViewCC.UseCompatibleStateImageBehavior = false;
            this.LabelViewCC.UseCustomBackColor = true;
            this.LabelViewCC.UseCustomForeColor = true;
            this.LabelViewCC.UseSelectable = true;
            this.LabelViewCC.UseStyleColors = true;
            this.LabelViewCC.View = System.Windows.Forms.View.List;
            this.LabelViewCC.MouseDown += new System.Windows.Forms.MouseEventHandler(this.LabelViewCC_MouseDown);
            // 
            // Loading
            // 
            this.Loading.Image = global::visualization.Properties.Resources.loading;
            this.Loading.Location = new System.Drawing.Point(12, 11);
            this.Loading.Name = "Loading";
            this.Loading.Size = new System.Drawing.Size(1033, 720);
            this.Loading.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.Loading.TabIndex = 264;
            this.Loading.TabStop = false;
            this.Loading.Click += new System.EventHandler(this.Loading_Click);
            // 
            // scatterPlot
            // 
            this.scatterPlot.BackColor = System.Drawing.SystemColors.Control;
            this.scatterPlot.Location = new System.Drawing.Point(594, 479);
            this.scatterPlot.Name = "scatterPlot";
            this.scatterPlot.Size = new System.Drawing.Size(150, 150);
            this.scatterPlot.TabIndex = 258;
            // 
            // graph_table
            // 
            this.graph_table.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            this.graph_table.BackColor = System.Drawing.SystemColors.Control;
            this.graph_table.CausesValidation = false;
            this.graph_table.ForeColor = System.Drawing.Color.Black;
            this.graph_table.Location = new System.Drawing.Point(0, 87);
            this.graph_table.Name = "graph_table";
            this.graph_table.Size = new System.Drawing.Size(1296, 323);
            this.graph_table.TabIndex = 0;
            this.graph_table.Load += new System.EventHandler(this.graph_table_Load);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1298, 767);
            this.Controls.Add(this.Loading);
            this.Controls.Add(this.LabelViewCC);
            this.Controls.Add(this.LabelView2);
            this.Controls.Add(this.scatterPlot);
            this.Controls.Add(this.updateScatterPlot);
            this.Controls.Add(this.mode);
            this.Controls.Add(this.alphaBar);
            this.Controls.Add(this.dataNumLabel);
            this.Controls.Add(this.preTime);
            this.Controls.Add(this.curTime);
            this.Controls.Add(this.updateBtn);
            this.Controls.Add(this.LabelView);
            this.Controls.Add(this.LabelList);
            this.Controls.Add(this.graph_table);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Name = "Form1";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Graph";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.DragOver += new System.Windows.Forms.DragEventHandler(this.Form1_DragOver);
            this.DragLeave += new System.EventHandler(this.Form1_DragLeave);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            this.RightClickMenu.ResumeLayout(false);
            this.LabelViewMode.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Loading)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Graph graph_table;
        private MetroFramework.Controls.MetroListView LabelView;
        private MetroFramework.Controls.MetroButton updateBtn;
        private MetroFramework.Controls.MetroDateTime preTime;
        private System.Windows.Forms.ListBox LabelList;
        private MetroFramework.Controls.MetroDateTime curTime;
        private MetroFramework.Controls.MetroLabel dataNumLabel;
        private MetroFramework.Controls.MetroScrollBar alphaBar;
        private MetroFramework.Controls.MetroLabel mode;
        private MetroFramework.Controls.MetroButton updateScatterPlot;
        private ScatterPlot scatterPlot;
        private MetroFramework.Controls.MetroListView LabelView2;
        private MetroFramework.Controls.MetroContextMenu RightClickMenu;
        private System.Windows.Forms.ToolStripMenuItem graphToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem correlationCoefficientToolStripMenuItem;
        private MetroFramework.Controls.MetroContextMenu LabelViewMode;
        private System.Windows.Forms.ToolStripMenuItem normalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sortByCCToolStripMenuItem;
        private MetroFramework.Controls.MetroListView LabelViewCC;
        private System.Windows.Forms.PictureBox Loading;
       

    }
}

