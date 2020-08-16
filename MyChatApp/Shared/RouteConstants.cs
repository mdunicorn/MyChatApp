using System;
using System.Collections.Generic;
using System.Text;

namespace MyChatApp.Shared
{
    public static class RouteConstants
    {
        public const string Messages = "/api/messages";
        public const string Recent = "recent";
        public const string RecentMessages = Messages + "/" + Recent;
    }
}
