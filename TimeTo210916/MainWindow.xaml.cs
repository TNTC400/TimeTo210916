using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
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
using System.Windows.Threading;
using System.IO;

namespace TimeTo210916
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private RemainingTime remainingTime;
        private DispatcherTimer dispatcherTimer;
        private string configPath = System.IO.Path.GetFullPath("config.txt");
        public MainWindow()
        {
            InitializeComponent();

            LocateWindow();
            InitVars();
            this.DataContext = remainingTime;
            UpdateContent();
            InitEvents();
        }

        private void LocateWindow()
        {
            string leftStr = "-1";
            string topStr = "-1";
            if (File.Exists(configPath))
            {
                string[] configs = File.ReadAllLines(configPath);
                leftStr = configs[0].Replace("WindowLeft: ", "").Trim();
                topStr = configs[1].Replace("WindowTop: ", "").Trim();
            }

            double windowLeft, windowTop;
            double.TryParse(leftStr, out windowLeft);
            double.TryParse(topStr, out windowTop);

            if(windowLeft >= 0 && windowTop >= 0)
            {
                this.Left = windowLeft;
                this.Top = windowTop;
            }
            else
            {
                this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            }

        }

        private void InitEvents()
        {
            this.MouseLeftButtonDown += MainWindow_MouseLeftButtonDown;
        }

        private void MainWindow_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();

            SaveWindowPosition();
        }

        private void SaveWindowPosition()
        {
            string[] positions = new string[2];
            positions[0] = string.Format("WindowLeft: {0}", this.Left);
            positions[1] = string.Format("WindowTop: {0}", this.Top);

            File.WriteAllLines(configPath, positions);
        }

        private void InitVars()
        {
            remainingTime = new RemainingTime(0, 0, 0, 0);
            CreateTimer();
        }

        private void CreateTimer()
        {
            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Interval = TimeSpan.FromSeconds(1);
            dispatcherTimer.Tick += DispatcherTimer_Tick;

            dispatcherTimer.Start();
        }

        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            UpdateContent();
        }

        private void UpdateContent()
        {
            TimeSpan remainTime = new DateTime(2021, 9, 16) - DateTime.Now;
            int days = remainTime.Days;
            int hours = remainTime.Hours;
            int mins = remainTime.Minutes;
            int secs = remainTime.Seconds;

            remainingTime.UpdateTime(days, hours, mins, secs);
        }
    }

    public class RemainingTime : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        private int days, hours, mins, secs;

        public int Days
        {
            get
            {
                return this.days;
            }
            set
            {
                if (this.days != value)
                {
                    this.days = value;
                    OnPropertyChanged("Days");
                }
            }
        }

        public int Hours
        {
            get
            {
                return this.hours;
            }
            set
            {
                if (this.hours != value)
                {
                    this.hours = value;
                    OnPropertyChanged("Hours");
                }
            }
        }

        public int Mins
        {
            get
            {
                return this.mins;
            }
            set
            {
                if (this.mins != value)
                {
                    this.mins = value;
                    OnPropertyChanged("Mins");
                }
            }
        }

        public int Secs
        {
            get
            {
                return this.secs;
            }
            set
            {
                if (this.secs != value)
                {
                    this.secs = value;
                    OnPropertyChanged("Secs");
                }
            }
        }

        public RemainingTime(int remainDays, int remainHours, int remainMins, int remainSecs)
        {
            Days = remainDays;
            Hours = remainHours;
            Mins = remainMins;
            Secs = remainSecs;
        }

        public void UpdateTime(int remainDays, int remainHours, int remainMins, int remainSecs)
        {
            Days = remainDays;
            Hours = remainHours;
            Mins = remainMins;
            Secs = remainSecs;
        }
    }
}
