using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Test_NIPI
{
    public partial class Form1 : Form
    {
        int x1, y1, x2, y2;
        List<Point> pointsLines = new List<Point>();
        List<Point> pointsRectangle = new List<Point>();
        List<Point> pointsIntersection = new List<Point>();
        int Drawshape;
        private Bitmap bmp;
        public int widthrec;
        public int heightrec;

        Point lastPoint = Point.Empty;
        bool isMouseDown; // это используется для оценки того, нажата ли наша кнопка мыши или нет

        public Form1()
        {
            InitializeComponent();
            bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Invalidate();
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (Drawshape == 1)
                {
                    lastPoint = e.Location; // мы присваиваем последнюю точку текущей позиции мыши: e.Location ('e' - это значение из MouseEventArgs, переданного в событие MouseDown).
                    isMouseDown = true; // мы устанавливаем значение true, потому что наша кнопка мыши нажата
                }
                else if (Drawshape == 2)
                {
                    pointsIntersection.Clear();
                    x2 = e.X; // координата по оси X
                    y2 = e.Y; // координата по оси Y              
                    Form Form2 = new Form2();
                    DialogResult dialog = Form2.ShowDialog(this);
                    if (dialog == DialogResult.OK)
                    {
                        if (pictureBox1.Image == null) // если в picturebox нет доступного растрового изображения, на котором можно было бы рисовать
                        {
                            // создание нового растрового изображения
                            Bitmap bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
                            // присвоение созданному растровому изображению свойство picturebox.Image
                            pictureBox1.Image = bmp;
                        }
                        Rectangle rectangle = new Rectangle(x2, y2, widthrec, heightrec);
                        
                        // нам нужно создать графический объект для рисования на picturebox, это наш основной инструмент
                        using (Graphics g = Graphics.FromImage(pictureBox1.Image))
                        {
                            g.DrawRectangle(new Pen(Color.Black, 2), rectangle);
                            g.Save();
                        }
                        foreach (Point item in pointsLines)
                        {
                            if (rectangle.Contains(item))
                            {
                                pointsIntersection.Add(item);
                            }
                        }
                        pictureBox1.Invalidate();
                    }
                    else if (dialog == DialogResult.Cancel)
                    {
                        pictureBox1.Invalidate();
                    }
                }
                else
                {
                    MessageBox.Show("Выберите что будете рисовать.", "Предупреждение!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            // возвращение предыдущей точки в нулевое значение, если пользователь отпустит кнопку мыши
            isMouseDown = false;
            lastPoint = Point.Empty;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                if (Drawshape == 1) 
                {
                    toolTip1.RemoveAll();
                    if (isMouseDown == true) // проверка, нажата ли кнопка мыши
                    {
                        if (lastPoint != null) // если наша последняя точка не равна нулю, что в данном случае мы указали выше
                        {
                            if (pictureBox1.Image == null) // если в picturebox нет доступного растрового изображения, на котором можно было бы рисовать
                            {
                                // создание нового растрового изображения
                                Bitmap bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
                                // присвоение созданному растровому изображению свойство picturebox.Image
                                pictureBox1.Image = bmp;
                            }
                            
                            // нам нужно создать графический объект для рисования на picturebox, это наш основной инструмент
                            using (Graphics g = Graphics.FromImage(pictureBox1.Image))
                            {
                                pointsLines.Add(lastPoint);
                                // при создании объекта с пером вы можете просто придать ему только цвет или задать цвет и размер пера
                                g.DrawLine(new Pen(Color.Green, 2), lastPoint, e.Location);
                                // это делается для того, чтобы придать рисунку более плавный и менее резкий вид
                                g.SmoothingMode = SmoothingMode.AntiAlias;
                                g.Save();                         
                            }
                            pictureBox1.Invalidate(); // обновляет picturebox
                            lastPoint = e.Location; // назначение lastPoint текущему положению мыши
                        }
                    }
                }      
                if (Drawshape == 2)
                {
                    toolTip1.SetToolTip(pictureBox1, "Укажите точку");
                }        

            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}","Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }          
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (pointsIntersection.Count == 0)
                {
                    MessageBox.Show("Пересечений нет!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (pictureBox1.Image == null) // если в picturebox нет доступного растрового изображения, на котором можно было бы рисовать
                {
                    // создание нового растрового изображения
                    Bitmap bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
                    // присвоение созданному растровому изображению свойство picturebox.Image
                    pictureBox1.Image = bmp;
                }

                using (Graphics g = Graphics.FromImage(pictureBox1.Image))
                {
                    // при создании объекта с пером вы можете просто придать ему только цвет или задать цвет и размер пера
                    g.DrawCurve(new Pen(Color.Yellow, 2), pointsIntersection.ToArray());
                    g.SmoothingMode = SmoothingMode.AntiAlias;
                    g.Save();
                    //this is to give the drawing a more smoother, less sharper look
                }
                pictureBox1.Invalidate(); //refreshes the picturebox
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            // Для отрисовки набора соединенных отрезков
            Drawshape = 1;
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            // Для отрисовки прямоугольной области
            Drawshape = 2;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            #region Очистка области рисования
            try
            {
                pointsLines.Clear();
                pointsIntersection.Clear();
                bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
                pictureBox1.Image = bmp;
                pictureBox1.Invalidate();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            #endregion
        }
    }
}
