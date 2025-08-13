using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Collections.Generic;

namespace QuizApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void FinishTest(object sender, RoutedEventArgs e)
        {
            int correctAnswers = 0;

            // Перевірка правильних відповідей для кожного питання
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

            // Виведення результату
            ResultText.Text = $"Правильних відповідей: {correctAnswers}/20";
            ResultText.Visibility = Visibility.Visible;

            // Підсвічування правильних і неправильних відповідей
            HighlightAnswers();
        }

        private void HighlightAnswers()
        {
            if (Q1_Answer.IsChecked == true) Q1_Answer.Background = Brushes.Green;
            else HighlightIncorrectAnswer(Q1_Answer);

            if (Q2_Answer.IsChecked == true) Q2_Answer.Background = Brushes.Green;
            else HighlightIncorrectAnswer(Q2_Answer);

            if (Q3_Answer.IsChecked == true) Q3_Answer.Background = Brushes.Green;
            else HighlightIncorrectAnswer(Q3_Answer);

            if (Q4_Answer.IsChecked == true) Q4_Answer.Background = Brushes.Green;
            else HighlightIncorrectAnswer(Q4_Answer);

            if (Q5_Answer.IsChecked == true) Q5_Answer.Background = Brushes.Green;
            else HighlightIncorrectAnswer(Q5_Answer);

            if (Q6_Answer.IsChecked == true) Q6_Answer.Background = Brushes.Green;
            else HighlightIncorrectAnswer(Q6_Answer);

            if (Q7_Answer.IsChecked == true) Q7_Answer.Background = Brushes.Green;
            else HighlightIncorrectAnswer(Q7_Answer);

            if (Q8_Answer.IsChecked == true) Q8_Answer.Background = Brushes.Green;
            else HighlightIncorrectAnswer(Q8_Answer);

            if (Q9_Answer.IsChecked == true) Q9_Answer.Background = Brushes.Green;
            else HighlightIncorrectAnswer(Q9_Answer);

            if (Q10_Answer.IsChecked == true) Q10_Answer.Background = Brushes.Green;
            else HighlightIncorrectAnswer(Q10_Answer);

            if (Q11_Answer.IsChecked == true) Q11_Answer.Background = Brushes.Green;
            else HighlightIncorrectAnswer(Q11_Answer);

            if (Q12_Answer.IsChecked == true) Q12_Answer.Background = Brushes.Green;
            else HighlightIncorrectAnswer(Q12_Answer);

            if (Q13_Answer.IsChecked == true) Q13_Answer.Background = Brushes.Green;
            else HighlightIncorrectAnswer(Q13_Answer);

            if (Q14_Answer.IsChecked == true) Q14_Answer.Background = Brushes.Green;
            else HighlightIncorrectAnswer(Q14_Answer);

            if (Q15_Answer.IsChecked == true) Q15_Answer.Background = Brushes.Green;
            else HighlightIncorrectAnswer(Q15_Answer);

            if (Q16_Answer.IsChecked == true) Q16_Answer.Background = Brushes.Green;
            else HighlightIncorrectAnswer(Q16_Answer);

            if (Q17_Answer.IsChecked == true) Q17_Answer.Background = Brushes.Green;
            else HighlightIncorrectAnswer(Q17_Answer);

            if (Q18_Answer.IsChecked == true) Q18_Answer.Background = Brushes.Green;
            else HighlightIncorrectAnswer(Q18_Answer);

            if (Q19_Answer.IsChecked == true) Q19_Answer.Background = Brushes.Green;
            else HighlightIncorrectAnswer(Q19_Answer);

            if (Q20_Answer.IsChecked == true) Q20_Answer.Background = Brushes.Green;
            else HighlightIncorrectAnswer(Q20_Answer);
        }

        private void HighlightIncorrectAnswer(RadioButton answer)
        {
            if (answer.IsChecked == false)
            {
                answer.Background = Brushes.Red;
            }
        }

        // --- Додаємо метод для кнопки "Скинути тест" ---
        private void ResetTest(object sender, RoutedEventArgs e)
        {
            foreach (var rb in FindVisualChildren<RadioButton>(this))
            {
                rb.IsChecked = false;
                rb.Background = Brushes.Transparent; // повернення стандартного фону
            }

            ResultText.Text = "";
            ResultText.Visibility = Visibility.Collapsed;
        }

        // Метод для пошуку всіх RadioButton у вікні
        private static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child is T t)
                        yield return t;

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                        yield return childOfChild;
                }
            }
        }
    }
}