﻿using System;
using System.Collections.Generic;

namespace CI_PLATFORM.Models;

public partial class PasswordReset
{
    public string? Email { get; set; }

    public string? Token { get; set; }

    public DateTime CreatedAt { get; set; }

    public long Id { get; set; }
}