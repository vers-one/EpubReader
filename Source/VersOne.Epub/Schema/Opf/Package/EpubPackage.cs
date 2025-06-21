using System;
using System.Collections.Generic;
using VersOne.Epub.Internal;

namespace VersOne.Epub.Schema
{
    /// <summary>
    /// <para>Parsed content of the OPF package of the EPUB book.</para>
    /// <para>
    /// See <see href="https://www.w3.org/TR/epub-33/#sec-package-doc" />
    /// and <see href="https://idpf.org/epub/20/spec/OPF_2.0.1_draft.htm" /> for more information.
    /// </para>
    /// </summary>
    public class EpubPackage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EpubPackage" /> class.
        /// </summary>
        /// <param name="uniqueIdentifier">
        /// The ID of the <see cref="EpubMetadataIdentifier" /> element that provides the preferred, or primary, identifier of the EPUB book
        /// or <c>null</c> if the package doesn't specify the identifier of the book.
        /// </param>
        /// <param name="epubVersion">The version of EPUB specification to which this EPUB package conforms.</param>
        /// <param name="metadata">The meta information for the EPUB book.</param>
        /// <param name="manifest">The EPUB manifest which provides an exhaustive list of content items that constitute the EPUB book.</param>
        /// <param name="spine">The EPUB spine which defines an ordered list of <see cref="EpubSpineItemRef" /> items that represent the default reading order of the EPUB book.</param>
        /// <param name="guide">
        /// The EPUB 2 guide which provides machine-processable navigation to the key structural components of the EPUB book or <c>null</c> if the book doesn't have a EPUB 2 guide.
        /// </param>
        /// <param name="collections">A list of <see cref="EpubCollection" /> elements which define related groups of resources within the EPUB book.</param>
        /// <param name="id">The ID of the OPF package or <c>null</c> if the package doesn't specify an ID.</param>
        /// <param name="textDirection">The default text direction of the content of the EPUB book or <c>null</c> if the package doesn't specify a text direction.</param>
        /// <param name="prefix">The additional prefix mappings or <c>null</c> if the package doesn't specify additional prefix mappings.</param>
        /// <param name="language">The language of the content of the EPUB book or <c>null</c> if the package doesn't specify the language.</param>
        /// <exception cref="ArgumentNullException">The <paramref name="metadata" /> parameter is <c>null</c>.</exception>
        /// <exception cref="ArgumentNullException">The <paramref name="manifest" /> parameter is <c>null</c>.</exception>
        /// <exception cref="ArgumentNullException">The <paramref name="spine" /> parameter is <c>null</c>.</exception>
        public EpubPackage(string? uniqueIdentifier, EpubVersion epubVersion, EpubMetadata metadata, EpubManifest manifest, EpubSpine spine, EpubGuide? guide,
            List<EpubCollection>? collections = null, string? id = null, EpubTextDirection? textDirection = null, string? prefix = null, string? language = null)
        {
            UniqueIdentifier = uniqueIdentifier;
            EpubVersion = epubVersion;
            Metadata = metadata ?? throw new ArgumentNullException(nameof(metadata));
            Manifest = manifest ?? throw new ArgumentNullException(nameof(manifest));
            Spine = spine ?? throw new ArgumentNullException(nameof(spine));
            Guide = guide;
            Collections = collections ?? new List<EpubCollection>();
            Id = id;
            TextDirection = textDirection;
            Prefix = prefix;
            Language = language;
        }

        /// <summary>
        /// <para>Gets the ID of the <see cref="EpubMetadataIdentifier" /> element that provides the preferred, or primary, identifier of the EPUB book
        /// or <c>null</c> if the package doesn't specify the identifier of the book. This value is required for EPUB 3 books.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub-33/#attrdef-package-unique-identifier" /> for more information.</para>
        /// </summary>
        public string? UniqueIdentifier { get; }

