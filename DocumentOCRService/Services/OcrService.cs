using Tesseract;

namespace DocumentOCRService.Services
{
    public class OcrService: IOcrService
    {
        private readonly string _tessDataPath;

        public OcrService()
        {
            _tessDataPath = Path.Combine(Directory.GetCurrentDirectory(), "tessdata");
        }

        public async Task<string> ReadTextFromImage(string imagePath)
        {
            if (!File.Exists(imagePath))
                throw new FileNotFoundException("Image file not found.", imagePath);

            using var engine = new TesseractEngine(_tessDataPath, "eng", EngineMode.Default);
            using var img = Pix.LoadFromFile(imagePath);
            using var page = engine.Process(img);
            return await Task.Run(()=> page.GetText());
        }

        public async Task<string> ReadTextFromStreamAsync(Stream imageStream)
        {
            var tempFilePath = Path.GetTempFileName();
            await using var fs = File.Create(tempFilePath);
            await imageStream.CopyToAsync(fs);
            fs.Close(); 

            var result = ReadTextFromImage(tempFilePath);

            File.Delete(tempFilePath);
            return await Task.Run(()=> result);
        }
    }
}
