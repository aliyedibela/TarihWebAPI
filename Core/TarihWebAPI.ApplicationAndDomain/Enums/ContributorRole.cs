using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TarihWebAPI.ApplicationAndDomain.Enums
{
    public enum ContributorRole
    {
        Author = 0,      // Yazar
        CoAuthor = 1,    // Ortak yazar
        Translator = 2,  // Mütercim
        Editor = 3,      // Editör
        Commentator = 4, // Şarih / Yorumcu
        Illustrator = 5, // Ressam/Nakkaş
        Compiler = 6,    // Derleyici
        Other = 99
    }
}
