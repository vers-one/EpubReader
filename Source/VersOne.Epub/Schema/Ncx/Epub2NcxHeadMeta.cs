using System;

namespace VersOne.Epub.Schema
{
    /// <summary>
    /// <para>NCX document metadata item.</para>
    /// <para>
    /// See <see href="https://idpf.org/epub/20/spec/OPF_2.0.1_draft.htm#Section2.4.1" />,
    /// <see href="https://daisy.org/activities/standards/daisy/daisy-3/z39-86-2005-r2012-specifications-for-the-digital-talking-book/#NCX" />,
    /// and <see href="http://www.daisy.org/z3986/2005/ncx-2005-1.dtd" /> for more information.
    /// </para>
    /// </summary>
    public class Epub2NcxHeadMeta
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Epub2NcxHeadMeta" /> class.
        /// </summary>
        /// <param name="name">The name of the NCX metadata item.</param>
        /// <param name="content">The content (i.e. the value) of the NCX metadata item.</param>
        /// <param name="scheme">The name of the scheme for the <see cref="Name" /> property or <c>null</c> if the scheme is not specified.</param>
        /// <exception cref="ArgumentNullException">The <paramref name="name"/> argument is <c>null</c>.</exception>
        /// <exception cref="ArgumentNullException">The <paramref name="content"/> argument is <c>null</c>.</exception>
        public Epub2NcxHeadMeta(string name, string content, string? scheme = null)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Content = content ?? throw new ArgumentNullException(nameof(content));
            Scheme = scheme;
        }

        /// <summary>
        /// <para>Gets the name of the NCX metadata item.</para>
        /// <para>See <see href="http://www.daisy.org/z3986/2005/ncx-2005-1.dtd" /> for more information.</para>
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// <para>Gets the content (i.e. the value) of the NCX metadata item.</para>
        /// <para>See <see href="http://www.daisy.org/z3986/2005/ncx-2005-1.dtd" /> for more information.</para>
        /// </summary>
        public string Content { get; }

        /// <summary>
        /// <para>Gets the name of the scheme for the <see cref="Name" /> property or <c>null</c> if the scheme is not specified.</para>
        /// <para>See <see href="http://www.daisy.org/z3986/2005/ncx-2005-1.dtd" /> for more information.</para>
        /// </summary>
        public string? Scheme { get; }
    }
}
