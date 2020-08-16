using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MyChatApp.Client.Services
{
    public interface IUserHttpClientFactory
    {
        Task<HttpClient> CreateClientAsync();
    }
}
