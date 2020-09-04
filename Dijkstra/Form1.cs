using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dijkstra
{
    public partial class Form1 : MetroFramework.Forms.MetroForm
    {
        Graphics g;
        bool buttonclicked = false;
        int numerator = 65,digit = 1;
        List<Rectangle> vertex = new List<Rectangle>();
        List<Line> edges = new List<Line>();
        public Form1()
        {
            InitializeComponent();
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new Size(Width, Height);
            this.MaximumSize = this.MinimumSize;
            panel1.Paint += Panel1_Paint;
            g = panel1.CreateGraphics();
        }

        private void Panel1_Paint(object sender, PaintEventArgs e)
        {
            drawEdges(e.Graphics);
            drawVertex(e.Graphics);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            buttonclicked = true;
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            Rectangle r = new Rectangle(e.X - 25, e.Y - 25, 50, 50);
            if (buttonclicked && radioButton1.Checked == true)
            {
                Pen myPen = new Pen(Brushes.Black);
                myPen.Width = 2.0F;
                g.DrawEllipse(myPen, r);
                g.FillEllipse(Brushes.White, r);
                var measureString = g.MeasureString(((char)numerator).ToString(), this.Font);
                g.DrawString(((char)numerator).ToString(), new Font(this.Font, FontStyle.Bold), Brushes.BlueViolet, e.X - measureString.Width / 2, e.Y - measureString.Height / 2);
                buttonclicked = false;
                numerator++;
                vertex.Add(r);
                myPen.Dispose();
            }
            if(buttonclicked && radioButton2.Checked == true)
            {
                Pen myPen = new Pen(Brushes.Black);
                myPen.Width = 2.0F;
                g.DrawEllipse(myPen, r);
                g.FillEllipse(Brushes.White, r);
                var measureString = g.MeasureString(digit.ToString(), this.Font);
                g.DrawString((digit).ToString(), new Font(this.Font, FontStyle.Bold), Brushes.BlueViolet, e.X - measureString.Width / 2, e.Y - measureString.Height / 2);
                buttonclicked = false;
                digit++;
                vertex.Add(r);
                myPen.Dispose();
            }
        }

        private void radioButton1_Click(object sender, EventArgs e)
        {
            radioButton2.Enabled = false;
        }

        private void radioButton2_Click(object sender, EventArgs e)
        {
            radioButton1.Enabled = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Pen myPen = new Pen(Color.Black,15);
            myPen.Width = 1.0F;
            Point start, endp;
            if (radioButton2.Checked)
            {
                int head = Convert.ToInt32(textBox1.Text);
                int end = Convert.ToInt32(textBox2.Text);
                start = new Point(vertex[head - 1].X + 25, vertex[head - 1].Y + 25);
                endp = new Point(vertex[end - 1].X + 25, vertex[end - 1].Y + 25);
                g.DrawLine(myPen, start, endp);
                var measureString = g.MeasureString(textBox3.Text, this.Font);
                g.DrawString(textBox3.Text, new Font(this.Font, FontStyle.Bold), Brushes.Red, (start.X + endp.X) / 2 - measureString.Width / 2, (start.Y + endp.Y) / 2 - measureString.Height / 2);
                drawVertex(g);
                edges.Add(new Line(start, endp, textBox3.Text, head.ToString(), end.ToString()));
            }
            else
            {
                char head  = textBox1.Text[0];
                char end = textBox2.Text[0];
                start = new Point(vertex[head - 65].X + 25, vertex[head - 65].Y + 25);
                endp = new Point(vertex[end - 65].X + 25, vertex[end - 65].Y + 25);
                g.DrawLine(myPen, start, endp);
                var measureString = g.MeasureString(textBox3.Text, this.Font);
                g.DrawString(textBox3.Text, new Font(this.Font, FontStyle.Bold), Brushes.Red, (start.X + endp.X) / 2 - measureString.Width / 2, (start.Y + endp.Y) / 2 - measureString.Height / 2);
                drawVertex(g);
                edges.Add(new Line(start, endp, textBox3.Text, head.ToString(), end.ToString()));
            }
        }

        private void drawVertex(Graphics g)
        {
            Pen myPen = new Pen(Brushes.Black);
            myPen.Width = 2.0F;
            int digit = 1;
            int nums = 65;
            if (radioButton2.Checked)
            {
                foreach (Rectangle rec in vertex)
                {
                    var measureString = g.MeasureString(digit.ToString(), this.Font);
                    g.DrawEllipse(myPen, rec);
                    g.FillEllipse(Brushes.White, rec);
                    g.DrawString((digit).ToString(), new Font(this.Font, FontStyle.Bold), Brushes.BlueViolet, rec.X - measureString.Width / 2 + 25, rec.Y - measureString.Height / 2 + 25);
                    digit++;
                }
            }
            if(radioButton1.Checked)
            {
                foreach (Rectangle rec in vertex)
                {
                    var measureString = g.MeasureString(((char)nums).ToString(), this.Font);
                    g.DrawEllipse(myPen, rec);
                    g.FillEllipse(Brushes.White, rec);
                    g.DrawString(((char)nums).ToString(), new Font(this.Font, FontStyle.Bold), Brushes.BlueViolet, rec.X - measureString.Width / 2 + 25, rec.Y - measureString.Height / 2 + 25);
                    nums++;
                }
            }
            myPen.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GetDijkstra(textBox5.Text,textBox6.Text);
        }
        private void GetDijkstra(string p1, string p2)
        {           
            List<List<string>> weights = new List<List<string>>();
            richTextBox1.Text += "       D[2]  D[3]  D[4]\n";
            richTextBox1.Text += "1      ";
            for (int j = 1; j < vertex.Count + 1; j++)
            {
                weights.Add(new List<string>());
                for (int i = 2; i < vertex.Count + 1; i++)
                {
                    Line tmp = edges.FirstOrDefault(value => value.p1 == p1 && value.p2 == i.ToString());
                    if (tmp != null)
                    {
                        weights[j-1].Add(tmp.weight);
                        richTextBox1.Text += tmp.weight + new string(' ', 6);
                    }
                    else richTextBox1.Text += 0.ToString() + new string(' ', 6);
                }
                int minimum = (from tmp in weights[j-1] select Convert.ToInt32(tmp)).Min();
                p1 = (weights[j-1].IndexOf(minimum.ToString())+2).ToString();
                richTextBox1.Text += $"\n{p1}      ";
            }
            //
            for (int i = 0; i < weights.Count; i++)
            {
                for (int j = 0; j < weights[i].Count; j++)
                    richTextBox1.Text += weights[i][j] + " ";
                richTextBox1.Text += '\n';
            }
        }
        private void drawEdges(Graphics g)
        {
            Pen myPen = new Pen(Color.Black, 15);
            myPen.Width = 1.0F;
            foreach (Line rec in edges)
            {
                g.DrawLine(myPen, rec.start, rec.end);
                var measureString = g.MeasureString(rec.weight, this.Font);
                g.DrawString(rec.weight, new Font(this.Font, FontStyle.Bold), Brushes.Red, (rec.start.X + rec.end.X) / 2 - measureString.Width / 2, (rec.start.Y + rec.end.Y) / 2 - measureString.Height / 2);
            }
        }
    }
}

                        //if (j==1)
                        //{
                        //    if ((edge.p1 == p1 && edge.p2 == i.ToString()) || (edge.p1 == i.ToString() && edge.p2 == p1))
                        //    {
                        //        weights[j - 1].Add(edge.weight);
                        //        break;
                        //    }
                        //}
                        //else
                        //{
                        //    if ((edge.p1 == p1.ToString() && edge.p2 == i.ToString()) || (edge.p2 == p1.ToString() && edge.p1 == i.ToString()))
                        //    {
                        //        weights[j - 1].Add(edge.weight);
                        //        break;
                        //    }
                        //}
