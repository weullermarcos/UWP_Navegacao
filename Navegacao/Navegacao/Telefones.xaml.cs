﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Navegacao
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Telefones : Page
    {
        public new Frame Frame => App.RootFrame;
        List<Contato> telefones = new List<Contato>();

        public Telefones()
        {
            this.InitializeComponent();

            //Criando evento Navigated
            Frame.Navigated += Frame_Navigated;
        }

        private void Frame_Navigated(object sender, NavigationEventArgs e)
        {
            telefones = e.Parameter as List<Contato>;

            if (telefones == null)
                return;

            myListView.ItemsSource = telefones;

        }


    private void Button_Click(object sender, RoutedEventArgs e)
    {
        Frame.Navigate(typeof(Todos), "Telefones");
    }

    private void Button_Click_1(object sender, RoutedEventArgs e)
    {
        Frame.Navigate(typeof(Emails), "Telefones");
    }

    private void Button_Click_2(object sender, RoutedEventArgs e)
    {
        Frame.Navigate(typeof(Favoritos), "Telefones");
    }

    }
}
