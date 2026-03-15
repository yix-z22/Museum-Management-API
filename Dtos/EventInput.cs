using System.ComponentModel.DataAnnotations;

public class EventInput
{
    [Required]
    public string start { get; set; }
    [Required]
    public string end { get; set; }
    public string summary { get; set; }
    public string description { get; set; }
    [Required]
    public string location { get; set; }
}