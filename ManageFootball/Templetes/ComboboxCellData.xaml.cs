using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;

namespace ManageFootball.Templetes
{
    public class ComboboxCellData
    {
        public static void ComboBox_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (sender is ComboBox comboBox)
            {
                MessageBox.Show($"Bạn vừa nhấn phím: {e.Key}");
            }
        }
    }
}
