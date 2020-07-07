# 914Escape
914Escape is a plugins that allows the use of SCP-914 to escape when in an emergency.

# Main Features
- Ability to get teleported to a random location in the facility.
- Ability to apply a damage penalty upon teleport

# Default Config:
```yaml
escape_enabled: true # Enable or disable the plugin.
escape_teleport_points: LC_CAFE,CROSSING # Possible teleport locations. (List at the bottom of page)
escape_damage_human: 50 # Amount of damage done to humans.
escape_damage_scp: 50 # Amount of damage done to SCPs.
escape_damage_type: 0 # The type of damage done. (0 for percentage of current health, 1 for damage done in HP count)
escape_914_setting: 1 # The 914 setting to trigger the teleport.
```

# 914 Settings:
```
Rough = 0
Coarse = 1
One to One = 2
Fine = 3
Very Fine = 4
```

# All Teleport Locations:
```
topsite
CROSSING
LC_CAFE
LC_914_CR
HC_079_HALL
HC_079_MON
HC_079_CR
HC_TESLA_B
HC_106_CR
HC_SERVERS
HC_096_CR
nukesite
HC_457_CR
LC_ARMORY
Shelter
Straight_4
Offices_PCs
Offices_upstair
Smallrooms2
```
