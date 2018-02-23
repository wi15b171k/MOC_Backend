using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;

namespace ATWPJWebService.Helpers
{
    public class PictureHelper
    {
        public Bitmap ResizeImage(Image image, int width, int height)
        {
            //calculate new size
            // Figure out the ratio
            double ratioX = (double)width / (double)image.Width;
            double ratioY = (double)height / (double)image.Height;
            // use whichever multiplier is smaller
            double ratio = ratioX < ratioY ? ratioX : ratioY;

            // now we can get the new height and width
            int newHeight = Convert.ToInt32(image.Height * ratio);
            int newWidth = Convert.ToInt32(image.Width * ratio);

            // Now calculate the X,Y position of the upper-left corner 
            // (one of these will always be zero)
            int posX = Convert.ToInt32((width - (image.Width * ratio)) / 2);
            int posY = Convert.ToInt32((height - (image.Height * ratio)) / 2);

            var destImage = new Bitmap(width, height);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    graphics.Clear(Color.Black); // black padding
                    graphics.DrawImage(image, posX, posY, newWidth, newHeight);
                }
            }

            return destImage;
        }

    }
}