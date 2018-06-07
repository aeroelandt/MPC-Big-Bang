using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Newtonsoft.Json;

namespace MPCProject2
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        void App_Startup(object sender, StartupEventArgs e)
        {

            var allMovements = new List<MovementObject>
            {
                new MovementObject()
                {
                    Source = "Resources/horLefToRig.png",
                    Active = true,
                    Type = MovementType.HorizontalLeftToRight
                },
                new MovementObject()
                {
                    Source = "Resources/horRigToLef.png",
                    Active = true,
                    Type = MovementType.HorizontalRightToLeft
                },
                new MovementObject()
                {
                    Source = "Resources/verDowToUp.png",
                    Active = true,
                    Type = MovementType.VerticalDownToUp
                },
                new MovementObject()
                {
                    Source = "Resources/verUpToDow.png",
                    Active = true,
                    Type = MovementType.VerticalUpToDown
                },
                new MovementObject()
                {
                    Source = "Resources/DiaUpLToDoR.png",
                    Active = true,
                    Type = MovementType.DiagonalUpLeftToDownRight
                },
                new MovementObject()
                {
                    Source = "Resources/DiaDoRToUpL.png",
                    Active = true,
                    Type = MovementType.DiagonalDownRightToUpLeft
                },
                new MovementObject()
                {
                    Source = "Resources/DiaDoLToUpR.png",
                    Active = true,
                    Type = MovementType.DiagonalDownLeftToUpRight
                },
                new MovementObject()
                {
                    Source = "Resources/DiaUpRToDoL.png",
                    Active = true,
                    Type = MovementType.DiagonalUpRightToDownLeft
                },
                new MovementObject()
                {
                    Source = "Resources/circular.png",
                    Active = true,
                    Type = MovementType.Circular
                }
            };

            var allImages = new List<ImageObject>
            {
                new ImageObject
                {
                    SourceMid = "Resources/Gifs/Aap/aapMiddel.gif",
                    SourceSmall = "Resources/Gifs/Aap/aapKlein.gif",
                    SourceBig = "Resources/Gifs/Aap/aapGroot.gif",
                    NormalDirection = true,
                    Active = true,
                    AttentionSound = new Uri("Resources/Sounds/monkeyAttention.wav", UriKind.Relative),
                    MovementSound = new Uri("Resources/Sounds/monkeySound.wav", UriKind.Relative),
                    Type = new List<AfbeeldingType>(){AfbeeldingType.Dier},
                    ShowMe = true
                },
                new ImageObject
                {
                    SourceMid = "Resources/Gifs/Auto/auto1Middel.gif",
                    SourceSmall = "Resources/Gifs/Auto/auto1Klein.gif",
                    SourceBig = "Resources/Gifs/Auto/auto1Groot.gif",
                    NormalDirection = false,
                    Active = true,
                    AttentionSound = new Uri("Resources/Sounds/auto1Attention.wav", UriKind.Relative),
                    MovementSound = new Uri("Resources/Sounds/carSound1.wav", UriKind.Relative),
                    Type = new List<AfbeeldingType>(){AfbeeldingType.Voertuigen},
                    ShowMe = true
                },
                new ImageObject
                {
                    SourceMid = "Resources/Gifs/Auto/auto2Middel.gif",
                    SourceSmall = "Resources/Gifs/Auto/auto2Klein.gif",
                    SourceBig = "Resources/Gifs/Auto/auto2Groot.gif",
                    NormalDirection = false,
                    Active = true,
                    AttentionSound = new Uri("Resources/Sounds/auto2Attention.wav", UriKind.Relative),
                    MovementSound = new Uri("Resources/Sounds/carSound2.wav", UriKind.Relative),
                    Type = new List<AfbeeldingType>(){AfbeeldingType.Voertuigen},
                    ShowMe = true
                },
                new ImageObject
                {
                    SourceMid = "Resources/Gifs/Auto/motorMiddel.gif",
                    SourceSmall = "Resources/Gifs/Auto/motorKlein.gif",
                    SourceBig = "Resources/Gifs/Auto/motorGroot.gif",
                    NormalDirection = true,
                    Active = true,
                    AttentionSound = new Uri("Resources/Sounds/motorAttention.wav", UriKind.Relative),
                    MovementSound = new Uri("Resources/Sounds/motorSound.wav", UriKind.Relative),
                    Type = new List<AfbeeldingType>(){AfbeeldingType.Voertuigen},
                    ShowMe = true
                },
                new ImageObject
                {
                    SourceMid = "Resources/Gifs/Fiets/fietsMid.gif",
                    SourceSmall = "Resources/Gifs/Fiets/fietsSmall.gif",
                    SourceBig = "Resources/Gifs/Fiets/fietsBig.gif",
                    NormalDirection = false,
                    Active = true,
                    AttentionSound = new Uri("Resources/Sounds/BicycleBells.wav", UriKind.Relative),
                    MovementSound = new Uri("Resources/Sounds/Bicycle.wav", UriKind.Relative),
                    Type = new List<AfbeeldingType>(){AfbeeldingType.Voertuigen},
                    ShowMe = true
                },
                new ImageObject
                {
                    SourceMid = "Resources/Gifs/Kat/katMiddel.gif",
                    SourceSmall = "Resources/Gifs/Kat/katKlein.gif",
                    SourceBig = "Resources/Gifs/Kat/katGroot.gif",
                    NormalDirection = true,
                    Active = true,
                    AttentionSound = new Uri("Resources/Sounds/katAttention.wav", UriKind.Relative),
                    MovementSound = new Uri("Resources/Sounds/katSound.wav", UriKind.Relative),
                    Type = new List<AfbeeldingType>(){AfbeeldingType.Dier},
                    ShowMe = true
                },
                new ImageObject
                {
                    SourceMid = "Resources/Gifs/Kerstmis/kerstmanMiddel.gif",
                    SourceSmall = "Resources/Gifs/Kerstmis/kerstmanKlein.gif",
                    SourceBig = "Resources/Gifs/Kerstmis/kerstmanGroot.gif",
                    NormalDirection = true,
                    Active = true,
                    AttentionSound = new Uri("Resources/Sounds/kerstmanAttention.wav", UriKind.Relative),
                    MovementSound = new Uri("Resources/Sounds/kerstmanSound.wav", UriKind.Relative),
                    Type = new List<AfbeeldingType>(){AfbeeldingType.Kertmis},
                    ShowMe = true
                },
                new ImageObject
                {
                    SourceMid = "Resources/Gifs/Kikker/kikkerMiddel.gif",
                    SourceSmall = "Resources/Gifs/Kikker/kikkerKlein.gif",
                    SourceBig = "Resources/Gifs/Kikker/kikkerGroot.gif",
                    NormalDirection = false,
                    Active = true,
                    AttentionSound = new Uri("Resources/Sounds/kikkerAttention.wav", UriKind.Relative),
                    MovementSound = new Uri("Resources/Sounds/kikkerSound.wav", UriKind.Relative),
                    Type = new List<AfbeeldingType>(){AfbeeldingType.Dier},
                    ShowMe = true
                },
                //new ImageObject
                //{
                //    SourceMid = "Resources/Gifs/Krokodil/krokodilMiddel.gif",
                //    SourceSmall = "Resources/Gifs/Krokodil/krokodilKlein.gif",
                //    SourceBig = "Resources/Gifs/Krokodil/krokodilGroot.gif",
                //    NormalDirection = false,
                //    Active = true,
                //    AttentionSound = new Uri("Resources/Sounds/BicycleBells.wav", UriKind.Relative),
                //    MovementSound = new Uri("Resources/Sounds/Bicycle.wav", UriKind.Relative),
                //    Type = new List<AfbeeldingType>(){AfbeeldingType.Dier},
                //    ShowMe = true
                //},
                new ImageObject
                {
                    SourceMid = "Resources/Gifs/Clown/clownMiddel.gif",
                    SourceSmall = "Resources/Gifs/Clown/clownKlein.gif",
                    SourceBig = "Resources/Gifs/Clown/clownGroot.gif",
                    NormalDirection = false,
                    Active = true,
                    AttentionSound = new Uri("Resources/Sounds/clownAttention.wav", UriKind.Relative),
                    MovementSound = new Uri("Resources/Sounds/clownSound.wav", UriKind.Relative),
                    Type = new List<AfbeeldingType>(){},
                    ShowMe = true
                },
                new ImageObject
                {
                    SourceMid = "Resources/Gifs/Hond/hond2Middel.gif",
                    SourceSmall = "Resources/Gifs/Hond/hond2Klein.gif",
                    SourceBig = "Resources/Gifs/Hond/hond2Groot.gif",
                    NormalDirection = true,
                    Active = true,
                    AttentionSound = new Uri("Resources/Sounds/hondAttention.wav", UriKind.Relative),
                    MovementSound = new Uri("Resources/Sounds/hondSound.wav", UriKind.Relative),
                    Type = new List<AfbeeldingType>(){AfbeeldingType.Dier},
                    ShowMe = true
                },
                new ImageObject
                {
                    SourceMid = "Resources/Gifs/Paard/paardMiddel.gif",
                    SourceSmall = "Resources/Gifs/Paard/paardKlein.gif",
                    SourceBig = "Resources/Gifs/Paard/paardGroot.gif",
                    NormalDirection = false,
                    Active = true,
                    AttentionSound = new Uri("Resources/Sounds/paardAttention.wav", UriKind.Relative),
                    MovementSound = new Uri("Resources/Sounds/paardSound.wav", UriKind.Relative),
                    Type = new List<AfbeeldingType>(){AfbeeldingType.Dier},
                    ShowMe = true
                },
                new ImageObject
                {
                    SourceMid = "Resources/Gifs/Paard/paard1Middel.gif",
                    SourceSmall = "Resources/Gifs/Paard/paard1Klein.gif",
                    SourceBig = "Resources/Gifs/Paard/paard1Groot.gif",
                    NormalDirection = true,
                    Active = true,
                    AttentionSound = new Uri("Resources/Sounds/paardAttention.wav", UriKind.Relative),
                    MovementSound = new Uri("Resources/Sounds/horse2Sound.wav", UriKind.Relative),
                    Type = new List<AfbeeldingType>(){AfbeeldingType.Dier},
                    ShowMe = true
                },
                new ImageObject
                {
                    SourceMid = "Resources/Gifs/Schaap/schaapMiddel.gif",
                    SourceSmall = "Resources/Gifs/Schaap/schaapKlein.gif",
                    SourceBig = "Resources/Gifs/Schaap/schaapGroot.gif",
                    NormalDirection = false,
                    Active = true,
                    AttentionSound = new Uri("Resources/Sounds/schaapAttention.wav", UriKind.Relative),
                    MovementSound = new Uri("Resources/Sounds/schaapSound.wav", UriKind.Relative),
                    Type = new List<AfbeeldingType>(){AfbeeldingType.Dier},
                    ShowMe = true
                },
                new ImageObject
                {
                    SourceMid = "Resources/Gifs/Vogel/vogelMiddel.gif",
                    SourceSmall = "Resources/Gifs/Vogel/vogelKlein.gif",
                    SourceBig = "Resources/Gifs/Vogel/vogelGroot.gif",
                    NormalDirection = true,
                    Active = true,
                    AttentionSound = new Uri("Resources/Sounds/vogelAttention.wav", UriKind.Relative),
                    MovementSound = new Uri("Resources/Sounds/vogelSound.wav", UriKind.Relative),
                    Type = new List<AfbeeldingType>(){AfbeeldingType.Dier},
                    ShowMe = true
                },
                new ImageObject
                {
                    SourceMid = "Resources/Gifs/Circus/olifantMiddel.gif",
                    SourceSmall = "Resources/Gifs/Circus/olifantKlein.gif",
                    SourceBig = "Resources/Gifs/Circus/olifantGroot.gif",
                    NormalDirection = false,
                    Active = true,
                    AttentionSound = new Uri("Resources/Sounds/olifantAttention.wav", UriKind.Relative),
                    MovementSound = new Uri("Resources/Sounds/olifantSound.wav", UriKind.Relative),
                    Type = new List<AfbeeldingType>(){AfbeeldingType.Dier},
                    ShowMe = true
                },
                new ImageObject
                {
                    SourceMid = "Resources/Gifs/Halloween/heksMiddel.gif",
                    SourceSmall = "Resources/Gifs/Halloween/heksKlein.gif",
                    SourceBig = "Resources/Gifs/Halloween/heksGroot.gif",
                    NormalDirection = true,
                    Active = true,
                    AttentionSound = new Uri("Resources/Sounds/heksAttention.wav", UriKind.Relative),
                    MovementSound = new Uri("Resources/Sounds/heksSound.wav", UriKind.Relative),
                    Type = new List<AfbeeldingType>(){AfbeeldingType.Halloween},
                    ShowMe = true
                },
                //new ImageObject
                //{
                //    SourceMid = "Resources/Gifs/Halloween/katMiddel.gif",
                //    SourceSmall = "Resources/Gifs/Halloween/katKlein.gif",
                //    SourceBig = "Resources/Gifs/Halloween/katGroot.gif",
                //    NormalDirection = true,
                //    Active = true,
                //    AttentionSound = new Uri("Resources/Sounds/BicycleBells.wav", UriKind.Relative),
                //    MovementSound = new Uri("Resources/Sounds/Bicycle.wav", UriKind.Relative),
                //    Type = new List<AfbeeldingType>(){AfbeeldingType.Halloween},
                //    ShowMe = true
                //},
                new ImageObject
                {
                    SourceMid = "Resources/Gifs/Halloween/skeletMiddel.gif",
                    SourceSmall = "Resources/Gifs/Halloween/skeletKlein.gif",
                    SourceBig = "Resources/Gifs/Halloween/skeletGroot.gif",
                    NormalDirection = true,
                    Active = true,
                    AttentionSound = new Uri("Resources/Sounds/skeletonAttention.wav", UriKind.Relative),
                    MovementSound = new Uri("Resources/Sounds/skeletonSound.wav", UriKind.Relative),
                    Type = new List<AfbeeldingType>(){AfbeeldingType.Halloween},
                    ShowMe = true
                },
                new ImageObject
                {
                    SourceMid = "Resources/Gifs/Halloween/spinMiddel.gif",
                    SourceSmall = "Resources/Gifs/Halloween/spinKlein.gif",
                    SourceBig = "Resources/Gifs/Halloween/spinGroot.gif",
                    NormalDirection = true,
                    Active = true,
                    AttentionSound = new Uri("Resources/Sounds/spiderAttention.wav", UriKind.Relative),
                    MovementSound = new Uri("Resources/Sounds/spiderSound.wav", UriKind.Relative),
                    Type = new List<AfbeeldingType>(){AfbeeldingType.Halloween},
                    ShowMe = true
                },
            };

            this.Properties["AllImages"] = allImages;
            this.Properties["AllMovements"] = allMovements;
            this.Properties["BackgroundColor"] = "#FFF";
            this.Properties["Speed"] = 1;
            this.Properties["TotalCount"] = 10;
            this.Properties["InteractionTime"] = 5;
            this.Properties["RandomOrder"] = "False";
            this.Properties["RandomOrderImage"] = "False";
            this.Properties["CurrentMovement"] = 0;
            this.Properties["CurrentImage"] = 0;
            this.Properties["Sound"] = 0;
            this.Properties["Pause"] = 1;
            this.Properties["Size"] = 1;
        }
    }
}
