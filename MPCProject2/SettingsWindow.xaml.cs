using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace MPCProject2
{
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : INotifyPropertyChanged
    {
        private double _speedValue = double.Parse(Application.Current.Properties["Speed"].ToString());
        private int _totalCountValue = Int32.Parse(Application.Current.Properties["TotalCount"].ToString());
        private int _pauseValue = Int32.Parse(Application.Current.Properties["Pause"].ToString());
        private int _sizeValue = Int32.Parse(Application.Current.Properties["Size"].ToString());
        private int _soundValue = Int32.Parse(Application.Current.Properties["Sound"].ToString());
        private int _interactionTime = Int32.Parse(Application.Current.Properties["InteractionTime"].ToString());
        private bool _randomOrder = bool.Parse(Application.Current.Properties["RandomOrder"].ToString());
        private bool _randomOrderImage = bool.Parse(Application.Current.Properties["RandomOrderImage"].ToString());
        private List<ImageObject> _allImages = ((List<ImageObject>)Application.Current.Properties["AllImages"]);

        private bool _showDieren;
        private bool _showKerstmis;
        private bool _showVoertuigen;
        private bool _showHalloween;
        private List<AfbeeldingType> _showList = new List<AfbeeldingType>();

        private List<MovementObject> _allMovements =
            (List<MovementObject>) Application.Current.Properties["AllMovements"];

        private string _backgroundColor = Application.Current.Properties["BackgroundColor"].ToString();


        public string BackgroundColor
        {
            get { return _backgroundColor; }
            set
            {
                if (value != _backgroundColor)
                {
                    _backgroundColor = value;
                    NotifyPropertyChanged("BackgroundColor");
                }
            }
        }
        public double Speed
        {
            get { return _speedValue; }
            set
            {
                if (value != _speedValue)
                {
                    _speedValue = value;
                    NotifyPropertyChanged("Speed");
                }
            }
        }
        public int TotalCount
        {
            get { return _totalCountValue; }
            set
            {
                if (value != _totalCountValue)
                {
                    _totalCountValue = value;
                    NotifyPropertyChanged("TotalCount");
                }
            }
        }
        public int Pause
        {
            get { return _pauseValue; }
            set
            {
                if (value != _pauseValue)
                {
                    _pauseValue = value;
                    NotifyPropertyChanged("Pause");
                }
            }
        }
        public int Size
        {
            get { return _sizeValue; }
            set
            {
                if (value != _sizeValue)
                {
                    _sizeValue = value;
                    NotifyPropertyChanged("Size");
                }
            }
        }
        public int Sound
        {
            get { return _soundValue; }
            set
            {
                if (value != _soundValue)
                {
                    _soundValue = value;
                    NotifyPropertyChanged("Sound");
                }
            }
        }
        public int InteractionTime
        {
            get { return _interactionTime; }
            set
            {
                if (value != _interactionTime)
                {
                    _interactionTime = value;
                    NotifyPropertyChanged("InteractionTime");
                }
            }
        }
        public bool RandomOrder
        {
            get { return _randomOrder; }
            set
            {
                if (value != _randomOrder)
                {
                    _randomOrder = value;
                    NotifyPropertyChanged("RandomOrder");
                }
            }
        }
        public bool RandomOrderImage
        {
            get { return _randomOrderImage; }
            set
            {
                if (value != _randomOrderImage)
                {
                    _randomOrderImage = value;
                    NotifyPropertyChanged("RandomOrderImage");
                }
            }
        }
        public List<MovementObject> AllMovements
        {
            get { return _allMovements; }
            set
            {
                if (value != _allMovements)
                {
                    _allMovements = value;
                    NotifyPropertyChanged("AllMovements");
                }
            }
        }
        public List<ImageObject> AllImages
        {
            get { return _allImages; }
            set
            {
                if (value != _allImages)
                {
                    _allImages = value;
                    NotifyPropertyChanged("AllImages");
                }
            }
        }
        public bool ShowDieren
        {
            get { return _showDieren; }
            set
            {
                if (value != _showDieren)
                {
                    _showDieren = value;
                    NotifyPropertyChanged("ShowDieren");
                    UpdateShowList();
                }
            }
        }
        public bool ShowKerstmis
        {
            get { return _showKerstmis; }
            set
            {
                if (value != _showKerstmis)
                {
                    _showKerstmis = value;
                    NotifyPropertyChanged("ShowKerstmis");
                    UpdateShowList();
                }
            }
        }
        public bool ShowVoertuigen
        {
            get { return _showVoertuigen; }
            set
            {
                if (value != _showVoertuigen)
                {
                    _showVoertuigen = value;
                    NotifyPropertyChanged("ShowVoertuigen");
                    UpdateShowList();
                }
            }
        }
        public bool ShowHalloween
        {
            get { return _showHalloween; }
            set
            {
                if (value != _showHalloween)
                {
                    _showHalloween = value;
                    NotifyPropertyChanged("ShowHalloween");
                    UpdateShowList();
                }
            }
        }
        public List<AfbeeldingType> ShowList
        {
            get { return _showList; }
            set
            {
                if (value != _showList)
                {
                    _showList = value;
                    NotifyPropertyChanged("ShowList");
                }
            }
        }

        public SettingsWindow()
        {
            InitializeComponent();
        }

        public void UpdateShowList()
        {
            ShowList.Clear();
            if (ShowDieren)
                ShowList.Add(AfbeeldingType.Dier);
            if (ShowKerstmis)
                ShowList.Add(AfbeeldingType.Kertmis);
            if (ShowVoertuigen)
                ShowList.Add(AfbeeldingType.Voertuigen);
            if (ShowHalloween)
                ShowList.Add(AfbeeldingType.Halloween);

            if (ShowList.Count == 0)
            {
                foreach (var image in AllImages)
                {
                    image.ShowMe = true;
                }
            }
            else
            {
                foreach (var image in AllImages)
                {
                    image.ShowMe = ShowList.Any(a => image.Type.Contains(a));
                }
            }
        }

        private void ReturnToMain_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Properties["Speed"] = _speedValue;
            Application.Current.Properties["TotalCount"] = _totalCountValue;
            Application.Current.Properties["Pause"] = _pauseValue;
            Application.Current.Properties["Size"] = _sizeValue;
            Application.Current.Properties["Sound"] = _soundValue;
            Application.Current.Properties["RandomOrder"] = _randomOrder;
            Application.Current.Properties["RandomOrderImage"] = _randomOrderImage;
            Application.Current.Properties["InteractionTime"] = _interactionTime;

            MainWindow window = new MainWindow();
            window.Show();
            Close();
        }

        private void ClrPcker_Background_SelectedColorChanged(object sender, EventArgs e)
        {
            Application.Current.Properties["BackgroundColor"] = _backgroundColor;
        }

        protected void NotifyPropertyChanged(String propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }

    public class RadioBoolToIntConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int integer = (int) value;
            if (integer == int.Parse(parameter.ToString()))
                return true;
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return parameter;
        }
    }

    public sealed class VisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (Boolean)value ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
