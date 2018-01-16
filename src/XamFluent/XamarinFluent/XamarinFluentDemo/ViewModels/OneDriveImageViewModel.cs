using ActiveDevelop.MvvmBaseLib;
using Microsoft.Graph;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
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

            try
            {
                var thumbnailSet = (await client.Me.Drive.Items[itemId].Thumbnails.Request().GetAsync()).FirstOrDefault();
                if (thumbnailSet!=null)
                {
                    if (thumbnailSet.Large!=null)
                    {
                        image.Source = ImageSource.FromUri(new Uri(thumbnailSet.Large.Url));
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
                    Task.Run(async () =>
                    {
                        var image = await LoadThumbnailAsync(this.Id);
                        SetProperty(ref myThumbnailSource, image.Source);
                    });
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
