using HkMobileApp.Droid.Services;
using HkMobileApp.Services;
using HkMobileApp.ViewModels;
using System;
using System.IO;
using System.Net;
using Xamarin.Essentials;
using Xamarin.Forms;

[assembly:Dependency(typeof(FileSavePdf))]
namespace HkMobileApp.Droid.Services
{
    public class FileSavePdf : IFileSavePdf
    {
        public bool DownloadPdf()
        {
            try
            {
                var webClient = new WebClient();

                webClient.DownloadDataCompleted += (s, e) => {
                     
                    byte[] data = e.Result;

                    //string documentsPath = Android.OS.Environment.DirectoryDownloads;
                    //string documentsPath = Android.App.Application.Context.GetExternalFilesDir("").AbsolutePath;
                    string documentsPath = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDownloads).AbsolutePath;

                    string localFilename = $"bronchure.pdf";

                    File.WriteAllBytes(Path.Combine(documentsPath, localFilename), data);

                    MainThread.BeginInvokeOnMainThread(async () => {

                        await DependencyService.Get<PolicyDetailsViewModel>().ShowSuccessMessage("File Downloaded Successfully");
                    
                    });
                };
                var url = new Uri("http://www.africau.edu/images/default/sample.pdf");
                webClient.DownloadDataAsync(url);



                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }
    }
}