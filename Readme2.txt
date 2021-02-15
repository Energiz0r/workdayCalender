
Dette er min Rema1000 løsning ("Det enkle er ofte det beste") :)

En løsning i denne type case er alltid å kjøre så lav entropi som mulig og ticke til man finner en løsning. 
Denne var ganske enkel, og fikk en løsning fort. 
Problemet med denne løsnignen er at den har dårlig performance når workdaysToAdd blir høyt.
performance blir O=(n*m*p) hvor n er workdaysToAdd, m er timer per arbeidsdag, p er fritid som den må ticke over
Så dette må løses ved neste iterasjon

Her støtter man ikke SOLID prinsippenen veldig bra heller