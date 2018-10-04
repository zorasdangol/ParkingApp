using ParkingApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ParkingApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ParkingMainTabbedPage : TabbedPage
	{
        public ParkingMainTabbedPageVM viewModel { get; set; }
		public ParkingMainTabbedPage ()
		{
			InitializeComponent ();
            BindingContext = viewModel = new ParkingMainTabbedPageVM();
		}
	}
}