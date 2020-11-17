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
            this.выберитеФигуруToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.circleMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.squareMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.треугольникToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.отрезокToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.polygonMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.chooseColor = new System.Windows.Forms.ColorDialog();
            ((System.ComponentModel.ISupportInitialize)(this.drawBox)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // drawBox
            // 
            this.drawBox.Location = new System.Drawing.Point(0, 27);
            this.drawBox.Name = "drawBox";
            this.drawBox.Size = new System.Drawing.Size(1184, 523);
            this.drawBox.TabIndex = 0;
            this.drawBox.TabStop = false;
            this.drawBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ShapeCreate);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.оПрограммеToolStripMenuItem,
            this.выберитеФигуруToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1184, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // оПрограммеToolStripMenuItem
            // 
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
            // выберитеФигуруToolStripMenuItem
            // 
            this.выберитеФигуруToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.circleMenu,
            this.squareMenu,
            this.треугольникToolStripMenuItem,
            this.отрезокToolStripMenuItem,
            this.polygonMenu});
            this.выберитеФигуруToolStripMenuItem.Name = "выберитеФигуруToolStripMenuItem";
            this.выберитеФигуруToolStripMenuItem.Size = new System.Drawing.Size(116, 20);
            this.выберитеФигуруToolStripMenuItem.Text = "Выберите фигуру";
            // 
            // circleMenu
            // 
            this.circleMenu.Checked = true;
            this.circleMenu.CheckOnClick = true;
            this.circleMenu.CheckState = System.Windows.Forms.CheckState.Checked;
            this.circleMenu.Name = "circleMenu";
            this.circleMenu.Size = new System.Drawing.Size(234, 22);
            this.circleMenu.Text = "Круг";
            // 
            // squareMenu
            // 
            this.squareMenu.CheckOnClick = true;
            this.squareMenu.Name = "squareMenu";
            this.squareMenu.Size = new System.Drawing.Size(234, 22);
            this.squareMenu.Text = "Квадрат";
            // 
            // треугольникToolStripMenuItem
            // 
            this.треугольникToolStripMenuItem.CheckOnClick = true;
            this.треугольникToolStripMenuItem.Name = "треугольникToolStripMenuItem";
            this.треугольникToolStripMenuItem.Size = new System.Drawing.Size(234, 22);
            this.треугольникToolStripMenuItem.Text = "Треугольник";
            // 
            // отрезокToolStripMenuItem
            // 
            this.отрезокToolStripMenuItem.CheckOnClick = true;
            this.отрезокToolStripMenuItem.Name = "отрезокToolStripMenuItem";
            this.отрезокToolStripMenuItem.Size = new System.Drawing.Size(234, 22);
            this.отрезокToolStripMenuItem.Text = "Отрезок";
            // 
            // polygonMenu
            // 
            this.polygonMenu.CheckOnClick = true;
            this.polygonMenu.Name = "polygonMenu";
            this.polygonMenu.Size = new System.Drawing.Size(234, 22);
            this.polygonMenu.Text = "Правильный многоугольник";
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
        private System.Windows.Forms.ToolStripMenuItem выберитеФигуруToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem circleMenu;
        private System.Windows.Forms.ToolStripMenuItem squareMenu;
        private System.Windows.Forms.ToolStripMenuItem треугольникToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem отрезокToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem polygonMenu;
    }
}

