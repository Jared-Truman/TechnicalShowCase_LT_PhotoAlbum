using System.Threading.Tasks;

namespace LT_PhotoAlbum.Abstractions
{
    public interface IConsoleWriter<TArgument>
    {
        public Task WriteLineAsync(TArgument argument);
    }
}
