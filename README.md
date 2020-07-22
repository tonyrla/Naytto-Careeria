# Naytto-Careeria
Varastonhallintasovellus
========================

Sovelluksen toiminta perustuu tekstipohjaiseen käyttöliittymään joka kyselee käyttäjältä mihin moduuliin siirrytään työskentelemään ja moduulien sisällä taas kysellään mitä toimintoja käytetään.
Sovelluksessa voidaan lisätä, poistaa ja listata tuotteita varastossa, sekä suorittaa joitain myyntiin ja tavarantoimittajiin liittyviä toimintoja.

Käyttöliittymä toimii pyyhkimällä konsolin jokaisen toiminnon laukaisemisen yhteydessä ja listaa käytettävissä olevat toiminnot tai moduulit käyttäjälle, näinollen pitäen käyttöliittymän siistinä.

Käytössä olevat moduulit ovat:
	Varasto - Varastonhallinta
		Tuotteiden listaus
		Tuotteen lisääminen
		Tuotteen poisto
	Myynti - Myyntitilausten hallinta
		Tilausten tulostus
		Tilauksen lisääminen - Tilauksen lisääminen vähentää Varastossa olevien tuotteiden määrää.
		Tilauksen poistaminen - Tilauksen poistaminen palauttaa tuotteet varaston saldoihin
		Keräilylistojen tulostus, joka on myyntitilaus josta on poistettu asiakkaalle kuulumaton tieto
	Toimittaja - Tavarantoimittajien hallinta
		Toimittajien tulostus
		Toimittajan lisäys
		Toimittajan poisto

Tietorakenteena käytän useata List-oliota ajonaikaiseen käyttöön ja ohjelmasta hallitusti poistuttaessa serialisoin datan jokaisen moduulin omaan JSON tiedostoon käyttäen newtonsoftin json kirjastoa, mitkä ladataan muistiin ohjelman käynnistyessä.

Toiminallisuutta testatteassa huomasin että paras tapa taata ohjelman toiminnallisuus on rajoittaa käyttäjän vaihtoehdot syöttää merkkejä, joten ainoastaan listatut pikakomennot hyväksytään komentorivillä, paitsi tietoja syotettäessä.

Enemmän dokumentaatiota löytyy lähdekoodista.
