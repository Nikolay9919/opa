using opa.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace opa.Processor
{
    public class DisplayProcessor
    {
        public DisplayProcessor()
        {
        }
    

        private List<Shape> shapeList1 = new List<Shape>();
        public List<Shape> ShapeList1
        {
            get { return shapeList1; }
            set { shapeList1 = value; }
        }

        private List<Shape> shapeList = new List<Shape>();
        public List<Shape> ShapeList
        {
            get { return shapeList; }
            set { shapeList = value; }            
        }

        public void ReDraw(object sender, PaintEventArgs e)
        {           
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            Draw(e.Graphics);
        }

        public virtual void Draw(Graphics grfx)
        {
            foreach (Shape item in ShapeList)
            {
                DrawShape(grfx, item);                               
            }
        }   

        public virtual void DrawShape(Graphics grfx, Shape item)
        {
            item.DrawSelf(grfx);
        }

        public virtual void ChangeSizeRectangle(Graphics grfx, Shape item)
        {
          
        }

    }
}
