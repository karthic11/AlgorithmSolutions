namespace Puzzles
{
    partial class ObjectOrientedDesign
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
            this.label19 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.button18 = new System.Windows.Forms.Button();
            this.button15 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Location = new System.Drawing.Point(426, 56);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(207, 20);
            this.label19.TabIndex = 128;
            this.label19.Text = "Object Oriented Design";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(87, 106);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(803, 17);
            this.label2.TabIndex = 130;
            this.label2.Text = "Design the data structure for a generic deck of cards. Explain how you would subc" +
    "lass the datastrures to implement blackjack?";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Puzzles.Properties.Resources.cards;
            this.pictureBox1.Location = new System.Drawing.Point(90, 146);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1257, 343);
            this.pictureBox1.TabIndex = 131;
            this.pictureBox1.TabStop = false;
            // 
            // button18
            // 
            this.button18.Location = new System.Drawing.Point(563, 561);
            this.button18.Name = "button18";
            this.button18.Size = new System.Drawing.Size(106, 32);
            this.button18.TabIndex = 133;
            this.button18.Text = "Back";
            this.button18.UseVisualStyleBackColor = true;
            // 
            // button15
            // 
            this.button15.Location = new System.Drawing.Point(400, 561);
            this.button15.Name = "button15";
            this.button15.Size = new System.Drawing.Size(106, 32);
            this.button15.TabIndex = 132;
            this.button15.Text = "Continue";
            this.button15.UseVisualStyleBackColor = true;
            this.button15.Click += new System.EventHandler(this.button15_Click);
            // 
            // ObjectOrientedDesign
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1368, 637);
            this.Controls.Add(this.button18);
            this.Controls.Add(this.button15);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label19);
            this.Name = "ObjectOrientedDesign";
            this.Text = "ObjectOrientedDesing";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button button18;
        private System.Windows.Forms.Button button15;
    }
}