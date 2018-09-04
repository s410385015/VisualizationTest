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
            this.LabelView = new MetroFramework.Controls.MetroListView();
            this.updateBtn = new MetroFramework.Controls.MetroButton();
            this.preTime = new MetroFramework.Controls.MetroDateTime();
            this.LabelList = new System.Windows.Forms.ListBox();
            this.curTime = new MetroFramework.Controls.MetroDateTime();
            this.dataNumLabel = new MetroFramework.Controls.MetroLabel();
            this.metroScrollBar1 = new MetroFramework.Controls.MetroScrollBar();
            this.alphaBar = new MetroFramework.Controls.MetroScrollBar();
            this.graph_table = new visualization.Graph();
            this.SuspendLayout();
            // 
            // LabelView
            // 
            this.LabelView.AutoArrange = false;
            this.LabelView.CheckBoxes = true;
            this.LabelView.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.LabelView.ForeColor = System.Drawing.SystemColors.WindowText;
            this.LabelView.FullRowSelect = true;
            this.LabelView.Location = new System.Drawing.Point(12, 436);
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
            this.preTime.MinimumSize = new System.Drawing.Size(0, 30);
            this.preTime.Name = "preTime";
            this.preTime.Size = new System.Drawing.Size(180, 30);
            this.preTime.TabIndex = 8;
            this.preTime.Value = new System.DateTime(2007, 1, 2, 0, 0, 0, 0);
            // 
            // LabelList
            // 
            this.LabelList.FormattingEnabled = true;
            this.LabelList.ItemHeight = 15;
            this.LabelList.Location = new System.Drawing.Point(602, 712);
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
            this.curTime.MinimumSize = new System.Drawing.Size(0, 30);
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
            // metroScrollBar1
            // 
            this.metroScrollBar1.LargeChange = 10;
            this.metroScrollBar1.Location = new System.Drawing.Point(0, 0);
            this.metroScrollBar1.Maximum = 100;
            this.metroScrollBar1.Minimum = 0;
            this.metroScrollBar1.MouseWheelBarPartitions = 10;
            this.metroScrollBar1.Name = "metroScrollBar1";
            this.metroScrollBar1.Orientation = MetroFramework.Controls.MetroScrollOrientation.Vertical;
            this.metroScrollBar1.ScrollbarSize = 10;
            this.metroScrollBar1.Size = new System.Drawing.Size(10, 200);
            this.metroScrollBar1.TabIndex = 10;
            this.metroScrollBar1.UseSelectable = true;
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
            this.alphaBar.Size = new System.Drawing.Size(200, 15);
            this.alphaBar.TabIndex = 255;
            this.alphaBar.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.alphaBar.UseBarColor = true;
            this.alphaBar.UseCustomBackColor = true;
            this.alphaBar.UseSelectable = true;
            this.alphaBar.Scroll += new System.Windows.Forms.ScrollEventHandler(this.alphaBar_Scroll);
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
            this.Controls.Add(this.alphaBar);
            this.Controls.Add(this.metroScrollBar1);
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
            this.DragOver += new System.Windows.Forms.DragEventHandler(this.Form1_DragOver);
            this.DragLeave += new System.EventHandler(this.Form1_DragLeave);
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
        private MetroFramework.Controls.MetroScrollBar metroScrollBar1;
        private MetroFramework.Controls.MetroScrollBar alphaBar;
       

    }
}

