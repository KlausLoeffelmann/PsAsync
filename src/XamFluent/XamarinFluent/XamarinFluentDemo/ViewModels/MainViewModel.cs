using ActiveDevelop.MvvmBaseLib;
using ActiveDevelop.MvvmBaseLib.Mvvm;
using Microsoft.Graph;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using XamImage = Xamarin.Forms.Image;

namespace XamarinFluentDemo.ViewModels
{
    public class MainViewModel : BindableBase
    {
        public const int MAX_PICS_TO_TAKE = 15;

        public static Rectangle IPhoneSafeRec;
        private static ImageSource myStaticDefaultPic;

        private ObservableCollection<OneDriveImageViewModel> myImages;
        private string myStatusLine;
        private ICommand myRefreshCommand;
        private ICommand myShowThumbnailsAsyncCommand;
        private ICommand myShowThumbnailsSyncCommand;
        private float myPhoneStatusLineMargin;
        private bool myLoadAsync;
        private bool myIsRefreshing;

        public MainViewModel()
        {
            myRefreshCommand = new RelayCommand((state)=>
            {

            });

            myShowThumbnailsSyncCommand = new RelayCommand(async (state) =>
            {
                myLoadAsync = false;
                IsRefreshing = true;
                Images = await GetPictureListAsync(CameraRollFolder);
                IsRefreshing = false;
            });

            myShowThumbnailsAsyncCommand = new RelayCommand(async (state) => 
            {
                myLoadAsync = true;
                IsRefreshing = true;
                Images = await GetPictureListAsync(CameraRollFolder);
                IsRefreshing = false;
            });

            // Getting the default pic from the Assets folder.
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

        public async Task<ObservableCollection<OneDriveImageViewModel>> GetPictureListAsync(DriveItem cameraRoll)
        {
            var imageCollection = new ObservableCollection<OneDriveImageViewModel>();

            if (cameraRoll != null)
            {
                var graphClient = AuthenticationHelper.GetAuthenticatedClient();

                var files = await graphClient.Me.Drive.Items[cameraRoll.Id].Children.Request().GetAsync();
                var images = files.Where(item => item.Image != null).Take(MAX_PICS_TO_TAKE);

                foreach (var pItem in images)
                {
                    imageCollection.Add(new OneDriveImageViewModel { ImageDescription = pItem.Name,
                                                                     Id=pItem.Id,
                                                                     ProcessAsync=myLoadAsync});
                }
            }

            return imageCollection;
        }

        public ICommand RefreshCommand
        {
            get
            {
                return myRefreshCommand;
            }

            set
            {
                SetProperty(ref myRefreshCommand, value);
            }
        }

        public ICommand ShowThumbnailsSyncCommand
        {
            get
            {
                return myShowThumbnailsSyncCommand;
            }

            set
            {
                SetProperty(ref myShowThumbnailsAsyncCommand, value);
            }
        }

        public ICommand ShowThumbnailsAsyncCommand
        {
            get
            {
                return myShowThumbnailsAsyncCommand;
            }

            set
            {
                SetProperty(ref myShowThumbnailsAsyncCommand , value);
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

        public bool IsRefreshing
        {
            get
            {
                return myIsRefreshing;
            }

            set
            {
                SetProperty(ref myIsRefreshing, value);
            }
        }

        public DriveItem CameraRollFolder { get; internal set; }

        public static ImageSource DefaultPic
        {
            get
            {
                if (myStaticDefaultPic==null)
                {
                    using (var fStream = new FileStream("Assets/hourglas3d.png", FileMode.Open))
                    {
                        byte[] bytes = new byte[fStream.Length];
                        var result = fStream.Read(bytes, 0, (int)fStream.Length);


                        myStaticDefaultPic = ImageSource.FromStream(() => new MemoryStream(bytes));
                    }
                }

                return myStaticDefaultPic;

            }
        }
    }
}
