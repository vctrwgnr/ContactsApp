using ContactsApp.Model;
using ContactsApp.View;

namespace ContactsApp;

class Program
{
    static void Main(string[] args)
    {
        string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "Data", "contacts.json");
        ContactsUI view = new ContactsUI();
        view.Menu(filePath);

    }
}