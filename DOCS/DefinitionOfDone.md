# Definition of Done

## De feature is ontwikkeld op de correcte feature branch

- Gebruik voor de naam van de branch `feature/[featureNaam]` (lowercase)

## Folderstructuur

- Todo's en assets zijn verwerkt binnen de juiste folderstructuur

```
Assets/                                    # Hoofdmap van het Unity-project
├── Animations/                            # Animator controllers en animatieclips
│   ├── AC_bumper.controller
│   └── AN_bumper.anim
├── GameData/                              # Speldata en configuratiebestanden
│   └── Physics/                           # Physics materials voor botsingsgedrag
│       └── PM_ball.physicsMaterial2D
├── Graphics/                              # Alle visuele assets (afbeeldingen, fonts, sprites)
│   ├── Background/                        # Achtergrondafbeeldingen voor het speelveld
│   │   ├── BG_space.jpg
│   │   ├── BG_space_2.jpg
│   │   └── ...
│   ├── Fonts/                             # Lettertypen en bijbehorende SDF-assets voor TextMeshPro
│   │   ├── ARCADECLASSIC.TTF
│   │   ├── Cosmic-Cube.ttf
│   │   └── ...
│   ├── GraphicsData/                      # Render-instellingen en volume profiles
│   │   └── Global Volume Profile.asset
│   └── Sprites/                           # 2D sprites voor game-objecten en UI
│       ├── SP_arrow.png
│       ├── SP_ball.png
│       ├── ...
│       └── Bumper/                        # Sprite sheet frames voor de bumper-animatie
│           ├── SP_bumper_0001.png
│           ├── SP_bumper_0002.png
│           └── ...
├── Materials/                             # Unity materials (momenteel leeg)
│   (leeg)
├── Prefabs/                               # Herbruikbare prefab-objecten
│   ├── ArtOnly/                           # Prefabs met alleen visuele assets (momenteel leeg)
│   │   (leeg)
│   └── Usable/                            # Functionele prefabs voor gebruik in de scene
│       ├── PF_ball.prefab
│       ├── PF_bumper.prefab
│       └── PF_cannon.prefab
├── Scenes/                                # Unity scenes
│   └── Galactic_Ricochet.unity
├── Scripts/                               # Alle C#-scripts, ingedeeld per domein
│   ├── Audio/                             # Scripts voor geluid en muziek
│   │   └── PlaySounds.cs
│   ├── Gameplay/                          # Kernlogica van het spel (scores, levens, schieten, etc.)
│   │   ├── Aim.cs
│   │   ├── Combo.cs
│   │   └── ...
│   ├── Input/                             # Scripts voor spelerinvoer en besturing
│   │   ├── CrosshairInput.cs
│   │   └── SelectInitials.cs
│   └── Visuals/                           # Scripts voor visuele effecten (screenshake, popups, etc.)
│       ├── AnimationOffset.cs
│       ├── ComboMood.cs
│       └── ...
├── Sounds/                                # Geluidseffecten en achtergrondmuziek
│   ├── AF_boom.wav
│   ├── AF_bumper.aiff
│   └── ...
├── TextMeshPro/                           # Niet gebruiken
└── TutorialInfo/                          # Niet gebruiken
```

## Naming conventions

Algemeen:

- Taal: Engels
- Bestanden gebruiken een **prefix** die het assettype aangeeft, gevolgd door een underscore en een beschrijvende naam

### Prefixes per assettype

| Prefix | Type                | Voorbeeld                   |
| ------ | ------------------- | --------------------------- |
| `AC_`  | Animator Controller | `AC_bumper.controller`      |
| `AN_`  | Animation Clip      | `AN_bumper.anim`            |
| `PM_`  | Physics Material    | `PM_ball.physicsMaterial2D` |
| `BG_`  | Background Image    | `BG_space.jpg`              |
| `SP_`  | Sprite              | `SP_arrow.png`              |
| `PF_`  | Prefab              | `PF_ball.prefab`            |
| `AF_`  | Audio Effect (SFX)  | `AF_boom.wav`               |
| `AL_`  | Audio Loop (muziek) | `AL_bg_loop.wav`            |

