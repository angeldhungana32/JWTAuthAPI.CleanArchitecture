namespace JWTAuthAPI.Application.Core.Helpers
{
    public static class RoutesConstant 
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
}
