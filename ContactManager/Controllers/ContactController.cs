using Microsoft.AspNetCore.Mvc;
using ContactManager.Models;
using ContactManager.Services;

[Route("api/[controller]")]
[ApiController]
public class ContactController : Controller
{
    private ContactRepository contactRepository;
    public ContactController()
    {
        this.contactRepository = new ContactRepository();
    }

    [HttpGet]
    public Contact[] Get()
    {
        return contactRepository.GetAllContacts();
    }

    [HttpGet("{id}")]
    public Contact Get(long id)
    {
        return contactRepository.GetContactById(id);
    }

    [HttpPost]
    public IActionResult Post(Contact contact)
    {
        bool saved = contactRepository.SaveContact(contact);

        if (saved)
        {
            return Ok(contact);
        }

        return StatusCode(500);
    }

    [HttpPut]
    public IActionResult Put(Contact contact)
    {
        bool updated = contactRepository.UpdateContact(contact);

        if (updated)
        {
            return Ok(contact);
        }

        return NotFound();
    }
}