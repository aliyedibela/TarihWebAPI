using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TarihWebAPI.ApplicationAndDomain.Enums
{
    public enum Occupation
    {
        // Siyasi Liderler
        Emperor = 0,        // İmparator
        Sultan = 1,         // Sultan
        King = 2,           // Kral
        Queen = 3,          // Kraliçe
        Caliph = 4,         // Halife
        Prince = 5,         // Prens/Şehzade
        Princess = 6,       // Prenses
        President = 7,      // Cumhurbaşkanı
        PrimeMinister = 8,  // Başbakan
        Governor = 9,       // Vali/Beylerbeyi

        // Askeri
        General = 10,       // General
        Admiral = 11,       // Amiral
        Commander = 12,     // Komutan
        Soldier = 13,       // Asker

        // Dini Liderler
        Prophet = 14,       // Peygamber
        Pope = 15,          // Papa
        Patriarch = 16,     // Patrik
        Sheikh = 17,        // Şeyh
        Imam = 18,          // İmam
        Mufti = 19,         // Müftü
        Rabbi = 20,         // Haham
        Monk = 21,          // Rahip

        // Bilim İnsanları
        Scientist = 22,     // Bilim İnsanı
        Mathematician = 23, // Matematikçi
        Astronomer = 24,    // Astronom
        Physician = 25,     // Hekim/Doktor
        Alchemist = 26,     // Simyacı
        Geographer = 27,    // Coğrafyacı

        // Düşünürler ve Yazarlar
        Philosopher = 28,   // Filozof
        Scholar = 29,       // Alim
        Poet = 30,          // Şair
        Writer = 31,        // Yazar
        Historian = 32,     // Tarihçi
        Theologian = 33,    // İlahiyatçı

        // Sanatçılar
        Painter = 34,       // Ressam
        Sculptor = 35,      // Heykeltıraş
        Architect = 36,     // Mimar
        Musician = 37,      // Müzisyen
        Composer = 38,      // Besteci

        // Keşifçiler
        Explorer = 39,      // Kaşif
        Traveler = 40,      // Seyyah
        Navigator = 41,     // Seyirci/Denizci
        Cartographer = 42,  // Haritacı

        // Diğer
        Merchant = 43,      // Tüccar
        Diplomat = 44,      // Diplomat
        Teacher = 45,       // Öğretmen
        Inventor = 46,      // Mucit
        Revolutionary = 47, // Devrimci
        Reformer = 48,      // Reformcu
        Other = 99          // Diğer
    }

}
