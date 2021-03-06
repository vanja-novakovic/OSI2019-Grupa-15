﻿using Eve.Helpers;
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
using System.Windows.Shapes;

namespace Eve
{
    /// <summary>
    /// Interaction logic for AfterLoginForm.xaml
    /// </summary>
    public partial class WithAccount : Window
    {
        public WithAccount()
        {
            InitializeComponent();
        }

        private void EventButton_Click(object sender, RoutedEventArgs e)
        {
            WindowHelper.ShowWindow(this, new Eve.Events.MainWindow(ApplicationMode.REGISTERED_USER) { PreviousWindow = new WithAccount() });
        }

        private void CategoryButton_Click(object sender, RoutedEventArgs e)
        {
            WindowHelper.ShowWindow(this, new Eve.Categories.MainWindow() { PreviousWindow = new WithAccount() });
        }

        private void SignOutButton_Click(object sender, RoutedEventArgs e)
        {
            WindowHelper.ShowWindow(this, new LoginWindow());
        }
    }
}
