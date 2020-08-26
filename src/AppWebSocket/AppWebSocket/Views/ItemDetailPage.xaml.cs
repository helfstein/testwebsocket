using System.ComponentModel;
using Xamarin.Forms;
using AppWebSocket.ViewModels;

namespace AppWebSocket.Views {
    public partial class ItemDetailPage : ContentPage {
        public ItemDetailPage() {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}