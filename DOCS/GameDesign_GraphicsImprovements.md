# Game Design Document — Graphics improvements


## 1. Overzicht

| Veld             | Invullen                                      |
| ---------------- | --------------------------------------------- |
| **Feature Naam** | Graphics Update                   |
| **Auteur**       | Ellie and Isa                       |
| **Datum**        | 31/03/2026                                  |
| **Versie**       | _1.0_                                         |
| **Branch**       | `Feature/graphics-update`                       |
| **Status**       | ✅ Afgerond |

---

## 2. User Story

> 13. as a player, I want to have clear and appealing graphics so that I can enjoy the look of the game

---

## 3. Beschrijving

Added different character graphics for the bumpers, added different sounds to the bumpers to fit the graphics and reworked the post processing system

---

## 4. Gameplay Impact

### 4.1 Kernmechanisme

the feature is passive it just improves already exisiting features

### 4.2 Relatie met bestaande systemen

_Geef aan welke bestaande systemen worden beïnvloed of aangevuld (bijv. Score, Combo, Lives, Multiplier, Input). Verwijs waar nodig naar het [Technisch Design](./TechnicalDesign.md)._

| Bestaand Systeem   | Relatie / Impact               |
| ------------------ | ------------------------------ |
| Score              | same as before_   |
| Combo / Multiplier | same as before |
| Lives              | same as before           |
| Input              | same as before       |
| _Ander systeem_    | _…_                            |

### 4.3 Game Feel

_Welke feedback krijgt de speler? Denk aan: screenshake, geluid, visuele effecten, UI-updates, animaties._

| Feedback Type | Beschrijving                       |
| ------------- | ---------------------------------- |
| Visueel       | particles on bumper hit, psot processing effects |
| Audio         | pain sounds from bumpers collision            |
| Screenshake   | same as before  |
| UI            | no color changing UI for consitency    |
| Animatie      | bumper animations upon collision with ball       |

---

## 5. Regels & Parameters

_Definieer de concrete spelregels en instelbare waarden voor deze feature._

| Parameter            | Waarde  | Beschrijving                    |
| -------------------- | ------- | ------------------------------- |
| _bijv. cooldown_     | _2 sec_ | none  |
| _bijv. puntenwaarde_ | _500_   | depends on bumper          |
| _bijv. duur_         | _5 sec_ |none |

---

## 6. Visueel Ontwerp



### Schetsen / Referenties
<img width="940" height="727" alt="Lava Alien" src="https://github.com/user-attachments/assets/09d865ee-0d19-4ce3-b0b4-458332376513" />
<img width="752" height="750" alt="Dino Alien" src="https://github.com/user-attachments/assets/9b3e37b6-dc58-446e-b9df-3f505e135729" />
<img width="791" height="844" alt="Bug Alien" src="https://github.com/user-attachments/assets/63c33b92-dd91-4477-9da7-ad474b1e17df" />
<img width="758" height="889" alt="Frog Alien" src="https://github.com/user-attachments/assets/8a648e79-0640-407a-abca-02045a876cda" />
<img width="816" height="738" alt="Eye Alien" src="https://github.com/user-attachments/assets/b394006b-13c2-4892-ad18-a00d4547f9a9" />

### Placeholder Art Beschrijving

no placeholders, art is final

---

## 7. Audio Ontwerp

_Welke geluiden zijn nodig? Beschrijf per geluid het gewenste karakter._

| Geluid               | Beschrijving / Karakter            | Placeholder  |
| -------------------- | ---------------------------------- | ------------ |
| pain1 | synthetic pain sound          | ☐ Nee |
| pain2 | synthetic pain sound          | ☐ Nee |
| pain3 | synthetic pain sound          | ☐ Nee |

---

## 8. Technische Overwegingen

### 8.1 Architectuurlaag

_In welke laag van de architectuur past deze feature? (Input & Control / Interaction / Game Logic / Feedback)_

```
┌─────────────────────────────────────┐
│   Feedback Layer                    │  
│   (UI, Visuals, Sound)              │
├─────────────────────────────────────┤
```

### 8.2 Benodigde Events

_Welke nieuwe events worden aangemaakt? Op welke bestaande events wordt geabonneerd?_

no events added

### 8.3 Benodigde Scripts / Componenten

no scripts added

### 8.4 Uitschakelbaar

bumpers are updated, they work slightly different now, the post processing is still easily disablable

## 9. Todo Lijst

_Maak een concrete checklist van alle taken die nodig zijn om deze feature te implementeren._

- [DONE] Game design document invullen en reviewen
- [DONE] Placeholder art maken / verzamelen
- [DONE] Placeholder audio maken / verzamelen
- [DONE] Script(s) aanmaken en implementeren
- [NA] Events koppelen aan bestaande systemen
- [NA] UI elementen toevoegen
- [DONE] Feature testen op bugs
- [NA] Usertest uitvoeren (min. 3 spelers)
- [NA] Usertest documentatie schrijven (`Usertest_[FeatureNaam].md`)
- [DONE] Technisch design document updaten
- [DONE] Code review / pull request aanmaken
- [NA] _Voeg extra taken toe indien nodig_

---

## 10. Acceptatiecriteria

_Wanneer is deze feature "af"? Verwijs ook naar de [Definition of Done](./DefinitionOfDone.md)._

- [DONE] De user story is volledig geïmplementeerd
- [DONE] Alle parameters zijn instelbaar via de Unity Inspector
- [DONE] De feature is uitschakelbaar zonder bugs
- [DONE] Alle placeholder art/audio is aanwezig
- [NA] Usertest is afgerond en gedocumenteerd
- [DONE] Geen errors of bugs in test build
- [DONE] Technisch design document is bijgewerkt
- [DONE] Pull request is goedgekeurd en gemerged naar `development`

---

## 11. Opmerkingen / Open Vragen

_Noteer hier eventuele openstaande vragen, risico's of afhankelijkheden._

- _…_
