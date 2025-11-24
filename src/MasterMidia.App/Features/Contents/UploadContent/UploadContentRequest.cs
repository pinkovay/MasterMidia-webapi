using System;
using MasterMidia.App.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace MasterMidia.App.Features.Contents.UploadContent;

public class UploadContentRequest
{
    public IFormFile File { get; set; } = default!;
    public string Title { get; set; } = string.Empty;
    public MediaType MediaType { get; set; }
}
