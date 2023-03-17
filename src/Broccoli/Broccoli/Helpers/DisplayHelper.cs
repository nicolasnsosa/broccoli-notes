using Windows.Graphics.Display;
using Microsoft.UI.Xaml;

namespace Broccoli.Helpers;

public class DisplayHelper
{
    public static (double Width, double Height) GetScreenSizeInPixels()
    {
        var displayInformation = DisplayInformation.GetForCurrentView();
        var scaleFactor = displayInformation.RawPixelsPerViewPixel;

        var bounds = Window.Current.Bounds;
        var widthInPixels = bounds.Width * scaleFactor;
        var heightInPixels = bounds.Height * scaleFactor;

        return (widthInPixels, heightInPixels);
    }
}