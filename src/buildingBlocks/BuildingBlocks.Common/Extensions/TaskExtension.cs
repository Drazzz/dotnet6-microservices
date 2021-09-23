namespace BuildingBlocks.Common.Extensions
{
    public static class TaskExtension
    {
        public static Task<IReadOnlyCollection<T>> ToReadOnlyCollection<T>(this Task<List<T>> asyncList)
            => asyncList.ContinueWith(x => x.Result as IReadOnlyCollection<T>);
        
    }
}
