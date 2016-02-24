using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace SiteDevelopment.Repository
{
    public static class GraphicsExtention
    {
        //public static void DrawHeader(this Graphics g, Font normalFont, int lineHeight, string value, string zakl,
        //    string norm)
        //{
        //    var sf = new StringFormat {Alignment = StringAlignment.Center};

        //    g.DrawString(value, normalFont, Brushes.Black, new PointF(610F, lineHeight), sf);
        //    g.DrawString(zakl, normalFont, Brushes.Black, new PointF(700F, lineHeight), sf);
        //    g.DrawString(norm, normalFont, Brushes.Black, new PointF(510F, lineHeight), sf);
        //}

        public static void DrawScaledArImage(this Graphics context, Image sourceImage, Rectangle sourceRect)
        {
            int width = sourceImage.Width;
            int height = sourceImage.Height;
            double ratio;
            int destWidth;
            int destHeight;
            context.InterpolationMode = InterpolationMode.HighQualityBicubic;

            if (width > height)
            {
                ratio = height/(double) width;
                destWidth = sourceRect.Width;
                destHeight = Convert.ToInt32(sourceRect.Height*ratio);
                context.DrawImage(sourceImage,
                    new Rectangle(
                        sourceRect.X,
                        sourceRect.Y + ((sourceRect.Height - destHeight)/2),
                        destWidth, destHeight),
                    new Rectangle(0, 0, sourceImage.Width, sourceImage.Height),
                    GraphicsUnit.Pixel);
            }
            else
            {
                ratio = width/(double) height;
                destWidth = Convert.ToInt32(sourceRect.Width*ratio);
                destHeight = sourceRect.Height;
                context.DrawImage(sourceImage,
                    new Rectangle(
                        sourceRect.X + ((sourceRect.Width - destWidth)/2),
                        sourceRect.Y,
                        destWidth, destHeight),
                    new Rectangle(0, 0, sourceImage.Width, sourceImage.Height),
                    GraphicsUnit.Pixel);
            }
        }
    }
}