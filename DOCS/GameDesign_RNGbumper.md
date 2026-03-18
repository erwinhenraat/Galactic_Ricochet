# Game Design Document — [RNGbumper]

> **Instructie:** Kopieer dit bestand en hernoem het naar `GameDesign_[FeatureNaam].md`.
> Vul alle secties zo volledig mogelijk in voordat je begint met ontwikkelen.

---

## 1. Overzicht

| Veld             | Invullen                   |
| ---------------- | -------------------------- |
| **Feature Naam** | _RNG Bumper_               |
| **Auteur**       | _Mads Hoogeveen_           |
| **Datum**        | _3-3-2026_                 |
| **Versie**       | _1.0_                      |
| **Branch**       | `origin/feature/RNGbumper` |
| **Status**       | ✅ Afgerond                |

---

## 2. User Story

> Als speler wil ik dat als de game begint dat er RNG bumpers zijn die de bal kan aanraken.
> Als deze Bumper word aangeraakt word er een random waarde aan punten aan de speler gegeven.

---

## 3. Beschrijving

Deze feature bevat een RNG bumper prefab die er voor
zorgt als het balletje de bumper raakt, dat er dan een
random aantal aan punten tussen bepaalde eenheden word gegeven aan de speler.

---

## 4. Gameplay Impact

Deze feature zorgt ervoor dat er meer spanning en variatie in gameplay
is vanwege de random punten die kunnen worden verdiend. Spelers zullen worden aangemoedigd
om risico's te nemen door de bumper te raken, in de hoop op een hoge score.

### 4.1 Kernmechanisme

Het balletje moet de bumper hitten
het directe resultaat is een random aantal aan punten dat aan de speler word gegeven.

### 4.2 Relatie met bestaande systemen

_Geef aan welke bestaande systemen worden beïnvloed of aangevuld (bijv. Score, Combo, Lives, Multiplier, Input). Verwijs waar nodig naar het [Technisch Design](./TechnicalDesign.md)._

| Bestaand Systeem   | Relatie / Impact                                                                                     |
| ------------------ | ---------------------------------------------------------------------------------------------------- |
| Score              | Random punten word aan de speler gegeven                                                             |
| Combo / Multiplier | De mulitplier word gebruikt voor de RNG bumper zodat de punten hiervan vermedigvuldigd kunnen worden |
| Lives              | geen invloed                                                                                         |
| Input              | geen invloed                                                                                         |
| _Ander systeem_    | geen invloed                                                                                         |

### 4.3 Game Feel

_Welke feedback krijgt de speler? Denk aan: screenshake, geluid, visuele effecten, UI-updates, animaties._

| Feedback Type | Beschrijving                        |
| ------------- | ----------------------------------- |
| Visueel       | RNG bumper balletje met (?) Symbool |
| Audio         | Reward geluidje                     |
| Screenshake   | geen invloed                        |
| UI            | geen invloed                        |
| Animatie      | geen invloed                        |

---

## 5. Regels & Parameters

_Definieer de concrete spelregels en instelbare waarden voor deze feature._

| Parameter   | Waarde                     | Beschrijving                                                                                                    |
| ----------- | -------------------------- | --------------------------------------------------------------------------------------------------------------- |
| RNG Systeem | Random Nummer tussen 1-... | De RNG bumper prefab kan worden aangepast op basis van hoe laag en hoog de maximale en minmale random waarde is |

---

## 6. Visueel Ontwerp

_Voeg schetsen, wireframes, of referentiebeelden toe. Beschrijf de gewenste look & feel._

