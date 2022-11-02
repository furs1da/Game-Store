namespace GameStore.TagHelpers
{
    public static class Nav
    {
        public static string Active(string value, string current)
        {
            return value == current ? "active" : "";
        }
    }
}
