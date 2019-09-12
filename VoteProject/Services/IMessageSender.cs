using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VoteProject.Services
{
    public interface IMessageSender
    {
        void Send(string FIO,string Tel,string message, string messageAlt);
    }
}
