using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PresionArter.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InicioView : ContentPage
    {
        public InicioView()
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new NavigationPage(new PresionView());
        }
    }
}