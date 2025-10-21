namespace Lab4
{
    partial class Lab4
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Lab4));
            ToggleButterflyButton = new Button();
            MoveButton = new Button();
            ToggleMoveButton = new Button();
            ToggleObstaclesButton = new Button();
            SuspendLayout();
            // 
            // ToggleButterflyButton
            // 
            ToggleButterflyButton.Location = new Point(30, 621);
            ToggleButterflyButton.Name = "ToggleButterflyButton";
            ToggleButterflyButton.Size = new Size(110, 48);
            ToggleButterflyButton.TabIndex = 0;
            ToggleButterflyButton.Text = "Додати/забрати друге зображення";
            ToggleButterflyButton.UseVisualStyleBackColor = true;
            ToggleButterflyButton.Click += ToggleButterflyButton_Click;
            // 
            // MoveButton
            // 
            MoveButton.Location = new Point(146, 621);
            MoveButton.Name = "MoveButton";
            MoveButton.Size = new Size(106, 48);
            MoveButton.TabIndex = 1;
            MoveButton.Text = "Перемістити зображення";
            MoveButton.UseVisualStyleBackColor = true;
            MoveButton.Click += MoveButton_Click;
            // 
            // ToggleMoveButton
            // 
            ToggleMoveButton.Location = new Point(258, 621);
            ToggleMoveButton.Name = "ToggleMoveButton";
            ToggleMoveButton.Size = new Size(100, 48);
            ToggleMoveButton.TabIndex = 2;
            ToggleMoveButton.Text = "Безперервний рух";
            ToggleMoveButton.UseVisualStyleBackColor = true;
            ToggleMoveButton.Click += ToggleMoveButton_Click;
            // 
            // ToggleObstaclesButton
            // 
            ToggleObstaclesButton.Location = new Point(366, 620);
            ToggleObstaclesButton.Name = "ToggleObstaclesButton";
            ToggleObstaclesButton.Size = new Size(128, 49);
            ToggleObstaclesButton.TabIndex = 3;
            ToggleObstaclesButton.Text = "Увімкнути/вимкнути перешкоди";
            ToggleObstaclesButton.UseVisualStyleBackColor = true;
            ToggleObstaclesButton.Click += ToggleObstaclesButton_Click;
            // 
            // Lab4
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(944, 681);
            Controls.Add(ToggleObstaclesButton);
            Controls.Add(ToggleMoveButton);
            Controls.Add(MoveButton);
            Controls.Add(ToggleButterflyButton);
            Name = "Lab4";
            Text = "Lab4";
            Paint += Lab4_Paint;
            ResumeLayout(false);
        }

        #endregion

        private Button ToggleButterflyButton;
        private Button MoveButton;
        private Button ToggleMoveButton;
        private Button ToggleObstaclesButton;
    }
}
