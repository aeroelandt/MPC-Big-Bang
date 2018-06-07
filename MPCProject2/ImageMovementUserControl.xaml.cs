using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Timers;
using System.Windows.Threading;
using WpfAnimatedGif;

namespace MPCProject2
{
    /// <summary>
    /// Interaction logic for ImageMovementUserControl.xaml
    /// </summary>
    public partial class ImageMovementUserControl : UserControl
    {
        public Storyboard ImageMovement;
        public Window GameWindow;
        public bool SoundNeeded = false;
        public readonly System.Timers.Timer InteractionTimer = new System.Timers.Timer();
        private readonly List<MovementObject> _allMovementObjects =
            (List<MovementObject>) Application.Current.Properties["AllMovements"];
        private readonly List<ImageObject> _allImages = 
            (List<ImageObject>) Application.Current.Properties["AllImages"];
        private readonly int _sizeValue = Int32.Parse(Application.Current.Properties["Size"].ToString());
        private ImageObject _currentImage;
        private readonly SoundType _sound = (SoundType)Application.Current.Properties["Sound"];

        public ImageAnimationController Controller = null;

        private readonly List<int> _positionList;

        private readonly List<int> _positionImageList;

        public ImageMovementUserControl()
        {
            InitializeComponent();
            _positionList = CreateRandomList();
            _positionImageList = CreateRandomImageList();
            InteractionTimer.Elapsed += interactionTimer_Elapsed;
            Dispatcher.BeginInvoke(DispatcherPriority.Loaded, new Action(AnimatingControl_Loaded));

        }