### Scripts

- PascalCasing, geen prefix: `AnimationOffset.cs`, `GameManager.cs`

### Scenes

- PascalCasing met underscores: `Galactic_Ricochet.unity`

### Sprite sheets (animatieframes)

- Prefix + naam + underscore + volgnummer (4 cijfers): `SP_bumper_0001.png`

## Art

- Art assets voldoen aan de eisen van de styleguide
- Visuele consistentie met de rest van de assets en/of de art direction
- `Polygonen` en `Polyflow` correct (geen dubbele / flipped faces)
- Geen onnodige drawcalls , texture atlassing en single materials per object
- Game assets zijn verwerkt tot prefabs in Unity en staan in de juiste folder
- Game assets hebben zonodig: controllers animaties materials en of shaders in de engine.

## De code voldoet aan basic code conventions voor C# en Unity

- Check [hier](https://github.com/djsjollema/lessen-gamedevelopment/tree/main/M6/PROG/01_Code_Conventions) de code conventions

## De feature is grondig getest op bugs

- De feature is ook getest op bugs door anderen
- De feature is getest in een test build
- Voor alle Bugs zijn er issues aangemaakt en deze zijn ook allemaal weer afgesloten bij het oplossen
- De feature bevat geen error's
- De feature bevat geen Bugs
- De feature is getest in Unity versie **6000.3.8**

## De feature is grondig ge-usertest op fun, usability en playability

- Er is door minimaal 3 verschillende spelers getest
- Hierbij is de speler niet geholpen tenzij deze vast kwam te zitten
- Hierbij heeft de speler hardop gedacht
- Hierbij zijn notities gemaakt van opmerkingen van de speler en observaties van het test team (geen conclusies)
- Hierbij zijn videos van de speler gemaakt (gezicht en handen)
- Hierbij zijn screen captures gemaakt
- Hierbij zijn de videos en screen captures bewerkt tot een gesynchroniseerde video
- Hierbij is er een vragenlijst ingevuld
- Er is een analyse gedaan van al het test materiaal met onderouwde conclusies
- De usertest documentatie is terug te vinden in de file `Usertest_[FeatureNaam].md`

## De feature heeft een duidelijk en concreet game design

- Er is een [game design formulier](.//GameDesign_Template.md) ingevuld
- Er is een Userstory voor de feature gescherven
- Er in een complete todo lijst gemaakt voor de userstory
- De Game Gesign Documentatie van de feature is terug te vinden in de file `GameDesign_[FeatureNaam].md`

## De feature is volledig afgewerkt met correcte placeholders

- De feature bevat passende placeholder art
- De feature bevat passende placeholders voor visuele effecten
- De feature bevat passende placeholder audio
- De feature bevat een passende UI

## De code is voldoet aan alle technische eisen

- De code past binnen de [bestaande architectuur](./TechnicalDesign.md)
- De feature is succesvol uit te schakelen zonder dat de game breekt of er bugs ontstaan. Dit kan door het gameobject te deactiveren. Dit is ook grondig getest.
- De geschreven code is verwerkt in een update van alle onderdelen van het [technisch design document](./TechnicalDesign.md).

---

## Pull request maken

_Heb je aan de bovenstaande voorwaarden voldaan?_

Zorg dat je de laatste aanpassingen op de development branch "Pulled" naar jouw feature branch.

gebruik in github desktop `branch > update from development`

of op de commandline :

```
git checkout jouw-branch-naam
git fetch origin
git merge origin/development

```

![update from dev](./src/update_from_development.png)

Los eventuele conflicten op, voeg ze toe met git add . en voltooi met git commit.

Nadat je dit hebt gedaan maak je een [pull request](https://github.com/erwinhenraat/Galactic_Ricochet/pulls) aan.\_

---

## De pull request is goedgekeurd

- De game is getest op de development branch werkt zoals het moet:
  - geen bugs
  - geen conflicten
  - geen merge errors
