using System.ComponentModel.DataAnnotations;

namespace EntityModel
{
    [MetadataType(typeof(BlogMetadata))]
    public partial class BlogMD
    {
    }

    [MetadataType(typeof(PostMetadata))]
    public partial class PostMD
    {
    }
}
