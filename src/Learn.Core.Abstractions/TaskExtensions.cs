namespace System.Threading.Tasks
{
    public static class TaskExtensions
    {
        /// <inheritdoc cref="Task.FromResult{TResult}(TResult)"/>
        public static Task<TResult> ToTask<TResult>(this TResult value)
        {
            return Task.FromResult(value);
        }
    }
}