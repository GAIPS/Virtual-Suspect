using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace VirtualSuspectUI {

    /// <summary>
    /// Interaction logic for DimensionPropertiesBox.xaml
    /// </summary>
    /// 
    public partial class ComboBoxDimensionPropertiesBox : UserControl, ConditionBox {

        public ComboBoxDimensionPropertiesBox() {
            InitializeComponent();
        }

        public string Dimension {
            get {
                return (string)DimensionLabel.Content;
            }
        }

        public bool Known {
            get {
                return (bool)KnwonCheckBox.IsChecked;
            }
        }

        public string Value {
            get {
                return PossibleValueComboBox.SelectedItem.ToString();
            }
        }

        bool ConditionBox.Focus {
            get {
                return (bool)ToDiscoverCheckBox.IsChecked;
            }
        }

        private void RemoveDimensionBox_Click(object sender, RoutedEventArgs e) {

            ((StackPanel)this.Parent).Children.Remove(this);
        }

        private void ToDiscoverCheckBox_Checked(object sender, RoutedEventArgs e) {

            //Disable Other Components
            KnwonCheckBox.IsEnabled = false;
            PossibleValueComboBox.IsEnabled = false;
        }

        private void ToDiscoverCheckBox_UnChecked(object sender, RoutedEventArgs e) {

            //Disable Other Components
            KnwonCheckBox.IsEnabled = true;
            PossibleValueComboBox.IsEnabled = true;
        }
    }
}
