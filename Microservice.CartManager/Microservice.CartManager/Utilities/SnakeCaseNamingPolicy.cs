namespace Microservice.CartManager.Utilities
{
    using System.Text.Json;
    using Newtonsoft.Json.Serialization;

    /// <summary>
    /// Sets up a snake case naming policy for JSON de/serialization for System.Text.Json.
    /// </summary>
    public class SnakeCaseNamingPolicy : JsonNamingPolicy
    {
        private static SnakeCaseNamingPolicy instance = null;

        private readonly SnakeCaseNamingStrategy namingStrategy = new SnakeCaseNamingStrategy();

        private SnakeCaseNamingPolicy()
        {
        }

        /// <summary>
        /// Gets the public singleton instance of the naming policy.
        /// </summary>
        public static SnakeCaseNamingPolicy Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SnakeCaseNamingPolicy();
                }

                return instance;
            }
        }

        /// <inheritdoc/>
        public override string ConvertName(string name)
        {
            return this.namingStrategy.GetPropertyName(name, false);
        }
    }
}
