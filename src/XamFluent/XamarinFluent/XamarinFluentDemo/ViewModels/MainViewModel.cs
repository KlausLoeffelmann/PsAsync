using ActiveDevelop.MvvmBaseLib;
using ActiveDevelop.MvvmBaseLib.Mvvm;
using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using XamImage = Xamarin.Forms.Image;

namespace XamarinFluentDemo.ViewModels
{
    public class MainViewModel : BindableBase
    {
        public static Rectangle IPhoneSafeRec;

        private ObservableCollection<OneDriveImageViewModel> myImages;
        private string myStatusLine;
        private ICommand myRefreshCommand;
        private ICommand myTestCommand;
        private float myPhoneStatusLineMargin;

        public MainViewModel()
        {
            myRefreshCommand = new RelayCommand((state)=>
            {

            });

            myTestCommand = new RelayCommand((state) =>
            {

            });
        }

        public async Task<DriveItem> FindCameraRollFolderAsync()
        {
            var expandString = "thumbnails, children($expand=thumbnails)";
            DriveItem cameraRollFolder=null;
            try
            {
                var graphClient = AuthenticationHelper.GetAuthenticatedClient();

                var root = await graphClient.Me.Drive.Root.Request().Expand(expandString).GetAsync();
                var folders = root.Children.CurrentPage.Where(child => child.Folder != null ||
                                                               child.Image != null);

                cameraRollFolder = folders.Where(item => item.SpecialFolder?.Name == "cameraRoll").FirstOrDefault();
            }

            catch (ServiceException ex)
            {
                Debug.Print(ex.Message);
            }

            return cameraRollFolder;
        }

        public async Task<ObservableCollection<OneDriveImageViewModel>> GetThumbnailsAsync(DriveItem cameraRoll)
        {
            var imageCollection = new ObservableCollection<OneDriveImageViewModel>();

            if (cameraRoll != null)
            {
                var graphClient = AuthenticationHelper.GetAuthenticatedClient();

                var files = await graphClient.Me.Drive.Items[cameraRoll.Id].Children.Request().GetAsync();
                var images = files.Where(item => item.Image != null);

                var count = 1;

                foreach (var pItem in images)
                {
                    var image = await LoadImageAsync(pItem.Id);
                    imageCollection.Add(new OneDriveImageViewModel { ImageDescription = pItem.Description });
                    Debug.Print($"Loaded No: {count++} - Picture: {pItem.Name}, Size: {new Size(image.Width, image.Height).ToString()}");
                }
            }

            return imageCollection;
        }

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

        public ICommand RefreshCommand
        {
            get
            {
                return myRefreshCommand;
            }

            set
            {
                myRefreshCommand = value;
            }
        }

        public ICommand TestCommand
        {
            get
            {
                return myTestCommand;
            }

            set
            {
                myTestCommand = value;
            }
        }

        public ObservableCollection<OneDriveImageViewModel> Images
        {
            get
            {
                return myImages;
            }

            set
            {
                SetProperty(ref myImages, value);
            }
        }

        public string StatusLine
        {
            get
            {
                return myStatusLine;
            }

            set
            {
                SetProperty(ref myStatusLine, value);
            }
        }

        public float PhoneStatusLineMargin
        {
            get
            {
                return myPhoneStatusLineMargin;
            }

            set
            {
                SetProperty(ref myPhoneStatusLineMargin, value);
            }
        }
    }
}
