using System;
using System.Collections.Generic;

namespace NayttoProjekti
{
    /*
     * Täällä hallitaan moduulien käynnistämistä ja esitystä.
     *      Valikoiden esitys hoidetaan string-listoilta, joitan ajan printModule-methodin läpi jossa (ei toimi) tekstin omaavat toiminnot värjätään punaisella.
     *      Tämä poistetaan kun kehitän terminaali-guin.
     */

    // Tällä hetkellä moduulien vaihtamiset hoidetaan Enumien kautta, luettavuuden ja ymmärtämisen takia.
    //      Tulevaisuudessa tämä tulee tapahtumaan skripteillä, joissa moduulien logiikka tulee olemaan ja itse ohjelma tulee vain hallitsemaan yhteyksiä, tietokantoja jne.
    enum Moduuli
    {
        Lopeta,
        Paanakyma,
        Varastonakyma,
        Myyntinakyma,
        Ostonakyma,
        Toimittajanakyma,
        Asiakasnakyma
    }
    class Valikot
    {
        string nimi;
        Varasto liskoVarasto;
        Moduuli nakyma;
        readonly Dictionary<Moduuli, List<string>> teksti; 
        public Valikot(string nimi)
        {
            liskoVarasto = new Varasto();
            this.nimi = nimi;
            nakyma = Moduuli.Paanakyma;

            /*
             * Moduulien valikkotekstit sijoitin tähän tyyliin sen takia että saisin  pidettyä luokan vähän pienempänä ja koodihirviötä olisi vähemmän.
             *      Joudun käyttämään näitä sen takia että saan toimimattomat osat näkymään selkeästi ja että muokattavuus olisi mahdollisimman yksinkertaista.
             */
            teksti = new Dictionary<Moduuli, List<string>>();
            teksti.Add(Moduuli.Paanakyma, new List<string>() { $"*** {nimi} - Varastonhallinta - Päänäkymä ***\n\n\n", "V)arasto näkymä".PadRight(40, ' '), "M)yynti näkymä\n", "O)sto näkymä(Ei toimi)".PadRight(40, ' '), "T)oimittaja näkymä\n", "A)siakas näkymä(Ei toimi)", "\n\n\nL)opeta" });
            teksti.Add(Moduuli.Varastonakyma, new List<String>() { 
                $"*** {nimi} - Varastonhallinta - Varastonäkymä ***\n\n\n",
                "T)uotteiden listaus".PadRight(40, ' '),"L)isää tuote\n",
                "P)oista tuote".PadRight(40, ' '),"N)äytä tuotteen tiedot (Ei toimi)\n",
                "K)uittaa tuotteet varastoon(Ei toimi)".PadRight(40, ' '), "S)aldon päivitys(Ei toimi)\n",
                "\n\n\nA)lkuun"});
            teksti.Add(Moduuli.Asiakasnakyma, new List<String>()
            {
            $"*** {nimi} - Varastonhallinta - Asiakasnäkymä ***\n\n\n",
            "T)ulosta asiakkaat".PadRight(40, ' '), "L)isää asiakas (Ei toimi)\n",
            "P)oista asiakas (Ei toimi)".PadRight(40, ' '), "N)äytä asiakkaan tiedot (Ei toimi)",
            "\n\nA)lkuun",
            });
            teksti.Add(Moduuli.Myyntinakyma, new List<String>() {
            $"*** {nimi} - Varastonhallinta - Myyntinäkymä ***\n\n\n",
            "T)ulosta tilaukset".PadRight(40, ' '),"L)isää tilaus\n",
            "P)oista tilaus".PadRight(40, ' '),"M)uuta tilausta (Ei toimi)",
            "\nK)eräilylistojen tulostus",
            "\n\n\nA)lkuun"
            });
            teksti.Add(Moduuli.Ostonakyma, new List<String>()
            {
                $"*** {nimi} - Varastonhallinta - Ostonäkymä ***\n\n\n",
                "T)ulosta ostot(Ei toimi)".PadRight(40, ' '),"L)isää ostotilaus (Ei toimi)\n",
                "P)oista ostotilaus (Ei toimi)".PadRight(40, ' '),"M)uuta ostotilausta (Ei toimi)\n",
                "N)äytä ostotilauksen tiedot (Ei toimi)",
                "\n\n\nA)lkuun"
            });
            teksti.Add(Moduuli.Toimittajanakyma, new List<String>()
            {
                $"*** {nimi} - Varastonhallinta - Toimittajanäkymä ***\n\n\n",
                "T)ulosta toimittajat".PadRight(40, ' '),"L)isää toimittaja\n",
                "P)oista toimittaja".PadRight(40, ' '),"N)äytä toimittajan tiedot (Ei toimi)",
                "\n\n\nA)lkuun"
            });

        }

