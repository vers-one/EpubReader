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
    internal class Epub3NavDocumentReader
    {
        private readonly EpubReaderOptions epubReaderOptions;

        public Epub3NavDocumentReader(EpubReaderOptions epubReaderOptions = null)
        {
            this.epubReaderOptions = epubReaderOptions ?? new EpubReaderOptions();
        }

        public async Task<Epub3NavDocument> ReadEpub3NavDocumentAsync(IZipFile epubFile, string contentDirectoryPath, EpubPackage package)
        {
            Epub3NavDocument result = new Epub3NavDocument();
            EpubManifestItem navManifestItem =
                package.Manifest.Items.FirstOrDefault(item => item.Properties != null && item.Properties.Contains(EpubManifestProperty.NAV));
            if (navManifestItem == null)
            {
                if (package.EpubVersion == EpubVersion.EPUB_2)
                {
                    return null;
                }
                else
                {
                    throw new Epub3NavException("EPUB parsing error: NAV item not found in EPUB manifest.");
                }
            }
            string navFileEntryPath = ZipPathUtils.Combine(contentDirectoryPath, navManifestItem.Href);
            IZipFileEntry navFileEntry = epubFile.GetEntry(navFileEntryPath);
            if (navFileEntry == null)
            {
                throw new Epub3NavException($"EPUB parsing error: navigation file {navFileEntryPath} not found in the EPUB file.");
            }
            if (navFileEntry.Length > Int32.MaxValue)
            {
                throw new Epub3NavException($"EPUB parsing error: navigation file {navFileEntryPath} is larger than 2 GB.");
            }
            XDocument navDocument;
            using (Stream containerStream = navFileEntry.Open())
            {
                navDocument = await XmlUtils.LoadDocumentAsync(containerStream, epubReaderOptions.XmlReaderOptions).ConfigureAwait(false);
            }
            XNamespace xhtmlNamespace = navDocument.Root.Name.Namespace;
            XElement htmlNode = navDocument.Element(xhtmlNamespace + "html");
            if (htmlNode == null)
            {
                throw new Epub3NavException("EPUB parsing error: navigation file does not contain html element.");
            }
            XElement bodyNode = htmlNode.Element(xhtmlNamespace + "body");
            if (bodyNode == null)
            {
                throw new Epub3NavException("EPUB parsing error: navigation file does not contain body element.");
            }
            result.Navs = new List<Epub3Nav>();
            foreach (XElement navNode in bodyNode.Elements(xhtmlNamespace + "nav"))
            {
                Epub3Nav epub3Nav = ReadEpub3Nav(navNode);
                result.Navs.Add(epub3Nav);
            }
            return result;
        }

        private static Epub3Nav ReadEpub3Nav(XElement navNode)
        {
            Epub3Nav epub3Nav = new Epub3Nav();
            foreach (XAttribute navNodeAttribute in navNode.Attributes())
            {
                string attributeValue = navNodeAttribute.Value;
                switch (navNodeAttribute.GetLowerCaseLocalName())
                {
                    case "type":
                        epub3Nav.Type = Epub3NavStructuralSemanticsPropertyParser.Parse(attributeValue);
                        break;
                    case "hidden":
                        epub3Nav.IsHidden = true;
                        break;
                }
            }
            foreach (XElement navChildNode in navNode.Elements())
            {
                switch (navChildNode.GetLowerCaseLocalName())
                {
                    case "h1":
                    case "h2":
                    case "h3":
                    case "h4":
                    case "h5":
                    case "h6":
                        epub3Nav.Head = navChildNode.Value.Trim();
                        break;
                    case "ol":
                        Epub3NavOl epub3NavOl = ReadEpub3NavOl(navChildNode);
                        epub3Nav.Ol = epub3NavOl;
                        break;
                }
            }
            return epub3Nav;
        }

        private static Epub3NavOl ReadEpub3NavOl(XElement epub3NavOlNode)
        {
            Epub3NavOl epub3NavOl = new Epub3NavOl();
            foreach (XAttribute navOlNodeAttribute in epub3NavOlNode.Attributes())
            {
                switch (navOlNodeAttribute.GetLowerCaseLocalName())
                {
                    case "hidden":
                        epub3NavOl.IsHidden = true;
                        break;
                }
            }
            epub3NavOl.Lis = new List<Epub3NavLi>();
            foreach (XElement navOlChildNode in epub3NavOlNode.Elements())
            {
                switch (navOlChildNode.GetLowerCaseLocalName())
                {
                    case "li":
                        Epub3NavLi epub3NavLi = ReadEpub3NavLi(navOlChildNode);
                        epub3NavOl.Lis.Add(epub3NavLi);
                        break;
                }
            }
            return epub3NavOl;
        }

        private static Epub3NavLi ReadEpub3NavLi(XElement epub3NavLiNode)
        {
            Epub3NavLi epub3NavLi = new Epub3NavLi();
            foreach (XElement navLiChildNode in epub3NavLiNode.Elements())
            {
                switch (navLiChildNode.GetLowerCaseLocalName())
                {
                    case "a":
                        Epub3NavAnchor epub3NavAnchor = ReadEpub3NavAnchor(navLiChildNode);
                        epub3NavLi.Anchor = epub3NavAnchor;
                        break;
                    case "span":
                        Epub3NavSpan epub3NavSpan = ReadEpub3NavSpan(navLiChildNode);
                        epub3NavLi.Span = epub3NavSpan;
                        break;
                    case "ol":
                        Epub3NavOl epub3NavOl = ReadEpub3NavOl(navLiChildNode);
                        epub3NavLi.ChildOl = epub3NavOl;
                        break;
                }
            }
            return epub3NavLi;
        }

        private static Epub3NavAnchor ReadEpub3NavAnchor(XElement epub3NavAnchorNode)
        {
            Epub3NavAnchor epub3NavAnchor = new Epub3NavAnchor();
            foreach (XAttribute navAnchorNodeAttribute in epub3NavAnchorNode.Attributes())
            {
                string attributeValue = navAnchorNodeAttribute.Value;
                switch (navAnchorNodeAttribute.GetLowerCaseLocalName())
                {
                    case "href":
                        epub3NavAnchor.Href = attributeValue;
                        break;
                    case "title":
                        epub3NavAnchor.Title = attributeValue;
                        break;
                    case "alt":
                        epub3NavAnchor.Alt = attributeValue;
                        break;
                    case "type":
                        epub3NavAnchor.Type = Epub3NavStructuralSemanticsPropertyParser.Parse(attributeValue);
                        break;
                }
            }
            epub3NavAnchor.Text = epub3NavAnchorNode.Value.Trim();
            return epub3NavAnchor;
        }

        private static Epub3NavSpan ReadEpub3NavSpan(XElement epub3NavSpanNode)
        {
            Epub3NavSpan epub3NavSpan = new Epub3NavSpan();
            foreach (XAttribute navSpanNodeAttribute in epub3NavSpanNode.Attributes())
            {
                string attributeValue = navSpanNodeAttribute.Value;
                switch (navSpanNodeAttribute.GetLowerCaseLocalName())
                {
                    case "title":
                        epub3NavSpan.Title = attributeValue;
                        break;
                    case "alt":
                        epub3NavSpan.Alt = attributeValue;
                        break;
                }
            }
            epub3NavSpan.Text = epub3NavSpanNode.Value.Trim();
            return epub3NavSpan;
        }
    }
}
