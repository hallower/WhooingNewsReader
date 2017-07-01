﻿using Android.Widget;
using Xamarin.Forms.Platform.Android;
using System.ComponentModel;
using Android.Text;
using Xamarin.Forms;
using WhooingNewsReader.Droid;
using WhooingNewsReader.Views;
using Android.Text.Method;

[assembly: ExportRenderer(typeof(HtmlLabel), typeof(HtmlLabelRenderer))]

namespace WhooingNewsReader.Droid
{
    class HtmlLabelRenderer : LabelRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Label> e)
        {
            base.OnElementChanged(e);

            if (Control == null)
                return;

            Control.MovementMethod = LinkMovementMethod.Instance;
            Control?.SetText(Html.FromHtml(Element.Text), TextView.BufferType.Spannable);
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == Xamarin.Forms.Label.TextProperty.PropertyName)
            {
                if (Control == null)
                    return;

                Control.MovementMethod = LinkMovementMethod.Instance;
                Control?.SetText(Html.FromHtml(Element.Text), TextView.BufferType.Spannable);

            }
        }
    }
}