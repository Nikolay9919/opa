﻿using opa.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace opa.Processor
{

   public class DialogProcesor : DisplayProcessor
    {    
       

        public DialogProcesor()
        {
        }

        private bool resizeshape;
        public bool ResizeShape
        {
            get { return resizeshape; }
            set { resizeshape = value; }
        }      


        private Shape selection;
        public Shape Selection
        {
            get { return selection; }
            set { selection = value; }
        }

        private List<Shape> selectionShape = new List<Shape>();
        public List<Shape> SelectionShape
        {
            get { return selectionShape; }
            set { selectionShape = value; }
        }      

        private bool isDragging;
        public bool IsDragging
        {
            get { return isDragging; }
            set { isDragging = value; }
        }

        private PointF lastLocation;
        public PointF LastLocation
        {
            get { return lastLocation; }
            set { lastLocation = value; }
        }
        
        public ShapePoint ResizePoint;
   

        public void AddRandomRectangle()
       {
          Random rnd = new Random();
           int x = rnd.Next(100, 1000);
           int y = rnd.Next(100, 600);

          RectangleShape rect = new RectangleShape(new Rectangle(x, y, 100, 300));            
           rect.FillColor = Color.White;
           rect.BorderColor = Color.Black;
           rect.BorderWidth = 5;
           ShapeList.Add(rect);
       }

        public void AddRandomEllispe()
        {
            Random rnd = new Random();
            int x = rnd.Next(100, 700);
            int y = rnd.Next(100, 1000);

            EllipseShape ellipse = new EllipseShape(new Rectangle(x, y, 100, 300));
            ellipse.BorderColor = Color.Black;
            ellipse.FillColor = Color.White;
            ellipse.BorderWidth = 5;
            ShapeList.Add(ellipse);
        }

        public void SetFillColor(Color color)
        {
            foreach (var item in SelectionShape)            
            {
                item.FillColor = color;
            }
        }


        public void SetBorderWight(int wight)
        {
            foreach (var item in SelectionShape)

            {
                item.BorderWidth = wight;
            }
        }


        public void SetFillBorder(Color color)
        {
            foreach (var item in SelectionShape)
            {
                item.BorderColor = color;
            }
        }

     public Shape ContainsPoint(PointF point)
		{
			for(int i = ShapeList.Count - 1; i >= 0; i--){
				if (ShapeList[i].Contains(point)){
					ShapeList[i].FillColor = Color.Red;
						
					return ShapeList[i];
				}	
			}
			return null;
		}

        public override void Draw(Graphics grfx)
        {
            base.Draw(grfx);
            foreach (var item in selectionShape)
            {
                grfx.DrawRectangle(Pens.Red, item.Rectangle.Left, item.Rectangle.Top, item.Rectangle.Width , item.Rectangle.Height);
                
            }
        }



        public void TranslateTo(PointF p)
        {
            foreach (var item in SelectionShape)
            {
                item.Move(p.X - lastLocation.X, p.Y - lastLocation.Y);
                lastLocation = p;
            }
        }

        public void UngroupSelected()
        {           
            List<Shape> OldSelection = new List<Shape>(SelectionShape);
            SelectionShape.Clear();
            foreach (var item in OldSelection)
            {
                if (item is GroupShape)
                {
                    GroupShape group = (GroupShape)item;
                    foreach (var subItem in group.SubItems)
                    {
                        SelectionShape.Add(subItem);
                        ShapeList.Add(subItem);
                    }

                    ShapeList.Remove(item);
                }
            }
        }

        public void GroupSelected()
        {
            if (SelectionShape.Count < 2) return;

            float minX = float.PositiveInfinity;
            float minY = float.PositiveInfinity;
            float maxX = float.NegativeInfinity;
            float maxY = float.NegativeInfinity;

            foreach (var item in SelectionShape)
            {
                if (minX > item.Location.X) minX = item.Location.X;
                if (minY > item.Location.Y) minY = item.Location.Y;
                if (maxX < item.Location.X + item.Width) maxX = item.Location.X + item.Width;
                if (maxY < item.Location.Y + item.Height) maxY = item.Location.Y + item.Height;
            }

            var group = new GroupShape(new RectangleF(minX, minY, maxX - minX, maxY - minY));

            group.SubItems = SelectionShape;

            SelectionShape = new List<Shape>();

            foreach (var item in group.SubItems)
            {
                ShapeList.Remove(item);
            }

            SelectionShape.Add(group);

            ShapeList.Add(group);          
        }
     

        public void ChangeCoordinate(PointF p)
        {
            float diffX = p.X - lastLocation.X;
            float diffY = p.Y - lastLocation.Y;
            selection.ChangeCoordinate(ResizePoint.XParam, diffX, diffY);
            lastLocation = p;      
        }

        public void Resize(PointF point, Shape rs)
        {
            for (int i = ShapeList.Count - 1; i >= 0; i--)
            {

                Shape shape = ShapeList[i];
                RectangleF rectangle = shape.Rectangle;

                var ResizePoint = shape.GetResizePoint(point);

                if (ResizePoint != null)
                {
                    this.ResizePoint = (ShapePoint)ResizePoint;
                    return;
                }
            }
        }


        public void DeleteSelected()
        {
            foreach (var item in SelectionShape)
                ShapeList.Remove(item);
            SelectionShape = new List<Shape>();
        }

        public void SelectAll()
        {
            SelectionShape = new List<Shape>(ShapeList);
        }

        public void SaveAs(string fileName)
        {
            FileStream fs = new FileStream(fileName, FileMode.Create);
            BinaryFormatter bf = new BinaryFormatter();

            bf.Serialize(fs, ShapeList);
        }


        public void Import(OpenFileDialog openFileDialog1)
        {
            Stream myStream = null;
            try
            {
                if ((myStream = openFileDialog1.OpenFile()) != null)
                {
                    using (myStream)
                    {
                        BinaryFormatter bf = new BinaryFormatter();
                        ShapeList = (List<Shape>)bf.Deserialize(myStream);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
            }
        }

    }
}       

