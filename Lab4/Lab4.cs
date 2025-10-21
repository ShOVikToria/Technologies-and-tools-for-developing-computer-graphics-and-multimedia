using System;
using System.Drawing;
using System.Threading.Channels;
using System.Windows.Forms;

namespace Lab4
{
    public partial class Lab4 : Form
    {
        private Image ladybird;
        private Image butterfly;
        private PointF pointLadybird;
        private PointF pointButterfly;
        private PointF nextPointLadybird;
        private PointF nextPointButterfly;
        private bool showButterfly = false; // Прапорець для відображення другого зображення
        private int selectedImage = -1; // -1: жодне не вибрано, 0: ladybird, 1: butterfly
        private bool isMoving = false; // Прапорець для безперервного руху
        private double dxLadybird = 8.0, dyLadybird = 6.0; // Швидкість для ladybird
        private double dxButterfly = 6.0, dyButterfly = 8.0; // Швидкість для butterfly
        private bool showObstacles = false; // Прапорець для відображення перешкод
        private Rectangle[] obstacles; // Масив перешкод
        private System.Windows.Forms.Timer timer;
        private Random random = new Random();
        private const int imageSize = 80; // Розмір зображень
        private Image backgroundImage;
        private bool isDragging = false; // Прапорець для перетягування мишею
        private PointF dragOffset; // Зміщення для перетягування

