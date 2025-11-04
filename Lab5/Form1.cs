using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Lab5
{
    public partial class Form1 : Form
    {
        // Оригінальні дані (не змінюються)
        private readonly PointF[] originalPoints;
        private const string displayText = "Лабораторна №5";
        private readonly Font textFont;
        private readonly PointF textOrigin = new PointF(400, 200);

        // Незалежні матриці
        private readonly Matrix figureMatrix = new Matrix();
        private readonly Matrix textMatrix = new Matrix();

        public Form1()
        {
            InitializeComponent();
            DoubleBuffered = true;

            originalPoints = new[]
            {
                new PointF(200, 250),
                new PointF(300, 350),
                new PointF(100,  350)
            };

            textFont = new Font("Arial", 28, FontStyle.Bold);
        }
        // PAINT
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            // 1. Оригінальна фігура
            g.FillPolygon(Brushes.LightGray, originalPoints);
            g.DrawPolygon(Pens.Black, originalPoints);

            // 2. Оригінальний текст
            GraphicsPath origPath = new GraphicsPath();
            origPath.AddString(displayText, textFont.FontFamily,
                               (int)textFont.Style, textFont.Size,
                               textOrigin, StringFormat.GenericDefault);
            g.FillPath(Brushes.LightGray, origPath);
            g.DrawPath(Pens.Gray, origPath);

            // 3. Трансформована фігура
            PointF[] fig = (PointF[])originalPoints.Clone();
            figureMatrix.TransformPoints(fig);
            g.FillPolygon(Brushes.Coral, fig);
            g.DrawPolygon(Pens.Red, fig);

            // 4. Трансформований текст
            GraphicsPath txtPath = (GraphicsPath)origPath.Clone();
            txtPath.Transform(textMatrix);
            g.FillPath(Brushes.Navy, txtPath);
            g.DrawPath(Pens.DarkBlue, txtPath);
        }

        // ФУНКЦІЇ ДЛЯ ПОШУКУ ЦЕНТРУ 
        private PointF GetCurrentFigureCenter()
        {
            PointF[] pts = (PointF[])originalPoints.Clone();
            figureMatrix.TransformPoints(pts);
            float cx = (pts[0].X + pts[1].X + pts[2].X) / 3f;
            float cy = (pts[0].Y + pts[1].Y + pts[2].Y) / 3f;
            return new PointF(cx, cy);
        }

        // ФІГУРА: Переміщення
        private void btnTranslate_Click(object sender, EventArgs e)
        {
            figureMatrix.Translate(40, 20, MatrixOrder.Append);
            Invalidate();
        }

        // ФІГУРА: Обертання 
        private void btnRotate_Click(object sender, EventArgs e)
        {
            PointF center = GetCurrentFigureCenter();

            Matrix m = new Matrix();
            m.Translate(-center.X, -center.Y, MatrixOrder.Append);
            m.Rotate(15, MatrixOrder.Append);
            m.Translate(center.X, center.Y, MatrixOrder.Append);

            figureMatrix.Multiply(m, MatrixOrder.Append);
            Invalidate();
        }

        // ФІГУРА: Відображення
        private void btnReflect_Click(object sender, EventArgs e)
        {
            PointF center = GetCurrentFigureCenter();

            Matrix m = new Matrix();
            m.Translate(-(originalPoints[1].X), 0, MatrixOrder.Append);
            m.Scale(-1, 1, MatrixOrder.Append);
            m.Translate(originalPoints[1].X, 0, MatrixOrder.Append);

            figureMatrix.Multiply(m, MatrixOrder.Append);
            Invalidate();
        }

        //private void btnReflect_Click(object sender, EventArgs e)
        //{
        //    PointF center = GetCurrentFigureCenter();

        //    Matrix m = new Matrix();
        //    m.Translate(0, -center.Y, MatrixOrder.Append);
        //    m.Scale(1, -1, MatrixOrder.Append);
        //    m.Translate(0, center.Y, MatrixOrder.Append);

        //    figureMatrix.Multiply(m, MatrixOrder.Append);
        //    Invalidate();
        //}

        // =================================================================
        // ФІГУРА: Розтягування
        // =================================================================
        private void btnScale_Click(object sender, EventArgs e)
        {
            PointF center = GetCurrentFigureCenter();

            Matrix m = new Matrix();
            m.Translate(-center.X, -center.Y, MatrixOrder.Append);
            m.Scale(1.6f, 0.6f, MatrixOrder.Append);
            m.Translate(center.X, center.Y, MatrixOrder.Append);

            figureMatrix.Multiply(m, MatrixOrder.Append);
            Invalidate();
        }

        // ТЕКСТ: Переміщення
        private void btnTextTranslate_Click(object sender, EventArgs e)
        {
            textMatrix.Translate(60, 40, MatrixOrder.Append);
            Invalidate();
        }

        // СКИДАННЯ
        private void btnResetFigure_Click(object sender, EventArgs e)
        {
            figureMatrix.Reset();
            Invalidate();
        }

        private void btnResetText_Click(object sender, EventArgs e)
        {
            textMatrix.Reset();
            Invalidate();
        }
    }
}