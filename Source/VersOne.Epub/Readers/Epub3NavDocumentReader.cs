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

        public Epub3NavDocumentReader(EpubReaderOptions? epubReaderOptions = null)
        {
            this.epubReaderOptions = epubReaderOptions ?? new EpubReaderOptions();
        }

        public async Task<Epub3NavDocument?> ReadEpub3NavDocumentAsync(IZipFile epubFile, string contentDirectoryPath, EpubPackage package)
        {
            EpubManifestItem navManifestItem = package.Manifest.Items.FirstOrDefault(item => item.Properties != null && item.Properties.Contains(EpubManifestProperty.NAV));
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
            IZipFileEntry? navFileEntry = epubFile.GetEntry(navFileEntryPath);
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
            List<Epub3Nav> navs = new();
            foreach (XElement navNode in bodyNode.Elements(xhtmlNamespace + "nav"))
            {
                Epub3Nav epub3Nav = ReadEpub3Nav(navNode);
                navs.Add(epub3Nav);
            }
            return new(navFileEntryPath, navs);
        }

        private static Epub3Nav ReadEpub3Nav(XElement navNode)
        {
            Epub3StructuralSemanticsProperty? type = null;
            bool isHidden = false;
            string? head = null;
            Epub3NavOl? ol = null;
            foreach (XAttribute navNodeAttribute in navNode.Attributes())
            {
                string attributeValue = navNodeAttribute.Value;
                switch (navNodeAttribute.GetLowerCaseLocalName())
                {
                    case "type":
                        type = Epub3StructuralSemanticsPropertyParser.ParseProperty(attributeValue);
                        break;
                    case "hidden":
                        isHidden = true;
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
                        head = navChildNode.Value.Trim();
                        break;
                    case "ol":
                        ol = ReadEpub3NavOl(navChildNode);
                        break;
                }
            }
            if (ol == null)
            {
                throw new Epub3NavException("EPUB parsing error: 'nav' element in the navigation file does not contain a required 'ol' element.");
            }
            return new(type, isHidden, head, ol);
        }

        private static Epub3NavOl ReadEpub3NavOl(XElement epub3NavOlNode)
        {
            bool isHidden = false;
            List<Epub3NavLi> lis = new();
            foreach (XAttribute navOlNodeAttribute in epub3NavOlNode.Attributes())
            {
                switch (navOlNodeAttribute.GetLowerCaseLocalName())
                {
                    case "hidden":
                        isHidden = true;
                        break;
                }
            }
            foreach (XElement navOlChildNode in epub3NavOlNode.Elements())
            {
                switch (navOlChildNode.GetLowerCaseLocalName())
                {
                    case "li":
                        Epub3NavLi epub3NavLi = ReadEpub3NavLi(navOlChildNode);
                        lis.Add(epub3NavLi);
                        break;
                }
            }
            return new(isHidden, lis);
        }

        private static Epub3NavLi ReadEpub3NavLi(XElement epub3NavLiNode)
        {
            Epub3NavAnchor? anchor = null;
            Epub3NavSpan? span = null;
            Epub3NavOl? childOl = null;
            foreach (XElement navLiChildNode in epub3NavLiNode.Elements())
            {
                switch (navLiChildNode.GetLowerCaseLocalName())
                {
                    case "a":
                        Epub3NavAnchor epub3NavAnchor = ReadEpub3NavAnchor(navLiChildNode);
                        anchor = epub3NavAnchor;
                        break;
                    case "span":
                        Epub3NavSpan epub3NavSpan = ReadEpub3NavSpan(navLiChildNode);
                        span = epub3NavSpan;
                        break;
                    case "ol":
                        Epub3NavOl epub3NavOl = ReadEpub3NavOl(navLiChildNode);
                        childOl = epub3NavOl;
                        break;
                }
            }
            if (anchor == null && span == null)
            {
                throw new Epub3NavException("EPUB parsing error: 'li' element in the navigation file must contain either an 'a' element or a 'span' element.");
            }
            return new(anchor, span, childOl);
        }

        private static Epub3NavAnchor ReadEpub3NavAnchor(XElement epub3NavAnchorNode)
        {
            string? href = null;
            string text;
            string? title = null;
            string? alt = null;
            Epub3StructuralSemanticsProperty? type = null;
            foreach (XAttribute navAnchorNodeAttribute in epub3NavAnchorNode.Attributes())
            {
                string attributeValue = navAnchorNodeAttribute.Value;
                switch (navAnchorNodeAttribute.GetLowerCaseLocalName())
                {
                    case "href":
                        href = attributeValue;
                        break;
                    case "title":
                        title = attributeValue;
                        break;
                    case "alt":
                        alt = attributeValue;
                        break;
                    case "type":
                        type = Epub3StructuralSemanticsPropertyParser.ParseProperty(attributeValue);
                        break;
                }
            }
            text = epub3NavAnchorNode.Value.Trim();
            return new(href, text, title, alt, type);
        }

        private static Epub3NavSpan ReadEpub3NavSpan(XElement epub3NavSpanNode)
        {
            string text;
            string? title = null;
            string? alt = null;
            foreach (XAttribute navSpanNodeAttribute in epub3NavSpanNode.Attributes())
            {
                string attributeValue = navSpanNodeAttribute.Value;
                switch (navSpanNodeAttribute.GetLowerCaseLocalName())
                {
                    case "title":
                        title = attributeValue;
                        break;
                    case "alt":
                        alt = attributeValue;
                        break;
                }
            }
            text = epub3NavSpanNode.Value.Trim();
            return new(text, title, alt);
        }
    }
}
