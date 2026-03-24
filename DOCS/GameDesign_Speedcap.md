# Game Design Document — [Feature Naam]

> **Instructie:** Kopieer dit bestand en hernoem het naar `GameDesign_[FeatureNaam].md`.
> Vul alle secties zo volledig mogelijk in voordat je begint met ontwikkelen.

---

## 1. Overzicht

| Veld             | Invullen                                      |
| ---------------- | --------------------------------------------- |
| **Feature Naam** | Speedcap                                      |
| **Auteur**       | Thomas & Jeffrey                              |
| **Datum**        | 04-03-2026                                    |
| **Versie**       | _1.0_                                         |
| **Branch**       | `Feature/Speedcap`                            |
| **Status**       | ✅ Afgerond                                   |

---

## 2. User Story

> Als player wil ik een betere launch zodat de bal niet door de bumbers heen gaat op hoge snelheiden.

---

## 3. Beschrijving

_Deze Feature maakt het zodat De launch niet infinite door kan gaan en zodat de bal niet door objecten gaat en dat je kan stoppen met schieten zodat je meer kan nadenken over je schot_

---

## 4. Gameplay Impact

### 4.1 Kernmechanisme

_De speler ziet dat de launch limited is en dat de hit box goed werkt en hij weet dat hij kan cancelen als hij wilt stoppen met schieten_

### 4.2 Relatie met bestaande systemen


| Bestaand Systeem   | Relatie / Impact               |
| ------------------ | ------------------------------ |
| Shoot              | een cancel is toegevoegd  |

### 4.3 Game Feel


| Feedback Type | Beschrijving                       |
| ------------- | ---------------------------------- |
| Visueel       | hij ziet dat de bal niet meer door objecten gaat |
---

## 5. Regels & Parameters

_Definieer de concrete spelregels en instelbare waarden voor deze feature._

| Parameter            | Waarde  | Beschrijving                    |
| -------------------- | ------- | ------------------------------- |
| Cancel               | rechtermuisknop  & x knop   | als je de rechter muis knop indrukt dan stop je met schieten  |

---

## 6. Visueel Ontwerp

Niet van toepassing

### Schetsen / Referenties

> _Plaats hier afbeeldingen of links naar referentiemateriaal._
> `![beschrijving](pad/naar/afbeelding.png)`

### Placeholder Art Beschrijving

Niet van toepassing

---

## 7. Audio Ontwerp


| Geluid               | Beschrijving / Karakter            | Placeholder  |
| -------------------- | ---------------------------------- | ------------ |
| N.V.T | N.V.T  |N.V.T |

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
│   Interaction Layer                 │  ☐
│   (Bumpers, Ball Physics)           │
├─────────────────────────────────────┤
│   Input & Control Layer             │  X
│   (Crosshair, Aim, Shoot)           │
└─────────────────────────────────────┘
```

### 8.2 Benodigde Events

_Welke nieuwe events worden aangemaakt? Op welke bestaande events wordt geabonneerd?_

| Event                        | Richting        | Beschrijving                  |
| ---------------------------- | --------------- | ----------------------------- |
|  `onPressFire2`       | Publish (nieuw) | _Cancels de launch_ |

### 8.3 Benodigde Scripts / Componenten

| Script / Component   | Verantwoordelijkheid                   |
| -------------------- | -------------------------------------- |
| _Shoot.cs_  | bal schieten en schieten cancelen |
| _Crosshairinput.cs_  | verwerkt de input van de controller & muis |


### 8.4 Uitschakelbaar

_Disable de shoot script._

---

## 9. Todo Lijst

_Maak een concrete checklist van alle taken die nodig zijn om deze feature te implementeren._

- [x] Game design document invullen en reviewen
- [x] Placeholder art maken / verzamelen
- [x] Placeholder audio maken / verzamelen
- [x] Script(s) aanmaken en implementeren
- [x] Events koppelen aan bestaande systemen
- [x] UI elementen toevoegen
- [x] Feature testen op bugs
- [x] Usertest uitvoeren (min. 3 spelers)
- [x] Usertest documentatie schrijven (`Usertest_[FeatureNaam].md`)
- [x] Technisch design document updaten
- [x] Code review / pull request aanmaken
- [x] _Voeg extra taken toe indien nodig_

---

## 10. Acceptatiecriteria

_Wanneer is deze feature "af"? Verwijs ook naar de [Definition of Done](./DefinitionOfDone.md)._

- [x] De user story is volledig geïmplementeerd
- [x] Alle parameters zijn instelbaar via de Unity Inspector
- [x] De feature is uitschakelbaar zonder bugs
- [x] Alle placeholder art/audio is aanwezig
- [x] Usertest is afgerond en gedocumenteerd
- [x] Geen errors of bugs in test build
- [x] Technisch design document is bijgewerkt
- [x] Pull request is goedgekeurd en gemerged naar `development`

---

## 11. Opmerkingen / Open Vragen


- _N.V.T_
