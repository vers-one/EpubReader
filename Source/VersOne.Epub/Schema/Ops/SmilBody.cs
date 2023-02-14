using System;
using System.Collections.Generic;

namespace VersOne.Epub.Schema
{
    /// <summary>
    /// <para>
    /// Parsed content of the media overlay body which is the starting point for the presentation contained in the media overlay document.
    /// This class corresponds to the &lt;body&gt; element in a media overlay document.
    /// </para>
    /// <para>See <see href="https://www.w3.org/publishing/epub32/epub-mediaoverlays.html#elemdef-smil-body" /> for more information.</para>
    /// </summary>
    public class SmilBody
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SmilBody" /> class.
        /// </summary>
        /// <param name="id">An optional identifier of this element.</param>
        /// <param name="epubTypes">An optional list of the structural semantics of the corresponding element in the EPUB content document.</param>
        /// <param name="epubTextRef">The relative IRI reference of the corresponding EPUB content document, including a fragment identifier that references the specific element.</param>
        /// <param name="seqs">The list of the media objects which are to be rendered sequentially.</param>
        /// <param name="pars">The list of the media objects which are to be rendered in parallel.</param>
        /// <exception cref="ArgumentNullException">The <paramref name="seqs" /> parameter is <c>null</c>.</exception>
        /// <exception cref="ArgumentNullException">The <paramref name="pars" /> parameter is <c>null</c>.</exception>
        public SmilBody(string? id, List<Epub3StructuralSemanticsProperty>? epubTypes, string? epubTextRef, List<SmilSeq> seqs, List<SmilPar> pars)
        {
            Id = id;
            EpubTypes = epubTypes;
            EpubTextRef = epubTextRef;
            Seqs = seqs ?? throw new ArgumentNullException(nameof(seqs));
            Pars = pars ?? throw new ArgumentNullException(nameof(pars));
        }

        /// <summary>
        /// Gets an optional identifier of this element. If present, this value is unique within the scope of the media overlay document.
        /// </summary>
        public string? Id { get; }

        /// <summary>
        /// Gets an optional list of the structural semantics of the corresponding element in the EPUB content document.
        /// </summary>
        public List<Epub3StructuralSemanticsProperty>? EpubTypes { get; }

        /// <summary>
        /// Gets the relative IRI reference of the corresponding EPUB content document, including a fragment identifier that references the specific element.
        /// </summary>
        public string? EpubTextRef { get; }

        /// <summary>
        /// Gets the list of the media objects which are to be rendered sequentially.
        /// This list represents a sequence of nested containers such as sections, asides, headers, and footnotes.
        /// It allows the structure inherent in these containers to be retained in the media overlay document.
        /// </summary>
        public List<SmilSeq> Seqs { get; }

        /// <summary>
        /// Gets the list of the media objects which are to be rendered in parallel.
        /// Each element in this list represents a phrase and identifies a text and audio component to synchronize during playback.
        /// </summary>
        public List<SmilPar> Pars { get; }
    }
}
