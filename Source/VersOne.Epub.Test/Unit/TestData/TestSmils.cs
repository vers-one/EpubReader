using System.Xml.Linq;
using VersOne.Epub.Schema;
using static VersOne.Epub.Test.Unit.TestData.TestEpubData;

namespace VersOne.Epub.Test.Unit.TestData
{
    internal static class TestSmils
    {
        public static Smil CreateFullTestSmil()
        {
            return new
            (
                id: "smil",
                version: SmilVersion.SMIL_3,
                epubPrefix: "test: http://example.com/test/spec/",
                head: new SmilHead
                (
                    metadata: new SmilMetadata
                    (
                        items: new List<XElement>()
                        {
                            new XElement("item1", "value1"),
                            new XElement("item2", "value2")
                        }
                    )
                ),
                body: new SmilBody
                (
                    id: "body",
                    epubTypes: new List<Epub3StructuralSemanticsProperty>()
                    {
                        Epub3StructuralSemanticsProperty.BODYMATTER
                    },
                    epubTextRef: CHAPTER1_FILE_NAME,
                    seqs: new List<SmilSeq>()
                    {
                        new SmilSeq
                        (
                            id: "seq1",
                            epubTypes: new List<Epub3StructuralSemanticsProperty>()
                            {
                                Epub3StructuralSemanticsProperty.CHAPTER
                            },
                            epubTextRef: CHAPTER1_FILE_NAME + "#section1",
                            seqs: new List<SmilSeq>()
                            {
                                new SmilSeq
                                (
                                    id: "seq2",
                                    epubTypes: null,
                                    epubTextRef: CHAPTER1_FILE_NAME + "#figure1",
                                    seqs: new List<SmilSeq>(),
                                    pars: new List<SmilPar>()
                                    {
                                        new SmilPar
                                        (
                                            id: "par3",
                                            epubTypes: new List<Epub3StructuralSemanticsProperty>()
                                            {
                                                Epub3StructuralSemanticsProperty.FIGURE
                                            },
                                            text: new SmilText
                                            (
                                                id: "text3",
                                                src: CHAPTER1_FILE_NAME + "#photo"
                                            ),
                                            audio: new SmilAudio
                                            (
                                                id: "audio3",
                                                src: AUDIO_FILE_NAME,
                                                clipBegin: "0:24:18.123",
                                                clipEnd: "0:24:28.764"
                                            )
                                        ),
                                        new SmilPar
                                        (
                                            id: "par4",
                                            epubTypes: new List<Epub3StructuralSemanticsProperty>()
                                            {
                                                Epub3StructuralSemanticsProperty.TITLE
                                            },
                                            text: new SmilText
                                            (
                                                id: "text4",
                                                src: CHAPTER1_FILE_NAME + "#caption"
                                            ),
                                            audio: new SmilAudio
                                            (
                                                id: "audio4",
                                                src: AUDIO_FILE_NAME,
                                                clipBegin: "0:24:28.764",
                                                clipEnd: "0:24:50.010"
                                            )
                                        )
                                    }
                                )
                            },
                            pars: new List<SmilPar>()
                            {
                                new SmilPar
                                (
                                    id: "par1",
                                    epubTypes: null,
                                    text: new SmilText
                                    (
                                        id: null,
                                        src: CHAPTER1_FILE_NAME + "#paragraph1"
                                    ),
                                    audio: new SmilAudio
                                    (
                                        id: null,
                                        src: AUDIO_FILE_NAME,
                                        clipBegin: "0:23:34.221",
                                        clipEnd: "0:23:59.003"
                                    )
                                ),
                                new SmilPar
                                (
                                    id: "par2",
                                    epubTypes: null,
                                    text: new SmilText
                                    (
                                        id: null,
                                        src: CHAPTER1_FILE_NAME + "#paragraph2"
                                    ),
                                    audio: new SmilAudio
                                    (
                                        id: null,
                                        src: AUDIO_FILE_NAME,
                                        clipBegin: "0:23:59.003",
                                        clipEnd: "0:24:15.000"
                                    )
                                )
                            }
                        )
                    },
                    pars: new List<SmilPar>()
                    {
                    }
                )
            );
        }
    }
}
