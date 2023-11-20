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

        public List<string> _log { get; set; }
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

        public string _latestlog { get; set; }
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


        public void RunSimulation()
        {
            for(int i = 0; i < 1000000; i++) 
            {


                for(int j = 0; j < Bodies.Count; j++)
                {
                    Bodies[j].XVelocity += Bodies[j].XAcceleration * (TimeStep / 2);
                    Bodies[j].YVelocity += Bodies[j].YAcceleration * (TimeStep / 2);

                    Bodies[j].XCoordinate += Bodies[j].XVelocity * TimeStep;
                    Bodies[j].YCoordinate += Bodies[j].YVelocity * TimeStep;

                }

                GetAccelerations();

                for (int j = 0; j < Bodies.Count; j++)
                {
                    Bodies[j].XVelocity += Bodies[j].XAcceleration * (TimeStep / 2);
                    Bodies[j].YVelocity += Bodies[j].YAcceleration * (TimeStep / 2);

                }

                Time += TimeStep;
                Thread.Sleep(TimeDelay);

            }
        }
   

        

        public void GetAccelerations()
        {
            for(int i = 0; i < Bodies.Count; i++)
            {
                Bodies[i].XAcceleration = 0.0;
                Bodies[i].YAcceleration = 0.0;
                for (int j = 0; Bodies.Count > j; j++)
                {

                    if (i != j)
                    {
                        double dx = Bodies[j].XCoordinate - Bodies[i].XCoordinate;
                        double dy = Bodies[j].YCoordinate - Bodies[i].YCoordinate;
                        double inv_r3 = Math.Pow((dx * dx + dy * dy + Softening * Softening), -1.5);

                        Bodies[i].XAcceleration += G * (dx * inv_r3) * Bodies[j].Mass;
                        Bodies[i].YAcceleration += G * (dy * inv_r3) * Bodies[j].Mass;
                    }
     


                }
            }
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void AddBodies(Body body1) => Bodies.Add(body1);

    }
}
