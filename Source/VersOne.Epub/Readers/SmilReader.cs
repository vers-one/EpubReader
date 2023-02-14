using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using VersOne.Epub.Environment;
using VersOne.Epub.Options;
using VersOne.Epub.Schema;
using VersOne.Epub.Utils;

namespace VersOne.Epub.Internal
{
    internal class SmilReader
    {
        private readonly EpubReaderOptions epubReaderOptions;

        public SmilReader(EpubReaderOptions? epubReaderOptions = null)
        {
            this.epubReaderOptions = epubReaderOptions ?? new EpubReaderOptions();
        }

        public async Task<List<Smil>> ReadAllSmilDocumentsAsync(IZipFile epubFile, string contentDirectoryPath, EpubPackage package)
        {
            List<Smil> result = new();
            foreach (EpubManifestItem smilManifestItem in package.Manifest.Items.Where(manifestItem => manifestItem.MediaType.CompareOrdinalIgnoreCase("application/smil+xml")))
            {
                string smilFilePath = ZipPathUtils.Combine(contentDirectoryPath, smilManifestItem.Href);
                Smil smil = await ReadSmilAsync(epubFile, smilFilePath);
                result.Add(smil);
            }
            return result;
        }

        public async Task<Smil> ReadSmilAsync(IZipFile epubFile, string smilFilePath)
        {
            IZipFileEntry? smilFile = epubFile.GetEntry(smilFilePath);
            if (smilFile == null)
            {
                throw new EpubSmilException($"EPUB parsing error: SMIL file {smilFilePath} not found in the EPUB file.", smilFilePath);
            }
            if (smilFile.Length > Int32.MaxValue)
            {
                throw new EpubSmilException($"EPUB parsing error: SMIL file {smilFilePath} is larger than 2 GB.", smilFilePath);
            }
            XDocument smilDocument;
            using (Stream containerStream = smilFile.Open())
            {
                smilDocument = await XmlUtils.LoadDocumentAsync(containerStream, epubReaderOptions.XmlReaderOptions).ConfigureAwait(false);
            }
            XNamespace smilNamespace = "http://www.w3.org/ns/SMIL";
            XElement smilNode = smilDocument.Element(smilNamespace + "smil");
            if (smilNode == null)
            {
                throw new EpubSmilException("SMIL parsing error: smil XML element is missing in the file.", smilFilePath);
            }
            Smil smil = ReadSmil(smilNode, smilFilePath);
            return smil;
        }

        private static Smil ReadSmil(XElement smilNode, string smilFilePath)
        {
            string? id = null;
            string? smilVersionString = null;
            string? epubPrefix = null;
            SmilHead? head = null;
            SmilBody? body = null;
            foreach (XAttribute smilNodeAttribute in smilNode.Attributes())
            {
                string attributeValue = smilNodeAttribute.Value;
                switch (smilNodeAttribute.GetLowerCaseLocalName())
                {
                    case "id":
                        id = attributeValue;
                        break;
                    case "version":
                        smilVersionString = attributeValue;
                        break;
                    case "prefix":
                        epubPrefix = attributeValue;
                        break;
                }
            }
            foreach (XElement smilChildNode in smilNode.Elements())
            {
                switch (smilChildNode.GetLowerCaseLocalName())
                {
                    case "head":
                        head = ReadHead(smilChildNode);
                        break;
                    case "body":
                        body = ReadBody(smilChildNode, smilFilePath);
                        break;
                }
            }
            SmilVersion version = smilVersionString switch
            {
                "3.0" => SmilVersion.SMIL_3,
                _ => throw new EpubSmilException($"SMIL parsing error: unsupported SMIL version: \"{smilVersionString}\".", smilFilePath)
            };
            if (body == null)
            {
                throw new EpubSmilException("SMIL parsing error: body XML element is missing in the file.", smilFilePath);
            }
            return new(id, version, epubPrefix, head, body);
        }

        private static SmilHead ReadHead(XElement headNode)
        {
            SmilMetadata? metadata = null;
            foreach (XElement headChildNode in headNode.Elements())
            {
                if (headChildNode.GetLowerCaseLocalName() == "metadata")
                {
                    metadata = ReadMetadata(headChildNode);
                    break;
                }
            }
            return new(metadata);
        }

        private static SmilMetadata ReadMetadata(XElement metadataNode)
        {
            List<XElement> items = metadataNode.Elements().ToList();
            return new(items);
        }

