using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modul12PR
{

    public class PropertyEventArgs : EventArgs
    {
        public string PropertyName { get; }

        public PropertyEventArgs(string propertyName)
        {
            PropertyName = propertyName;
        }
    }

    public delegate void PropertyEventHandler(object sender, PropertyEventArgs e);

    public interface IPropertyChanged
    {
        event PropertyEventHandler PropertyChanged;
    }

    public class MyClass : IPropertyChanged
    {
        private int myProperty;

        public int MyProperty
        {
            get => myProperty;
            set
            {
                if (myProperty != value)
                {
                    myProperty = value;
                    OnPropertyChange(nameof(MyProperty));
                }
            }
        }

        public event PropertyEventHandler PropertyChanged;

        protected virtual void OnPropertyChange(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyEventArgs(propertyName));
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            MyClass myObject = new MyClass();
            myObject.PropertyChanged += OnMyPropertyChanged;

            myObject.MyProperty = 10;
        }

        static void OnMyPropertyChanged(object sender, PropertyEventArgs e)
        {
            Console.WriteLine($"Property '{e.PropertyName}' changed");
        }
    }
}

