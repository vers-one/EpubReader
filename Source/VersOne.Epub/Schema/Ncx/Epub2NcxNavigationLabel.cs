using System;

namespace VersOne.Epub.Schema
{
    /// <summary>
    /// <para>Textual description of a navigational element for the reader.</para>
    /// <para>
    /// See <see href="https://idpf.org/epub/20/spec/OPF_2.0.1_draft.htm#Section2.4.1" />,
    /// <see href="https://daisy.org/activities/standards/daisy/daisy-3/z39-86-2005-r2012-specifications-for-the-digital-talking-book/#NCX" />,
    /// and <see href="http://www.daisy.org/z3986/2005/ncx-2005-1.dtd" /> for more information.
    /// </para>
    /// </summary>
    public class Epub2NcxNavigationLabel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Epub2NcxNavigationLabel" /> class.
        /// </summary>
        /// <param name="text">A textual representation of the navigation label.</param>
        /// <exception cref="ArgumentNullException">The <paramref name="text" /> parameter is <c>null</c>.</exception>
        public Epub2NcxNavigationLabel(string text)
        {
            Text = text ?? throw new ArgumentNullException(nameof(text));
        }

        /// <summary>
        /// <para>Gets a textual representation of the navigation label.</para>
        /// <para>See <see href="http://www.daisy.org/z3986/2005/ncx-2005-1.dtd" /> for more information.</para>
        /// </summary>
        public string Text { get; }

        /// <summary>
        /// Returns a string containing the value of the <see cref="Text" /> property for debugging purposes.
        /// </summary>
        /// <returns>A string containing the value of the <see cref="Text" /> property.</returns>
        public override string ToString()
        {
            return Text;
        }
    }
}
