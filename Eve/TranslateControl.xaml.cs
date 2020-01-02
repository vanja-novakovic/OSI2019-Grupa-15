using Eve.Helpers;
using System.Globalization;
using System.Threading;
using System.Windows;
using System.Windows.Controls;

namespace Eve
{
    /// <summary>
    /// Interaction logic for TranslateControl.xaml
    /// </summary>
    public partial class TranslateControl : UserControl
    {
        private static readonly string SERBIAN_LANGUAGE = "sr-SR";
        private static readonly string ENGLISH_LANGUAGE = "en-EN";
        private static readonly string SWITCH_TO_SERBIAN = "Switch to Serbian";
        private static readonly string SWITCH_TO_ENGLISH = "Prevedi na engleski";
        public TranslateControl()
        {
            InitializeComponent();
            ButtonText.Text = Shared.Config.Properties.Default.TranslateText;
        }

        private void TranslateButton_Click(object sender, RoutedEventArgs e)
        {
            if (Shared.Config.Properties.Default.Language == SERBIAN_LANGUAGE)
            {
                Shared.Config.Properties.Default.Language = ENGLISH_LANGUAGE;

                Shared.Config.Properties.Default.TranslateText = SWITCH_TO_SERBIAN;

            }
            else
            {
                Shared.Config.Properties.Default.Language = SERBIAN_LANGUAGE;
                Shared.Config.Properties.Default.TranslateText = SWITCH_TO_ENGLISH;
            }
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo(Shared.Config.Properties.Default.Language);
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(Shared.Config.Properties.Default.Language);
            WindowHelper.Refresh(Window.GetWindow(this) as IRefreshable);
        }
    }
}
