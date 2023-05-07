using System;
using System.Threading.Tasks;

namespace VersOne.Epub.Utils
{
    internal static class TaskExtensionMethods
    {
        public static T ExecuteAndUnwrapAggregateException<T>(this Task<T> task)
        {
            try
            {
                return task.Result;
            }
            catch (AggregateException aggregateException)
            {
                throw aggregateException.InnerException;
            }
        }
    }
}
