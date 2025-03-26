using System.Text.Json;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Serialization;

namespace ContactsApp.Model;

public class Contacts
{
    private List<Contact> _contacts = new List<Contact>();
    public List<Contact> ContactsList { get => _contacts; }


    public bool AddContact(string firstName, string lastName, string email, string phoneNumber, string filePath)
    {
        LoadContacts(filePath);
        if (!ValidateContact(firstName, lastName, email, phoneNumber))
        {
            return false;
        }
        _contacts.Add(new Contact() { FirstName = firstName, LastName = lastName, Email = email, PhoneNumber = phoneNumber });
        SaveContact(filePath);
        return true;
    }

    public void SaveContact(string filePath)
    {
        string json = JsonSerializer.Serialize(_contacts, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(filePath, json);
    }

    public void LoadContacts(string filePath)
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            _contacts = JsonSerializer.Deserialize<List<Contact>>(json) ?? new List<Contact>();
        }
    }

    public bool DeleteContact(string email, string filePath)
    {
        LoadContacts(filePath);
        email = email.Trim();
        Contact contactToRemove = null;
        foreach (Contact contact in _contacts)
        {
            if (contact.Email == email)
            {
                contactToRemove = contact;
                break; 
            }
        }

        if (contactToRemove != null)
        {
            _contacts.Remove(contactToRemove);
            SaveContact(filePath);
            return true;
        }
        return false;
    }

    public bool UpdateContact(string chosenEmail, string firstName, string lastName, string email, string phoneNumber, string filePath)
    {
        LoadContacts(filePath);
        chosenEmail = chosenEmail.Trim();
        Contact contactToUpdate = null;
        foreach (Contact contact in _contacts)
        {
            if (contact.Email == chosenEmail)
            {
                contactToUpdate = contact;
                break; 
            }
        }
        if (contactToUpdate == null || !ValidateContact(firstName, lastName, email, phoneNumber))
        {
            return false;
        }
        
        
        contactToUpdate.FirstName = firstName;
        contactToUpdate.LastName = lastName;
        contactToUpdate.Email = email;
        contactToUpdate.PhoneNumber = phoneNumber;
        SaveContact(filePath);
        return true;
    }


    private bool ValidateContact(string firstName, string lastName, string email, string phoneNumber)
    {
        return !string.IsNullOrWhiteSpace(firstName) &&
               !string.IsNullOrWhiteSpace(lastName) &&
               Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$") &&
               Regex.IsMatch(phoneNumber, @"^\+?\d{10,15}$");
    }

}