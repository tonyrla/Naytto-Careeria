#Varastonhallintasovellus - Careeria Ohjelmoinnin perusteet C#
========================

Sovelluksen toiminta perustuu tekstipohjaiseen käyttöliittymään joka kyselee käyttäjältä mihin moduuliin siirrytään työskentelemään ja moduulien sisällä taas kysellään mitä toimintoja käytetään.
Sovelluksessa voidaan lisätä, poistaa ja listata tuotteita varastossa, sekä suorittaa joitain myyntiin ja tavarantoimittajiin liittyviä toimintoja.


Käyttöliittymä toimii pyyhkimällä konsolin jokaisen toiminnon laukaisemisen yhteydessä ja listaa käytettävissä olevat toiminnot tai moduulit käyttäjälle, pitäen näin käyttöliittymän siistinä.

Käytössä olevat moduulit ovat:
* Varasto - Varastonhallinta
	* Tuotteiden listaus
	* Tuotteen lisääminen
	* Tuotteen poisto
* Myynti - Myyntitilausten hallinta
	* Tilausten tulostus
	* Tilauksen lisääminen - Tilauksen lisääminen vähentää Varastossa olevien tuotteiden määrää.
	* Tilauksen poistaminen - Tilauksen poistaminen palauttaa tuotteet varaston saldoihin
	* Keräilylistojen tulostus, joka on myyntitilaus josta on poistettu asiakkaalle kuulumaton tieto
* Toimittaja - Tavarantoimittajien hallinta
	* Toimittajien tulostus
	* Toimittajan lisäys
	* Toimittajan poisto

Tietorakenteena käytetään useimmissa tapauksissa __List__-taulukkoa ajonaikaiseen käyttöön ja ohjelmasta hallitusti poistuttaessa serialisoin datan jokaisen moduulin omaan JSON tiedostoon käyttäen newtonsoftin json-kirjastoa, mitkä ladataan muistiin ohjelman käynnistyessä. Tilauksissa käytetään __SortedList__-taulukkoa jotta voidaan hallita tulostusta ja säilömistä paremmin.

Toiminallisuutta testatteassa huomasin että paras tapa taata ohjelman toiminnallisuus on rajoittaa käyttäjän vaihtoehdot syöttää merkkejä, joten ainoastaan listatut pikakomennot hyväksytään komentorivillä, paitsi tietoja syotettäessä.

Enemmän dokumentaatiota näyttötutkintoon kuuluvasta ohjelmasta löytyy lähdekoodista.

## Selitystä valitsemastani tavasta ohjelmoida kyseinen ohjelma
Tarkoituksenani on tulevaisuudessa laajentaa ohjelma vanhaan BBS tyyliseen käyttöliittymään, jota on tarkoitus käsitellä pääsääntöisesti näppämistöllä pikanäppäimiä käyttäen, mutta toiminnallisuus hiirelle löytyy myös. 
Tämän vuoksi koodi näyttää tältä. Koen helpommaksi rakentaa graafisen käyttöliittymän testiversion päälle, kuin suunnitella sen.

Oikeiden tietokantojen käyttöön tullessa, otan käyttöön myös kenttiin täyttyvät autocomplete ehdotukset tietokannasta. Kokemuksesta tiedän että tuhansien tuotteiden tai tuotenumeroiden muistaminen on hankalaa. mutta manuaalinen myynti tuotenumeroilla on vain niin paljon nopeampaa ettei kannata lähteä muihin ratkaisuihin.


Yksi ominaisuuksista tulee olemaan modulaarinen rakennustapa järjestelmälle, jossa voidaan lisätä lisää ominaisuuksia LUA:lla tai muulla skriptauskielellä, tai miksei vaikka C#:lla. Tämä mahdollistaa sen ettei koko ohjelmaa tarvitse aina kääntää uudelleen ja tämä myös vähentää bugien määrää, kun kaikki joudutaan rakentamaan osa kerrallaan.
Tavoitteena on siis että itse ohjelmakoodiin ei tarvitsisi koskea turhaan.

Raskaimmat ja suurinta muistiturvallisuutta vaativimmat toiminnot tulen todennäköisesti kirjoittamaan Rust-ohjelmointikielellä, jos C#:n kantokyky ei riitä itselleni.

