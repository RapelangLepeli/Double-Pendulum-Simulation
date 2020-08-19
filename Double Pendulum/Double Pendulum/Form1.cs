using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Double_Pendulum
{
    public partial class Form1 : Form
    {
        static Point Origin;
        static Point Bob1;
        static Point Bob2;
        static float Length1;
        static float Length2;
        static float Mass1;
        static float Mass2;
        static float Angle1;
        static float Angle2;
        static Graphics g;
        static Pen pen;

        static float AnVel1,AnVel2;
       // static float AnAcc1,AnAcc2;
        static float Grav;

        static List<Color> lstColours;
        static Random random;

        public Form1()
        {
            InitializeComponent();
            g = panel1.CreateGraphics();
          
            Origin = new Point(panel1.Width / 2, 80);
            Mass1 = 30;
            Mass2 = 30;
            Length1 = 10;
            Length2 = 100;
            Angle1 = (float)Math.PI / 2;
            Angle2 = (float)Math.PI / 3;
            AnVel1 = (float)0.0;
            AnVel2 = (float)0.0;
            Grav = (float)1;
            lstColours = new List<Color>();
            random = new Random();
            lstColours.Add(Color.Blue);
            lstColours.Add(Color.Black);
            lstColours.Add(Color.Red);
            lstColours.Add(Color.Green);
            
            lstColours.Add(Color.Chartreuse);
            lstColours.Add(Color.Firebrick);
            lstColours.Add(Color.Purple);
            lstColours.Add(Color.Maroon);
            lstColours.Add(Color.Violet);

            lstColours.Add(Color.Chartreuse);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 1000; i++)
            {

                Thread.Sleep(40);
               //g.Clear(Color.Aqua);

                int ColourPicker = random.Next(lstColours.Count);
                pen = new Pen(lstColours[ColourPicker], 4);

                float num1 = (float)(-Grav * (2 * Mass1 + Mass2) * Math.Sin(Angle1));
                float num2 = (float)(-Mass2 * Grav * Math.Sin(Angle1 - 2 * Angle2));
                float num3 = (float)(-2 * Math.Sin(Angle1 - Angle2) * Mass2);
                float num4 = (float)(AnVel2 * AnVel2 * Length2 + AnVel1 * AnVel1 * Length1 * Math.Cos(Angle1 - Angle2));
                float den = (float)(Length1 * (2 * Mass1 + Mass2 - Mass2 * Math.Cos(2 * Angle1 - 2 * Angle2)));

                float Aac1 = (num1 + num2 + num3 * num4) / den;


                num1 = (float)(2 * Math.Sin(Angle1 - Angle2));
                num2 = (float)(AnVel1 * AnVel1 * Length1 * (Mass1 + Mass2));
                num3 = (float)(Grav * (Mass1 + Mass2) * Math.Cos(Angle1));
                num4 = (float)(AnVel2 * AnVel2 * Length2 * Mass2 * Math.Cos(Angle1 - Angle2));
                den = (float)(Length2 * (2 * Mass1 + Mass2 - Mass2 * Math.Cos(2 * Angle1 - 2 * Angle2)));

                float Acc2 = (num1 * (num2 + num3 + num4)) / den;


                Bob1.X = (int)(Origin.X + Length1 * Math.Sin(Angle1));
                Bob1.Y = (int)(Origin.Y + Length1 * Math.Cos(Angle1));

                g.DrawLine(pen, Origin, Bob1);
                g.DrawEllipse(pen, Bob1.X - 20, Bob1.Y, Mass1, Mass1);

                Bob2.X = (int)(Bob1.X + Length2 * Math.Sin(Angle2));
                Bob2.Y = (int)(Bob1.Y + Length2 * Math.Cos(Angle2));

                g.DrawLine(pen, Bob1, Bob2);
                g.DrawEllipse(pen, Bob2.X - 20, Bob2.Y, Mass2, Mass2);

                AnVel1 += Aac1;
                AnVel2 += Acc2;

                Angle1 += AnVel1;
                Angle2 += AnVel2;
            }


        }
    }
}
