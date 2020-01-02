using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Eve.Helpers
{
    public static class LabelHelper
    {
        /// <summary>
        /// Sets labels look
        /// </summary>
        /// <param name="label">Label to set</param>
        /// <param name="text">Text to show</param>
        public static void SetLabelContent(Label label, string text)
        {
            label.Content = text;
            label.FontWeight = FontWeights.Bold;
            label.FontSize = Eve.Shared.Config.Properties.Default.LabelFontSize;
            label.FontFamily = Eve.Shared.Config.Properties.Default.FontFamily;
            label.Foreground = new SolidColorBrush(Eve.Shared.Config.Properties.Default.FontColor);
        }
    }
}
