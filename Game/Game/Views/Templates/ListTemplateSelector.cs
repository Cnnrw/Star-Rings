using Game.Models;
using Game.Templates.Data;
using Game.Views.Templates.Data;

using Xamarin.Forms;

namespace Game.Templates
{
    public class ListTemplateSelector : DataTemplateSelector
    {
        DataTemplate _item;
        DataTemplate _character;
        DataTemplate _monster;

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
                ItemModel _      => _item ??= new DataTemplate(typeof(ItemCell)),
                CharacterModel _ => _character ??= new DataTemplate(typeof(CharacterCell)),
                MonsterModel _   => _monster ??= new DataTemplate(typeof(MonsterCell)),
                _                => null
            };
    }
}
