using Core.Common;
using Core.Services.Interfaces;
using Eve.Helpers;
using System.Windows;

namespace Eve.Quiz
{
    /// <summary>
    /// Interaction logic for QuizWindow.xaml
    /// </summary>
    public partial class QuizWindow : Window, IWindowReturnable
    {
        private readonly Quiz quiz;
        public Window windowToReturn;

        public QuizWindow(Quiz quiz)
        {
            this.quiz = quiz;
            windowToReturn = new GuestMode();
            InitializeComponent();
            InitializeQuestionAndAnswers();
            ConfirmButton.IsEnabled = false;
        }

        public void ReturnToPreviousWindow()
        {

            MessageBoxResult result = MessageBox.Show("Are you sure?", "Warning", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
                WindowHelper.ShowWindow(this, windowToReturn);
        }

        public void SetReturningWindow(Window window)
        {
            windowToReturn = window;
        }

        private void InitializeQuestionAndAnswers()
        {
            // Set question
            GroupBoxQuestion.Header = quiz.CurrentQuestion.Content;

            // Set answers
            First.Content = quiz.Answers[0].Content;
            Second.Content = quiz.Answers[1].Content;
            Third.Content = quiz.Answers[2].Content;
            NumberLabel.Content = "Correct: " + quiz.CorrectAnswersNum + "\nWrong: " + quiz.WrongAnswersNum;
        }

        private async void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            int indexOfAnswer = 0;
            if (First.IsChecked.Value)
                indexOfAnswer = 0;
            else if (Second.IsChecked.Value)
                indexOfAnswer = 1;
            else if (Third.IsChecked.Value)
                indexOfAnswer = 2;
            quiz.ChooseAnswer(indexOfAnswer);

            Quiz newQuiz = await quiz.GetNext();
            if (newQuiz == null)
                WindowHelper.ShowWindow(this, new QuizResultWindow(quiz.CorrectAnswersNum, Quiz.TotalNumberOfQuestions));
            else
                WindowHelper.ShowWindow(this, new QuizWindow(newQuiz));
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            ReturnToPreviousWindow();
        }

        private void First_Checked(object sender, RoutedEventArgs e)
        {
            ConfirmButton.IsEnabled = true;
        }

        private void Second_Checked(object sender, RoutedEventArgs e)
        {
            ConfirmButton.IsEnabled = true;
        }

        private void Third_Checked(object sender, RoutedEventArgs e)
        {
            ConfirmButton.IsEnabled = true;
        }
    }
}
