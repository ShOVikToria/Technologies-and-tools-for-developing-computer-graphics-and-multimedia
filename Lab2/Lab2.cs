using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab2
{
    public partial class Lab2 : Form
    {
        public Lab2()
        {
            InitializeComponent();
        }

        private async void buttonSquare_Click(object sender, EventArgs e)
        {
            Graphics g = CreateGraphics();

            g.Clear(Color.White);

            Pen pen = new Pen(Color.DodgerBlue, 2);

            float startX = 160;
            float startY = 25;

            float size = 500;
            int count = 50;
            float ratio = 0.92f;

            PointF[] square = GetSquare(startX, startY, size);

            for (int i = 0; i < count; i++)
            {
                g.DrawPolygon(pen, square);

                square = GetNextSquare(square, ratio);

                // затримка 100 мс, щоб видно було малювання
                await Task.Delay(50);
            }

            pen.Dispose();
        }


        // Повертає вершини квадрата
        private PointF[] GetSquare (float x, float y, float size)
        {
            return new PointF[]
            {
                new PointF(x, y),
                new PointF(x+ size, y),
                new PointF(x+size, y+ size),
                new PointF(x, y+ size)
            };
        }

        // Шукає вершини наступного квадрата
        private PointF[] GetNextSquare(PointF[] sq, float r)
        {
            PointF[] newSquare = new PointF[4];

            for(int i = 0; i < 4; i++)
            {
                PointF cur = sq[i];
                PointF next = sq[(i+1)%4];

                float newX = cur.X + r * (next.X - cur.X);
                float newY = cur.Y + r * (next.Y - cur.Y);

                newSquare[i] = new PointF(newX, newY);
            }

            return newSquare;
        }

        private async void buttonTriangle_Click(object sender, EventArgs e)
        {
            Graphics g = CreateGraphics();

            g.Clear(Color.White);

            Pen pen = new Pen(Color.Chocolate, 2);

            PointF top = new PointF(410, 25);
            PointF left = new PointF(160, 525);
            PointF right = new PointF(660, 525);

            await DrawSirpTriangle(g, pen, top, left, right, 7);

            pen.Dispose();
        }


        private async Task DrawSirpTriangle(Graphics g, Pen p, PointF t, PointF l, PointF r, int level)
        {
            if (level == 0)
            {
                g.DrawLine(p, t, l);
                g.DrawLine(p, l, r);
                g.DrawLine(p, r, t);

                await Task.Delay(80); // пауза, щоб побачити процес малювання
            }
            else
            {
                PointF leftMid = MiddlePoint(t, l);
                PointF rightMid = MiddlePoint(t, r);
                PointF bottomMid = MiddlePoint(l, r);

                await DrawSirpTriangle(g, p, t, leftMid, rightMid, level - 1);
                await DrawSirpTriangle(g, p, leftMid, l, bottomMid, level - 1);
                await DrawSirpTriangle(g, p, rightMid, bottomMid, r, level - 1);
            }
        }

        private PointF MiddlePoint(PointF p1, PointF p2)
        {
            return new PointF((p1.X + p2.X) / 2, (p1.Y + p2.Y) / 2);
        }

    }
}