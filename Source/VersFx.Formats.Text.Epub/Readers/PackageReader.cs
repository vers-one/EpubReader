using System;
using System.IO;
using System.IO.Compression;
using System.Xml;
using VersFx.Formats.Text.Epub.Schema.Opf;

namespace VersFx.Formats.Text.Epub.Readers
{
    internal static class PackageReader
    {
        public static EpubPackage ReadPackage(ZipArchive epubArchive, string rootFilePath)
        {
            ZipArchiveEntry rootFileEntry = epubArchive.GetEntry(rootFilePath);
            if (rootFileEntry == null)
                throw new Exception("EPUB parsing error: root file not found in archive.");
            using (Stream containerStream = rootFileEntry.Open())
            {
                XmlDocument containerDocument = new XmlDocument();
                containerDocument.Load(containerStream);
                XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(containerDocument.NameTable);
                xmlNamespaceManager.AddNamespace("opf", "http://www.idpf.org/2007/opf");
                XmlNode packageNode = containerDocument.DocumentElement.SelectSingleNode("/opf:package", xmlNamespaceManager);
                EpubPackage result = new EpubPackage();
                string epubVersionValue = packageNode.Attributes["version"].Value;
                if (epubVersionValue == "2.0")
                    result.EpubVersion = EpubVersion.EPUB_2;
                else
                    if (epubVersionValue == "3.0")
                        result.EpubVersion = EpubVersion.EPUB_3;
                    else
                        throw new Exception(String.Format("Unsupported EPUB version: {0}.", epubVersionValue));
                XmlNode metadataNode = packageNode.SelectSingleNode("opf:metadata", xmlNamespaceManager);
                if (metadataNode == null)
                    throw new Exception("EPUB parsing error: metadata not found in the package.");
                EpubMetadata metadata = ReadMetadata(metadataNode);
                result.Metadata = metadata;
                XmlNode manifestNode = packageNode.SelectSingleNode("opf:manifest", xmlNamespaceManager);
                if (manifestNode == null)
                    throw new Exception("EPUB parsing error: manifest not found in the package.");
                EpubManifest manifest = ReadManifest(manifestNode);
                result.Manifest = manifest;
                XmlNode spineNode = packageNode.SelectSingleNode("opf:spine", xmlNamespaceManager);
                if (spineNode == null)
                    throw new Exception("EPUB parsing error: spine not found in the package.");
                EpubSpine spine = ReadSpine(spineNode);
                result.Spine = spine;
                XmlNode guideNode = packageNode.SelectSingleNode("opf:guide", xmlNamespaceManager);
                if (guideNode != null)
                {
                    EpubGuide guide = ReadGuide(guideNode);
                    result.Guide = guide;
                }
                return result;
            }
        }

        private static EpubMetadata ReadMetadata(XmlNode metadataNode)
        {
            EpubMetadata result = new EpubMetadata();
            foreach (XmlNode metadataItemNode in metadataNode.ChildNodes)
            {
                string innerText = metadataItemNode.InnerText;
                switch (metadataItemNode.LocalName.ToLowerInvariant())
                {
                    case "identifier":
                        result.Identifier = innerText;
                        break;
                    case "title":
                        result.Title = innerText;
                        break;
                    case "language":
                        result.Language = innerText;
                        break;
                    case "contributor":
                        result.Contributor = innerText;
                        break;
                    case "coverage":
                        result.Coverage = innerText;
                        break;
                    case "creator":
                        result.Creator = innerText;
                        break;
                    case "date":
                        result.Date = innerText;
                        break;
                    case "description":
                        result.Description = innerText;
                        break;
                    case "format":
                        result.Format = innerText;
                        break;
                    case "publisher":
                        result.Publisher = innerText;
                        break;
                    case "relation":
                        result.Relation = innerText;
                        break;
                    case "rights":
                        result.Rights = innerText;
                        break;
                    case "source":
                        result.Source = innerText;
                        break;
                    case "subject":
                        result.Subject = innerText;
                        break;
                    case "type":
                        result.Type = innerText;
                        break;
                }
            }
            return result;
        }

