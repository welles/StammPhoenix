namespace StammPhoenix.Web.Models.Kontakt;

public class KontaktModel
{
    public KontaktModel(string name, string phoneNumber)
    {
        this.Name = name;
        this.PhoneNumber = phoneNumber;
    }

    public string Name { get; }

    public string PhoneNumber { get; }
}
