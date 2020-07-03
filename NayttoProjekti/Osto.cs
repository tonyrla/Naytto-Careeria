using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace NayttoProjekti
{
    [Serializable()]
    class Osto : ISerializable
    {
        static int counter = 0;
        int id;
        Toimittaja toimittaja;
        DateTime tilausPvm;
        DateTime toimitusPvm;
        List<Tuote> tilauksenTuotteet;

        public Osto(Toimittaja toimittaja, DateTime tilausPvm, DateTime toimitusPvm, List<Tuote> tilauksenTuotteet)
        {
            this.id = counter;
            this.toimittaja = toimittaja;
            this.tilausPvm = tilausPvm;
            this.toimitusPvm = toimitusPvm;
            this.tilauksenTuotteet = tilauksenTuotteet;
            counter++;
        }
        public Osto(SerializationInfo info, StreamingContext ctxt)
        {
            this.id = (int)info.GetValue("Id", typeof(int));
            this.toimittaja = (Toimittaja)info.GetValue("Toimittaja", typeof(Toimittaja));
            this.tilausPvm = (DateTime)info.GetValue("TilausPVM", typeof(DateTime));
            this.toimitusPvm = (DateTime)info.GetValue("ToimitusPVM", typeof(DateTime));
            this.tilauksenTuotteet = (List<Tuote>)info.GetValue("TilatutTuotteet", typeof(List<Tuote>));
            if (counter <= this.id)
                counter = id + 1;
        }

        //Serialization function.
        public void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            info.AddValue("Id", this.id);
            info.AddValue("Toimittaja", this.toimittaja);
            info.AddValue("TilausPVM", this.tilausPvm);
            info.AddValue("ToimitusPVM", this.toimitusPvm);
            info.AddValue("TilatutTuotteet", this.tilauksenTuotteet);

        }
    }
    
}