        public void AnimatingControl_Loaded()
        {
            var imageToLoad = Int32.Parse(Application.Current.Properties["CurrentMovement"].ToString());
            if (imageToLoad >= _positionImageList.Count)
            {
                MainWindow window = new MainWindow();
                window.Show();
                Window.GetWindow(this).Close();
                return;
            }
            _currentImage = _allImages[_positionImageList[imageToLoad]];

            switch (_sizeValue)
            {
                case 0:
                    //big
                    _currentImage.Source = _currentImage.SourceBig;
                    break;
                case 1:
                    //mid
                    _currentImage.Source = _currentImage.SourceMid;
                    break;
                case 2:
                    //small
                    _currentImage.Source = _currentImage.SourceSmall;
                    break;
            }
            var bitmap = new BitmapImage(new Uri(_currentImage.Source, UriKind.Relative));
            ImageToMove.Source = bitmap;
            ImageBehavior.SetAnimatedSource(ImageToMove, bitmap);
            Controller = ImageBehavior.GetAnimationController(ImageToMove);
            ImageMovement = new Storyboard();
            GameWindow = Window.GetWindow(this);
            var timeToMove = Convert.ToInt32(Application.Current.Properties["Speed"].ToString());
            var movementToComplete = Int32.Parse(Application.Current.Properties["CurrentMovement"].ToString());
            var currentMovement = _allMovementObjects[_positionList[movementToComplete]];

            if (currentMovement.Active)
            {
                DoubleAnimation horizontalMovement;
                DoubleAnimation verticalMovement;
                double xStart;
                double yStart;
                double xEnd;
                double yEnd;
                ImageToMove.Width = bitmap.Width;
                ImageToMove.Height = bitmap.Height;
                ImageToMove.Visibility = Visibility.Visible;
                switch (currentMovement.Type)
                {
                    case MovementType.HorizontalLeftToRight:
                        FaceDirection(true);
                        xStart = 0;
                        xEnd = GameWindow.ActualWidth + ImageToMove.Width;
                        yStart = (GameWindow.ActualHeight - ImageToMove.Height) / 2;
                        Canvas.SetTop(ImageToMove, yStart);
                        Canvas.SetLeft(ImageToMove, xStart);
                        horizontalMovement = new DoubleAnimation(xStart, xEnd,
                            new Duration(new TimeSpan(0, 0, 0, timeToMove)));
                        Storyboard.SetTarget(horizontalMovement, ImageToMove);
                        Storyboard.SetTargetProperty(horizontalMovement, new PropertyPath("(Canvas.Left)"));
                        ImageMovement.Children.Add(horizontalMovement);
                        break;
                    case MovementType.HorizontalRightToLeft:
                        FaceDirection(false);

                        yStart = (GameWindow.ActualHeight - ImageToMove.Height) / 2;
                        xStart = GameWindow.ActualWidth - ImageToMove.Width;
                        xEnd = 0 - ImageToMove.Width - 10;
                        Canvas.SetTop(ImageToMove, yStart);
                        Canvas.SetLeft(ImageToMove, xStart);
                        horizontalMovement = new DoubleAnimation(xStart, xEnd,
                            new Duration(new TimeSpan(0, 0, 0, timeToMove)));
                        Storyboard.SetTarget(horizontalMovement, ImageToMove);
                        Storyboard.SetTargetProperty(horizontalMovement, new PropertyPath("(Canvas.Left)"));
                        ImageMovement.Children.Add(horizontalMovement);
                        break;
                    case MovementType.DiagonalUpLeftToDownRight:
                        FaceDirection(true);

                        xStart = 0;
                        yStart = 0;
                        xEnd = GameWindow.ActualWidth;
                        yEnd = GameWindow.ActualHeight;
                        Canvas.SetTop(ImageToMove, yStart);
                        Canvas.SetLeft(ImageToMove, xStart);
                        horizontalMovement = new DoubleAnimation(xStart, xEnd,
                            new Duration(new TimeSpan(0, 0, 0, timeToMove)));
                        verticalMovement = new DoubleAnimation(yStart, yEnd,
                            new Duration(new TimeSpan(0, 0, 0, timeToMove)));
                        Storyboard.SetTarget(horizontalMovement, ImageToMove);
                        Storyboard.SetTarget(verticalMovement, ImageToMove);
                        Storyboard.SetTargetProperty(horizontalMovement, new PropertyPath("(Canvas.Left)"));
                        Storyboard.SetTargetProperty(verticalMovement, new PropertyPath("(Canvas.Top)"));
                        ImageMovement.Children.Add(horizontalMovement);
                        ImageMovement.Children.Add(verticalMovement);
                        break;
                    case MovementType.DiagonalDownRightToUpLeft:
                        FaceDirection(false);

                        xStart = GameWindow.ActualWidth - ImageToMove.Width;
                        yStart = GameWindow.ActualHeight - ImageToMove.Height;
                        xEnd = 0 - ImageToMove.Width;
                        yEnd = 0 - ImageToMove.Height;
                        Canvas.SetTop(ImageToMove, yStart);
                        Canvas.SetLeft(ImageToMove, xStart);
                        horizontalMovement = new DoubleAnimation(xStart, xEnd,
                            new Duration(new TimeSpan(0, 0, 0, timeToMove)));
                        verticalMovement = new DoubleAnimation(yStart, yEnd,
                            new Duration(new TimeSpan(0, 0, 0, timeToMove)));
                        Storyboard.SetTarget(horizontalMovement, ImageToMove);
                        Storyboard.SetTarget(verticalMovement, ImageToMove);
                        Storyboard.SetTargetProperty(horizontalMovement, new PropertyPath("(Canvas.Left)"));
                        Storyboard.SetTargetProperty(verticalMovement, new PropertyPath("(Canvas.Top)"));
                        ImageMovement.Children.Add(horizontalMovement);
                        ImageMovement.Children.Add(verticalMovement);
                        break;
                    case MovementType.VerticalUpToDown:
                        xStart = (GameWindow.ActualWidth - ImageToMove.Width) / 2;
                        yStart = 0;
                        yEnd = GameWindow.ActualHeight;
                        Canvas.SetTop(ImageToMove, yStart);
                        Canvas.SetLeft(ImageToMove, xStart);
                        verticalMovement = new DoubleAnimation(yStart, yEnd,
                            new Duration(new TimeSpan(0, 0, 0, timeToMove)));
                        Storyboard.SetTarget(verticalMovement, ImageToMove);
                        Storyboard.SetTargetProperty(verticalMovement, new PropertyPath("(Canvas.Top)"));
                        ImageMovement.Children.Add(verticalMovement);
                        break;
                    case MovementType.VerticalDownToUp:
                        xStart = (GameWindow.ActualWidth - ImageToMove.Width) / 2;
                        yStart = GameWindow.ActualHeight - ImageToMove.Height;
                        yEnd = 0 - ImageToMove.Height;
                        Canvas.SetTop(ImageToMove, yStart);
                        Canvas.SetLeft(ImageToMove, xStart);
                        verticalMovement = new DoubleAnimation(yStart, yEnd,
                            new Duration(new TimeSpan(0, 0, 0, timeToMove)));
                        Storyboard.SetTarget(verticalMovement, ImageToMove);
                        Storyboard.SetTargetProperty(verticalMovement, new PropertyPath("(Canvas.Top)"));
                        ImageMovement.Children.Add(verticalMovement);
                        break;
                    case MovementType.DiagonalDownLeftToUpRight:
                        FaceDirection(true);
                        xStart = 0;
                        yStart = GameWindow.ActualHeight - ImageToMove.Height;
                        xEnd = GameWindow.ActualWidth;
                        yEnd = 0 - ImageToMove.Height;
                        Canvas.SetTop(ImageToMove, yStart);
                        Canvas.SetLeft(ImageToMove, xStart);
                        horizontalMovement = new DoubleAnimation(xStart, xEnd,
                            new Duration(new TimeSpan(0, 0, 0, timeToMove)));
                        verticalMovement = new DoubleAnimation(yStart, yEnd,
                            new Duration(new TimeSpan(0, 0, 0, timeToMove)));
                        Storyboard.SetTarget(horizontalMovement, ImageToMove);
                        Storyboard.SetTarget(verticalMovement, ImageToMove);
                        Storyboard.SetTargetProperty(horizontalMovement, new PropertyPath("(Canvas.Left)"));
                        Storyboard.SetTargetProperty(verticalMovement, new PropertyPath("(Canvas.Top)"));
                        ImageMovement.Children.Add(horizontalMovement);
                        ImageMovement.Children.Add(verticalMovement);
                        break;
                    case MovementType.DiagonalUpRightToDownLeft:
                        FaceDirection(false);
                        xStart = GameWindow.ActualWidth - ImageToMove.Width;
                        yStart = 0;
                        xEnd = 0 - ImageToMove.Width;
                        yEnd = GameWindow.ActualHeight;
                        Canvas.SetTop(ImageToMove, yStart);
                        Canvas.SetLeft(ImageToMove, xStart);
                        horizontalMovement = new DoubleAnimation(xStart, xEnd,
                            new Duration(new TimeSpan(0, 0, 0, timeToMove)));
                        verticalMovement = new DoubleAnimation(yStart, yEnd,
                            new Duration(new TimeSpan(0, 0, 0, timeToMove)));
                        Storyboard.SetTarget(horizontalMovement, ImageToMove);
                        Storyboard.SetTarget(verticalMovement, ImageToMove);
                        Storyboard.SetTargetProperty(horizontalMovement, new PropertyPath("(Canvas.Left)"));
                        Storyboard.SetTargetProperty(verticalMovement, new PropertyPath("(Canvas.Top)"));
                        ImageMovement.Children.Add(horizontalMovement);
                        ImageMovement.Children.Add(verticalMovement);
                        break;
                    case MovementType.Circular:
                        FaceDirection(true);
                        xStart = 0;
                        yStart = (GameWindow.ActualHeight - ImageToMove.Height) / 2;
                        Canvas.SetTop(ImageToMove, yStart);
                        Canvas.SetLeft(ImageToMove, xStart);

                        System.Globalization.CultureInfo customCulture = (System.Globalization.CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
                        customCulture.NumberFormat.NumberDecimalSeparator = ".";
                        System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;
                        var xMid = (GameWindow.ActualWidth - ImageToMove.Width) / 2;
                        var yMid = (GameWindow.ActualHeight - ImageToMove.Height) / 2;

                        var path = $"M 0,{yStart} A {xMid},{yMid} 0 1 1 0,{yStart + 0.0001}";
                        var pathUpperHalf = new PathGeometry();
                        pathUpperHalf.Figures = PathFigureCollection.Parse(path);

                        var circularMovement1 = new DoubleAnimationUsingPath();
                        circularMovement1.PathGeometry = pathUpperHalf;
                        circularMovement1.Duration = new Duration(new TimeSpan(0, 0, 0, timeToMove));
                        circularMovement1.Source = PathAnimationSource.X;

                        var circularMovement2 = new DoubleAnimationUsingPath();
                        circularMovement2.PathGeometry = pathUpperHalf;
                        circularMovement2.Duration = new Duration(new TimeSpan(0, 0, 0, timeToMove));
                        circularMovement2.Source = PathAnimationSource.Y;

                        Storyboard.SetTarget(circularMovement1, ImageToMove);
                        Storyboard.SetTargetProperty(circularMovement1, new PropertyPath("(Canvas.Left)"));

                        Storyboard.SetTarget(circularMovement2, ImageToMove);
                        Storyboard.SetTargetProperty(circularMovement2, new PropertyPath("(Canvas.Top)"));

                        ImageMovement.Children.Add(circularMovement1);
                        ImageMovement.Children.Add(circularMovement2);
                        break;
                    default:
                        break;
                }
                Controller.Pause();
                ImageToMove.Visibility = Visibility.Visible;
                var t = int.Parse(Application.Current.Properties["InteractionTime"].ToString());
                //var pause = Convert.ToInt32(Application.Current.Properties["Pause"].ToString()) * 1000;
                InteractionTimer.Interval = t * 1000/* + pause*/;
                InteractionTimer.Start();
                SoundNeeded = true;

            }
            else
            {
                Application.Current.Properties["CurrentMovement"] =
                    Int32.Parse(Application.Current.Properties["CurrentMovement"].ToString()) + 1;
                AnimatingControl_Loaded();
            }
        }

        private void FaceDirection(bool leftToRight)
        {
            if (!_currentImage.NormalDirection)
                leftToRight = !leftToRight;
            if (leftToRight)
            {
                ImageToMove.RenderTransformOrigin = new Point(0, 0);
                ScaleTransform flipTranss = new ScaleTransform {ScaleX = 1};
                ImageToMove.RenderTransform = flipTranss;

            }
            else
            {
                ImageToMove.RenderTransformOrigin = new Point(0.5, 0.5);
                ScaleTransform flipTranss = new ScaleTransform {ScaleX = -1};
                ImageToMove.RenderTransform = flipTranss;

            }
        }

        private void interactionTimer_Elapsed(object source, ElapsedEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                Controller.Play();
                if (_sound == SoundType.AllSounds || _sound == SoundType.OnlyAttentionSounds)
                {
                    SoundToPlay.Source = _currentImage.AttentionSound;
                    SoundToPlay.Play();
                }
            });
            InteractionTimer.Stop();
        }

