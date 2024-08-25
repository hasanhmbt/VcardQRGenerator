using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VcardQRGenerator.Data;
using VcardQRGenerator.Models;

namespace VcardQRGenerator.Controllers;
public class VCardController : Controller
{
    private readonly VcardDbContext _context;
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly HttpClient _httpClient;

    public VCardController(VcardDbContext context, IWebHostEnvironment webHostEnvironment, HttpClient httpClient)
    {
        _context = context;
        _webHostEnvironment = webHostEnvironment;
        _httpClient = httpClient;
    }

    public IActionResult Index()
    {

        return View(_context.Vcards.ToList());
    }



    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }




    [HttpPost]
    public async Task<IActionResult> Create(Vcard model)
    {
        if (ModelState.IsValid)
        {
            string qrCodeText = GenerateVCardText(model);
            string fileName = await SaveQrCodeImageAsync(qrCodeText);
            model.QRCodeUrl = $"/images/{fileName}";
            _context.Vcards.Add(model);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        return View(model);
    }



    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        var vcard = await _context.Vcards.FindAsync(id);

        if (vcard == null)
        {
            return NotFound();
        }

        return View(vcard);
    }


    [HttpPost]
    public async Task<IActionResult> Edit(int id, Vcard model)
    {
        if (id != model.Id)
        {
            return NotFound();
        }
        if (ModelState.IsValid)
        {
            try
            {
                string qrCodeText = GenerateVCardText(model);
                string fileName = await SaveQrCodeImageAsync(qrCodeText);
                model.QRCodeUrl = $"/images/{fileName}";
                _context.Update(model);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Vcards.Any(e => e.Id == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction("Index");
        }
        return View(model);
    }




    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        var vcard = await _context.Vcards.FirstOrDefaultAsync(m => m.Id == id);
        if (vcard == null)
        {
            return NotFound();
        }
        return View(vcard);
    }







    //Formatting VCard Text
    private string GenerateVCardText(Vcard model)
    {
        return $@"BEGIN:VCARD
VERSION:3.0
N:{model.LastName};{model.FirstName}
FN:{model.FirstName} {model.LastName}
ORG:{model.Company}
TITLE:{model.JobTitle}
TEL;TYPE=CELL:{model.Mobile}
TEL;TYPE=WORK,VOICE:{model.Phone}
TEL;TYPE=FAX:{model.Fax}
ADR;TYPE=WORK:;;{model.Street};{model.City};{model.State};{model.Zip};{model.Country}
EMAIL:{model.Email}
URL:{model.Website}
END:VCARD".Trim();
    }


    //Image Upload Method
    private async Task<string> SaveQrCodeImageAsync(string qrCodeText)
    {
        string qrCodeUrl = $"https://quickchart.io/qr?text={Uri.EscapeDataString(qrCodeText)}&size=300";

        byte[] imageBytes = await _httpClient.GetByteArrayAsync(qrCodeUrl);
        string fileName = $"{Guid.NewGuid()}.png";
        string folderPath = Path.Combine(_webHostEnvironment.WebRootPath, "images");
        Directory.CreateDirectory(folderPath);
        string filePath = Path.Combine(folderPath, fileName);
        await System.IO.File.WriteAllBytesAsync(filePath, imageBytes);
        return fileName;
    }


}

