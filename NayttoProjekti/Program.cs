using System.Linq;


namespace NayttoProjekti
{    
    class Program
    {
        static void Main(string[] args)
        {
            //Luodaan uusi Valikot-olio antamalla "Kaupalle" nimi, ja käynnistetään ohjelman main loop.
            //Koska luokkaa ei käytetä sen enempää, niin oliota ei tarvitse asettaa muuttujaan.
            new Kayttoliittyma("Liskokauppa").run();

            //Graafisen terminaalin käynnistys, tämä toiminnallisuus on poistettu toistaiseksi.
            //Terminal.GUI
            //new TerminalGUI().RunGUI();
        }

    }
}
