
namespace CoralDraw_WinForms
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
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.stateButton = new System.Windows.Forms.Button();
            this.colorButton = new System.Windows.Forms.Button();
            this.colorDialog = new System.Windows.Forms.ColorDialog();
            this.SuspendLayout();
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Ellipse",
            "Rectangle"});
            this.comboBox1.Location = new System.Drawing.Point(14, 13);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(151, 28);
            this.comboBox1.TabIndex = 0;
            this.comboBox1.SelectedIndex = 0;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.OnChangeFigureFactory);
            // 
            // comboBox2
            // 
            this.comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] {
            "Solid",
            "Bound"});
            this.comboBox2.Location = new System.Drawing.Point(13, 47);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(151, 28);
            this.comboBox2.TabIndex = 1;
            this.comboBox2.SelectedIndex = 0;
            this.comboBox2.SelectedIndexChanged += new System.EventHandler(this.OnChangeDrawerFactory);
            // 
            // button1
            // 
            this.stateButton.Location = new System.Drawing.Point(171, 13);
            this.stateButton.Name = "button1";
            this.stateButton.Size = new System.Drawing.Size(123, 29);
            this.stateButton.TabIndex = 2;
            this.stateButton.Text = "Change state";
            this.stateButton.UseVisualStyleBackColor = true;
            this.stateButton.Click += new System.EventHandler(this.OnChangeState);
            // 
            // button2
            // 
            this.colorButton.Location = new System.Drawing.Point(171, 45);
            this.colorButton.Name = "button2";
            this.colorButton.Size = new System.Drawing.Size(123, 29);
            this.colorButton.TabIndex = 3;
            this.colorButton.Text = "Color";
            this.colorButton.UseVisualStyleBackColor = true;
            this.colorButton.Click += new System.EventHandler(this.OnChangeColor);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1338, 635);
            this.Controls.Add(this.colorButton);
            this.Controls.Add(this.stateButton);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.comboBox1);
            this.KeyPreview = true;
            this.Name = "Form1";
            this.Text = "CoralDraw (Almost version)";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnKeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.OnKeyUp);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnMouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.OnMouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.OnMouseUp);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Button stateButton;
        private System.Windows.Forms.Button colorButton;
        private System.Windows.Forms.ColorDialog colorDialog;
    }
}

