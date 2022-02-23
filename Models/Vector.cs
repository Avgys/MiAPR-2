using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace MiAPR.Models
{
    public class Vector
    {
        public Cluster ClusterOwner { get; set; }

        public double X { get; set; }
        public double Y { get; set; }

        public Ellipse Ellipse { get; set; }

        public Vector(double x, double y)
        {
            X = x;
            Y = y;
            ClusterOwner = null;
        }

        public Vector Copy()
        {
            return new Vector(this.X, this.Y);
        }

        public void Update()
        {

        }
    }
}
