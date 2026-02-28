using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TarihWebAPI.ApplicationAndDomain.Enums
{
    public enum PersonRelationType
    {
        Father = 0,       // Baba
        Mother = 1,       // Anne
        Son = 2,          // Oğul
        Daughter = 3,     // Kız
        Brother = 4,      // Erkek kardeş
        Sister = 5,       // Kız kardeş
        Spouse = 6,       // Eş
        Teacher = 7,      // Hoca
        Student = 8,      // Öğrenci
        Mentor = 9,       // Akıl hocası
        Vizier = 10,      // Vezir / Baş danışman
        Successor = 11,   // Halef
        Predecessor = 12, // Selef
        Rival = 13,       // Rakip
        Ally = 14,        // Müttefik
        Other = 99
    }
}
