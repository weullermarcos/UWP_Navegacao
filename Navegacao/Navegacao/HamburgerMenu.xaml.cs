﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Core;
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
    public sealed partial class HamburgerMenu : Page
    {
        //Substituindo o frame da página pelo RootFrame definido no App.xaml.cs
        public new Frame Frame => App.RootFrame;

        private List<Contato> meusContatos;


        public HamburgerMenu()
        {
            this.InitializeComponent();

            Frame.Navigated += Frame_Navigated;
        }

        private async void Frame_Navigated(object sender, NavigationEventArgs e)
        {
            //Cri uma instância da lista de contatos
            meusContatos = new List<Contato>();

            MySplitView.Content = Frame;

            if (Frame.CanGoBack)
                Windows.UI.Core.SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = Windows.UI.Core.AppViewBackButtonVisibility.Visible;
            else
                Windows.UI.Core.SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = Windows.UI.Core.AppViewBackButtonVisibility.Collapsed;

            //Verifica se a lista de contatos já foi carregada.
            if (meusContatos?.Count > 0)
                return;

            //Caso a lista de contatos ainda não foi carregada, carregue utilizando o arquivo json.
            StorageFile file = await StorageFile.GetFileFromApplicationUriAsync(new Uri(@"ms-appx:///ListaDeContatos.json"));

            using (StreamReader sRead = new StreamReader(await file.OpenStreamForReadAsync()))
            {
                string json = await sRead.ReadToEndAsync();
                meusContatos = JsonConvert.DeserializeObject<List<Contato>>(json);
            }
        }

        private void btnMenu_Click(object sender, RoutedEventArgs e)
        {
            MySplitView.IsPaneOpen = !MySplitView.IsPaneOpen;
        }

        private void Favoritos_Tapped(object sender, TappedRoutedEventArgs e)
        {
            //Filtra contatos favoritos
            List<Contato> favoritos = new List<Contato>(meusContatos.Where(x => x.IsFavorito).OrderBy(x => x.Nome));

            //Navega e passa contatos favoritos como parâmetro
            Frame.Navigate(typeof(Favoritos), favoritos);
        }

        private void Todos_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(Todos), meusContatos);
        }

        private void Email_Tapped(object sender, TappedRoutedEventArgs e)
        {
            //Filtra contatos com e-mail
            List<Contato> email = new List<Contato>(meusContatos.Where(x => !string.IsNullOrWhiteSpace(x.Email)).OrderBy(x => x.Nome));
            
            Frame.Navigate(typeof(Emails), email);
        }

        private void Telefones_Tapped(object sender, TappedRoutedEventArgs e)
        {
            //Filtra contatos com telefone
            List<Contato> telefones = new List<Contato>(meusContatos.Where(x => !string.IsNullOrWhiteSpace(x.Telefone)).OrderBy(x => x.Nome));

            Frame.Navigate(typeof(Telefones), telefones);
        }
    }
}
