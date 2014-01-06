namespace GeomComp
{
    partial class View
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
            this.Frame = new System.Windows.Forms.PictureBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.Frame)).BeginInit();
            this.SuspendLayout();
            // 
            // Frame
            // 
            this.Frame.Location = new System.Drawing.Point(100, 0);
            this.Frame.Name = "Frame";
            this.Frame.Size = new System.Drawing.Size(720, 480);
            this.Frame.TabIndex = 0;
            this.Frame.TabStop = false;
            this.Frame.Paint += new System.Windows.Forms.PaintEventHandler(this.Frame_Paint);
            // 
            // View
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(834, 492);
            this.Controls.Add(this.Frame);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.Margin = new System.Windows.Forms.Padding(6, 4, 6, 4);
            this.Name = "View";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "GeomComp";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.Frame)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox Frame;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
    }
}

