﻿using Game.Models;
using Game.Services;

namespace Game.ViewModels
{
    public class GenericViewModel<T> : BaseViewModel
        where T : class
    {
        #region Ctors

        /// <summary>
        ///     Empty GenericViewModel constructor
        /// </summary>
        public GenericViewModel() : this(null, App.NavigationService)
        { }

        /// <summary>
        ///     Constructor takes an existing item and sets
        ///     the Title for the page to match the text of data
        /// </summary>
        /// <param name="data"></param>
        public GenericViewModel(T data) : this(data, App.NavigationService)
        { }

        GenericViewModel(T data, INavigationService navigationService) : base(navigationService)
        {
            if (data != null)
            {
                Title = (data as BaseModel<T>)?.Name;
            }
            Data = data;
        }

        #endregion
        #region Properties

        /// <summary>
        ///     Data being bound to
        /// </summary>
        private T BindingData { get; set; }

        public T Data
        {
            get => BindingData;
            set
            {
                var data = BindingData;
                SetProperty(ref data, value);
                BindingData = data;
            }
        }

        #endregion
    }
}
