using opa.Processor;
using opa.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace opa
{
    public partial class Form1 : Form
    {

        private DialogProcesor dialogProcessor = new DialogProcesor();

        
        public Form1()
        {
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            dialogProcessor.ReDraw(sender, e);
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            dialogProcessor.AddRandomRectangle();

            panel1.Invalidate();
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            dialogProcessor.AddRandomRectangle();

            panel1.Invalidate();
        }



        private void AddEllipse_Click(object sender, EventArgs e)
        {
            dialogProcessor.AddRandomEllispe();

            panel1.Invalidate();
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (SpeedButton.Checked)
            {
                dialogProcessor.Selection = dialogProcessor.ContainsPoint(e.Location);
                var shapesel = dialogProcessor.ContainsPoint(e.Location);
                if (dialogProcessor.Selection != null)
                {
                    if (shapesel != null)
                    {
                        if (dialogProcessor.SelectionShape.Contains(shapesel))
                            dialogProcessor.SelectionShape.Remove(shapesel);
                        else
                            dialogProcessor.SelectionShape.Add(shapesel);

                        dialogProcessor.Resize(e.Location, dialogProcessor.Selection);
                        if (dialogProcessor.ResizePoint == null)
                        {
                            dialogProcessor.IsDragging = true;                            
                            panel1.Invalidate();
                        }
                        dialogProcessor.LastLocation = e.Location;
                    }
                }
                
            }
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (dialogProcessor.ResizePoint != null)
            {
                dialogProcessor.ChangeCoordinate(e.Location);
                panel1.Invalidate();    
            }
            else if (dialogProcessor.IsDragging)
            {
                if (dialogProcessor.Selection != null)                    
                dialogProcessor.Resize(e.Location,dialogProcessor.Selection);             
                dialogProcessor.TranslateTo(e.Location);
                panel1.Invalidate();
                             
            }
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            dialogProcessor.IsDragging = false;
            dialogProcessor.ResizePoint = null;
        }

        private void SpeedButton_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                dialogProcessor.SaveAs(saveFileDialog1.FileName);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                dialogProcessor.Import(openFileDialog1);
            }
            panel1.Invalidate();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {

        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dialogProcessor.DeleteSelected();
            panel1.Invalidate();
        }

        private void ColorOfShape_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                dialogProcessor.SetFillColor(colorDialog1.Color);
                panel1.Invalidate();
            }
                
        }

        private void groupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dialogProcessor.GroupSelected();
            panel1.Invalidate();
        }

        private void unGroupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dialogProcessor.UngroupSelected();
            panel1.Invalidate();
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            if (colorDialog2.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                dialogProcessor.SetFillBorder(colorDialog2.Color);
                panel1.Invalidate();
            }

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

    

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
         
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            int value = Int32.Parse(textBox1.Text);
            dialogProcessor.SetBorderWight(value);
            panel1.Invalidate();
        }

        private void rectangleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dialogProcessor.AddRandomRectangle();

            panel1.Invalidate();
        }

        private void ellipseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dialogProcessor.AddRandomEllispe();

            panel1.Invalidate();
        }
    }
}
