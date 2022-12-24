using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace GenericToolKit.Mvvm
{
	public abstract class ObservableObject : INotifyPropertyChanged, INotifyPropertyChanging, IDisposable
	{
		public event PropertyChangedEventHandler? PropertyChanged;

		public event PropertyChangingEventHandler? PropertyChanging;

		public virtual void Dispose()
		{
			this.PropertyChanged = null;
			this.PropertyChanging = null;
		}

		protected bool SetProperty<TValue>(ref TValue oldValue, TValue newValue, [CallerMemberName] string? propertyName = null)
		{
			if (EqualityComparer<TValue>.Default.Equals(oldValue, newValue))
			{
				return false;
			}
			this.PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));
			oldValue = newValue;
			this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
			return true;
		}

		protected bool SetProperty<TValue>(ref TValue oldValue, TValue newValue, IEqualityComparer<TValue> comparer, [CallerMemberName] string? propertyName = null)
		{
			if (comparer == null)
			{
				throw new ArgumentNullException("Comparer cannot be null.");
			}
			if (comparer.Equals(oldValue, newValue))
			{
				return false;
			}
			this.PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));
			oldValue = newValue;
			this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
			return true;
		}

		protected bool SetProperty<TModel, TValue>(TValue oldValue, TValue newValue, TModel model, Action<TModel, TValue> callBack, [CallerMemberName] string? propertyName = null)
		{
			if (model == null)
			{
				throw new ArgumentNullException("Model cannot be null.");
			}
			if (EqualityComparer<TValue>.Default.Equals(oldValue, newValue))
			{
				return false;
			}
			this.PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));
			callBack(model, newValue);
			this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
			return true;
		}

		protected bool SetProperty<TModel, TValue>(TValue oldValue, TValue newValue, TModel model, IEqualityComparer<TValue> comparer, Action<TModel, TValue> callBack, [CallerMemberName] string? propertyName = null)
		{
			if (model == null)
			{
				throw new ArgumentNullException("Model cannot be null.");
			}
			if (comparer == null)
			{
				throw new ArgumentNullException("Comparer cannot be null.");
			}
			if (EqualityComparer<TValue>.Default.Equals(oldValue, newValue))
			{
				return false;
			}
			this.PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));
			callBack(model, newValue);
			this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
			return true;
		}
	}
}