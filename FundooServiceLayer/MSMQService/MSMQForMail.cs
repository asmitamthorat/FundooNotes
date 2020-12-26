using System;
using Experimental.System.Messaging;
using System.Collections.Generic;
using System.Text;
using FundooServiceLayer.EmailService;
using Newtonsoft.Json;

namespace FundooServiceLayer.MSMQService
{
    public class MSMQForMail:IMSMQForMail
    {
        private readonly MessageQueue messageQueue = new MessageQueue();
        private IEmailSender _emailSender;
        public MSMQForMail(IEmailSender emailsender)
        {
            _emailSender = emailsender;
            this.messageQueue.Path = @".\private$\FundooApplication";
            if (MessageQueue.Exists(this.messageQueue.Path))
            {
                this.messageQueue = new MessageQueue(this.messageQueue.Path);
            }
            else
            {
                MessageQueue.Create(this.messageQueue.Path);
            }
        }

        public void AddToQueue(EmailService.Message message)
        {
            string mailInfo = JsonConvert.SerializeObject(message);

            this.messageQueue.Formatter = new BinaryMessageFormatter();

            this.messageQueue.ReceiveCompleted += this.ReceiveFromQueue;

            this.messageQueue.Send(mailInfo);

            this.messageQueue.BeginReceive();

            this.messageQueue.Close();
        }

        public void ReceiveFromQueue(object sender, ReceiveCompletedEventArgs e)
        {
            try
            {
                var msg = this.messageQueue.EndReceive(e.AsyncResult);

                EmailService.Message message = JsonConvert.DeserializeObject<EmailService.Message>(msg.Body.ToString());

                // Process the logic be sending the message

                // Restart the asynchronous receive operation.
                _emailSender.SendEmail(message);

                this.messageQueue.BeginReceive();
            }
            catch (MessageQueueException qexception)
            {
                Console.WriteLine(qexception);
            }
        }

    }
}
