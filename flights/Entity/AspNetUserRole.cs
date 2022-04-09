﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace flights.Entity
{
    [Keyless]
    [Index(nameof(RoleId), Name = "IX_AspNetUserRoles_RoleId")]
    public partial class AspNetUserRole
    {
        [Required]
        [StringLength(450)]
        public string UserId { get; set; }
        [Required]
        public string RoleId { get; set; }

        [ForeignKey(nameof(RoleId))]
        public virtual AspNetRole Role { get; set; }
        [ForeignKey(nameof(UserId))]
        public virtual AspNetUser User { get; set; }
    }
}