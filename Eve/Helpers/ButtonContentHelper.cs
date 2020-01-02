using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Eve.Helpers
{
    public static class ButtonContentHelper
    {
        /// <summary>
        /// Sets button's look
        /// </summary>
        /// <param name="button">Button to change</param>
        /// <param name="text">Content to show on button</param>
        public static void SetContent(Button button, string text)
        {
            button.Content = new TextBlock()
            {
                Text = text,
                TextWrapping = System.Windows.TextWrapping.Wrap,
                FontWeight = FontWeights.Bold,
                FontFamily = Eve.Shared.Config.Properties.Default.FontFamily,
                FontSize = Eve.Shared.Config.Properties.Default.ButtonFontSize,
                TextAlignment = TextAlignment.Center,
                Foreground = new SolidColorBrush(Eve.Shared.Config.Properties.Default.FontColor)
            };
        }

        /// <summary>
        /// Sets complext button with image and text
        /// </summary>
        /// <param name="buttonsStackPanel">Button's stack panel to set</param>
        /// <param name="textBlock">TextBlock were text will be shown on button</param>
        /// <param name="text">Content to show on button</param>
        public static void SetStackPaneledButton(StackPanel buttonsStackPanel, TextBlock textBlock, string text)
        {
            buttonsStackPanel.Children.Remove(textBlock);
            buttonsStackPanel.Children.Add(new TextBlock()

            {
                Text = text,
                TextWrapping = TextWrapping.Wrap,
                FontWeight = FontWeights.Bold,
                FontFamily = Eve.Shared.Config.Properties.Default.FontFamily,
                FontSize = Eve.Shared.Config.Properties.Default.ButtonFontSize,
                TextAlignment = TextAlignment.Center,
                Foreground = new SolidColorBrush(Eve.Shared.Config.Properties.Default.FontColor)

            });
        }

    }
}
