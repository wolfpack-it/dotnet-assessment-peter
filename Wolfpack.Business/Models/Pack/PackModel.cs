namespace Wolfpack.Business.Models.Pack;

public class PackForModificationModel
{
    public string Name { get; set; } = null!;
}

public class PackForCreationModel : PackForModificationModel
{
}

public class PackForUpdateModel : PackForModificationModel
{
}

public class PackModel : PackForModificationModel
{
    public Guid Id { get; set; }
}