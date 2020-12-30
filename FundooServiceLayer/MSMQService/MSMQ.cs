using Experimental.System.Messaging;
using FundooServiceLayer.EmailService;
using System;
using System.Collections.Generic;
using System.Text;

namespace FundooServiceLayer.MSMQService
{
    public class MSMQ:IMSMQ
    {
        private readonly MessageQueue messageQueue = new MessageQueue();
        private IEmailSender _emailSender;
        public MSMQ(IEmailSender emailsender)
        {
            _emailSender = emailsender;
            this.messageQueue.Path = @".\private$\FundooApp";
            if (MessageQueue.Exists(this.messageQueue.Path))
            {
            }
            else
            {
                MessageQueue.Create(this.messageQueue.Path);
            }
        }
        /// <summary>
        /// Adds to queue.
        /// </summary>
        /// <param name="message">The message.</param>
        public void AddToQueue(string message)
        {
            this.messageQueue.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });

            this.messageQueue.ReceiveCompleted += this.ReceiveFromQueue;

            this.messageQueue.Send(message);

            this.messageQueue.BeginReceive();

            this.messageQueue.Close();
        }

        /// <summary>
        /// Method to fetch message from MSMQ.
        /// </summary>
        /// <param name="sender"></param>
        /// <returns></returns>
        public void ReceiveFromQueue(object sender, ReceiveCompletedEventArgs e)
        {
            try
            {
                var msg = this.messageQueue.EndReceive(e.AsyncResult);

                string data = msg.Body.ToString();
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"C:\Users\com\Desktop\FundooUsingAsp.net\FundooNotes\FundooServiceLayer\MSMQService\MSMQText.txt", true))
                {
                    file.WriteLine(data);
                }

                this.messageQueue.BeginReceive();
            }
            catch (MessageQueueException qexception)
            {
                Console.WriteLine(qexception);
            }
        }
    }
}
