using System.Collections.Generic;
using System.Xml.Linq;
using VersOne.Epub.Schema;
using VersOne.Epub.Utils;

namespace VersOne.Epub.Internal
{
    internal static class MetadataReader
    {
        public static EpubMetadata ReadMetadata(XElement metadataNode)
        {
            List<EpubMetadataTitle> titles = new();
            List<EpubMetadataCreator> creators = new();
            List<EpubMetadataSubject> subjects = new();
            List<EpubMetadataDescription> descriptions = new();
            List<EpubMetadataPublisher> publishers = new();
            List<EpubMetadataContributor> contributors = new();
            List<EpubMetadataDate> dates = new();
            List<EpubMetadataType> types = new();
            List<EpubMetadataFormat> formats = new();
            List<EpubMetadataIdentifier> identifiers = new();
            List<EpubMetadataSource> sources = new();
            List<EpubMetadataLanguage> languages = new();
            List<EpubMetadataRelation> relations = new();
            List<EpubMetadataCoverage> coverages = new();
            List<EpubMetadataRights> rights = new();
            List<EpubMetadataLink> links = new();
            List<EpubMetadataMeta> metaItems = new();
            foreach (XElement metadataItemNode in metadataNode.Elements())
            {
                switch (metadataItemNode.GetLowerCaseLocalName())
                {
                    case "title":
                        EpubMetadataTitle title = ReadTitle(metadataItemNode);
                        titles.Add(title);
                        break;
                    case "creator":
                        EpubMetadataCreator creator = ReadCreator(metadataItemNode);
                        creators.Add(creator);
                        break;
                    case "subject":
                        EpubMetadataSubject subject = ReadSubject(metadataItemNode);
                        subjects.Add(subject);
                        break;
                    case "description":
                        EpubMetadataDescription description = ReadDescription(metadataItemNode);
                        descriptions.Add(description);
                        break;
                    case "publisher":
                        EpubMetadataPublisher publisher = ReadPublisher(metadataItemNode);
                        publishers.Add(publisher);
                        break;
                    case "contributor":
                        EpubMetadataContributor contributor = ReadContributor(metadataItemNode);
                        contributors.Add(contributor);
                        break;
                    case "date":
                        EpubMetadataDate date = ReadDate(metadataItemNode);
                        dates.Add(date);
                        break;
                    case "type":
                        EpubMetadataType type = ReadType(metadataItemNode);
                        types.Add(type);
                        break;
                    case "format":
                        EpubMetadataFormat format = ReadFormat(metadataItemNode);
                        formats.Add(format);
                        break;
                    case "identifier":
                        EpubMetadataIdentifier identifier = ReadIdentifier(metadataItemNode);
                        identifiers.Add(identifier);
                        break;
                    case "source":
                        EpubMetadataSource source = ReadSource(metadataItemNode);
                        sources.Add(source);
                        break;
                    case "language":
                        EpubMetadataLanguage language = ReadLanguage(metadataItemNode);
                        languages.Add(language);
                        break;
                    case "relation":
                        EpubMetadataRelation relation = ReadRelation(metadataItemNode);
                        relations.Add(relation);
                        break;
                    case "coverage":
                        EpubMetadataCoverage coverage = ReadCoverage(metadataItemNode);
                        coverages.Add(coverage);
                        break;
                    case "rights":
                        EpubMetadataRights rightsItem = ReadRightsItem(metadataItemNode);
                        rights.Add(rightsItem);
                        break;
                    case "link":
                        EpubMetadataLink link = ReadLink(metadataItemNode);
                        links.Add(link);
                        break;
                    case "meta":
                        EpubMetadataMeta meta = ReadMeta(metadataItemNode);
                        metaItems.Add(meta);
                        break;
                }
            }
            return new(titles, creators, subjects, descriptions, publishers, contributors, dates, types, formats, identifiers, sources,
                languages, relations, coverages, rights, links, metaItems);
        }

