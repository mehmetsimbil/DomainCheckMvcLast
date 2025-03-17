using Business.Abstracts;
using Business.Requests.Domain;
using Business.Responses.Domain;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;

namespace WEB.Controllers
{
    public class DomainController : Controller
    {
        private readonly IDomainService _domainService;

        public DomainController(IDomainService domainService)
        {
            _domainService = domainService;
        }

        public IActionResult Index(GetDomainListRequest request)
        {
            ViewData["Title"] = "Domain";
            GetDomainListResponse result = _domainService.GetList(request);
            var model = result.Items;
            return View(model);
        }

        public async Task<IActionResult> OnComing(GetDomainListRequest request)
        {
            ViewData["Title"] = "Yaklaşan Domainler";
            GetDomainListResponse result = await _domainService.GetListMin90Days(request); 
            var model = result.Items;
            return View(model);
        }


        [HttpPost]
        public IActionResult Add(AddDomainRequest request)
        {
            AddDomainResponse result = _domainService.Add(request);
            return RedirectToAction("Index", "Domain");
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetById(GetDomainByIdRequest request) {

            GetDomainByIdResponse result = _domainService.GetById(request);
            var updateRequest = new UpdateDomainRequest
            {
                Id = request.Id,
            };
            return View("UpdateDomain", updateRequest);
        }

        [HttpPost]
        public IActionResult Update(UpdateDomainRequest request)
        {
            try
            {
                UpdateDomainResponse result = _domainService.Update(request);
                TempData["SuccessMessage"] = "Domain başarıyla güncellendi.";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Güncelleme sırasında hata oluştu: " + ex.Message;
            }

            return RedirectToAction("Index");
        }


        public IActionResult Delete(DeleteDomainRequest request)
        {
            DeleteDomainResponse result = _domainService.Delete(request);
            return RedirectToAction("Index");
        }
        //public async Task<IActionResult> ExportDomains()
        //{
        //    var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Uploads");
        //    var filePath = Path.Combine(directoryPath, "Domains.xlsx");

        //    // Dosya yolunun çıktısını konsola yazdıralım
        //    Console.WriteLine($"Dosya yolu: {filePath}");

        //    // Dosyanın doğru dizinde ve mevcut olduğuna dair kontrol
        //    if (!System.IO.File.Exists(filePath))
        //    {
        //        Console.WriteLine($"Dosya mevcut değil. Yol: {filePath}");
        //        return NotFound("Dosya bulunamadı.");
        //    }

        //    var fileBytes = await System.IO.File.ReadAllBytesAsync(filePath);
        //    var fileName = "Domains.xlsx";

        //    return File(fileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        //}

        public ActionResult ExcelToExport()
        {
            var abouts = _domainService.GetListToExcel();
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Domain");
                worksheet.Cells[1, 1].Value = "Id";
                worksheet.Cells[1, 2].Value = "Domain Adı";
                worksheet.Cells[1, 3].Value = "Alınan Site";
                worksheet.Cells[1, 4].Value = "Bitiş Tarihi";

                for (int i = 0; i < abouts.Count; i++)
                {

                    worksheet.Cells[i + 2, 1].Value = abouts[i].Id;
                    worksheet.Cells[i + 2, 2].Value = abouts[i].DomainName;
                    worksheet.Cells[i + 2, 3].Value = abouts[i].BuyedDomainSite;
                    worksheet.Cells[i + 2, 4].Value = abouts[i].EndTime.ToString("yyyy-MM-dd"); 
                }

                var stream = new MemoryStream();
                package.SaveAs(stream);
                stream.Position = 0;

                var fileName = "Domains.xlsx";
                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
            }
        }

        public ActionResult ExcelToExportLast15Days()
        {
            var abouts = _domainService.GetListToExcelLast15Days();
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Domain");
                worksheet.Cells[1, 1].Value = "Id";
                worksheet.Cells[1, 2].Value = "Domain Adı";
                worksheet.Cells[1, 3].Value = "Alınan Site";
                worksheet.Cells[1, 4].Value = "Bitiş Tarihi";

                for (int i = 0; i < abouts.Count; i++)
                {

                    worksheet.Cells[i + 2, 1].Value = abouts[i].Id;
                    worksheet.Cells[i + 2, 2].Value = abouts[i].DomainName;
                    worksheet.Cells[i + 2, 3].Value = abouts[i].BuyedDomainSite;
                    worksheet.Cells[i + 2, 4].Value = abouts[i].EndTime.ToString("yyyy-MM-dd");
                }

                var stream = new MemoryStream();
                package.SaveAs(stream);
                stream.Position = 0;

                var fileName = "Domains.xlsx";
                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
            }
        }


        [HttpPost]
        public async Task<IActionResult> ImportDomains(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                TempData["ErrorMessage"] = "Lütfen bir Excel dosyası yükleyin.";
                return RedirectToAction("Index"); 
            }

            var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");
            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }

            var filePath = Path.Combine(uploadPath, file.FileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            try
            {
                await _domainService.ImportDomainsAsync(filePath);
                TempData["SuccessMessage"] = "Domainler başarıyla içe aktarıldı.";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"İçe aktarma sırasında bir hata oluştu: {ex.Message}";
            }

            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult DownloadTemplate()
        {
            var stream = new MemoryStream();
            using (var package = new ExcelPackage(stream))
            {
                var worksheet = package.Workbook.Worksheets.Add("DomainTemplate");

                worksheet.Cells[1, 1].Value = "Domain Adı";
                worksheet.Cells[1, 2].Value = "Domain Alınan Site";
                worksheet.Cells[1, 3].Value = "Bitiş Tarihi";

                package.Save();
            }

            stream.Position = 0; 
            var fileName = "DomainTemplate.xlsx";
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        }

    }
} 
