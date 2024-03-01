using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media;

namespace GravitySimulation
{
    public class Simulation : INotifyPropertyChanged
    {
        private ObservableCollection<Body> _bodies { get; set; }
        public ObservableCollection<Body> Bodies
        {
            get { return _bodies; }
            set
            {
                if (_bodies != value)
                {
                    _bodies = value;
                    OnPropertyChanged();
                }
            }
        }

        public double G  { get; set; } // Gravitational Constant
 
        public double Softening { get; set; }

        private double _time { get; set; }
        public double Time
        {
            get { return _time; }
            set
            {
                if (_time != value)
                {
                    _time = value;
                    OnPropertyChanged();
                }
            }
        }

        private double _timeStep { get; set; }
        public double TimeStep
        {
            get { return _timeStep; }
            set
            {
                if (_timeStep != value)
                {
                    _timeStep = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _timeDelay { get; set; }
        public int TimeDelay
        {
            get { return _timeDelay; }
            set
            {
                if (_timeDelay != value)
                {
                    _timeDelay = value;
                    OnPropertyChanged();
                }
            }
        }

        private List<string> _log { get; set; }
        public List<string> Log
        {
            get { return _log; }
            set
            {
                if (_log != value)
                {
                    _log = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _latestlog { get; set; }
        public string Latestlog
        {
            get { return _latestlog; }
            set
            {
                if (_latestlog != value)
                {
                    _latestlog = value;
                    OnPropertyChanged();
                }
            }
        }

        public Simulation(ObservableCollection<Body> bodies)
        {
            this.Bodies = bodies;
            G = 6.67e-11;
            Softening = 10;
            TimeStep = 10.0;
            Time = 0;
            Log = new List<string>();
        }
        public Simulation()
        {
            this.Bodies = new ObservableCollection<Body>();
            G = 6.67e-11;
            Softening = 10;
            TimeStep = 10.0;
            Time = 0;
            Log = new List<string>();
        }


        private bool _isRunning { get; set; }
        public bool IsRunning
        {
            get { return _isRunning; }
            set
            {
                if (_isRunning != value)
                {
                    _isRunning = value;
                    OnPropertyChanged();
                }
            }
        }
        public void RunSimulation()
        {
            IsRunning = true;
            while(IsRunning)
            {
                for (int j = 0; j < Bodies.Count; j++)
                {

                    if (!Bodies[j].isStatic)
                    {
                        Bodies[j].XVelocity += Bodies[j].XAcceleration * (TimeStep / 2);
                        Bodies[j].YVelocity += Bodies[j].YAcceleration * (TimeStep / 2);
                        Bodies[j].ZVelocity += Bodies[j].ZAcceleration * (TimeStep / 2);

                        Bodies[j].XCoordinate += Bodies[j].XVelocity * TimeStep;
                        Bodies[j].YCoordinate += Bodies[j].YVelocity * TimeStep;
                        Bodies[j].ZCoordinate += Bodies[j].ZVelocity * TimeStep;

                    }

                }
                GetAccelerations();

                for (int j = 0; j < Bodies.Count; j++)
                {
                    if (!Bodies[j].isStatic)
                    {
                        Bodies[j].XVelocity += Bodies[j].XAcceleration * (TimeStep / 2);
                        Bodies[j].YVelocity += Bodies[j].YAcceleration * (TimeStep / 2);
                        Bodies[j].ZVelocity += Bodies[j].ZAcceleration * (TimeStep / 2);
                    }

                }
                Time += TimeStep;
                Thread.Sleep(TimeDelay);

            }


        }
   

        public void GetAccelerations()
        {
            for(int i = 0; i < Bodies.Count; i++)
            {

                if (!Bodies[i].isStatic)
                {
                    Bodies[i].XAcceleration = 0.0;
                    Bodies[i].YAcceleration = 0.0;
                    Bodies[i].ZAcceleration = 0.0;
                    for (int j = 0; Bodies.Count > j; j++)
                    {

                        if (i != j)
                        {
                            //double dx = Bodies[j].XCoordinate - Bodies[i].XCoordinate;
                            //double dy = Bodies[j].ZCoordinate - Bodies[i].ZCoordinate;
                            //double inv_r3 = Math.Pow((dx * dx + dy * dy + Softening * Softening), -1.5);

                            //Bodies[i].XAcceleration += G * (dx * inv_r3) * Bodies[j].Mass;
                            //Bodies[i].YAcceleration += G * (dy * inv_r3) * Bodies[j].Mass;

                            double dx = Bodies[j].XCoordinate - Bodies[i].XCoordinate;
                            double dy = Bodies[j].YCoordinate - Bodies[i].YCoordinate;
                            double dz = Bodies[j].ZCoordinate - Bodies[i].ZCoordinate;
                            double distanceSquared = dx * dx + dy * dy + dz * dz + Softening * Softening;
                            double inv_r3 = Math.Pow(distanceSquared, -1.5);

                            Bodies[i].XAcceleration += G * (dx * inv_r3) * Bodies[j].Mass;
                            Bodies[i].YAcceleration += G * (dy * inv_r3) * Bodies[j].Mass;
                            Bodies[i].ZAcceleration += G * (dz * inv_r3) * Bodies[j].Mass;


                        }

                    }
                }


            }
        }


        //public bool CollisionChecker(Body body1, Body body2)
        //{

        //    if(body1.XCoordinate + body1.Size )
        //    return false;

        //}


        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void AddBodies(Body body1) => Bodies.Add(body1);



        public void Pause()
        {
            IsRunning = false;
        }

    }
}