        public static EpubMetadataLink ReadLink(XElement linkNode)
        {
            string? href = null;
            string? id = null;
            string? mediaType = null;
            List<EpubMetadataLinkProperty>? properties = null;
            string? refines = null;
            List<EpubMetadataLinkRelationship>? relationships = null;
            string? hrefLanguage = null;
            foreach (XAttribute linkNodeAttribute in linkNode.Attributes())
            {
                string attributeValue = linkNodeAttribute.Value;
                switch (linkNodeAttribute.GetLowerCaseLocalName())
                {
                    case "href":
                        href = attributeValue;
                        break;
                    case "id":
                        id = attributeValue;
                        break;
                    case "media-type":
                        mediaType = attributeValue;
                        break;
                    case "properties":
                        properties = EpubMetadataLinkPropertyParser.ParsePropertyList(attributeValue);
                        break;
                    case "refines":
                        refines = attributeValue;
                        break;
                    case "rel":
                        relationships = EpubMetadataLinkRelationshipParser.ParseRelationshipList(attributeValue);
                        break;
                    case "hreflang":
                        hrefLanguage = attributeValue;
                        break;
                }
            }
            if (href == null)
            {
                throw new EpubPackageException("Incorrect EPUB metadata link: href is missing.");
            }
            if (relationships == null)
            {
                throw new EpubPackageException("Incorrect EPUB metadata link: rel is missing.");
            }
            return new(href, id, mediaType, properties, refines, relationships, hrefLanguage);
        }

        private static EpubMetadataTitle ReadTitle(XElement titleNode)
        {
            ReadMetadataItemWithIdDirLangAndContent(titleNode, out string? id, out EpubTextDirection? textDirection, out string? language, out string title);
            return new(title, id, textDirection, language);
        }

        private static EpubMetadataCreator ReadCreator(XElement creatorNode)
        {
            string? id = null;
            string? fileAs = null;
            string? role = null;
            EpubTextDirection? textDirection = null;
            string? language = null;
            foreach (XAttribute creatorNodeAttribute in creatorNode.Attributes())
            {
                string attributeValue = creatorNodeAttribute.Value;
                switch (creatorNodeAttribute.GetLowerCaseLocalName())
                {
                    case "id":
                        id = attributeValue;
                        break;
                    case "file-as":
                        fileAs = attributeValue;
                        break;
                    case "role":
                        role = attributeValue;
                        break;
                    case "dir":
                        textDirection = EpubTextDirectionParser.Parse(attributeValue);
                        break;
                    case "lang":
                        language = attributeValue;
                        break;
                }
            }
            string creator = creatorNode.Value;
            return new(creator, id, fileAs, role, textDirection, language);
        }

        private static EpubMetadataSubject ReadSubject(XElement subjectNode)
        {
            ReadMetadataItemWithIdDirLangAndContent(subjectNode, out string? id, out EpubTextDirection? textDirection, out string? language, out string subject);
            return new(subject, id, textDirection, language);
        }

        private static EpubMetadataDescription ReadDescription(XElement descriptionNode)
        {
            ReadMetadataItemWithIdDirLangAndContent(descriptionNode, out string? id, out EpubTextDirection? textDirection, out string? language, out string description);
            return new(description, id, textDirection, language);
        }

        private static EpubMetadataPublisher ReadPublisher(XElement publisherNode)
        {
            ReadMetadataItemWithIdDirLangAndContent(publisherNode, out string? id, out EpubTextDirection? textDirection, out string? language, out string publisher);
            return new(publisher, id, textDirection, language);
        }

        private static EpubMetadataContributor ReadContributor(XElement contributorNode)
        {
            string? id = null;
            string? fileAs = null;
            string? role = null;
            EpubTextDirection? textDirection = null;
            string? language = null;
            foreach (XAttribute contributorNodeAttribute in contributorNode.Attributes())
            {
                string attributeValue = contributorNodeAttribute.Value;
                switch (contributorNodeAttribute.GetLowerCaseLocalName())
                {
                    case "id":
                        id = attributeValue;
                        break;
                    case "file-as":
                        fileAs = attributeValue;
                        break;
                    case "role":
                        role = attributeValue;
                        break;
                    case "dir":
                        textDirection = EpubTextDirectionParser.Parse(attributeValue);
                        break;
                    case "lang":
                        language = attributeValue;
                        break;
                }
            }
            string contributor = contributorNode.Value;
            return new(contributor, id, fileAs, role, textDirection, language);
        }

        private static EpubMetadataDate ReadDate(XElement dateNode)
        {
            string date;
            string? id = null;
            string? @event = null;
            foreach (XAttribute dateNodeAttribute in dateNode.Attributes())
            {
                string attributeValue = dateNodeAttribute.Value;
                switch (dateNodeAttribute.GetLowerCaseLocalName())
                {
                    case "id":
                        id = attributeValue;
                        break;
                    case "event":
                        @event = attributeValue;
                        break;
                }
            }
            date = dateNode.Value;
            return new EpubMetadataDate(date, id, @event);
        }

