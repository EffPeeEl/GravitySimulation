using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GravitySimulation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : INotifyPropertyChanged
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            BodyXAxis = 5;
            BodyYAxis = 5;

  
            
        }


        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        private decimal _bodyXAxis;
        public decimal BodyXAxis
        {
            get { return _bodyXAxis; }
            set
            {
                if (_bodyXAxis != value)
                {
                    _bodyXAxis = value;
                    OnPropertyChanged();
                }
            }
        }
        private decimal _bodyYAxis;
        public decimal BodyYAxis
        {
            get { return _bodyYAxis; }
            set
            {
                if (_bodyYAxis != value)
                {
                    _bodyYAxis = value;
                    OnPropertyChanged();
                }
            }
        }
        private ObservableCollection<Body> _gravBodies;
        public ObservableCollection<Body> GravBodies
        {
            get { return _gravBodies; ; }
            set
            {
                if (_gravBodies != value)
                {
                    _gravBodies = value;
                    OnPropertyChanged();
                }
            }
        }

        private Simulation _simulation { get; set; }
        public Simulation simulation
        {
            get { return _simulation; ; }
            set
            {
                if (_simulation != value)
                {
                    _simulation = value;
                    OnPropertyChanged();
                }
            }
        }




        public void MoveSimulation()
        {
            
            var body1 = new Body("Blue")
            {
                Name = "Earth",
                Mass = 10,
                Size = 15,
                XVelocity = 0,
                YVelocity = -0.5,
                XCoordinate = 400,
                YCoordinate = 400,
            };
            var body2 = new Body("Red")
            {
                Name = "Mars",
                Mass = 0.0000001,
                Size = 30,
                XVelocity = 0,
                YVelocity = 0,
                XCoordinate = 300,
                YCoordinate = 400,
            };
            
    

            var bodies = new ObservableCollection<Body> { body1, body2 };

            for (int i = 0; i < 10; i++)
            {
                Random r= new Random();
                bodies.Add(new Body()
                {
                    XCoordinate = r.NextDouble() * 1000,
                    YCoordinate = r.NextDouble() * 1000,
                    YVelocity = r.NextDouble() * 25,
                    XVelocity = r.NextDouble() * 25,
                    Mass = r.NextDouble() * 1000,
                    Size = r.NextDouble() * 10
                });
            }
            simulation = new Simulation(bodies);

            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                simulation.RunSimulation();

            }).Start();

     
            

        }


        private void MoveBodyButton_Click(object sender, RoutedEventArgs e)
        {
            MoveSimulation();

        }

        private void GravityMenuButton_Click(object sender, RoutedEventArgs e)
        {
            OpenPage(ViewRootGrid);
        }

        private void AnalysisButton_Click(object sender, RoutedEventArgs e)
        {
            OpenPage(AnalysisGrid);
        }

        private void InternetButton_Click(object sender, RoutedEventArgs e)
        {
            OpenPage(InternetGrid);
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            OpenPage(SettingsGrid);
        }

        private void EvalButton_Click(object sender, RoutedEventArgs e)
        {
            OpenPage(EvalGrid);
        }

        private void OpenPage(Grid sender)
        {
            foreach (Grid g in ViewRootGrid.Children)
            {
                g.Visibility = Visibility.Collapsed;
            }
            sender.Visibility = Visibility.Visible;
        }


    }

}
