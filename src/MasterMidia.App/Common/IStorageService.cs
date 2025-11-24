using System;
using Microsoft.AspNetCore.Http;

namespace MasterMidia.App.Common;

public interface IStorageService
{
    Task<string> UploadFileAsync(IFormFile file, string destinationPath);
}
