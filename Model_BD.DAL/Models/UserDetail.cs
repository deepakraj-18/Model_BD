﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace Model_BD.DAL.Models;

public partial class UserDetail
{
    public long Id { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Address { get; set; }

    public string MobileNo { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }

    public long? RoleId { get; set; }

    public long? CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public long? ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public bool? IsDeleted { get; set; }

    public long? DeletedBy { get; set; }

    public string Username { get; set; }

    public long? AgentId { get; set; }

    public virtual RoleMaster Role { get; set; }
}