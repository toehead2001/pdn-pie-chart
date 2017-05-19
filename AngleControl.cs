//Downloaded from
//Visual C# Kicks - http://vckicks.110mb.com
//The Code Project - http://www.codeproject.com

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace AngleControl
{
    public sealed class AngleSelector : UserControl
    {
        private double angle;

        private Rectangle drawRegion;
        private Point origin;

        private float outlinePenWidth = 1.0f;
        private float anglePenWidth = 1.6f;

        public AngleSelector()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
        }

        protected override void OnLoad(EventArgs e)
        {
            setDrawRegion();

            base.OnLoad(e);
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            this.Height = this.Width; //Keep it a square
            setDrawRegion();

            base.OnSizeChanged(e);
        }

        private void setDrawRegion()
        {
            drawRegion = new Rectangle(0, 0, this.Width, this.Height);
            drawRegion.X += 2;
            drawRegion.Y += 2;
            drawRegion.Width -= 4;
            drawRegion.Height -= 4;

            int offset = 2;
            origin = new Point(drawRegion.Width / 2 + offset, drawRegion.Height / 2 + offset);

            this.Refresh();
        }

        public double Angle
        {
            get { return angle; }
            set
            {
                angle = value;

                if (!this.DesignMode)
                    AngleChanged?.Invoke(); //Raise event

                this.Refresh();
            }
        }

        public delegate void AngleChangedDelegate();
        public event AngleChangedDelegate AngleChanged;

        private static PointF DegreesToXY(double degrees, float radius, Point origin)
        {
            double radians = degrees * Math.PI / 180.0;

            return new PointF
            {
                X = (float)Math.Cos(radians) * radius + origin.X,
                Y = (float)Math.Sin(-radians) * radius + origin.Y
            };
        }

        private static double XYToDegrees(Point xy, Point origin)
        {
            double angle = 0.0;

            if (xy.Y < origin.Y)
            {
                if (xy.X > origin.X)
                {
                    angle = (double)(xy.X - origin.X) / (double)(origin.Y - xy.Y);
                    angle = Math.Atan(angle);
                    angle = 90.0 - angle * 180.0 / Math.PI;
                }
                else if (xy.X < origin.X)
                {
                    angle = (double)(origin.X - xy.X) / (double)(origin.Y - xy.Y);
                    angle = Math.Atan(-angle);
                    angle = 90.0 - angle * 180.0 / Math.PI;
                }
            }
            else if (xy.Y > origin.Y)
            {
                if (xy.X > origin.X)
                {
                    angle = (double)(xy.X - origin.X) / (double)(xy.Y - origin.Y);
                    angle = Math.Atan(-angle);
                    angle = 270.0 - angle * 180.0 / Math.PI;
                }
                else if (xy.X < origin.X)
                {
                    angle = (double)(origin.X - xy.X) / (double)(xy.Y - origin.Y);
                    angle = Math.Atan(angle);
                    angle = 270.0 - angle * 180.0 / Math.PI;
                }
            }

            if (angle > 180) angle -= 360; //Optional. Keeps values between -180 and 180
            return angle;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            PointF anglePoint = DegreesToXY(angle, origin.X - 3, origin);
            Rectangle originSquare = new Rectangle(origin.X -3, origin.Y - 3, 6, 6);

            //Draw
            using (SolidBrush fill = new SolidBrush(SystemColors.ControlLightLight))
                e.Graphics.FillEllipse(fill, drawRegion);
            using (Pen outline = new Pen(SystemColors.ControlDark, outlinePenWidth))
                e.Graphics.DrawEllipse(outline, drawRegion);
            using (Pen anglePen = new Pen(SystemColors.ControlDark, anglePenWidth))
                e.Graphics.DrawLine(anglePen, origin, anglePoint);
            using (SolidBrush centerFill = new SolidBrush(SystemColors.ControlDark))
                e.Graphics.FillEllipse(centerFill, originSquare);

            base.OnPaint(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            double thisAngle = findNearestAngle(new Point(e.X, e.Y));

            if (thisAngle != -1)
            {
                this.Angle = thisAngle;
                this.Refresh();
            }

            base.OnMouseDown(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left || e.Button == MouseButtons.Right)
            {
                double thisAngle = findNearestAngle(e.Location);

                if (thisAngle != -1)
                {
                    this.Angle = thisAngle;
                    this.Refresh();
                }
            }

            base.OnMouseMove(e);
        }

        private double findNearestAngle(Point mouseXY)
        {
            double thisAngle = XYToDegrees(mouseXY, origin);
            if (thisAngle != 0)
                return thisAngle;
            else
                return -1;
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // AngleSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "AngleSelector";
            this.Size = new System.Drawing.Size(60, 60);
            this.ResumeLayout(false);
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            outlinePenWidth = 1.6f;
            anglePenWidth = 2.0f;
            this.Refresh();

            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            outlinePenWidth = 1.0f;
            anglePenWidth = 1.6f;
            this.Refresh();

            base.OnMouseLeave(e);
        }
    }
}