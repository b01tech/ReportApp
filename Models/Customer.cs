using System.ComponentModel.DataAnnotations;

namespace ReportApp.Models;

internal class Customer
{
    [Key]
    public int CustomerId { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string State { get; set; }

    public Customer()
    {
        
    }
}