        private void Element_MediaEnded(object sender, EventArgs e)
        {
            SoundToPlay.Stop();
            if (SoundNeeded)
            {
                SoundToPlay.Play();
            }
        }

        public void StartMovement()
        {
            SoundToPlay.Stop();
            if (_sound == SoundType.AllSounds || _sound == SoundType.OnlyMovementSounds)
            {
                SoundToPlay.Source = _currentImage.MovementSound;
                SoundToPlay.Play();
            }
        }

        private List<int> CreateRandomList()
        {
            var result = new List<int>();
            var total = _allMovementObjects.Count;
            var totalCount = Int32.Parse(Application.Current.Properties["TotalCount"].ToString());
            var rand = new Random();
            var start = 0;
            var alreadyPicked = new Dictionary<int, bool>();

            while (result.Count < totalCount)
            {
                if (start < total)
                {
                    if (bool.Parse(Application.Current.Properties["RandomOrder"].ToString()))
                    {
                        var newInt = rand.Next(total);
                        if (!alreadyPicked.ContainsKey(newInt))
                            alreadyPicked.Add(newInt, false);
                        var currentObject = _allMovementObjects[newInt];
                        if (currentObject.Active)
                        {
                            result.Add(newInt);
                        }
                        else
                        {
                            if (!alreadyPicked[newInt])
                            {
                                start++;
                                alreadyPicked[newInt] = true;
                            }
                        }

                    }
                    else
                    {
                        var currentObject = _allMovementObjects[start];
                        if (currentObject.Active)
                        {
                            result.Add(start);
                        }
                        start++;
                    }
                }
                else
                {
                    start = 0;
                    alreadyPicked = new Dictionary<int, bool>();
                }
            }
            result.RemoveRange(totalCount, result.Count - totalCount);
            return result;
        }

