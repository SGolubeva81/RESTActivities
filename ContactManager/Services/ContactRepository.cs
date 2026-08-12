using ContactManager.Models;
using Newtonsoft.Json;

namespace ContactManager.Services
{
    public class ContactRepository
    {
        private string databaseFileName = "database.json";
        private List<Contact> _contacts = new List<Contact>();
        public ContactRepository()
        {
            string jsonData = File.ReadAllText(databaseFileName);
            _contacts = JsonConvert.DeserializeObject<List<Contact>>(jsonData) ?? new List<Contact>();
        }
        public Contact[] GetAllContacts()
        {
            return _contacts.ToArray();
        }

        public Contact GetContactById(long id)
        {
            Contact[] contacts = GetAllContacts();

            foreach (Contact contact in contacts)
            {
                if (contact.Id == id)
                {
                    return contact;
                }
            }

            return null;
        }

        public bool SaveContact(Contact contact)
        {
            try
            {
                _contacts.Add(contact);

                string jsonData = JsonConvert.SerializeObject(_contacts, Formatting.Indented);

                File.WriteAllText(databaseFileName, jsonData);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool UpdateContact(Contact updatedContact)
        {
            try
            {
                Contact existingContact = _contacts.FirstOrDefault(
                    contact => contact.Id == updatedContact.Id
                );

                if (existingContact == null)
                {
                    return false;
                }

                existingContact.Name = updatedContact.Name;

                string jsonData = JsonConvert.SerializeObject(
                    _contacts,
                    Formatting.Indented
                );

                File.WriteAllText(databaseFileName, jsonData);

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}