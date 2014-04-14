using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace _FivePies
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private int n = 0;
        private int[,] chess = new int[11, 11];
        int countx = 0;
        int county = 0;
        int countz = 0;
        int countt = 0;
        //int a = 0;//横线胜利
        //int b = 0;//竖线胜利
        //int c = 0;//左斜线胜利
        //int d = 0;//右斜线胜利

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen p = new Pen(Color.Silver, 2);
            //竖线
            int x = 0;
            int y = 0;
            //<string, string> listbeg = new List<string,string>();
            for (int i = 0; i < 11; i++)
            {
                g.DrawLine(p, 50 + x, 50, 50 + x, 450);
                g.DrawLine(p, 50, 50 + y, 450, 50 + y);
                x += 40;
                y += 40;
            }

            g.DrawEllipse(p, 10, 10, 40, 40);
            //标记所有空格为false;


            //g.DrawLine(p, 50, 50, 450, 50);
            //g.DrawLine(p, 450, 50, 450, 450);
            //g.DrawLine(p, 450, 450, 50, 450);
            //g.DrawLine(p, 50, 450, 50, 50);
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            Graphics g = this.CreateGraphics();
            Pen p1 = new Pen(Color.Blue, 2);
            //判断颜色，先入为蓝，后入为红
            Pen p2 = new Pen(Color.Black, 2);
            Pen p;

            if (n % 2 == 0)
            {
                p = p1;
            }
            else
            {
                p = p2;
            }
            n += 1;

            //画圆
            //点击的坐标
            
            //左边距50，方框 上下 左右 均间隔40；
            //修正坐标
            int x = 50 + 40 * (((e.X - 50) / 40 + 1) - 1);
            int y = 50 + 40 * (((e.Y - 50) / 40 + 1) - 1);
            //保存棋子
            //蓝棋 1；黑棋2；无棋0；

            //先判断该点是否为空（是否下过棋）
            if (chess[((e.X - 50) / 40 + 1) - 1, ((e.Y - 50) / 40 + 1) - 1] != 0)
            {
                return;
            }
            if (p.Color == Color.Blue)
            {
                //数组从0开始的
                chess[((e.X - 50) / 40 + 1) - 1, ((e.Y - 50) / 40 + 1) - 1] = 1;
            }
            else if (p.Color == Color.Black)
            {
                chess[((e.X - 50) / 40 + 1) - 1, ((e.Y - 50) / 40 + 1) - 1] = 2;
            }
            g.DrawEllipse(p, x, y, 40, 40);
            // int check = checkrow(((e.X - 50) / 40 + 1) - 1, ((e.Y - 50) / 40 + 1) - 1);
            //MessageBox.Show(check.ToString());
            int x_ = ((e.X - 50) / 40 + 1) - 1;
            int y_ = ((e.Y - 50) / 40 + 1) - 1;


            checkxl(x_, y_);
            int a = checkxr(x_, y_);

            checkyh(x_, y_);
            int b = checkyl(x_, y_);

            checkzzs(x_, y_);
            int c = checkzyx(x_, y_);

            checktzx(x_, y_);
            int t = checktys(x_, y_);

            if (p == p1)
            {
                label1.Text = "横向 蓝色:" + a.ToString();//横向
                label2.Text = "竖向 蓝色：" + b.ToString();//竖向
                label3.Text = "左斜向 蓝色：" + c.ToString();//左斜向
                label4.Text = "右斜向 蓝色" + t.ToString();//右斜向
            }
            if (p == p2)
            {
                label1.Text = "横向 黑色:" + a.ToString();//横向
                label2.Text = "竖向 黑色：" + b.ToString();//竖向
                label3.Text = "左斜向 黑色：" + c.ToString();//左斜向
                label4.Text = "右斜向 黑色：" + t.ToString();//右斜向
            }
            countx = 0;
            county = 0;
            countz = 0;
            countt = 0;
        }
        private void btnCreate_Click(object sender, EventArgs e)
        {
            string str = "";
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    str += chess[j, i].ToString() + ",";
                }
                str += "\r\n";
            }
            MessageBox.Show(str);
        }

        #region 当棋盘触碰窗口导致棋子消失时候，重绘
        private void btnAg_Click(object sender, EventArgs e)
        {
            Graphics g = this.CreateGraphics();
            Pen p1 = new Pen(Color.Blue, 2);
            //判断颜色，先入为蓝，后入为红
            Pen p2 = new Pen(Color.Black, 2);
            Pen p;
            for (int i = 0; i < 11; i++)
			{
                for (int j = 0; j < 11; j++)
			    {
                    if (chess[i, j] == 1)//蓝色
                    {
                        p = p1; 
                        g.DrawEllipse(p, i * 40 + 50, j * 40 + 50, 40, 40);
                    }
                    if(chess[i,j]==2)
                    {
                        p = p2;
                        g.DrawEllipse(p, i * 40 + 50, j * 40 + 50, 40, 40);
                    }

			    }
			}
        }
        #endregion

        #region 向左搜索
        private int checkxl(int x, int y)
        {
            if (x - 1 >= 0)
            {
                if (chess[x - 1, y] == chess[x, y])
                {
                    countx++;
                    checkxl(x - 1, y);
                }
            }
            return countx;
        }
        #endregion

        #region 向右搜索
        //向右搜索
        private int checkxr(int x, int y)
        {
            if (x + 1 <= 10)
            {
                if (chess[x + 1, y] == chess[x, y])
                {
                    countx++;
                    checkxr(x + 1, y);
                }
            }
            return countx;
        }
        #endregion

        #region 向上搜索
        private int checkyh(int x, int y)
        {
            if (y - 1 >= 0)
            {
                if (chess[x,y-1]==chess[x,y])
                {
                    county++;
                    checkyh(x,y-1);
                }
            }
            return county;
        }
        #endregion

        #region 向下搜索
        private int checkyl(int x, int y)
        {
            if (y+1<=10)
            {
                if (chess[x, y + 1] == chess[x, y])
                {
                    county++;
                    checkyl(x, y + 1);
                }
            }
            return county;
        }
        #endregion

        #region 左上搜索
        private int checkzzs(int x, int y)
        {
            if (x-1>=0&&y-1>=0)
            {
                if (chess[x-1,y-1]==chess[x,y])
                {
                    countz++;
                    checkzzs(x-1,y-1);
                }
            }
            return countz;
        }
        #endregion

        #region 右下搜索
        private int checkzyx(int x, int y)
        {
            if (x+1<=10&&y+1<=10)
            {
                if (chess[x+1,y+1]==chess[x,y])
                {
                    countz++;
                    checkzyx(x+1,y+1);
                }
            }
            return countz;
        }
        #endregion

        #region 右上搜索
        private int checktys(int x, int y)
        {
            if (x+1<=10&&y-1>=0)
            {
                if (chess[x+1,y-1]==chess[x,y])
                {
                    countt++;
                    checktys(x+1,y-1);
                }
            }
            return countt;
        }
        #endregion

        #region 左下搜索
        private int checktzx(int x, int y)
        {
            if (x-1>=0&&y+1<=10)
            {
                if (chess[x-1,y+1]==chess[x,y])
                {
                    countt++;
                    checktzx(x - 1, y + 1);
                }
            }
            return countt;
        }

        #endregion

    }
}