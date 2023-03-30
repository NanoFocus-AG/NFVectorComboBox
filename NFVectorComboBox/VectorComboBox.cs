using System;
using System.ComponentModel;
using System.Windows.Forms;
using NFNumericTextBox;
using de.nanofocus.NFEval;

namespace NFVectorComboBox
{
    public partial class VectorComboBox : UserControl
    {

        public VectorComboBox()
        {
            InitializeComponent();
            Controls.Add(comboBox1);
            Controls.Add(numericTextBox1);
            numericTextBox1.DataBindings.Add("Text", bindingSource, "", true, DataSourceUpdateMode.OnPropertyChanged);
            comboBox1.DataSource = bindingSource;
        }


        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public BindingSource bindingSource => bindingSource1;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public NumericTextBox numericTextBox => numericTextBox1;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public ComboBox ComboBox => comboBox1;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public NFVariant MyProperty { get; set; }

        private void checkType()
        {
            uint count = MyProperty.getNumberOfElements();
            switch (MyProperty.getType())
            {
                case NFVariant.DataType.DOUBLE_VECTOR_TYPE:
                    numericTextBox.IsInteger = false;
                    double[] doubleList = new double[count];
                    MyProperty.getDoubleVector(doubleList, count);
                    doubleList[comboBox1.SelectedIndex] = Convert.ToDouble(comboBox1.SelectedValue);
                    MyProperty.setDoubleVector(doubleList, count);
                    break;
                case NFVariant.DataType.INT_VECTOR_TYPE:
                    numericTextBox.IsInteger = true;
                    long[] intList = new long[count];
                    MyProperty.getIntVector(intList, count);
                    intList[comboBox1.SelectedIndex] = Convert.ToInt64(comboBox1.SelectedValue);
                    MyProperty.setIntVector(intList, count);
                    break;

                default:
                    break;
            }
        }
        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex >= 0)
            {
                checkType();

            }
        }

        //public enum Type { Integer = 1, Float }
        //[Browsable(true), DisplayName("Data Type")]
        //public Type TextBoxType { get; set; } = Type.Integer;
    }
}