        //run-methodi pitää sisällään ohjelman pyörimiseen vaadittavan toistolausekkeen, joka vaihtaa näkymiä ja lopettaa ohjelman toistamisen.
        public void run()
        {
            //Looppi pyörittää moduuleita 'nakyma' muuttujaan asetetun arvon perusteella, joka tulee mooduleissa valittujen funktioiden palautusarvona

            while (true)
            {
                //Pyyhitään konsoli aina ennen moduulin vaihtoa, niin ruutu pysyy siistinä ja logiikka toimii paremmin
                //  Tästäkin päästään eroon seuraavassa vaiheessa, jossa alan rakentamaan terminaalia
                Console.Clear();
                if (!(nakyma == Moduuli.Lopeta))
                    printModule(teksti[nakyma]);
                switch (nakyma)
                {
                    case Moduuli.Myyntinakyma: { nakyma = myyntiNakyma(); break; }
                    case Moduuli.Varastonakyma: { nakyma = varastoNakyma(); break; }
                    case Moduuli.Toimittajanakyma: { nakyma = toimittajaNakyma(); break; }

                    case Moduuli.Lopeta: { liskoVarasto.saveData(); return; }
                    default: { nakyma = paaNakyma(); break; }
                }
            }
        }

        /*
         * Moduulien valikoiden tulostus ja toiminnallisuus
         * 
         * Valikoissa tulostetaan vaihtoehdot mitä voi käyttää, ja epäkelvon napin painallus/vaihtoehdon valinta palauttaa logiikan takaisin vaiheeseen jossa luetaan käyttäjältä napin painallusta.
         *      Tulevaisuudessa tämä olisi tarkoitus hoitaa esimerkiksi LUA skripteillä, jotta modulaarisuus olisi mahdollisimman ongelmavapaa toteuttaa ja ei vaatisi jatkuvasti lähdekoodin kääntämistä.
         * 
         * paaNakyma näyttää mitä moduuleita on käytössä, ja siirtymällä moduuliin saa listauksen moduulin toiminnoista.
         *      Toiminnot olisi myös tarkoitus hoitaa skriptikielellä modulaarisuutta ajatellen.
         * Tämän saisi kompaktimmaksi sijoittamalla moduulien herättelyn esim. dictionaryyn
         * Dictionary<Moduuli, Func<Moduuli>>
         * 
         */

        Moduuli paaNakyma()
        {
            ConsoleKey key = Console.ReadKey().Key;
            Console.Clear();

            switch (key)
            {
                case ConsoleKey.M: { return Moduuli.Myyntinakyma; }
                case ConsoleKey.V: { return Moduuli.Varastonakyma; }
                case ConsoleKey.T: { return Moduuli.Toimittajanakyma; }
                case ConsoleKey.L: { return Moduuli.Lopeta; }
                default: return nakyma;
            }
        }

