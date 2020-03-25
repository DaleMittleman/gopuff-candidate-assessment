namespace Microservice.CartManager.Extensions
{
    using System;
    using System.Text;
    using System.Text.Json;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Caching.Distributed;

    /// <summary>
    /// Extensions on the distributed cache interface for working with objects.
    /// </summary>
    public static class DistributedCacheExtensions
    {
        /// <summary>
        /// Gets an object value from the cache.
        /// </summary>
        /// <typeparam name="T">The object type.</typeparam>
        /// <param name="cache">The cache.</param>
        /// <param name="key">The cache key.</param>
        /// <returns>The object value.</returns>
        public static T Get<T>(
            this IDistributedCache cache,
            string key)
        {
            if (cache == null)
            {
                throw new ArgumentNullException(nameof(cache));
            }

            var bytes = cache.Get(key);

            if (bytes == null)
            {
                return default;
            }
            else
            {
                var serialized = Encoding.UTF8.GetString(bytes);
                return JsonSerializer.Deserialize<T>(serialized);
            }
        }

        /// <summary>
        /// Sets an object value in the cache.
        /// </summary>
        /// <typeparam name="T">The object type.</typeparam>
        /// <param name="cache">The cache.</param>
        /// <param name="key">The cache key.</param>
        /// <param name="value">The object value.</param>
        /// <param name="entryOptions">The entry expiration options.</param>
        public static void Set<T>(
            this IDistributedCache cache,
            string key,
            T value,
            DistributedCacheEntryOptions entryOptions = null)
        {
            if (cache == null)
            {
                throw new ArgumentNullException(nameof(cache));
            }

            if (entryOptions == null)
            {
                entryOptions = new DistributedCacheEntryOptions();
            }

            var serialized = JsonSerializer.Serialize(value);
            var bytes = Encoding.UTF8.GetBytes(serialized);

            cache.Set(key, bytes, entryOptions);
        }

        /// <summary>
        /// Gets an object value from the cache asynchronously.
        /// </summary>
        /// <typeparam name="T">The object type.</typeparam>
        /// <param name="cache">The cache.</param>
        /// <param name="key">The cache key.</param>
        /// <param name="token">Optional. The <see cref="T:System.Threading.CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>The object value.</returns>
        public static async Task<T> GetAsync<T>(
            this IDistributedCache cache,
            string key,
            CancellationToken token = default)
        {
            if (cache == null)
            {
                throw new ArgumentNullException(nameof(cache));
            }

            var bytes = await cache.GetAsync(key, token);

            if (bytes == null)
            {
                return default;
            }
            else
            {
                var serialized = Encoding.UTF8.GetString(bytes);
                return JsonSerializer.Deserialize<T>(serialized);
            }
        }

        /// <summary>
        /// Sets an object value in the cache.
        /// </summary>
        /// <typeparam name="T">The object type.</typeparam>
        /// <param name="cache">The cache.</param>
        /// <param name="key">The cache key.</param>
        /// <param name="value">The object value.</param>
        /// <param name="entryOptions">The entry expiration options.</param>
        /// <param name="token">Optional. The <see cref="T:System.Threading.CancellationToken" /> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>A <see cref="Task"/>.</returns>
        public static async Task SetAsync<T>(
            this IDistributedCache cache,
            string key,
            T value,
            DistributedCacheEntryOptions entryOptions = null,
            CancellationToken token = default)
        {
            if (cache == null)
            {
                throw new ArgumentNullException(nameof(cache));
            }

            if (entryOptions == null)
            {
                entryOptions = new DistributedCacheEntryOptions();
            }

            var serialized = JsonSerializer.Serialize(value);
            var bytes = Encoding.UTF8.GetBytes(serialized);

            await cache.SetAsync(key, bytes, entryOptions, token);
        }
    }
}
