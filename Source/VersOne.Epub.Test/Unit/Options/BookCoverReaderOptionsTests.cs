using VersOne.Epub.Options;

namespace VersOne.Epub.Test.Unit.Options
{
    public class BookCoverReaderOptionsTests
    {
        [Fact(DisplayName = "Constructing a BookCoverReaderOptions instance with a non-null preset parameter should succeed")]
        public void ConstructorWithNonNullPresetTest()
        {
            _ = new BookCoverReaderOptions(EpubReaderOptionsPreset.RELAXED);
        }

        [Fact(DisplayName = "Constructing a BookCoverReaderOptions instance with a null preset parameter should succeed")]
        public void ConstructorWithNullPresetTest()
        {
            _ = new BookCoverReaderOptions(null);
        }

        [Fact(DisplayName = "Constructing a BookCoverReaderOptions instance with a null preset parameter should initialize properties with the expected values.")]
        public void InitializationWithNullPresetTest()
        {
            BookCoverReaderOptions bookCoverReaderOptions = new(null);
            Assert.False(bookCoverReaderOptions.Epub2MetadataIgnoreMissingManifestItem);
        }

        [Fact(DisplayName = "Constructing a BookCoverReaderOptions instance with the STRICT preset parameter should initialize properties with the expected values.")]
        public void InitializationWithStrictPresetTest()
        {
            BookCoverReaderOptions bookCoverReaderOptions = new(EpubReaderOptionsPreset.STRICT);
            Assert.False(bookCoverReaderOptions.Epub2MetadataIgnoreMissingManifestItem);
        }

        [Fact(DisplayName = "Constructing a BookCoverReaderOptions instance with the RELAXED preset parameter should initialize properties with the expected values.")]
        public void InitializationWithRelaxedPresetTest()
        {
            BookCoverReaderOptions bookCoverReaderOptions = new(EpubReaderOptionsPreset.RELAXED);
            Assert.True(bookCoverReaderOptions.Epub2MetadataIgnoreMissingManifestItem);
        }

        [Fact(DisplayName = "Constructing a BookCoverReaderOptions instance with the IGNORE_ALL_ERRORS preset parameter should initialize properties with the expected values.")]
        public void InitializationWithIgnoreAllErrorsPresetTest()
        {
            BookCoverReaderOptions bookCoverReaderOptions = new(EpubReaderOptionsPreset.IGNORE_ALL_ERRORS);
            Assert.True(bookCoverReaderOptions.Epub2MetadataIgnoreMissingManifestItem);
        }
    }
}
