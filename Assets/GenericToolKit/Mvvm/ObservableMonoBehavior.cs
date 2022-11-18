using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

#nullable enable
namespace GenericToolKit.Mvvm
{
    public abstract class ObservableMonoBehavior : MonoBehaviour, INotifyPropertyChanged, INotifyPropertyChanging, IDisposable
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public event PropertyChangingEventHandler? PropertyChanging;

        public virtual void Dispose()
        {
            PropertyChanged = null;
            PropertyChanging = null;
        }

        protected bool SetProperty<TValue>(ref TValue oldValue, TValue newValue,
            [CallerMemberName] string? propertyName = null)
        {
            if (EqualityComparer<TValue>.Default.Equals(oldValue, newValue))
                return false;

            PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));
            oldValue = newValue;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

            return true;
        }

        protected bool SetProperty<TValue>(ref TValue oldValue, TValue newValue,
            IEqualityComparer<TValue> comparer, [CallerMemberName] string? propertyName = null)
        {
            if (comparer == null)
                throw new ArgumentNullException("Comparer cannot be null.");

            if (comparer.Equals(oldValue, newValue))
                return false;

            PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));
            oldValue = newValue;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

            return true;
        }

        protected bool SetProperty<TModel, TValue>(TValue oldValue, TValue newValue,
            TModel model, Action<TModel, TValue> callBack, [CallerMemberName] string? propertyName = null)
        {
            if (model == null)
                throw new ArgumentNullException("Model cannot be null.");

            if (EqualityComparer<TValue>.Default.Equals(oldValue, newValue))
                return false;

            PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));
            callBack.Invoke(model, newValue);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

            return true;
        }

        protected bool SetProperty<TModel, TValue>(TValue oldValue, TValue newValue, TModel model,
            IEqualityComparer<TValue> comparer, Action<TModel, TValue> callBack, [CallerMemberName] string? propertyName = null)
        {
            if (model == null)
                throw new ArgumentNullException("Model cannot be null.");

            if (comparer == null)
                throw new ArgumentNullException("Comparer cannot be null.");

            if (EqualityComparer<TValue>.Default.Equals(oldValue, newValue))
                return false;

            PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));
            callBack.Invoke(model, newValue);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

            return true;
        }
    }

}