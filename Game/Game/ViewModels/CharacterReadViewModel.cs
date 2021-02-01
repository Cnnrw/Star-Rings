using System;
using System.Linq;
using System.Collections.Generic;

using Xamarin.Forms;

using Game.Models;
using Game.Views;
using Game.GameRules;

namespace Game.ViewModels
{
    /// <summary>
    /// Character Read View Model
    /// Manages the list of data records
    /// </summary>
    public class CharacterReadViewModel : BaseViewModel<CharacterModel>
    {
        public CharacterModel Character { get; set; }


        public CharacterReadViewModel(CharacterModel character = null)
        {
            Title = character?.Name;
            Character = character;
        }

    }
}