using System;
using System.Collections.Generic;
using System.Text;

namespace BS.Core
{
    public enum AccountType
    {
        Client,
        Management,
        Employee,
        SubContractor
    }

    public enum CategoryType
    {
        Product,
        Service
    }

    public enum Gender
    {
        Male,
        Female,
        Child,
        Everyone
    }

    public enum OfficeStatus
    {
        Open,
        Closed
    }

    public enum AppointmentStatus
    {
        Reserved,
        InProgress,
        Complete,
        Invoinced
    }

    public enum OrderStatus
    {
        Pending,
        Cancelled,
        InProcess,
        Processed,
        Complete,
        All
    }

    public enum ContentType
    {
        Header,
        Text,
        Image,
        Link,
        Space,
        Row
    }

    public enum ContentContainment
    {
        Boxed,
        Full
    }

    public enum OrderType
    {
        Product,
        Service
    }

    public enum InvoiceStatus
    {
        Pending, 
        Paid
    }
}
