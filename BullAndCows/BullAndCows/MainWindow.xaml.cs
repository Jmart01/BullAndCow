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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BullAndCows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int[] secretNum = { 0, 0, 0, 0 };
        public MainWindow()
        {
            InitializeComponent();
            Result_Txt.Text += "Please Enter A 4 Digit Number" + Environment.NewLine;
            GenerateNumber();
        }

        private void Guess_BTN_Click(object sender, RoutedEventArgs e)
        {
            CheckGuess(Guess_Txt.Text, secretNum);
        }

        private void NewNum_BTN_Click(object sender, RoutedEventArgs e)
        {
            Result_Txt.Text = "";
            Result_Txt.Text += "Please Enter A 4 Digit Number" + Environment.NewLine;
            GenerateNumber();
        }

        private void GenerateNumber()
        {
            Random newNum = new Random();
            for (int i = 0; i < secretNum.Length; i++)
            {
                secretNum[i] = newNum.Next(1, 9);
            }

            if (!nonRepeating(secretNum))
            {
                GenerateNumber();
            }
        }

        public bool nonRepeating(int[] array)
        {
            HashSet<int> set = new HashSet<int>(array);
            return (set.Count == array.Length);
        }

        private void CheckGuess(string guess, int[] superSecretNum)
        {
            int bullsCount = 0;
            int cowsCount = 0;
            if (guess.Length < 4)
            {
                Result_Txt.Text += "Please enter a 4 digit number" + Environment.NewLine;
                return;
            }

            if (!int.TryParse(guess, out int results))
            {
                Result_Txt.Text += "Invalid Guess" + Environment.NewLine;
                return;
            }

            for (int i = 0; i < 4; i++)
            {
                int currentGuess = (int)char.GetNumericValue(guess[i]);
                if (currentGuess < 1 || currentGuess > 9)
                {
                    Result_Txt.Text += "Digits must be greater than 0 and lower than 10" + Environment.NewLine;
                    return;
                }
                if (currentGuess == superSecretNum[i])
                {
                    bullsCount++;
                }
                else
                {
                    for (int j = 0; j < 4; j++)
                    {
                        if (currentGuess == superSecretNum[j])
                        {
                            cowsCount++;
                        }
                    }
                }
            }
            Bulls_Txt.Text = "Bulls: " + bullsCount;
            Cows_Txt.Text = "Cows: " + cowsCount;
            Result_Txt.Text += guess + Environment.NewLine;
            if (bullsCount == 4)
            {
                Result_Txt.Text += "You Have 4 Bulls! You Win!";
            }
        }
    }
}