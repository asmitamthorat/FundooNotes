using MimeKit;
using System;
using System.Collections.Generic;
using System.Text;

namespace FundooServiceLayer.EmailService
{
    public interface IEmailSender
    {
        void Send(MimeMessage email);
        void SendEmail(Message message);
    }
}
