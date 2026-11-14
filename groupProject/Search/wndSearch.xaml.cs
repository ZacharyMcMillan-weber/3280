using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace groupProject
{
    /// <summary>
    /// Initialized the search window
    /// </summary>
    public partial class wndSearch : Window
    {
        public wndSearch()
        {
            InitializeComponent();
        }


        /// <summary>
        /// For now closes the window, til I get further in the assignment
        /// </summary>
        private void btnSelect_Click(object sender, RoutedEventArgs e)
        {
            // Get the id from the selected row,
            // save it in a private field. main will read it through the id
            // after will set a variable to true so main knows to read and the invoice

            Close();
        }

        /// <summary>
        /// Closes the Search Window
        /// </summary>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
