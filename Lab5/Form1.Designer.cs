using System;
using System.Drawing;
using System.Windows.Forms;

namespace Lab5
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
            this.btnTranslate = new System.Windows.Forms.Button();
            this.btnRotate = new System.Windows.Forms.Button();
            this.btnReflect = new System.Windows.Forms.Button();
            this.btnScale = new System.Windows.Forms.Button();
            this.btnTextTranslate = new System.Windows.Forms.Button();
            this.btnResetFigure = new System.Windows.Forms.Button();
            this.btnResetText = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnTranslate
            // 
            this.btnTranslate.Location = new System.Drawing.Point(20, 20);
            this.btnTranslate.Name = "btnTranslate";
            this.btnTranslate.Size = new System.Drawing.Size(130, 35);
            this.btnTranslate.TabIndex = 0;
            this.btnTranslate.Text = "Фігура: Переміщення";
            this.btnTranslate.UseVisualStyleBackColor = true;
            this.btnTranslate.Click += new System.EventHandler(this.btnTranslate_Click);
            // 
            // btnRotate
            // 
            this.btnRotate.Location = new System.Drawing.Point(160, 20);
            this.btnRotate.Name = "btnRotate";
            this.btnRotate.Size = new System.Drawing.Size(130, 35);
            this.btnRotate.TabIndex = 1;
            this.btnRotate.Text = "Фігура: Обертання";
            this.btnRotate.UseVisualStyleBackColor = true;
            this.btnRotate.Click += new System.EventHandler(this.btnRotate_Click);
            // 
            // btnReflect
            // 
            this.btnReflect.Location = new System.Drawing.Point(300, 20);
            this.btnReflect.Name = "btnReflect";
            this.btnReflect.Size = new System.Drawing.Size(130, 35);
            this.btnReflect.TabIndex = 2;
            this.btnReflect.Text = "Фігура: Відображення";
            this.btnReflect.UseVisualStyleBackColor = true;
            this.btnReflect.Click += new System.EventHandler(this.btnReflect_Click);
            // 
            // btnScale
            // 
            this.btnScale.Location = new System.Drawing.Point(440, 20);
            this.btnScale.Name = "btnScale";
            this.btnScale.Size = new System.Drawing.Size(130, 35);
            this.btnScale.TabIndex = 3;
            this.btnScale.Text = "Фігура: Розтягування";
            this.btnScale.UseVisualStyleBackColor = true;
            this.btnScale.Click += new System.EventHandler(this.btnScale_Click);
            // 
            // btnTextTranslate
            // 
            this.btnTextTranslate.Location = new System.Drawing.Point(20, 70);
            this.btnTextTranslate.Name = "btnTextTranslate";
            this.btnTextTranslate.Size = new System.Drawing.Size(130, 35);
            this.btnTextTranslate.TabIndex = 4;
            this.btnTextTranslate.Text = "Текст: Переміщення";
            this.btnTextTranslate.UseVisualStyleBackColor = true;
            this.btnTextTranslate.Click += new System.EventHandler(this.btnTextTranslate_Click);
            // 
            // btnResetFigure
            // 
            this.btnResetFigure.BackColor = System.Drawing.Color.OrangeRed;
            this.btnResetFigure.ForeColor = System.Drawing.Color.White;
            this.btnResetFigure.Location = new System.Drawing.Point(580, 20);
            this.btnResetFigure.Name = "btnResetFigure";
            this.btnResetFigure.Size = new System.Drawing.Size(100, 35);
            this.btnResetFigure.TabIndex = 8;
            this.btnResetFigure.Text = "Скид. Фігуру";
            this.btnResetFigure.UseVisualStyleBackColor = false;
            this.btnResetFigure.Click += new System.EventHandler(this.btnResetFigure_Click);
            // 
            // btnResetText
            // 
            this.btnResetText.BackColor = System.Drawing.Color.RoyalBlue;
            this.btnResetText.ForeColor = System.Drawing.Color.White;
            this.btnResetText.Location = new System.Drawing.Point(580, 70);
            this.btnResetText.Name = "btnResetText";
            this.btnResetText.Size = new System.Drawing.Size(100, 35);
            this.btnResetText.TabIndex = 9;
            this.btnResetText.Text = "Скид. Текст";
            this.btnResetText.UseVisualStyleBackColor = false;
            this.btnResetText.Click += new System.EventHandler(this.btnResetText_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 500);
            this.Controls.Add(this.btnTranslate);
            this.Controls.Add(this.btnRotate);
            this.Controls.Add(this.btnReflect);
            this.Controls.Add(this.btnScale);
            this.Controls.Add(this.btnTextTranslate);
            this.Controls.Add(this.btnResetFigure);
            this.Controls.Add(this.btnResetText);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Лабораторна робота №5 - Перетворення";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.ResumeLayout(false);

        }

        #endregion

        // === Кнопки для ФІГУРИ ===
        private Button btnTranslate;
        private Button btnRotate;
        private Button btnReflect;
        private Button btnScale;

        // === Кнопка для ТЕКСТУ ===
        private Button btnTextTranslate;

        // === Кнопка скидання ===
        private Button btnResetFigure;
        private Button btnResetText;
    }
}