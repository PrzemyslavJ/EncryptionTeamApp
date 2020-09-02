using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace EncryptionTeamApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        string InputPasswordText, InputCodeText;
        string FileToEncryptText, CypherCodeFileText, ResultEncryptFileText;
        string Filemame;

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            InputText.Text = "";
            CodeText.Text = "";
            OutputText.Text = "";
        }

        private void ReadFileButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog EnFile = new OpenFileDialog();
            EnFile.DefaultExt = ".txt";
            EnFile.Filter = "Text Document (.txt)|*.txt";
            if(EnFile.ShowDialog() == true)
            {
                Filemame = EnFile.FileName;
                FileText.Text = File.ReadAllText(Filemame);
            }
        }

        private void CodeFileButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog CodeFile = new OpenFileDialog();
            CodeFile.DefaultExt = ".txt";
            CodeFile.Filter = "Text Document (.txt)|*.txt";
            if (CodeFile.ShowDialog() == true)
            {
                string Codemame = CodeFile.FileName;
                CodeFileText.Text = File.ReadAllText(Codemame);
            }
        }

        private void ReplaceFileButton_Click(object sender, RoutedEventArgs e)
        {
            if (CodeFileText.Text.Length > 0 && FileText.Text.Length > 0 && ResultFileText.Text.Length > 0)
            {
                if( OptionsFile.SelectedIndex == 0 || OptionsFile.SelectedIndex == 1){
                        MessageBoxResult result = MessageBox.Show("Czy napewno chcesz nadpisać plik szyfem ?", "Komunikat", MessageBoxButton.YesNo);

                        if (result == MessageBoxResult.Yes)
                        {
                            ResultEncryptFileText = ResultFileText.Text;
                            string text = File.ReadAllText(Filemame);
                            text = text.Replace(text, ResultEncryptFileText);
                            File.WriteAllText(Filemame, text);
                            FileText.Text = File.ReadAllText(Filemame);
                            MessageBox.Show("Dokonano pomyślnego nadpisania pliku", "Komunikat");
                        }
                    }
                else
                    MessageBox.Show("Najpierw wybierz opcję szyfrowania", "Komunikat");

            }
            else
                MessageBox.Show("Najpierw dokonaj szyfrowania !", "Komunikat");

        }

        private void OptionsFile_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            label_opt.Content = "";
        }

        private void ClearButtonFile_Click(object sender, RoutedEventArgs e)
        {
            FileText.Text = "";
            CodeFileText.Text = "";
            ResultFileText.Text = "";
        }

        private void ExecutionFileButton_Click(object sender, RoutedEventArgs e)
        {
            Encryptor ObjFile = new Encryptor();
            FileToEncryptText = FileText.Text;
            CypherCodeFileText = CodeFileText.Text;

            if (FileToEncryptText.Length > 0 && CypherCodeFileText.Length > 0)
            {
                if (OptionsFile.SelectedIndex == 0)
                {
                    ResultFileText.Text = ObjFile.Encrypt(FileToEncryptText, CypherCodeFileText);
                }

                else if (OptionsFile.SelectedIndex == 1)
                {
                    ResultFileText.Text = ObjFile.Decrypt(FileToEncryptText, CypherCodeFileText);
                }
                else
                {
                    MessageBox.Show("Wybierz opcję szyfrowania lub deszyfrowania !", "Komunikat");
                }
            }
            else
                MessageBox.Show("Najpierw wprowadź hasło i kod szyfrowania !", "Komunikat");
        }

        private void CodeButton_Click(object sender, RoutedEventArgs e)
        {
            Encryptor ObjPassword = new Encryptor();
            InputPasswordText = InputText.Text;
            InputCodeText = CodeText.Text;

            if (InputPasswordText.Length > 0 && InputCodeText.Length > 0)
            {
                if (CodeOptions.SelectedIndex == 0)
                { 
                    OutputText.Text = ObjPassword.Encrypt(InputPasswordText, InputCodeText);
                }

                else if (CodeOptions.SelectedIndex == 1)
                {
                    OutputText.Text = ObjPassword.Decrypt(InputPasswordText, InputCodeText);
                }
                else
                {
                    MessageBox.Show("Wybierz opcję szyfrowania lub deszyfrowania !", "Komunikat");
                }
            }
            else
                MessageBox.Show("Najpierw wprowadź hasło i kod szyfrowania !", "Komunikat");
        }
    }
}
