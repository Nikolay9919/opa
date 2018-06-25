using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace opa.Model
{
    [Serializable]
    public class ShapePoint
    {
        public string XParam;
        public string YParam;
        public PointF Point;

        public ShapePoint(string XParam, PointF Point)
        {
            this.XParam = XParam;
            this.Point = Point;
        }
    }
    [Serializable]
    public class Shape
    {

        public Shape()
        {
        }

        public Shape(RectangleF rect)
        {
            rectangle = rect;
        }

        public virtual bool Contains(PointF point)
        {
            return Rectangle.Contains(point.X, point.Y);
        }


        public virtual void Move(float dx, float dy)
        {
            Location = new PointF(Location.X + dx, Location.Y + dy);
        }

        public virtual double GetDistance(double x1, double y1, double x2, double y2)
        {
            return Math.Sqrt(Math.Pow((x2 - x1), 2) + Math.Pow((y2 - y1), 2));
        }

        public virtual object GetResizePoint(PointF point)
        {

            ShapePoint[] destPoints = new ShapePoint[4] {
                new ShapePoint("Left Top", new PointF(Rectangle.Left, Rectangle.Top)),
                new ShapePoint("Right Top",  new PointF(Rectangle.Right, Rectangle.Top)),
                new ShapePoint("Right Bottom",  new PointF(Rectangle.Right, Rectangle.Bottom)),
                new ShapePoint("Left Bottom", new PointF(Rectangle.Left, Rectangle.Bottom))
            };

            for (int i = 0; i < destPoints.Length; i++)
            {
                PointF p = destPoints[i].Point;
                double distance = GetDistance(p.X, p.Y, point.X, point.Y);
                if (distance < 10) return destPoints[i];
            }

            return null;
        }



        public Shape(Shape shape)
        {
            this.Height = shape.Height;
            this.Width = shape.Width;
            this.Location = shape.Location;
            this.rectangle = shape.rectangle;

            this.FillColor = shape.FillColor;
        }

        public virtual PointF Location
        {
            get { return Rectangle.Location; }
            set { rectangle.Location = value; }
        }



        /// <summary>
        /// Обхващащ правоъгълник на елемента.
        /// </summaryz
     	private RectangleF rectangle;
        public virtual RectangleF Rectangle
        {
            get { return rectangle; }
            set { rectangle = value; }
        }

        public virtual float Width
        {
            get { return Rectangle.Width; }
            set { rectangle.Width = value; }
        }


        private int borderwidht;
        public int BorderWidth
        {
            get { return borderwidht; }
            set { borderwidht = value; }
        }

        public virtual float Height
        {
            get { return Rectangle.Height; }
            set { rectangle.Height = value; }
        }


        private Color fillColor;
        public virtual Color FillColor
        {
            get { return fillColor; }
            set { fillColor = value; }
        }

        private Color borderColor;
        public virtual Color BorderColor
        {            
            get { return borderColor; }
            set { borderColor = value; }
        }      
        /// <summary>
        /// Визуализира елемента.
        /// </summary>
        /// <param name="grfx">Къде да бъде визуализиран елемента.</param>
        public virtual void DrawSelf(Graphics grfx)
        {
            // shape.Rectangle.Inflate(shape.BorderWidth, shape.BorderWidth);
        }

        internal void ChangeCoordinate(string xParam, float v1, float v2)
        {
            float X = rectangle.X;
            float Y = rectangle.Y;
            float Width = rectangle.Width;
            float Height = rectangle.Height;

            if (xParam == "Left Top")
            {
                Console.WriteLine("dsa");
                rectangle.X += v1;
                rectangle.Y += v2;
                rectangle.Width -= v1;
                rectangle.Height -= v2;
            }
            else if (xParam == "Left Bottom")
            {
                rectangle.X += v1;
                rectangle.Width -= v1;
                rectangle.Height += v2;
            }
            else if (xParam == "Right Top")
            {
                rectangle.Width += v1;
                rectangle.Y += v2;
                rectangle.Height -= v2;
            }
            else if (xParam == "Right Bottom")
            {
                rectangle.Width += v1;
                rectangle.Height += v2;
            }

            if (rectangle.Width <= 15)
            {
                rectangle.X = X;
                rectangle.Width = Width;
            }

            if (rectangle.Height <= 15)
            {
                rectangle.Y = Y;
                rectangle.Height = Height;
            }
        }
    }
}