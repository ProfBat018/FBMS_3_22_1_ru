// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System.ComponentModel.DataAnnotations;

namespace RazorAreas.Areas.Identity.Data.DTOs;

public class ResendEmailInputModel
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }
}
