using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

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
        DateTime pvm;

        public Myynti(int myyjaId, Asiakas asiakas, List<Tuote> myydytTuotteet, DateTime pvm)
        {
            this.id = counter;
            this.myyjaId = myyjaId;
            this.asiakas = asiakas;
            this.myydytTuotteet = myydytTuotteet;
            this.pvm = pvm;
            counter++;
        }
        public Myynti(SerializationInfo info, StreamingContext ctxt)
        {
            this.id = (int)info.GetValue("Id", typeof(int));
            this.myyjaId = (int)info.GetValue("MyyjanId", typeof(int));
            this.asiakas = (Asiakas)info.GetValue("Asiakas", typeof(Asiakas));
            this.myydytTuotteet = (List<Tuote>)info.GetValue("MyydytTuotteet", typeof(List<Tuote>));
            this.pvm = (DateTime)info.GetValue("PVM", typeof(DateTime));
            if (counter <= this.id)
                counter = id + 1;
        }

        //Serialization function.
        public void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            info.AddValue("Id", this.id);
            info.AddValue("MyyjaId", this.myyjaId);
            info.AddValue("Asiakas", this.asiakas);
            info.AddValue("MyydytTuotteet", this.myydytTuotteet);
            info.AddValue("PVM", this.pvm);
        }
    }
}
