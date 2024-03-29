﻿using System.ComponentModel.DataAnnotations.Schema;

namespace StammPhoenix.Persistence.Models;

[Table("PAGE_CONTACT")]
public class PageContact : Entity
{
    [Column("NAME")]
    public string Name { get; internal set; }

    [Column("PHONE_NUMBER")]
    public string? PhoneNumber { get; internal set; }

    [Column("ADDRESS_CITY")]
    public string? AddressCity { get; internal set; }
    
    [Column("ADDRESS_STREET")]
    public string? AddressStreet { get; internal set; }
}
