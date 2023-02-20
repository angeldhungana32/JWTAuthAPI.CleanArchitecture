namespace JWTAuthAPI.Application.Core.Helpers
{
    public static class RouteConstants
    {
        public const string DefaultControllerRoutev1 = "api/v1/[controller]";

        // Accounts
        public const string Login = "Login";
        public const string Register = "Register";
        public const string GetUser = "Users/{id}";
        public const string GetAllUsers = "Users";
        public const string UpdateUser = "Users/{id}";
        public const string DeleteUser = "Users/{id}";

        // Products
        public const string AddProduct = "";
        public const string GetProduct = "{id}";
        public const string GetAllProductsByUserId = "Users/{id}";
        public const string UpdateProduct = "{id}";
        public const string DeleteProduct = "{id}";
    }

    public class ConfigurationSectionKeyConstants
    {
        public const string JWT = "JWT";
        public const string Roles = "Roles";
        public const string Admin = "AdminCredentials";
        public const string Swagger = "Swagger";
        public const string DBConnectionString = "DefaultConnection";
        public const string UseInMemoryDB = "UseInMemoryDatabase";
        public const string DBName = "JWTAuthAPIDb";
    }
}
