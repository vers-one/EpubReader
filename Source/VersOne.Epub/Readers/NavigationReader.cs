using System;
using System.Collections.Generic;
using System.Linq;
using VersOne.Epub.Options;
using VersOne.Epub.Schema;

namespace VersOne.Epub.Internal
{
    internal static class NavigationReader
    {
        public static List<EpubNavigationItemRef>? GetNavigationItems(EpubSchema epubSchema, EpubContentRef epubContentRef,
            NavigationReaderOptions navigationReaderOptions)
        {
            if (epubSchema.Package.EpubVersion == EpubVersion.EPUB_2)
            {
                return epubSchema.Epub2Ncx != null ? GetNavigationItems(epubSchema, epubContentRef, epubSchema.Epub2Ncx, navigationReaderOptions) : null;
            }
            else
            {
                return epubSchema.Epub3NavDocument != null ?
                    GetNavigationItems(epubSchema, epubContentRef, epubSchema.Epub3NavDocument, navigationReaderOptions) : null;
            }
        }

        public static List<EpubNavigationItemRef> GetNavigationItems(EpubSchema epubSchema, EpubContentRef epubContentRef, Epub2Ncx epub2Ncx,
            NavigationReaderOptions navigationReaderOptions)
        {
            return GetNavigationItems(epubSchema, epubContentRef, epub2Ncx.NavMap.Items, ContentPathUtils.GetDirectoryPath(epub2Ncx.FilePath),
                navigationReaderOptions);
        }

        public static List<EpubNavigationItemRef> GetNavigationItems(EpubSchema epubSchema, EpubContentRef epubContentRef, Epub3NavDocument epub3NavDocument,
            NavigationReaderOptions navigationReaderOptions)
        {
            return GetNavigationItems(epubSchema, epubContentRef, epub3NavDocument.Navs.Find(nav => nav.Type == Epub3StructuralSemanticsProperty.TOC),
                ContentPathUtils.GetDirectoryPath(epub3NavDocument.FilePath), navigationReaderOptions);
        }

        private static List<EpubNavigationItemRef> GetNavigationItems(EpubSchema epubSchema, EpubContentRef epubContentRef,
            List<Epub2NcxNavigationPoint> navigationPoints, string epub2NcxBaseDirectoryPath, NavigationReaderOptions navigationReaderOptions)
        {
            List<EpubNavigationItemRef> result = new();
            if (navigationPoints != null)
            {
                foreach (Epub2NcxNavigationPoint navigationPoint in navigationPoints)
                {
                    EpubNavigationItemType type = EpubNavigationItemType.LINK;
                    Epub2NcxNavigationLabel? firstNavigationLabel = navigationPoint.NavigationLabels.FirstOrDefault();
                    string title;
                    if (firstNavigationLabel != null)
                    {
                        title = firstNavigationLabel.Text;
                    }
                    else
                    {
                        if (navigationReaderOptions.AllowEpub2NavigationItemsWithEmptyTitles)
                        {
                            title = String.Empty;
                        }
                        else
                        {
                            throw new Epub2NcxException($"Incorrect EPUB 2 NCX: navigation point \"{navigationPoint.Id}\" should contain at least one navigation label.");
                        }
                    }
                    string source = navigationPoint.Content.Source;
                    if (!ContentPathUtils.IsLocalPath(source))
                    {
                        if (navigationReaderOptions.SkipRemoteNavigationItems)
                        {
                            continue;
                        }
                        throw new Epub2NcxException($"Incorrect EPUB 2 NCX: content source \"{source}\" cannot be a remote resource.");
                    }
                    EpubNavigationItemLink link = new(source, epub2NcxBaseDirectoryPath);
                    EpubLocalTextContentFileRef? htmlContentFileRef = GetLocalHtmlContentFileRef(epubContentRef, link.ContentFilePath);
                    if (htmlContentFileRef == null)
                    {
                        if (navigationReaderOptions.SkipNavigationItemsReferencingMissingContent)
                        {
                            continue;
                        }
                        throw new Epub2NcxException($"Incorrect EPUB 2 NCX: content source \"{source}\" not found in EPUB manifest.");
                    }
                    List<EpubNavigationItemRef> nestedItems = GetNavigationItems(epubSchema, epubContentRef, navigationPoint.ChildNavigationPoints,
                        epub2NcxBaseDirectoryPath, navigationReaderOptions);
                    result.Add(new EpubNavigationItemRef(type, title, link, htmlContentFileRef, nestedItems));
                }
            }
            return result;
        }

