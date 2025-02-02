using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace QuizApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            int correctAnswers = 0;
            int totalQuestions = 20;

            // Перевірка відповідей
            if (Q1_Answer.IsChecked == true) correctAnswers++;
            if (Q2_Answer.IsChecked == true) correctAnswers++;
            if (Q3_Answer.IsChecked == true) correctAnswers++;
            if (Q4_Answer.IsChecked == true) correctAnswers++;
            if (Q5_Answer.IsChecked == true) correctAnswers++;
            if (Q6_Answer.IsChecked == true) correctAnswers++;
            if (Q7_Answer.IsChecked == true) correctAnswers++;
            if (Q8_Answer.IsChecked == true) correctAnswers++;
            if (Q9_Answer.IsChecked == true) correctAnswers++;
            if (Q10_Answer.IsChecked == true) correctAnswers++;
            if (Q11_Answer.IsChecked == true) correctAnswers++;
            if (Q12_Answer.IsChecked == true) correctAnswers++;
            if (Q13_Answer.IsChecked == true) correctAnswers++;
            if (Q14_Answer.IsChecked == true) correctAnswers++;
            if (Q15_Answer.IsChecked == true) correctAnswers++;
            if (Q16_Answer.IsChecked == true) correctAnswers++;
            if (Q17_Answer.IsChecked == true) correctAnswers++;
            if (Q18_Answer.IsChecked == true) correctAnswers++;
            if (Q19_Answer.IsChecked == true) correctAnswers++;
            if (Q20_Answer.IsChecked == true) correctAnswers++;

            // Формуємо результат
            string resultMessage = $"Правильних відповідей: {correctAnswers} з {totalQuestions}.";

            // Вивід результату у вікні повідомлення
            MessageBox.Show(resultMessage, "Результат тесту", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
