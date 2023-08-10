// © Copyright (c) Renet Consulting, Inc. All right reserved.
// Licensed under the MIT.

namespace Sdk.Communication
{
    using System.Net.Mail;
    /// <summary>
    /// The ISmtp is the default interface for email communications
    /// </summary>
    public interface ISmtp
    {
        /// <summary>
        /// Send async message. 
        /// </summary>
        /// <param name="message"> The <seealso cref="MailMessage"/> object</param>
        /// <param name="action">The optional <see cref="Action<object>" parameter. 
        /// The action may take place after the message send is completed./></param>
        /// <returns></returns>
        Task SendAsync(MailMessage message, Action<object>? action = null);
    }
}