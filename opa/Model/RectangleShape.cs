﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace opa.Model
{
    [Serializable]
    public class RectangleShape : Shape
    {

       

        public RectangleShape(RectangleF rect) : base(rect)
        {
        }

        public RectangleShape(RectangleShape rectangle) : base(rectangle)
        {
        }

        public override bool Contains(PointF point)
        {
            if (base.Contains(point))
                return true;
            else
            return false;
        }

        public override void DrawSelf(Graphics grfx)
        {
            base.DrawSelf(grfx);

            grfx.FillRectangle(new SolidBrush(FillColor), Rectangle.X, Rectangle.Y, Rectangle.Width, Rectangle.Height);
            grfx.DrawRectangle(new Pen(BorderColor,BorderWidth), Rectangle.X, Rectangle.Y, Rectangle.Width, Rectangle.Height);
     
        }
    }
}
