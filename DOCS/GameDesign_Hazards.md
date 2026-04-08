# Game Design Document — [Feature Naam]

---

## 1. Overzicht

| Veld             | Invullen                                      |
| ---------------- | --------------------------------------------- |
| **Feature Naam** | _lazer Hazard_                                |
| **Auteur**       | _steale van Walbeek & Shannon Ruiter_         |
| **Datum**        | _11-03-2026_                                  |
| **Versie**       | _1.0_                                         |
| **Branch**       | `Feature/Hazards`                             |
| **Status**       | 📝 Concept / 🔨 In ontwikkeling / ✅ Afgerond |

---

## 2. User Story

als player wil ik niet dat alle combo rewards positief zijn dus wil ik een instant death feature zodat de ball dan gelijk verdwijnt

---

## 3. Beschrijving

[!] Deze feature voegt een _hazard-object_ toe die door het speelveld heen schiet en de bal laat verdwijnen op contact.

---

## 4. Gameplay Impact

### 4.1 Kernmechanisme

Tijdens het spelen neemt de speler een _Waarschuwings-Teken_ waar aan de zijkant van de scherm. Na een bepaald aantal tijd instantiate de _hazard-object_ en beweegt het object langs de scherm van de directie van de _Waarschuwings-Teken_. als de bal de _hazard-object_ raakt verdwijnt de bal, verliest de speler zijn combo en een van zijn levens.

Na een bepaald aantal tijd speelt de routine weer opnieuw af in een random locatie.

### 4.2 Relatie met bestaande systemen

_Geef aan welke bestaande systemen worden beïnvloed of aangevuld (bijv. Score, Combo, Lives, Multiplier, Input). Verwijs waar nodig naar het [Technisch Design](./TechnicalDesign.md)._

| Bestaand Systeem   | Relatie / Impact                                             |
| ------------------ | ------------------------------------------------------------ |
| Combo / Multiplier | _reset combo bij missen bij impact van Hazard-Object en bal_ |
| SoundFX            | _SFX toegevoegd_                                             |
| Lives              | _Haalt leven weg bij impact van Hazard-Object en bal_        |

### 4.3 Game Feel

_Welke feedback krijgt de speler? Denk aan: screenshake, geluid, visuele effecten, UI-updates, animaties._

| Feedback Type | Beschrijving                                              |
| ------------- | --------------------------------------------------------- |
| Visueel       | _User krijgt waarschuwing van directie van Hazard-Object_ |
| Audio         | _SFX bij 'Waarschuwing, Hazard fired en Ball destroyed'_  |

---

## 5. Regels & Parameters

_Definieer de concrete spelregels en instelbare waarden voor deze feature._

| Parameter            | Waarde  | Beschrijving                    |
| -------------------- | ------- | ------------------------------- |
| _bijv. cooldown_     | _2 sec_ | _Tijd voordat het opnieuw kan_  |
| _bijv. puntenwaarde_ | _500_   | _Punten per activatie_          |
| _bijv. duur_         | _5 sec_ | _Hoe lang het effect actief is_ |

---

## 6. Visueel Ontwerp

### Schetsen / Referenties

> _Plaats hier afbeeldingen of links naar referentiemateriaal._
> [Concept idee van de feature](./DOCS_Content/GalacticHazard_Concept.png)
[Idee van de graphics van de warning sign](./DOCS_Content/GalacticWarning_Concept.png)

### Placeholder Art Beschrijving

- Visual van de _Warning-sign_
- visual van de _Hazard-Object_
- VFX voor impat van _Hazard-Object_ en _bal_

---

## 7. Audio Ontwerp

| Geluid               | Beschrijving / Karakter            | Placeholder  |
| -------------------- | ---------------------------------- | ------------ |
| Ball_Destroyed.SFX   | _Korte, punchy synth hit_          | ☐ Ja / ☐ Nee |
| Hazard_Fired.SFX     | _Zacht ambient hum tijdens actief_ | ☐ Ja / ☐ Nee |
| Warning_Sign.SFX     | _Alarmerend waarschuw geluid_      | ☐ Ja / ☐ Nee |

---

## 8. Technische Overwegingen

### 8.1 Architectuurlaag

```
┌─────────────────────────────────────┐
│   Feedback Layer                    │  ✅
│   (UI, Visuals, Sound)              │
├─────────────────────────────────────┤
│   Game Logic Layer                  │  ✅
│   (Scoring, Lives, Combos)          │
├─────────────────────────────────────┤
│   Interaction Layer                 │  ✅
│   (Bumpers, Ball Physics)           │
├─────────────────────────────────────┤
│   Input & Control Layer             │  ☐
│   (Crosshair, Aim, Shoot)           │
└─────────────────────────────────────┘
```

### 8.2 Benodigde Events

_Welke nieuwe events worden aangemaakt? Op welke bestaande events wordt geabonneerd?_

| Event                        | Richting        | Beschrijving                            |
| ---------------------------- | --------------- | --------------------------------------- |
| `onTimerHit`                 | Publish         | _Fired bij activering van warningteken_ |
| `onHazardWarning`            | Publish         | _Fired als laser geinstantieerd wordt_  |
| `onBallDestroyed`            | Publish         | _Fired als laser en bal impact maken_   |
| `onGetScore`                 | Subscribe       | _Luistert naar behaalde score waarde_   |

### 8.3 Benodigde Scripts / Componenten

| Script / Component   | Verantwoordelijkheid                                            |
| -------------------- | --------------------------------------------------------------- |
| Score.cs             | _Bepaald score-treshhold voor waneer de laser begint met vuren_ |
| HazardObject.cs      | _Geeft functionaliteit aan Hazard-Object_                       |
| HazardSpawner.cs     | _Bepaald waar en waneer hazard-object inspawned_                |

### 8.4 Uitschakelbaar

_Beschrijf hoe deze feature uitgeschakeld kan worden zonder dat de rest van het spel breekt (conform de [Definition of Done](./DefinitionOfDone.md)). Welk GameObject moet gedeactiveerd worden?_

---

## 9. Todo Lijst

_Maak een concrete checklist van alle taken die nodig zijn om deze feature te implementeren._

- [ ] Game design document invullen en reviewen
- [ ] Placeholder art maken / verzamelen
- [ ] Placeholder audio maken / verzamelen
- [ ] Script(s) aanmaken en implementeren
- [ ] Events koppelen aan bestaande systemen
- [ ] UI elementen toevoegen
- [ ] Feature testen op bugs
- [ ] Usertest uitvoeren (min. 3 spelers)
- [ ] Usertest documentatie schrijven (`Usertest_[FeatureNaam].md`)
- [ ] Technisch design document updaten
- [ ] Code review / pull request aanmaken
- [ ] _Voeg extra taken toe indien nodig_

---

## 10. Acceptatiecriteria

_Wanneer is deze feature "af"? Verwijs ook naar de [Definition of Done](./DefinitionOfDone.md)._

- [ ] De user story is volledig geïmplementeerd
- [ ] Alle parameters zijn instelbaar via de Unity Inspector
- [ ] De feature is uitschakelbaar zonder bugs
- [ ] Alle placeholder art/audio is aanwezig
- [ ] Usertest is afgerond en gedocumenteerd
- [ ] Geen errors of bugs in test build
- [ ] Technisch design document is bijgewerkt
- [ ] Pull request is goedgekeurd en gemerged naar `development`

---

## 11. Opmerkingen / Open Vragen

_Noteer hier eventuele openstaande vragen, risico's of afhankelijkheden._

- _…_
