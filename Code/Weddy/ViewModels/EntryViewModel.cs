using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Weddy.Helpers;

namespace Weddy.ViewModels
{
    public class EntryViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public int Amount { get; set; }

        public int DeviationMin { get; set; }
        public int DeviationMax { get; set; }

        public string Gravatar
        {
            get { return GravatarHelper.GetGravatarUrl(Email, 64); }
        }

        public string DeviationText
        {
            get
            {
                if (DeviationMin == DeviationMax)
                {
                    return "On average";
                }
                else if (DeviationMin < 0)
                {
                    return ("Between " + @Math.Abs(DeviationMin) + "% to " + @Math.Abs(DeviationMax) + "% below average");
                }
                else
                {
                    return ("Between " + @Math.Abs(DeviationMin) + "% to " + @Math.Abs(DeviationMax) + "% above average");
                }
            }
        }
    }
}