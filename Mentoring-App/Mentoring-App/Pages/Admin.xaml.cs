using MaterialDesignThemes.Wpf.Converters;
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
    /// Interaktionslogik für Admin.xaml
    /// </summary>
    public partial class Admin : Page
    {
        public Admin()
        {
            Update();
            InitializeComponent();
        }

        private void button_accept_Click(object sender, RoutedEventArgs e)
        {
            Mentor m = outputMentor.SelectedItem as Mentor;
            m.IsApproved = true;
            UserManagement.UpdateMentor(m);
            Update();
        }

        private void button_reject_Click(object sender, RoutedEventArgs e)
        {
            Mentor m = outputMentor.SelectedItem as Mentor;
            m.IsApproved = false;
            UserManagement.UpdateMentor(m);
            Update();
        }

        private void button_delete_Click(object sender, RoutedEventArgs e)
        {
            Mentor m = currentMentors.SelectedItem as Mentor;
            UserManagement.DeleteMentor(m);
            Update();
        }
        public void Update()
        {
            currentMentors.ItemsSource = UserManagement.GetApproved();
            outputMentor.ItemsSource = UserManagement.GetAwaiting();
            outputName.Content = "";
            outputClass.Content = "";
            outputSubjects.Content = "";
        }

        private void outputMentor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Mentor m = outputMentor.SelectedItem as Mentor;
            outputName.Content = "Name: " + m.Name;
            outputClass.Content = "Jhg.: " + m.Grade;
            outputSubjects.Content = "Fächer: " + m.Subjects;
        }

        private void Shutdown_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
