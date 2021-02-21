#nullable enable
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Game.Components
{
    /// <summary>
    /// Observable object with INotifyPropertyChanged implemented
    /// </summary>
    public class ObservableObject : INotifyPropertyChanged
    {

        /// <summary>
        /// Occurs when property changed.
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Sets the property.
        /// </summary>
        /// <returns><c>true</c>, if property was set, <c>false</c> otherwise.</returns>
        /// <param name="backingStore">Backing store.</param>
        /// <param name="value">Value.</param>
        /// <param name="propertyName">Validates value.</param>
        /// <param name="onChanged">Property name.</param>
        /// <param name="validateValue">On changed.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        protected virtual bool SetProperty<T>(ref T                     backingStore,
                                              T                         value,
                                              [CallerMemberName] string propertyName  = "",
                                              Action?                   onChanged     = null,
                                              Func<T, T, bool>?         validateValue = null)
        {
            // If value didn't change
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            // If value changed but didn't validate
            if (validateValue != null && !validateValue(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        /// <summary>
        /// Raises the property changed event.
        /// </summary>
        /// <param name="propertyName"></param>
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}