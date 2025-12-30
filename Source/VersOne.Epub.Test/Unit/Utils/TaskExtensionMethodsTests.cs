using VersOne.Epub.Utils;

namespace VersOne.Epub.Test.Unit.Utils
{
    public class TaskExtensionMethodsTests
    {
        [Fact(DisplayName = "ExecuteAndUnwrapAggregateException should return the task result if it doesn't throw any exceptions")]
        public void ExecuteAndUnwrapAggregateExceptionWithNoExceptionTest()
        {
            Task<int> task = Task.Run(() => 1);
            Assert.Equal(1, task.ExecuteAndUnwrapAggregateException());
        }

        [Fact(DisplayName = "ExecuteAndUnwrapAggregateException should rethrow the exception thrown by the task")]
        public void ExecuteAndUnwrapAggregateExceptionWithInnerExceptionTest()
        {
            static async Task<int> TestAsyncFunction(bool throwException)
            {
                if (throwException)
                {
                    throw new InvalidOperationException();
                }
                return 1;
            }
            Assert.Throws<InvalidOperationException>(() => TestAsyncFunction(throwException: true).ExecuteAndUnwrapAggregateException());
        }
    }
}
