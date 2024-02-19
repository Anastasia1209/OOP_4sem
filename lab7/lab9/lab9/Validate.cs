using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace lab9
{
    public class Validate: DependencyObject
    {
        public static readonly DependencyProperty NumberProperty;

        static Validate()
        {
            FrameworkPropertyMetadata metadata = new FrameworkPropertyMetadata();
            metadata.CoerceValueCallback = new CoerceValueCallback(CorrectValue);

            NumberProperty = DependencyProperty.Register("Number", typeof(int), typeof(Validate), metadata, new ValidateValueCallback(ValidateValue));
        }

        private static object CorrectValue(DependencyObject dependencyObject, object value)
        {
            int currentValue = (int)value;
            return (currentValue > 100000) ? 100000 : currentValue;
            
        }

        public static bool ValidateValue(object value)
        {
            int currentValue = (int)value;
            return (currentValue >= 0);
                
        }

        public int Number
        {
            get { return (int)GetValue(NumberProperty); }
            set { SetValue(NumberProperty, value); }
        }
    }
}
