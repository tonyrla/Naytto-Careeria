using System;
using System.Collections.Generic;

namespace NayttoProjekti
{
    class Kauppa
    {
        string nimi;
        Varasto liskoKauppa;
        Moduuli nakyma;
        readonly Dictionary<Moduuli, List<string>> teksti; 
        public Kauppa(string nimi)
        {
            liskoKauppa = new Varasto();
            this.nimi = nimi;
            nakyma = Moduuli.Paanakyma;

            //Moduulien valikkotekstit sijoitin tähän tyyliin sen takia että saisin  pidettyä luokan vähän pienempänä ja koodihirviötä olisi vähemmän.
            //Joudun käyttämään näitä sen takia että saan toimimattomat osat näkymään selkeästi ja että muokattavuus olisi mahdollisimman yksinkertaista.
            teksti = new Dictionary<Moduuli, List<string>>();
            teksti.Add(Moduuli.Paanakyma, new List<string>() { $"*** {nimi} - Varastonhallinta - Päänäkymä ***\n\n\n", "V)arasto näkymä".PadRight(40, ' '), "M)yynti näkymä(Ei toimi)\n", "O)sto näkymä(Ei toimi)".PadRight(40, ' '), "T)oimittaja näkymä\n", "A)siakas näkymä(Ei toimi)", "\n\n\nL)opeta" });
            teksti.Add(Moduuli.Varastonakyma, new List<String>() { 
                $"*** {nimi} - Varastonhallinta - Varastonäkymä ***\n\n\n",
                "T)uotteidenlistaus".PadRight(40, ' '),"L)isää tuote\n",
                "P)oista tuote (Ei toimi)".PadRight(40, ' '),"N)äytä tuotteen tiedot (Ei toimi)",
                "\n\n\nA)lkuun"});
            teksti.Add(Moduuli.Asiakasnakyma, new List<String>()
            {
            $"*** {nimi} - Varastonhallinta - Asiakasnäkymä ***\n\n\n",
            "T)ulosta asiakkaat (Ei toimi)".PadRight(40, ' '), "L)isää asiakas (Ei toimi)\n",
            "P)oista asiakas (Ei toimi)".PadRight(40, ' '), "N)äytä asiakkaan tiedot (Ei toimi)",
            "\n\nA)lkuun",
            });
            teksti.Add(Moduuli.Myyntinakyma, new List<String>() {
            $"*** {nimi} - Varastonhallinta - Myyntinäkymä ***\n\n\n",
            "T)ulosta tilaukset (Ei toimi)".PadRight(40, ' '),"L)isää tilaus (Ei toimi)\n",
            "P)oista tilaus (Ei toimi)".PadRight(40, ' '),"M)uuta tilausta (Ei toimi)",
            "\n\n\nA)lkuun"
            });
            teksti.Add(Moduuli.Ostonakyma, new List<String>()
            {
                $"*** {nimi} - Varastonhallinta - Ostonäkymä ***\n\n\n",
                "T)ulosta ostot (Ei toimi)".PadRight(40, ' '),"L)isää ostotilaus (Ei toimi)\n",
                "P)oista ostotilaus (Ei toimi)".PadRight(40, ' '),"M)uuta ostotilausta (Ei toimi)\n",
                "N)äytä ostotilauksen tiedot (Ei toimi)",
                "\n\n\nA)lkuun"
            });
            teksti.Add(Moduuli.Toimittajanakyma, new List<String>()
            {
                $"*** {nimi} - Varastonhallinta - Toimittajanäkymä ***\n\n\n",
                "T)ulosta toimittajat (Ei toimi)".PadRight(40, ' '),"L)isää toimittaja (Ei toimi)\n",
                "P)oista toimittaja (Ei toimi)".PadRight(40, ' '),"N)äytä toimittajan tiedot (Ei toimi)",
                "\n\n\nA)lkuun"
            });

        }

        public void run()
        {
            //Looppi pyörittää moduuleita 'nakyma' muuttujaan asetetun arvon perusteella, joka tulee mooduleissa valittujen funktioiden perusteella

            while (true)
            {
                //Pyyhitään konsoli aina ennen moduulin vaihtoa, niin ruutu pysyy siistinä ja logiikka toimii paremmin
                //  Tästäkin päästään eroon seuraavassa vaiheessa, jossa alan rakentamaan terminaalia
                Console.Clear();
                switch (nakyma)
                {
                    case Moduuli.Toimittajanakyma: { nakyma = toimittajaNakyma(); break; }
                    case Moduuli.Lopeta: { liskoKauppa.saveData(); return; }
                    case Moduuli.Varastonakyma: { nakyma = varastoNakyma(); break; }
                    default: { nakyma = paaNakyma(); break; }

                }
            }
        }
        //Käytännössä etusivu, tästä valitaan moduuli mihin siirrytään

        Moduuli paaNakyma()
        {
            printModule(teksti[Moduuli.Paanakyma]);

            ConsoleKey key = Console.ReadKey().Key;
            Console.Clear();

            switch (key)
            {
                case ConsoleKey.V:
                    {
                        return Moduuli.Varastonakyma;
                    }
                case ConsoleKey.T: { return Moduuli.Toimittajanakyma; }
                case ConsoleKey.L: { return Moduuli.Lopeta; }
                default: return nakyma;
            }
        }

        Moduuli varastoNakyma()
        {
            printModule(teksti[Moduuli.Varastonakyma]);
        kysy:
            ConsoleKey key = Console.ReadKey(true).Key;

            switch (key)
            {
                case ConsoleKey.L: { liskoKauppa.lisaaTuote(); return nakyma; }
                case ConsoleKey.T: { Tuote.printList(liskoKauppa.Tuotteet); return nakyma; };
                case ConsoleKey.A: { return Moduuli.Paanakyma; }
                default: goto kysy;
            }
        }


        Moduuli myyntiNakyma()
        {
            printModule(teksti[Moduuli.Myyntinakyma]);
        kysy:

            ConsoleKey key = Console.ReadKey().Key;
            switch (key)
            {
                case ConsoleKey.A: { return Moduuli.Paanakyma; }
                default: goto kysy;
            }
        }
        Moduuli ostoNakyma()
        {
            printModule(teksti[Moduuli.Ostonakyma]);
        kysy:

            ConsoleKey key = Console.ReadKey(true).Key;

            switch (key)
            {
                case ConsoleKey.A: { return Moduuli.Paanakyma; }
                default: goto kysy;
            }
        }
        Moduuli toimittajaNakyma()
        {
            printModule(teksti[Moduuli.Toimittajanakyma]);
        kysy:

            ConsoleKey key = Console.ReadKey(true).Key;

            switch (key)
            {
                case ConsoleKey.A: { return Moduuli.Paanakyma; }
                default: goto kysy;
            }
        }
        Moduuli asiakasNakyma()
        {
            printModule(teksti[Moduuli.Asiakasnakyma]);
        kysy:

            ConsoleKey key = Console.ReadKey(true).Key;

            switch (key)
            {
                case ConsoleKey.A: { return Moduuli.Paanakyma; }
                default: goto kysy;
            }

        }

        private void printModule(List<string> teksti)
        {
            foreach (string s in teksti)
            {
                Console.ResetColor();
                if (s.Contains("(Ei toimi)"))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                }
                Console.Write(s);
            }
            Console.WriteLine();
        }
    }
}
