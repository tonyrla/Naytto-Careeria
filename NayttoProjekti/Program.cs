using System.Linq;
/*
 * Tekstipohjainen prototyyppi moduulipohjaisesta varastonhallintajärjestelmästä, jossa voidaan tarpeen mukaan lisätä toiminnallisuutta helposti.
 * 
 * Tulevaisuuden ideana on tehdä terminaalityylinen käyttöliittymä mitä käsitellään pääsääntöisesti näppäimistön kautta, muuta hiiren käyttö on myös mahdollista.
 * Tämä tulee toimimaan todennäköisesti telnetillä ja toistaiseksi tietokanta tulee sijaitsemaan ohjelman kanssa samalla palvelimella.
 * 
 * Todelliset moduulit tulen todennäköisesti kehittämään jollakin skriptauskielellä, mikä mahdollistaa sen että koko ohjelmaa ei tarvitse rakentaa uusiksi moodulien lisäyksen takia. 
 * Tämä vähentää ongelmia ja helpottaa moduulien kehitystä uskomattoman paljon. LUA on aika hyvä ja helppo, pitää selvitellä miten liitettävyys toimii C#:n kanssa, C++ käytettäessä se oli täydellinen.
 * 
 * Tulen mahdollisesti toteuttamaan kaiken nopeutta ja ehdotonta muistiturvallisuutta vaativaa kirjastoina Rust kielellä, joita sitten kutsun tarpeen mukaan. Riippuen vähän miltä Rust vaikuttaa siinä vaiheessa, en ole seurannut kehitystä hetkeen.
 * 
 * Siihen saakka kunnes aloitan seuraavan vaiheen, kirjoitan suuria koodihirviöitä sillä idealla että niistä on sitten helppo lähteä kulkemaan kohti tietokantoja ja kunnon käyttöliittymää.
 * 
 * Ainoastaan perustoiminnalisuus on toiminnassa tässä vaiheessa, koska käytännössä jokaisessa moduulissa jokainen toiminto toimii samalla idealla ja tähän tapaan tehtynä rivejä tulee todella suuri määrä.
 * GUI vaiheeseen siirtyminen on suhteellisen nopea operaatio ja toiminnallisuus muuttuu jonkin verran siinä vaiheessa, joten en halua kirjoittaa turhaa koodia tulevaisuutta ajatellen.
 

 *      3 moduulia käytössä: Varasto, Myynti ja Toimittaja
 *      Varasto:
 *          Tuotteiden listaus, lisäys ja poisto.
 *      Myynti:
 *          Tilausten tulostus, lisäys ja poisto, sekä "Keräilylistan" tulostus, joka on kuin tilaus mutta riisuttu turhasta ja asiakkaalle kuulumattomasta tiedosta.
 *      Toimittaja:
 *          Toimittajien tulostus, lisäys ja poisto.
 */


namespace NayttoProjekti
{    
    class Program
    {
        static void Main(string[] args)
        {
            //Luodaan uusi Valikot-olio antamalla "Kaupalle" nimi, ja käynnistetään ohjelman main loop.
            //Koska luokkaa ei käytetä sen enempää, niin oliota ei tarvitse asettaa muuttujaan.
            new Valikot("Liskokauppa").run();

            //Graafisen terminaalin käynnistys, tätä en ole testaamista enempää kehittänyt tätä projektia varten.
            //new TerminalGUI().RunGUI();
        }

    }
}