[https://cdn.discordapp.com/attachments/1316403516184723466/1478335161862062130/image.png?ex=69a80668&is=69a6b4e8&hm=5a072585c88a09326c18247d725c3e3ad10f24ef1a81be751c2b1fac90864180&](https://github.com/erwinhenraat/Galactic_Ricochet/blob/bf518f4c89d1f5be3ccfd961db4c0917fe035a38/lightrainbowRNGbumper.png)

### Schetsen / Referenties

> _Plaats hier afbeeldingen of links naar referentiemateriaal._

[lightrainbowRNGbumper.png](https://github.com/erwinhenraat/Galactic_Ricochet/blob/9b7ce014c88d6c4d54e9210f5ee047a766e8fffe/purpleRNGbumper.png)
> https://github.com/erwinhenraat/Galactic_Ricochet/blob/bf518f4c89d1f5be3ccfd961db4c0917fe035a38/lightrainbowRNGbumper.png

### Placeholder Art Beschrijving

_Beschrijf welke placeholder art nodig is om de feature te ontwikkelen en te testen._

al bestaande bumper

## 7. Audio Ontwerp

_Welke geluiden zijn nodig? Beschrijf per geluid het gewenste karakter._

| Geluid    | Beschrijving / Karakter                  | Placeholder  |
| --------- | ---------------------------------------- | ------------ |
| hit sound | appart geluid als je de rng bumper raakt | ☐ Ja / ☐ Nee |

---

## 8. Technische Overwegingen

we hebben niks anders gebruikt

### 8.1 Architectuurlaag

_In welke laag van de architectuur past deze feature? (Input & Control / Interaction / Game Logic / Feedback)_

```
┌─────────────────────────────────────┐
│   Feedback Layer                    │  [x]
│   (UI, Visuals, Sound)              │
├─────────────────────────────────────┤
│   Game Logic Layer                  │  [x]
│   (Scoring, Lives, Combos)          │
├─────────────────────────────────────┤
│   Interaction Layer                 │  [x]
│   (Bumpers, Ball Physics)           │
├─────────────────────────────────────┤
│   Input & Control Layer             │  ☐
│   (Crosshair, Aim, Shoot)           │
└─────────────────────────────────────┘
```

### 8.2 Benodigde Events

_Welke nieuwe events worden aangemaakt? Op welke bestaande events wordt geabonneerd?_

| Event             | Richting  | Beschrijving       |
| ----------------- | --------- | ------------------ |
| On hit RNG bumper | Subscribe | Get a random score |

### 8.3 Benodigde Scripts / Componenten

| Script / Component | Verantwoordelijkheid                 |
| ------------------ | ------------------------------------ |
| RNGBumper.cs       | Detectie collision, activeren effect |

### 8.4 Uitschakelbaar

_Beschrijf hoe deze feature uitgeschakeld kan worden zonder dat de rest van het spel breekt (conform de [Definition of Done](./DefinitionOfDone.md)). Welk GameObject moet gedeactiveerd worden?_

als de RNG bumpers wodern uigeschakeld kan het spel nog steeds gespeeld worden maar zonder de RNG bumpers.

## 9. Todo Lijst

_Maak een concrete checklist van alle taken die nodig zijn om deze feature te implementeren._

- [x] Game design document invullen en reviewen
- [x] Placeholder art maken / verzamelen
- [x] Placeholder audio maken / verzamelen
- [x] Script(s) aanmaken en implementeren
- [x] Events koppelen aan bestaande systemen
- [x] UI elementen toevoegen
- [x] Feature testen op bugs
- [ ] Usertest uitvoeren (min. 3 spelers)
- [ ] Usertest documentatie schrijven (`Usertest_[FeatureNaam].md`)
- [x] Technisch design document updaten
- [x] Code review / pull request aanmaken

## 10. Acceptatiecriteria

_Wanneer is deze feature "af"? Verwijs ook naar de [Definition of Done](./DefinitionOfDone.md)._

- [x] De user story is volledig geïmplementeerd
- [ ] Alle parameters zijn instelbaar via de Unity Inspector
- [x] De feature is uitschakelbaar zonder bugs
- [x] Alle placeholder art/audio is aanwezig
- [ ] Usertest is afgerond en gedocumenteerd
- [x] Geen errors of bugs in test build
- [x] Technisch design document is bijgewerkt
- [ ] Pull request is goedgekeurd en gemerged naar `development`

## 11. Opmerkingen / Open Vragen

_Noteer hier eventuele openstaande vragen, risico's of afhankelijkheden._

voor nu niet
