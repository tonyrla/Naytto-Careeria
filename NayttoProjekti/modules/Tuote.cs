using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

/*
 * 
 * 
 */

namespace NayttoProjekti
{
    [Serializable()]
    class Tuote : ISerializable
    {
        static int counter = 0;
        readonly int id;
        string nimi;
        Tuoteryhma tuoteRyhma;
        int saldo;
        VarastoTila varastoTila;
        int hyllyPaikka;
        decimal hinta_sisaan;
        decimal hinta_ulos;
        TuoteEra era;
        public int Saldo { get => saldo; set => saldo = value; }

        public Tuote(string nimi, Tuoteryhma tuoteRyhma, int saldo, VarastoTila varastoTila, int hyllyPaikka, decimal hinta_sisaan, decimal hinta_ulos, TuoteEra era)
        {
            this.id = counter;
            this.nimi = nimi;
            this.tuoteRyhma = tuoteRyhma;
            this.saldo = saldo;
            this.varastoTila = varastoTila;
            this.hyllyPaikka = hyllyPaikka;
            this.hinta_sisaan = hinta_sisaan;
            this.hinta_ulos = hinta_ulos;
            this.era = era;
            counter++;
        }
        public decimal HintaUlos { get => hinta_ulos; }
        public string Nimi { get => nimi; }
        public Tuote(Tuote t)
        {
            this.id = t.id;
            this.nimi = t.nimi;
            this.tuoteRyhma = t.tuoteRyhma;
            this.saldo = t.saldo;
            this.varastoTila = t.varastoTila;
            this.hyllyPaikka = t.hyllyPaikka;
            this.hinta_sisaan = t.hinta_sisaan;
            this.hinta_ulos = t.hinta_ulos;
            this.era = t.era;

        }
        public Tuote(SerializationInfo info, StreamingContext ctxt)
        {
            this.id = (int)info.GetValue("Id", typeof(int));
            this.nimi = (String)info.GetValue("Nimi", typeof(string));
            this.tuoteRyhma = (Tuoteryhma)info.GetValue("TuoteRyhma", typeof(Tuoteryhma));
            this.saldo = (int)info.GetValue("Saldo", typeof(int));
            this.varastoTila = (VarastoTila)info.GetValue("Varasto", typeof(VarastoTila));
            this.hyllyPaikka = (int)info.GetValue("Hyllypaikka", typeof(int));
            this.hinta_sisaan = (Decimal)info.GetValue("HintaSisaan", typeof(Decimal));
            this.hinta_ulos = (Decimal)info.GetValue("HintaUlos", typeof(Decimal));
            this.era = (TuoteEra)info.GetValue("Era", typeof(TuoteEra));

            if (counter <= this.id)
                counter = id + 1;
        }

        public void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            info.AddValue("Id", this.id);
            info.AddValue("Nimi", this.nimi);
            info.AddValue("TuoteRyhma", this.tuoteRyhma);
            info.AddValue("Saldo", this.saldo);
            info.AddValue("Varasto", this.varastoTila);
            info.AddValue("Hyllypaikka", this.hyllyPaikka);
            info.AddValue("HintaSisaan", this.hinta_sisaan);
            info.AddValue("HintaUlos", this.hinta_ulos);
            info.AddValue("Era", this.era);
        }
        public override string ToString()
        {
            return "[" + id + " | " + nimi + " | " + tuoteRyhma + "] " + "Saldo: " + saldo + " Sijainti: " + varastoTila + " - Hylly " + hyllyPaikka + "\n\tOsto= " + @hinta_sisaan.ToString("C", CultureInfo.GetCultureInfo("fi-FI")) + ", Myynti= " + @hinta_ulos.ToString("C", CultureInfo.GetCultureInfo("fi-FI")) + "\n\tErätiedot: " + era;
        }
        public string asiakasString()
        {

            return "[" + id + " | " + nimi + " | Erä: " + era.Era_Numero + "] " + "Määrä: " + saldo + " Hinta/me: " + @hinta_ulos.ToString("C", CultureInfo.GetCultureInfo("fi-FI")) + " Yhteensä: " + saldo*hinta_ulos;
        }

        public override bool Equals(object obj)
        {
            return obj is Tuote tuote &&
                   id == tuote.id &&
                   nimi == tuote.nimi;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(id, nimi);
        }
    }

}
