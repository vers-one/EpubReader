using System;
using VersOne.Epub.Internal;

namespace VersOne.Epub.Schema
{
    /// <summary>
    /// <para>Parsed content of the OPF package of the EPUB book.</para>
    /// <para>
    /// See <see href="https://www.w3.org/publishing/epub32/epub-packages.html#sec-package-doc" />
    /// and <see href="https://idpf.org/epub/20/spec/OPF_2.0.1_draft.htm" /> for more information.
    /// </para>
    /// </summary>
    public class EpubPackage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EpubPackage" /> class.
        /// </summary>
        /// <param name="epubVersion">The version of EPUB specification to which this EPUB package conforms.</param>
        /// <param name="metadata">The meta information for the EPUB book.</param>
        /// <param name="manifest">The EPUB manifest which provides an exhaustive list of content items that constitute the EPUB book.</param>
        /// <param name="spine">The EPUB spine which defines an ordered list of <see cref="EpubSpineItemRef" /> items that represent the default reading order of the EPUB book.</param>
        /// <param name="guide">
        /// The EPUB 2 guide which provides machine-processable navigation to the key structural components of the EPUB book or <c>null</c> if the book doesn't have a EPUB 2 guide.
        /// </param>
        /// <exception cref="ArgumentNullException">The <paramref name="metadata" /> parameter is <c>null</c>.</exception>
        /// <exception cref="ArgumentNullException">The <paramref name="manifest" /> parameter is <c>null</c>.</exception>
        /// <exception cref="ArgumentNullException">The <paramref name="spine" /> parameter is <c>null</c>.</exception>
        public EpubPackage(EpubVersion epubVersion, EpubMetadata metadata, EpubManifest manifest, EpubSpine spine, EpubGuide? guide)
        {
            EpubVersion = epubVersion;
            Metadata = metadata ?? throw new ArgumentNullException(nameof(metadata));
            Manifest = manifest ?? throw new ArgumentNullException(nameof(manifest));
            Spine = spine ?? throw new ArgumentNullException(nameof(spine));
            Guide = guide;
        }

        /// <summary>
        /// <para>Gets the version of EPUB specification to which this EPUB package conforms.</para>
        /// <para>
        /// See <see href="https://www.w3.org/publishing/epub32/epub-packages.html#attrdef-package-version" />
        /// and <see href="https://idpf.org/epub/20/spec/OPF_2.0.1_draft.htm#Section1.3.2" /> for more information.
        /// </para>
        /// </summary>
        public EpubVersion EpubVersion { get; }

        /// <summary>
        /// <para>Gets the meta information for the EPUB book.</para>
        /// <para>
        /// See <see href="https://www.w3.org/publishing/epub32/epub-packages.html#sec-pkg-metadata" />
        /// and <see href="https://idpf.org/epub/20/spec/OPF_2.0.1_draft.htm#Section2.2" /> for more information.
        /// </para>
        /// </summary>
        public EpubMetadata Metadata { get; }

        /// <summary>
        /// <para>Gets the EPUB manifest which provides an exhaustive list of content items that constitute the EPUB book.</para>
        /// <para>
        /// See <see href="https://www.w3.org/publishing/epub32/epub-packages.html#sec-manifest-elem" />
        /// and <see href="https://idpf.org/epub/20/spec/OPF_2.0.1_draft.htm#Section2.3" /> for more information.
        /// </para>
        /// </summary>
        public EpubManifest Manifest { get; }

        /// <summary>
        /// <para>Gets the EPUB spine which defines an ordered list of <see cref="EpubSpineItemRef" /> items that represent the default reading order of the EPUB book.</para>
        /// <para>
        /// See <see href="https://www.w3.org/publishing/epub32/epub-packages.html#sec-pkg-spine" />
        /// and <see href="https://idpf.org/epub/20/spec/OPF_2.0.1_draft.htm#Section2.4" /> for more information.
        /// </para>
        /// </summary>
        public EpubSpine Spine { get; }

        /// <summary>
        /// <para>
        /// Gets the EPUB 2 guide which provides machine-processable navigation to the key structural components of the EPUB book
        /// or <c>null</c> if the book doesn't have a EPUB 2 guide.</para>
        /// <para>
        /// See <see href="https://www.w3.org/publishing/epub32/epub-packages.html#sec-opf2-guide" />
        /// and <see href="https://idpf.org/epub/20/spec/OPF_2.0.1_draft.htm#Section2.6" /> for more information.
        /// </para>
        /// </summary>
        public EpubGuide? Guide { get; }

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
