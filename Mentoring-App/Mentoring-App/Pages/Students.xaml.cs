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
        public List<string> subjectList = new List<string>() { "Deutsch", "Mathematik", "Englisch", "Geografie,Geschichte,Politische Bildung", "Naturwissenschaften", "Wirtschaft und Recht",
                                                            "Netzwerktechnik", "Softwareentwicklung", "Medientechnik", "Computerpraktikum", "IT-Sicherheit", "Informationstechnische Projekte",
                                                            "Informationssysteme", "Systemtechnik-E", "Systemtechnik", "Cloud Computing und industrielle Technologien"};

        public Students()
        {
            InitializeComponent();
        }

        private void subjectSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            subjects.Items.Clear();

            foreach (string subject in searchMatchingSubjects(subjectSearch.Text))
            {
                subjects.Items.Add(new TextBox() { Text = subject });
            }
        }

        public List<string> searchMatchingSubjects(string searchText)
        {
            List<string> matchList = new List<string>();

            foreach (string subject in subjectList)
            {
                if (subject.Contains(searchText))
                {
                    matchList.Add(subject);
                }
            }

            return matchList;
        }
    }
}
