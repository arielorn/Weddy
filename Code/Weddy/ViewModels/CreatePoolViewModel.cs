using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Weddy.ViewModels
{
    public class CreatePoolViewModel
    {
        public string EventType { get; set; }
        public string EventName { get; set; }

        public List<ParticipantViewModel> Participants{ get; set; }

        public List<EventType> EventTypes 
        { 
            get
            {
                return new List<EventType>(new[]
                    {
                        new EventType {Name = "Wedding", DisplayName = "Wedding"},
                        new EventType {Name = "Birthday", DisplayName = "Birthday"},
                        new EventType {Name = "Other", DisplayName = "Other"},
                    });
            }
        } 

        public CreatePoolViewModel()
        {
            Participants = new List<ParticipantViewModel>();
        }

    }

    public class EventType
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
    }
}