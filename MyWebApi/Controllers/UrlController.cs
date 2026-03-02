using Microsoft.AspNetCore.Mvc;
using System.Collections.Concurrent;

namespace MyWebApi.Controllers
{
    [ApiController]
    [Route("url")]
    public class UrlController : ControllerBase
    {
        // simple in‑memory store
        private static readonly ConcurrentDictionary<string,string> _urls = new();

        [HttpPost("shorten")]
        public IActionResult Shorten([FromBody] string url)
        {
            if (_urls.TryGetValue(url, out var existing))
                return Ok(existing);

            var shortCode = Guid.NewGuid().ToString("N").Substring(0, 8);
            var shortened = $"https://short.url/{shortCode}";
            _urls[url] = shortened;
            return Ok(shortened);
        }

        [HttpGet("expand")]
        public IActionResult Expand([FromQuery] string shortenedUrl)
        {
            var original = _urls.FirstOrDefault(kvp => kvp.Value == shortenedUrl).Key;
            if (original is not null)
                return Ok(original);

            return NotFound("Shortened URL not found.");
        }
    }
}