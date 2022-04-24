namespace AroxInstaller
{
    internal class HttpClientWrapper : IDisposable
    {
        private readonly HttpClient _httpClient;

        public HttpClientWrapper(string uriString)
        {
            _httpClient = new()
            {
                BaseAddress = new Uri(uriString),
            };
        }

        public void setUri(Uri uri)
        {
            _httpClient.BaseAddress = uri;
        }

        public void setHeader(string name, string value)
        {
            _httpClient.DefaultRequestHeaders.Add(name, value);
        }

        public async Task downloadFileAsync(string filename)
        {
            var bytes = await _httpClient.GetByteArrayAsync("");
            await File.WriteAllBytesAsync(filename, bytes);
        }

        public void Dispose()
        {
            _httpClient.Dispose();
        }
    }
}