        public Lab4()
        {
            InitializeComponent();
            this.DoubleBuffered = true; // Увімкнення подвійної буферизації
            backgroundImage = Image.FromFile("grass2.jpg");

            // Завантаження та масштабування зображень
            ladybird = Image.FromFile("ladybird.png");
            butterfly = Image.FromFile("butterfly.png");
            ladybird = new Bitmap(ladybird, new Size(imageSize, imageSize));
            butterfly = new Bitmap(butterfly, new Size(imageSize, imageSize));

            // Початкові позиції
            pointLadybird = new PointF(100, 100);
            pointButterfly = new PointF(200, 100);
            nextPointLadybird = pointLadybird;
            nextPointButterfly = pointButterfly;

            // Ініціалізація перешкод
            obstacles = new Rectangle[]
            {
                new Rectangle(500, 150, 300, 50),
                new Rectangle(400, 300, 50, 200),
                new Rectangle(100, 450, 150, 50)
            };

            // Налаштування таймера
            timer = new System.Windows.Forms.Timer();
            timer.Interval = 70;
            timer.Tick += Timer_Tick;

            // Події для вибору, перетягування та клавіатури
            this.MouseClick += Lab4_MouseClick;
            this.MouseDown += Lab4_MouseDown;
            this.MouseMove += Lab4_MouseMove;
            this.MouseUp += Lab4_MouseUp;
            this.KeyPreview = true; // Увімкнення обробки клавіатури на рівні форми

            // Вимкнення TabStop для всіх кнопок
            foreach (Control control in this.Controls)
            {
                if (control is Button)
                    control.TabStop = false;
            }

            // Встановлення фокусу на форму
            this.Focus();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            // Перемикання між зображеннями за допомогою Tab
            if (keyData == Keys.Tab)
            {
                if (selectedImage == -1)
                {
                    selectedImage = 0; // Вибрати ladybird, якщо нічого не вибрано
                }
                else if (selectedImage == 0)
                {
                    if (showButterfly)
                        selectedImage = 1; // Переключити на butterfly, якщо він увімкнений
                    else
                        selectedImage = -1; // Скинути вибір, якщо butterfly відключений
                }
                else if (selectedImage == 1 && showButterfly)
                {
                    selectedImage = 0; // Повернутися до ladybird
                }
                this.Invalidate(); // Перемалювати форму, щоб оновити позначення вибраного зображення
                this.Focus(); // Зберегти фокус на формі
                return true; // Подія оброблена
            }

            // Скидання вибору за допомогою Esc
            if (keyData == Keys.Escape)
            {
                selectedImage = -1; // Скинути вибір зображення
                this.Invalidate(); // Перемалювати форму, щоб прибрати синій контур
                this.Focus(); // Зберегти фокус на формі
                return true; // Подія оброблена
            }

            // Перехоплення стрілок для обробки формою
            if (keyData == Keys.Left || keyData == Keys.Right || keyData == Keys.Up || keyData == Keys.Down)
            {
                if (selectedImage == -1)
                {
                    MessageBox.Show("Треба обов'язково вибрати зображення!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return true;
                }

                // Вимкнення безперервного руху
                if (isMoving)
                {
                    isMoving = false;
                    timer.Stop();
                }

                float step = 5.0f;
                PointF newPosition = selectedImage == 0 ? pointLadybird : pointButterfly;

                switch (keyData)
                {
                    case Keys.Left:
                        newPosition.X -= step;
                        break;
                    case Keys.Right:
                        newPosition.X += step;
                        break;
                    case Keys.Up:
                        newPosition.Y -= step;
                        break;
                    case Keys.Down:
                        newPosition.Y += step;
                        break;
                }

                newPosition.X = Math.Max(0, Math.Min(newPosition.X, this.ClientSize.Width - imageSize));
                newPosition.Y = Math.Max(0, Math.Min(newPosition.Y, this.ClientSize.Height - imageSize));

                if (selectedImage == 0)
                {
                    pointLadybird = newPosition;
                    nextPointLadybird = newPosition;
                }
                else if (showButterfly && selectedImage == 1)
                {
                    pointButterfly = newPosition;
                    nextPointButterfly = newPosition;
                }

                this.Invalidate();
                this.Focus();
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void Lab4_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                // Визначення, чи клікнули на зображення
                Rectangle ladybirdRect = new Rectangle((int)pointLadybird.X, (int)pointLadybird.Y, imageSize, imageSize);
                if (ladybirdRect.Contains(e.Location))
                {
                    selectedImage = 0; // Вибрано ladybird
                }
                else if (showButterfly)
                {
                    Rectangle butterflyRect = new Rectangle((int)pointButterfly.X, (int)pointButterfly.Y, imageSize, imageSize);
                    if (butterflyRect.Contains(e.Location))
                    {
                        selectedImage = 1; // Вибрано butterfly
                    }
                    else
                    {
                        selectedImage = -1; // Нічого не вибрано
                    }
                }
                else
                {
                    selectedImage = -1; // Нічого не вибрано
                }
                this.Invalidate();
                this.Focus(); // Повернення фокусу на форму
            }
        }

        private void Lab4_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && selectedImage != -1)
            {
                PointF mousePoint = new PointF(e.X, e.Y);
                Rectangle selectedRect = selectedImage == 0
                    ? new Rectangle((int)pointLadybird.X, (int)pointLadybird.Y, imageSize, imageSize)
                    : new Rectangle((int)pointButterfly.X, (int)pointButterfly.Y, imageSize, imageSize);

                if (selectedRect.Contains(e.Location))
                {
                    isDragging = true;
                    dragOffset = new PointF(mousePoint.X - (selectedImage == 0 ? pointLadybird.X : pointButterfly.X),
                                            mousePoint.Y - (selectedImage == 0 ? pointLadybird.Y : pointButterfly.Y));

                    // Вимкнення безперервного руху при початку перетягування
                    if (isMoving)
                    {
                        isMoving = false;
                        timer.Stop();
                    }
                }
                this.Focus(); // Повернення фокусу на форму
            }
        }

