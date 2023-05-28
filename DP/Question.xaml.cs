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
using System.Windows.Shapes;

namespace DP
{
    /// <summary>
    /// Логика взаимодействия для Question.xaml
    /// </summary>
    public partial class Question : Window
    {

        public bool flag = false;
        public Question()
        {
            InitializeComponent();
        }

        private void button_no(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void button_yes(object sender, RoutedEventArgs e)
        {
            flag = true;
            this.Close();
        }
    }
}
