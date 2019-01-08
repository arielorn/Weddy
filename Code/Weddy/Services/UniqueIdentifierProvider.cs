using System;
using System.Linq;
using Weddy.Models;

namespace Weddy.Services
{
    public class UniqueIdentifierProvider : IUniqueIdentifierProvider
    {
        private WeddyDbContext _context;

        public UniqueIdentifierProvider(WeddyDbContext context)
        {
            _context = context;
        }

        public string GenerateIdentifier()
        {
            var isUnique = false;
            var identifier = string.Empty;
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();

            while (!isUnique)
            {
                identifier = new string(
                    Enumerable.Repeat(chars, 8)
                        .Select(s => s[random.Next(s.Length)])
                        .ToArray());

                isUnique = !_context.Pools
                                .Any(p => p.UniqueIdentifier == identifier);
            }

            return identifier;
        }
    }
}