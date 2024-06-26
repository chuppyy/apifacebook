﻿using System;
using NCore.Modals;

namespace ITC.Domain.Core.ModelShare.CompanyManager.CustomerManagers;

public class CustomerManagerEventModel : PublishModal
{
    public Guid   Id          { get; set; }
    public string Name        { get; set; }
    public string Description { get; set; }
    public string Phone       { get; set; }
    public string Address     { get; set; }
    public string Email       { get; set; }
}