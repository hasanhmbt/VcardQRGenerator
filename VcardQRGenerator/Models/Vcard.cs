using System.ComponentModel.DataAnnotations;

namespace VcardQRGenerator.Models;

public class Vcard
{

    public int Id { get; set; }
    [Required]
    [Display(Name = "First Name")]
    public string FirstName { get; set; }
    [Required]
    [Display(Name = "Last Name")]
    public string LastName { get; set; }
    [Display(Name = "Mobile")]
    public string Mobile { get; set; }
    [Display(Name = "Phone")]
    public string Phone { get; set; }
    [Display(Name = "Fax")]
    public string Fax { get; set; }
    [Required]
    [EmailAddress]
    [Display(Name = "Email")]
    public string Email { get; set; }
    [Display(Name = "Company")]
    public string Company { get; set; }
    [Display(Name = "Job Title")]
    public string JobTitle { get; set; }
    [Display(Name = "Street")]
    public string Street { get; set; }
    [Display(Name = "City")]
    public string City { get; set; }
    [Display(Name = "State")]
    public string State { get; set; }
    [Display(Name = "ZIP")]
    public string Zip { get; set; }
    [Display(Name = "Country")]
    public string Country { get; set; }
    [Display(Name = "Website")]
    public string Website { get; set; }
    public string QRCodeUrl { get; set; }
}

