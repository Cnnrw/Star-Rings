using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

using Game.Enums;
using Game.Models;

using Newtonsoft.Json.Linq;

namespace Game.Helpers
{
    /// <summary>
    /// Helps Parse the Json from the Server into Items
    /// </summary>
    public static class ItemModelJsonHelper
    {

        /// <summary>
        /// ParseJson takes the raw stirng and parses it into valid ItemModel
        ///
        /// The returned data will be a list of items.  Need to pull that list out
        /// </summary>
        /// <param name="myJsonData"></param>
        /// <returns></returns>
        public static List<ItemModel> ParseJson(string myJsonData)
        {
            var myData = new List<ItemModel>();

            try
            {
                var json = JObject.Parse(myJsonData);

                // Data is a List of Items, so need to pull them out one by one...

                var myTempList = json["ItemList"]?.ToObject<List<JObject>>();

                myData.AddRange(myTempList!.Select(ConvertFromJson)
                                           .Where(myTempObject => myTempObject != null));

                return myData;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// Converts a single object that is a json string into a single ItemModel
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static ItemModel ConvertFromJson(JObject json)
        {
            var myData = new ItemModel
            {
                Name = JsonHelper.GetJsonString(json, "Name"),
                Guid = JsonHelper.GetJsonString(json, "Guid"),
                Id = JsonHelper.GetJsonString(json, "Guid"),
                Description = JsonHelper.GetJsonString(json, "Description"),
                ImageURI = JsonHelper.GetJsonString(json, "ImageURI"),
                Value = JsonHelper.GetJsonInteger(json, "Value"),
                Range = JsonHelper.GetJsonInteger(json, "Range"),
                Damage = JsonHelper.GetJsonInteger(json, "Damage"),
                Location = (ItemLocationEnum)JsonHelper.GetJsonInteger(json, "Location"),
                Attribute = (AttributeEnum)JsonHelper.GetJsonInteger(json, "Attribute"),
                Category = (ItemCategories)JsonHelper.GetJsonInteger(json, "Category"),
                IsConsumable = JsonHelper.GetJsonBool(json, "IsConsumable")
            };

            return myData;
        }
    }
}
