using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MiddleWareIIsChange.Service
{
    public interface ISampleService
    {
        Task ProcessItemsAsync();
    }

    public class SampleClass : ISampleService
    {
        private readonly ILogger<SampleClass> _logger;

        public SampleClass(ILogger<SampleClass> logger)
        {
            _logger = logger;
        }

        public async Task ProcessItemsAsync()
        {
            List<string> items = new List<string> { "Item1", "Item2", "Item3" };

            foreach (var item in items)
            {
                _logger.LogInformation("Processing item: {Item}", item);
                await Task.Delay(1000); // Simulate some processing time
            }

            _logger.LogInformation("Task completed.");
        }
    }
}