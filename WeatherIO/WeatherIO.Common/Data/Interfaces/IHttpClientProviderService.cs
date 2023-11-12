using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherIO.Common.Data.Interfaces
{
    public interface IHttpClientProviderService
    {
        public Task<HttpResponseMessage> GetAsync(string url);
    }
}
