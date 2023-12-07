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
using Mentoring_App.Pages;
namespace Mentoring_App.Pages
{
    /// <summary>
    /// Interaktionslogik für Login.xaml
    /// </summary>
    public partial class Login : Page
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Register_Button_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void Login_Button_Click(object sender, RoutedEventArgs e)
        {
            if (UserManagement.confirmLogin(email_input.Text, password_input.Password))
            {
                if (UserManagement.GetUserStatus(email_input.Text) == "student")
                {
                   NavigationService.Navigate(new Students());
                }
                else if (UserManagement.GetUserStatus(email_input.Text) == "mentor")
                {
                    NavigationService.Navigate(new MentorPage());
                }
            }
        }
    }
}
