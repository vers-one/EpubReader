using System;
using System.IO;
using System.IO.Compression;
using System.Xml;

namespace VersFx.Formats.Text.Epub.Readers
{
    internal static class RootFilePathReader
    {
        public static string GetRootFilePath(ZipArchive epubArchive)
        {
            const string EPUB_CONTAINER_FILE_PATH = "META-INF/container.xml";
            ZipArchiveEntry containerFileEntry = epubArchive.GetEntry(EPUB_CONTAINER_FILE_PATH);
            if (containerFileEntry == null)
                throw new Exception(String.Format("EPUB parsing error: {0} file not found in archive.", EPUB_CONTAINER_FILE_PATH));
            using (Stream containerStream = containerFileEntry.Open())
            {
                XmlDocument containerDocument = new XmlDocument();
                containerDocument.Load(containerStream);
                XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(containerDocument.NameTable);
                xmlNamespaceManager.AddNamespace("cns", "urn:oasis:names:tc:opendocument:xmlns:container");
                XmlNode rootFileNode = containerDocument.DocumentElement.SelectSingleNode("/cns:container/cns:rootfiles/cns:rootfile", xmlNamespaceManager);
                return rootFileNode.Attributes["full-path"].Value;
            }
        }
    }
}
