namespace LT_PhotoAlbum.Abstractions
{
    public interface IConsoleWrapper
    {
        public void WriteLine(string message);
        public string ReadLine();
    }
}
