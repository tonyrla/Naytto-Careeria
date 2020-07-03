using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace NayttoProjekti
{

    class Varasto
    {
        List<Tuote> tuotteet;
        List<Toimittaja> tavaranToimittajat;
        List<Osto> ostot;
        List<Asiakas> asiakkaat;
        SortedList<DateTime, Myynti> tilaukset;

        //TO-DO Database
        //Varastonhallintajärjestelmän tietokantaa ajaa toistaiseksi jokaisen moduulin oma JSON tiedosto, jota serialisoidaan ja deserialisoidaan ohjelman käynnistyessä ja lopetettaessa (hallitusti, painamalla L).
        //  Tähän on käytetty Newtonsoftin JSON kirjastoa koska Microsoftin oma tuntui hirveän tönköltä jopa näin yksinkertaisen asian tekoon.

        public void saveData()
        {
            System.IO.File.WriteAllText(@"res/Asiakkaat.json", JsonConvert.SerializeObject(asiakkaat));
            System.IO.File.WriteAllText(@"res/Tuotteet.json", JsonConvert.SerializeObject(tuotteet));
            System.IO.File.WriteAllText(@"res/Toimittajat.json", JsonConvert.SerializeObject(TavaranToimittajat));
            System.IO.File.WriteAllText(@"res/Ostot.json", JsonConvert.SerializeObject(ostot));
            System.IO.File.WriteAllText(@"res/Tilaukset.json", JsonConvert.SerializeObject(tilaukset));
        }
        public Varasto()
        {
            this.tuotteet = new List<Tuote>();
            this.TavaranToimittajat = new List<Toimittaja>();
            this.ostot = new List<Osto>();
            this.asiakkaat = new List<Asiakas>();
            this.tilaukset = new SortedList<DateTime, Myynti>();

            asiakkaat = JsonConvert.DeserializeObject<List<Asiakas>>(File.ReadAllText("res/Asiakkaat.json"));
            tuotteet = JsonConvert.DeserializeObject<List<Tuote>>(File.ReadAllText("res/Tuotteet.json"));
            TavaranToimittajat = JsonConvert.DeserializeObject<List<Toimittaja>>(File.ReadAllText("res/Toimittajat.json"));
            ostot = JsonConvert.DeserializeObject<List<Osto>>(File.ReadAllText("res/Ostot.json"));
            tilaukset = JsonConvert.DeserializeObject<SortedList<DateTime, Myynti>>(File.ReadAllText("res/Tilaukset.json"));

        }

        internal List<Tuote> Tuotteet { get => tuotteet; }
        internal List<Asiakas> Asiakkaat { get => asiakkaat; }
        internal List<Toimittaja> TavaranToimittajat { get => tavaranToimittajat; set => tavaranToimittajat = value; }

        //Tämä vaihe tullaan tekemään terminaali-ikkunassa samaan tapaan kuin kaavakkeet täytetään, tietokannasta tulevilla ehdotuksilla.
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

            Console.WriteLine("Anna tuotteen nimi (esim. Toukka, 200g tai Leopardigekko, Uros");
            nimi = Console.ReadLine();
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
            TavaranToimittajat.ForEach(x => { Console.WriteLine(iter + ")" + x.Nimi); iter++; });
            index = 0;
            while (!int.TryParse(Console.ReadLine(), out index) || index >= TavaranToimittajat.Count || index < 0) ;
            Console.WriteLine("Anna saapumispäivä muodossa PP.KK.VVVV");
            saapumis_paiva = DateTime.ParseExact(Console.ReadLine(), "d", null);
            TuoteEra era = new TuoteEra(saapumis_paiva, TavaranToimittajat[index]);

            //Tuotteen luominen aiemman kyselyn perusteella
            tuotteet.Add(new Tuote(nimi, tuoteRyhma, saldo, varastoTila, hyllyPaikka, hinta_sisaan, hinta_ulos, era));
        }
    }

}
