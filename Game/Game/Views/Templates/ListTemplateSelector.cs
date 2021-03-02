using Game.Models;
using Game.Views.Templates.Data;

using Xamarin.Forms;

namespace Game.Views.Templates
{
    public class ListTemplateSelector : DataTemplateSelector
    {
        DataTemplate _character;

        /// <summary>
        /// Matches an item by its base model and returns the data template that
        /// corresponds to that model.
        /// </summary>
        /// <param name="item"></param>
        /// <param name="container"></param>
        /// <returns></returns>
        protected override DataTemplate OnSelectTemplate(object item, BindableObject container) =>
            item switch
            {
                CharacterModel _ => _character ??= new DataTemplate(typeof(CharacterCell)),
                _                => null
            };
    }
}
