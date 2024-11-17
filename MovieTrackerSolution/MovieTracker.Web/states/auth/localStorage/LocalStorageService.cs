using Blazored.LocalStorage;
using System.Text;
using System.Text.Json;

namespace MovieTracker.Web.states.auth.localStorage
{
    public static class LocalStorageService
    {
        public static async Task SaveItemEncryptedAsync<T>(this ILocalStorageService localStorageService, string key, T item)
        {
            var itemJson = JsonSerializer.Serialize(item); // json string from object
            var itemJsonBytes = Encoding.UTF8.GetBytes(itemJson); // convert to bytes
            var base64Json = Convert.ToBase64String(itemJsonBytes); // convert bytes to base64 string
            await localStorageService.SetItemAsync(key, base64Json); // set key-value to local storage
        }

        public static async Task<T> ReadEncryptedItemAsync<T>(this ILocalStorageService localStorageService, string key)
        {
            var base64Json = await localStorageService.GetItemAsync<string>(key);
            var itemJsonBytes = Convert.FromBase64String(base64Json);
            var itemJson = Encoding.UTF8.GetString(itemJsonBytes);
            var item = JsonSerializer.Deserialize<T>(itemJson);
            return item;
        }


    }
}
