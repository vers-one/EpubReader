namespace VersOne.Epub.Schema
{
    /// <summary>
    /// <para>The type of the page target in EPUB 2 NCX.</para>
    /// <para>
    /// See <see href="https://daisy.org/activities/standards/daisy/daisy-3/z39-86-2005-r2012-specifications-for-the-digital-talking-book/#li_392f" />
    /// and <see href="http://www.daisy.org/z3986/2005/ncx-2005-1.dtd" /> for more information.
    /// </para>
    /// </summary>
    public enum Epub2NcxPageTargetType
    {
        /// <summary>
        /// <para>Front-matter page target type. These pages appear at the beginning of the book and are usually numbered with Roman numerals.</para>
        /// <para>
        /// See <see href="https://daisy.org/activities/standards/daisy/daisy-3/z39-86-2005-r2012-specifications-for-the-digital-talking-book/#li_392f" /> for more information.
        /// </para>
        /// </summary>
        FRONT = 1,

        /// <summary>
        /// <para>Body matter page target type. These pages constitute the main content of the book and are usually numbered with Arabic numerals.</para>
        /// <para>
        /// See <see href="https://daisy.org/activities/standards/daisy/daisy-3/z39-86-2005-r2012-specifications-for-the-digital-talking-book/#li_392f" /> for more information.
        /// </para>
        /// </summary>
        NORMAL,

        /// <summary>
        /// <para>Special section page (not a front or a body matter) target type.</para>
        /// <para>
        /// See <see href="https://daisy.org/activities/standards/daisy/daisy-3/z39-86-2005-r2012-specifications-for-the-digital-talking-book/#li_392f" /> for more information.
        /// </para>
        /// </summary>
        SPECIAL,

        /// <summary>
        /// A page target type which is not present in this enumeration.
        /// </summary>
        UNKNOWN
    }
}
