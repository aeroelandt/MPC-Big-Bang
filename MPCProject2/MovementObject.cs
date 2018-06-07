using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using MPCProject2.Annotations;

namespace MPCProject2
{
    public class MovementObject
    {
        public string Source { get; set; }
        public bool Active { get; set; }
        public MovementType Type { get; set; }
    }

    public class ImageObject : INotifyPropertyChanged
    {
        public string Source { get; set; }
        public string SourceMid { get; set; }
        public string SourceBig { get; set; }
        public string SourceSmall { get; set; }
        public string SettingSource { get; set; }
        public bool NormalDirection { get; set; }
        public Uri MovementSound { get; set; }
        public Uri AttentionSound { get; set; }
        public GifSize Size { get; set; }
        public bool Active { get; set; }
        public List<AfbeeldingType> Type { get; set; }

        private bool LastShowMe;
        public bool ShowMe
        {
            get => LastShowMe;
            set
            {
                LastShowMe = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ShowMe"));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged(String propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public enum AfbeeldingType
    {
        Dier = 0,
        Kertmis = 1,
        Voertuigen = 2,
        Halloween = 3
    }

    public enum MovementType
    {
        HorizontalLeftToRight = 0,
        HorizontalRightToLeft = 1,
        DiagonalUpLeftToDownRight = 2,
        DiagonalUpRightToDownLeft = 3,
        DiagonalDownLeftToUpRight = 4,
        DiagonalDownRightToUpLeft = 5,
        VerticalUpToDown = 6,
        VerticalDownToUp = 7,
        Circular = 8
    }

    public enum SoundType
    {
        AllSounds = 0,
        NoSounds = 1,
        OnlyMovementSounds = 2,
        OnlyAttentionSounds = 3
    }

    public enum GifSize
    {
        Big = 0,
        Mid = 1,
        Small = 2
    }
}
