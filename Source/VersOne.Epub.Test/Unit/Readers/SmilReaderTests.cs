using System.Xml;
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

        private const string SMIL_FILE_WITHOUT_SMIL_VERSION = """
            <?xml version='1.0' encoding='utf-8'?>
            <smil xmlns="http://www.w3.org/ns/SMIL">
                <body>
                    <par>
                        <text src="chapter1.html#paragraph1" />
                        <audio src="audio.mp3" clipBegin="0s" clipEnd="10s" />
                    </par>
                </body>
            </smil>
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
                    seqs: [],
                    pars:
                    [
                        new
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
                    ]
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
                        items:
                        [
                            new(SmilXmlNamespace + "item1", "value1"),
                            new(SmilXmlNamespace + "item2", "value2")
                        ]
                    )
                ),
                body: new SmilBody
                (
                    id: "body",
                    epubTypes:
                    [
                        Epub3StructuralSemanticsProperty.BODYMATTER
                    ],
                    epubTextRef: "chapter1.html",
                    seqs:
                    [
                        new
                        (
                            id: "seq1",
                            epubTypes:
                            [
                                Epub3StructuralSemanticsProperty.CHAPTER
                            ],
                            epubTextRef: "chapter1.html#section1",
                            seqs:
                            [
                                new
                                (
                                    id: "seq2",
                                    epubTypes: null,
                                    epubTextRef: "chapter1.html#figure1",
                                    seqs: [],
                                    pars:
                                    [
                                        new
                                        (
                                            id: "par3",
                                            epubTypes:
                                            [
                                                Epub3StructuralSemanticsProperty.FIGURE
                                            ],
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
                                        new
                                        (
                                            id: "par4",
                                            epubTypes:
                                            [
                                                Epub3StructuralSemanticsProperty.TITLE
                                            ],
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
                                    ]
                                )
                            ],
                            pars:
                            [
                                new
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
                                new
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
                            ]
                        )
                    ],
                    pars: []
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
                    seqs: [],
                    pars:
                    [
                        new
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
                    ]
                )
            );

        private static Smil MinimalSmilWithEmptyBody =>
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
                    seqs: [],
                    pars: []
                )
            );

        private static Smil MinimalSmilWithEmptySeq =>
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
                    seqs:
                    [
                        new
                        (
                            id: null,
                            epubTypes: null,
                            epubTextRef: null,
                            seqs: [],
                            pars: []
                        )
                    ],
                    pars: []
                )
            );

        private static Smil MinimalSmilWithoutAudio =>
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
                    seqs: [],
                    pars:
                    [
                        new
                        (
                            id: null,
                            epubTypes: null,
                            text: new SmilText
                            (
                                id: null,
                                src: "chapter1.html#paragraph1"
                            ),
                            audio: null
                        )
                    ]
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

        [Fact(DisplayName = "Constructing a SmilReader instance with a null SmilReaderOptions property inside the epubReaderOptions parameter should succeed")]
        public void ConstructorWithNullSmilReaderOptionsTest()
        {
            _ = new SmilReader(new EpubReaderOptions
            {
                SmilReaderOptions = null!
            });
        }

        [Fact(DisplayName = "Reading a minimal SMIL file should succeed")]
        public async Task ReadSmilAsyncWithMinimalSmilFileTest()
        {
            await TestSuccessfulReadOperation(MINIMAL_SMIL_FILE, MinimalSmil);
        }

        [Fact(DisplayName = "Reading a full SMIL file should succeed")]
        public async Task ReadSmilAsyncWithFullSmilFileTest()
        {
            await TestSuccessfulReadOperation(FULL_SMIL_FILE, FullSmil);
        }

        [Fact(DisplayName = "Reading all SMIL documents in a EPUB package should succeed")]
        public async Task ReadAllSmilDocumentsAsyncTest()
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
            List<Smil> expectedSmils = [MinimalSmil, FullSmil];
            List<Smil> actualSmils = await smilReader.ReadAllSmilDocumentsAsync(testZipFile, CONTENT_DIRECTORY_PATH, testEpubPackage);
            SmilComparers.CompareSmilLists(expectedSmils, actualSmils);
        }

        [Fact(DisplayName = "ReadSmilAsync should throw EpubSmilException if EPUB file is missing the specified SMIL file and no SmilReaderOptions are provided")]
        public async Task ReadSmilAsyncWithoutSmilFileAndDefaultOptionsTest()
        {
            TestZipFile testZipFile = new();
            SmilReader smilReader = new();
            await Assert.ThrowsAsync<EpubSmilException>(() => smilReader.ReadSmilAsync(testZipFile, SMIL_FILE_PATH));
        }

        [Fact(DisplayName = "ReadSmilAsync should return null if EPUB file is missing the specified SMIL file and IgnoreMissingSmilFileError = true")]
        public async Task ReadSmilAsyncWithoutSmilFileAndIgnoreMissingSmilFileErrorTest()
        {
            TestZipFile testZipFile = new();
            EpubReaderOptions epubReaderOptions = new()
            {
                SmilReaderOptions = new()
                {
                    IgnoreMissingSmilFileError = true
                }
            };
            SmilReader smilReader = new(epubReaderOptions);
            Smil? actualSmil = await smilReader.ReadSmilAsync(testZipFile, SMIL_FILE_PATH);
            Assert.Null(actualSmil);
        }

        [Fact(DisplayName = "ReadSmilAsync should throw EpubSmilException if the SMIL file is larger than 2 GB and no SmilReaderOptions are provided")]
        public async Task ReadSmilAsyncWithLargeSmilFileAndDefaultOptionsTest()
        {
            TestZipFile testZipFile = new();
            testZipFile.AddEntry(SMIL_FILE_PATH, new Test4GbZipFileEntry());
            SmilReader smilReader = new();
            await Assert.ThrowsAsync<EpubSmilException>(() => smilReader.ReadSmilAsync(testZipFile, SMIL_FILE_PATH));
        }

        [Fact(DisplayName = "ReadSmilAsync should return null if the SMIL file is larger than 2 GB and IgnoreSmilFileIsTooLargeError = true")]
        public async Task ReadSmilAsyncWithLargeSmilFileAndIgnoreSmilFileIsTooLargeErrorTest()
        {
            TestZipFile testZipFile = new();
            testZipFile.AddEntry(SMIL_FILE_PATH, new Test4GbZipFileEntry());
            EpubReaderOptions epubReaderOptions = new()
            {
                SmilReaderOptions = new()
                {
                    IgnoreSmilFileIsTooLargeError = true
                }
            };
            SmilReader smilReader = new(epubReaderOptions);
            Smil? actualSmil = await smilReader.ReadSmilAsync(testZipFile, SMIL_FILE_PATH);
            Assert.Null(actualSmil);
        }

        [Fact(DisplayName = "ReadSmilAsync should throw EpubSmilException with an inner XmlException if the SMIL file is not a valid XML file and no SmilReaderOptions are provided")]
        public async Task ReadEpub3NavDocumentAsyncWithInvalidXhtmlFileAndDefaultOptionsTest()
        {
            TestZipFile testZipFile = CreateTestZipFileWithSmilFile("not a valid XML file");
            SmilReader smilReader = new();
            EpubSmilException outerException = await Assert.ThrowsAsync<EpubSmilException>(() => smilReader.ReadSmilAsync(testZipFile, SMIL_FILE_PATH));
            Assert.NotNull(outerException.InnerException);
            Assert.Equal(typeof(XmlException), outerException.InnerException.GetType());
        }

        [Fact(DisplayName = "ReadSmilAsync should throw EpubSmilException with an inner XmlException if the SMIL file is not a valid XML file and IgnoreSmilFileIsNotValidXmlError = true")]
        public async Task ReadEpub3NavDocumentAsyncWithInvalidXhtmlFileAndIgnoreSmilFileIsNotValidXmlErrorTest()
        {
            EpubReaderOptions epubReaderOptions = new()
            {
                SmilReaderOptions = new()
                {
                    IgnoreSmilFileIsNotValidXmlError = true
                }
            };
            await TestSuccessfulReadOperation("not a valid XML file", null, epubReaderOptions);
        }

        [Fact(DisplayName = "ReadSmilAsync should throw EpubSmilException if the SMIL file has no 'smil' XML element and no SmilReaderOptions are provided")]
        public async Task ReadSmilAsyncWithoutSmilElementAndDefaultOptionsTest()
        {
            await TestFailingReadOperation(SMIL_FILE_WITHOUT_SMIL_ELEMENT);
        }

        [Fact(DisplayName = "ReadSmilAsync should return null if the SMIL file has no 'smil' XML element and IgnoreMissingSmilElementError = true")]
        public async Task ReadSmilAsyncWithoutSmilElementAndIgnoreMissingSmilElementErrorTest()
        {
            EpubReaderOptions epubReaderOptions = new()
            {
                SmilReaderOptions = new()
                {
                    IgnoreMissingSmilElementError = true
                }
            };
            await TestSuccessfulReadOperation(SMIL_FILE_WITHOUT_SMIL_ELEMENT, null, epubReaderOptions);
        }

        [Fact(DisplayName = "ReadSmilAsync should throw EpubSmilException if the SMIL version is missing in the file and no SmilReaderOptions are provided")]
        public async Task ReadSmilAsyncWithoutSmilVersionAndDefaultOptionsTest()
        {
            await TestFailingReadOperation(SMIL_FILE_WITHOUT_SMIL_VERSION);
        }

        [Fact(DisplayName = "ReadSmilAsync should succeed if the SMIL version is missing in the file and IgnoreMissingSmilVersionError = true")]
        public async Task ReadSmilAsyncWithoutSmilVersionAndIgnoreMissingSmilVersionErrorTest()
        {
            EpubReaderOptions epubReaderOptions = new()
            {
                SmilReaderOptions = new()
                {
                    IgnoreMissingSmilVersionError = true
                }
            };
            await TestSuccessfulReadOperation(SMIL_FILE_WITHOUT_SMIL_VERSION, MinimalSmil, epubReaderOptions);
        }

        [Fact(DisplayName = "ReadSmilAsync should throw EpubSmilException if the SMIL version in the file is not 3.0 and no SmilReaderOptions are provided")]
        public async Task ReadSmilAsyncWithWrongSmilVersionAndDefaultOptionsTest()
        {
            await TestFailingReadOperation(SMIL_FILE_WITH_WRONG_SMIL_VERSION);
        }

        [Fact(DisplayName = "ReadSmilAsync should throw EpubSmilException if the SMIL version in the file is not 3.0 and IgnoreUnsupportedSmilVersionError = true")]
        public async Task ReadSmilAsyncWithWrongSmilVersionAndIgnoreUnsupportedSmilVersionErrorTest()
        {
            EpubReaderOptions epubReaderOptions = new()
            {
                SmilReaderOptions = new()
                {
                    IgnoreUnsupportedSmilVersionError = true
                }
            };
            await TestSuccessfulReadOperation(SMIL_FILE_WITH_WRONG_SMIL_VERSION, MinimalSmil, epubReaderOptions);
        }

        [Fact(DisplayName = "ReadSmilAsync should throw EpubSmilException if the 'smil' XML element has no 'body' element and no SmilReaderOptions are provided")]
        public async Task ReadSmilAsyncWithoutBodyElementAndDefaultOptionsTest()
        {
            await TestFailingReadOperation(SMIL_FILE_WITHOUT_BODY_ELEMENT);
        }

        [Fact(DisplayName = "ReadSmilAsync should throw EpubSmilException if the 'smil' XML element has no 'body' element and IgnoreMissingBodyElementError = true")]
        public async Task ReadSmilAsyncWithoutBodyElementAndIgnoreMissingBodyElementErrorTest()
        {
            EpubReaderOptions epubReaderOptions = new()
            {
                SmilReaderOptions = new()
                {
                    IgnoreMissingBodyElementError = true
                }
            };
            await TestSuccessfulReadOperation(SMIL_FILE_WITHOUT_BODY_ELEMENT, null, epubReaderOptions);
        }

        [Fact(DisplayName = "Non-metadata XML elements in the 'head' element should be ignored")]
        public async Task ReadSmilAsyncWithNonMetadataElementsInHeadAndDefaultOptionsTest()
        {
            await TestSuccessfulReadOperation(MINIMAL_SMIL_FILE_WITH_NON_METADATA_ELEMENT_IN_HEAD, MinimalSmilWithEmptyHead);
        }

        [Fact(DisplayName = "ReadSmilAsync should throw EpubSmilException if the 'body' XML element has neither 'seq' nor 'par' elements and no SmilReaderOptions are provided")]
        public async Task ReadSmilAsyncWithoutSeqAndParElementsInBodyAndDefaultOptionsTest()
        {
            await TestFailingReadOperation(SMIL_FILE_WITHOUT_SEQ_AND_PAR_ELEMENTS_IN_BODY);
        }

        [Fact(DisplayName = "ReadSmilAsync should succeed if the 'body' XML element has neither 'seq' nor 'par' elements and IgnoreBodyMissingSeqOrParElementsError = true")]
        public async Task ReadSmilAsyncWithoutSeqAndParElementsInBodyAndIgnoreBodyMissingSeqOrParElementsErrorTest()
        {
            EpubReaderOptions epubReaderOptions = new()
            {
                SmilReaderOptions = new()
                {
                    IgnoreBodyMissingSeqOrParElementsError = true
                }
            };
            await TestSuccessfulReadOperation(SMIL_FILE_WITHOUT_SEQ_AND_PAR_ELEMENTS_IN_BODY, MinimalSmilWithEmptyBody, epubReaderOptions);
        }

        [Fact(DisplayName = "ReadSmilAsync should throw EpubSmilException if the 'seq' XML element has neither 'seq' nor 'par' elements and no SmilReaderOptions are provided")]
        public async Task ReadSmilAsyncWithoutSeqAndParElementsInSeqAndDefaultOptionsTest()
        {
            await TestFailingReadOperation(SMIL_FILE_WITHOUT_SEQ_AND_PAR_ELEMENTS_IN_SEQ);
        }

        [Fact(DisplayName = "ReadSmilAsync should succeed if the 'seq' XML element has neither 'seq' nor 'par' elements and IgnoreSeqMissingSeqOrParElementsError = true")]
        public async Task ReadSmilAsyncWithoutSeqAndParElementsInSeqAndIgnoreSeqMissingSeqOrParElementsErrorTest()
        {
            EpubReaderOptions epubReaderOptions = new()
            {
                SmilReaderOptions = new()
                {
                    IgnoreSeqMissingSeqOrParElementsError = true
                }
            };
            await TestSuccessfulReadOperation(SMIL_FILE_WITHOUT_SEQ_AND_PAR_ELEMENTS_IN_SEQ, MinimalSmilWithEmptySeq, epubReaderOptions);
        }

        [Fact(DisplayName = "ReadSmilAsync should throw EpubSmilException if the 'par' XML element has no 'text' element and no SmilReaderOptions are provided")]
        public async Task ReadSmilAsyncWithoutTextElementInParAndDefaultOptionsTest()
        {
            await TestFailingReadOperation(SMIL_FILE_WITHOUT_TEXT_ELEMENT_IN_PAR);
        }

        [Fact(DisplayName = "ReadSmilAsync should skip 'par' XML elements without 'text' elements when SkipParsWithoutTextElements = true")]
        public async Task ReadSmilAsyncWithoutTextElementInParAndSkipParsWithoutTextElementsTest()
        {
            EpubReaderOptions epubReaderOptions = new()
            {
                SmilReaderOptions = new()
                {
                    SkipParsWithoutTextElements = true,
                    IgnoreBodyMissingSeqOrParElementsError = true
                }
            };
            await TestSuccessfulReadOperation(SMIL_FILE_WITHOUT_TEXT_ELEMENT_IN_PAR, MinimalSmilWithEmptyBody, epubReaderOptions);
        }

        [Fact(DisplayName = "ReadSmilAsync should throw EpubSmilException if the 'text' XML element has no 'src' attribute and no SmilReaderOptions are provided")]
        public async Task ReadSmilAsyncWithoutTextSrcAttributeAndDefaultOptionsTest()
        {
            await TestFailingReadOperation(SMIL_FILE_WITHOUT_TEXT_SRC_ATTRIBUTE);
        }

        [Fact(DisplayName = "ReadSmilAsync should 'text' XML elements without 'src' attributes when SkipTextsWithoutSrcAttributes = true")]
        public async Task ReadSmilAsyncWithoutTextSrcAttributeAndSkipTextsWithoutSrcAttributesTest()
        {
            EpubReaderOptions epubReaderOptions = new()
            {
                SmilReaderOptions = new()
                {
                    SkipTextsWithoutSrcAttributes = true,
                    SkipParsWithoutTextElements = true,
                    IgnoreBodyMissingSeqOrParElementsError = true
                }
            };
            await TestSuccessfulReadOperation(SMIL_FILE_WITHOUT_TEXT_SRC_ATTRIBUTE, MinimalSmilWithEmptyBody, epubReaderOptions);
        }

        [Fact(DisplayName = "ReadSmilAsync should throw EpubSmilException if the 'audio' XML element has no 'src' attribute and no SmilReaderOptions are provided")]
        public async Task ReadSmilAsyncWithoutAudioSrcAttributeAndDefaultOptionsTest()
        {
            await TestFailingReadOperation(SMIL_FILE_WITHOUT_AUDIO_SRC_ATTRIBUTE);
        }

        [Fact(DisplayName = "ReadSmilAsync should skip 'audio' XML elements without 'src' attributes when SkipAudiosWithoutSrcAttributes = true")]
        public async Task ReadSmilAsyncWithoutAudioSrcAttributeAndSkipAudiosWithoutSrcAttributesTest()
        {
            EpubReaderOptions epubReaderOptions = new()
            {
                SmilReaderOptions = new()
                {
                    SkipAudiosWithoutSrcAttributes = true
                }
            };
            await TestSuccessfulReadOperation(SMIL_FILE_WITHOUT_AUDIO_SRC_ATTRIBUTE, MinimalSmilWithoutAudio, epubReaderOptions);
        }

        private static async Task TestSuccessfulReadOperation(string smilFileContent, Smil? expectedSmil, EpubReaderOptions? epubReaderOptions = null)
        {
            TestZipFile testZipFile = CreateTestZipFileWithSmilFile(smilFileContent);
            SmilReader smilReader = new(epubReaderOptions ?? new EpubReaderOptions());
            Smil? actualSmil = await smilReader.ReadSmilAsync(testZipFile, SMIL_FILE_PATH);
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
