using ContactsApp.Model;

namespace ContactsApp.View;

public class ContactsUI
{
    private readonly Contacts _contacts;
   

    public ContactsUI()
    {
        _contacts = new Contacts();
    }

    public void Menu(string filePath)
    {
        bool running = true;
        while (running)
        {
            Console.Clear();
            displayMenu();
            string input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    ShowContacts(filePath);
                    Console.WriteLine("\nPress Enter to return to the menu...");
                    Console.ReadLine();
                    break;
                case "2":
                    AddContact(filePath);
                    Console.WriteLine("\nPress Enter to return to the menu...");
                    Console.ReadLine();
                    break;
                case "3":
                    UpdateContact(filePath);
                    Console.WriteLine("\nPress Enter to return to the menu...");
                    Console.ReadLine();
                    break;
                case "4":
                    DeleteContact(filePath);
                    Console.WriteLine("\nPress Enter to return to the menu...");
                    Console.ReadLine();
                    break;
                    
                case "5":
                    running = false;
                    break;
                default:
                    Console.WriteLine("Invalid option. Please select a number between 1 and 5.");
                    Console.ReadLine();
                    break;
            }
            
        }

    }
    public void displayMenu()
    {
        Console.WriteLine("1. Show All Contacts");
        Console.WriteLine("2. Add Contact");
        Console.WriteLine("3. Edit Contact");
        Console.WriteLine("4. Delete Contact");
        Console.WriteLine("5. Exit");
    }
    public void ShowContacts(string filePath)
    {
        _contacts.LoadContacts(filePath);
        
        foreach (Contact contact in _contacts.ContactsList)
        {
            Console.WriteLine($"{contact.FirstName} {contact.LastName} - {contact.Email} - {contact.PhoneNumber}");
            
        }
    }

    public void AddContact(string filePath)
    {
        Console.WriteLine("Enter First Name: ");
        string firstName = Console.ReadLine();
        
        Console.Write("Enter Last Name: ");
        string lastName = Console.ReadLine();
        Console.Write("Enter Email: ");
        string email = Console.ReadLine();
        Console.Write("Enter Phone Number: ");
        string phone = Console.ReadLine();
        
        if (_contacts.AddContact(firstName, lastName, email, phone, filePath))
        {
            Console.WriteLine("Contact added successfully!");
        }
        else
        {
            Console.WriteLine("Invalid input. Please try again.");
        }
        
        
    }

    public void DeleteContact(string filePath)
    {
        Console.WriteLine("Please enter the email address of the contact you'd like to remove:");
        string email = Console.ReadLine();
         ;
        if (_contacts.DeleteContact(email, filePath))
        {
            Console.WriteLine("Contact deleted");
        }
        else
        {
            Console.WriteLine("Contact not found");
        }
    }

    public void UpdateContact(string filePath)
    {
        Console.WriteLine("Please enter the email address of the contact you'd like to edit:");
        string chosenEmail = Console.ReadLine();
        
        Console.WriteLine("Enter First Name: ");
        string firstName = Console.ReadLine();
        Console.Write("Enter Last Name: ");
        string lastName = Console.ReadLine();
        Console.Write("Enter Email: ");
        string email = Console.ReadLine();
        Console.Write("Enter Phone Number: ");
        string phone = Console.ReadLine();

        if ( _contacts.UpdateContact(chosenEmail, firstName, lastName, email, phone, filePath))
        {
            Console.WriteLine("Contact updated");
        }
        else
        {
            Console.WriteLine("Contact not found");
        }
        
    }
}