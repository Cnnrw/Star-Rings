using Game.Models;

using Xamarin.Forms;

namespace Game.Views
{
    public class ListTemplateSelector : DataTemplateSelector
    {
        DataTemplate _item;
        DataTemplate _character;
        DataTemplate _monster;
        DataTemplate _score;

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
                ScoreModel _     => _score ??= new DataTemplate(typeof(ScoreCell)),
                _                => null
            };
    }
}
