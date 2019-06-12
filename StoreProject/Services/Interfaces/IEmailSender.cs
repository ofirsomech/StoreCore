using StoreProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreProject.Services.Interfaces
{
    public interface IEmailSender
    {
        bool SendEmail(User user);

    }
}
