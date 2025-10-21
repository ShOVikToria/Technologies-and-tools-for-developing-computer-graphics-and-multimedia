using System;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Lab1 : Form
    {
        Point StartSh = new Point(180, 320);
        Point StartE, StartV, StartCh, StartU, StartK;
        int widthBig = 64, heightBig = 75, width = 40, height = 50, interval = 20, widthSmall = 30;

        public Lab1()
        {
            InitializeComponent();
        }

        Pen penBlue = new Pen(Color.DeepSkyBlue, 5);

        private void Lab1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = CreateGraphics();

            // Використання Brush
            // Розрахунок координат фону
            int textWidth = widthBig + 5 * width + 4 * interval;
            int textHeight = heightBig + 20;
            int backgroundX = StartSh.X - 25;
            int backgroundY = StartSh.Y - 15;

            Brush brushBackground = new SolidBrush(Color.Moccasin);
            g.FillEllipse(brushBackground, backgroundX, backgroundY, textWidth + 40, textHeight + 20);

            // Використання Pen
            // Ш
            g.DrawLine(penBlue, StartSh, new Point(StartSh.X, StartSh.Y + 75));
            g.DrawLine(penBlue, new Point(StartSh.X, StartSh.Y + heightBig), new Point(StartSh.X + widthBig, StartSh.Y + heightBig));
            g.DrawLine(penBlue, new Point(StartSh.X + widthBig, StartSh.Y), new Point(StartSh.X + widthBig, StartSh.Y + heightBig));
            g.DrawLine(penBlue, new Point(StartSh.X + widthBig / 2, StartSh.Y), new Point(StartSh.X + widthBig / 2, StartSh.Y + heightBig));

            // е
            StartE.X = StartSh.X + widthBig + interval;
            StartE.Y = StartSh.Y + heightBig;

            g.DrawLine(penBlue, StartE.X, StartE.Y - 25, StartE.X + 40, StartE.Y - 25);
            g.DrawArc(penBlue, StartE.X, StartE.Y - 50, width, height, 50, 318);
            //Rectangle rect = new Rectangle(StartE.X, StartE.Y - 50, 40, 50);
            //g.DrawRectangle(Pens.Red, rect); // допоміжний контур
            //g.DrawArc(penBlue, rect, 60, 300);

            // в
            StartV.X = StartE.X + width + interval;
            StartV.Y = StartE.Y;

            g.DrawLine(penBlue, StartV.X, StartV.Y, StartV.X, StartV.Y - heightBig);
            g.DrawArc(penBlue, StartV.X - 10, StartV.Y - 30, width + 5, 30, 245, 250); // нижня дуга
            //Rectangle rect = new Rectangle(StartV.X - 10, StartV.Y - 30, width, 30);
            //g.DrawRectangle(Pens.Red, rect); // допоміжний контур
            //g.DrawArc(penBlue, rect, 240, 250);

            g.DrawArc(penBlue, StartV.X - 10, StartV.Y - heightBig , width - 10, heightBig - 30, 255, 210); // верхня дуга
            //Rectangle rect = new Rectangle(StartV.X - 10, StartV.Y - heightBig , width - 10, heightBig - 30);
            //g.DrawRectangle(Pens.Red, rect); // допоміжний контур
            //g.DrawArc(penBlue, rect, 250, 220);

            // ч
            StartCh.X = StartV.X + widthSmall + interval;
            StartCh.Y = StartE.Y;

            g.DrawLine(penBlue, StartCh.X + widthSmall, StartCh.Y, StartCh.X + widthSmall, StartCh.Y - height);
            g.DrawArc(penBlue, StartCh.X, StartV.Y - (height + height/2), height, height, 77, 105);
            //Rectangle rect = new Rectangle(StartCh.X, StartV.Y - 75, height, height);
            //g.DrawRectangle(Pens.Red, rect); // допоміжний контур
            //g.DrawArc(penBlue, rect, 77, 105);

            // y

            StartU.X = StartCh.X + widthSmall + interval;
            StartU.Y = StartCh.Y;

            g.DrawLine(penBlue, StartU.X + width, StartU.Y - height, StartU.X + width/3, StartU.Y + heightBig - height);
            g.DrawLine(penBlue, StartU.X, StartU.Y - height, StartU.X + width / 2, StartU.Y );

            // к

            StartK.X = StartU.X + width + interval;
            StartK.Y = StartU.Y;

            g.DrawLine(penBlue, StartK.X, StartK.Y, StartK.X, StartK.Y - height);
            g.DrawLine(penBlue, StartK.X, StartK.Y - height / 2, StartK.X + widthSmall, StartK.Y);
            g.DrawLine(penBlue, StartK.X, StartK.Y - height / 2, StartK.X + widthSmall, StartK.Y - height);

        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}