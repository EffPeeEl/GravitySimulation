using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
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
            UIBodyToAdd = new Body("Green")
            {
                Size = 5000
            };

        }



        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }



        private double _canvasWidth;
        public double CanvasWidth
        {
            get { return _canvasWidth; ; }
            set
            {
                if (_canvasWidth != value)
                {
                    _canvasWidth = value; 
                    OnPropertyChanged();
                }
            }
        }
        private double _canvasHeight;
        public double CanvasHeight
        {
            get { return _canvasHeight; ; }
            set
            {
                if (_canvasHeight != value)
                {
                    _canvasHeight = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _inputBodyName;
        public string InputBodyName
        {
            get { return _inputBodyName; ; }
            set
            {
                if (_inputBodyName != value)
                {
                    _inputBodyName = value;
                    OnPropertyChanged();
                }
            }
        }

        private double _inputBodyMass;
        public double InputBodyMass
        {
            get { return _inputBodyMass; ; }
            set
            {
                if (_inputBodyMass != value)
                {
                    _inputBodyMass = value;
                    OnPropertyChanged();
                }
            }
        }

        

        public double InputBodySize
        {
            get { return UIBodyToAdd.Size; ; }
            set
            {
                UIBodyToAdd.Size = value;
                OnPropertyChanged();
                OnPropertyChanged("UIBodyCordXY");

            }
        }

        private double _uiBodyCordXY;
        public double UIBodyCordXY
        {
            get { return 50 - (UIBodyToAdd.presentableSize / 2); ; }
            set
            {
                OnPropertyChanged();

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

        private Body _uiBodyToAdd { get; set; }
        public Body UIBodyToAdd
        {
            get { return _uiBodyToAdd; ; }
            set
            {
                if (_uiBodyToAdd != value)
                {
                    _uiBodyToAdd = value;
                    OnPropertyChanged();
                }
            }
        }


        public void MoveSimulation()
        {

            // random bodies
            //for (int i = 0; i < 2; i++)
            //{
            //    Random r= new Random();
            //    bodies.Add(new Body()
            //    {
            //        XCoordinate = r.NextDouble() * 1000,
            //        YCoordinate = r.NextDouble() * 1000,
            //        YVelocity = r.NextDouble() ,
            //        XVelocity = r.NextDouble() ,
            //        Mass = r.NextDouble() * 1000,
            //        Size = r.NextDouble() * 100
            //    });
            //
            
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
            foreach (Grid g in GridViews.Children)
            {
                g.Visibility = Visibility.Collapsed;
            }
            sender.Visibility = Visibility.Visible;
        }

        private void GravityCanvas_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            CanvasHeight = GravityCanvas.ActualHeight;
            CanvasWidth = GravityCanvas.ActualWidth;
        }

        private void SpawnDefBodies_Click(object sender, RoutedEventArgs e)
        {
            simulation = new Simulation();
            var body1 = new Body("Blue")
            {
                Name = "Earth",
                Mass = 5.972e24 / 1.0e19,
                Size = 6371,
                XVelocity = 0,
                YVelocity = 0,
                XCoordinate = 400,
                YCoordinate = 400,
                ZCoordinate = 0,
                isStatic = true,
            };
            var body2 = new Body("Red")
            {
                Name = "Mars",
                Mass = 6.39e23/ 1.0e19,
                Size = 3389,
                XVelocity = 0,
                YVelocity = -6.67e-4,
                XCoordinate = 300,
                YCoordinate = 400,
            };


            var body4 = new Body("Black")
            {
                Name = "Moon",
                Mass = 7.34767e22 / 1.0e19,
                Size = 1737 ,
                XVelocity = 0,
                YVelocity = -3.67e-4,
                XCoordinate = 245,
                YCoordinate = 0,
            };


            simulation.AddBodies(body1);
            simulation.AddBodies(body2);
            simulation.AddBodies(body4);
        }

        private void AddBodyButton_Click(object sender, RoutedEventArgs e)
        {

        }



        private void PauseSimButton_Click(object sender, RoutedEventArgs e)
        {
            simulation.Pause();
        }
    }


}
