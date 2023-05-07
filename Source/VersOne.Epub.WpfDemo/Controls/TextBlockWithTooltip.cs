using System;
using System.Windows;
using System.Windows.Controls;

namespace VersOne.Epub.WpfDemo.Controls
{
    /// <summary>
    /// A text block control that shows a tooltip with the content of its <see cref="TextBlock.Text" /> property if it is trimmed on the screen.
    /// </summary>
    public class TextBlockWithTooltip : TextBlock
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TextBlockWithTooltip" /> class.
        /// </summary>
        public TextBlockWithTooltip()
        {
            SizeChanged += TextBlockWithTooltip_SizeChanged;
        }

        private void TextBlockWithTooltip_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Measure(new Size(Double.PositiveInfinity, Double.PositiveInfinity));
            if (ActualWidth < DesiredSize.Width)
            {
                ToolTipService.SetToolTip(this, Text);
            }
            else
            {
                ToolTipService.SetToolTip(this, null);
            }
        }
    }
}
