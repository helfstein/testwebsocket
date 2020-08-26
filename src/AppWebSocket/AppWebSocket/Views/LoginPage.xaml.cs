using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppWebSocket.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppWebSocket.Views {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage {
        public LoginPage() {
            InitializeComponent();
            this.BindingContext = new LoginViewModel();
        }
    }
}