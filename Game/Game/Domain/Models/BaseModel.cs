namespace Game.Models
{
    /// <summary>
    /// Base model for records that get saved
    /// </summary>
    public class BaseModel<T> : DefaultModel
    {
        string _imageURI;
        /// <summary>
        /// Location to the image for the item.
        /// Will come from the server as a fully qualified URI example:  https://developer.android.com/images/robot-tiny.png
        /// </summary>
        public string ImageURI
        {
            get => _imageURI;
            set => SetProperty(ref _imageURI, value);
        }

        string _iconImageURI;
        /// <summary>
        /// Thumbnail icon image
        /// </summary>
        public string IconImageURI
        {
            get => _iconImageURI;
            set => SetProperty(ref _iconImageURI, value);
        }

        /// <summary>
        /// Update Method is virtual and changed for each class
        /// </summary>
        /// <param name="newData"></param>
        /// <returns></returns>
        public virtual bool Update(T newData) => true;
    }
}
