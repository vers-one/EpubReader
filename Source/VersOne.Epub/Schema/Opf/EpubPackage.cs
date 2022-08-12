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
        /// <para>Gets the version of EPUB specification to which the given <see cref="EpubPackage" /> conforms.</para>
        /// <para>
        /// See <see href="https://www.w3.org/publishing/epub32/epub-packages.html#attrdef-package-version" />
        /// and <see href="https://idpf.org/epub/20/spec/OPF_2.0.1_draft.htm#Section1.3.2" /> for more information.
        /// </para>
        /// </summary>
        public EpubVersion EpubVersion { get; internal set; }

        /// <summary>
        /// <para>Gets the meta information for the EPUB book.</para>
        /// <para>
        /// See <see href="https://www.w3.org/publishing/epub32/epub-packages.html#sec-pkg-metadata" />
        /// and <see href="https://idpf.org/epub/20/spec/OPF_2.0.1_draft.htm#Section2.2" /> for more information.
        /// </para>
        /// </summary>
        public EpubMetadata Metadata { get; internal set; }

        /// <summary>
        /// <para>Gets the EPUB manifest which provides an exhaustive list of content items that constitute the EPUB book.</para>
        /// <para>
        /// See <see href="https://www.w3.org/publishing/epub32/epub-packages.html#sec-manifest-elem" />
        /// and <see href="https://idpf.org/epub/20/spec/OPF_2.0.1_draft.htm#Section2.3" /> for more information.
        /// </para>
        /// </summary>
        public EpubManifest Manifest { get; internal set; }

        /// <summary>
        /// <para>Gets the EPUB spine which defines an ordered list of <see cref="EpubSpineItemRef" /> items that represent the default reading order of the EPUB book.</para>
        /// <para>
        /// See <see href="https://www.w3.org/publishing/epub32/epub-packages.html#sec-pkg-spine" />
        /// and <see href="https://idpf.org/epub/20/spec/OPF_2.0.1_draft.htm#Section2.4" /> for more information.
        /// </para>
        /// </summary>
        public EpubSpine Spine { get; internal set; }

        /// <summary>
        /// <para>Gets the EPUB 2 guide which provides machine-processable navigation to the key structural components of the EPUB book.</para>
        /// <para>
        /// See <see href="https://www.w3.org/publishing/epub32/epub-packages.html#sec-opf2-guide" />
        /// and <see href="https://idpf.org/epub/20/spec/OPF_2.0.1_draft.htm#Section2.6" /> for more information.
        /// </para>
        /// </summary>
        public EpubGuide Guide { get; internal set; }

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
