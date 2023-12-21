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

namespace Mentoring_App.Pages
{
    /// <summary>
    /// Interaktionslogik für mentorreg.xaml
    /// </summary>
    public partial class mentorreg : Page
    {
        public mentorreg()
        {
            InitializeComponent();
        }

        private void mentorreg_Click(object sender, RoutedEventArgs e)
        {
            UserManagement.MentorRegister();
        }
    }
}
