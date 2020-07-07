using System.Linq;
using NayttoProjekti.modules;
/*
 * Tekstipohjainen ensimmäisen vaiheen prototyyppi moduulipohjaisesta varastonhallintajärjestelmästä, jossa voidaan tarpeen mukaan lisätä toiminnallisuutta helposti.
 * 
 * Todelliset moduulit tulen todennäköisesti kehittämään jollakin skriptauskielellä, mikä mahdollistaa sen että koko ohjelmaa ei tarvitse rakentaa uusiksi moodulien lisäyksen takia. 
 * Tämä vähentää ongelmia ja helpottaa moduulien kehitystä uskomattoman paljon. LUA on aika hyvä ja helppo, pitää selvitellä miten liitettävyys toimii C#:n kanssa, C++ käytettäessä se oli täydellinen.
 * 
 * Tulevaisuuden ideana on tehdä terminaalityylinen käyttöliittymä mitä käsitellään pääsääntöisesti näppäimistön kautta, muuta hiiren käyttö on myös mahdollista.
 * Tämä tulee toimimaan todennäköisesti telnetillä ja toistaiseksi tietokanta tulee sijaitsemaan ohjelman kanssa samalla palvelimella.
 * 
 * Tulen mahdollisesti toteuttamaan kaiken nopeutta ja ehdotonta muistiturvallisuutta vaativaa kirjastoina Rust kielellä, joita sitten kutsun tarpeen mukaan. Riippuen vähän miltä Rust vaikuttaa siinä vaiheessa, en ole seurannut kehitystä hetkeen.
 * 
 * Siihen saakka kunnes aloitan seuraavan protovaiheen, kirjoitan suuria koodihirviöitä sillä idealla että niistä on sitten helppo lähteä kulkemaan kohti tietokantoja ja kunnon käyttöliittymää.
 * 
 */


namespace NayttoProjekti
{
    enum Tuoteryhma
    {
        RuokaHyonteiset,
        LisaRavinteet,
        Koristeet,
        Lemmikit,
        Tuntematon
    }
    enum VarastoTila
    {
        Jaakaappi,
        Kuivatila,
        Halli,
        Tuntematon
    }

    // Tällä hetkellä moduulien vaihtamiset hoidetaan Enumien kautta, luettavuuden ja ymmärtämisen takia.
    //      Tulevaisuudessa tämä tulee tapahtumaan skripteillä, joissa moduulien logiikka tulee olemaan ja itse ohjelma tulee vain hallitsemaan yhteyksiä, tietokantoja jne.
    enum Moduuli
    {
        Lopeta,
        Paanakyma,
        Varastonakyma,
        Myyntinakyma,
        Ostonakyma,
        Toimittajanakyma,
        Asiakasnakyma
    }
    class Program
    {
        static void Main(string[] args)
        {
            new TerminalGUI().RunGUI();
            //new Valikot("Liskokauppa").run();
        }

    }
}
