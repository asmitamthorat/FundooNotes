using System;
using System.Collections.Generic;
using System.Text;

namespace FundooServiceLayer.MSMQService
{
    public interface IMSMQForMail
    {
        void AddToQueue(EmailService.Message message);
    }
}
