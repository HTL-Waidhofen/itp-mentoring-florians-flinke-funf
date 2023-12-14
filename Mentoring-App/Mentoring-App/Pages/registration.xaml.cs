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
    /// Interaktionslogik für registration.xaml
    /// </summary>
    public partial class registration : Page
    {
        public registration()
        {
            InitializeComponent();
        }

        private void StudentRegister_Click(object sender, RoutedEventArgs e)
        {
            if (UserManagement.IsEmailValid(Email_TextBox.Text) == true && UserManagement.IsPasswordValid(Password_TextBox.Password, PasswordConfirm_TextBox.Password) == true)
            {
                UserManagement.StudentRegister(Name_TextBox.Text, Email_TextBox.Text, Password_TextBox.Password);
            }
            else
            {
                MessageBox.Show("Email or password not valid");
                throw new Exception();
            }
        }
        private void GoToMentorreg_Click(object sender, RoutedEventArgs e)
        {
            
            if (UserManagement.IsEmailValid(Email_TextBox.Text) == true && UserManagement.IsPasswordValid(Password_TextBox.Password, PasswordConfirm_TextBox.Password) == true)
            {
                UserManagement.localemail = Email_TextBox.Text;
                NavigationService.Navigate(new mentorreg());
                UserManagement.StudentRegister(Name_TextBox.Text, Email_TextBox.Text, Password_TextBox.Password);
            }
            else
            {
                MessageBox.Show("Email or password not valid");
                throw new Exception();
            }
        }
    }
}
