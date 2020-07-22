using Microsoft.VisualBasic;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.IO;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace NayttoProjekti
{
    //Varasto-luokka pitää sisällään toiminnallisuuden moduulien hallintaan ja sisällön muokkaamiseen
    //
    //  GUI/UI rework vaiheessa nämä kaikki kyselyt tullaan poistamaan ja tämä kaikki hoidetaan forms tyylisesti tietokannan kanssa.
    //  Tällä hetkellä tietueen lisääminen, poistaminen ja muokkaaminen vaatii ~50 riviä koodia per osuus, terminaali GUIn kanssa pystyn refaktoroimaan sen niin että pärjäämme kaikkineen puolella tuosta.

    class Varasto
    {
        List<Tuote> tuotteet;
        List<Toimittaja> tavaranToimittajat;
        List<Osto> ostot;
        List<Asiakas> asiakkaat;
        SortedList<DateTime, Myynti> tilaukset;

        public List<Tuote> Tuotteet { get => tuotteet; }
        public List<Asiakas> Asiakkaat { get => asiakkaat; }
        public List<Osto> Ostot { get => ostot; }
        public List<Toimittaja> TavaranToimittajat { get => tavaranToimittajat; }
        public List<Myynti> getTilaukset(DateTime pvm)
        {
            return tilaukset.Where(x => x.Key.Date == pvm.Date).Select(x=>x.Value).ToList();
        }
        public List<Myynti> getTilaukset()
        {
            return tilaukset.Select(x => x.Value).ToList();
        }

        //Tilauksen poistaminen tietokannasta tilauksen id:n mukaan
        public void RemoveTilaus(int id)
        {
            var toRemove = tilaukset.Where(pair => pair.Value.TilausId == id)
                         .Select(pair => pair.Key)
                         .ToList();

            foreach (var key in toRemove)
            {
                if (tilaukset[key].TilausId == id)
                    tilaukset.Remove(key);
            }
        }

        //Tietokantaan tallennettujen keräilylistojen tulostus
        public void tulostaKerailyListat()
        {
            List<string> tilaukset = new List<string>();
            foreach (Myynti m in getTilaukset())
            {
                int padding = 75;
                decimal kokonaisHinta = 0;
                StringBuilder sb = new StringBuilder();
                sb.Append("".PadRight(padding, '#'));
                sb.Append($"\n[#{m.TilausId} | Myyjä#: {m.MyyjaId} | Toimitus: {m.ToimitusPvm.Date}]\n");                
                sb.Append(m.Tilaaja);
                sb.Append("\n".PadRight(padding, '#'));
                sb.Append("\n\n");
                foreach (Tuote t in m.TilatutTuotteet)
                {
                    kokonaisHinta += t.HintaUlos * t.Saldo;
                    sb.Append(t.asiakasString() + "\n");
                }
                sb.Append($"\n\n\n*** Tilaus yhteensä : {kokonaisHinta} ***\n");

                sb.Append("".PadRight(padding, '#'));
                tilaukset.Add(sb.ToString());
            }
            foreach (string s in tilaukset)
            {
                Console.WriteLine(s);
                Console.WriteLine("\nPaina mitä tahansa näppäintä jatkaaksesi\n");
                Console.ReadKey(true);
            }
        }
        //TO-DO Database
        //Varastonhallintajärjestelmän tietokantaa ajaa toistaiseksi jokaisen moduulin oma JSON tiedosto, jota serialisoidaan ja deserialisoidaan ohjelman käynnistyessä ja lopetettaessa (hallitusti, painamalla L).
        //  Tähän on käytetty Newtonsoftin JSON kirjastoa koska Microsoftin oma tuntui hirveän tönköltä jopa näin yksinkertaisen asian tekoon.

        public void saveData()
        {
            System.IO.File.WriteAllText(@"res/Asiakkaat.json", JsonConvert.SerializeObject(asiakkaat));
            System.IO.File.WriteAllText(@"res/Tuotteet.json", JsonConvert.SerializeObject(tuotteet));
            System.IO.File.WriteAllText(@"res/Toimittajat.json", JsonConvert.SerializeObject(tavaranToimittajat));
            System.IO.File.WriteAllText(@"res/Ostot.json", JsonConvert.SerializeObject(ostot));
            System.IO.File.WriteAllText(@"res/Tilaukset.json", JsonConvert.SerializeObject(tilaukset));
        }
        // Alustetaan listat konstruktorissa ja luetaan JSON tiedostoista data niihin
        public Varasto()
        {
            this.tuotteet = new List<Tuote>();
            this.tavaranToimittajat = new List<Toimittaja>();
            this.ostot = new List<Osto>();
            this.asiakkaat = new List<Asiakas>();
            this.tilaukset = new SortedList<DateTime, Myynti>();

            asiakkaat = JsonConvert.DeserializeObject<List<Asiakas>>(File.ReadAllText("res/Asiakkaat.json"));
            tuotteet = JsonConvert.DeserializeObject<List<Tuote>>(File.ReadAllText("res/Tuotteet.json"));
            tavaranToimittajat = JsonConvert.DeserializeObject<List<Toimittaja>>(File.ReadAllText("res/Toimittajat.json"));
            ostot = JsonConvert.DeserializeObject<List<Osto>>(File.ReadAllText("res/Ostot.json"));
            tilaukset = JsonConvert.DeserializeObject<SortedList<DateTime, Myynti>>(File.ReadAllText("res/Tilaukset.json"));

        }

        //Tuotteen lisäys
        //  Kysytään käyttäjältä tuotteelle nimi, tuoteryhmä, määrä, sijainti varastossa ja hinnat. Jos nimi löytyy jo varastosta, estetään tuotteen lisääminen.

        //  Tämä vaihe tullaan tekemään terminaali-ikkunassa samaan tapaan kuin kaavakkeet täytetään, tietokannasta tulevilla ehdotuksilla.
        //  Tähän tapaan, mutta 90-luvun BBS-purkki tyylisellä terminaalikäyttöliittymällä: https://www.c-sharpcorner.com/UploadFile/deepak.sharma00/autosuggest-textbox-from-database-column-in-windows-forms/
        //  Siihen saakka kunnes aloitan terminaali UI:n rakentamisen ja tietokantojen rakentamisen, kaikki logiikka tulee olemaan koodihirviötä.
        internal void lisaaTuote()
        {
            string nimi;
            DateTime saapumis_paiva;
            decimal hinta_sisaan, hinta_ulos;
            int saldo, hyllyPaikka, iter = 0, index = 0;
            VarastoTila varastoTila;
            Tuoteryhma tuoteRyhma;

            Console.WriteLine("Anna tuotteen nimi (esim. Toukka, 200g tai Leopardigekko, Uros\nTyhjä lopettaa.");
            nimi = Console.ReadLine();
            if (nimi == "")
                return;
            foreach (Tuote t in tuotteet)
            {
                if (t.Nimi == nimi)
                {
                    Console.WriteLine("Kyseinen tuote löytyy jo varastosta\nPaina mitä tahansa näppäintä palataksesi");
                    Console.ReadKey(true);
                    return;
                }
            }
            Console.WriteLine("Valitse tuoteryhmän numero listasta:");
            iter = 0;
            foreach (Tuoteryhma foo in Enum.GetValues(typeof(Tuoteryhma)))
            {
                Console.WriteLine(iter + ")" + foo);
                iter++;
            }
            index = 0;
            while (!int.TryParse(Console.ReadLine(), out index) || index >= Enum.GetNames(typeof(Tuoteryhma)).Length || index < 0) ;
            tuoteRyhma = (Tuoteryhma)index;
            Console.WriteLine("Anna tuotteen määrä myyntierissä (esim 20)");
            while (!int.TryParse(Console.ReadLine(), out saldo) || saldo < 0) ;
            Console.WriteLine("Valitse tuotteelle varastopaikka listasta:");
            iter = 0;
            foreach (VarastoTila foo in Enum.GetValues(typeof(VarastoTila)))
            {
                Console.WriteLine(iter + ")" + foo);
                iter++;
            }
            index = 0;
            while (!int.TryParse(Console.ReadLine(), out index) || index >= Enum.GetNames(typeof(VarastoTila)).Length || index < 0) ;
            varastoTila = (VarastoTila)index;
            Console.WriteLine("Valitse tuotteelle hyllynumero");
            while (!int.TryParse(Console.ReadLine(), out hyllyPaikka) || hyllyPaikka < 0) ;
            Console.WriteLine("Aseta tuotteen sisäänostohinta: (muodossa 2,20)");
            while (!decimal.TryParse(Console.ReadLine(), out hinta_sisaan) || hinta_sisaan < 0) ;
            Console.WriteLine("Aseta tuotteen ulosmyyntihinta: (muodossa 5,20)");
            while (!decimal.TryParse(Console.ReadLine(), out hinta_ulos) || hinta_ulos < 0) ;

            //Erän luominen
            Console.WriteLine("Valitse toimittajan numero listasta:");
            iter = 0;
            tavaranToimittajat.ForEach(x => { Console.WriteLine(iter + ")" + x.Nimi); iter++; });
            index = 0;
            while (!int.TryParse(Console.ReadLine(), out index) || index >= tavaranToimittajat.Count || index < 0) ;
            Console.WriteLine("Anna saapumispäivä muodossa PP.KK.VVVV");
            saapumis_paiva = DateTime.ParseExact(Console.ReadLine(), "d", null);
            TuoteEra era = new TuoteEra(saapumis_paiva, tavaranToimittajat[index]);

            //Tuotteen luominen aiemman kyselyn perusteella
            tuotteet.Add(new Tuote(nimi, tuoteRyhma, saldo, varastoTila, hyllyPaikka, hinta_sisaan, hinta_ulos, era));
        }

        //Tilauksen lisääminen
        //  Kysytään käyttäjältä myyjän ID, toimituspäivä, tuotteet ja asiakas

        // Tuotteiden lisäämiseen käytetään tulostaLista-methodis joka vaatii parametreiksi tyyppivapaan listan, boolean-arvon poistamista varten, boolean arvon myyntiä varten
        internal void lisaaTilaus()
        {
            int myyjaId;
            Asiakas asiakas;
            List<Tuote> myydytTuotteet;
            DateTime toimituspvm;
            Console.WriteLine("Anna MyyjäID: \n-1 lopettaa.");
            while (!(int.TryParse(Console.ReadLine(), out myyjaId)));
            if (myyjaId == -1)
                return;

            Console.WriteLine("\nAnna toimituspäivä muodossa PP.KK.VVVV");
            while (!(DateTime.TryParse(Console.ReadLine(), out toimituspvm))) ;

            myydytTuotteet = tulostaLista(tuotteet, false,true);

            asiakas = valitseAsiakas();

            tilaukset.Add(DateTime.Now, new Myynti(myyjaId, asiakas, myydytTuotteet, toimituspvm));
            

        }

        //Toimittajan lisääminen tietokantaan
        internal void lisaaToimittaja()
        {
            Console.Clear();
            Console.WriteLine("*** Toimittajan lisäys ***");
            string nimi, puh, email;
            Console.WriteLine("Syötä toimittajan nimi\nTyhjä lopettaa.");
            nimi = Console.ReadLine();
            if (nimi == "")
                return;

            Console.WriteLine("Syötä toimittajan puhelinnumero");
            puh = Console.ReadLine();
            Console.WriteLine("Syötä toimittajan email");
            email = Console.ReadLine();
            tavaranToimittajat.Add(new Toimittaja(nimi, puh, email));
        }

        //Asiakkaan valinta tietokannasta
        private Asiakas valitseAsiakas()
        {
            //Kuinka monta tulostetaan, ja numeroilla tehtävän poiston hallinta
            int jako = 5;

            int indeksi = 0, indeksinLaskuri = 0;
            if (asiakkaat.Count <= 0)
                throw new Exception("Ei asiakkaita");
            alku:
            Console.Clear();
            while (true)
            {

                Console.WriteLine("#\t[ ID | NIMI ]");
                if (indeksinLaskuri >= asiakkaat.Count)
                {
                    Console.WriteLine("Paina mitä tahansa näppäintä palataksesi");
                    Console.ReadKey(true);
                    indeksi = 0; indeksinLaskuri = 0;
                    goto alku;
                }
                for (int i = indeksi; i < asiakkaat.Count; i++)
                {
                    Console.WriteLine($"#{i - indeksi}\t" + asiakkaat[i].ToString() + "\n");
                    indeksinLaskuri++;
                    if (indeksinLaskuri % jako == 0 || indeksinLaskuri >= asiakkaat.Count)
                    {
                        Console.WriteLine("\n*** Valitse toiminto ***\n");

                        Console.WriteLine($"0-{i - indeksi})" + " Valitse asiakas listalta");
                        if (!(indeksinLaskuri >= asiakkaat.Count))
                            Console.WriteLine("J)atka listan tulostusta");
                    kysy:
                        ConsoleKeyInfo keyinfo = Console.ReadKey(true);
                        ConsoleKey key = keyinfo.Key;
                        if (indeksinLaskuri >= asiakkaat.Count && key == ConsoleKey.J)
                            goto kysy;
                        switch (key)
                        {
                            case ConsoleKey randomKey when (keyinfo.KeyChar >= '0' && keyinfo.KeyChar <= (char)jako + 48):
                                {
                                    int numero = int.Parse(keyinfo.KeyChar.ToString()) + indeksi;
                                    return asiakkaat[numero];
                                }

                            //Pyyhitään konsoli, siirrytään lopun laskuriosioon ja jatketaan listan tulostusta.
                            case ConsoleKey.J:
                                {
                                    Console.Clear();
                                    goto lasku;
                                }
                            //Tuntematon tai virheellinen valinta siirtää suorituksen takaisin napin tarkastukseen.
                            default: goto kysy;
                        }
                    }
                }
            lasku:
                indeksi = indeksinLaskuri;
            }
        }

      

        /*
         *  Tyyppivapaa listojen tulostus ja alkioiden poisto, niin säästytään turhilta koodiriveiltä
         */
        internal List<Tuote> tulostaLista<T>(List<T> lista, bool poista = false, bool myy = false)
        {
            //Kuinka monta tulostetaan, ja numeroilla tehtävän poiston hallinta
            int jako = 5;


            List<Tuote> myyntiLista = new List<Tuote>();
        alku:
            int indeksi = 0, indeksinLaskuri = 0;
            if (lista.Count <= 0)
                return myyntiLista;
            Console.Clear();
            while (true)
            {

                
                if (indeksinLaskuri >= lista.Count)
                {
                    Console.WriteLine("Paina mitä tahansa näppäintä palataksesi");
                    Console.ReadKey(true);
                    return myyntiLista;
                }
                Console.WriteLine("#\t[ ID | NIMI ]");
                for (int i = indeksi; i < lista.Count; i++)
                {
                    Console.WriteLine($"#{i-indeksi}\t" + lista[i].ToString());
                    indeksinLaskuri++;
                    if (indeksinLaskuri % jako == 0 || indeksinLaskuri >= lista.Count)
                    {
                        Console.WriteLine("\n*** Valitse toiminto ***\n");
                        if (poista)
                            Console.WriteLine($"0-{i-indeksi})" + " Valitse poistettava listalta");
                        if (myy)
                            Console.WriteLine($"0-{i - indeksi})" + " Valitse myytävä listalta");
                        if (!(indeksinLaskuri >= lista.Count))
                            Console.WriteLine("J)atka listan tulostusta");
                        Console.WriteLine("T)akaisin\n");
                    kysy:
                        ConsoleKeyInfo keyinfo = Console.ReadKey(true);
                        ConsoleKey key = keyinfo.Key;

                        int nappaimenArvo = (int) keyinfo.KeyChar -48;
                        if (indeksinLaskuri >= lista.Count && key == ConsoleKey.J)
                            goto kysy;
                        switch (key)
                        {
                            /*
                             * Tarkistetaan poistamista varten onko painettu näppäin numero väliltä 0 - tulosteiden määrä
                             * Jos on, mutta poistamista ei ole valittu, palataan napin tarkistukseen
                             * Jos poistaminen on valittu, siirrytään methodiin joka poistaa tietueen jos käyttäjä on varma valinnastaan.
                            */
                            case ConsoleKey randomKey when (nappaimenArvo >= 0 && nappaimenArvo <= i-indeksi):
                                {   
                                    if (!poista && !myy)
                                        goto kysy;
                                    int numero = int.Parse(keyinfo.KeyChar.ToString())+indeksi;
                                    if (poista)
                                        poistaListasta(lista, nappaimenArvo + indeksi);
                                    if (myy)
                                    {
                                        myyntiLista.Add(myyListasta(tuotteet, numero));
                                        goto alku;
                                    }
                                    break;
                                }
                                //Palataan takaisin Varasto näkymään
                            case ConsoleKey.T:
                                {
                                    return myyntiLista;
                                }
                                //Pyyhitään konsoli, siirrytään lopun laskuriosioon ja jatketaan listan tulostusta.
                            case ConsoleKey.J:
                                {
                                    Console.Clear();
                                    goto lasku;
                                }
                                //Tuntematon tai virheellinen valinta siirtää suorituksen takaisin napin tarkastukseen.
                            default: goto kysy;
                        }
                    }
                }
                lasku:
                indeksi = indeksinLaskuri;
            }
        }

        //Tuotteen poisto tyyppivapaalta listalta indeksin mukaan
        //  Tuotteen myynti vähentää saldoa ja hyväksyttävä myyntimäärä maksimissaan se mitä on saldoilla
        private Tuote myyListasta(List<Tuote> lista, int numero)
        {
            int maara = 0;
            Console.WriteLine($"Kuinka monta myyntierää myydään? Tuotetta on: {lista[numero].Saldo}");
            while (!(int.TryParse(Console.ReadLine(), out maara) || maara < 0 || maara > lista[numero].Saldo));
            Console.WriteLine("");
            lista[numero].Saldo = lista[numero].Saldo - maara;
            Tuote t = new Tuote(lista[numero]);
            t.Saldo = maara;
            return t;
        }

        //Tyyppivapaalta listalta tapahtuva olion poisto indeksin mukaan
        private void poistaListasta<T>(List<T> lista, int indeksi = -1)
        {
            Console.Clear();
            Console.WriteLine("\n\t\tPOISTETAANKO ALLA OLEVA MERKINTÄ? (K/E)\n");
            Console.WriteLine(lista[indeksi]);
            ConsoleKey varmistus;
        uusinta:
            varmistus = Console.ReadKey(true).Key;
            while (varmistus != ConsoleKey.K && varmistus != ConsoleKey.E)
                goto uusinta;

            if (varmistus == ConsoleKey.K)
            {
                if (lista[0] is Myynti)
                {
                    Myynti m = (Myynti)Convert.ChangeType(lista.ElementAt(indeksi), conversionType: typeof(Myynti));
                    RemoveTilaus(m.TilausId);
                    foreach (Tuote t in m.TilatutTuotteet)
                    {
                        int index = tuotteet.IndexOf(t);
                        tuotteet[index].Saldo += t.Saldo;
                    }
                }
                lista.RemoveAt(indeksi);
            }
        }
    }

}
