﻿using Xamarin.Forms;
using System;
using System.Net;
using System.IO;
using Plugin.Toasts;
using DAT190_Bachelor_Project.Model;
using DAT190_Bachelor_Project.View;
using SkiaSharp;
using SkiaSharp.Views.Forms;

namespace DAT190_Bachelor_Project
{
    public partial class FrontPage : ContentPage
    {

        OBPUtil obp;
        CarbonFootprint co2;

        public FrontPage()
        {
            InitializeComponent();

            co2 = new CarbonFootprint();

            obp = new OBPUtil("oob", "0yao55bdkuhka0g0ocxpsklwshfx5bh2jd3rqdbz", "qfwzyqdhmm4xz1zmqobfboa4sofnxhkoydgjsbjs");
            obp.getRequestToken(FinishWebRequest);

        }

        // ** CALLBACK METODE
        // ** Henter respons streng og åpner device browser og med OBP uri pg mottatt oauth token
        public void FinishWebRequest(IAsyncResult result)
        {
            WebResponse wr = obp.request.EndGetResponse(result);
            Stream dataStream = wr.GetResponseStream();
            wr.Dispose();
            StreamReader reader = new StreamReader(dataStream);
            string responseFromServer = reader.ReadToEnd();
            reader.Dispose();
            dataStream.Dispose();
            System.Diagnostics.Debug.WriteLine(responseFromServer);
            string oauth_token = responseFromServer.Split('&')[0];
            Uri uri = new Uri("https://apisandbox.openbankproject.com/oauth/authorize?" + oauth_token);
            System.Diagnostics.Debug.WriteLine(uri.AbsoluteUri);

            ShowToast(responseFromServer);
        }

        private void ShowToast(string text)
        {
            var notificator = DependencyService.Get<IToastNotificator>();

            var options = new NotificationOptions()
            {
                Title = "Open Bank Project Response:",
                Description = text,
                IsClickable = true
            };

            notificator.Notify(options);
        }

        private void OnPaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {

            float width = (float)carconFootprintCanvas.Width;
            float height = (float)carconFootprintCanvas.Height;



            EmissionsCakeView emissionsCake = new EmissionsCakeView(32, 6, 33, height, width, e, co2);
            emissionsCake.DrawCake();
            emissionsCake.DrawCenterHole();
            emissionsCake.DrawText();
            emissionsCake.DrawDots();
            emissionsCake.DrawIcons();

        }
        
    }


}
