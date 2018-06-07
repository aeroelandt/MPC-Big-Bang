using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Shapes;
using System.Windows.Threading;
using WpfAnimatedGif;

namespace MPCProject2
{
    /// <summary>
    /// Interaction logic for GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        private bool _hasBegun = false;
        private bool _bgwStarted = false;
        private bool _canFinish = true;
        private BackgroundWorker bgw;

        public GameWindow()
        {
            KeyDown += gameWindow_KeyDown;
            bgw = new BackgroundWorker();
            bgw.DoWork += new DoWorkEventHandler(Bgw_DoWork);
            bgw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(Bgw_Finished);
            InitializeComponent();
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            ImageMovementUserControl.GameWindow = this;
        }

        private void gameWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                MainWindow window = new MainWindow();
                window.Show();
                Close();
            }
        }

        private void Bgw_Finished(object sender, RunWorkerCompletedEventArgs e)
        {
            if (_canFinish)
            {
                ImageMovementUserControl.ImageMovement.Remove();
                ImageMovementUserControl.SoundNeeded = false;
                var newCurrent = Int32.Parse(Application.Current.Properties["CurrentMovement"].ToString()) + 1;
                Application.Current.Properties["CurrentMovement"] = newCurrent;
                ImageMovementUserControl.AnimatingControl_Loaded();
                _hasBegun = false;
            }
            _bgwStarted = false;

        }

        private void Bgw_DoWork(object sender, DoWorkEventArgs e)
        {
            if (_hasBegun)
            {
                var pause = Convert.ToInt32(Application.Current.Properties["Pause"].ToString()) * 1000;
                Thread.Sleep(pause);
            }
            else
            {
                _canFinish = false;
            }
        }

        private void ReloadGame(object sender, EventArgs e)
        {
            if (!_bgwStarted)
            {
                _bgwStarted = true;
                ImageMovementUserControl.SoundToPlay.Stop();
                _canFinish = true;
                bgw.RunWorkerAsync();
            }
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (_hasBegun) return;
            ImageMovementUserControl.InteractionTimer.Stop();
            ImageMovementUserControl.ImageMovement.Completed += ReloadGame;
            ImageMovementUserControl.StartMovement();
            ImageMovementUserControl.Controller.Play();
            ImageMovementUserControl.ImageMovement.Begin();
            _hasBegun = true;
        }
    }
}
