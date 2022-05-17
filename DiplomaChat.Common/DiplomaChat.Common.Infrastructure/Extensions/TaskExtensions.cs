namespace DiplomaChat.Common.Infrastructure.Extensions
{
    public static class TaskExtensions
    {
        public static async Task<TResult> ThenSelect<TResult, TTaskResult>(
            this Task<TTaskResult> task,
            Func<TTaskResult, TResult> selector)
        {
            var taskResult = await task;

            return selector(taskResult);
        }

        public static async Task<IEnumerable<TResult>> ThenSelect<TResult, TTaskResult>(
            this Task<IEnumerable<TTaskResult>> task,
            Func<TTaskResult, TResult> selector)
        {
            var taskResult = await task;

            return taskResult.Select(selector);
        }

        public static async Task<TResult[]> ThenSelect<TResult, TTaskResult>(
            this Task<TTaskResult[]> task,
            Func<TTaskResult, TResult> selector)
        {
            var taskResult = await task;

            return taskResult.Select(selector).ToArray();
        }

        public static async Task<List<TResult>> ThenSelect<TResult, TTaskResult>(
            this Task<List<TTaskResult>> task,
            Func<TTaskResult, TResult> selector)
        {
            var taskResult = await task;

            return taskResult.Select(selector).ToList();
        }

        public static async Task<TResult[]> ThenSelectArray<TResult, TTaskResult>(
            this Task<IEnumerable<TTaskResult>> task,
            Func<TTaskResult, TResult> selector)
        {
            var selected = await task.ThenSelect(selector);

            return selected.ToArray();
        }

        public static async Task<List<TResult>> ThenSelectList<TResult, TTaskResult>(
            this Task<IEnumerable<TTaskResult>> task,
            Func<TTaskResult, TResult> selector)
        {
            var selected = await task.ThenSelect(selector);

            return selected.ToList();
        }
    }
}
