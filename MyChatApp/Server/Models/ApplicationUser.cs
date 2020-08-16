using Microsoft.AspNetCore.Identity;
using MyChatApp.Server.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyChatApp.Server.Models
{
    public class ApplicationUser : IdentityUser, IEntity<string>
    {
    }
}
