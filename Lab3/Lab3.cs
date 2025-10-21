using System;            
using System.Drawing;    
using System.Windows.Forms;

namespace Lab3
{
    public partial class Lab3 : Form
    {
        private const float size = 500f;
        private const float OffsetX = 250f;
        private const float OffsetY = 90f;

        public Lab3()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int depth;

            bool parsed = int.TryParse(textBox1.Text, out depth);
            if (!parsed)
            {
                MessageBox.Show("Будь ласка, введіть правильне число для глибини.");
                return;
            }

            if (depth > 10)
            {
                MessageBox.Show("Будь ласка, введіть число не більше 10.");
                return;
            }

            Graphics g = CreateGraphics();
            Pen pen = new Pen(Color.Blue, 2);
            // Pen pen1 = new Pen(Color.Red, 2);
            g.Clear(Color.WhiteSmoke);

            // DrawKochSnowflake(g, pen1, depth-1);
            DrawKochSnowflake(g, pen, depth);
        }

        private void DrawKochSnowflake(Graphics g, Pen pen, int depth)
        {
            // Висота рівностороннього трикутника
            float h = size * (float)Math.Sqrt(3) / 2f;

            // Вершини рівностороннього трикутника
            PointF v1 = new PointF(OffsetX + size / 2f, OffsetY);   // Верхня вершина
            PointF v2 = new PointF(OffsetX, OffsetY + h);           // Ліва нижня вершина
            PointF v3 = new PointF(OffsetX + size, OffsetY + h);    // Права нижня вершина

            // Малюємо три сторони трикутника, передаючи флаг flip для напрямку піків
            DrawKochSegment(g, pen, v1, v2, depth);
            DrawKochSegment(g, pen, v2, v3, depth);
            DrawKochSegment(g, pen, v3, v1, depth);
        }

        // Рекурсивний метод для малювання одного відрізка Коха
        // a, b — початкова та кінцева точки відрізка
        // depth — рівень рекурсії
        private void DrawKochSegment(Graphics g, Pen pen, PointF a, PointF b, int depth)
        {
            if (depth <= 0)
            {
                g.DrawLine(pen, a, b);
                return;
            }

            // Знаходимо точки на третинах відрізка
            PointF p1 = Lerp(a, b, 1f / 3f);
            PointF p2 = Lerp(a, b, 2f / 3f);

            // Обчислюємо вершину піка сніжинки
            PointF peak = GetPeak(p1, p2);

            // Рекурсивно малюємо 4 частини нового відрізка
            DrawKochSegment(g, pen, a, p1, depth - 1); // перша тритина
            DrawKochSegment(g, pen, p1, peak, depth - 1); // лівий бік піку
            DrawKochSegment(g, pen, peak, p2, depth - 1); // правий бік піку
            DrawKochSegment(g, pen, p2, b, depth - 1); // остання третина
        }

        private PointF Lerp(PointF a, PointF b, float t)
        {
            float x = a.X + (b.X - a.X) * t;
            float y = a.Y + (b.Y - a.Y) * t;
            return new PointF(x, y);
        }

        // Обчислення вершини піка сніжинки
        private PointF GetPeak(PointF p1, PointF p2)
        {
            // Кут повороту для піка 60°
            double angle = Math.PI / 3.0;

            // Вектор від p1 до p2
            float vx = p2.X - p1.X;
            float vy = p2.Y - p1.Y;

            // Поворот вектора на кут angle
            float x = (float)(vx * Math.Cos(angle) - vy * Math.Sin(angle));
            float y = (float)(vx * Math.Sin(angle) + vy * Math.Cos(angle));

            // Додаємо поворотний вектор до точки p1 → отримуємо вершину піка
            return new PointF(p1.X + x, p1.Y + y);
        }
    }
}