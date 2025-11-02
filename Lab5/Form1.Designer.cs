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
            this.components = new System.ComponentModel.Container();
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(800, 500);
            this.Text = "Лабораторна робота №5 - Перетворення";
            this.StartPosition = FormStartPosition.CenterScreen;

            // === Кнопки для ФІГУРИ ===
            this.btnTranslate = new Button();
            this.btnRotate = new Button();
            this.btnReflect = new Button();
            this.btnScale = new Button();

            // === Кнопки для ТЕКСТУ ===
            this.btnTextTranslate = new Button();
            this.btnTextRotate = new Button();
            this.btnTextReflect = new Button();
            this.btnTextScale = new Button();

            // === Кнопка Скидання ===
            this.btnReset = new Button();

            this.SuspendLayout();

            // -----------------------------------------------------------------
            // КНОПКИ ДЛЯ ФІГУРИ (перший рядок)
            // -----------------------------------------------------------------
            int top1 = 20;

            this.btnTranslate.Location = new Point(20, top1);
            this.btnTranslate.Size = new Size(130, 35);
            this.btnTranslate.Text = "Фігура: Переміщення";
            this.btnTranslate.UseVisualStyleBackColor = true;
            this.btnTranslate.Click += new EventHandler(this.btnTranslate_Click);

            this.btnRotate.Location = new Point(160, top1);
            this.btnRotate.Size = new Size(130, 35);
            this.btnRotate.Text = "Фігура: Обертання";
            this.btnRotate.UseVisualStyleBackColor = true;
            this.btnRotate.Click += new EventHandler(this.btnRotate_Click);

            this.btnReflect.Location = new Point(300, top1);
            this.btnReflect.Size = new Size(130, 35);
            this.btnReflect.Text = "Фігура: Відображення";
            this.btnReflect.UseVisualStyleBackColor = true;
            this.btnReflect.Click += new EventHandler(this.btnReflect_Click);

            this.btnScale.Location = new Point(440, top1);
            this.btnScale.Size = new Size(130, 35);
            this.btnScale.Text = "Фігура: Розтягування";
            this.btnScale.UseVisualStyleBackColor = true;
            this.btnScale.Click += new EventHandler(this.btnScale_Click);

            // -----------------------------------------------------------------
            // КНОПКИ ДЛЯ ТЕКСТУ (другий рядок)
            // -----------------------------------------------------------------
            int top2 = 70;

            this.btnTextTranslate.Location = new Point(20, top2);
            this.btnTextTranslate.Size = new Size(130, 35);
            this.btnTextTranslate.Text = "Текст: Переміщення";
            this.btnTextTranslate.UseVisualStyleBackColor = true;
            this.btnTextTranslate.Click += new EventHandler(this.btnTextTranslate_Click);

            this.btnTextRotate.Location = new Point(160, top2);
            this.btnTextRotate.Size = new Size(130, 35);
            this.btnTextRotate.Text = "Текст: Обертання";
            this.btnTextRotate.UseVisualStyleBackColor = true;
            this.btnTextRotate.Click += new EventHandler(this.btnTextRotate_Click);

            this.btnTextReflect.Location = new Point(300, top2);
            this.btnTextReflect.Size = new Size(130, 35);
            this.btnTextReflect.Text = "Текст: Відображення";
            this.btnTextReflect.UseVisualStyleBackColor = true;
            this.btnTextReflect.Click += new EventHandler(this.btnTextReflect_Click);

            this.btnTextScale.Location = new Point(440, top2);
            this.btnTextScale.Size = new Size(130, 35);
            this.btnTextScale.Text = "Текст: Розтягування";
            this.btnTextScale.UseVisualStyleBackColor = true;
            this.btnTextScale.Click += new EventHandler(this.btnTextScale_Click);

            // -----------------------------------------------------------------
            // КНОПКА СКИДАННЯ
            // -----------------------------------------------------------------
            this.btnReset.Location = new Point(660, top1);
            this.btnReset.Size = new Size(120, 75);
            this.btnReset.Text = "СКИДАННЯ";
            this.btnReset.BackColor = Color.IndianRed;
            this.btnReset.ForeColor = Color.White;
            this.btnReset.Font = new Font("Arial", 10, FontStyle.Bold);
            this.btnReset.UseVisualStyleBackColor = false;
            this.btnReset.Click += new EventHandler(this.btnReset_Click);

            // -----------------------------------------------------------------
            // ДОДАВАННЯ НА ФОРМУ
            // -----------------------------------------------------------------
            this.Controls.Add(this.btnTranslate);
            this.Controls.Add(this.btnRotate);
            this.Controls.Add(this.btnReflect);
            this.Controls.Add(this.btnScale);

            this.Controls.Add(this.btnTextTranslate);
            this.Controls.Add(this.btnTextRotate);
            this.Controls.Add(this.btnTextReflect);
            this.Controls.Add(this.btnTextScale);

            this.Controls.Add(this.btnReset);

            // Подія малювання
            this.Paint += new PaintEventHandler(this.Form1_Paint);

            this.ResumeLayout(false);
        }

        #endregion

        // === Кнопки для ФІГУРИ ===
        private Button btnTranslate;
        private Button btnRotate;
        private Button btnReflect;
        private Button btnScale;

        // === Кнопки для ТЕКСТУ ===
        private Button btnTextTranslate;
        private Button btnTextRotate;
        private Button btnTextReflect;
        private Button btnTextScale;

        // === Кнопка скидання ===
        private Button btnReset;
    }
}