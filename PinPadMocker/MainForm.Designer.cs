namespace PinPadMocker
{
	partial class MainForm
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
			this.components = new System.ComponentModel.Container();
			this.UxComboVirtualCom = new System.Windows.Forms.ComboBox();
			this.UxLabelVirtualCom = new System.Windows.Forms.Label();
			this.UxLabelRealCom = new System.Windows.Forms.Label();
			this.UxComboRealCom = new System.Windows.Forms.ComboBox();
			this.UxRadioModeSimulate = new System.Windows.Forms.RadioButton();
			this.UxRadioModeIntercept = new System.Windows.Forms.RadioButton();
			this.UxButtonStart = new System.Windows.Forms.Button();
			this.UxTextLog = new System.Windows.Forms.RichTextBox();
			this.UxComUpdateTimer = new System.Windows.Forms.Timer(this.components);
			this.UxButtonSave = new System.Windows.Forms.Button();
			this.UxButtonLoad = new System.Windows.Forms.Button();
			this.UxButtonReset = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// UxComboVirtualCom
			// 
			this.UxComboVirtualCom.FormattingEnabled = true;
			this.UxComboVirtualCom.Location = new System.Drawing.Point(12, 25);
			this.UxComboVirtualCom.Name = "UxComboVirtualCom";
			this.UxComboVirtualCom.Size = new System.Drawing.Size(76, 21);
			this.UxComboVirtualCom.TabIndex = 1;
			// 
			// UxLabelVirtualCom
			// 
			this.UxLabelVirtualCom.AutoSize = true;
			this.UxLabelVirtualCom.Location = new System.Drawing.Point(12, 9);
			this.UxLabelVirtualCom.Name = "UxLabelVirtualCom";
			this.UxLabelVirtualCom.Size = new System.Drawing.Size(76, 13);
			this.UxLabelVirtualCom.TabIndex = 2;
			this.UxLabelVirtualCom.Text = "PinPad Virtual:";
			// 
			// UxLabelRealCom
			// 
			this.UxLabelRealCom.AutoSize = true;
			this.UxLabelRealCom.Location = new System.Drawing.Point(94, 9);
			this.UxLabelRealCom.Name = "UxLabelRealCom";
			this.UxLabelRealCom.Size = new System.Drawing.Size(69, 13);
			this.UxLabelRealCom.TabIndex = 3;
			this.UxLabelRealCom.Text = "PinPad Real:";
			// 
			// UxComboRealCom
			// 
			this.UxComboRealCom.Enabled = false;
			this.UxComboRealCom.FormattingEnabled = true;
			this.UxComboRealCom.Location = new System.Drawing.Point(94, 25);
			this.UxComboRealCom.Name = "UxComboRealCom";
			this.UxComboRealCom.Size = new System.Drawing.Size(76, 21);
			this.UxComboRealCom.TabIndex = 4;
			// 
			// UxRadioModeSimulate
			// 
			this.UxRadioModeSimulate.AutoSize = true;
			this.UxRadioModeSimulate.Checked = true;
			this.UxRadioModeSimulate.Location = new System.Drawing.Point(176, 29);
			this.UxRadioModeSimulate.Name = "UxRadioModeSimulate";
			this.UxRadioModeSimulate.Size = new System.Drawing.Size(59, 17);
			this.UxRadioModeSimulate.TabIndex = 5;
			this.UxRadioModeSimulate.TabStop = true;
			this.UxRadioModeSimulate.Text = "Simular";
			this.UxRadioModeSimulate.UseVisualStyleBackColor = true;
			this.UxRadioModeSimulate.CheckedChanged += new System.EventHandler(this.UxRadioModeSimulate_CheckedChanged);
			// 
			// UxRadioModeIntercept
			// 
			this.UxRadioModeIntercept.AutoSize = true;
			this.UxRadioModeIntercept.Location = new System.Drawing.Point(176, 7);
			this.UxRadioModeIntercept.Name = "UxRadioModeIntercept";
			this.UxRadioModeIntercept.Size = new System.Drawing.Size(76, 17);
			this.UxRadioModeIntercept.TabIndex = 6;
			this.UxRadioModeIntercept.Text = "Interceptar";
			this.UxRadioModeIntercept.UseVisualStyleBackColor = true;
			this.UxRadioModeIntercept.CheckedChanged += new System.EventHandler(this.UxRadioModeListen_CheckedChanged);
			// 
			// UxButtonStart
			// 
			this.UxButtonStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.UxButtonStart.Location = new System.Drawing.Point(258, 7);
			this.UxButtonStart.Name = "UxButtonStart";
			this.UxButtonStart.Size = new System.Drawing.Size(80, 39);
			this.UxButtonStart.TabIndex = 7;
			this.UxButtonStart.Text = "START";
			this.UxButtonStart.UseVisualStyleBackColor = true;
			this.UxButtonStart.Click += new System.EventHandler(this.UxButtonStart_Click);
			// 
			// UxTextLog
			// 
			this.UxTextLog.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.UxTextLog.Location = new System.Drawing.Point(0, 52);
			this.UxTextLog.Name = "UxTextLog";
			this.UxTextLog.ReadOnly = true;
			this.UxTextLog.Size = new System.Drawing.Size(606, 549);
			this.UxTextLog.TabIndex = 8;
			this.UxTextLog.Text = "";
			// 
			// UxComUpdateTimer
			// 
			this.UxComUpdateTimer.Enabled = true;
			this.UxComUpdateTimer.Interval = 1000;
			this.UxComUpdateTimer.Tick += new System.EventHandler(this.UxComUpdateTimer_Tick);
			// 
			// UxButtonSave
			// 
			this.UxButtonSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.UxButtonSave.Location = new System.Drawing.Point(344, 7);
			this.UxButtonSave.Name = "UxButtonSave";
			this.UxButtonSave.Size = new System.Drawing.Size(80, 39);
			this.UxButtonSave.TabIndex = 9;
			this.UxButtonSave.Text = "SAVE";
			this.UxButtonSave.UseVisualStyleBackColor = true;
			this.UxButtonSave.Click += new System.EventHandler(this.UxButtonSave_Click);
			// 
			// UxButtonLoad
			// 
			this.UxButtonLoad.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.UxButtonLoad.Location = new System.Drawing.Point(430, 7);
			this.UxButtonLoad.Name = "UxButtonLoad";
			this.UxButtonLoad.Size = new System.Drawing.Size(80, 39);
			this.UxButtonLoad.TabIndex = 10;
			this.UxButtonLoad.Text = "LOAD";
			this.UxButtonLoad.UseVisualStyleBackColor = true;
			this.UxButtonLoad.Click += new System.EventHandler(this.UxButtonLoad_Click);
			// 
			// UxButtonReset
			// 
			this.UxButtonReset.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.UxButtonReset.Location = new System.Drawing.Point(516, 7);
			this.UxButtonReset.Name = "UxButtonReset";
			this.UxButtonReset.Size = new System.Drawing.Size(80, 39);
			this.UxButtonReset.TabIndex = 11;
			this.UxButtonReset.Text = "RESET";
			this.UxButtonReset.UseVisualStyleBackColor = true;
			this.UxButtonReset.Click += new System.EventHandler(this.UxButtonReset_Click);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(606, 601);
			this.Controls.Add(this.UxButtonReset);
			this.Controls.Add(this.UxButtonLoad);
			this.Controls.Add(this.UxButtonSave);
			this.Controls.Add(this.UxTextLog);
			this.Controls.Add(this.UxButtonStart);
			this.Controls.Add(this.UxRadioModeIntercept);
			this.Controls.Add(this.UxRadioModeSimulate);
			this.Controls.Add(this.UxComboRealCom);
			this.Controls.Add(this.UxLabelRealCom);
			this.Controls.Add(this.UxLabelVirtualCom);
			this.Controls.Add(this.UxComboVirtualCom);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Name = "MainForm";
			this.Text = "PinPadMocker";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.ComboBox UxComboVirtualCom;
		private System.Windows.Forms.Label UxLabelVirtualCom;
		private System.Windows.Forms.Label UxLabelRealCom;
		private System.Windows.Forms.ComboBox UxComboRealCom;
		private System.Windows.Forms.RadioButton UxRadioModeSimulate;
		private System.Windows.Forms.RadioButton UxRadioModeIntercept;
		private System.Windows.Forms.Button UxButtonStart;
		private System.Windows.Forms.RichTextBox UxTextLog;
		private System.Windows.Forms.Timer UxComUpdateTimer;
		private System.Windows.Forms.Button UxButtonSave;
		private System.Windows.Forms.Button UxButtonLoad;
		private System.Windows.Forms.Button UxButtonReset;
	}
}

