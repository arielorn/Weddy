using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Weddy.ViewModels
{
    [DataContract]
    public class ParticipantFillInViewModel
    {
        [DataMember(IsRequired = true)]
        public string Name { get; set; }

        [DataMember(IsRequired = true)]
        public string Email { get; set; }
        
        [Required, Range(0, 100000)]
        [DataMember(IsRequired = true)]
        public int Amount { get; set; }

        [DataMember(IsRequired = true)]
        public string UniqueIdentifier { get; set; }
    }
}