using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Tom4u.Toolkit.WpfControls.Properties;

namespace Tom4u.Toolkit.WpfControls.Common
{
    public abstract class AbstractViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private readonly Dictionary<string, object> properties = new Dictionary<string, object>();

        protected T GetValue<T>(T defaultValue = default, [CallerMemberName] string property = "")
        {
            if (properties.ContainsKey(property))
                return (T) properties[property];

            SetValue(defaultValue, property);

            return defaultValue;
        }

        protected void SetValue<T>(T value, [CallerMemberName] string property = "")
        {
            properties[property] = value;
            OnPropertyChanged(property);
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
