using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows;
using MiAPR.Models;
using Vector = MiAPR.Models.Vector;

namespace MiAPR
{   

    public class MainLogic
    {
        private readonly Canvas _canvas;
        private List<Vector> _points;
        //private List<Vector> _centers;
        //private List<List<Vector>> _pointsClasses;
        private List<Cluster> _clusters { get; set; }
        private readonly List<SolidColorBrush> brushes;

        public MainLogic(Canvas canvas)
        {
            brushes = new List<SolidColorBrush>()
            {
                Brushes.Black,
                Brushes.Red,
                Brushes.Yellow,
                Brushes.Green,
                Brushes.Blue,
                Brushes.Magenta,
                Brushes.Gray,
                Brushes.LightCyan
            };
            _canvas = canvas;
            _points = new List<Vector>();
            _clusters = new();
        }

        public void DrawPoints(int numP = 30000, int numC = 6)
        {
            _points.Clear();
            _canvas.Children.Clear();
            _clusters.Clear();                        
            
            //Gen Points
            numP = numP % 100000;
            if (numP < 10000)
            {
                numP = 10000;
            }
            if (numC < 1)
            {
                numC = 1;
            }
            var rand = new Random();
            Vector vector;
            Ellipse elipse;
            for (int i = 0; i < numP; i++)
            {
                vector = new Vector(rand.Next((int)Math.Floor(_canvas.ActualWidth)), rand.Next((int)Math.Floor(_canvas.ActualHeight)));    
                
                elipse = new Ellipse();
                elipse.Width = 2;
                elipse.Height = 2;
                elipse.StrokeThickness = 1;
                elipse.Margin = new Thickness(vector.X - 1, vector.Y - 1, 0, 0);
                vector.Ellipse = elipse;

                _canvas.Children.Add(elipse);
                _points.Add(vector);
            }
                      

            var centerId = rand.Next(_points.Count);
            vector = _points[centerId];
            var cluster = new Cluster()
            {
                Center = vector,
                Id = 0
            };
            _clusters.Add(cluster);
            cluster.ellipse.Stroke = brushes[cluster.Id];
            cluster.setEllipseMargin(vector);
            _canvas.Children.Add(cluster.ellipse);
            SeparateZones();
        }

        public List<Vector> GetFutherestInClass() {
            var list = new List<List<Vector>>();

            for (int i = 0; i < _clusters.Count; i++)
            {

            }
        }

        public bool CreateNewCenter()
        {
            if (_clusters.Count == 1)
            {
                var center = _clusters[0].Center;

                //Find futherest point to center
                double maxDest = GetDistance(center, _points[0]);
                Vector vector = _points[0];
                for (int i = 1; i < _points.Count; i++)
                {
                    double dst = GetDistance(center, _points[i]);
                    if (dst > maxDest)
                    {
                        maxDest = dst;
                        vector = _points[1];
                    }
                }

                Cluster cluster = new()
                {
                    Id = 1,
                    Center = vector
                };
                cluster.ellipse.Stroke = brushes[cluster.Id];
                cluster.setEllipseMargin(vector);
                vector.ClusterOwner = cluster;
                _canvas.Children.Add(cluster.ellipse);
                _clusters.Add(cluster);

            }
            else
            {
                var points = GetFutherestInClass();
                double maxDest = GetDistance(center, _points[0]);
                Vector vector = _points[0];
                for (int i = 1; i < _points.Count; i++)
                {
                    double dst = GetDistance(center, _points[i]);
                    if (dst > maxDest)
                    {
                        maxDest = dst;
                        vector = _points[1];
                    }
                }
            }

            //bool flag = false;
            //for (int i = 0; i < _pointsClasses.Count; i++)
            //{
            //    double sumX = 0;
            //    double sumY = 0;
            //    for (int k = 0; k < _pointsClasses[i].Count; k++)
            //    {
            //        sumX += _pointsClasses[i][k].X;
            //        sumY += _pointsClasses[i][k].Y;
            //    }

            //    var x = Math.Floor(sumX / _pointsClasses[i].Count);
            //    if (_centers[i].X != x)
            //    {
            //        flag = true;
            //    }
            //    _centers[i].X = x;
            //    var y = Math.Floor(sumY / _pointsClasses[i].Count);
            //    if (_centers[i].Y != y)
            //    {
            //        flag = true;
            //    }
            //    _centers[i].Y = y;
            //    _centers[i].Ellipse.Margin = new Thickness(_centers[i].X - 3, _centers[i].Y - 3, 0, 0);
            //}

            SeparateZones();
            return false;
        }

        public double GetDistance(Vector point1, Vector point2)
        {
            return Math.Pow((point1.X - point2.X), 2) + Math.Pow(point1.Y - point2.Y, 2);
        }

        public void SeparateZones()
        {
            for (int i = 0; i < _clusters.Count; i++)
            {
                _clusters[i].Vectors.Clear();
            }

            for (int i = 0; i < _points.Count; i++)
            {
                double lengthToCenter;
                if (_points[i].ClusterOwner != null)
                {
                    lengthToCenter = GetDistance(_points[i], _points[i].ClusterOwner.Center);
                }
                else
                {
                    lengthToCenter = double.MaxValue;
                }
                for (int j = 0; j < _clusters.Count; j++)
                {
                    double length = GetDistance(_points[i], _clusters[j].Center);
                    if (length < lengthToCenter)
                    {
                        lengthToCenter = length;
                        _points[i].ClusterOwner = _clusters[j];
                    }
                }
                _points[i].ClusterOwner.Vectors.Add(_points[i]);
                _points[i].Ellipse.Stroke = brushes[_points[i].ClusterOwner.Id];
                
            }
        }
    }
}
