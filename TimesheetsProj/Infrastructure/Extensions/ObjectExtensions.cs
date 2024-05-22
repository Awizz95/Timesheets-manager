namespace TimesheetsProj.Infrastructure.Extensions
{
    public static class ObjectExtensions
    {
        public static void EnsureNotNull(this object @object, string name)
        {
            if (@object == null)
            {
                throw new ArgumentNullException(name, $"Parameter {name} cannot be null.");
            }
        }
    }
}
