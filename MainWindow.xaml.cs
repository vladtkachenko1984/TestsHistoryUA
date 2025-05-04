using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace QuizApp
{
    public partial class MainWindow : Window
    {
        private Dictionary<string, bool> correctAnswers = new Dictionary<string, bool>
        {
            {"Q1_Answer", true},
            {"Q2_Answer", true},
            {"Q3_Answer", true},
            {"Q4_Answer", true},
            {"Q5_Answer", true},
            {"Q6_Answer", true},
            {"Q7_Answer", true},
            {"Q8_Answer", true},
            {"Q9_Answer", true},
            {"Q10_Answer", true},
            {"Q11_Answer", true},
            {"Q12_Answer", true},
            {"Q13_Answer", true},
            {"Q14_Answer", true},
            {"Q15_Answer", true},
            {"Q16_Answer", true},
            {"Q17_Answer", true},
            {"Q18_Answer", true},
            {"Q19_Answer", true},
            {"Q20_Answer", true}
        };

        public MainWindow()
        {
            InitializeComponent();
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            int correctCount = 0;
            int totalQuestions = correctAnswers.Count;
            List<string> correctList = new List<string>();
            List<string> incorrectList = new List<string>();

            foreach (var entry in correctAnswers)
            {
                CheckBox checkBox = FindName(entry.Key) as CheckBox;
                if (checkBox != null)
                {
                    if (checkBox.IsChecked == entry.Value)
                    {
                        correctCount++;
                        correctList.Add(entry.Key);
                    }
                    else
                    {
                        incorrectList.Add(entry.Key);
                    }
                }
            }

            string correctText = correctList.Count > 0 ? "\nПравильні: " + string.Join(", ", correctList) : "";
            string incorrectText = incorrectList.Count > 0 ? "\nНеправильні: " + string.Join(", ", incorrectList) : "";

            string resultMessage = $"Правильних відповідей: {correctCount} з {totalQuestions}.{correctText}{incorrectText}";

            MessageBox.Show(resultMessage, "Результат тесту", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}

