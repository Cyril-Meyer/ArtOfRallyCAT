# ArtOfRallyCATransmission
ArtOfRally Control for Automatic Transmission

This mod allows to use the automatic transmission mod, while allowing a manual modification of the gear engaged.
The mod allows for example to force a downshift of the gear  after braking in a climb.

### Usage

1. Download the [mod file](https://github.com/Cyril-Meyer/ArtOfRallyCATransmission/releases/tag/v1.0)
2. Download [Unity Mod Manager](https://www.nexusmods.com/site/mods/21)
3. Install the mod using Unity Mod Manager
4. Enable / Disable the mod using Unity Mod Manager

### How it works ?

In the original code, the button for switching gear are only checked if the drivetrain is set to manual.
```
if (!this.drivetrain.automatic)
{
  [...] // checking this.player.GetButtonDown to apply gear changes
}
```

The IL code for the condition is the following.
```
155	01AB	ldarg.0
156	01AC	ldfld	class Drivetrain CarController::drivetrain
157	01B1	ldfld	bool Drivetrain::automatic
158	01B6	brtrue	218 (0257) ret 
```

The mod remove the `158	01B6	brtrue	218 (0257) ret` line to avoid the check, which make the following result.
```
this.drivetrain.automatic;
[...] // checking this.player.GetButtonDown to apply gear changes
```
