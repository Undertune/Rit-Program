using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Schema;

namespace Prog2ExamineradeUppgift1
{
    public partial class MyPaint : Form
    {

        public Machine m = new Machine();

        public MyPaint()
        {
            InitializeComponent();
            m.Init(panel1);
            widthBar.Value = 1;
        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {
            PictureBox p = (PictureBox)sender;
            m.PickColor(p);
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            m.MouseDown(e);
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            m.Paint(e);
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            m.MouseUp(e, panel1);
        }

        private void widthBar_Scroll(object sender, EventArgs e)
        {
            m.ChangeWidth(widthBar.Value);
        }

        private void btnEraser_Click(object sender, EventArgs e)
        {
            m.Eraser(pictureBox2);
        }

        private void btnPen_Click(object sender, EventArgs e)
        {
            m.Pencil();
        }

        private void btnRectangle_Click(object sender, EventArgs e)
        {

        }

        private void btnElipse_Click(object sender, EventArgs e)
        {
            m.Elipse();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // MyPaint
            // 
            this.ClientSize = new System.Drawing.Size(432, 368);
            this.Name = "MyPaint";
            this.ResumeLayout(false);

        }
    }

    public class Machine
    {
        Graphics g;
        Pen pen;
        private int x = -1;
        private int y = -1;
        private bool usingPen = true;
        private bool usingRectangle = false;
        private bool usingElipse = false;
        private bool moving = false;
        private Point previousPoint;
        private float width;
        private ColorDialog c = new ColorDialog();

        public Machine()
        {
            c.Color = Color.Black;
            width = 1;
        }

        public void Init(Panel panel1)
        {
            g = panel1.CreateGraphics();
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
        }

        public void PickColor(PictureBox item)
        {
            if (c.ShowDialog() == DialogResult.OK)
            {
                item.BackColor = c.Color;
            }
        }

        public void Paint(MouseEventArgs e)
        {

            Pen pen = new Pen(c.Color, width);
            Brush brush = new SolidBrush(c.Color);
            Rectangle rectangle = new Rectangle(x, y, previousPoint.X-x, previousPoint.Y-y);
            if (moving && usingPen)
            {
                g.DrawLine(pen, new Point(x,y), e.Location);
                x = e.X;
                y = e.Y;
            }
            if (moving && usingRectangle)
            {
                g.FillRectangle(brush , rectangle);
                x = e.X;
                y = e.Y;
            }
            if (moving && usingElipse)
            {
                g.FillEllipse(brush , rectangle);
                x = e.X;
                y = e.Y;
            }
        }
        public void ChangeWidth(float choice) 
        {
            width = choice;
        }

        public void Pencil()
        {
            usingRectangle = false;
            usingPen = true;
            usingElipse = false;
        }
        public void Elipse()
        {
            usingPen = false;
            usingRectangle = false;
            usingElipse = true;
        }

        public void Eraser(PictureBox pictureBox2)
        {
            usingRectangle = false;
            usingElipse = false;
            usingPen = true;
            pen = new Pen(Color.White, width);
            pictureBox2.BackColor = Color.White;
        }
        public void MouseUp(MouseEventArgs e, Panel panel1)
        {
            if (moving == true)
            {
                previousPoint = e.Location;
            }
            moving = false;
            x = e.X;
            y = e.Y;
        }
        
        public void MouseDown(MouseEventArgs e)
        {
  
            moving = true;
            previousPoint = e.Location;
            x = e.X;
            y = e.Y;
            
        }
    }
}
