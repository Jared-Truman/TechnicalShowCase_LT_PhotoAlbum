using CommandLine;

namespace LT_PhotoAlbum.Arguments
{
    [Verb("photo", HelpText = "Get all available photos for a given album.")]
    public class PhotoArgument
    {
        [Option('i', "AlbumId", HelpText = "Filter on a given album id.", Required = true)]
        public int AlbumId { get; set; }

        [Option('p', "PhotoId", HelpText = "Filter on a optional photo id.", Required = false)]
        public int? PhotoId { get; set; }
    }
}