        private static EpubMetadataType ReadType(XElement typeNode)
        {
            ReadMetadataItemWithIdAndContent(typeNode, out string? id, out string type);
            return new(type, id);
        }

        private static EpubMetadataFormat ReadFormat(XElement formatNode)
        {
            ReadMetadataItemWithIdAndContent(formatNode, out string? id, out string format);
            return new(format, id);
        }

        private static EpubMetadataIdentifier ReadIdentifier(XElement identifierNode)
        {
            string? id = null;
            string? scheme = null;
            foreach (XAttribute identifierNodeAttribute in identifierNode.Attributes())
            {
                string attributeValue = identifierNodeAttribute.Value;
                switch (identifierNodeAttribute.GetLowerCaseLocalName())
                {
                    case "id":
                        id = attributeValue;
                        break;
                    case "scheme":
                        scheme = attributeValue;
                        break;
                }
            }
            string identifier = identifierNode.Value;
            return new(identifier, id, scheme);
        }

        private static EpubMetadataSource ReadSource(XElement sourceNode)
        {
            ReadMetadataItemWithIdAndContent(sourceNode, out string? id, out string source);
            return new(source, id);
        }

        private static EpubMetadataLanguage ReadLanguage(XElement languageNode)
        {
            ReadMetadataItemWithIdAndContent(languageNode, out string? id, out string language);
            return new(language, id);
        }

        private static EpubMetadataRelation ReadRelation(XElement relationNode)
        {
            ReadMetadataItemWithIdDirLangAndContent(relationNode, out string? id, out EpubTextDirection? textDirection, out string? language, out string relation);
            return new(relation, id, textDirection, language);
        }

        private static EpubMetadataCoverage ReadCoverage(XElement coverageNode)
        {
            ReadMetadataItemWithIdDirLangAndContent(coverageNode, out string? id, out EpubTextDirection? textDirection, out string? language, out string coverage);
            return new(coverage, id, textDirection, language);
        }

        private static EpubMetadataRights ReadRightsItem(XElement rightsNode)
        {
            ReadMetadataItemWithIdDirLangAndContent(rightsNode, out string? id, out EpubTextDirection? textDirection, out string? language, out string rights);
            return new(rights, id, textDirection, language);
        }

        private static EpubMetadataMeta ReadMeta(XElement metaNode)
        {
            string? name = null;
            string? content = null;
            string? id = null;
            string? refines = null;
            string? property = null;
            string? scheme = null;
            EpubTextDirection? textDirection = null;
            string? language = null;
            foreach (XAttribute metaNodeAttribute in metaNode.Attributes())
            {
                string attributeValue = metaNodeAttribute.Value;
                switch (metaNodeAttribute.GetLowerCaseLocalName())
                {
                    case "name":
                        name = attributeValue;
                        break;
                    case "content":
                        content = attributeValue;
                        break;
                    case "id":
                        id = attributeValue;
                        break;
                    case "refines":
                        refines = attributeValue;
                        break;
                    case "property":
                        property = attributeValue;
                        break;
                    case "scheme":
                        scheme = attributeValue;
                        break;
                    case "dir":
                        textDirection = EpubTextDirectionParser.Parse(attributeValue);
                        break;
                    case "lang":
                        language = attributeValue;
                        break;
                }
            }
            content ??= metaNode.Value;
            return new(name, content, id, refines, property, scheme, textDirection, language);
        }

        private static void ReadMetadataItemWithIdAndContent(XElement metadataItemNode, out string? id, out string content)
        {
            XAttribute idAttribute = metadataItemNode.Attribute("id");
            id = idAttribute?.Value;
            content = metadataItemNode.Value;
        }

        private static void ReadMetadataItemWithIdDirLangAndContent(XElement metadataItemNode, out string? id, out EpubTextDirection? textDirection, out string? language,
            out string content)
        {
            id = null;
            textDirection = null;
            language = null;
            foreach (XAttribute metadataItemNodeAttribute in metadataItemNode.Attributes())
            {
                string attributeValue = metadataItemNodeAttribute.Value;
                switch (metadataItemNodeAttribute.GetLowerCaseLocalName())
                {
                    case "id":
                        id = attributeValue;
                        break;
                    case "dir":
                        textDirection = EpubTextDirectionParser.Parse(attributeValue);
                        break;
                    case "lang":
                        language = attributeValue;
                        break;
                }
            }
            content = metadataItemNode.Value;
        }
    }
}
