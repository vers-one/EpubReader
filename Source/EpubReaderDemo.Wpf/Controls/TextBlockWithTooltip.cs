using System;
using System.Windows;
using System.Windows.Controls;

namespace EpubReaderDemo.Wpf.Controls
{
    public class TextBlockWithTooltip : TextBlock
    {
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
