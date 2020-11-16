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
            this.выберитеФигуруToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.circleMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.squareMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.треугольникToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.отрезокToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.drawBox)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // drawBox
            // 
            this.drawBox.Location = new System.Drawing.Point(0, 27);
            this.drawBox.Name = "drawBox";
            this.drawBox.Size = new System.Drawing.Size(1185, 600);
            this.drawBox.TabIndex = 0;
            this.drawBox.TabStop = false;
            this.drawBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ShapeCreate);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.выберитеФигуруToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1184, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // выберитеФигуруToolStripMenuItem
            // 
            this.выберитеФигуруToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.circleMenu,
            this.squareMenu,
            this.треугольникToolStripMenuItem,
            this.отрезокToolStripMenuItem});
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
            this.circleMenu.Size = new System.Drawing.Size(144, 22);
            this.circleMenu.Text = "Круг";
            // 
            // squareMenu
            // 
            this.squareMenu.CheckOnClick = true;
            this.squareMenu.Name = "squareMenu";
            this.squareMenu.Size = new System.Drawing.Size(144, 22);
            this.squareMenu.Text = "Квадрат";
            // 
            // треугольникToolStripMenuItem
            // 
            this.треугольникToolStripMenuItem.CheckOnClick = true;
            this.треугольникToolStripMenuItem.Name = "треугольникToolStripMenuItem";
            this.треугольникToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.треугольникToolStripMenuItem.Text = "Треугольник";
            // 
            // отрезокToolStripMenuItem
            // 
            this.отрезокToolStripMenuItem.CheckOnClick = true;
            this.отрезокToolStripMenuItem.Name = "отрезокToolStripMenuItem";
            this.отрезокToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.отрезокToolStripMenuItem.Text = "Отрезок";
            // 
            // PaintBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 624);
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
        private System.Windows.Forms.ToolStripMenuItem выберитеФигуруToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem circleMenu;
        private System.Windows.Forms.ToolStripMenuItem squareMenu;
        private System.Windows.Forms.ToolStripMenuItem треугольникToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem отрезокToolStripMenuItem;
    }
}

