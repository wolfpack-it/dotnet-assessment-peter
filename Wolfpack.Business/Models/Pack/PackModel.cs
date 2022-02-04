namespace Wolfpack.Business.Models.Pack
{
    public class PackForModificationModel
    {
        public string Name { get; set; } = null!;

        public float Latitude { get; set; }

        public float Longitude { get; set; }
    }

    public class PackForCreationModel : PackForModificationModel
    {

    }

    public class PackModel : PackForModificationModel
    {
        public Guid Id { get; set; }
    }
}