        /// <summary>
        /// <para>Gets the version of EPUB specification to which this EPUB package conforms.</para>
        /// <para>
        /// See <see href="https://www.w3.org/TR/epub-33/#attrdef-package-version" />
        /// and <see href="https://idpf.org/epub/20/spec/OPF_2.0.1_draft.htm#Section1.3.2" /> for more information.
        /// </para>
        /// </summary>
        public EpubVersion EpubVersion { get; }

        /// <summary>
        /// <para>Gets the meta information for the EPUB book.</para>
        /// <para>
        /// See <see href="https://www.w3.org/TR/epub-33/#sec-pkg-metadata" />
        /// and <see href="https://idpf.org/epub/20/spec/OPF_2.0.1_draft.htm#Section2.2" /> for more information.
        /// </para>
        /// </summary>
        public EpubMetadata Metadata { get; }

        /// <summary>
        /// <para>Gets the EPUB manifest which provides an exhaustive list of content items that constitute the EPUB book.</para>
        /// <para>
        /// See <see href="https://www.w3.org/TR/epub-33/#sec-manifest-elem" />
        /// and <see href="https://idpf.org/epub/20/spec/OPF_2.0.1_draft.htm#Section2.3" /> for more information.
        /// </para>
        /// </summary>
        public EpubManifest Manifest { get; }

        /// <summary>
        /// <para>Gets the EPUB spine which defines an ordered list of <see cref="EpubSpineItemRef" /> items that represent the default reading order of the EPUB book.</para>
        /// <para>
        /// See <see href="https://www.w3.org/TR/epub-33/#sec-pkg-spine" />
        /// and <see href="https://idpf.org/epub/20/spec/OPF_2.0.1_draft.htm#Section2.4" /> for more information.
        /// </para>
        /// </summary>
        public EpubSpine Spine { get; }

        /// <summary>
        /// <para>
        /// Gets the EPUB 2 guide which provides machine-processable navigation to the key structural components of the EPUB book
        /// or <c>null</c> if the book doesn't have a EPUB 2 guide.</para>
        /// <para>
        /// See <see href="https://www.w3.org/TR/epub-33/#sec-opf2-guide" />
        /// and <see href="https://idpf.org/epub/20/spec/OPF_2.0.1_draft.htm#Section2.6" /> for more information.
        /// </para>
        /// </summary>
        public EpubGuide? Guide { get; }

        /// <summary>
        /// <para>Gets a list of <see cref="EpubCollection" /> elements which define related groups of resources within the EPUB book.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub-33/#elemdef-collection" /> for more information.</para>
        /// </summary>
        public List<EpubCollection> Collections { get; }

        /// <summary>
        /// <para>Gets the ID of the OPF package or <c>null</c> if the package doesn't specify an ID.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub-33/#attrdef-id" /> for more information.</para>
        /// </summary>
        public string? Id { get; }

        /// <summary>
        /// <para>Gets the default text direction of the content of the EPUB book or <c>null</c> if the package doesn't specify a text direction.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub-33/#attrdef-dir" /> for more information.</para>
        /// </summary>
        public EpubTextDirection? TextDirection { get; }

        /// <summary>
        /// <para>Gets the additional prefix mappings or <c>null</c> if the package doesn't specify additional prefix mappings.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub-33/#attrdef-package-prefix" /> for more information.</para>
        /// </summary>
        public string? Prefix { get; }

        /// <summary>
        /// <para>Gets the language of the content of the EPUB book or <c>null</c> if the package doesn't specify the language.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub-33/#attrdef-xml-lang" /> for more information.</para>
        /// </summary>
        public string? Language { get; }

        /// <summary>
        /// Gets the string representation of the <see cref="EpubVersion" /> property.
        /// </summary>
        /// <returns>String representation of the <see cref="EpubVersion" /> property.</returns>
        public string GetVersionString()
        {
            return VersionUtils.GetVersionString(EpubVersion);
        }
    }
}
