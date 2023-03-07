using System.Xml.Linq;
using VersOne.Epub.Internal;
using VersOne.Epub.Options;
using VersOne.Epub.Schema;
using VersOne.Epub.Test.Comparers;
using VersOne.Epub.Test.Unit.Mocks;
using VersOne.Epub.Test.Unit.TestData;

namespace VersOne.Epub.Test.Unit.Readers
{
    public class SmilReaderTests
    {
        private const string CONTENT_DIRECTORY_PATH = "Content";
        private const string SMIL_FILE_NAME = "chapter1.smil";
        private const string SMIL_FILE_PATH = $"{CONTENT_DIRECTORY_PATH}/{SMIL_FILE_NAME}";

        private const string MINIMAL_SMIL_FILE = """
            <?xml version='1.0' encoding='utf-8'?>
            <smil xmlns="http://www.w3.org/ns/SMIL" version="3.0">
                <body>
                    <par>
                        <text src="chapter1.html#paragraph1" />
                        <audio src="audio.mp3" clipBegin="0s" clipEnd="10s" />
                    </par>
                </body>
            </smil>
            """;

        private const string FULL_SMIL_FILE = """
            <?xml version='1.0' encoding='utf-8'?>
            <smil xmlns="http://www.w3.org/ns/SMIL" xmlns:epub="http://www.idpf.org/2007/ops" id="smil" version="3.0" epub:prefix="test: http://example.com/test/spec/">
                <head>
                    <metadata>
                        <item1>value1</item1>
                        <item2>value2</item2>
                    </metadata>
                </head>
                <body id="body" epub:type="bodymatter" epub:textref="chapter1.html">
                    <seq id="seq1" epub:type="chapter" epub:textref="chapter1.html#section1">
                        <seq id="seq2" epub:textref="chapter1.html#figure1">
                            <par id="par3" epub:type="figure">
                                <text id="text3" src="chapter1.html#photo" />
                                <audio id="audio3" src="audio.mp3" clipBegin="0:24:18.123" clipEnd="0:24:28.764" />
                            </par>
                            <par id="par4" epub:type="title">
                                <text id="text4" src="chapter1.html#caption" />
                                <audio id="audio4" src="audio.mp3" clipBegin="0:24:28.764" clipEnd="0:24:50.010" />
                            </par>
                        </seq>
                        <par id="par1">
                            <text src="chapter1.html#paragraph1" />
                            <audio src="audio.mp3" clipBegin="0:23:34.221" clipEnd="0:23:59.003" />
                        </par>
                        <par id="par2">
                            <text src="chapter1.html#paragraph2" />
                            <audio src="audio.mp3" clipBegin="0:23:59.003" clipEnd="0:24:15.000" />
                        </par>
                    </seq>
                </body>
            </smil>
            """;

        private const string SMIL_FILE_WITHOUT_SMIL_ELEMENT = """
            <?xml version='1.0' encoding='utf-8'?>
            <test />
            """;

        private const string SMIL_FILE_WITH_WRONG_SMIL_VERSION = """
            <?xml version='1.0' encoding='utf-8'?>
            <smil xmlns="http://www.w3.org/ns/SMIL" version="2.0">
                <body>
                    <par>
                        <text src="chapter1.html#paragraph1" />
                        <audio src="audio.mp3" clipBegin="0s" clipEnd="10s" />
                    </par>
                </body>
            </smil>
            """;

        private const string SMIL_FILE_WITHOUT_BODY_ELEMENT = """
            <?xml version='1.0' encoding='utf-8'?>
            <smil xmlns="http://www.w3.org/ns/SMIL" version="3.0">
                <test />
            </smil>
            """;

        private const string MINIMAL_SMIL_FILE_WITH_NON_METADATA_ELEMENT_IN_HEAD = """
            <?xml version='1.0' encoding='utf-8'?>
            <smil xmlns="http://www.w3.org/ns/SMIL" version="3.0">
                <head>
                    <test1 />
                    <test2 />
                </head>
                <body>
                    <par>
                        <text src="chapter1.html#paragraph1" />
                        <audio src="audio.mp3" clipBegin="0s" clipEnd="10s" />
                    </par>
                </body>
            </smil>
            """;

