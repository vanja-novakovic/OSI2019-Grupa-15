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

namespace Eve.Quiz
{
    /// <summary>
    /// Interaction logic for QuizResultWindow.xaml
    /// </summary>
    public partial class QuizResultWindow : Window
    {
        public QuizResultWindow(int correctQuestions, int totalNumberOfQuestions)
        {
            int wrongQuestion = totalNumberOfQuestions - correctQuestions;
            InitializeComponent();
            ResultLabel.Content = "Your result: " + correctQuestions;
        }


    }
}
