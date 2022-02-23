using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Shapes;

namespace MiAPR.Models
{
    public class Cluster
    {
        public Vector Center { get; set; }
        public List<Vector> Vectors { get; set; } = new();
        public Ellipse ellipse;
        public int Id;
        public Cluster()
        {
            ellipse = new Ellipse();
            ellipse.Width = 6;
            ellipse.Height = 6;
            ellipse.StrokeThickness = 2;
        }

        public void setEllipseMargin(Vector vector)
        {
            ellipse.Margin = new Thickness(vector.X - 3, vector.Y - 3, 0, 0);
        }
    }
}
