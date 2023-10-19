// © Copyright (c) Renet Consulting, Inc. All right reserved.
// Licensed under the MIT.

using Azure;
using Azure.Communication.Email;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Sdk.Communication.Azure
{
    public class SmtpClient : ISmtp
    {
        private readonly EmailClient _emailClient;
        private readonly ILogger<SmtpClient> _logger;
        private readonly AzureCommunicationSettings _settings;
        public SmtpClient(ILogger<SmtpClient> logger, EmailClient emailClient, IOptions<AzureCommunicationSettings> options)
        {
            _logger = logger;
            _emailClient = emailClient;
            _settings = options.Value;
        }

        /// <inheritdoc/>
        public async Task SendAsync(MailMessage message, Action<object>? action = null)
        {
            IList<EmailAddress> emailAddresses = new List<EmailAddress>();
            foreach(var item in message.To)
            {
                emailAddresses.Add(item.ToEmailAddress());
            }

            EmailRecipients emailRecipients = new EmailRecipients(emailAddresses);
            EmailContent emailContent = new EmailContent(message.Subject);
            if (message.IsBodyHtml)
            {
                emailContent.Html = message.Body;
            }
            else 
            {
                emailContent.PlainText = message.Body;
            }

            EmailMessage emailMessage = new EmailMessage(message?.From?.Address, emailRecipients, emailContent);

            try
            {
                WaitUntil waitUntil = _settings.WaitUntilCompleted ? WaitUntil.Completed : WaitUntil.Started;

                var response = await _emailClient.SendAsync(waitUntil, emailMessage).ConfigureAwait(false);

                if(response.HasCompleted)
                {
                    // After send call action
                    action?.Invoke(response);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{message}", ex.Message);
                throw;
            }
        }
    }
}