using ActiveDevelop.MvvmBaseLib;
using Xamarin.Forms;

namespace XamarinFluentDemo.ViewModels
{
    public class OneDriveImageViewModel : BindableBase
    {
        private Image myThumbnail;
        private Image myImage;
        private string myImageDescription;

        public Image Thumbnail
        {
            get 
            {
                return myThumbnail;
            }

            set
            {
                SetProperty(ref myThumbnail, value);
            }
        }

        public Image Image
        {
            get
            {
                return myImage;
            }

            set
            {
                SetProperty(ref myImage, value);
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
    }


}
