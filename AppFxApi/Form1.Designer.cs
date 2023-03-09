namespace AppFxApi
{
    partial class Form1
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
            this.tbOutputLog = new System.Windows.Forms.TextBox();
            this.tbLookupId = new System.Windows.Forms.TextBox();
            this.lbLookupId = new System.Windows.Forms.Label();
            this.lbName = new System.Windows.Forms.Label();
            this.lbID = new System.Windows.Forms.Label();
            this.lbNickName = new System.Windows.Forms.Label();
            this.lbAddressBlock = new System.Windows.Forms.Label();
            this.lbCity = new System.Windows.Forms.Label();
            this.lbState = new System.Windows.Forms.Label();
            this.lbCountry = new System.Windows.Forms.Label();
            this.lbPostCode = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tbOutputLog
            // 
            this.tbOutputLog.Location = new System.Drawing.Point(13, 276);
            this.tbOutputLog.Multiline = true;
            this.tbOutputLog.Name = "tbOutputLog";
            this.tbOutputLog.Size = new System.Drawing.Size(2026, 864);
            this.tbOutputLog.TabIndex = 0;
            // 
            // tbLookupId
            // 
            this.tbLookupId.Location = new System.Drawing.Point(74, 10);
            this.tbLookupId.Name = "tbLookupId";
            this.tbLookupId.Size = new System.Drawing.Size(100, 20);
            this.tbLookupId.TabIndex = 1;
            // 
            // lbLookupId
            // 
            this.lbLookupId.AutoSize = true;
            this.lbLookupId.Location = new System.Drawing.Point(13, 13);
            this.lbLookupId.Name = "lbLookupId";
            this.lbLookupId.Size = new System.Drawing.Size(55, 13);
            this.lbLookupId.TabIndex = 2;
            this.lbLookupId.Text = "Lookup Id";
            // 
            // lbName
            // 
            this.lbName.AutoSize = true;
            this.lbName.Location = new System.Drawing.Point(10, 75);
            this.lbName.Name = "lbName";
            this.lbName.Size = new System.Drawing.Size(35, 13);
            this.lbName.TabIndex = 3;
            this.lbName.Text = "Name";
            // 
            // lbID
            // 
            this.lbID.AutoSize = true;
            this.lbID.Location = new System.Drawing.Point(10, 43);
            this.lbID.Name = "lbID";
            this.lbID.Size = new System.Drawing.Size(18, 13);
            this.lbID.TabIndex = 4;
            this.lbID.Text = "ID";
            // 
            // lbNickName
            // 
            this.lbNickName.AutoSize = true;
            this.lbNickName.Location = new System.Drawing.Point(10, 175);
            this.lbNickName.Name = "lbNickName";
            this.lbNickName.Size = new System.Drawing.Size(55, 13);
            this.lbNickName.TabIndex = 5;
            this.lbNickName.Text = "Nickname";
            // 
            // lbAddressBlock
            // 
            this.lbAddressBlock.AutoSize = true;
            this.lbAddressBlock.Location = new System.Drawing.Point(10, 108);
            this.lbAddressBlock.Name = "lbAddressBlock";
            this.lbAddressBlock.Size = new System.Drawing.Size(45, 13);
            this.lbAddressBlock.TabIndex = 6;
            this.lbAddressBlock.Text = "Address";
            // 
            // lbCity
            // 
            this.lbCity.AutoSize = true;
            this.lbCity.Location = new System.Drawing.Point(10, 136);
            this.lbCity.Name = "lbCity";
            this.lbCity.Size = new System.Drawing.Size(24, 13);
            this.lbCity.TabIndex = 7;
            this.lbCity.Text = "City";
            // 
            // lbState
            // 
            this.lbState.AutoSize = true;
            this.lbState.Location = new System.Drawing.Point(71, 136);
            this.lbState.Name = "lbState";
            this.lbState.Size = new System.Drawing.Size(32, 13);
            this.lbState.TabIndex = 8;
            this.lbState.Text = "State";
            // 
            // lbCountry
            // 
            this.lbCountry.AutoSize = true;
            this.lbCountry.Location = new System.Drawing.Point(152, 136);
            this.lbCountry.Name = "lbCountry";
            this.lbCountry.Size = new System.Drawing.Size(43, 13);
            this.lbCountry.TabIndex = 9;
            this.lbCountry.Text = "Country";
            // 
            // lbPostCode
            // 
            this.lbPostCode.AutoSize = true;
            this.lbPostCode.Location = new System.Drawing.Point(234, 136);
            this.lbPostCode.Name = "lbPostCode";
            this.lbPostCode.Size = new System.Drawing.Size(64, 13);
            this.lbPostCode.TabIndex = 10;
            this.lbPostCode.Text = "Postal Code";
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(170, 8);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(35, 23);
            this.btnSearch.TabIndex = 11;
            this.btnSearch.Text = "...";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2039, 1140);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.lbPostCode);
            this.Controls.Add(this.lbCountry);
            this.Controls.Add(this.lbState);
            this.Controls.Add(this.lbCity);
            this.Controls.Add(this.lbAddressBlock);
            this.Controls.Add(this.lbNickName);
            this.Controls.Add(this.lbID);
            this.Controls.Add(this.lbName);
            this.Controls.Add(this.lbLookupId);
            this.Controls.Add(this.tbLookupId);
            this.Controls.Add(this.tbOutputLog);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbOutputLog;
        private System.Windows.Forms.TextBox tbLookupId;
        private System.Windows.Forms.Label lbLookupId;
        private System.Windows.Forms.Label lbName;
        private System.Windows.Forms.Label lbID;
        private System.Windows.Forms.Label lbNickName;
        private System.Windows.Forms.Label lbAddressBlock;
        private System.Windows.Forms.Label lbCity;
        private System.Windows.Forms.Label lbState;
        private System.Windows.Forms.Label lbCountry;
        private System.Windows.Forms.Label lbPostCode;
        private System.Windows.Forms.Button btnSearch;
    }
}