        private const string SMIL_FILE_WITHOUT_SEQ_AND_PAR_ELEMENTS_IN_BODY = """
            <?xml version='1.0' encoding='utf-8'?>
            <smil xmlns="http://www.w3.org/ns/SMIL" version="3.0">
                <body />
            </smil>
            """;

        private const string SMIL_FILE_WITHOUT_SEQ_AND_PAR_ELEMENTS_IN_SEQ = """
            <?xml version='1.0' encoding='utf-8'?>
            <smil xmlns="http://www.w3.org/ns/SMIL" version="3.0">
                <body>
                    <seq />
                </body>
            </smil>
            """;

        private const string SMIL_FILE_WITHOUT_TEXT_ELEMENT_IN_PAR = """
            <?xml version='1.0' encoding='utf-8'?>
            <smil xmlns="http://www.w3.org/ns/SMIL" version="3.0">
                <body>
                    <par />
                </body>
            </smil>
            """;

        private const string SMIL_FILE_WITHOUT_TEXT_SRC_ATTRIBUTE = """
            <?xml version='1.0' encoding='utf-8'?>
            <smil xmlns="http://www.w3.org/ns/SMIL" version="3.0">
                <body>
                    <par>
                        <text />
                    </par>
                </body>
            </smil>
            """;

        private const string SMIL_FILE_WITHOUT_AUDIO_SRC_ATTRIBUTE = """
            <?xml version='1.0' encoding='utf-8'?>
            <smil xmlns="http://www.w3.org/ns/SMIL" version="3.0">
                <body>
                    <par>
                        <text src="chapter1.html#paragraph1" />
                        <audio />
                    </par>
                </body>
            </smil>
            """;

        private static Smil MinimalSmil =>
            new
            (
                id: null,
                version: SmilVersion.SMIL_3,
                epubPrefix: null,
                head: null,
                body: new SmilBody
                (
                    id: null,
                    epubTypes: null,
                    epubTextRef: null,
                    seqs: new List<SmilSeq>(),
                    pars: new List<SmilPar>()
                    {
                        new SmilPar
                        (
                            id: null,
                            epubTypes: null,
                            text: new SmilText
                            (
                                id: null,
                                src: "chapter1.html#paragraph1"
                            ),
                            audio: new SmilAudio
                            (
                                id: null,
                                src: "audio.mp3",
                                clipBegin: "0s",
                                clipEnd: "10s"
                            )
                        )
                    }
                )
            );

        private static XNamespace SmilXmlNamespace => "http://www.w3.org/ns/SMIL";

