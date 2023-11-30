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
    /// Interaktionslogik für Mentor.xaml
    /// </summary>
    public partial class MentorPage : Page
    {
        public MentorPage()
        {
            InitializeComponent();
        }

        private void LoadComboboxes(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < 24; i++)
            {
                HoursComboBox.Items.Add(i.ToString("00"));
            }

            for (int i = 0; i < 60; i += 5)
            {
                MinutesComboBox.Items.Add(i.ToString("00"));
            }

            for (int i = 0; i < 24; i++)
            {
                Hours2ComboBox.Items.Add(i.ToString("00"));
            }

            for (int i = 0; i < 60; i += 5)
            {
                Minutes2ComboBox.Items.Add(i.ToString("00"));
            }
        }
    }
}
