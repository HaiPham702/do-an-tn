using ElectronicLibrary.Core.Entity;
using ElectronicLibrary.Core.Interface.Repository;
using ElectronicLibrary.Core.Interface.Service;
using ElectronicLibrary.Core.Models;
using ElectronicLibrary.Core.Resources;
using Ghostscript.NET;
using Microsoft.AspNetCore.Mvc;

namespace ElectronicLibrary.Controllers
{
    public class BookController : BaseController<Book>
    {
        private readonly IBookService _service;
        private readonly IBookRepo _repo;

        private readonly IWebHostEnvironment _webHostEnvironment;

        public BookController(IBookRepo repo, IBookService service, IWebHostEnvironment webHostEnvironment) : base(repo, service)
        {
            _service = service;
            _repo = repo;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpPost("UploadFile")]
        public IActionResult UploadPdf([FromForm] IFormFile file, IFormFile? imageCover)
        {
            try
            {
                // Kiểm tra file có phải là PDF hay không
                if (file.ContentType != "application/pdf")
                {
                    return BadRequest("File không phải là PDF.");
                }



                // Đường dẫn đến thư mục để lưu trữ file PDF
                var uploadFolder = Path.Combine(_webHostEnvironment.ContentRootPath, "BookFile");

                // Tạo thư mục nếu nó không tồn tại
                if (!Directory.Exists(uploadFolder))
                {
                    Directory.CreateDirectory(uploadFolder);
                }

                var fileExtension = Path.GetExtension(file.FileName);

                var fileId = Guid.NewGuid();

                var fileName = $"{fileId}{fileExtension}";

                // Tạo đường dẫn lưu trữ của file PDF
                var filePath = Path.Combine(uploadFolder, fileName);

                // Lưu file PDF vào thư mục
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                var fileCoverExtension = imageCover != null ? Path.GetExtension(imageCover.FileName) : ".jpeg";


                string imageCoverName = $"{fileId}{fileCoverExtension}";
                var ImgFolderPath = Path.Combine(uploadFolder, imageCoverName);

                if (imageCover == null)
                {

                    var info = new System.IO.FileInfo(ImgFolderPath);
                    if (info.Exists.Equals(false))
                    {
                        GhostscriptPngDevice img = new GhostscriptPngDevice(GhostscriptPngDeviceType.Png16m);
                        img.GraphicsAlphaBits = GhostscriptImageDeviceAlphaBits.V_4;
                        img.TextAlphaBits = GhostscriptImageDeviceAlphaBits.V_4;
                        img.ResolutionXY = new GhostscriptImageDeviceResolution(200, 200);
                        img.InputFiles.Add(filePath);
                        img.Pdf.FirstPage = 1;
                        img.Pdf.LastPage = 1;
                        img.PostScript = string.Empty;
                        img.OutputPath = ImgFolderPath;
                        img.Process();
                    }
                }
                else
                {
                    // Lưu file PDF vào thư mục
                    using (var stream = new FileStream(ImgFolderPath, FileMode.Create))
                    {
                        imageCover.CopyTo(stream);
                    }
                }

                return Ok(new
                {
                    fileId,
                    fileName,
                    fileNameUser = file.FileName,
                    imageCoverName
                });
            }
            catch (Exception ex)
            {
                var res = new ErrorEntity(MessegeResources.ErrorMessage, ex.Message);
                StatusCode(500, res);
                throw;
            }
        }
    }

}