        private static SmilBody ReadBody(XElement bodyNode, string smilFilePath)
        {
            string? id = null;
            List<Epub3StructuralSemanticsProperty>? epubTypes = null;
            string? epubTextRef = null;
            List<SmilSeq> seqs = new();
            List<SmilPar> pars = new();
            foreach (XAttribute bodyNodeAttribute in bodyNode.Attributes())
            {
                string attributeValue = bodyNodeAttribute.Value;
                switch (bodyNodeAttribute.GetLowerCaseLocalName())
                {
                    case "id":
                        id = attributeValue;
                        break;
                    case "type":
                        epubTypes = Epub3StructuralSemanticsPropertyParser.ParsePropertyList(attributeValue);
                        break;
                    case "textref":
                        epubTextRef = attributeValue;
                        break;
                }
            }
            foreach (XElement bodyChildNode in bodyNode.Elements())
            {
                switch (bodyChildNode.GetLowerCaseLocalName())
                {
                    case "seq":
                        SmilSeq seq = ReadSeq(bodyChildNode, smilFilePath);
                        seqs.Add(seq);
                        break;
                    case "par":
                        SmilPar par = ReadPar(bodyChildNode, smilFilePath);
                        pars.Add(par);
                        break;
                }
            }
            if (!seqs.Any() && !pars.Any())
            {
                throw new EpubSmilException("SMIL parsing error: body XML element must contain at least one seq or par XML element.", smilFilePath);
            }
            return new(id, epubTypes, epubTextRef, seqs, pars);
        }

        private static SmilSeq ReadSeq(XElement seqNode, string smilFilePath)
        {
            string? id = null;
            List<Epub3StructuralSemanticsProperty>? epubTypes = null;
            string? epubTextRef = null;
            List<SmilSeq> seqs = new();
            List<SmilPar> pars = new();
            foreach (XAttribute seqNodeAttribute in seqNode.Attributes())
            {
                string attributeValue = seqNodeAttribute.Value;
                switch (seqNodeAttribute.GetLowerCaseLocalName())
                {
                    case "id":
                        id = attributeValue;
                        break;
                    case "type":
                        epubTypes = Epub3StructuralSemanticsPropertyParser.ParsePropertyList(attributeValue);
                        break;
                    case "textref":
                        epubTextRef = attributeValue;
                        break;
                }
            }
            foreach (XElement bodyChildNode in seqNode.Elements())
            {
                switch (bodyChildNode.GetLowerCaseLocalName())
                {
                    case "seq":
                        SmilSeq seq = ReadSeq(bodyChildNode, smilFilePath);
                        seqs.Add(seq);
                        break;
                    case "par":
                        SmilPar par = ReadPar(bodyChildNode, smilFilePath);
                        pars.Add(par);
                        break;
                }
            }
            if (!seqs.Any() && !pars.Any())
            {
                throw new EpubSmilException("SMIL parsing error: seq XML element must contain at least one nested seq or par XML element.", smilFilePath);
            }
            return new(id, epubTypes, epubTextRef, seqs, pars);
        }

        private static SmilPar ReadPar(XElement parNode, string smilFilePath)
        {
            string? id = null;
            List<Epub3StructuralSemanticsProperty>? epubTypes = null;
            SmilText? text = null;
            SmilAudio? audio = null;
            foreach (XAttribute parNodeAttribute in parNode.Attributes())
            {
                string attributeValue = parNodeAttribute.Value;
                switch (parNodeAttribute.GetLowerCaseLocalName())
                {
                    case "id":
                        id = attributeValue;
                        break;
                    case "type":
                        epubTypes = Epub3StructuralSemanticsPropertyParser.ParsePropertyList(attributeValue);
                        break;
                }
            }
            foreach (XElement parChildNode in parNode.Elements())
            {
                switch (parChildNode.GetLowerCaseLocalName())
                {
                    case "text":
                        text = ReadText(parChildNode, smilFilePath);
                        break;
                    case "audio":
                        audio = ReadAudio(parChildNode, smilFilePath);
                        break;
                }
            }
            if (text == null)
            {
                throw new EpubSmilException("SMIL parsing error: par XML element must contain one text XML element.", smilFilePath);
            }
            return new(id, epubTypes, text, audio);
        }

        private static SmilText ReadText(XElement textNode, string smilFilePath)
        {
            string? id = null;
            string? src = null;
            foreach (XAttribute textNodeAttribute in textNode.Attributes())
            {
                string attributeValue = textNodeAttribute.Value;
                switch (textNodeAttribute.GetLowerCaseLocalName())
                {
                    case "id":
                        id = attributeValue;
                        break;
                    case "src":
                        src = attributeValue;
                        break;
                }
            }
            if (src == null)
            {
                throw new EpubSmilException("SMIL parsing error: text XML element must have an src attribute.", smilFilePath);
            }
            return new(id, src);
        }

        private static SmilAudio ReadAudio(XElement audioNode, string smilFilePath)
        {
            string? id = null;
            string? src = null;
            string? clipBegin = null;
            string? clipEnd = null;
            foreach (XAttribute audioNodeAttribute in audioNode.Attributes())
            {
                string attributeValue = audioNodeAttribute.Value;
                switch (audioNodeAttribute.GetLowerCaseLocalName())
                {
                    case "id":
                        id = attributeValue;
                        break;
                    case "src":
                        src = attributeValue;
                        break;
                    case "clipBegin":
                        clipBegin = attributeValue;
                        break;
                    case "clipEnd":
                        clipEnd = attributeValue;
                        break;
                }
            }
            if (src == null)
            {
                throw new EpubSmilException("SMIL parsing error: audio XML element must have an src attribute.", smilFilePath);
            }
            return new(id, src, clipBegin, clipEnd);
        }
    }
}
