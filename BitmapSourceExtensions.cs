using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace DropShadowSample
{
    public static class BitmapSourceExtensions
    {
        public static BitmapSource ReplaceTransparency(this BitmapSource bitmap, Color color)
        {
            Rect rect = new Rect(0, 0, bitmap.PixelWidth, bitmap.PixelHeight);
            DrawingVisual visual = new DrawingVisual();
            DrawingContext context = visual.RenderOpen();
            context.DrawRectangle(new SolidColorBrush(color), null, rect);
            context.DrawImage(bitmap, rect);
            context.Close();

            RenderTargetBitmap render = new RenderTargetBitmap(bitmap.PixelWidth, bitmap.PixelHeight, 96, 96, PixelFormats.Pbgra32);
            render.Render(visual);
            return render;
        }
    }
}
