﻿using System;
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
            MainWindow m = (MainWindow)Application.Current.MainWindow;
            m.Resize();
            m.application.Content = new registration();
        }

        private void Login_Button_Click(object sender, RoutedEventArgs e)
        {
            if (UserManagement.confirmLogin(email_input.Text, password_input.Password))
            {
                UserManagement.localEmail = email_input.Text;
                if (UserManagement.GetUserStatus(email_input.Text) == "student")
                {
                    UserManagement.localEmail = email_input.Text;
                    MainWindow m = (MainWindow)Application.Current.MainWindow;
                    m.Resize();
                    NavigationService.Navigate(new Students());
                }
                else if (UserManagement.GetUserStatus(email_input.Text) == "mentor")
                {
                    UserManagement.localEmail = email_input.Text;
                    MainWindow m = (MainWindow)Application.Current.MainWindow;
                    m.Resize();
                    NavigationService.Navigate(new Students(true));
                }

            }
        }
    }
}