        private void Lab4_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging && selectedImage != -1)
            {
                PointF newPosition = new PointF(e.X - dragOffset.X, e.Y - dragOffset.Y);

                // Обмеження позиції в межах форми
                newPosition.X = Math.Max(0, Math.Min(newPosition.X, this.ClientSize.Width - imageSize));
                newPosition.Y = Math.Max(0, Math.Min(newPosition.Y, this.ClientSize.Height - imageSize));

                if (selectedImage == 0)
                {
                    pointLadybird = newPosition;
                    nextPointLadybird = newPosition;
                }
                else if (showButterfly && selectedImage == 1)
                {
                    pointButterfly = newPosition;
                    nextPointButterfly = newPosition;
                }

                this.Invalidate();
            }
        }

        private void Lab4_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = false;
                this.Focus(); // Повернення фокусу на форму
            }
        }

        // Кнопка для додавання/видалення другого зображення
        public void ToggleButterflyButton_Click(object sender, EventArgs e)
        {
            showButterfly = !showButterfly;
            if (!showButterfly)
                selectedImage = -1; // Скидаємо вибір, якщо butterfly прибрано
            this.Invalidate();
            this.Focus(); // Повернення фокусу на форму
        }

        // Кнопка для переміщення зображення
        public void MoveButton_Click(object sender, EventArgs e)
        {
            // Якщо вибрано зображення, переміщуємо лише його
            if (selectedImage == 0)
            {
                pointLadybird = new PointF(random.Next(0, this.ClientSize.Width - imageSize),
                                          random.Next(0, this.ClientSize.Height - imageSize));
                nextPointLadybird = pointLadybird;
            }
            else if (showButterfly && selectedImage == 1)
            {
                pointButterfly = new PointF(random.Next(0, this.ClientSize.Width - imageSize),
                                           random.Next(0, this.ClientSize.Height - imageSize));
                nextPointButterfly = pointButterfly;
            }
            else // Якщо нічого не вибрано, переміщуємо обидва зображення
            {
                pointLadybird = new PointF(random.Next(0, this.ClientSize.Width - imageSize),
                                          random.Next(0, this.ClientSize.Height - imageSize));
                nextPointLadybird = pointLadybird;
                if (showButterfly)
                {
                    pointButterfly = new PointF(random.Next(0, this.ClientSize.Width - imageSize),
                                               random.Next(0, this.ClientSize.Height - imageSize));
                    nextPointButterfly = pointButterfly;
                }
            }
            // Вимкнення безперервного руху при натисканні кнопки
            if (isMoving)
            {
                isMoving = false;
                timer.Stop();
            }
            this.Invalidate();
            this.Focus(); // Повернення фокусу на форму
        }

        // Кнопка для увімкнення/вимкнення руху
        public void ToggleMoveButton_Click(object sender, EventArgs e)
        {
            isMoving = !isMoving;
            if (isMoving)
                timer.Start();
            else
                timer.Stop();
            this.Invalidate();
            this.Focus(); // Повернення фокусу на форму
        }

        // Кнопка для увімкнення/вимкнення перешкод
        public void ToggleObstaclesButton_Click(object sender, EventArgs e)
        {
            showObstacles = !showObstacles;
            this.Invalidate();
            this.Focus(); // Повернення фокусу на форму
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            // Якщо вибрано зображення, рухаємо лише його
            if (selectedImage == 0)
            {
                pointLadybird = nextPointLadybird;
                nextPointLadybird = new PointF((float)(pointLadybird.X + dxLadybird), (float)(pointLadybird.Y + dyLadybird));

                // Перевірка меж форми з додаванням хаосу
                if (nextPointLadybird.X <= 0 || nextPointLadybird.X + imageSize >= this.ClientSize.Width)
                {
                    dxLadybird = -dxLadybird;
                }
                if (nextPointLadybird.Y <= 0 || nextPointLadybird.Y + imageSize >= this.ClientSize.Height)
                {
                    dyLadybird = -dyLadybird;
                }

                // Перевірка зіткнень з перешкодами 
                if (showObstacles)
                {
                    Rectangle ladybirdRect = new Rectangle((int)nextPointLadybird.X, (int)nextPointLadybird.Y, imageSize, imageSize);
                    foreach (var obstacle in obstacles)
                    {
                        if (ladybirdRect.IntersectsWith(obstacle))
                        {
                            float overlapLeft = ladybirdRect.Right - obstacle.Left;
                            float overlapRight = obstacle.Right - ladybirdRect.Left;
                            float overlapTop = ladybirdRect.Bottom - obstacle.Top;
                            float overlapBottom = obstacle.Bottom - ladybirdRect.Top;

                            float minX = Math.Min(overlapLeft, overlapRight);
                            float minY = Math.Min(overlapTop, overlapBottom);

                            if (minX < minY)
                            {
                                dxLadybird = -dxLadybird; // Відбиття по X
                            }
                            else
                            {
                                dyLadybird = -dyLadybird; // Відбиття по Y
                            }

                            nextPointLadybird = new PointF((float)(pointLadybird.X + dxLadybird), (float)(pointLadybird.Y + dyLadybird));
                        }
                    }
                }
            }
            else if (showButterfly && selectedImage == 1)
            {
                pointButterfly = nextPointButterfly;
                nextPointButterfly = new PointF((float)(pointButterfly.X + dxButterfly), (float)(pointButterfly.Y + dyButterfly));

                // Перевірка меж форми
                if (nextPointButterfly.X <= 0 || nextPointButterfly.X + imageSize >= this.ClientSize.Width)
                {
                    dxButterfly = -dxButterfly;
                }
                if (nextPointButterfly.Y <= 0 || nextPointButterfly.Y + imageSize >= this.ClientSize.Height)
                {
                    dyButterfly = -dyButterfly;
                }

                // Перевірка зіткнень з перешкодами
                if (showObstacles)
                {
                    Rectangle butterflyRect = new Rectangle((int)nextPointButterfly.X, (int)nextPointButterfly.Y, imageSize, imageSize);
                    foreach (var obstacle in obstacles)
                    {
                        if (butterflyRect.IntersectsWith(obstacle))
                        {
                            float overlapLeft = butterflyRect.Right - obstacle.Left;
                            float overlapRight = obstacle.Right - butterflyRect.Left;
                            float overlapTop = butterflyRect.Bottom - obstacle.Top;
                            float overlapBottom = obstacle.Bottom - butterflyRect.Top;

                            float minX = Math.Min(overlapLeft, overlapRight);
                            float minY = Math.Min(overlapTop, overlapBottom);

                            if (minX < minY)
                            {
                                dxButterfly = -dxButterfly; // Відбиття по X
                            }
                            else
                            {
                                dyButterfly = -dyButterfly; // Відбиття по Y
                            }

                            nextPointButterfly = new PointF((float)(pointButterfly.X + dxButterfly), (float)(pointButterfly.Y + dyButterfly));
                        }
                    }
                }
            }
            else // Якщо нічого не вибрано, рухаємо обидва зображення
            {
                // Рух ladybird
                pointLadybird = nextPointLadybird;
                nextPointLadybird = new PointF((float)(pointLadybird.X - dxLadybird), (float)(pointLadybird.Y + dyLadybird));
                if (nextPointLadybird.X <= 0 || nextPointLadybird.X + imageSize >= this.ClientSize.Width)
                {
                    dxLadybird = -dxLadybird;
                }
                if (nextPointLadybird.Y <= 0 || nextPointLadybird.Y + imageSize >= this.ClientSize.Height)
                {
                    dyLadybird = -dyLadybird;
                }

                if (showObstacles)
                {
                    Rectangle ladybirdRect = new Rectangle((int)nextPointLadybird.X, (int)nextPointLadybird.Y, imageSize, imageSize);
                    foreach (var obstacle in obstacles)
                    {
                        if (ladybirdRect.IntersectsWith(obstacle))
                        {
                            float overlapLeft = ladybirdRect.Right - obstacle.Left;
                            float overlapRight = obstacle.Right - ladybirdRect.Left;
                            float overlapTop = ladybirdRect.Bottom - obstacle.Top;
                            float overlapBottom = obstacle.Bottom - ladybirdRect.Top;

                            float minX = Math.Min(overlapLeft, overlapRight);
                            float minY = Math.Min(overlapTop, overlapBottom);

                            if (minX < minY)
                            {
                                dxLadybird = -dxLadybird;
                            }
                            else
                            {
                                dyLadybird = -dyLadybird;
                            }

                            nextPointLadybird = new PointF((float)(pointLadybird.X + dxLadybird), (float)(pointLadybird.Y + dyLadybird));
                        }
                    }
                }

                // Рух butterfly, якщо увімкнено
                if (showButterfly)
                {
                    pointButterfly = nextPointButterfly;
                    nextPointButterfly = new PointF((float)(pointButterfly.X - dxButterfly), (float)(pointButterfly.Y + dyButterfly));
                    if (nextPointButterfly.X <= 0 || nextPointButterfly.X + imageSize >= this.ClientSize.Width)
                    {
                        dxButterfly = -dxButterfly;
                    }
                    if (nextPointButterfly.Y <= 0 || nextPointButterfly.Y + imageSize >= this.ClientSize.Height)
                    {
                        dyButterfly = -dyButterfly;
                    }

                    if (showObstacles)
                    {
                        Rectangle butterflyRect = new Rectangle((int)nextPointButterfly.X, (int)nextPointButterfly.Y, imageSize, imageSize);
                        foreach (var obstacle in obstacles)
                        {
                            if (butterflyRect.IntersectsWith(obstacle))
                            {
                                float overlapLeft = butterflyRect.Right - obstacle.Left;
                                float overlapRight = obstacle.Right - butterflyRect.Left;
                                float overlapTop = butterflyRect.Bottom - obstacle.Top;
                                float overlapBottom = obstacle.Bottom - butterflyRect.Top;

                                float minX = Math.Min(overlapLeft, overlapRight);
                                float minY = Math.Min(overlapTop, overlapBottom);

                                if (minX < minY)
                                {
                                    dxButterfly = -dxButterfly;
                                }
                                else
                                {
                                    dyButterfly = -dyButterfly;

                                    nextPointButterfly = new PointF((float)(pointButterfly.X + dxButterfly), (float)(pointButterfly.Y + dyButterfly));
                                }
                            }
                        }
                    }
                }
                this.Invalidate();
            }
        }

        private void Lab4_Paint(object sender, PaintEventArgs e)
        {
            // Малювання фонового зображення
            if (backgroundImage != null)
            {
                e.Graphics.DrawImage(backgroundImage, 0, 0, this.ClientSize.Width, this.ClientSize.Height);
            }
            else
            {
                e.Graphics.Clear(this.BackColor);
            }

            // Малювання першого зображення
            e.Graphics.DrawImage(ladybird, pointLadybird);

            // Малювання другого зображення, якщо увімкнено
            if (showButterfly)
                e.Graphics.DrawImage(butterfly, pointButterfly);

            // Малювання перешкод, якщо увімкнено
            if (showObstacles)
            {
                foreach (var obstacle in obstacles)
                    e.Graphics.FillRectangle(Brushes.Red, obstacle);
            }

            // Позначення вибраного зображення
            if (selectedImage == 0)
                e.Graphics.DrawRectangle(new Pen(Color.Blue, 2), pointLadybird.X, pointLadybird.Y, imageSize, imageSize);
            else if (showButterfly && selectedImage == 1)
                e.Graphics.DrawRectangle(new Pen(Color.Blue, 2), pointButterfly.X, pointButterfly.Y, imageSize, imageSize);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            ladybird?.Dispose();
            butterfly?.Dispose();
            backgroundImage?.Dispose();
        }
    }
}