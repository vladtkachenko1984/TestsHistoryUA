using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Threading;
using System;
using System.IO;
using System.Linq;

namespace QuizApp
{
    public partial class MainWindow : Window
    {
        private Stopwatch stopwatch;
        private DispatcherTimer timer;
        private readonly TimeSpan TimeLimit = TimeSpan.FromMinutes(1);
        private readonly int totalQuestions = 20;
        private readonly Dictionary<int, RadioButton> correctAnswers = new Dictionary<int, RadioButton>();
        private readonly Dictionary<int, TextBlock> explanations = new Dictionary<int, TextBlock>();

        public MainWindow()
        {
            InitializeComponent();

            stopwatch = new Stopwatch();
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;

            stopwatch.Start();
            timer.Start();

            foreach (var rb in FindVisualChildren<RadioButton>(this))
            {
                rb.Checked += AnswerSelected;
            }

            // Правильні відповіді
            correctAnswers[1] = Q1_Answer;
            correctAnswers[2] = Q2_Answer;
            correctAnswers[3] = Q3_Answer;
            correctAnswers[4] = Q4_Answer;
            correctAnswers[5] = Q5_Answer;
            correctAnswers[6] = Q6_Answer;
            correctAnswers[7] = Q7_Answer;
            correctAnswers[8] = Q8_Answer;
            correctAnswers[9] = Q9_Answer;
            correctAnswers[10] = Q10_Answer;
            correctAnswers[11] = Q11_Answer;
            correctAnswers[12] = Q12_Answer;
            correctAnswers[13] = Q13_Answer;
            correctAnswers[14] = Q14_Answer;
            correctAnswers[15] = Q15_Answer;
            correctAnswers[16] = Q16_Answer;
            correctAnswers[17] = Q17_Answer;
            correctAnswers[18] = Q18_Answer;
            correctAnswers[19] = Q19_Answer;
            correctAnswers[20] = Q20_Answer;

            // Пояснення
            explanations[1] = Q1_Explanation;
            explanations[2] = Q2_Explanation;
            explanations[3] = Q3_Explanation;
            explanations[4] = Q4_Explanation;
            explanations[5] = Q5_Explanation;
            explanations[6] = Q6_Explanation;
            explanations[7] = Q7_Explanation;
            explanations[8] = Q8_Explanation;
            explanations[9] = Q9_Explanation;
            explanations[10] = Q10_Explanation;
            explanations[11] = Q11_Explanation;
            explanations[12] = Q12_Explanation;
            explanations[13] = Q13_Explanation;
            explanations[14] = Q14_Explanation;
            explanations[15] = Q15_Explanation;
            explanations[16] = Q16_Explanation;
            explanations[17] = Q17_Explanation;
            explanations[18] = Q18_Explanation;
            explanations[19] = Q19_Explanation;
            explanations[20] = Q20_Explanation;

            HideAllExplanations();
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            TimerText.Text = $"Час: {stopwatch.Elapsed.Minutes:D2}:{stopwatch.Elapsed.Seconds:D2}";
            if (stopwatch.Elapsed >= TimeLimit)
                FinishTest(this, new RoutedEventArgs());
        }

        private void FinishTest(object? sender, RoutedEventArgs e)
        {
            // --- Перевірка на незаповнені питання за GroupName ---
            List<int> unansweredQuestions = new List<int>();

            foreach (var kvp in correctAnswers)
            {
                string groupName = kvp.Value.GroupName;
                bool anyChecked = FindVisualChildren<RadioButton>(this)
                                 .Any(rb => rb.GroupName == groupName && rb.IsChecked == true);

                if (!anyChecked)
                    unansweredQuestions.Add(kvp.Key);
            }

            if (unansweredQuestions.Count > 0)
            {
                string msg = $"Ви не відповіли на {unansweredQuestions.Count} з {totalQuestions} питань.\n" +
                             $"Номери пропущених: {string.Join(", ", unansweredQuestions)}\n\n" +
                             "Бажаєте завершити тест?";
                var result = MessageBox.Show(msg, "Попередження",
                    MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result == MessageBoxResult.No)
                    return; // ❌ користувач передумав
            }

            // --- Основна логіка завершення ---
            stopwatch.Stop();
            timer.Stop();

            int correctCount = correctAnswers.Count(kvp => kvp.Value.IsChecked == true);

            double percent = (double)correctCount / totalQuestions * 100;

            TestProgressBar.Value = correctCount;
            ProgressText.Text = $"{correctCount} / {totalQuestions} питань ({percent:F0}%)";

            ResultText.Text = $"Правильних відповідей: {correctCount}/{totalQuestions} ({percent:F0}%)\n" +
                              $"Час проходження: {stopwatch.Elapsed.Minutes:D2}:{stopwatch.Elapsed.Seconds:D2}";
            ResultText.Visibility = Visibility.Visible;

            HighlightAnswers();
            ShowExplanationsForAll();
            SaveResultToFile(correctCount, percent);
        }

        private void HighlightAnswers()
        {
            foreach (var kvp in correctAnswers)
            {
                var answer = kvp.Value;
                answer.Background = answer.IsChecked == true ? Brushes.Green : Brushes.Red;
            }
        }

        private void ShowExplanationsForAll()
        {
            foreach (var kvp in correctAnswers)
            {
                explanations[kvp.Key].Visibility = Visibility.Visible;
            }
        }

        private void HideAllExplanations()
        {
            foreach (var exp in explanations.Values)
                exp.Visibility = Visibility.Collapsed;
        }

        private void AnswerSelected(object? sender, RoutedEventArgs e)
        {
            // Оновлення прогресу тесту на основі GroupName
            int answered = correctAnswers.Keys.Count(q =>
            {
                string groupName = correctAnswers[q].GroupName;
                return FindVisualChildren<RadioButton>(this)
                       .Any(rb => rb.GroupName == groupName && rb.IsChecked == true);
            });

            double percent = (double)answered / totalQuestions * 100;
            TestProgressBar.Value = answered;
            ProgressText.Text = $"{answered} / {totalQuestions} питань ({percent:F0}%)";
        }

        private void ResetTest(object? sender, RoutedEventArgs e)
        {
            foreach (var rb in FindVisualChildren<RadioButton>(this))
            {
                rb.IsChecked = false;
                rb.Background = Brushes.Transparent;
            }

            ResultText.Text = "";
            ResultText.Visibility = Visibility.Collapsed;

            TestProgressBar.Value = 0;
            ProgressText.Text = $"0 / {totalQuestions} питань (0%)";

            stopwatch.Reset();
            TimerText.Text = "Час: 00:00";
            stopwatch.Start();
            timer.Start();

            HideAllExplanations();
        }

        private void SaveResultToFile(int correctCount, double percent)
        {
            string filePath = "history.txt";
            string record = $"{DateTime.Now:dd.MM.yyyy HH:mm} — {correctCount}/{totalQuestions} ({percent:F0}%), Час: {stopwatch.Elapsed.Minutes:D2}:{stopwatch.Elapsed.Seconds:D2}";
            File.AppendAllText(filePath, record + Environment.NewLine);
        }

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
