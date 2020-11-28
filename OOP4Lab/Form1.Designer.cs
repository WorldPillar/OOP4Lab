namespace OOP4Lab
{
    partial class PaintBox
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.drawBox = new System.Windows.Forms.PictureBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.оПрограммеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.сВызовColorDialogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.увеличениеФигурыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.уменьшениФигурыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.стрелкиДвижениеФигурыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.chooseShape = new System.Windows.Forms.ToolStripMenuItem();
            this.circleMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.triangleMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.squareMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.fiveMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.sixMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.starMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.createMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.groupMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.MakeGroup = new System.Windows.Forms.ToolStripMenuItem();
            this.DeleteGroupMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.SaveMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.LoadMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.chooseColor = new System.Windows.Forms.ColorDialog();
            ((System.ComponentModel.ISupportInitialize)(this.drawBox)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // drawBox
            // 
            this.drawBox.BackColor = System.Drawing.Color.White;
            this.drawBox.Location = new System.Drawing.Point(0, 27);
            this.drawBox.Name = "drawBox";
            this.drawBox.Size = new System.Drawing.Size(1184, 523);
            this.drawBox.TabIndex = 0;
            this.drawBox.TabStop = false;
            this.drawBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.drawBoxClick);
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.MediumTurquoise;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.оПрограммеToolStripMenuItem,
            this.chooseShape,
            this.createMenu,
            this.groupMenu,
            this.MakeGroup,
            this.DeleteGroupMenu,
            this.SaveMenu,
            this.LoadMenu});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1184, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // оПрограммеToolStripMenuItem
            // 
            this.оПрограммеToolStripMenuItem.BackColor = System.Drawing.Color.MediumTurquoise;
            this.оПрограммеToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.сВызовColorDialogToolStripMenuItem,
            this.увеличениеФигурыToolStripMenuItem,
            this.уменьшениФигурыToolStripMenuItem,
            this.стрелкиДвижениеФигурыToolStripMenuItem});
            this.оПрограммеToolStripMenuItem.Name = "оПрограммеToolStripMenuItem";
            this.оПрограммеToolStripMenuItem.Size = new System.Drawing.Size(94, 20);
            this.оПрограммеToolStripMenuItem.Text = "О программе";
            // 
            // сВызовColorDialogToolStripMenuItem
            // 
            this.сВызовColorDialogToolStripMenuItem.Name = "сВызовColorDialogToolStripMenuItem";
            this.сВызовColorDialogToolStripMenuItem.Size = new System.Drawing.Size(231, 22);
            this.сВызовColorDialogToolStripMenuItem.Text = "\'С\' - вызов color dialog";
            // 
            // увеличениеФигурыToolStripMenuItem
            // 
            this.увеличениеФигурыToolStripMenuItem.Name = "увеличениеФигурыToolStripMenuItem";
            this.увеличениеФигурыToolStripMenuItem.Size = new System.Drawing.Size(231, 22);
            this.увеличениеФигурыToolStripMenuItem.Text = "\'+\' - увеличение фигуры";
            // 
            // уменьшениФигурыToolStripMenuItem
            // 
            this.уменьшениФигурыToolStripMenuItem.Name = "уменьшениФигурыToolStripMenuItem";
            this.уменьшениФигурыToolStripMenuItem.Size = new System.Drawing.Size(231, 22);
            this.уменьшениФигурыToolStripMenuItem.Text = "\'-\' - уменьшени фигуры";
            // 
            // стрелкиДвижениеФигурыToolStripMenuItem
            // 
            this.стрелкиДвижениеФигурыToolStripMenuItem.Name = "стрелкиДвижениеФигурыToolStripMenuItem";
            this.стрелкиДвижениеФигурыToolStripMenuItem.Size = new System.Drawing.Size(231, 22);
            this.стрелкиДвижениеФигурыToolStripMenuItem.Text = "Стрелки - движение фигуры";
            // 
            // chooseShape
            // 
            this.chooseShape.BackColor = System.Drawing.Color.MediumTurquoise;
            this.chooseShape.CheckOnClick = true;
            this.chooseShape.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.circleMenu,
            this.triangleMenu,
            this.squareMenu,
            this.fiveMenu,
            this.sixMenu,
            this.starMenu});
            this.chooseShape.Name = "chooseShape";
            this.chooseShape.Size = new System.Drawing.Size(109, 20);
            this.chooseShape.Text = "Выбрать фигуру";
            // 
            // circleMenu
            // 
            this.circleMenu.Checked = true;
            this.circleMenu.CheckOnClick = true;
            this.circleMenu.CheckState = System.Windows.Forms.CheckState.Checked;
            this.circleMenu.Name = "circleMenu";
            this.circleMenu.Size = new System.Drawing.Size(160, 22);
            this.circleMenu.Text = "Круг";
            this.circleMenu.Click += new System.EventHandler(this.ToolStripMenueItem_Click);
            // 
            // triangleMenu
            // 
            this.triangleMenu.CheckOnClick = true;
            this.triangleMenu.Name = "triangleMenu";
            this.triangleMenu.Size = new System.Drawing.Size(160, 22);
            this.triangleMenu.Text = "Треугольник";
            this.triangleMenu.Click += new System.EventHandler(this.ToolStripMenueItem_Click);
            // 
            // squareMenu
            // 
            this.squareMenu.CheckOnClick = true;
            this.squareMenu.Name = "squareMenu";
            this.squareMenu.Size = new System.Drawing.Size(160, 22);
            this.squareMenu.Text = "Квадрат";
            this.squareMenu.Click += new System.EventHandler(this.ToolStripMenueItem_Click);
            // 
            // fiveMenu
            // 
            this.fiveMenu.CheckOnClick = true;
            this.fiveMenu.Name = "fiveMenu";
            this.fiveMenu.Size = new System.Drawing.Size(160, 22);
            this.fiveMenu.Text = "Пятиугольник";
            this.fiveMenu.Click += new System.EventHandler(this.ToolStripMenueItem_Click);
            // 
            // sixMenu
            // 
            this.sixMenu.CheckOnClick = true;
            this.sixMenu.Name = "sixMenu";
            this.sixMenu.Size = new System.Drawing.Size(160, 22);
            this.sixMenu.Text = "Шестиугольник";
            this.sixMenu.Click += new System.EventHandler(this.ToolStripMenueItem_Click);
            // 
            // starMenu
            // 
            this.starMenu.CheckOnClick = true;
            this.starMenu.Name = "starMenu";
            this.starMenu.Size = new System.Drawing.Size(160, 22);
            this.starMenu.Text = "Звезда";
            this.starMenu.Click += new System.EventHandler(this.ToolStripMenueItem_Click);
            // 
            // createMenu
            // 
            this.createMenu.BackColor = System.Drawing.Color.MediumTurquoise;
            this.createMenu.Name = "createMenu";
            this.createMenu.Size = new System.Drawing.Size(105, 20);
            this.createMenu.Text = "Создать фигуру";
            this.createMenu.Click += new System.EventHandler(this.ActiveActionChange);
            // 
            // groupMenu
            // 
            this.groupMenu.BackColor = System.Drawing.Color.MediumTurquoise;
            this.groupMenu.Name = "groupMenu";
            this.groupMenu.Size = new System.Drawing.Size(133, 20);
            this.groupMenu.Text = "Выделить несколько";
            this.groupMenu.Click += new System.EventHandler(this.ActiveActionChange);
            // 
            // MakeGroup
            // 
            this.MakeGroup.BackColor = System.Drawing.Color.MediumTurquoise;
            this.MakeGroup.Name = "MakeGroup";
            this.MakeGroup.Size = new System.Drawing.Size(103, 20);
            this.MakeGroup.Text = "Сгруппировать";
            this.MakeGroup.Visible = false;
            this.MakeGroup.Click += new System.EventHandler(this.MakeGroup_Click);
            // 
            // DeleteGroupMenu
            // 
            this.DeleteGroupMenu.BackColor = System.Drawing.Color.MediumTurquoise;
            this.DeleteGroupMenu.Name = "DeleteGroupMenu";
            this.DeleteGroupMenu.Size = new System.Drawing.Size(159, 20);
            this.DeleteGroupMenu.Text = "Разгруппировать фигуры";
            this.DeleteGroupMenu.Click += new System.EventHandler(this.DeleteGroupMenu_Click);
            // 
            // SaveMenu
            // 
            this.SaveMenu.BackColor = System.Drawing.Color.MediumTurquoise;
            this.SaveMenu.Name = "SaveMenu";
            this.SaveMenu.Size = new System.Drawing.Size(128, 20);
            this.SaveMenu.Text = "Сохранить объекты";
            this.SaveMenu.Click += new System.EventHandler(this.SaveMenu_Click);
            // 
            // LoadMenu
            // 
            this.LoadMenu.BackColor = System.Drawing.Color.MediumTurquoise;
            this.LoadMenu.Name = "LoadMenu";
            this.LoadMenu.Size = new System.Drawing.Size(123, 20);
            this.LoadMenu.Text = "Загрузить объекты";
            this.LoadMenu.Click += new System.EventHandler(this.LoadMenu_Click);
            // 
            // chooseColor
            // 
            this.chooseColor.AnyColor = true;
            this.chooseColor.SolidColorOnly = true;
            // 
            // PaintBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1184, 550);
            this.Controls.Add(this.drawBox);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "PaintBox";
            this.Text = "PaintBox";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Paint_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.drawBox)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox drawBox;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ColorDialog chooseColor;
        private System.Windows.Forms.ToolStripMenuItem оПрограммеToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem сВызовColorDialogToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem увеличениеФигурыToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem уменьшениФигурыToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem стрелкиДвижениеФигурыToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem chooseShape;
        private System.Windows.Forms.ToolStripMenuItem circleMenu;
        private System.Windows.Forms.ToolStripMenuItem squareMenu;
        private System.Windows.Forms.ToolStripMenuItem triangleMenu;
        private System.Windows.Forms.ToolStripMenuItem fiveMenu;
        private System.Windows.Forms.ToolStripMenuItem sixMenu;
        private System.Windows.Forms.ToolStripMenuItem starMenu;
        private System.Windows.Forms.ToolStripMenuItem groupMenu;
        private System.Windows.Forms.ToolStripMenuItem DeleteGroupMenu;
        private System.Windows.Forms.ToolStripMenuItem MakeGroup;
        private System.Windows.Forms.ToolStripMenuItem createMenu;
        private System.Windows.Forms.ToolStripMenuItem SaveMenu;
        private System.Windows.Forms.ToolStripMenuItem LoadMenu;
    }
}

