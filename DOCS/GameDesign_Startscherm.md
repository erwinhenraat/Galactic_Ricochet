# Game Design Document — Startsherm

---

## 1. Overzicht

| Veld             | Invullen                                      |
| ---------------- | --------------------------------------------- |
| **Feature Naam** | _Startscherm_                                 |
| **Auteur**       | _Steale van Walbeek & Shannon Ruiter_         |
| **Datum**        | _18-02-2026_                                  |
| **Versie**       | _0.0_                                         |
| **Branch**       | _Feature/Startscherm_                         |
| **Status**       | ✅ Afgerond                                   |

---

## 2. User Story

> Als player wil ik een startscherm waar ik de levels kan selecteren en niet gelijk in de game gegooid wordt.

---

## 3. Beschrijving

Deze feature voegt een startscherm toe die je krijgt te zien als je de game opstart. Van dit scherm kan de user de game starten door de gegeven knop te clicken. Dit zorgt ervoor dat de user een makkelijker transitie van  het opstarten van de game naar de core van de gameplay en voorkomt dat de user overprikeld raakt door .

---

## 4. Gameplay Impact

### 4.1 Kernmechanisme

Deze feature maakt het zo dat de speler een zogenaamde _landing page_ heeft. Via deze _Landing page_ kan de speler een menu openen waar die uit een of meerdere _Levels_ kan kiezen. Als de speler een keuze maakt kan de speler op de knop drukken van de gecorreleerde _Level_, daarna wordt de speler verwijsd naar de correcte scene waarin die _Level_ zich bevindt.

### 4.2 Relatie met bestaande systemen

| Bestaand Systeem   | Relatie / Impact                                                |
| ------------------ | --------------------------------------------------------------- |
| Restart.cs         | Als de game eindigt wordt je teruggestuurt naar de startscherm. |

### 4.3 Game Feel

_Feedback die de speler krijgt van onze mechanic._

| Feedback Type | Beschrijving                               |
| ------------- | -------------------------------------------|
| Visueel       | _Thematische visualisatie van componenten_ |
| UI            | _Knop & menu voor levels_                  |
| Animatie      | _opacity-transitie van button-text_        |

---

## 5. Regels & Parameters

| Parameter            | Waarde  | Beschrijving                                                                                          |
| -------------------- | ------- | ----------------------------------------------------------------------------------------------------- |
| Timer                | 0 sec   | als de timer een bepaald raakt verandert hij de _alpha_ van de start-tekst en reset de timer zichzelf |

---

## 6. Visueel Ontwerp

### Schetsen / Referenties

> [Inpsiratie van de layout van de Startscherm](./DOCS_Content/arcadeStart.png)

## 7. Audio Ontwerp

_Audio die gebruikt wordt in de mechanic._

| Geluid               | Beschrijving / Karakter            | Placeholder  |
| -------------------- | ---------------------------------- | ------------ |
| Achtergrond muziek   | Ambiance meegeven aan de speler    | ☐ Ja / ✅ Nee|

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
│   Interaction Layer                 │  ☐
│   (Bumpers, Ball Physics)           │
├─────────────────────────────────────┤
│   Input & Control Layer             │  ✅
│   (Crosshair, Aim, Shoot)           │
└─────────────────────────────────────┘
```

### 8.2 Benodigde Events

| Event                        | Richting        | Beschrijving                                        |
| ---------------------------- | --------------- | --------------------------------------------------- |
| onReadyToRestart             | subscribe       | listens for when player runs out of lives           |
| onPressFire1                 | subscribe       | Listens if 'fire' is pressed                        |
| onGameOver                   | unsubscribe     | prevents two similar text-popups displaying at once |

### 8.3 Benodigde Scripts / Componenten

| Script / Component   | Verantwoordelijkheid                             |
| -------------------- | ------------------------------------------------ |
| ButtonPress.cs       | Transitions scene when pressed.                  |
| StartEnter.cs        | controls UI in startscreen                       |
| Scenes.cs            | prevents scenes being coupled with magic numbers |
| Restart.cs           | Transitions scene when game finishes             |
| Scorepop.cs          | Shows floating text feedback                     |

### 8.4 Uitschakelbaar

Als we de game willen laten runnen zonder deze feature moeten wij der voor zorgen dat in _Restart.cs_ op lijn 46, dat _SceneManager.LoadScene((int)Scenes.Start);_ vervangt wordt met  _SceneManager.LoadScene((int)Scenes.Main)_.

---

## 9. Todo Lijst

- [✅] Game design document invullen en reviewen
- [✅] Script(s) aanmaken en implementeren
- [✅] UI elementen toevoegen
- [✅] Feature testen op bugs
- [✅] Usertest uitvoeren (min. 3 spelers)
- [ ] Usertest documentatie schrijven (`Usertest_[FeatureNaam].md`)
- [✅] Alle documentatie dubbelchecken
- [✅] Code review / pull request aanmaken

---

## 10. Acceptatiecriteria

_Wanneer is deze feature "af"? Verwijs ook naar de [Definition of Done](./DefinitionOfDone.md)._

- [✅] De user story is volledig geïmplementeerd
- [✅] De feature is uitschakelbaar zonder bugs
- [✅] Alle placeholder art/audio is aanwezig
- [ ] Usertest is afgerond en gedocumenteerd
- [✅] Geen errors of bugs in test build
- [ ] Pull request is goedgekeurd en gemerged naar `development`

---

## 11. Opmerkingen / Open Vragen

Omdat Wij niet weten welke scene's nog moeten worden toegevoegd kunnen wij deze niet in de menu zetten en moet deze kwestie later weer in aanvraag komen.

- _…_
