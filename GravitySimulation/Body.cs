using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace GravitySimulation
{
    public class Body : INotifyPropertyChanged
    {

        public Body(string color = "Red")
        {
            Name = "body";
            Mass = 10;
            Size = 10;
            isStatic = false;
            


            BrushConverter bc = new BrushConverter();
            brush = (Brush)bc.ConvertFrom(color);


        }

        public double presentableXCordinate 
        { 
            get => (Math.Round(XCoordinate - (presentableSize / 2)));
            set
            {
                OnPropertyChanged();
            }
         }
        public double presentableYCordinate
        {
            get => Math.Round(YCoordinate - (presentableSize / 2)); 
            set
            {
                OnPropertyChanged();
            }
        }

        private Brush _brush { get; set; }
        public Brush brush
        {
            get { return _brush; }
            set
            {
                if (_brush != value)
                {
                    _brush = value;
                    OnPropertyChanged();
                }
            }
        }

        public double presentableSize
        {
            get => Math.Round(Size / 100);
            set
            {
                OnPropertyChanged();
            }
        }

        private string _name { get; set; }
        public string Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _isStatic { get; set; }
        public bool isStatic
        {
            get { return _isStatic; }
            set
            {
                if (_isStatic != value)
                {
                    _isStatic = value;
                    OnPropertyChanged();
                }
            }
        }

        private double _mass;
        public double Mass
        {
            get { return _mass; }
            set
            {
                if (_mass != value)
                {
                    _mass = value;
                    OnPropertyChanged();
                }
            }
        }
        
        private double _size { get; set; }
        public double Size
        {
            get { return _size / 2 ; }
            set
            {
                if (_size != value)
                {
                    _size = value;
                    OnPropertyChanged();
                }
            }
        }
       
        private double _velocityY { get; set; }
        public double YVelocity
        {
            get { return _velocityY; }
            set
            {
                if (_velocityY != value)
                {
                    _velocityY = value;
                    OnPropertyChanged();
                }
            }
        }
        
        private double _velocityX { get; set; }
        public double XVelocity
        {
            get { return _velocityX; }
            set
            {
                if (_velocityX != value)
                {
                    _velocityX = value;
                    OnPropertyChanged();
                }
            }
        }

        private double _xCoordinate { get; set; }
        public double XCoordinate
        {
            get { return _xCoordinate; }
            set
            {
                if (_xCoordinate != value)
                {
                    presentableXCordinate = value;
                    _xCoordinate = value;
                    OnPropertyChanged();
                }
            }
        }

        private double _yCoordinate { get; set; }
        public double YCoordinate
        {
            get { return _yCoordinate; }
            set
            {
                if (_yCoordinate != value)
                {
                    presentableYCordinate = value;
                    _yCoordinate = value;
                    OnPropertyChanged();
                }
            }
        }

        private double _yAcceleration { get; set; }
        public double YAcceleration
        {
            get { return _yAcceleration; }
            set
            {
                if (_yAcceleration != value)
                {
                    _yAcceleration = value;
                    OnPropertyChanged();
                }
            }
        }

        private double _xAcceleration { get; set; }
        public double XAcceleration
        {
            get { return _xAcceleration; }
            set
            {
                if (_xAcceleration != value)
                {
                    _xAcceleration = value;
                    OnPropertyChanged();
                }
            }
        }



        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        public override string ToString()
        {
            return Name;
        }

    }
}
