using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace Kursach_Zherzdev
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>


    public partial class MainWindow : Window
    {
        public bool cypherWordApplied = false;
        public static string alphabet = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя";
        public string keyWord = "";
        public MainWindow()
        {
            InitializeComponent();


        }

        private void FileChoose(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "txt files (*.txt)|*.txt";
            dialog.FilterIndex = 2;
            if (dialog.ShowDialog() == true)
            {
                using (StreamReader rdr = new StreamReader($"{dialog.FileName}", Encoding.UTF8))
                {
                    InputTextBox.Text = rdr.ReadToEnd();
                }

            }


        }


        public void CryptInput(object sender, RoutedEventArgs e)
        {
            string inputT = InputTextBox.Text;
            string result = "";
            int kwIndex = 0;

            foreach (char symbol in inputT)
            {
                if (!alphabet.Contains(symbol))
                {
                    result += symbol;
                }
                else
                {
                    int cryptCharIndex = (Array.IndexOf(alphabet.ToArray(), symbol) +
                        Array.IndexOf(alphabet.ToArray(), keyWord[kwIndex])) % alphabet.Length;
                    result += alphabet[cryptCharIndex];

                    kwIndex++;
                    if (kwIndex == keyWord.Length)
                    {
                        kwIndex = 0;
                    }
                }
            }
            OutputTextBox.Text = result;



        }

        private void DecryptInfo(object sender, RoutedEventArgs e)
        {

            string inputT = InputTextBox.Text;
            string result = "";
            int kwIndex = 0;
            foreach (char symbol in inputT)
            {
                if (!alphabet.Contains(symbol))
                {
                    result += symbol;
                }
                else
                {
                    int deCryptCharIndex = (Array.IndexOf(alphabet.ToArray(), symbol) + alphabet.Length - Array.IndexOf(alphabet.ToArray(), keyWord[kwIndex])) % alphabet.Length;
                    result += alphabet[deCryptCharIndex];
                    kwIndex++;
                    if (kwIndex == keyWord.Length)
                    {
                        kwIndex = 0;
                    }
                }
            }
            OutputTextBox.Text = result;


        }

        private void SaveFileButton(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Текстовый документ (*.txt)|*.txt|Все файлы (*.*)|*.*";

            if (saveFileDialog.ShowDialog() == true)
            {
                using (StreamWriter sw = new StreamWriter(saveFileDialog.FileName))
                {
                    sw.WriteLine(OutputTextBox.Text);
                    sw.Close();
                }
            }
        }

        private void InputCypherTextStarted(object sender, TextChangedEventArgs e)
        {
            if (KeyInputTextBox.Text.Length != 0)
            {
                CypherApplyButton.IsEnabled = true;
            }
            else
            {
                CypherApplyButton.IsEnabled = false;
            }
        }

        private void CypherApplyButton_Click(object sender, RoutedEventArgs e)
        {
            if (!Regex.IsMatch(KeyInputTextBox.Text, @"\P{IsCyrillic}"))
            {
                MessageBox.Show("Шифр успешно применен!");
                cypherWordApplied = true;
                if (InputTextBox.Text.Length != 0)
                {
                    CryptButton.IsEnabled = true;
                    DecryptButton.IsEnabled = true;
                    keyWord = KeyInputTextBox.Text;
                }

            }
            else
            {
                MessageBox.Show("Ключ должнен содержать только кириллицу!");
                cypherWordApplied = false;
                CryptButton.IsEnabled = false;
                DecryptButton.IsEnabled = false;
            }
        }

        private void InputText_Changed(object sender, TextChangedEventArgs e)
        {
            if (InputTextBox.Text.Length != 0 && (CypherApplyButton.IsEnabled == true))
            {
                CryptButton.IsEnabled = true;
                DecryptButton.IsEnabled = true;
            }
            else
            {
                CryptButton.IsEnabled = false;
                DecryptButton.IsEnabled = false;
            }
        }
        // SUPERTESTS HERE
        public string DecryptMethod(string inputT)
        {
            string result = "";
            int kwIndex = 0;
            keyWord = "скорпион";


            foreach (char symbol in inputT)
            {
                if (!alphabet.Contains(symbol))
                {
                    result += symbol;
                }
                else
                {
                    int deCryptCharIndex = (Array.IndexOf(alphabet.ToArray(), symbol) + alphabet.Length - Array.IndexOf(alphabet.ToArray(), keyWord[kwIndex])) % alphabet.Length;
                    result += alphabet[deCryptCharIndex];
                    kwIndex++;
                    if (kwIndex == keyWord.Length)
                    {
                        kwIndex = 0;
                    }
                }
            }
            OutputTextBox.Text = result;
            return result;
        
    }
        public string CryptMethod(string inputT)
        {
            string result = "";
            int kwIndex = 0;
            keyWord = "скорпион";


            foreach (char symbol in inputT)
            {
                if (!alphabet.Contains(symbol))
                {
                    result += symbol;
                }
                else
                {
                    int cryptCharIndex = (Array.IndexOf(alphabet.ToArray(), symbol) +
                        Array.IndexOf(alphabet.ToArray(), keyWord[kwIndex])) % alphabet.Length;
                    result += alphabet[cryptCharIndex];
                    kwIndex++;
                    if (kwIndex == keyWord.Length)
                    {
                        kwIndex = 0;
                    }
                }
            }
            OutputTextBox.Text = result;
            return result;
        
    }
    }
}
