using System.Threading.Tasks;

namespace Dotnet
{    
    public class Tasks
    {
        public async Task<decimal> AddWithDelayAsync(decimal x, decimal y, int millisecondsDelay) {
            await Task.Delay(millisecondsDelay);
            return x + y;
        }
    }
}