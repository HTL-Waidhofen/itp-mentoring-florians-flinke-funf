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
    /// Interaktionslogik für Students.xaml
    /// </summary>
    public partial class Students : Page
    {
        public Students(bool mentor = false)
        {
            InitializeComponent();
            if(mentor)
                GetMenu();
        }

        private void GetMenu()
        {
            MenuItem mi = new MenuItem();
            mi.Header = "Mentor";
            mi.Visibility = Visibility.Visible;
            mainmenu.Items.Add(mi);
            mainmenu.UpdateLayout();
        }
    }
}
