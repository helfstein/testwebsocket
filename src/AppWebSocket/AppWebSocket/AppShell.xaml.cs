using System;
using System.Collections.Generic;
using AppWebSocket.ViewModels;
using AppWebSocket.Views;
using Xamarin.Forms;

namespace AppWebSocket {
    public partial class AppShell : Xamarin.Forms.Shell {
        public AppShell() {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
        }

        private async void OnMenuItemClicked(object sender, EventArgs e) {
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}
