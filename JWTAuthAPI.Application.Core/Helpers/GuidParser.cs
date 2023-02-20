namespace JWTAuthAPI.Application.Core.Helpers
{
    public static class GuidParser
    {
        public static Guid Parse(string? input)
        {
            bool isValid = Guid.TryParse(input, out Guid result);
            return isValid ? result : throw new ArgumentException("Invalid Guid Id");
        }
    }
}
