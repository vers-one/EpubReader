using System.Collections.Generic;

namespace VersOne.Epub.Schema
{
    /// <summary>
    /// <para>NCX navigation map which acts as a container for one or more NCX navigation points for the primary navigation within the book (e.g., table of contents).</para>
    /// <para>
    /// See <see href="https://idpf.org/epub/20/spec/OPF_2.0.1_draft.htm#Section2.4.1" />,
    /// <see href="https://daisy.org/activities/standards/daisy/daisy-3/z39-86-2005-r2012-specifications-for-the-digital-talking-book/#NCX" />,
    /// and <see href="http://www.daisy.org/z3986/2005/ncx-2005-1.dtd" /> for more information.
    /// </para>
    /// </summary>
    public class Epub2NcxNavigationMap
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Epub2NcxNavigationMap" /> class.
        /// </summary>
        /// <param name="items">A list of NCX navigation points for the primary navigation within the book (e.g., table of contents).</param>
        public Epub2NcxNavigationMap(List<Epub2NcxNavigationPoint>? items = null)
        {
            Items = items ?? new List<Epub2NcxNavigationPoint>();
        }

        /// <summary>
        /// <para>Gets a list of NCX navigation points for the primary navigation within the book (e.g., table of contents).</para>
        /// <para>
        /// See <see href="https://idpf.org/epub/20/spec/OPF_2.0.1_draft.htm#Section2.4.1" />,
        /// <see href="https://daisy.org/activities/standards/daisy/daisy-3/z39-86-2005-r2012-specifications-for-the-digital-talking-book/#NCX" />,
        /// and <see href="http://www.daisy.org/z3986/2005/ncx-2005-1.dtd" /> for more information.
        /// </para>
        /// </summary>
        public List<Epub2NcxNavigationPoint> Items { get; }
    }
}
