using opa.Processor;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace opa.Model
{
    [Serializable]
    public class GroupShape : Shape
    {
        #region Constructor  

        DialogProcesor dia = new DialogProcesor();

        public GroupShape(RectangleF rect)
            : base(rect)
        {
            SubItems = new List<Shape>();
        }

        public GroupShape(GroupShape rectangle)
            : base(rectangle)
        {
            SubItems = new List<Shape>();
        }

        #endregion

        public List<Shape> SubItems { get; set; }

        public override bool Contains(PointF point)
        {
            if (base.Contains(point))
            {               
                foreach (var item in SubItems)
                {
                    if (item.Contains(point)) return true;
                }
                return true;
            }
            else               
                return false;
        }


        public override void Move(float dx, float dy)
        {
            base.Move(dx, dy);

            foreach (var item in SubItems)
            {
                item.Move(dx, dy);
            }
        }


        public override void DrawSelf(Graphics grfx)
        {
            base.DrawSelf(grfx);

            foreach (var item in SubItems)
            {
                item.DrawSelf(grfx);
            }

        }      
     
    
    }
}
