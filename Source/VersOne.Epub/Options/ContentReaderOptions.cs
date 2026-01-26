using System;

namespace VersOne.Epub.Options
{
    /// <summary>
    /// Various options to configure the behavior of the EPUB content reader which is used for loading local content files.
    /// </summary>
    public class ContentReaderOptions
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ContentReaderOptions" /> class.
        /// </summary>
        /// <param name="preset">An optional preset to initialize the <see cref="ContentReaderOptions" /> class with a predefined set of options.</param>
        public ContentReaderOptions(EpubReaderOptionsPreset? preset = null)
        {
            switch (preset)
            {
                case EpubReaderOptionsPreset.IGNORE_ALL_ERRORS:
                    IgnoreMissingFileError = true;
                    IgnoreFileIsTooLargeError = true;
                    IgnoreRemoteEpub3NavigationFileError = true;
                    SkipItemsWithDuplicateHrefs = true;
                    SkipItemsWithDuplicateFilePaths = true;
                    SkipItemsWithDuplicateUrls = true;
                    break;
            }
        }

        /// <summary>
        /// <para>
        /// Occurs when a local content file is listed in the EPUB manifest, but the content reader is unable to find it in the EPUB archive.
        /// This event lets the application to be notified of such errors and to decide how EPUB content reader should handle the missing file.
        /// </para>
        /// <para>
        /// Setting <see cref="IgnoreMissingFileError" /> property to <c>true</c> does not prevent this event from being fired.
        /// </para>
        /// </summary>
        public event EventHandler<ContentFileMissingEventArgs>? ContentFileMissing;

        /// <summary>
        /// <para>
        /// Gets or sets a value indicating whether the content reader should ignore the error when a local content file is listed in the EPUB manifest,
        /// but the reader is unable to find it in the EPUB archive.
        /// </para>
        /// <para>
        /// If it's set to <c>false</c> and the local content file referenced by a manifest item is missing,
        /// and the exception was not suppressed by the <see cref="ContentFileMissing" /> event handler,
        /// then the "EPUB parsing error: file ... was not found in the EPUB file." exception will be thrown.
        /// This exception can be suppressed by setting this property to <c>true</c>, in which case the content reader will ignore the error
        /// and will treat the missing content file as an existing empty file.
        /// </para>
        /// <para>
        /// Default value is <c>false</c>.
        /// </para>
        /// </summary>
        public bool IgnoreMissingFileError { get; set; }

        /// <summary>
        /// <para>
        /// Gets or sets a value indicating whether the content reader should ignore the error when a local content file is larger than 2 GB.
        /// </para>
        /// <para>
        /// If it's set to <c>false</c> and the local content file referenced by a manifest item is larger than 2 GB,
        /// then the "EPUB parsing error: file ... is larger than 2 GB." exception will be thrown.
        /// This exception can be suppressed by setting this property to <c>true</c>, in which case the content reader will ignore the error
        /// and will treat the large content file as an empty file.
        /// </para>
        /// <para>
        /// Default value is <c>false</c>.
        /// </para>
        /// </summary>
        public bool IgnoreFileIsTooLargeError { get; set; }

        /// <summary>
        /// <para>
        /// Gets or sets a value indicating whether the content reader should ignore the error when the EPUB 3 navigation manifest item
        /// is referencing a remote resource.
        /// </para>
        /// <para>
        /// If it's set to <c>false</c> and the content file referenced by the navigation manifest item is a remote resource
        /// (i.e. the 'href' attribute of the manifest item is a remote URL),
        /// then the "Incorrect EPUB manifest: EPUB 3 navigation document ... cannot be a remote resource." exception will be thrown.
        /// This exception can be suppressed by setting this property to <c>true</c>, in which case the content reader will ignore the error.
        /// The navigation content file will still be added to the collection of remote HTML/XHTML content files, even if the error was ignored.
        /// </para>
        /// <para>
        /// Default value is <c>false</c>.
        /// </para>
        /// </summary>
        public bool IgnoreRemoteEpub3NavigationFileError { get; set; }

        /// <summary>
        /// <para>
        /// Gets or sets a value indicating whether the content reader should skip local content files with duplicate href values.
        /// </para>
        /// <para>
        /// If it's set to <c>false</c> and a local content file with a duplicate href is found in the EPUB manifest,
        /// then the "Incorrect EPUB manifest: item with href = ... is not unique." exception will be thrown.
        /// This exception can be suppressed by setting this property to <c>true</c>, in which case only the first file within each duplicate item set
        /// will be added to the <see cref="EpubContentCollection{TLocalContentFile, TRemoteContentFile}" />
        /// or the <see cref="EpubContentCollectionRef{TLocalContentFileRef, TRemoteContentFileRef}" /> collections and the others will be skipped.
        /// </para>
        /// <para>
        /// Default value is <c>false</c>.
        /// </para>
        /// </summary>
        public bool SkipItemsWithDuplicateHrefs { get; set; }

        /// <summary>
        /// <para>
        /// Gets or sets a value indicating whether the content reader should skip local content files with duplicate file paths.
        /// </para>
        /// <para>
        /// If it's set to <c>false</c> and a local content file with a duplicate file path is found in the EPUB manifest,
        /// then the "Incorrect EPUB manifest: item with absolute file path = ... is not unique." exception will be thrown.
        /// This exception can be suppressed by setting this property to <c>true</c>, in which case only the first file within each duplicate item set
        /// will be added to the <see cref="EpubContentCollection{TLocalContentFile, TRemoteContentFile}" />
        /// or the <see cref="EpubContentCollectionRef{TLocalContentFileRef, TRemoteContentFileRef}" /> collections and the others will be skipped.
        /// </para>
        /// <para>
        /// Most content files with duplicate file paths will also have duplicate href values which will be handled by the
        /// <see cref="SkipItemsWithDuplicateHrefs" /> configuration option. However, it is possible for two content files to have
        /// different relative paths in their 'href' attributes that point to the same absolute path
        /// (e.g. 'Chapters/chapter1.html' and '../Content/Chapters/chapter1.html', assuming that the package file
        /// is located inside the 'Content' directory).
        /// </para>
        /// <para>
        /// Default value is <c>false</c>.
        /// </para>
        /// </summary>
        public bool SkipItemsWithDuplicateFilePaths { get; set; }

        /// <summary>
        /// <para>
        /// Gets or sets a value indicating whether the content reader should skip remote content files with duplicate URLs.
        /// </para>
        /// <para>
        /// If it's set to <c>false</c> and a remote content file with a duplicate URL is found in the EPUB manifest,
        /// then the "Incorrect EPUB manifest: item with href = ... is not unique." exception will be thrown.
        /// This exception can be suppressed by setting this property to <c>true</c>, in which case only the first file within each duplicate item set
        /// will be added to the <see cref="EpubContentCollection{TLocalContentFile, TRemoteContentFile}" />
        /// or the <see cref="EpubContentCollectionRef{TLocalContentFileRef, TRemoteContentFileRef}" /> collections and the others will be skipped.
        /// </para>
        /// <para>
        /// Default value is <c>false</c>.
        /// </para>
        /// </summary>
        public bool SkipItemsWithDuplicateUrls { get; set; }

        internal void RaiseContentFileMissingEvent(ContentFileMissingEventArgs contentFileMissingEventArgs)
        {
            ContentFileMissing?.Invoke(this, contentFileMissingEventArgs);
        }
    }
}
