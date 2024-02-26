using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
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

        public double presentableZCordinate
        {
            get => Math.Round(ZCoordinate - (presentableSize / 2)); 
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

        //public double presentableSize
        //{
        //    get => Math.Round(Size / 100);
        //    set
        //    {
        //        OnPropertyChanged();
        //    }
        //}
        public double presentableSize
        {
            get
            {
                // Assuming the farther away (higher ZCoordinate), the smaller the size.
                // Adjust 'depthFactor' according to how sensitive you want the size to be relative to ZCoordinate.
                double depthFactor = 1 + (ZCoordinate / 1000); // Example scaling factor - adjust as needed.
                return Math.Round(Size / 100 / depthFactor); // Divide by 'depthFactor' to scale size by distance.
            }
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
        

        // Radius in KM
        private double _size { get; set; }
        public double Size
        {
            get { return _size ; }
            set
            {
                if (_size != value)
                {
                    _size = value;

                    OnPropertyChanged();
                    OnPropertyChanged("presentableSize");
                }
            }
        }


        #region Velocities
        private double _yVelocity { get; set; }
        public double YVelocity
        {
            get { return _yVelocity; }
            set
            {
                if (_yVelocity != value)
                {
                    _yVelocity = value;
                    OnPropertyChanged();
                }
            }
        }
        
        private double _xVelocity { get; set; }
        public double XVelocity
        {
            get { return _xVelocity; }
            set
            {
                if (_xVelocity != value)
                {
                    _xVelocity = value;
                    OnPropertyChanged();
                }
            }
        }

        private double _zVelocity { get; set; }
        public double ZVelocity
        {
            get { return _zVelocity; }
            set
            {
                if (_zVelocity != value)
                {
                    _zVelocity = value;
                    OnPropertyChanged();
                }
            }
        }
        #endregion

        #region Coordinates
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

        private double _zCoordinate { get; set; }
        public double ZCoordinate
        {
            get { return _zCoordinate; }
            set
            {
                if (_zCoordinate != value)
                {
                    _zCoordinate = value;
                    OnPropertyChanged();
                    OnPropertyChanged("presentableZCordinate");
                    OnPropertyChanged("presentableSize");
                }
            }
        }
        #endregion

        #region Acceleration
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


        private double _zAcceleration { get; set; }
        public double ZAcceleration
        {
            get { return _zAcceleration; }
            set
            {
                if (_zAcceleration != value)
                {
                    _zAcceleration = value;
                    OnPropertyChanged();
                }
            }
        }
        #endregion



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
