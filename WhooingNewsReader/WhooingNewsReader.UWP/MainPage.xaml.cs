﻿namespace WhooingNewsReader.UWP
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            this.InitializeComponent();
            LoadApplication(new WhooingNewsReader.App());
        }
    }
}