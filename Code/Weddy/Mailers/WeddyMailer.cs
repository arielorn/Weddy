using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Mvc.Mailer;
using System.Net.Mail;

namespace Weddy.Mailers
{ 
    public class WeddyMailer : MailerBase, IWeddyMailer     
	{
        public WeddyMailer() :
			base()
		{
			MasterName="_Layout";
		}


        public virtual MailMessage Invite(string email, string name, string uniqueIdentifier)
		{
			var mailMessage = new MailMessage{Subject = "Invite"};
			
			mailMessage.To.Add(email);
            ViewBag.Name = name;
            ViewBag.UniqueIdentifier = uniqueIdentifier;
			PopulateBody(mailMessage, viewName: "Invite");

			return mailMessage;
		}

		
	}
}