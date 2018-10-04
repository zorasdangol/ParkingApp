using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ParkingApp.Views.Popups
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoadingPopupPage : Grid
	{
		public LoadingPopupPage ()
		{
			InitializeComponent ();
		}
	}
}