        private static Smil FullSmil =>
            new
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
                            new XElement(SmilXmlNamespace + "item1", "value1"),
                            new XElement(SmilXmlNamespace + "item2", "value2")
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
                    epubTextRef: "chapter1.html",
                    seqs: new List<SmilSeq>()
                    {
                        new SmilSeq
                        (
                            id: "seq1",
                            epubTypes: new List<Epub3StructuralSemanticsProperty>()
                            {
                                Epub3StructuralSemanticsProperty.CHAPTER
                            },
                            epubTextRef: "chapter1.html#section1",
                            seqs: new List<SmilSeq>()
                            {
                                new SmilSeq
                                (
                                    id: "seq2",
                                    epubTypes: null,
                                    epubTextRef: "chapter1.html#figure1",
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
                                                src: "chapter1.html#photo"
                                            ),
                                            audio: new SmilAudio
                                            (
                                                id: "audio3",
                                                src: "audio.mp3",
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
                                                src: "chapter1.html#caption"
                                            ),
                                            audio: new SmilAudio
                                            (
                                                id: "audio4",
                                                src: "audio.mp3",
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
                                        src: "chapter1.html#paragraph1"
                                    ),
                                    audio: new SmilAudio
                                    (
                                        id: null,
                                        src: "audio.mp3",
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
                                        src: "chapter1.html#paragraph2"
                                    ),
                                    audio: new SmilAudio
                                    (
                                        id: null,
                                        src: "audio.mp3",
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

        private static Smil MinimalSmilWithEmptyHead =>
            new
            (
                id: null,
                version: SmilVersion.SMIL_3,
                epubPrefix: null,
                head: new SmilHead(null),
                body: new SmilBody
                (
                    id: null,
                    epubTypes: null,
                    epubTextRef: null,
                    seqs: new List<SmilSeq>(),
                    pars: new List<SmilPar>()
                    {
                        new SmilPar
                        (
                            id: null,
                            epubTypes: null,
                            text: new SmilText
                            (
                                id: null,
                                src: "chapter1.html#paragraph1"
                            ),
                            audio: new SmilAudio
                            (
                                id: null,
                                src: "audio.mp3",
                                clipBegin: "0s",
                                clipEnd: "10s"
                            )
                        )
                    }
                )
            );


        [Fact(DisplayName = "Constructing a SmilReader instance with a non-null epubReaderOptions parameter should succeed")]
        public void ConstructorWithNonNullEpubReaderOptionsTest()
        {
            _ = new SmilReader(new EpubReaderOptions());
        }

        [Fact(DisplayName = "Constructing a SmilReader instance with a null epubReaderOptions parameter should succeed")]
        public void ConstructorWithNullEpubReaderOptionsTest()
        {
            _ = new SmilReader(null);
        }

        [Fact(DisplayName = "Reading a minimal SMIL file should succeed")]
        public async void ReadSmilAsyncWithMinimalSmilFileTest()
        {
            await TestSuccessfulReadOperation(MINIMAL_SMIL_FILE, MinimalSmil);
        }

        [Fact(DisplayName = "Reading a full SMIL file should succeed")]
        public async void ReadSmilAsyncWithFullSmilFileTest()
        {
            await TestSuccessfulReadOperation(FULL_SMIL_FILE, FullSmil);
        }

        [Fact(DisplayName = "Reading all SMIL documents in a EPUB package should succeed")]
        public async void ReadAllSmilDocumentsAsyncTest()
        {
            TestZipFile testZipFile = new();
            string chapter1SmilFileName = "chapter1.smil";
            string chapter2SmilFileName = "chapter2.smil";
            testZipFile.AddEntry($"{CONTENT_DIRECTORY_PATH}/{chapter1SmilFileName}", MINIMAL_SMIL_FILE);
            testZipFile.AddEntry($"{CONTENT_DIRECTORY_PATH}/{chapter2SmilFileName}", FULL_SMIL_FILE);
            EpubPackage testEpubPackage = TestEpubPackages.CreateMinimalTestEpubPackage();
            testEpubPackage.Manifest.Items.Add(new EpubManifestItem("smil1", chapter1SmilFileName, "application/smil+xml"));
            testEpubPackage.Manifest.Items.Add(new EpubManifestItem("smil2", chapter2SmilFileName, "application/smil+xml"));
            SmilReader smilReader = new();
            List<Smil> expectedSmils = new() { MinimalSmil, FullSmil };
            List<Smil> actualSmils = await smilReader.ReadAllSmilDocumentsAsync(testZipFile, CONTENT_DIRECTORY_PATH, testEpubPackage);
            SmilComparers.CompareSmilLists(expectedSmils, actualSmils);
        }

        [Fact(DisplayName = "ReadSmilAsync should throw EpubSmilException if EPUB file is missing the specified SMIL file")]
        public async void ReadSmilAsyncWithoutSmilFileTest()
        {
            TestZipFile testZipFile = new();
            SmilReader smilReader = new();
            await Assert.ThrowsAsync<EpubSmilException>(() => smilReader.ReadSmilAsync(testZipFile, SMIL_FILE_PATH));
        }

        [Fact(DisplayName = "ReadSmilAsync should throw EpubSmilException if the SMIL file is larger than 2 GB")]
        public async void ReadSmilAsyncWithLargeSmilFileTest()
        {
            TestZipFile testZipFile = new();
            testZipFile.AddEntry(SMIL_FILE_PATH, new Test4GbZipFileEntry());
            SmilReader smilReader = new();
            await Assert.ThrowsAsync<EpubSmilException>(() => smilReader.ReadSmilAsync(testZipFile, SMIL_FILE_PATH));
        }

        [Fact(DisplayName = "ReadSmilAsync should throw EpubSmilException if the SMIL file has no 'smil' XML element")]
        public async void ReadSmilAsyncWithoutSmilElementTest()
        {
            await TestFailingReadOperation(SMIL_FILE_WITHOUT_SMIL_ELEMENT);
        }

        [Fact(DisplayName = "ReadSmilAsync should throw EpubSmilException if the SMIL version in the file is not 3.0")]
        public async void ReadSmilAsyncWithWrongSmilVersionTest()
        {
            await TestFailingReadOperation(SMIL_FILE_WITH_WRONG_SMIL_VERSION);
        }

        [Fact(DisplayName = "ReadSmilAsync should throw EpubSmilException if the 'smil' XML element has no 'body' element")]
        public async void ReadSmilAsyncWithoutBodyElementTest()
        {
            await TestFailingReadOperation(SMIL_FILE_WITHOUT_BODY_ELEMENT);
        }

        [Fact(DisplayName = "Non-metadata XML elements in the 'head' element should be ignored")]
        public async void ReadSmilAsyncWithNonMetadataElementsInHeadTest()
        {
            await TestSuccessfulReadOperation(MINIMAL_SMIL_FILE_WITH_NON_METADATA_ELEMENT_IN_HEAD, MinimalSmilWithEmptyHead);
        }

        [Fact(DisplayName = "ReadSmilAsync should throw EpubSmilException if the 'body' XML element has neither 'seq' nor 'par' elements")]
        public async void ReadSmilAsyncWithoutSeqAndParElementsInBodyTest()
        {
            await TestFailingReadOperation(SMIL_FILE_WITHOUT_SEQ_AND_PAR_ELEMENTS_IN_BODY);
        }

        [Fact(DisplayName = "ReadSmilAsync should throw EpubSmilException if the 'seq' XML element has neither 'seq' nor 'par' elements")]
        public async void ReadSmilAsyncWithoutSeqAndParElementsInSeqTest()
        {
            await TestFailingReadOperation(SMIL_FILE_WITHOUT_SEQ_AND_PAR_ELEMENTS_IN_SEQ);
        }

        [Fact(DisplayName = "ReadSmilAsync should throw EpubSmilException if the 'par' XML element has no 'text' element")]
        public async void ReadSmilAsyncWithoutTextElementInParTest()
        {
            await TestFailingReadOperation(SMIL_FILE_WITHOUT_TEXT_ELEMENT_IN_PAR);
        }

        [Fact(DisplayName = "ReadSmilAsync should throw EpubSmilException if the 'text' XML element has no 'src' attribute")]
        public async void ReadSmilAsyncWithoutTextSrcAttributeTest()
        {
            await TestFailingReadOperation(SMIL_FILE_WITHOUT_TEXT_SRC_ATTRIBUTE);
        }

        [Fact(DisplayName = "ReadSmilAsync should throw EpubSmilException if the 'audio' XML element has no 'src' attribute")]
        public async void ReadSmilAsyncWithoutAudioSrcAttributeTest()
        {
            await TestFailingReadOperation(SMIL_FILE_WITHOUT_AUDIO_SRC_ATTRIBUTE);
        }

        private static async Task TestSuccessfulReadOperation(string smilFileContent, Smil expectedSmil)
        {
            TestZipFile testZipFile = CreateTestZipFileWithSmilFile(smilFileContent);
            SmilReader smilReader = new();
            Smil actualSmil = await smilReader.ReadSmilAsync(testZipFile, SMIL_FILE_PATH);
            SmilComparers.CompareSmils(expectedSmil, actualSmil);
        }

        private static async Task TestFailingReadOperation(string smilFileContent)
        {
            TestZipFile testZipFile = CreateTestZipFileWithSmilFile(smilFileContent);
            SmilReader smilReader = new();
            await Assert.ThrowsAsync<EpubSmilException>(() => smilReader.ReadSmilAsync(testZipFile, SMIL_FILE_PATH));
        }

        private static TestZipFile CreateTestZipFileWithSmilFile(string smilFileContent)
        {
            TestZipFile testZipFile = new();
            testZipFile.AddEntry(SMIL_FILE_PATH, new TestZipFileEntry(smilFileContent));
            return testZipFile;
        }
    }
}
