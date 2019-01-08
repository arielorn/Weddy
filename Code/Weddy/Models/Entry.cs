namespace Weddy.Models
{
    public class Entry
    {
        public int Id { get; set; }
        public string Name{ get; set; }
        public string Email { get; set; }

        public int Amount { get; set; }
        
        public int DeviationMin { get; set; }
        public int DeviationMax { get; set; }
    }
}