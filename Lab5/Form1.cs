using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Lab5
{
    public partial class Form1 : Form
    {
        // ----- Оригінальні дані -----
        private readonly PointF[] originalPoints;
        private const string displayText = "Лабораторна №5";
        private readonly Font textFont;
        private readonly PointF textOrigin = new PointF(80, 320);

        // ----- Дві незалежні матриці -----
        private readonly Matrix figureMatrix = new Matrix();   // для трикутника
        private readonly Matrix textMatrix = new Matrix();   // для тексту

        public Form1()
        {
            InitializeComponent();
            DoubleBuffered = true;

            // ініціалізація геометрії
            originalPoints = new[]
            {
                new PointF(150, 100),
                new PointF(250, 200),
                new PointF(50,  200)
            };

            textFont = new Font("Arial", 28, FontStyle.Bold);
        }

        // -----------------------------------------------------------------
        //  PAINT
        // -----------------------------------------------------------------
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            /* ---------- 1. Оригінальна фігура (сірий) ---------- */
            g.FillPolygon(Brushes.LightGray, originalPoints);
            g.DrawPolygon(Pens.Black, originalPoints);

            /* ---------- 2. Оригінальний текст (світло‑сірий) ---------- */
            GraphicsPath origPath = new GraphicsPath();
            origPath.AddString(displayText, textFont.FontFamily,
                               (int)textFont.Style, textFont.Size,
                               textOrigin, StringFormat.GenericDefault);
            g.FillPath(Brushes.LightGray, origPath);
            g.DrawPath(Pens.Gray, origPath);

            /* ---------- 3. Трансформована фігура ---------- */
            PointF[] fig = (PointF[])originalPoints.Clone();
            figureMatrix.TransformPoints(fig);
            g.FillPolygon(Brushes.Coral, fig);
            g.DrawPolygon(Pens.Red, fig);

            /* ---------- 4. Трансформований текст ---------- */
            GraphicsPath txtPath = (GraphicsPath)origPath.Clone();
            txtPath.Transform(textMatrix);
            g.FillPath(Brushes.Navy, txtPath);
            g.DrawPath(Pens.DarkBlue, txtPath);
        }

        // -----------------------------------------------------------------
        //  КНОПКИ – ДІЇ НА ФІГУРУ
        // -----------------------------------------------------------------
        private void btnTranslate_Click(object sender, EventArgs e)
        {
            figureMatrix.Translate(60, 40, MatrixOrder.Append);
            Invalidate();
        }

        private void btnRotate_Click(object sender, EventArgs e)
        {
            float cx = (originalPoints[0].X + originalPoints[1].X + originalPoints[2].X) / 3f;
            float cy = (originalPoints[0].Y + originalPoints[1].Y + originalPoints[2].Y) / 3f;

            Matrix m = new Matrix();
            m.Translate(-cx, -cy, MatrixOrder.Append);
            m.Rotate(45, MatrixOrder.Append);
            m.Translate(cx, cy, MatrixOrder.Append);

            figureMatrix.Multiply(m, MatrixOrder.Append);
            Invalidate();
        }

        private void btnReflect_Click(object sender, EventArgs e)
        {
            Matrix m = new Matrix();
            m.Translate(-200, 0, MatrixOrder.Append);
            m.Scale(-1, 1, MatrixOrder.Append);
            m.Translate(200, 0, MatrixOrder.Append);

            figureMatrix.Multiply(m, MatrixOrder.Append);
            Invalidate();
        }

        private void btnScale_Click(object sender, EventArgs e)
        {
            float cx = (originalPoints[0].X + originalPoints[1].X + originalPoints[2].X) / 3f;
            float cy = (originalPoints[0].Y + originalPoints[1].Y + originalPoints[2].Y) / 3f;

            Matrix m = new Matrix();
            m.Translate(-cx, -cy, MatrixOrder.Append);
            m.Scale(1.6f, 0.6f, MatrixOrder.Append);
            m.Translate(cx, cy, MatrixOrder.Append);

            figureMatrix.Multiply(m, MatrixOrder.Append);
            Invalidate();
        }

        // -----------------------------------------------------------------
        //  КНОПКИ – ДІЇ НА ТЕКСТ (те саме, але на textMatrix)
        // -----------------------------------------------------------------
        private void btnTextTranslate_Click(object sender, EventArgs e)
        {
            textMatrix.Translate(60, 40, MatrixOrder.Append);
            Invalidate();
        }

        private void btnTextRotate_Click(object sender, EventArgs e)
        {
            // центр тексту – центр його bounding‑box
            GraphicsPath p = new GraphicsPath();
            p.AddString(displayText, textFont.FontFamily,
                        (int)textFont.Style, textFont.Size,
                        textOrigin, StringFormat.GenericDefault);
            RectangleF rc = p.GetBounds();
            float cx = rc.Left + rc.Width / 2f;
            float cy = rc.Top + rc.Height / 2f;

            Matrix m = new Matrix();
            m.Translate(-cx, -cy, MatrixOrder.Append);
            m.Rotate(30, MatrixOrder.Append);
            m.Translate(cx, cy, MatrixOrder.Append);

            textMatrix.Multiply(m, MatrixOrder.Append);
            Invalidate();
        }

        private void btnTextReflect_Click(object sender, EventArgs e)
        {
            Matrix m = new Matrix();
            m.Translate(-200, 0, MatrixOrder.Append);
            m.Scale(-1, 1, MatrixOrder.Append);
            m.Translate(200, 0, MatrixOrder.Append);

            textMatrix.Multiply(m, MatrixOrder.Append);
            Invalidate();
        }

        private void btnTextScale_Click(object sender, EventArgs e)
        {
            // центр тексту
            GraphicsPath p = new GraphicsPath();
            p.AddString(displayText, textFont.FontFamily,
                        (int)textFont.Style, textFont.Size,
                        textOrigin, StringFormat.GenericDefault);
            RectangleF rc = p.GetBounds();
            float cx = rc.Left + rc.Width / 2f;
            float cy = rc.Top + rc.Height / 2f;

            Matrix m = new Matrix();
            m.Translate(-cx, -cy, MatrixOrder.Append);
            m.Scale(1.4f, 0.7f, MatrixOrder.Append);
            m.Translate(cx, cy, MatrixOrder.Append);

            textMatrix.Multiply(m, MatrixOrder.Append);
            Invalidate();
        }

        // -----------------------------------------------------------------
        //  СКИДАННЯ
        // -----------------------------------------------------------------
        private void btnReset_Click(object sender, EventArgs e)
        {
            figureMatrix.Reset();
            textMatrix.Reset();
            Invalidate();
        }
    }
}