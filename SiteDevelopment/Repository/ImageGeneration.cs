using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SiteDevelopment.Models;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;

namespace SiteDevelopment.Repository
{
    public static class ImageGeneration
    {        
        public static string ImageProcessing(InputData data)
        {                 
            Bitmap imageTemplate = new Bitmap(1280, 960);

            Font defFont = new Font(FontFamily.GenericSansSerif, 32, FontStyle.Bold);
            Font scoreFont = new Font(FontFamily.GenericSansSerif, 96, FontStyle.Bold);
            Font addFont = new Font(FontFamily.GenericSansSerif, 60, FontStyle.Bold);
            int centerPoint = imageTemplate.Width / 2;
            
            StringFormat format = new StringFormat
            {
                LineAlignment = StringAlignment.Center,
                Alignment = StringAlignment.Center
            };
  
            Image background = Image.FromFile(data.BackgroundImage);
            Graphics g = Graphics.FromImage(imageTemplate);
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.DrawImage(background, 0, 0);

            Image home = Image.FromFile(data.HomeTeamLogo);
            g.DrawImage(home, 790, 200, 400, 400);
            Image away = Image.FromFile(data.AwayTeamLogo);
            g.DrawImage(away, 90, 200, 400, 400);

            
            g.DrawString(data.DateOfAMatch.ToString("dd. MMM."), defFont, Brushes.Black, new Point(centerPoint, 50), format);
            g.DrawString(data.DateOfAMatch.ToString("HH:mm"), defFont, Brushes.Black, new Point(centerPoint, 100), format);
            g.DrawString(data.Location, defFont, Brushes.Black, new Point(centerPoint, 900), format);
            g.DrawString(data.Arena, defFont, Brushes.Black, new Point(centerPoint, 825), format);

            switch (data.TypeOfBoard)
            {
                case TypeOfBoard.Preview: g.DrawString("vs", scoreFont, Brushes.Black, new Point(centerPoint, 400), format);
                    break;
                case TypeOfBoard.Recap: g.DrawString($"{data.AwayTeamScore}:{data.HomeTeamScore}" , scoreFont, Brushes.Black, new Point(centerPoint, 400), format);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            if (data.TypeOfBoard != TypeOfBoard.Preview)
            {
                switch (data.TypeOfWin)
                {
                    case TypeOfWin.OverTime:
                        g.DrawString("(OT)", addFont, Brushes.Black, new Point(centerPoint, 550), format);
                        break;
                    case TypeOfWin.Shootout:
                        g.DrawString("(SO)", addFont, Brushes.Black, new Point(centerPoint, 550), format);
                        break;
                }
            }
            

            string link;

            const string preview = @"Images\Posters\Preview\";
            const string recap = @"Images\Posters\Recap\";
            string nameOfFile =
                $"{data.AwayTeamShortName}_{data.HomeTeamShortName}_{data.DateOfAMatch.ToString("dd_MM")}.png";
            string directory = string.Empty;

            //if (data.TypeOfBoard == TypeOfBoard.Preview)
            //{
            //    if (!Directory.Exists(preview))
            //    {
            //        Directory.CreateDirectory(preview);
            //        imageTemplate.Save($@"{preview}\{nameOfFile}");
            //        link = $@"{preview}\{nameOfFile}";
            //    }
            //    else
            //    {
            //        imageTemplate.Save($@"{preview}\{nameOfFile}");
            //        link = $@"{preview}\{nameOfFile}";
            //    }
            //}
            //else
            //{
            //    if (!Directory.Exists(recap))
            //    {
            //        Directory.CreateDirectory(recap);
            //        imageTemplate.Save($@"{recap}\{nameOfFile}");
            //        link = $@"{recap}\{nameOfFile}";
            //    }
            //    else
            //    {
            //        imageTemplate.Save($@"{recap}\{nameOfFile}");
            //        link = $@"{recap}\{nameOfFile}";
            //    }
            //}

            switch (data.TypeOfBoard)
            {
                case TypeOfBoard.Preview:
                    directory = preview;
                    break;
                case TypeOfBoard.Recap:
                    directory = recap;
                    break;
            }

            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            link = $@"{directory}\{nameOfFile}";
            imageTemplate.Save(link);



            return link;
        }
    }
}