        private List<int> CreateRandomImageList()
        {
            var result = new List<int>();
            var total = _allImages.Count;
            var totalCount = Int32.Parse(Application.Current.Properties["TotalCount"].ToString());
            var rand = new Random();
            var start = 0;
            var alreadyPicked = new Dictionary<int, bool>();

            while (result.Count < totalCount)
            {
                if (start < total)
                {
                    if (bool.Parse(Application.Current.Properties["RandomOrderImage"].ToString()))
                    {
                        var newInt = rand.Next(total);
                        if (!alreadyPicked.ContainsKey(newInt))
                            alreadyPicked.Add(newInt, false);
                        var currentObject = _allImages[newInt];
                        if (currentObject.Active)
                        {
                            result.Add(newInt);
                        }
                        else
                        {
                            if (!alreadyPicked[newInt])
                            {
                                start++;
                                alreadyPicked[newInt] = true;
                            }
                        }

                    }
                    else
                    {
                        var currentObject = _allImages[start];
                        if (currentObject.Active)
                        {
                            result.Add(start);
                        }
                        start++;
                    }
                }
                else
                {
                    start = 0;
                    alreadyPicked = new Dictionary<int, bool>();
                }
            }
            result.RemoveRange(totalCount, result.Count - totalCount);
            return result;
        }

        protected void NotifyPropertyChanged(String propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
