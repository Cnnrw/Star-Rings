using System;
using System.Threading.Tasks;
using System.Windows.Input;

using Game.Models;
using Game.Services;
using Game.Views;

using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace Game.ViewModels
{
    public class GenericViewModel<T> : BaseViewModel
        where T : class
    {
        public ICommand UpdateCommand { get; } =
            new AsyncCommand<GenericViewModel<T>>(OnUpdateClicked);

        public ICommand DeleteCommand { get; } =
            new AsyncCommand<GenericViewModel<T>>(OnDeleteClicked);

        static async Task OnUpdateClicked(GenericViewModel<T> viewModel)
        {
            await (viewModel switch
                   {
                       GenericViewModel<CharacterModel> model => NavigationService.NavigateModalAsync(nameof(CharacterUpdatePage), model),
                       GenericViewModel<MonsterModel> model   => NavigationService.NavigateModalAsync(nameof(MonsterUpdatePage), model),
                       GenericViewModel<ItemModel> model      => NavigationService.NavigateModalAsync(nameof(ItemUpdatePage), model),
                       GenericViewModel<ScoreModel> model     => NavigationService.NavigateModalAsync(nameof(ScoreUpdatePage), model),
                       _                                      => throw new ArgumentOutOfRangeException(nameof(viewModel))
                   });
            await Application.Current.MainPage.Navigation.PopAsync();
        }

        static async Task OnDeleteClicked(GenericViewModel<T> viewModel)
        {
            await (viewModel switch
                   {
                       GenericViewModel<CharacterModel> m => NavigationService.NavigateModalAsync(nameof(CharacterDeletePage), m),
                       GenericViewModel<MonsterModel> m   => NavigationService.NavigateModalAsync(nameof(MonsterDeletePage), m),
                       GenericViewModel<ItemModel> m      => NavigationService.NavigateModalAsync(nameof(ItemDeletePage), m),
                       GenericViewModel<ScoreModel> m     => NavigationService.NavigateModalAsync(nameof(ScoreDeletePage), m),
                       _                                  => throw new ArgumentOutOfRangeException(nameof(viewModel))
                   });
            await Application.Current.MainPage.Navigation.PopAsync();
        }

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
        {
        }

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
