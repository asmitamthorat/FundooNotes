using System;
using System.Collections.Generic;
using System.Text;

namespace FundooServiceLayer.MSMQService
{
    public interface IMSMQ
    {
         void AddToQueue(string message);
    }
}
