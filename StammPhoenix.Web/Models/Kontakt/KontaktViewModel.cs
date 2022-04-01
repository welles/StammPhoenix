namespace StammPhoenix.Web.Models.Kontakt;

public class KontaktViewModel
{
    public KontaktViewModel(KontaktModel[] contacts)
    {
        this.Contacts = contacts;
    }

    public KontaktModel[] Contacts { get; }
}
