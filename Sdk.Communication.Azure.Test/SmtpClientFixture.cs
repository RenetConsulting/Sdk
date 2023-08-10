// © Copyright (c) Renet Consulting, Inc. All right reserved.
// Licensed under the MIT.

using Azure;
using Azure.Communication.Email;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using System.Net.Mail;
using Xunit;

namespace Sdk.Communication.Azure.Test
{
    public class SmtpClientFixture
    {
        [Fact]
        public async Task SendMockTestSuccess()
        {

            Mock<ILogger<SmtpClient>> mockLogger = new();
            Mock<EmailClient> emailClientMock = new();
            Mock<IOptions<AzureCommunicationSettings>> settingsMock = new();

            Mock<EmailSendOperation> operationMock = new();
            operationMock.Setup( a=> a.HasCompleted).Returns(true);

            AzureCommunicationSettings settings = new() { WaitUntilCompleted = true };

            settingsMock.Setup(o=> o.Value).Returns(settings);

            emailClientMock.Setup( a => a.SendAsync(WaitUntil.Completed, It.IsAny<EmailMessage>(), default)).Returns(Task.FromResult(operationMock.Object));

            SmtpClient client = new(mockLogger.Object, emailClientMock.Object, settingsMock.Object);
            
            MailMessage message = new(new MailAddress("DoNotReply@mycomapny.com"), new MailAddress("myname@mycompany.com"))
            {
                Body = "MyTest4",
                Subject = "My first email",
                IsBodyHtml = false
            };

            object? value = null;

            await client.SendAsync(message: message, action: (x) => { value = x;  }); 

            Assert.NotNull(value);
        }

    }
}