# Game Design Document — Rails

## 1. Overzicht

| Veld             | Invullen                                      |
| ---------------- | --------------------------------------------- |
| **Feature Naam** | Rails                                         |
| **Auteur**       | Ellie                                         |
| **Datum**        | 10/3/2026                                     |
| **Versie**       | 1.0                                           |
| **Branch**       | feature/rails                                   |
| **Status**       | ✅Afgerond                                   |

---

## 2. User Story

> as a player, I want to shoot my ball into a rail to move it quickly across the level.

---

## 3. Beschrijving

This feature adds a pinball style rail that the ball enters from one side, rolls through the rail and exits out the other side. This lets the player reposition the ball quickly and safely.

---

## 4. Gameplay Impact

### 4.1 Kernmechanisme

To interact with the rail the ball needs to hit the entrance of the rail and that starts the roll along the rail then the ball will exit from the other end of the rail.

### 4.2 Relatie met bestaande systemen

_Geef aan welke bestaande systemen worden beïnvloed of aangevuld (bijv. Score, Combo, Lives, Multiplier, Input). Verwijs waar nodig naar het [Technisch Design](./TechnicalDesign.md)._

| Bestaand Systeem   | Relatie / Impact               |
| ------------------ | ------------------------------ |
| Score              | Does not impact score          |
| Combo / Multiplier | Does not impact combo          |
| Lives              | Does not impact lives          |
| Input              | no extra input is needed       |
| _Ander systeem_    | the rail is self contained it only interacts with the ball and sound|

### 4.3 Game Feel

_Welke feedback krijgt de speler? Denk aan: screenshake, geluid, visuele effecten, UI-updates, animaties._

| Feedback Type | Beschrijving                       |
| ------------- | ---------------------------------- |
| Visueel       | particles on entrance and roll     |
| Audio         | entrance SFX and roll SFX          |
| Screenshake   | no screenshake                     |
| UI            | no UI changes                      |
| Animatie      | no animation                       |

---

## 5. Regels & Parameters

_Definieer de concrete spelregels en instelbare waarden voor deze feature._

| Parameter            | Waarde  | Beschrijving                    |
| -------------------- | ------- | ------------------------------- |
| _bijv. cooldown_     | _2 sec_ | does not have a use cooldown    |
| _bijv. puntenwaarde_ | _500_   | does not require points to activate|
| _bijv. duur_         | _5 sec_ | the effect duration depends on the ball speed and rail length |

---

## 6. Visueel Ontwerp

_Voeg schetsen, wireframes, of referentiebeelden toe. Beschrijf de gewenste look & feel._

### Schetsen / Referenties

> _Plaats hier afbeeldingen of links naar referentiemateriaal._
<img width="458" height="602" alt="image" src="https://github.com/user-attachments/assets/4792697f-df53-4e60-8bab-96ee5404fb2a" />


### Placeholder Art Beschrijving

there are no placeholders, the rails are final

## 7. Audio Ontwerp

_Welke geluiden zijn nodig? Beschrijf per geluid het gewenste karakter._

| Geluid               | Beschrijving / Karakter            | Placeholder  |
| -------------------- | ---------------------------------- | ------------ |
| enter SFX            | 3 part chip pulse                  |  ☐ Nee |
| Roll SFX             | randomized noise                   |  ☐ Nee |

---

## 8. Technische Overwegingen

### 8.1 Architectuurlaag

_In welke laag van de architectuur past deze feature? (Input & Control / Interaction / Game Logic / Feedback)_

```
├─────────────────────────────────────┤
│   Interaction Layer                 │
│   (Bumpers, Ball Physics)           │
├─────────────────────────────────────┤
```

### 8.2 Benodigde Events

_Welke nieuwe events worden aangemaakt? Op welke bestaande events wordt geabonneerd?_

| Event                        | Richting        | Beschrijving                  |
| ---------------------------- | --------------- | ----------------------------- |
| _bijv. `onIsOnRail`_         | Publish (nieuw) | _Fired when the ball is on rail entrance |
| _bijv. `onRailPlaySound`_    | Publish (nieuw) | _Fired when the ball has entered the rail   |
| _bijv. `onRailStopSound`_    | Publish (nieuw) | _Fired when the ball has exited the rail   |

### 8.3 Benodigde Scripts / Componenten

| Script / Component   | Verantwoordelijkheid                   |
| -------------------- | -------------------------------------- |
| _bijv. BallController.cs_   | Ball state manager |
| _bijv. RailController.cs_ | rail ball detetion and rail creation manager      |
| _changed PlaySounds.cs_ | Added new sounds      |
| _changed PlayArea.cs_ | changed to not destroy ball on rail      |

### 8.4 Uitschakelbaar

_Beschrijf hoe deze feature uitgeschakeld kan worden zonder dat de rest van het spel breekt (conform de [Definition of Done](./DefinitionOfDone.md)). Welk GameObject moet gedeactiveerd worden?_

the rails can be easily disabled or removed

## 9. Todo Lijst

_Maak een concrete checklist van alle taken die nodig zijn om deze feature te implementeren._

- [done] Game design document invullen en reviewen
- [done] Placeholder art maken / verzamelen
- [done] Placeholder audio maken / verzamelen
- [done] Script(s) aanmaken en implementeren
- [done] Events koppelen aan bestaande systemen
- [NA] UI elementen toevoegen
- [done] Feature testen op bugs
- [TBD] Usertest uitvoeren (min. 3 spelers)
- [TBD] Usertest documentatie schrijven (`Usertest_[FeatureNaam].md`)
- [done] Technisch design document updaten
- [TBD] Code review / pull request aanmaken
- [NA] _Voeg extra taken toe indien nodig_

---

## 10. Acceptatiecriteria

_Wanneer is deze feature "af"? Verwijs ook naar de [Definition of Done](./DefinitionOfDone.md)._

- [YES] De user story is volledig geïmplementeerd
- [YES] Alle parameters zijn instelbaar via de Unity Inspector
- [YES] De feature is uitschakelbaar zonder bugs
- [YES] Alle placeholder art/audio is aanwezig
- [NO] Usertest is afgerond en gedocumenteerd
- [YES] Geen errors of bugs in test build
- [YES] Technisch design document is bijgewerkt
- [NO] Pull request is goedgekeurd en gemerged naar `development`

---

## 11. Opmerkingen / Open Vragen

NOTES:
- use the unity spline editor and use AUTO spline mode (NOT BEZIER) otherwise visual artifacts will occur
- do not forcefully move the object, use the spline editor to move elements
