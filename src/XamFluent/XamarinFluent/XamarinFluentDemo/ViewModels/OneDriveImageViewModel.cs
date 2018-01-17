using ActiveDevelop.MvvmBaseLib;
using Microsoft.Graph;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamImage = Xamarin.Forms.Image;

namespace XamarinFluentDemo.ViewModels
{
    public class OneDriveImageViewModel : BindableBase
    {
        private ImageSource myThumbnailSource;
        private ImageSource myImageSource;
        private string myImageDescription;
        private string myId;

        private static Stopwatch myStopWatch = Stopwatch.StartNew();

        private async Task<XamImage> LoadImageAsync(string itemId)
        {
            GraphServiceClient client = AuthenticationHelper.GetAuthenticatedClient();
            XamImage image = new XamImage();

            using (var responseStream = await client.Me.Drive.Items[itemId].Content.Request().GetAsync())
            {
                if (responseStream is MemoryStream memoryStream)
                {
                    image.Source = ImageSource.FromStream(() => { return memoryStream; });
                }
                else
                {
                    using (memoryStream = new MemoryStream())
                    {
                        await responseStream.CopyToAsync(memoryStream);
                        memoryStream.Position = 0;
                        image.Source = ImageSource.FromStream(() => { return memoryStream; });
                    }
                }
                return image;
            }
        }

        private async Task<XamImage> LoadThumbnailAsync(string itemId)
        {
            GraphServiceClient client = AuthenticationHelper.GetAuthenticatedClient();
            XamImage image = new XamImage();

            var expandString = "thumbnails, children($expand=thumbnails)";

            try
            {
                var thumbnailSet = (await client.Me.Drive.Items[itemId].Request().Expand(expandString).GetAsync()).Thumbnails.FirstOrDefault();

                if (thumbnailSet!=null)
                {
                    if (thumbnailSet.Large!=null)
                    {
                        Debug.Print($"***** {myStopWatch.ElapsedMilliseconds} Setting Picture URI for itemID {itemId}");
                        UriImageSource uriImageSource = (UriImageSource) ImageSource.FromUri(new Uri(thumbnailSet.Large.Url));

                        using (var responseStream = await uriImageSource.GetStreamAsync())
                        {
                            byte[] bytes = new byte[responseStream.Length];
                                var result = await responseStream.ReadAsync(bytes, 0, (int)responseStream.Length);

                            image.Source = ImageSource.FromStream(() => new MemoryStream(bytes));
                            Debug.Print($"***** {myStopWatch.ElapsedMilliseconds} READY Setting Picture URI for itemID {itemId}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                if (Debugger.IsAttached)
                {
                    Debug.Print(ex.Message);
                    Debugger.Break();
                }
            }

            return image;
        }


        public string Id
        {
            get
            {
                return myId;
            }

            set
            {
                SetProperty(ref myId, value);
            }
        }

        public ImageSource ThumbnailSource
        {
            get 
            {
                if (myThumbnailSource==null)
                {
                    if (ProcessAsync)
                    {
                        Task.Run(async () =>
                        {
                            var image = await LoadThumbnailAsync(this.Id);
                            SetProperty(ref myThumbnailSource, image.Source);
                        });
                    }
                    else

                    {
                        // We need to "emulate sync" here.
                        var m = new ManualResetEvent(false);
                        XamImage image = null;

                        var task = Task.Run(() => 
                        {
                            image = LoadThumbnailAsync(this.Id).Result;
                            m.Set();
                        });

                        m.WaitOne();
                        return image.Source;
                    }
                }

                return myThumbnailSource;
            }

            set
            {
                SetProperty(ref myThumbnailSource, value);
            }
        }

        public ImageSource Image
        {
            get
            {
                if (myImageSource != null)
                {
                    Task.Run(async () =>
                    {
                        var image = await LoadImageAsync(this.Id);
                        SetProperty(ref myImageSource, image.Source);
                    });
                }

                return myImageSource;
            }

            set
            {
                SetProperty(ref myImageSource, value);
            }
        }

        public string ImageDescription
        {
            get
            {
                return myImageDescription;
            }

            set
            {
                SetProperty(ref myImageDescription, value);
            }
        }

        public bool ProcessAsync { get; set; }
    }

}


