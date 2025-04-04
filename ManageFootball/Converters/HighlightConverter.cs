using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows;
using System.Windows.Data;
using System.Windows.Controls;

namespace ManageFootball.Converters
{
    public class HighlightConverter: IMultiValueConverter
    {

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var textBlock = new TextBlock();

            if (values.Length < 2 || values[0] == null || values[1] == null)
                return textBlock;

            string text = values[0].ToString();
            string searchText = values[1].ToString();

            int index = 0;
            while (index < text.Length)
            {
                int matchIndex = text.IndexOf(searchText, index, StringComparison.OrdinalIgnoreCase);

                if (matchIndex == -1)
                {
                    textBlock.Inlines.Add(new Run(text.Substring(index)));
                    break;
                }

                if (matchIndex > index)
                {
                    textBlock.Inlines.Add(new Run(text.Substring(index, matchIndex - index)));
                }

                textBlock.Inlines.Add(new Run(text.Substring(matchIndex, searchText.Length))
                {
                    Background = Brushes.LightYellow,
                    FontWeight = FontWeights.Bold
                });

                index = matchIndex + searchText.Length;
            }

            return textBlock;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
