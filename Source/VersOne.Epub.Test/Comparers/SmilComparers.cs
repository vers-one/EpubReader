using System.Xml.Linq;
using VersOne.Epub.Schema;

namespace VersOne.Epub.Test.Comparers
{
    internal static class SmilComparers
    {
        public static void CompareSmilLists(List<Smil> expected, List<Smil> actual)
        {
            Assert.NotNull(actual);
            CollectionComparer.CompareCollections(expected, actual, CompareSmils);
        }

        public static void CompareSmils(Smil expected, Smil actual)
        {
            Assert.NotNull(actual);
            Assert.Equal(expected.Id, actual.Id);
            Assert.Equal(expected.Version, actual.Version);
            Assert.Equal(expected.EpubPrefix, actual.EpubPrefix);
            CompareSmilHeads(expected.Head, actual.Head);
            CompareSmilBodies(expected.Body, actual.Body);
        }

        private static void CompareSmilHeads(SmilHead? expected, SmilHead? actual)
        {
            if (expected == null)
            {
                Assert.Null(actual);
            }
            else
            {
                Assert.NotNull(actual);
                CompareSmilMetadatas(expected.Metadata, actual.Metadata);
            }
        }

        private static void CompareSmilMetadatas(SmilMetadata? expected, SmilMetadata? actual)
        {
            if (expected == null)
            {
                Assert.Null(actual);
            }
            else
            {
                Assert.NotNull(actual);
                CollectionComparer.CompareCollections(expected.Items, actual.Items, CompareXElements);
            }
        }

        private static void CompareXElements(XElement expected, XElement actual)
        {
            Assert.Equal(expected.ToString(), actual.ToString());
        }

        private static void CompareSmilBodies(SmilBody expected, SmilBody actual)
        {
            Assert.NotNull(actual);
            Assert.Equal(expected.Id, actual.Id);
            Assert.Equal(expected.EpubTypes, actual.EpubTypes);
            Assert.Equal(expected.EpubTextRef, actual.EpubTextRef);
            CollectionComparer.CompareCollections(expected.Seqs, actual.Seqs, CompareSmilSeqs);
            CollectionComparer.CompareCollections(expected.Pars, actual.Pars, CompareSmilPars);
        }

        private static void CompareSmilSeqs(SmilSeq expected, SmilSeq actual)
        {
            Assert.NotNull(actual);
            Assert.Equal(expected.Id, actual.Id);
            Assert.Equal(expected.EpubTypes, actual.EpubTypes);
            Assert.Equal(expected.EpubTextRef, actual.EpubTextRef);
            CollectionComparer.CompareCollections(expected.Seqs, actual.Seqs, CompareSmilSeqs);
            CollectionComparer.CompareCollections(expected.Pars, actual.Pars, CompareSmilPars);
        }

        private static void CompareSmilPars(SmilPar expected, SmilPar actual)
        {
            Assert.NotNull(actual);
            Assert.Equal(expected.Id, actual.Id);
            Assert.Equal(expected.EpubTypes, actual.EpubTypes);
            CompareSmilTexts(expected.Text, actual.Text);
            CompareSmilAudios(expected.Audio, actual.Audio);
        }

        private static void CompareSmilTexts(SmilText expected, SmilText actual)
        {
            Assert.NotNull(actual);
            Assert.Equal(expected.Id, actual.Id);
            Assert.Equal(expected.Src, actual.Src);
        }

        private static void CompareSmilAudios(SmilAudio? expected, SmilAudio? actual)
        {
            if (expected == null)
            {
                Assert.Null(actual);
            }
            else
            {
                Assert.NotNull(actual);
                Assert.Equal(expected.Id, actual.Id);
                Assert.Equal(expected.Src, actual.Src);
                Assert.Equal(expected.ClipBegin, actual.ClipBegin);
                Assert.Equal(expected.ClipEnd, actual.ClipEnd);
            }
        }
    }
}
