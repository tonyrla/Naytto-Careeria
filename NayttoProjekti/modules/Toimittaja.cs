using System;
using System.Diagnostics.Tracing;
using System.Runtime.Serialization;
using System.Transactions;

/*
 * Tietorakenne Toimittaja / Tavarantoimittaja-oliota varten
 * Ei sisällä muuta toiminnallisuutta kuin serialisoinnin ja tulostuksessa käytettävän merkkijonon.
 */
namespace NayttoProjekti
{
    [Serializable()]
    internal class Toimittaja : ISerializable
    {
        static int counter = 0;
        readonly int id;
        string nimi;
        string puhelinNumero;
        string email;

        public Toimittaja(string nimi = "Ei Saatavilla", string puhelin = "Ei saatavilla", string email = "Ei saatavilla")
        {
            this.id = counter;
            this.Nimi = nimi;
            this.puhelinNumero = puhelin;
            this.email = email;
            counter++;
        }

        public Toimittaja(SerializationInfo info, StreamingContext ctxt)
        {
            this.id = (int)info.GetValue("Id", typeof(int));
            this.nimi = (String)info.GetValue("Nimi", typeof(string));
            this.puhelinNumero = (String)info.GetValue("PuhNum", typeof(string));
            this.email = (String)info.GetValue("Email", typeof(string));
            if (counter <= this.id)
                counter = id + 1;
        }

        public void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            info.AddValue("Id", this.id);
            info.AddValue("Nimi", this.nimi);
            info.AddValue("PuhNum", this.puhelinNumero);
            info.AddValue("Email", this.email);
        }
        public override string ToString()
        {
            return $"[ ID: {id} ] Nimi: {nimi} PuhelinNum: {puhelinNumero} Email: {email}";
        }
        public int ToimittajaId => id;

        public string Nimi { get => nimi; set => nimi = value; }
    }
}