        private static List<EpubNavigationItemRef> GetNavigationItems(EpubSchema epubSchema, EpubContentRef epubContentRef, Epub3Nav? epub3Nav,
            string epub3NavigationBaseDirectoryPath, NavigationReaderOptions navigationReaderOptions)
        {
            List<EpubNavigationItemRef> result;
            if (epub3Nav != null)
            {
                if (epub3Nav.Head != null)
                {
                    result = new List<EpubNavigationItemRef>();
                    EpubNavigationItemType type = EpubNavigationItemType.HEADER;
                    string title = epub3Nav.Head;
                    List<EpubNavigationItemRef> nestedItems =
                        GetNavigationItems(epubSchema, epubContentRef, epub3Nav.Ol, epub3NavigationBaseDirectoryPath, navigationReaderOptions);
                    result.Add(new EpubNavigationItemRef(type, title, null, null, nestedItems));
                }
                else
                {
                    result = GetNavigationItems(epubSchema, epubContentRef, epub3Nav.Ol, epub3NavigationBaseDirectoryPath, navigationReaderOptions);
                }
            }
            else
            {
                result = new List<EpubNavigationItemRef>();
            }
            return result;
        }

        private static List<EpubNavigationItemRef> GetNavigationItems(EpubSchema epubSchema, EpubContentRef epubContentRef,
            Epub3NavOl? epub3NavOl, string epub3NavigationBaseDirectoryPath, NavigationReaderOptions navigationReaderOptions)
        {
            List<EpubNavigationItemRef> result = new();
            if (epub3NavOl != null)
            {
                foreach (Epub3NavLi epub3NavLi in epub3NavOl.Lis)
                {
                    if (epub3NavLi.Anchor != null)
                    {
                        Epub3NavAnchor navAnchor = epub3NavLi.Anchor;
                        EpubNavigationItemType type;
                        string title = GetFirstNonEmptyHeader(navAnchor.Text, navAnchor.Title, navAnchor.Alt);
                        EpubNavigationItemLink? link = null;
                        EpubLocalTextContentFileRef? htmlContentFileRef = null;
                        List<EpubNavigationItemRef> nestedItems =
                            GetNavigationItems(epubSchema, epubContentRef, epub3NavLi.ChildOl, epub3NavigationBaseDirectoryPath, navigationReaderOptions);
                        if (navAnchor.Href != null)
                        {
                            string href = navAnchor.Href;
                            if (!ContentPathUtils.IsLocalPath(href))
                            {
                                if (navigationReaderOptions.SkipRemoteNavigationItems)
                                {
                                    continue;
                                }
                                throw new Epub3NavException($"Incorrect EPUB 3 navigation document: anchor href \"{href}\" cannot be a remote resource.");
                            }
                            type = EpubNavigationItemType.LINK;
                            link = new(href, epub3NavigationBaseDirectoryPath);
                            htmlContentFileRef = GetLocalHtmlContentFileRef(epubContentRef, link.ContentFilePath);
                            if (htmlContentFileRef == null)
                            {
                                if (navigationReaderOptions.SkipNavigationItemsReferencingMissingContent)
                                {
                                    continue;
                                }
                                throw new Epub3NavException($"Incorrect EPUB 3 navigation document: target for anchor href \"{href}\" not found in EPUB manifest.");
                            }
                        }
                        else
                        {
                            type = EpubNavigationItemType.HEADER;
                        }
                        result.Add(new EpubNavigationItemRef(type, title, link, htmlContentFileRef, nestedItems));
                    }
                    else if (epub3NavLi.Span != null)
                    {
                        Epub3NavSpan navSpan = epub3NavLi.Span;
                        EpubNavigationItemType type = EpubNavigationItemType.HEADER;
                        string title = GetFirstNonEmptyHeader(navSpan.Text, navSpan.Title, navSpan.Alt);
                        List<EpubNavigationItemRef> nestedItems =
                            GetNavigationItems(epubSchema, epubContentRef, epub3NavLi.ChildOl, epub3NavigationBaseDirectoryPath, navigationReaderOptions);
                        result.Add(new EpubNavigationItemRef(type, title, null, null, nestedItems));
                    }
                }
            }
            return result;
        }

        private static EpubLocalTextContentFileRef? GetLocalHtmlContentFileRef(EpubContentRef epubContentRef, string localContentFilePath)
        {
            if (!epubContentRef.Html.TryGetLocalFileRefByFilePath(localContentFilePath, out EpubLocalTextContentFileRef? htmlContentFileRef) ||
                htmlContentFileRef == null)
            {
                return null;
            }
            return htmlContentFileRef;
        }

        private static string GetFirstNonEmptyHeader(params string?[] options)
        {
            foreach (string? option in options)
            {
                if (option != null && option != String.Empty)
                {
                    return option;
                }
            }
            return String.Empty;
        }
    }
}
