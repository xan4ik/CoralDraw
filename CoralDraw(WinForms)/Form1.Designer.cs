
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
            this.groupButton = new System.Windows.Forms.Button();
            this.ungroupButton = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
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
            this.comboBox2.SelectedIndexChanged += new System.EventHandler(this.OnChangeDrawerFactory);
            // 
            // stateButton
            // 
            this.stateButton.Location = new System.Drawing.Point(171, 13);
            this.stateButton.Name = "stateButton";
            this.stateButton.Size = new System.Drawing.Size(123, 29);
            this.stateButton.TabIndex = 2;
            this.stateButton.Text = "Change state";
            this.stateButton.UseVisualStyleBackColor = true;
            this.stateButton.Click += new System.EventHandler(this.OnChangeState);
            // 
            // colorButton
            // 
            this.colorButton.Location = new System.Drawing.Point(170, 48);
            this.colorButton.Name = "colorButton";
            this.colorButton.Size = new System.Drawing.Size(123, 29);
            this.colorButton.TabIndex = 3;
            this.colorButton.Text = "Color";
            this.colorButton.UseVisualStyleBackColor = true;
            this.colorButton.Click += new System.EventHandler(this.OnChangeColor);
            // 
            // button1
            // 
            this.groupButton.Location = new System.Drawing.Point(300, 13);
            this.groupButton.Name = "button1";
            this.groupButton.Size = new System.Drawing.Size(123, 29);
            this.groupButton.TabIndex = 4;
            this.groupButton.Text = "Group";
            this.groupButton.UseVisualStyleBackColor = true;
            this.groupButton.Click += new System.EventHandler(this.OnGroup);
            // 
            // button2
            // 
            this.ungroupButton.Location = new System.Drawing.Point(300, 48);
            this.ungroupButton.Name = "button2";
            this.ungroupButton.Size = new System.Drawing.Size(123, 29);
            this.ungroupButton.TabIndex = 5;
            this.ungroupButton.Text = "Ungroup";
            this.ungroupButton.UseVisualStyleBackColor = true;
            this.ungroupButton.Click += new System.EventHandler(this.OnUngroup);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(429, 13);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(123, 29);
            this.button3.TabIndex = 6;
            this.button3.Text = "Save";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.OnFigureSave);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1338, 635);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.ungroupButton);
            this.Controls.Add(this.groupButton);
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
        private System.Windows.Forms.Button groupButton;
        private System.Windows.Forms.Button ungroupButton;
        private System.Windows.Forms.Button button3;
    }
}

