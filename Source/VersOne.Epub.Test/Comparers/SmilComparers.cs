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

        public static void CompareSmilHeads(SmilHead? expected, SmilHead? actual)
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

        public static void CompareSmilMetadatas(SmilMetadata? expected, SmilMetadata? actual)
        {
            if (expected == null)
            {
                Assert.Null(actual);
            }
            else
            {
                Assert.NotNull(actual);
                CompareSmilMetadataItems(expected.Items, actual.Items);
            }
        }

        public static void CompareSmilMetadataItems(List<XElement> expected, List<XElement> actual)
        {
            CollectionComparer.CompareCollections(expected, actual, CompareXElements);
        }

        public static void CompareSmilBodies(SmilBody expected, SmilBody actual)
        {
            Assert.NotNull(actual);
            Assert.Equal(expected.Id, actual.Id);
            Assert.Equal(expected.EpubTypes, actual.EpubTypes);
            Assert.Equal(expected.EpubTextRef, actual.EpubTextRef);
            CompareSmilSeqLists(expected.Seqs, actual.Seqs);
            CompareSmilParLists(expected.Pars, actual.Pars);
        }

        public static void CompareSmilSeqLists(List<SmilSeq> expected, List<SmilSeq> actual)
        {
            CollectionComparer.CompareCollections(expected, actual, CompareSmilSeqs);
        }

        public static void CompareSmilSeqs(SmilSeq expected, SmilSeq actual)
        {
            Assert.NotNull(actual);
            Assert.Equal(expected.Id, actual.Id);
            Assert.Equal(expected.EpubTypes, actual.EpubTypes);
            Assert.Equal(expected.EpubTextRef, actual.EpubTextRef);
            CompareSmilSeqLists(expected.Seqs, actual.Seqs);
            CompareSmilParLists(expected.Pars, actual.Pars);
        }

        public static void CompareSmilParLists(List<SmilPar> expected, List<SmilPar> actual)
        {
            CollectionComparer.CompareCollections(expected, actual, CompareSmilPars);
        }

        public static void CompareSmilPars(SmilPar expected, SmilPar actual)
        {
            Assert.NotNull(actual);
            Assert.Equal(expected.Id, actual.Id);
            Assert.Equal(expected.EpubTypes, actual.EpubTypes);
            CompareSmilTexts(expected.Text, actual.Text);
            CompareSmilAudios(expected.Audio, actual.Audio);
        }

        public static void CompareSmilTexts(SmilText expected, SmilText actual)
        {
            Assert.NotNull(actual);
            Assert.Equal(expected.Id, actual.Id);
            Assert.Equal(expected.Src, actual.Src);
        }

        public static void CompareSmilAudios(SmilAudio? expected, SmilAudio? actual)
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

        private static void CompareXElements(XElement expected, XElement actual)
        {
            Assert.Equal(expected.ToString(), actual.ToString());
        }
    }
}