        Moduuli varastoNakyma()
        {
        kysy:
            ConsoleKey key = Console.ReadKey(true).Key;

            switch (key)
            {
                case ConsoleKey.L: { liskoVarasto.lisaaTuote(); return nakyma; }
                case ConsoleKey.T: { liskoVarasto.tulostaLista(liskoVarasto.Tuotteet); return nakyma; };
                case ConsoleKey.P: { liskoVarasto.tulostaLista(liskoVarasto.Tuotteet, true); return nakyma; }
                case ConsoleKey.A: { return Moduuli.Paanakyma; }
                default: goto kysy;
            }
        }
        Moduuli myyntiNakyma()
        {
        kysy:

            ConsoleKey key = Console.ReadKey(true).Key;
            switch (key)
            {
                case ConsoleKey.K: { liskoVarasto.tulostaKerailyListat(); return nakyma; }
                case ConsoleKey.L: { liskoVarasto.lisaaTilaus(); return nakyma; }
                case ConsoleKey.A: { return Moduuli.Paanakyma; }
                case ConsoleKey.T:
                    {
                        Console.WriteLine("Anna tulostettavien tilausten päivämäärä muodossa PP.KK.VVVV, tyhjä tulostaa kaikki tilaukset");
                        DateTime dt;
                        String rl = Console.ReadLine();
                        if (rl == "")
                        {
                            liskoVarasto.tulostaLista(liskoVarasto.getTilaukset());
                            return nakyma;
                        }
                        
                        while (!(DateTime.TryParse(rl, out dt)))
                            rl = Console.ReadLine();
                        liskoVarasto.tulostaLista(liskoVarasto.getTilaukset(dt)); return nakyma; };
                case ConsoleKey.P:
                    {
                        Console.WriteLine("Anna tulostettavien tilausten päivämäärä muodossa PP.KK.VVVV, tyhjä tulostaa kaikki tilaukset");
                        DateTime dt;
                        String rl = Console.ReadLine();
                        if (rl == "")
                        {
                            liskoVarasto.tulostaLista(liskoVarasto.getTilaukset(), true);
                            return nakyma;
                        }

                        while (!(DateTime.TryParse(rl, out dt)))
                            rl = Console.ReadLine(); 
                        liskoVarasto.tulostaLista(liskoVarasto.getTilaukset(dt), true); return nakyma; }
                default: goto kysy;
            }
        }
        Moduuli ostoNakyma()
        {
        kysy:
            ConsoleKey key = Console.ReadKey(true).Key;
            switch (key)
            {
                case ConsoleKey.A: { return Moduuli.Paanakyma; }

                case ConsoleKey.T: { liskoVarasto.tulostaLista(liskoVarasto.Ostot); return nakyma; }
                case ConsoleKey.P: { liskoVarasto.tulostaLista(liskoVarasto.Ostot, true); return nakyma; }
                default: goto kysy;
            }
        }
        Moduuli toimittajaNakyma()
        {
        kysy:
            ConsoleKey key = Console.ReadKey(true).Key;
            switch (key)
            {
                case ConsoleKey.A: { return Moduuli.Paanakyma; }
                case ConsoleKey.L: { liskoVarasto.lisaaToimittaja(); return nakyma; }
                case ConsoleKey.T: { liskoVarasto.tulostaLista(liskoVarasto.TavaranToimittajat); return nakyma; }
                case ConsoleKey.P: { liskoVarasto.tulostaLista(liskoVarasto.TavaranToimittajat, true); return nakyma; }
                default: goto kysy;
            }
        }
        Moduuli asiakasNakyma()
        {
        kysy:
            ConsoleKey key = Console.ReadKey(true).Key;
            switch (key)
            {
                case ConsoleKey.A: { return Moduuli.Paanakyma; }
                case ConsoleKey.T: { liskoVarasto.tulostaLista(liskoVarasto.Asiakkaat); return nakyma; }
                case ConsoleKey.P: { liskoVarasto.tulostaLista(liskoVarasto.Asiakkaat, true); return nakyma; }
                default: goto kysy;
            }
        }

        /*
         *  printModule methodilla tulostetaan moduulien toiminnot ja värjätään punaisella jos toiminto ei ole vielä käytössä
         */
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
