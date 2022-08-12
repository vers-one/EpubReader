namespace VersOne.Epub.WpfDemo.Entities
{
    /// <summary>
    /// Application constants.
    /// </summary>
    public static class Constants
    {
        /// <summary>
        /// The name of the folder to store book cover images.
        /// </summary>
        public const string COVER_IMAGES_FOLDER = "Covers";

        /// <summary>
        /// The name of the embedded resource of the image used for books that don't have covers.
        /// </summary>
        public const string GENERIC_COVER_IMAGE_SOURCE = "/Resources/Book.png";

        /// <summary>
        /// The maximum width (in pixels) for the cover image.
        /// </summary>
        public const int COVER_IMAGE_MAX_WIDTH = 256;

        /// <summary>
        /// The maximum height (in pixels) for the cover image.
        /// </summary>
        public const int COVER_IMAGE_MAX_HEIGHT = 256;
    }
}
