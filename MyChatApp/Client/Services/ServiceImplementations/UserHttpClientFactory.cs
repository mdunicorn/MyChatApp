using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MyChatApp.Client.Services.ServiceImplementations
{
    public class UserHttpClientFactory : IUserHttpClientFactory
    {
        IHttpClientFactory _factory;
        IUserIdentificationService _userIdentificationService;

        public UserHttpClientFactory(IHttpClientFactory factory, IUserIdentificationService userIdentificationService)
        {
            _factory = factory;
            _userIdentificationService = userIdentificationService;
        }

        public async Task<HttpClient> CreateClientAsync()
        {
            if (await _userIdentificationService.IsAuthenticatedAsync())
                return _factory.CreateClient(HttpClientHelper.AuthenticatedClientName);
            else
                return _factory.CreateClient(HttpClientHelper.UnauthenticatedClientName);
        }
    }
}
