using Microsoft.AspNetCore.Identity;

public class AppRole : IdentityRole
{
    public DateTime Created { get; set; }
    public DateTime Updated { get; set; }
} 