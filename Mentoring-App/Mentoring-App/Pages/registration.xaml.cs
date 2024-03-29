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
            MainWindow m = (MainWindow)Application.Current.MainWindow;
            if (UserManagement.IsEmailValid(Email_TextBox.Text) == true)
            {
                if (UserManagement.IsPasswordValid(Password_TextBox.Password, PasswordConfirm_TextBox.Password) == true)
                {
                    UserManagement.localEmail = Email_TextBox.Text;

                    if (StudentOrMentor.SelectedIndex == 0)
                    {
                        Student s = new Student(Name_TextBox.Text, Email_TextBox.Text, Password_TextBox.Password);
                        UserManagement.students.Add(s);
                        UserManagement.AddStudentToDB(s);

                        m.application.Content = new Students();
                    }
                    else
                    {
                        mentorreg.GetUser(Name_TextBox.Text, Email_TextBox.Text, Password_TextBox.Password);
                        m.application.Content = new mentorreg();
                    }
                }
                else
                {
                    MessageBox.Show("Password is not valid!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Email is not valid!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
