using System.Net.Mail;

namespace Weddy.Mailers
{ 
    public interface IWeddyMailer
    {
		MailMessage Invite(string email, string name, string uniqueIdentifier);
	}
}