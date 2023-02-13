using System;

namespace Ooievaar.Domain;

public record Release 
{
    public Guid Id { get; set; }
    public string Description { get; set; }
    public int VersionNumber { get; set; }
}