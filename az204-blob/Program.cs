using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

Console.WriteLine("Azure Blob Storage exercise\n");

// Run asynchronously, wait for the results before proceeding
ProcessAsync().GetAwaiter().GetResult();

Console.WriteLine("Press enter to exit the sample application.");
Console.ReadLine();

static async Task ProcessAsync(){

    string storageConnectionString = "CONNECTION STRING";

    // Auth connection string
    BlobServiceClient blobServiceClient = new BlobServiceClient(storageConnectionString);

    // Unique container name
    string containerName = "wtblob" + Guid.NewGuid().ToString();

    // Create container and return container client object
    BlobContainerClient containerClient = await blobServiceClient.CreateBlobContainerAsync(containerName);
    Console.WriteLine("A container named '" + containerName + "' has been created. " +
        "\nTake a minute and verify in the portal." + 
        "\nNext a file will be created and uploaded to the container.");
    Console.WriteLine("Press 'Enter' to continue.");
    Console.ReadLine();

    // Create  file in the data directory for uploading and downloading
    string localPath = "./data/";
    string fileName = "wtfile" + Guid.NewGuid().ToString() + ".txt";
    string localFilePath = Path.Combine(localPath, fileName);

    // Write text to file
    await File.WriteAllTextAsync(localFilePath, "Hello, World!");

    // Get blob client
    BlobClient blobClient = containerClient.GetBlobClient(fileName);

    Console.WriteLine("Uploading to Blob storage as blob:\n\t {0}\n", blobClient.Uri);

    // Open file and upload data to blob async
    using (FileStream uploadFileStream = File.OpenRead(localFilePath))
        {
    await blobClient.UploadAsync(uploadFileStream);
    uploadFileStream.Close();
        }

    Console.WriteLine("\nThe file was uploaded. We'll verify by listing" + 
            " the blobs next.");
    Console.WriteLine("Press 'Enter' to continue.");
    Console.ReadLine();

    // List blobs in the container
    Console.WriteLine("Listing blobs...");
    await foreach (BlobItem blobItem in containerClient.GetBlobsAsync())
    {
        Console.WriteLine("\t" + blobItem.Name);
    }

    Console.WriteLine("\nYou can also verify by looking inside the " + 
            "container in the portal." +
            "\nNext the blob will be downloaded with an altered file name.");
    Console.WriteLine("Press 'Enter' to continue.");
    Console.ReadLine();

    // Download blob to a local file
    // Append string "DOWNLOADED" before .txt 
    string downloadFilePath = localFilePath.Replace(".txt", "DOWNLOADED.txt");

    Console.WriteLine("\nDownloading blob to\n\t{0}\n", downloadFilePath);

    // Download  blob's contents and save to file
    BlobDownloadInfo download = await blobClient.DownloadAsync();

    using (FileStream downloadFileStream = File.OpenWrite(downloadFilePath))
    {
        await download.Content.CopyToAsync(downloadFileStream);
    }
    Console.WriteLine("\nLocate the local file in the data directory created earlier to verify it was downloaded.");
    Console.WriteLine("The next step is to delete the container and local files.");
    Console.WriteLine("Press 'Enter' to continue.");
    Console.ReadLine();
    
    // Delete the container and clean up local files created
    Console.WriteLine("\n\nDeleting blob container...");
    await containerClient.DeleteAsync();

    Console.WriteLine("Deleting the local source and downloaded files...");
    File.Delete(localFilePath);
    File.Delete(downloadFilePath);

    Console.WriteLine("Finished cleaning up.");

}
