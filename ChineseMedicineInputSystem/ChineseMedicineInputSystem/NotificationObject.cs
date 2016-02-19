using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;

namespace ChineseMedicineInputSystem
{
    public abstract class NotificationObject : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raises the <see cref="PropertyChanged"/> event.
        /// </summary>
        /// <param name="propertyName">The changed property name.</param>
        protected void OnPropertyChanged(string propertyName)
        {
            if (null != PropertyChanged)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        /// <summary>
        /// Raises the <see cref="PropertyChanged"/> event using expression.
        /// </summary>
        /// <typeparam name="TP">Expression</typeparam>
        /// <param name="property">Property</param>
        protected void OnPropertyChanged<T>(Expression<Func<T>> property)
        {
            var propertyInfo = (property.Body as MemberExpression).Member as PropertyInfo;

            if (null == propertyInfo)
            {
                throw new ArgumentException("The lambda expression 'property' should point to a valid Property");
            }

            OnPropertyChanged(propertyInfo.Name);
        }

        #endregion
    }
}
