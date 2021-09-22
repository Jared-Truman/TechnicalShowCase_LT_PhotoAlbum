using CommandLine;

namespace LT_PhotoAlbum.Arguments
{
    [Verb("album", HelpText = "Get available albums.")]
    public class AlbumArgument
    {
        [Option('i', "AlbumId", HelpText = "Filter on a optional album id.")]
        public int? AlbumId { get; set; }
    }
}
