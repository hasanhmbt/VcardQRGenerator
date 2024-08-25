using Microsoft.AspNetCore.Mvc;
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
            _context.Vcards.Add(model);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        return View(model);
    }

    private string GenerateVCardText(Vcard model)
    {
        return $@"
        BEGIN:VCARD
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

