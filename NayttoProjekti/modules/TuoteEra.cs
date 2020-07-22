using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Runtime.Serialization;
using System.Text;

/*
 * 
 * 
 */
namespace NayttoProjekti
{
    [Serializable()]
    class TuoteEra : ISerializable
    {
        static int counter = 0;
        readonly int era_numero;
        DateTime saapumis_paiva;
        Toimittaja toimittaja;
        public int Era_Numero { get => era_numero; }

        public TuoteEra(DateTime saapumis_paiva, Toimittaja toimittaja)
        {
            this.era_numero = counter;
            this.saapumis_paiva = saapumis_paiva;
            this.toimittaja = toimittaja;
            counter++;
        }
        public TuoteEra(SerializationInfo info, StreamingContext ctxt)
        {
            this.era_numero = (int)info.GetValue("Id", typeof(int));
            this.saapumis_paiva = (DateTime)info.GetValue("Saapuminen", typeof(DateTime));
            this.toimittaja = (Toimittaja)info.GetValue("Toimittaja", typeof(Toimittaja));

            if (counter <= this.era_numero)
                counter = era_numero + 1;
        }

        public void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            info.AddValue("Id", this.era_numero);
            info.AddValue("Saapuminen", this.saapumis_paiva);
            info.AddValue("Toimittaja", this.toimittaja);
        }
        public override string ToString()
        {
            return "Erä: " + era_numero + " Saapunut " + saapumis_paiva.ToString("d") + " Toimittaja: " + toimittaja.ToimittajaId + " | " + toimittaja.Nimi;
        }
    }
}
