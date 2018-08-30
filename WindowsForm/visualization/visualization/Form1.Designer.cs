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
            this.metroButton1 = new MetroFramework.Controls.MetroButton();
            this.preTime = new MetroFramework.Controls.MetroDateTime();
            this.LabelList = new System.Windows.Forms.ListBox();
            this.curTime = new MetroFramework.Controls.MetroDateTime();
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
            // metroButton1
            // 
            this.metroButton1.Location = new System.Drawing.Point(1200, 721);
            this.metroButton1.Name = "metroButton1";
            this.metroButton1.Size = new System.Drawing.Size(75, 23);
            this.metroButton1.TabIndex = 6;
            this.metroButton1.Text = "Update";
            this.metroButton1.UseSelectable = true;
            this.metroButton1.Click += new System.EventHandler(this.metroButton1_Click);
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
            this.Controls.Add(this.preTime);
            this.Controls.Add(this.curTime);
            this.Controls.Add(this.metroButton1);
            this.Controls.Add(this.LabelView);
            this.Controls.Add(this.LabelList);
            this.Controls.Add(this.graph_table);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Name = "Form1";
            this.Text = "Graph";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.DragOver += new System.Windows.Forms.DragEventHandler(this.Form1_DragOver);
            this.DragLeave += new System.EventHandler(this.Form1_DragLeave);
            this.ResumeLayout(false);

        }

        #endregion

        private Graph graph_table;
        private MetroFramework.Controls.MetroListView LabelView;
        private MetroFramework.Controls.MetroButton metroButton1;
        private MetroFramework.Controls.MetroDateTime preTime;
        private System.Windows.Forms.ListBox LabelList;
        private MetroFramework.Controls.MetroDateTime curTime;
       

    }
}

