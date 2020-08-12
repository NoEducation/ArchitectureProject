using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ArchitectureProject.Domain.Dto
{
    public class LoggedToken
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
