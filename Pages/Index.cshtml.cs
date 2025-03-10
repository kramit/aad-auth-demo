global using Microsoft.AspNetCore.Mvc;
global using Microsoft.AspNetCore.Mvc.RazorPages;

using Microsoft.AspNetCore.Authorization;
using Microsoft.Graph;
using Microsoft.Identity.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Graph.Models;

namespace az_auth_demo.Pages
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly GraphServiceClient _graphServiceClient;
        public User? UserProfile { get; private set; }

        public IndexModel(GraphServiceClient graphServiceClient)
        {
            _graphServiceClient = graphServiceClient;
        }

        public async Task OnGetAsync()
        {
            UserProfile = await _graphServiceClient.Me.GetAsync();
        }
    }
}
