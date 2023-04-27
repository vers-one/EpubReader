using System.Collections.Generic;

namespace VersOne.Epub.Schema
{
    /// <summary>
    /// <para>NCX page list. It contains one or more NCX page targets which provide pagination information.</para>
    /// <para>
    /// See <see href="https://idpf.org/epub/20/spec/OPF_2.0.1_draft.htm#Section2.4.1" />,
    /// <see href="https://daisy.org/activities/standards/daisy/daisy-3/z39-86-2005-r2012-specifications-for-the-digital-talking-book/#NCX" />,
    /// and <see href="http://www.daisy.org/z3986/2005/ncx-2005-1.dtd" /> for more information.
    /// </para>
    /// </summary>
    public class Epub2NcxPageList
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Epub2NcxPageList" /> class.
        /// </summary>
        /// <param name="items">A list of NCX page targets which provide pagination information.</param>
        public Epub2NcxPageList(List<Epub2NcxPageTarget>? items)
        {
            Items = items ?? new List<Epub2NcxPageTarget>();
        }

        /// <summary>
        /// <para>Gets a list of NCX page targets which provide pagination information.</para>
        /// <para>
        /// See <see href="https://idpf.org/epub/20/spec/OPF_2.0.1_draft.htm#Section2.4.1" />,
        /// <see href="https://daisy.org/activities/standards/daisy/daisy-3/z39-86-2005-r2012-specifications-for-the-digital-talking-book/#NCX" />,
        /// and <see href="http://www.daisy.org/z3986/2005/ncx-2005-1.dtd" /> for more information.
        /// </para>
        /// </summary>
        public List<Epub2NcxPageTarget> Items { get; }
    }
}
