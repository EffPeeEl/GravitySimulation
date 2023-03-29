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

        public double GravitationalConstant { get; set; }
 
        public double Softening { get; set; }

        public double Time;

        public double distanceTick;


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
            GravitationalConstant = 1;
            Softening = 0.1;
            distanceTick = 1;
            Time = 0;
            Log = new List<string>();
       }

        public void RunSimulation()
        {
            for(int i = 0; i < 10000; i++) 
            {
                for(int j = 0; j < Bodies.Count; j++)
                {
                    Bodies[j].XVelocity += Bodies[j].XAcceleration * (distanceTick / 2);
                    Bodies[j].YVelocity += Bodies[j].YAcceleration * (distanceTick / 2);

                    Bodies[j].XCoordinate += Bodies[j].XVelocity * distanceTick;
                    Bodies[j].YCoordinate += Bodies[j].YVelocity * distanceTick;

                    Log.Insert(0, $"{Bodies[j]}: X: {Bodies[j].XCoordinate} Y: {Bodies[j].YCoordinate}");
                }

                GetAccelerations();

                for (int j = 0; j < Bodies.Count; j++)
                {
                    Bodies[j].XVelocity += Bodies[j].XAcceleration * (distanceTick / 2);
                    Bodies[j].YVelocity += Bodies[j].YAcceleration * (distanceTick / 2);

                    Log.Insert(0,$"{Bodies[j]}: X: {Bodies[j].XCoordinate} Y: {Bodies[j].YCoordinate}");
                }

                Time = Time + distanceTick;
                Latestlog = Log.FirstOrDefault();
                Log.Insert(0,$"Time: {Time}");
                
            }
        }
   

        

        public void GetAccelerations()
        {
            for(int i = 0; i < Bodies.Count; i++)
            {
                for (int j = 0; Bodies.Count > j; j++)
                {

                    double dx = Bodies[j].XCoordinate - Bodies[i].XCoordinate;
                    double dy = Bodies[j].YCoordinate - Bodies[i].YCoordinate;
                    double inv_r3 = Math.Pow((dx * dx + dy * dy + Softening * Softening),-1.5) ;

                    Bodies[i].XAcceleration += GravitationalConstant * (dx * inv_r3) * Bodies[j].Mass;
                    Bodies[i].YAcceleration += GravitationalConstant * (dy * inv_r3) * Bodies[j].Mass;


                }
            }
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
