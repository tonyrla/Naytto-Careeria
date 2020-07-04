using System;
using System.Runtime.Serialization;

namespace NayttoProjekti
{
    [Serializable()]
    internal class Asiakas : ISerializable
    {

        static int counter = 0;
        readonly int id;
        string nimi;
        string puhelinNumero;
        string email;
        string osoite;
        string lisaTietoja;


        public Asiakas(string nimi, string puhelinNumero, string email, string osoite, string lisaTietoja)
        {
            this.id = counter;
            this.nimi = nimi;
            this.puhelinNumero = puhelinNumero;
            this.email = email;
            this.osoite = osoite;
            this.lisaTietoja = lisaTietoja;
            counter++;
        }
        public Asiakas(SerializationInfo info, StreamingContext ctxt)
        {
            this.id = (int)info.GetValue("Id", typeof(int));
            this.nimi = (String)info.GetValue("Nimi", typeof(string));
            this.puhelinNumero = (String)info.GetValue("PuhNum", typeof(string));
            this.email = (String)info.GetValue("Email", typeof(string));
            this.osoite = (String)info.GetValue("Osoite", typeof(string));
            this.lisaTietoja = (String)info.GetValue("Lisatietoja", typeof(string));
            if (counter <= this.id)
                counter = id + 1;
        }

        //Serialization function.
        public void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            info.AddValue("Id", this.id);
            info.AddValue("Nimi", this.nimi);
            info.AddValue("PuhNum", this.puhelinNumero);
            info.AddValue("Email", this.email);
            info.AddValue("Osoite", this.osoite);
            info.AddValue("Lisatietoja", this.lisaTietoja);
        }
        public override string ToString()
        {
            return "ID: " + id + " Nimi: " + nimi + " Puh: " + puhelinNumero + "\n\tEmail: " + email + " Osoite: " + osoite + "\n\tLisätietoja: " + lisaTietoja;
        }
    }
}