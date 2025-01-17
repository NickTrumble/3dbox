using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _3D_experiment
{
    public partial class Form1 : Form
    {
        public ENVIRONMENT Env = new ENVIRONMENT(20, new THREEVector(5, 5, 10));
        public CUBE Cube = new CUBE(new THREEVector(0, 0, 5));
        public CUBE Selected;
        public Point Center;
        public float Speed = 0;
        public Form1()
        {
            Env.Cubes.Add(Cube);
            Selected = Cube;
            Center = new Point(this.Width, this.Height);

            InitializeComponent();
            pictureBox1.Invalidate();
        }

        
        

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            //CAMERA MOVEMENT
            switch (e.KeyCode)
            {
                case Keys.W://MOVE CAMERA FORWARDS; CUBE GOES BACKWARDS
                    Selected.MOVE(THREEVector.BACKWARDS);
                    break;
                case Keys.S://MOVE CAMERA BACKWARDS; CUBE GOES FORWARDS
                    Selected.MOVE(THREEVector.FORWARDS);
                    break;
                case Keys.A://MOVE CAMERA LEFT; CUBE GOES RIGHT
                    Selected.MOVE(THREEVector.RIGHT);
                    break;
                case Keys.D://MOVE CAMERA RIGHT; CUBE GOES LEFT
                    Selected.MOVE(THREEVector.LEFT);
                    break;
                case Keys.Q://MOVE CAMERA UP; CUBE GOES DOWN
                    Selected.MOVE(THREEVector.DOWN);
                    break;
                case Keys.E://MOVE CAMERA DOWN; CUBE UP
                    Selected.MOVE(THREEVector.UP);
                    break;
            }

            pictureBox1.Invalidate();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {

            
            //disposes pen after
            foreach(var Obj in Env.Cubes)
            {
                
                Brush B = new SolidBrush(Color.Black);
                //FRONT SIDE
                e.Graphics.FillPolygon(B, new PointF[3] { DepthCorrector(Obj.Vertex[0]),
                    DepthCorrector(Obj.Vertex[1]), DepthCorrector(Obj.Vertex[2]) });
                e.Graphics.FillPolygon(B, new PointF[3] { DepthCorrector(Obj.Vertex[1]),
                    DepthCorrector(Obj.Vertex[2]), DepthCorrector(Obj.Vertex[3]) });


                if (Obj.Centre.Y <= -1)//TOP SIDE     
                {
                    B = new SolidBrush(Color.DarkGray);

                    e.Graphics.FillPolygon(B, new PointF[3] { DepthCorrector(Obj.Vertex[2]),
                    DepthCorrector(Obj.Vertex[3]), DepthCorrector(Obj.Vertex[6]) });
                    e.Graphics.FillPolygon(B, new PointF[3] { DepthCorrector(Obj.Vertex[6]),
                    DepthCorrector(Obj.Vertex[7]), DepthCorrector(Obj.Vertex[3]) });
                }

                if (Obj.Centre.X < 0)//RIGHT SIDE     
                {
                    B = new SolidBrush(Color.Gray);

                    e.Graphics.FillPolygon(B, new PointF[3] { DepthCorrector(Obj.Vertex[1]),
                    DepthCorrector(Obj.Vertex[3]), DepthCorrector(Obj.Vertex[5]) });
                    e.Graphics.FillPolygon(B, new PointF[3] { DepthCorrector(Obj.Vertex[3]),
                    DepthCorrector(Obj.Vertex[5]), DepthCorrector(Obj.Vertex[7]) });
                }

                if (Obj.Centre.X > 0)//LEFT SIDE     
                {
                    B = new SolidBrush(Color.Gray);

                    e.Graphics.FillPolygon(B, new PointF[3] { DepthCorrector(Obj.Vertex[0]),
                    DepthCorrector(Obj.Vertex[2]), DepthCorrector(Obj.Vertex[4]) });
                    e.Graphics.FillPolygon(B, new PointF[3] { DepthCorrector(Obj.Vertex[2]),
                    DepthCorrector(Obj.Vertex[4]), DepthCorrector(Obj.Vertex[6]) });
                }

                if (Obj.Centre.Y >= 1)//BOTTOM SIDE     
                {
                    B = new SolidBrush(Color.DarkGray);

                    e.Graphics.FillPolygon(B, new PointF[3] { DepthCorrector(Obj.Vertex[0]),
                    DepthCorrector(Obj.Vertex[1]), DepthCorrector(Obj.Vertex[4]) });
                    e.Graphics.FillPolygon(B, new PointF[3] { DepthCorrector(Obj.Vertex[1]),
                    DepthCorrector(Obj.Vertex[4]), DepthCorrector(Obj.Vertex[5]) });
                }
            }


            using (Pen p = new Pen(Color.Green, 2))
            {
                if (Selected.Centre.Y >= 1)//BOTTOM
                {
                    e.Graphics.DrawPolygon(p, new PointF[4] {
                            DepthCorrector(Selected.Vertex[0]), DepthCorrector(Selected.Vertex[1]),
                            DepthCorrector(Selected.Vertex[5]), DepthCorrector(Selected.Vertex[4])
                        });
                    
                }
                else if (Selected.Centre.Y <= -1)//TOP
                {
                    e.Graphics.DrawPolygon(p, new PointF[4] {
                            DepthCorrector(Selected.Vertex[2]), DepthCorrector(Selected.Vertex[6]),
                            DepthCorrector(Selected.Vertex[7]), DepthCorrector(Selected.Vertex[3])
                        });

                    if (DepthCorrector(Selected.Vertex[0]).Y > 2f * Center.Y)
                    {
                        float g = Selected.Centre.Z;
                    }
                }

                if (Selected.Centre.X < 0)//RIGHT
                {
                    e.Graphics.DrawPolygon(p, new PointF[4] {
                            DepthCorrector(Selected.Vertex[1]), DepthCorrector(Selected.Vertex[3]),
                            DepthCorrector(Selected.Vertex[7]), DepthCorrector(Selected.Vertex[5])
                        });
                }
                else if (Selected.Centre.X > 0)//LEFT
                {
                    e.Graphics.DrawPolygon(p, new PointF[4] {
                            DepthCorrector(Selected.Vertex[0]), DepthCorrector(Selected.Vertex[4]),
                            DepthCorrector(Selected.Vertex[6]), DepthCorrector(Selected.Vertex[2])
                        });
                }
                e.Graphics.DrawPolygon(p, new PointF[4] {//FRONT
                            DepthCorrector(Selected.Vertex[0]), DepthCorrector(Selected.Vertex[2]),
                            DepthCorrector(Selected.Vertex[3]), DepthCorrector(Selected.Vertex[1])
                        });
            }

            
        }

        public PointF DepthCorrector(THREEVector v)
        {

            float scale = 150f / v.Z;

            float ProjectedX = v.X * scale;
            float ProjectedY = v.Y * scale;

            return new PointF(Center.X - ProjectedX, Center.Y - ProjectedY);
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Env.Cubes.Add(new CUBE(new THREEVector(0, 0, 8)));
                pictureBox1.Invalidate();
            }
            else if (e.Button == MouseButtons.Right)
            {
                //FIND CUBE
                foreach(var box in Env.Cubes)
                {
                    PointF BoxCenter = DepthCorrector(box.Centre);
                    if (Math.Abs(e.X - BoxCenter.X) < 50 && Math.Abs(e.Y - BoxCenter.Y) < 50)
                    {
                        Selected = box;
                        pictureBox1.Invalidate();
                        break;

                    }
                    
                }
            }
            else if (e.Button == MouseButtons.Middle)
            {
                if (timer1.Enabled)
                {
                    timer1.Stop();
                }
                else
                {
                    Speed = 0;
                    timer1.Start();
                }
            }
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            Center = new Point(this.Width / 2, this.Height / 2);
            pictureBox1.Size = this.Size;
            pictureBox1.Invalidate();
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            Speed += 0.01f;
            for(int i = 0; i < Selected.Vertex.Count; i++)
            {
                Selected.MOVE(new THREEVector(0, -Speed, 0));
            }
            
            pictureBox1.Invalidate();
        }
    }

    public class THREEVector//3-DIMENSIONAL VECTOR
    {
        public float X;//LEFT + RIGHT
        public float Y;//UP + DOWN
        public float Z;//BACK + FORTH

        public static THREEVector LEFT = new THREEVector(-1, 0, 0);
        public static THREEVector RIGHT = new THREEVector(1, 0, 0);
        public static THREEVector UP = new THREEVector(0, 1, 0);
        public static THREEVector DOWN = new THREEVector(0, -1, 0);
        public static THREEVector FORWARDS = new THREEVector(0, 0, 1);
        public static THREEVector BACKWARDS = new THREEVector(0, 0, -1);

        public THREEVector(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public static THREEVector operator +(THREEVector a, THREEVector b)
        {
            return new THREEVector(
                Math.Min(ENVIRONMENT.Bounds.X, Math.Max(a.X + b.X, -ENVIRONMENT.Bounds.X)),
                Math.Min(ENVIRONMENT.Bounds.Y, Math.Max(a.Y + b.Y, -ENVIRONMENT.Bounds.Y)),
                Math.Min(ENVIRONMENT.Bounds.Z, Math.Max(a.Z + b.Z, 1))
                );
        }

        public static THREEVector operator /(THREEVector a, THREEVector b)
        {
            return new THREEVector(
                a.X + b.X,
                a.Y + b.Y,
                a.Z + b.Z
                );
        }

        public static THREEVector operator *(THREEVector a, float b)
        {
            return new THREEVector(
                Math.Min(ENVIRONMENT.Bounds.X, Math.Max(a.X * b, 1)),
                Math.Min(ENVIRONMENT.Bounds.Y, Math.Max(a.Y * b, 1)),
                Math.Min(ENVIRONMENT.Bounds.Z, Math.Max(a.Z * b, 1))
                );
        }

        public static bool operator ==(THREEVector a, THREEVector b)
        {
            return a.X == b.X && a.Y == b.Y && a.Z == b.Z;
        }
        public static bool operator !=(THREEVector a, THREEVector b)
        {
            return a.X != b.X || a.Y != b.Y || a.Z != b.Z;
        }


    }

    public class CUBE
    {
        public List<THREEVector> Vertex = new List<THREEVector>();
        public THREEVector Centre;

        public CUBE(THREEVector c)
        {
            Centre = c;
            Vertex.AddRange(new List<THREEVector>
            {
                // MIDDLE: (0, 0, 6) 
                //FRONT 4
                new THREEVector(-1, -1, 4) + c,//BOTTOM LEFT
                new THREEVector(1, -1, 4) + c,//BOTTOM RIGHT
                new THREEVector(-1, 1, 4) + c,//TOP LEFT
                new THREEVector(1, 1, 4) + c,//TOP RIGHT
                new THREEVector(-1, -1, 6) + c,//BACK 4
                new THREEVector(1, -1, 6) + c,
                new THREEVector(-1, 1, 6) + c,
                new THREEVector(1, 1, 6) + c
            });
        }

        public void MOVE(THREEVector a)
        {
            
            for (int i = 0; i < Vertex.Count; i++)
            {
                if ((Vertex[i] + a) != (Vertex[i] / a))
                {
                    return;
                }
            }
            Centre += a;
            for (int i = 0; i < Vertex.Count; i++)
            {
                Vertex[i] += a;
            }
        }

    }

    public class WALL
    {
        //add wall
    }

    public class ENVIRONMENT
    {
        public List<CUBE> Cubes = new List<CUBE>();
        public int Tile;
        public static THREEVector Bounds;
        
        public ENVIRONMENT(int tile, THREEVector boundaries)
        {
            Tile = tile;
            Bounds = boundaries;
        }
    }
}
