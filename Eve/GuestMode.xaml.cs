using Core.Common;
using Core.Services.Interfaces;
using Database.Entities;
using Eve.Helpers;
using Eve.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Eve
{
    /// <summary>
    /// Interaction logic for GuestMode.xaml
    /// </summary>
    public partial class GuestMode : Window
    {
        private readonly IQuizService quizService = ServicesFactory.GetInstance().CreateIQuizService();

        public GuestMode()
        {
            InitializeComponent();
        }

        private void ViewEventButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private async void PlayQuizButton_Click(object sender, RoutedEventArgs e)
        {

            Quiz.Quiz quiz = await Quiz.Quiz.GetFirst();
            WindowHelper.ShowWindow(this, new Quiz.QuizWindow(quiz));
        }
    }
}
