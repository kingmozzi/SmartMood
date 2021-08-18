
namespace LED_GUI
{
    partial class Form1
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
            this.label1 = new System.Windows.Forms.Label();
            this.liveButton = new System.Windows.Forms.Button();
            this.moodButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.redBox = new System.Windows.Forms.TextBox();
            this.greenBox = new System.Windows.Forms.TextBox();
            this.blueBox = new System.Windows.Forms.TextBox();
            this.submitButton = new System.Windows.Forms.Button();
            this.portComboBox = new System.Windows.Forms.ComboBox();
            this.portButton = new System.Windows.Forms.Button();
            this.closerButton = new System.Windows.Forms.Button();
            this.wiingbutton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("맑은 고딕", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(30, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(289, 47);
            this.label1.TabIndex = 0;
            this.label1.Text = "현재 연결된 포트";
            // 
            // liveButton
            // 
            this.liveButton.Font = new System.Drawing.Font("맑은 고딕", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.liveButton.Location = new System.Drawing.Point(47, 105);
            this.liveButton.Name = "liveButton";
            this.liveButton.Size = new System.Drawing.Size(233, 56);
            this.liveButton.TabIndex = 3;
            this.liveButton.Text = "Live Mode";
            this.liveButton.UseVisualStyleBackColor = true;
            this.liveButton.Click += new System.EventHandler(this.liveButton_Click);
            // 
            // moodButton
            // 
            this.moodButton.Font = new System.Drawing.Font("맑은 고딕", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.moodButton.Location = new System.Drawing.Point(47, 179);
            this.moodButton.Name = "moodButton";
            this.moodButton.Size = new System.Drawing.Size(233, 56);
            this.moodButton.TabIndex = 4;
            this.moodButton.Text = "Mood Mode";
            this.moodButton.UseVisualStyleBackColor = true;
            this.moodButton.Click += new System.EventHandler(this.moodButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("맑은 고딕", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(47, 287);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 37);
            this.label2.TabIndex = 5;
            this.label2.Text = "R";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("맑은 고딕", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label3.ForeColor = System.Drawing.Color.Green;
            this.label3.Location = new System.Drawing.Point(192, 287);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(36, 37);
            this.label3.TabIndex = 6;
            this.label3.Text = "G";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("맑은 고딕", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label4.ForeColor = System.Drawing.Color.Blue;
            this.label4.Location = new System.Drawing.Point(352, 287);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(33, 37);
            this.label4.TabIndex = 7;
            this.label4.Text = "B";
            // 
            // redBox
            // 
            this.redBox.Font = new System.Drawing.Font("맑은 고딕", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.redBox.Location = new System.Drawing.Point(86, 287);
            this.redBox.Name = "redBox";
            this.redBox.Size = new System.Drawing.Size(100, 39);
            this.redBox.TabIndex = 8;
            this.redBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.redBox_KeyPress);
            // 
            // greenBox
            // 
            this.greenBox.Font = new System.Drawing.Font("맑은 고딕", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.greenBox.Location = new System.Drawing.Point(234, 289);
            this.greenBox.Name = "greenBox";
            this.greenBox.Size = new System.Drawing.Size(100, 39);
            this.greenBox.TabIndex = 9;
            this.greenBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.greenBox_KeyPress);
            // 
            // blueBox
            // 
            this.blueBox.Font = new System.Drawing.Font("맑은 고딕", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.blueBox.Location = new System.Drawing.Point(391, 289);
            this.blueBox.Name = "blueBox";
            this.blueBox.Size = new System.Drawing.Size(100, 39);
            this.blueBox.TabIndex = 10;
            this.blueBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.blueBox_KeyPress);
            // 
            // submitButton
            // 
            this.submitButton.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.submitButton.Location = new System.Drawing.Point(539, 293);
            this.submitButton.Name = "submitButton";
            this.submitButton.Size = new System.Drawing.Size(81, 35);
            this.submitButton.TabIndex = 11;
            this.submitButton.Text = "Submit";
            this.submitButton.UseVisualStyleBackColor = true;
            this.submitButton.Click += new System.EventHandler(this.submitButton_Click);
            // 
            // portComboBox
            // 
            this.portComboBox.Font = new System.Drawing.Font("맑은 고딕", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.portComboBox.FormattingEnabled = true;
            this.portComboBox.Location = new System.Drawing.Point(325, 40);
            this.portComboBox.Name = "portComboBox";
            this.portComboBox.Size = new System.Drawing.Size(325, 40);
            this.portComboBox.TabIndex = 12;
            this.portComboBox.SelectedIndexChanged += new System.EventHandler(this.portComboBox_SelectedIndexChanged);
            // 
            // portButton
            // 
            this.portButton.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.portButton.Location = new System.Drawing.Point(675, 45);
            this.portButton.Name = "portButton";
            this.portButton.Size = new System.Drawing.Size(81, 35);
            this.portButton.TabIndex = 14;
            this.portButton.Text = "포트 선택";
            this.portButton.UseVisualStyleBackColor = true;
            this.portButton.Click += new System.EventHandler(this.portButton_Click);
            // 
            // closerButton
            // 
            this.closerButton.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.closerButton.Location = new System.Drawing.Point(675, 392);
            this.closerButton.Name = "closerButton";
            this.closerButton.Size = new System.Drawing.Size(81, 33);
            this.closerButton.TabIndex = 15;
            this.closerButton.Text = "포트 해제";
            this.closerButton.UseVisualStyleBackColor = true;
            this.closerButton.Click += new System.EventHandler(this.closerButton_Click);
            // 
            // wiingbutton
            // 
            this.wiingbutton.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.wiingbutton.Location = new System.Drawing.Point(47, 392);
            this.wiingbutton.Name = "wiingbutton";
            this.wiingbutton.Size = new System.Drawing.Size(81, 33);
            this.wiingbutton.TabIndex = 17;
            this.wiingbutton.Text = "진동 on/off";
            this.wiingbutton.UseVisualStyleBackColor = true;
            this.wiingbutton.Click += new System.EventHandler(this.wiingbutton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.wiingbutton);
            this.Controls.Add(this.closerButton);
            this.Controls.Add(this.portButton);
            this.Controls.Add(this.portComboBox);
            this.Controls.Add(this.submitButton);
            this.Controls.Add(this.blueBox);
            this.Controls.Add(this.greenBox);
            this.Controls.Add(this.redBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.moodButton);
            this.Controls.Add(this.liveButton);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button liveButton;
        private System.Windows.Forms.Button moodButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox redBox;
        private System.Windows.Forms.TextBox greenBox;
        private System.Windows.Forms.TextBox blueBox;
        private System.Windows.Forms.Button submitButton;
        private System.Windows.Forms.ComboBox portComboBox;
        private System.Windows.Forms.Button portButton;
        private System.Windows.Forms.Button closerButton;
        private System.Windows.Forms.Button wiingbutton;
    }
}

