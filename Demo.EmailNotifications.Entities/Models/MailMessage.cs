using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using System.Linq;

namespace Demo.EmailNotifications.Entities
{
    public class MailMessage
    {
        [Required]
        public string Body { get; set; }

        [Required]
        public string Subject { get; set; }

        [EmailAddress]
        public string From { get; set; }

        [ListShouldHaveAtleastOneEmail(ErrorMessage = "Please provide atleast one recipient")]
        [ListHasValidEmailAdresses(ErrorMessage = "Please provide valid email address")]
        public List<string> To { get; set; }

        [ListHasValidEmailAdresses(ErrorMessage = "Please provide valid email address")]
        public List<string> Cc { get; set; }

        [ListHasValidEmailAdresses(ErrorMessage = "Please provide valid email address")]
        public List<string> Bcc { get; set; }
    }

    public class ListShouldHaveAtleastOneEmail : ValidationAttribute
    {
        public override bool IsValid(object emailList)
        {
            return emailList == null || (emailList as List<string>).Any();
        }
    }

    public class ListHasValidEmailAdresses : ValidationAttribute
    {
        public override bool IsValid(object emailList)
        {
            if (emailList == null)
                return true;

            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            foreach (var email in emailList as List<string>)
            {
                Match match = regex.Match(email);
                if (match.Success)
                    continue;
                else
                    return false;
            }

            return true;
        }
    }
}