        private static EpubManifest ReadManifest(XmlNode manifestNode)
        {
            EpubManifest result = new EpubManifest();
            foreach (XmlNode manifestItemNode in manifestNode.ChildNodes)
                if (String.Compare(manifestItemNode.LocalName, "item", StringComparison.OrdinalIgnoreCase) == 0)
                {
                    EpubManifestItem manifestItem = new EpubManifestItem();
                    foreach (XmlAttribute manifestItemNodeAttribute in manifestItemNode.Attributes)
                    {
                        string attributeValue = manifestItemNodeAttribute.Value;
                        switch (manifestItemNodeAttribute.Name.ToLowerInvariant())
                        {
                            case "id":
                                manifestItem.Id = attributeValue;
                                break;
                            case "href":
                                manifestItem.Href = attributeValue;
                                break;
                            case "media-type":
                                manifestItem.MediaType = attributeValue;
                                break;
                            case "required-namespace":
                                manifestItem.RequiredNamespace = attributeValue;
                                break;
                            case "required-modules":
                                manifestItem.RequiredModules = attributeValue;
                                break;
                            case "fallback":
                                manifestItem.Fallback = attributeValue;
                                break;
                            case "fallback-style":
                                manifestItem.FallbackStyle = attributeValue;
                                break;
                        }
                    }
                    if (String.IsNullOrWhiteSpace(manifestItem.Id))
                        throw new Exception("Incorrect EPUB manifest: item ID is missing");
                    if (String.IsNullOrWhiteSpace(manifestItem.Href))
                        throw new Exception("Incorrect EPUB manifest: item href is missing");
                    if (String.IsNullOrWhiteSpace(manifestItem.MediaType))
                        throw new Exception("Incorrect EPUB manifest: item media type is missing");
                    result.Add(manifestItem);
                }
            return result;
        }

        private static EpubSpine ReadSpine(XmlNode spineNode)
        {
            EpubSpine result = new EpubSpine();
            XmlAttribute tocAttribute = spineNode.Attributes["toc"];
            if (tocAttribute == null || String.IsNullOrWhiteSpace(tocAttribute.Value))
                throw new Exception("Incorrect EPUB spine: TOC is missing");
            result.Toc = tocAttribute.Value;
            foreach (XmlNode spineItemNode in spineNode.ChildNodes)
                if (String.Compare(spineItemNode.LocalName, "itemref", StringComparison.OrdinalIgnoreCase) == 0)
                {
                    EpubSpineItemRef spineItemRef = new EpubSpineItemRef();
                    XmlAttribute idRefAttribute = spineItemNode.Attributes["idref"];
                    if (idRefAttribute == null || String.IsNullOrWhiteSpace(idRefAttribute.Value))
                        throw new Exception("Incorrect EPUB spine: item ID ref is missing");
                    spineItemRef.IdRef = idRefAttribute.Value;
                    XmlAttribute linearAttribute = spineItemNode.Attributes["linear"];
                    spineItemRef.IsLinear = linearAttribute == null || String.Compare(linearAttribute.Value, "no", StringComparison.OrdinalIgnoreCase) != 0;
                    result.Add(spineItemRef);
                }
            return result;
        }

        private static EpubGuide ReadGuide(XmlNode guideNode)
        {
            EpubGuide result = new EpubGuide();
            foreach (XmlNode guideReferenceNode in guideNode.ChildNodes)
                if (String.Compare(guideReferenceNode.LocalName, "reference", StringComparison.OrdinalIgnoreCase) == 0)
                {
                    EpubGuideReference guideReference = new EpubGuideReference();
                    foreach (XmlAttribute guideReferenceNodeAttribute in guideReferenceNode.Attributes)
                    {
                        string attributeValue = guideReferenceNodeAttribute.Value;
                        switch (guideReferenceNodeAttribute.Name.ToLowerInvariant())
                        {
                            case "type":
                                guideReference.Type = attributeValue;
                                break;
                            case "title":
                                guideReference.Title = attributeValue;
                                break;
                            case "href":
                                guideReference.Href = attributeValue;
                                break;
                        }
                    }
                    if (String.IsNullOrWhiteSpace(guideReference.Type))
                        throw new Exception("Incorrect EPUB guide: item type is missing");
                    if (String.IsNullOrWhiteSpace(guideReference.Href))
                        throw new Exception("Incorrect EPUB guide: item href is missing");
                    result.Add(guideReference);
                }
            return result;
        }
    }
}
