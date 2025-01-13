using webAPI.Models;

public class Dietitian
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public int Age { get; set; }
    public string UserId { get; set; }
    public ApplicationUser User { get; set; }
}
