# Game Design Document — Flippers

---

## 1. Overzicht

| Veld             | Invullen                                      |
| ---------------- | --------------------------------------------- |
| **Feature Naam** |Flippers                  |
| **Auteur**       | Ellie                      |
| **Datum**        | 24/03/26                                  |
| **Versie**       | 1.0                                         |
| **Branch**       | `Feature/flippers`                       |
| **Status**       | ✅ Afgerond |

---

## 2. User Story

> As a player I want to use a flipper to quickly re-direct my ball.

---

## 3. Beschrijving

Adds flippers to the game that can quickly re-direct the direction of the ball and push it back up from the bottom

---

## 4. Gameplay Impact

### 4.1 Kernmechanisme

the flipper is passive, it does not require input, when the ball hits the flipper it will be launched according to the flippers start angle

### 4.2 Relatie met bestaande systemen

_Geef aan welke bestaande systemen worden beïnvloed of aangevuld (bijv. Score, Combo, Lives, Multiplier, Input). Verwijs waar nodig naar het [Technisch Design](./TechnicalDesign.md)._

| Bestaand Systeem   | Relatie / Impact               |
| ------------------ | ------------------------------ |
| Score              | No change   |
| Combo / Multiplier | No change |
| Lives              | No change           |
| Input              | No input       |

### 4.3 Game Feel

_Welke feedback krijgt de speler? Denk aan: screenshake, geluid, visuele effecten, UI-updates, animaties._

| Feedback Type | Beschrijving                       |
| ------------- | ---------------------------------- |
| Visueel       | has trails to show movement |
| Audio         | has a hit sound            |
| Screenshake   | no screenshake  |
| UI            | no UI    |
| Animatie      | has a flip animation        |

---

## 5. Regels & Parameters

_Definieer de concrete spelregels en instelbare waarden voor deze feature._

| Parameter            | Waarde  | Beschrijving                    |
| -------------------- | ------- | ------------------------------- |
| _bijv. cooldown_     | NAN | N/A  |
| _bijv. puntenwaarde_ | NAN   | N/A          |
| _bijv. duur_         | NAN | N/A |

---

## 6. Visueel Ontwerp

_Voeg schetsen, wireframes, of referentiebeelden toe. Beschrijf de gewenste look & feel._

### Schetsen / Referenties

<img width="657" height="328" alt="image" src="https://github.com/user-attachments/assets/8745b3e8-e108-4319-b26a-f34c92eb530c" />


### Placeholder Art Beschrijving

No placeholders

---

## 7. Audio Ontwerp

_Welke geluiden zijn nodig? Beschrijf per geluid het gewenste karakter._

| Geluid               | Beschrijving / Karakter            | Placeholder  |
| -------------------- | ---------------------------------- | ------------ |
| _bijv. hit SFX_ | short 8-bit tom drum          | ☐ Nee |

---

## 8. Technische Overwegingen

### 8.1 Architectuurlaag

_In welke laag van de architectuur past deze feature? (Input & Control / Interaction / Game Logic / Feedback)_

```
┌─────────────────────────────────────┐
│   Feedback Layer                    │  ☐
│   (UI, Visuals, Sound)              │
├─────────────────────────────────────┤
│   Game Logic Layer                  │  ☐
│   (Scoring, Lives, Combos)          │
├─────────────────────────────────────┤
│   Interaction Layer                 │  < here
│   (Bumpers, Ball Physics)           │
├─────────────────────────────────────┤
│   Input & Control Layer             │  ☐
│   (Crosshair, Aim, Shoot)           │
└─────────────────────────────────────┘
```

### 8.2 Benodigde Events

_Welke nieuwe events worden aangemaakt? Op welke bestaande events wordt geabonneerd?_

| Event                        | Richting        | Beschrijving                  |
| ---------------------------- | --------------- | ----------------------------- |
| _bijv. `onFlipperPlaySound`_ | Publish (nieuw) | activated on flipper collision |

### 8.3 Benodigde Scripts / Componenten

| Script / Component   | Verantwoordelijkheid                   |
| -------------------- | -------------------------------------- |
| _bijv. FlipperController.cs_   | controls the flipper logic |
| _bijv. FlipperCollision.cs_ | collision detection for the flipper      |

### 8.4 Uitschakelbaar

the flippers can be disabled or deleted, they are independent from other systems

---

## 9. Todo Lijst

_Maak een concrete checklist van alle taken die nodig zijn om deze feature te implementeren._

- [done] Game design document invullen en reviewen
- [done] Placeholder art maken / verzamelen
- [done] Placeholder audio maken / verzamelen
- [done] Script(s) aanmaken en implementeren
- [done] Events koppelen aan bestaande systemen
- [N/A] UI elementen toevoegen
- [done] Feature testen op bugs
- [N/A] Usertest uitvoeren (min. 3 spelers)
- [N/A] Usertest documentatie schrijven (`Usertest_[FeatureNaam].md`)
- [done] Technisch design document updaten
- [done] Code review / pull request aanmaken
- [done] _Voeg extra taken toe indien nodig_

---

## 10. Acceptatiecriteria

_Wanneer is deze feature "af"? Verwijs ook naar de [Definition of Done](./DefinitionOfDone.md)._

- [yes] De user story is volledig geïmplementeerd
- [yes] Alle parameters zijn instelbaar via de Unity Inspector
- [yes] De feature is uitschakelbaar zonder bugs
- [yes] Alle placeholder art/audio is aanwezig
- [yes] Usertest is afgerond en gedocumenteerd
- [yes] Geen errors of bugs in test build
- [yes] Technisch design document is bijgewerkt
- [yes] Pull request is goedgekeurd en gemerged naar `development`

---

## 11. Opmerkingen / Open Vragen

none

- _…_
