namespace BuildingBlocks.Common.Extensions
{
    public static class ObjectExtension
    {
        public static bool CompareObjects(this object? x, object? y)
        {
            if (ReferenceEquals(x, y))
                return true;
            var xType = x?.GetType();
            var yType = y?.GetType();
            if (xType != yType)
                return false;

            foreach (var prop in xType.GetProperties())
            {
                if (prop.PropertyType == typeof(DateTime?))
                    continue;
                if (prop.PropertyType == typeof(string) && prop.GetValue(x, null) != prop.GetValue(y, null))
                    return false;
                if (!prop.GetValue(x, null).CompareObjects(prop.GetValue(y, null)))
                    return false;
            }
             
            return true;
        }
    }
}
