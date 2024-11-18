using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace MasterMind_1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DispatcherTimer timer = new DispatcherTimer();
        private string[] colors = { "Rood", "Geel", "Oranje", "Wit", "Groen", "Blauw" };
        private string[] secretCode;
        public MainWindow()
        {
            InitializeComponent();
            GenerateCode();
            FillComboBoxes();
            AttachColorChangeHandlers();
            
        }
        private void GenerateCode()
        {
          
            Random random = new Random();
            secretCode = new string[4];
            for (int i = 0; i < 4; i++)
            {
                secretCode[i] = colors[random.Next(colors.Length)];
            }

           
            this.Title = $"MasterMind ({string.Join(",", secretCode)})";
        }

        private void FillComboBoxes()
        {
          
            foreach (var comboBox in new[] { Color1, Color2, Color3, Color4 })
            {
                comboBox.ItemsSource = colors;
            }
        }

        private void AttachColorChangeHandlers()
        {
           
            Color1.SelectionChanged += (s, e) => UpdateBorderColor(Result1, Color1.Text);
            Color2.SelectionChanged += (s, e) => UpdateBorderColor(Result2, Color2.Text);
            Color3.SelectionChanged += (s, e) => UpdateBorderColor(Result3, Color3.Text);
            Color4.SelectionChanged += (s, e) => UpdateBorderColor(Result4, Color4.Text);
        }

        private void UpdateBorderColor(Border border, string color)
        {
            try
            {
                border.Background = (Brush)new BrushConverter().ConvertFromString(color);
            }
            catch
            {
                border.Background = Brushes.White; 
            }
        }

        private void ValidateButton_Click(object sender, RoutedEventArgs e)
        {
        
            var guess = new[] { Color1.Text, Color2.Text, Color3.Text, Color4.Text };
            ValidateGuess(guess);
        }

        private void ValidateGuess(string[] guess)
        {
            for (int i = 0; i < 4; i++)
            {
                if (guess[i] == secretCode[i])
                {
            
                    UpdateBorderValidation(Result1, i, Brushes.Green);
                }
                else if (secretCode.Contains(guess[i]))
                {
                  
                    UpdateBorderValidation(Result1, i, Brushes.Orange);
                }
                else
                {
               
                    UpdateBorderValidation(Result1, i, Brushes.Red);
                }
            }
        }

        private void UpdateBorderValidation(Border border, int index, Brush color)
        {
         
            var borders = new[] { Result1, Result2, Result3, Result4 };
            borders[index].BorderBrush = color;
        }

         
     
    }
}