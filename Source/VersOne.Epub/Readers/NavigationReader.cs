using System;
using System.Collections.Generic;
using System.Linq;
using VersOne.Epub.Schema;

namespace VersOne.Epub.Internal
{
    internal static class NavigationReader
    {
        public static List<EpubNavigationItemRef> GetNavigationItems(EpubBookRef bookRef)
        {
            if (bookRef.Schema.Package.EpubVersion == EpubVersion.EPUB_2)
            {
                return bookRef.Schema.Epub2Ncx != null ? GetNavigationItems(bookRef, bookRef.Schema.Epub2Ncx) : null;
            }
            else
            {
                return bookRef.Schema.Epub3NavDocument != null ? GetNavigationItems(bookRef, bookRef.Schema.Epub3NavDocument) : null;
            }
        }

        public static List<EpubNavigationItemRef> GetNavigationItems(EpubBookRef bookRef, Epub2Ncx epub2Ncx)
        {
            return GetNavigationItems(bookRef, epub2Ncx.NavMap.Items);
        }

        public static List<EpubNavigationItemRef> GetNavigationItems(EpubBookRef bookRef, Epub3NavDocument epub3NavDocument)
        {
            return GetNavigationItems(bookRef, epub3NavDocument.Navs.FirstOrDefault(nav => nav.Type == Epub3NavStructuralSemanticsProperty.TOC));
        }

        private static List<EpubNavigationItemRef> GetNavigationItems(EpubBookRef bookRef, List<Epub2NcxNavigationPoint> navigationPoints)
        {
            List<EpubNavigationItemRef> result = new List<EpubNavigationItemRef>();
            if (navigationPoints != null)
            {
                foreach (Epub2NcxNavigationPoint navigationPoint in navigationPoints)
                {
                    EpubNavigationItemLink navigationItemLink = new EpubNavigationItemLink(navigationPoint.Content.Source, bookRef.Schema.ContentDirectoryPath);
                    EpubNavigationItemRef navigationItemRef = new EpubNavigationItemRef()
                    {
                        Type = EpubNavigationItemType.LINK,
                        Title = navigationPoint.NavigationLabels.First().Text,
                        Link = navigationItemLink,
                        HtmlContentFileRef = GetHtmlContentFileRef(bookRef, navigationItemLink.ContentFileName),
                        NestedItems = GetNavigationItems(bookRef, navigationPoint.ChildNavigationPoints),
                    };
                    result.Add(navigationItemRef);
                }
            }
            return result;
        }

        private static List<EpubNavigationItemRef> GetNavigationItems(EpubBookRef bookRef, Epub3Nav epub3Nav)
        {
            List<EpubNavigationItemRef> result;
            if (epub3Nav != null)
            {
                string epub3NavigationBaseDirectoryPath = ZipPathUtils.GetDirectoryPath(bookRef.Content.NavigationHtmlFile.FilePathInEpubArchive);
                if (epub3Nav.Head != null)
                {
                    result = new List<EpubNavigationItemRef>();
                    EpubNavigationItemRef navigationItemRef = new EpubNavigationItemRef()
                    {
                        Type = EpubNavigationItemType.HEADER,
                        Title = epub3Nav.Head,
                        NestedItems = GetNavigationItems(bookRef, epub3Nav.Ol, epub3NavigationBaseDirectoryPath)
                    };
                    result.Add(navigationItemRef);
                }
                else
                {
                    result = GetNavigationItems(bookRef, epub3Nav.Ol, epub3NavigationBaseDirectoryPath);
                }
            }
            else
            {
                result = new List<EpubNavigationItemRef>();
            }
            return result;
        }

        private static List<EpubNavigationItemRef> GetNavigationItems(EpubBookRef bookRef, Epub3NavOl epub3NavOl, string epub3NavigationBaseDirectoryPath)
        {
            List<EpubNavigationItemRef> result = new List<EpubNavigationItemRef>();
            if (epub3NavOl != null && epub3NavOl.Lis != null)
            {
                foreach (Epub3NavLi epub3NavLi in epub3NavOl.Lis)
                {
                    if (epub3NavLi != null && (epub3NavLi.Anchor != null || epub3NavLi.Span != null))
                    {
                        if (epub3NavLi.Anchor != null)
                        {
                            Epub3NavAnchor navAnchor = epub3NavLi.Anchor;
                            EpubNavigationItemLink navigationItemLink = new EpubNavigationItemLink(navAnchor.Href, epub3NavigationBaseDirectoryPath);
                            EpubNavigationItemRef navigationItemRef = new EpubNavigationItemRef()
                            {
                                Type = EpubNavigationItemType.LINK,
                                Title = GetFirstNonEmptyHeader(navAnchor.Text, navAnchor.Title, navAnchor.Alt),
                                Link = navigationItemLink,
                                HtmlContentFileRef = GetHtmlContentFileRef(bookRef, navigationItemLink.ContentFileName),
                                NestedItems = GetNavigationItems(bookRef, epub3NavLi.ChildOl, epub3NavigationBaseDirectoryPath)
                            };
                            result.Add(navigationItemRef);
                        }
                        else if (epub3NavLi.Span != null)
                        {
                            Epub3NavSpan navSpan = epub3NavLi.Span;
                            EpubNavigationItemRef navigationItemRef = new EpubNavigationItemRef()
                            {
                                Type = EpubNavigationItemType.HEADER,
                                Title = GetFirstNonEmptyHeader(navSpan.Text, navSpan.Title, navSpan.Alt),
                                NestedItems = GetNavigationItems(bookRef, epub3NavLi.ChildOl, epub3NavigationBaseDirectoryPath)
                            };
                            result.Add(navigationItemRef);
                        }
                    }
                }
            }
            return result;
        }

        private static EpubTextContentFileRef GetHtmlContentFileRef(EpubBookRef bookRef, string contentFileName)
        {
            if (contentFileName == null)
            {
                return null;
            }
            if (!bookRef.Content.Html.TryGetValue(contentFileName, out EpubTextContentFileRef htmlContentFileRef))
            {
                return null;
            }
            return htmlContentFileRef;
        }

        private static string GetFirstNonEmptyHeader(params string[] options)
        {
            foreach (string option in options)
            {
                if (!String.IsNullOrEmpty(option))
                {
                    return option;
                }
            }
            return String.Empty;
        }
    }
}
