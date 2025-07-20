using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using NexKoala.WebApi.Invoice.Application.Dtos.EInvoice.Code;
using NexKoala.WebApi.Invoice.Application.Interfaces;

namespace NexKoala.WebApi.Invoice.Infrastructure.Services;

public class MsicService : IMsicService
{
    private Dictionary<string, string> _msicDict = new();
    private readonly string _msicPath = "msic.json";

    public async Task InitializeAsync()
    {
        string basePath = AppContext.BaseDirectory;
        string filePath = Path.GetFullPath(Path.Combine(basePath, _msicPath));

        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException($"MSIC file not found at path: {filePath}");
        }

        var json = await File.ReadAllTextAsync(filePath);
        var list = JsonSerializer.Deserialize<List<MsicSubCategoryCode>>(json);

        if (list == null || list.Count == 0)
        {
            throw new Exception("MSIC file is empty or malformed.");
        }

        // Remove duplicates by Code (if multiple with same Code, keep first one)
        _msicDict = list
            .GroupBy(x => x.Code)
            .ToDictionary(g => g.Key, g => g.First().Description);
    }

    public string GetDescription(string code) =>
        _msicDict.TryGetValue(code, out var description)
            ? description
            : $"Unknown MSIC code: {code}";
}
