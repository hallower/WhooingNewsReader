using NewsReader.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Xamarin.Forms;

namespace NewsReader.Views
{
    public class MainBase : ContentPage
    {
        public MainBase()
        {
            MessagingCenter.Subscribe<Object, MessagingCenterAlert>(this, "alert", (sender, arg) =>
            {
                Debug.WriteLine("recv");
                if (arg is MessagingCenterAlert alert)
                {
                    DisplayAlert(alert.Title, alert.Message, "확인");
                }
            });
        }

        public void WhooingClicked(object sender, EventArgs e)
        {
            Device.OpenUri(new Uri("https://new.whooing.com"));
        }
    }
}
