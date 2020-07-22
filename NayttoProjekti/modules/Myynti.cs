using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

/*
 * Tietorakenne Myynti / Tilaus-oliota varten
 * Ei sisällä muuta toiminnallisuutta kuin serialisoinnin ja tulostuksessa käytettävän merkkijonon.
 */
namespace NayttoProjekti
{
    [Serializable()]
    class Myynti : ISerializable
    {

        static int counter = 0;
        readonly int id;
        readonly int myyjaId;
        Asiakas asiakas;
        List<Tuote> myydytTuotteet;
        bool keratty;
        DateTime myyntiPvm;
        DateTime toimitusPvm;

        public int TilausId { get => this.id; }
        public int MyyjaId { get => this.myyjaId; }
        public Asiakas Tilaaja { get => this.asiakas; }
        public List<Tuote> TilatutTuotteet { get => this.myydytTuotteet; }
        public DateTime ToimitusPvm { get => this.toimitusPvm;  }

        public Myynti(int myyjaId, Asiakas asiakas, List<Tuote> myydytTuotteet, DateTime pvm)
        {
            this.id = counter;
            this.myyjaId = myyjaId;
            this.asiakas = asiakas;
            this.myydytTuotteet = myydytTuotteet;
            this.toimitusPvm = pvm;
            this.myyntiPvm = DateTime.Now;
            this.keratty = false;
            counter++;
        }
        public Myynti(SerializationInfo info, StreamingContext ctxt)
        {
            this.id = (int)info.GetValue("Id", typeof(int));
            this.myyjaId = (int)info.GetValue("MyyjaId", typeof(int));
            this.asiakas = (Asiakas)info.GetValue("Asiakas", typeof(Asiakas));
            this.myydytTuotteet = (List<Tuote>)info.GetValue("MyydytTuotteet", typeof(List<Tuote>));
            this.toimitusPvm = (DateTime)info.GetValue("ToimitusPVM", typeof(DateTime));
            this.myyntiPvm = (DateTime)info.GetValue("MyyntiPVM", typeof(DateTime));
            this.keratty = (bool)info.GetValue("Keratty", typeof(bool));
            if (counter <= this.id)
                counter = id + 1;
        }

        public void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            info.AddValue("Id", this.id);
            info.AddValue("MyyjaId", this.myyjaId);
            info.AddValue("Asiakas", this.asiakas);
            info.AddValue("MyydytTuotteet", this.myydytTuotteet);
            info.AddValue("ToimitusPVM", this.toimitusPvm);
            info.AddValue("MyyntiPVM", this.myyntiPvm);
            info.AddValue("Keratty", this.keratty);
        }
        public override string ToString()
        {
            return $"[ Myyjä : {myyjaId}]\n" +
                "\tAsiakas: " + asiakas + "\n\t" +
                string.Join(",", myydytTuotteet) + "\n\t" +
                "Toim.Pvm: " + toimitusPvm.Date + " Tilaus Pvm: " + myyntiPvm.Date + "\n\t" +
                "Tilaus kerätty: " + (keratty ? "K" : "E") + "\n";
        }
    }
}
