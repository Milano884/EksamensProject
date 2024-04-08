namespace ZBC.Exceptions
{
    public static class Extensions
    {
        public static void Update<T>(this List<T> list, Action<T> updateAction)
        {
            foreach (var obj in list)
            {
                updateAction(obj);
            }
        }
    }
}
