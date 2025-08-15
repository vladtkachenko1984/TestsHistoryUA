using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Threading;
using System;
using System.IO;

namespace QuizApp
{
    public partial class MainWindow : Window
    {
        private Stopwatch stopwatch;
        private DispatcherTimer timer;
        private readonly TimeSpan TimeLimit = TimeSpan.FromMinutes(1);
        private readonly int totalQuestions = 20;

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

            HideAllExplanations();
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            TimerText.Text = $"Час: {stopwatch.Elapsed.Minutes:D2}:{stopwatch.Elapsed.Seconds:D2}";
            if (stopwatch.Elapsed >= TimeLimit)
            {
                FinishTest(this, new RoutedEventArgs());
            }
        }

        private void FinishTest(object? sender, RoutedEventArgs e)
        {
            stopwatch.Stop();
            timer.Stop();

            int correctAnswers = 0;

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

            double percent = (double)correctAnswers / totalQuestions * 100;

            TestProgressBar.Value = correctAnswers;
            ProgressText.Text = $"{correctAnswers} / {totalQuestions} питань ({percent:F0}%)";

            ResultText.Text = $"Правильних відповідей: {correctAnswers}/{totalQuestions} ({percent:F0}%)\n" +
                              $"Час проходження: {stopwatch.Elapsed.Minutes:D2}:{stopwatch.Elapsed.Seconds:D2}";
            ResultText.Visibility = Visibility.Visible;

            HighlightAnswers();
            ShowExplanationsForCorrect(); // лише правильні відповіді

            string filePath = "history.txt";
            string record = $"{DateTime.Now:dd.MM.yyyy HH:mm} — {correctAnswers}/{totalQuestions} ({percent:F0}%), Час: {stopwatch.Elapsed.Minutes:D2}:{stopwatch.Elapsed.Seconds:D2}";
            File.AppendAllText(filePath, record + Environment.NewLine);
        }

        private void HighlightAnswers()
        {
            HighlightAnswer(Q1_Answer);
            HighlightAnswer(Q2_Answer);
            HighlightAnswer(Q3_Answer);
            HighlightAnswer(Q4_Answer);
            HighlightAnswer(Q5_Answer);
            HighlightAnswer(Q6_Answer);
            HighlightAnswer(Q7_Answer);
            HighlightAnswer(Q8_Answer);
            HighlightAnswer(Q9_Answer);
            HighlightAnswer(Q10_Answer);
            HighlightAnswer(Q11_Answer);
            HighlightAnswer(Q12_Answer);
            HighlightAnswer(Q13_Answer);
            HighlightAnswer(Q14_Answer);
            HighlightAnswer(Q15_Answer);
            HighlightAnswer(Q16_Answer);
            HighlightAnswer(Q17_Answer);
            HighlightAnswer(Q18_Answer);
            HighlightAnswer(Q19_Answer);
            HighlightAnswer(Q20_Answer);
        }

        private void HighlightAnswer(RadioButton answer)
        {
            if (answer.IsChecked == true)
                answer.Background = Brushes.Green;
            else
                answer.Background = Brushes.Red;
        }

        private void AnswerSelected(object? sender, RoutedEventArgs e)
        {
            int answered = 0;
            foreach (var rb in FindVisualChildren<RadioButton>(this))
            {
                var parentPanel = rb.Parent as Panel;
                if (parentPanel != null)
                {
                    bool anyChecked = false;
                    foreach (var child in parentPanel.Children)
                    {
                        if (child is RadioButton r && r.IsChecked == true)
                        {
                            anyChecked = true;
                            break;
                        }
                    }
                    if (anyChecked) answered++;
                }
            }

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

        private void ShowExplanationsForCorrect()
        {
            if (Q1_Answer.IsChecked == true) Q1_Explanation.Visibility = Visibility.Visible;
            if (Q2_Answer.IsChecked == true) Q2_Explanation.Visibility = Visibility.Visible;
            if (Q3_Answer.IsChecked == true) Q3_Explanation.Visibility = Visibility.Visible;
            if (Q4_Answer.IsChecked == true) Q4_Explanation.Visibility = Visibility.Visible;
            if (Q5_Answer.IsChecked == true) Q5_Explanation.Visibility = Visibility.Visible;
            if (Q6_Answer.IsChecked == true) Q6_Explanation.Visibility = Visibility.Visible;
            if (Q7_Answer.IsChecked == true) Q7_Explanation.Visibility = Visibility.Visible;
            if (Q8_Answer.IsChecked == true) Q8_Explanation.Visibility = Visibility.Visible;
            if (Q9_Answer.IsChecked == true) Q9_Explanation.Visibility = Visibility.Visible;
            if (Q10_Answer.IsChecked == true) Q10_Explanation.Visibility = Visibility.Visible;
            if (Q11_Answer.IsChecked == true) Q11_Explanation.Visibility = Visibility.Visible;
            if (Q12_Answer.IsChecked == true) Q12_Explanation.Visibility = Visibility.Visible;
            if (Q13_Answer.IsChecked == true) Q13_Explanation.Visibility = Visibility.Visible;
            if (Q14_Answer.IsChecked == true) Q14_Explanation.Visibility = Visibility.Visible;
            if (Q15_Answer.IsChecked == true) Q15_Explanation.Visibility = Visibility.Visible;
            if (Q16_Answer.IsChecked == true) Q16_Explanation.Visibility = Visibility.Visible;
            if (Q17_Answer.IsChecked == true) Q17_Explanation.Visibility = Visibility.Visible;
            if (Q18_Answer.IsChecked == true) Q18_Explanation.Visibility = Visibility.Visible;
            if (Q19_Answer.IsChecked == true) Q19_Explanation.Visibility = Visibility.Visible;
            if (Q20_Answer.IsChecked == true) Q20_Explanation.Visibility = Visibility.Visible;
        }

        private void HideAllExplanations()
        {
            Q1_Explanation.Visibility = Visibility.Collapsed;
            Q2_Explanation.Visibility = Visibility.Collapsed;
            Q3_Explanation.Visibility = Visibility.Collapsed;
            Q4_Explanation.Visibility = Visibility.Collapsed;
            Q5_Explanation.Visibility = Visibility.Collapsed;
            Q6_Explanation.Visibility = Visibility.Collapsed;
            Q7_Explanation.Visibility = Visibility.Collapsed;
            Q8_Explanation.Visibility = Visibility.Collapsed;
            Q9_Explanation.Visibility = Visibility.Collapsed;
            Q10_Explanation.Visibility = Visibility.Collapsed;
            Q11_Explanation.Visibility = Visibility.Collapsed;
            Q12_Explanation.Visibility = Visibility.Collapsed;
            Q13_Explanation.Visibility = Visibility.Collapsed;
            Q14_Explanation.Visibility = Visibility.Collapsed;
            Q15_Explanation.Visibility = Visibility.Collapsed;
            Q16_Explanation.Visibility = Visibility.Collapsed;
            Q17_Explanation.Visibility = Visibility.Collapsed;
            Q18_Explanation.Visibility = Visibility.Collapsed;
            Q19_Explanation.Visibility = Visibility.Collapsed;
            Q20_Explanation.Visibility = Visibility.Collapsed;
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