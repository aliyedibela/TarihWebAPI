using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TarihWebAPI.ApplicationAndDomain.Enums
{
    public enum PersonEventRole
    {
        Commander = 0,   // Komutan
        Negotiator = 1,  // Müzakereci
        Organizer = 2,   // Organizatör
        Signatory = 3,   // İmzacı
        Victim = 4,      // Mağdur
        Witness = 5,     // Tanık
        Leader = 6,      // Lider
        Founder = 7,     // Kurucu
        Participant = 8, // Katılımcı
        Diplomat = 9,    // Diplomat
        Advisor = 10,    // Danışman
        Other = 99
    }
}
