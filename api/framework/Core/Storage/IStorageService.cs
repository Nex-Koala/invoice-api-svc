﻿using NexKoala.Framework.Core.Storage.File;
using NexKoala.Framework.Core.Storage.File.Features;

namespace NexKoala.Framework.Core.Storage;

public interface IStorageService
{
    public Task<Uri> UploadAsync<T>(FileUploadCommand? request, FileType supportedFileType, CancellationToken cancellationToken = default)
    where T : class;

    public void Remove(Uri? path);
}
