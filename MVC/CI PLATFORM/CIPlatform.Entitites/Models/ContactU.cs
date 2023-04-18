using System;
using System.Collections.Generic;

namespace CIPlatform.Entitites.Models;

public partial class ContactU
{
    public long Id { get; set; }

    public long UserId { get; set; }

    public string Email { get; set; } = null!;

    public string Subject { get; set; } = null!;

    public string Message { get; set; } = null!;

    public string Name { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
