using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


namespace Wizard_Of_Legend_Clarity_Mod {
    public class ClarityMod: Partiality.Modloader.PartialityMod {

        public static Dictionary<string, string> CustomItemDescriptions = new Dictionary<string, string>()
        {
            {"DamageUp","Increases <color=#f93d09>all Damage by 8%</color>!" }, // Amulet of sundering
            {"FireDamageUp","Increase <color=#c60d0d>Fire damage by 12%</color>." }, //Ifrit's Matchstick
            {"AirDamageUp","Increase <color=#749ecc>Air damage by 12%</color>." }, //Rudra's Pinwheel
            {"EarthDamageUp","Increase <color=#844205>Earth damage by 12%</color>." }, //Gaia's Shovel
            {"LightningDamageUp","Increase <color=#ffff00ff>Lightning damage by 12%</color>." }, //Battery Of Taranis
            {"WaterDamageUp","Increase <color=#3c57e0>Water damage by 12%</color>. Increases <color=#1b78a0>Ice</color> some as well." }, //Shiva's Water Bottle
            {"IceStanza","Increase <color=#1b78a0>Ice damage by 15%</color>. Does NOT Increase <color=#3c57e0>Water</color> damage. First half of the [<color=#FFCC00>Poem of Fiery Rime</color>]." }, //Stanza of Frost
            {"BurnLevelUp","Increases your <color=#c60d0d>Burn</color> status effects by 1 Level! (Lv2 28 damage, Lv3 36 damage, Lv4 42 Damage, Lv5 48 damage)" }, //Agni's Sparkler
            {"ShockLevelUp","Increases your <color=#ffff00ff>Shock</color> status effects by 1 Level! (Each level adds 0.25 longer stun time from shock, and 4 extra total damage)" }, //Indra's Copper Plate
            {"PoisonLevelUp","Increases your <color=#8c0497>Poison</color> status effects by 2 levels! (Lv3 <color=#8c0497>Poison</color> is 35 damage, Lv4 40 damage, Lv5 50 damage)" }, //Devi's Bug Spray
            {"BurnChanceUp","Adds a <color=#c60d0d>6.25% chance to Burn</color> foes with all arcana!" }, //Phoenix Talon
            {"SlowChanceUp","Adds a <color=#749ecc>10% chance to Slow foes by 37.5% for 1.5 seconds</color> with all arcana!" }, //Nerite Shell
            {"PoisonChanceUp","Adds a <color=#8c0497>6.25% chance to Poison</color> foes with all arcana!" }, //Noxious Dappercap
            {"ShockChanceUp","Adds a <color=#ffff00ff>6.25% chance to Shock</color> foes with all arcana!" }, //Tesla Coil
            {"FreezeChanceUp","Adds a <color=#1b78a0>6.25% chance to Freeze</color> foes with all arcana!" }, // Permafrost Cube
            {"CritChanceUp","Increases <color=#e8d925>Critical hit chance by 8%</color>!" }, //Analytical Monocle
            {"MeleeCritChanceUp","Triples the <color=#e8d925>Critical hit chance</color> for all <color=#9d8766>Melee</color> arcana! Stacks with other Items." }, //Dark Katana
            {"DamageUpAtLowHealth","Increases <color=#f93d09>ALL Damage by 20%</color> while below <color=#f90233>30% Health</color>!" }, //Berserker's Helm
            {"DamageUpWithSameElement","For every arcana of the same element, increases <color=#f93d09>ALL Damage by 4%</color> for that element." }, //Royal Flush
            {"DamageUpWithDiffElement","Increases <color=#f93d09>ALL Damage by 4%</color> for every arcana of a diffrent element you have!" }, //Captain's Ring
            {"DamageUpWithGold","Every <color=#c9a110>25 Gold</color> increases <color=#f93d09>ALL Damage by 1%</color>! Caps at <color=#f93d09>20% Damage</color>! at <color=#c9a110>500 Gold</color>." }, //Dagger of Midas
            {"BuffWithFriendship","When in Co-op, Increases <color=#f93d09>ALL Damage by 8%</color>, <color=#13a835>15% Movement Speed</color>." }, //Friendship Bracelet
            {"BuffWithOverdrive","While signature arcana is charged Increase <color=#f93d09>ALL Damage by 8%</color> and <color=#13a835>Movement Speed by 16%</color>!" }, //Autograph Pad
            {"SevenFlushFire","Holding seven <color=#c60d0d>Fire</color> arcana increases damage by 21%! All arcana <color=#c60d0d>Burn</color> foes for 16 damage over 2 seconds!" }, //Neve's Ruby
            {"SevenFlushLightning","Holding seven <color=#ffff00ff>Lightning</color> arcana increases <color=#e8d925>Critical Chance</color> by 28%! All arcana <color=#ffff00ff>Shock</color> foes, Dealing 10 damage and <color=#ffff00ff>Stunning</color> for 0.625 seconds!" }, //Neve's Citrine
            {"LightningAura","Summons an aura that calls down <color=#ffff00ff>Lightning</color> on nearby enemies! <color=#ffff00ff>Stuns for 0.625 seconds, with 10 total damage</color>." }, //Sleepy Thunderstone
            {"FrostAura","Summons an aura that <color=#1b78a0>Freezes</color> nearby enemies!" }, //Lotus Froststone
            {"FireAura","Summons an aura that deals <color=#c60d0d>Fire Damage</color> to nearby enemies!" }, //Cobalt Firestone
            {"DamageUpWithInventoryCount","Increase <color=#f93d09>ALL Damage by 1.25%</color> for each relic in your inventory! (This counts towards itself)" }, //Merchant's Cart
            {"DamageUpWithKills","Increases <color=#f93d09>ALL Damage by 1%</color> for every enemy defeated, but resets when taking damage! Caps at <color=#f93d09>20% Bonus Damage</color>." }, //Sinister Ledger
            {"CritAfterEvade","After <color=#b5e61d>Evadeing</color> an attack, all of your attacks are <color=#e8d925>Critical hits</color> for 3 seconds!" }, //Covert Ops Mask
            {"ExplosionOnKillChance","Enemies have a <color=#c60d0d>20% chance to Explode</color> when defeated!" }, //Whimsical Explodium
            {"DamageUpWithCC","Against foes inflicted with status ailments, Increase <color=#f93d09>ALL Damage by 12%</color>!" }, //Hunter's Stiletto
            {"EmpowerArcanaOnStageLoad","Randomly enhances an arcana at the beginning of each stage! (Effect is temporary)" }, //Fortune Cookie
            {"DamageUpWhenHurt","Increases <color=#f93d09>all Damage by 20%</color>! for 3 seconds after taking damage!" }, //Berserker's Axe
            {"BasicCancelIncreasesCrit","Following up a basic arcana with another arcana increases its <color=#e8d925>Critical hit chance by 25%</color>!" }, //Tapping Gloves
            {"CritUpAtLowHealth","Increases <color=#e8d925>Critical hit chance by 25%</color> when below <color=#f90233>30% Health!</color>" }, //Auditor's Talisman
            {"BlackWolfCoat","Increases <color=#c60d0d>Fire</color> and <color=#ffff00ff>Lightning</color> arcana damage by 15%, but receive more damage from <color=#3c57e0>Water</color>, <color=#1b78a0>Ice</color> and <color=#844205>Earth</color> attacks!" }, //Ebon Wolf's Cloak
            {"EmpowerAllArcanaAtMaxHealth","All arcana are enhanced when at full health!" }, //Idealist's Mirror
            {"OverdriveDoubler","Your <color=#0060f0>Signature Charges 25% Slower, Decays 50% Faster</color>, but can now be <color=#0060f0>Used twice on a single charge</color>!" }, //Secret Wild Card
            {"CritDamageUp","Increases <color=#e8d925>Critical hit damage by 50%</color>! Stacks with other items, but is Additive not Multiplicative." }, //Assassin's Blade
            {"WanderersFigurine","Increases the damage of your <color=#0060f0>Signature arcana by 20%</color>! Core of the [<color=#FFCC00>Perfect Time Crystal</color>." }, //Wanderer's Mechanism
            {"DamageUpWithMapReveal","Increases <color=#f93d09>ALL Damage by upto 4%</color> based on map% revealed! Damage increase stacks every floor. (No damage cap)" }, //Cartographer's Quill
            {"FreezeOnKillChance","Defeated enemies have a <color=#c60d0d>20% chance to Explode</color> and then <color=#1b78a0>Freeze</color> nearby enemies!" }, //Special Snowflake
            {"OutOfCombatCrit","Not using arcana for 7 Seconds causes your next spell to be a <color=#e8d925>Critical hit</color>!" }, //Singing Bowl
            {"DoubleAttackChance","All <color=#512323>melee</color> basic arcana have a secondary hit! Second hit deals <color=#512323>25% Damage</color>." }, //Sidewinder's Badge
            {"SuperCritChance","Adds a 2% chance of dealing 300 bonus damage with your <color=#e8d925>Critical hits!</color>" }, //Dice of the Nemesis
            {"ODIncreaseOnUse","Your charged <color=#0060f0>Signature arcana deals 2%</color> more damage every time you use it! Caps at <color=#0060f0>40% Bonus</color>" }, //Ancient Fountain Pen
            {"HealthUp","Increases <color=#f90233>Max Health by 10%</color>!" }, //Giant's Heart
            {"ArmorUp","Reduces damage taken by 5%" }, //Euphie's Shawl
            {"ArmorUpAtLowHealth","When below <color=#f90233>30% Health</color>, Take 20% less damage!" }, //Armor of Resolve
            {"EvadeUp","Increases <color=#b5e61d>Evade by 8%</color>! Stacks with other <color=#b5e61d>Evade</color> items." }, //Antiquated Tabi
            {"MoveSpeedUp","Increases <color=#13a835>Movement Speed by 12%</color>!" }, //Mercury's Sandals
            {"IgnorePits","Briefly hover over pits!" }, //Hummingbird Feather
            {"DashInvulnerability","<color=#b5e61d>Evade</color> all attacks while dashing!" }, //Leemo's Leaf
            {"ShockShield","Release a burst of <color=#ffff00ff>Lightning</color> when taking damage! Has a 16 Second cooldown" }, //Lei's Drum
            {"BurnImmune","Prevents <color=#c60d0d>Burn</color> status effect!" }, //Super Sunscreen
            {"PoisonImmune","Prevents <color=#8c0497>Poison</color> status effect!" }, //Gummy Vitamins
            {"FreezeImmune","Prevents <color=#1b78a0>Freeze</color> status effect!" }, //Fuzzy Handwarmers
            {"EvadeCrit","<color=#b5e61d>Evade</color> all critical hits!" }, //Puffy Parka
            {"CapDamageItem","Reduces damage received to a maximum of <color=#f90233>10% of Maximum Health</color>!" }, //Limited Edition Robe
            {"HealOnPlatPickup","Picking up chaos gems <color=#f90233>heals for 3 Health!</color> Excludes gems collected from Council members." }, //Jewelry Box
            {"SevenFlushAir","Holding <color=#749ecc>Seven Air</color> arcana increases <color=#13a835>Movement Speed by 28%</color> and <color=#b5e61d>Evasion by 21%!</color> All arcana also <color=#749ecc>Slow foes by 50% for 1.75 seconds</color>!" }, //Neve's Quartz
            {"SevenFlushEarth","Holding <color=#844205>Seven Earth</color> arcana increase <color=#f90233>Max Health by 21%</color> Reduces damage taken by 21% and lowers stun and knockback effects by 49%!" }, //Neve's Emerald
            {"PaperDefense","Every point of damage received adds a page to this thesis! Every 200 pages reduces ALL DAMAGE taken by 1! (Maximum 600 pages)" }, //Thesis on Defense
            {"SequenceDefense","Memorizes the last three attacks taken and reduces damage taken by 16% from attacks of the same type!" }, //Memory Chainmail
            {"OverdriveDefense","Damage received is expended from your <color=#0060f0>Signature</color> meter while you have sufficient charge to shield the attack!" }, //Cushioned Flip-flops
            {"ReflectProjectileShield","Summons a shield that reflects one projectile! Shield breaks after reflect and respawns 18 seconds later." }, //Mirror Shield
            {"ShieldWithOverdriveTimeOut","Gain a shield every time a charged signature is not activated and times out!" }, //Ring of Recycling
            {"OverhealGrantsShield","When healed for more than your maximum health, gain a shield equal to half of the excess heal amount!" }, //Takeout Box
            {"HealWithMapReveal","On clearing a stage, <color=#f90233>receive a heal</color> based on the map reveal percentage!" }, //Pathfinder's Knapsack
            {"EnhanceDash","Increases the distances traveled by dash arcana by 35%!" }, //Greased Boots
            {"FireResistanceUp","Reduces damage taken from <color=#c60d0d>Fire by 25%</color>." }, //Resolute Svalinn
            {"WaterResistanceUp","Reduces damage taken from <color=#3c57e0>Water by 25%</color>." }, //Three Gorges Bulwark
            {"EarthResistanceUp","Reduces damage taken from <color=#844205>Earth by 25%</color>." }, //Auger of Poetry
            {"ElementalResistanceWithArcana","Arcana of matching elements reduce damage from that element! Requires two or more arcana of the same element. 5% per arcana." }, //Tracking Suit
            {"DefenseUpOnTakeDamage","Reduce damage taken by 20% for 3 seconds after taking damage!" }, //Calcifying Bonemail
            {"IncreaseBuffDuration","Increases the duration of buff arcana by 40%!" }, //Demi's Teapot
            {"MoveSpeedUpPerCD","Increase <color=#13a835>Movement Speed by 10%</color> for every arcana on cooldown." }, //Spell Thief's Socks
            {"OverdriveToHealth","Using a fully charged signature arcana <color=#f90233>heals</color> you instead of producing a signature spell!" }, //Regenerative Inkwell
            {"ParrySidestep","Pressing forward into an attack immediately before impact allows you to guard the attack! Doing so grants <color=#0060f0>15% Signature meter</color>!" }, //Bracers of the Beast
            {"ResurrectArcanaLoss","Sacrifice all standard arcana in your hand to revive from defeat! Revive with <color=#f90233>5% Health</color> per arcana lost." }, //Scissors of Vitality
            {"DestroyEnemyProjectilesOnHit","Destroys all projectiles in the area when hit by a projectile!" }, //Deafening Cymbals
            {"ArmorUpWithInventoryCount","Decreases damage taken by 1%, Plus another 1% for each relic held!" }, //Heavy Travel Jacket
            {"EvadeUpAtLowHealth","When below <color=#f90233>30% Health</color>, Gain <color=#b5e61d>25% Evade chance</color>! Stacks with other evasion items." }, //Calvin's Sandy Shoes
            {"MindControlOnHurt","<color=#ff00aa>30% chance to Charm</color> enemies when taking damage! <color=#ff00aa>Charm</color> lasts 8 Seconds." }, //Charming Teddy Bear
            {"DeathProofCrown","When you would die, instead <color=#f90233>heal 5% Health</color>, gain full signature charge and become invincible for 3.5 seconds! Can be re-used" }, //Kali's Flower Diadem
            {"RootShield","Releases a grove of <color=#844205>Rooting Vines</color> when taking damage! 20 second cooldown." }, //Reactive Vinemail
            {"PotionPack","Revive with <color=#f90233>20% Health</color> when defeated! This relic creates health orbs when dropped and is destroyed on use." }, //Pazu's Favorite Hat
            {"HealDropBoostItem","<color=#f90233>Health Orbs</color> drop 30% more frequently!" }, //Raspberry Cookie Box
            {"HealOnCrit","Regenerate <color=#f90233>1 Health</color> every time you land a <color=#e8d925>Critical hit!</color>" }, //Vampire's Eyeglasses
            {"InvulnerableOnMultiHurt","Briefly become invulnerable after taking damage in quick succession!" }, //Stygian Turtle Shell
            {"StatueCamo","Remaining still causes you to disappear from sight! Exiting stealth gives cooldown reduction on the first arcana used." }, //Elven Ears
            {"MeleeBasicNegatesProjectiles","Allows you to destroy enemy <color=#214051>Projectiles</color> with <color=#512323>Melee</color> basic arcana!" }, //Flak Gauntlet
            {"MaxHealthUpOnTakeDamage","Taking damage increases your <color=#f90233>Max Health by 2</color>! Caps at <color=#f90233>75 bonus Max Health!</color>" }, //Super Carrot Cake
            {"MaxHealthUpOnHeal","All healing increases your <color=#f90233>Max Health by 3</color>! Caps at <color=#f90233>75 bonus Max Health!</color>" }, //Tea of Mercy
            {"ReduceCooldown","Reduces all cooldowns by 8%!" }, //Roxel's Pendulum
            {"TozysPocketWatch","Reduces cooldowns by 20%, but also reduces <color=#0060f0>Signature charge rate by 25%</color>!" }, //Tozy's Pocket Watch
            {"KillsLowerCDs","Defeating foes lowers active cooldowns by 1 Second!" }, //Destructive Abacus
            {"KnockbackUp","Increases knockback when striking foes!" }, //Mystic Monopole
            {"FireChargeFamiliarItem","Summons a sprite that <color=#c60d0d>Burns</color> enemies! Only one sprite can be held at a time." }, //Igniting Sprite Vesa
            {"AirChargeFamiliarItem","Summons a sprite that <color=#749ecc>Slows</color> enemies! Only one sprite can be held at a time." }, //Blasting Sprite Aura
            {"EarthChargeFamiliarItem","Summons a sprite that <color=#8c0497>Poisons</color> enemies! Only one sprite can be held at a time." }, //Plaguing Sprite Dria
            {"LightningChargeFamiliarItem","Summons a sprite that <color=#ffff00ff>Shocks</color> enemies! Only one sprite can be held at a time." }, //Sparking Sprite Etra
            {"WaterChargeFamiliarItem","Summons a sprite that <color=#1b78a0>Freeze</color> enemies! Only one sprite can be held at a time." }, //Freezing Sprite Naya
            {"OverdriveBuildDecayDown","<color=#0060f0>Signature</color> charge no longer decays while building up!" }, //Surefire Rocket
            {"OverdriveDurationUp","<color=#0060f0>Signature</color> charge stays active until used!" }, //Infinity Marble
            {"OverdriveMaxAtLowHealth","<color=#0060f0>Signature charges 30% faster</color> when under <color=#f90233>30% Health!</color>" }, //Hyperbolic Train
            {"HealOverdriveItem","Receive <color=#0060f0>Signature</color> charge when healed!" }, //Albert's Formula
            {"SummonDamageItem","Increases the damage of summoned agents by 20%!" }, //Grimoire of Ruin
            {"SummonCountItem","Increases the number of summoned agents, but lowers their health by 25%!" }, //Pop-up Primer
            {"SummonDurationItem","Increases the duration of summoned agents by 25%!" }, //Yuna's Storybook
            {"ExtraBasicCombo","Basic arcana have an extra combo!" }, //Combo Gloves
            {"AutoBasicCombo","Basic arcana chains are completed automatically!" }, //Evening Gloves
            {"ExtraSkillCharge","Adds 1 more uses to all multi-use arcana!" }, //Ring of Reserves
            {"SevenFlushWater","Holding seven <color=#3c57e0>Water</color> arcana reduces all cooldowns by 28% and increases healing by 49%!" }, //Neve's Sapphire
            {"AltElementLowersCD","Using an arcana of a different element reduces the cooldown of the previously used arcana by 25%!" }, //Spice Rack
            {"GoldUp","Increase <color=#c9a110>Gold gain by 20%</color> for all wizards! Multiple gloves do not stack." }, //Glove of Midas
            {"GoldOnTakeDamage","Gain <color=#c9a110>Gold</color> when taking damage!" }, //Tears of Midas
            {"StageMap","Reveals the layout and main points of interest of the Chaos Trials!" }, //Pathfinder's Map
            {"RainbowTrail","Rainbows! Get to <color=#13a835>max Movement Speed 40% Faster</color>" }, //Unicorn Tail
            {"RelicDiscount","Relics cost <color=#c9a110>25% less Gold</color>" }, //Relic Rewards Card
            {"SpellDiscount","Arcana cost <color=#c9a110>25% less Gold</color>!" }, //Arcana Rewards Card
            {"ResetCDChance","Adds a 10% chance that an arcana will not go on cooldown after use!" }, //Nocturnal Sundial
            {"DashCancelLowersCD","Using a dash arcana immediately after another arcana lowers cooldowns for all arcana of the same element as your dash!" }, //Elemental Pointes
            {"FreeCooldownsAtLowHealth","Activates when taking damage below <color=#f90233>30% Health</color> and briefly removes the cooldown on all arcana!" }, //Phyyrnx's Hourglass
            {"HorseMaskItem","Gain <color=#13a835>8% Movement Speed</color>, Get to max speed 60% Faster." }, //Equestricap
            {"OverdriveOnTakeDamage","Gain <color=#0060f0>Signature charge</color> equel to 100% of damage taken!" }, //Absorption Coil
            {"IgnoreHurtDuringCast","You can no longer be interrupted while activating arcana!" }, //Sano's Headband
            {"StoreRestock","Stores instantly restock after a purchase! Health potions are in short supply and are not restocked." }, //Supply Crate
            {"SlowEnemyProjectiles","Enemy <color=#214051>Projectiles</color> move 30% slower, giving you more time to react!" }, //Dated Sunglasses
            {"AlwaysWinProjectiles","Your <color=#214051>Projectiles</color> destroy any other <color=#214051>Projectiles</color> they strike!" }, //Reinforced Bracers
            {"BuffOnProjectileNegate","Destroying an enemy projectile grants <color=#0060f0>15% Signature meter</color>!" }, //Bladed Buckler
            {"EnemyHPBars","Reveals health bars for all enemies in the Chaos Trials!" }, //Chaos Scanner
            {"FreePurchaseChance","Adds a 30% chance that an item purchased in the Chaos Trials will be free of charge!" }, //Raffle Ticket
            {"PortableTreasure","Gets heavier as you progress through the trials! Drop from inventory to open. Gives Gold and Gems! (Does not make you slower)" }, //C-99 Piggy Bank
            {"PlateEnemyPreventSkills","All Knights and Lancers have their spells limited! Only one Conqueror item can be in effect at a time." }, //Conqueror's Helmet
            {"LeatherEnemyPreventSkills","All Rogues and Archers have their spells limited! Only one Conqueror item can be in effect at a time." }, //Conqueror's Belt
            {"ClothEnemyPreventSkills","All Mages and Summoners have their spells limited! Only one Conqueror item can be in effect at a time." }, //Conqueror's Cloak
            {"GoldOnKillStreak","Gain <color=#c9a110>20% Extra Gold</color> when consecutively defeating enemies without taking damage!" }, //Journal of Midas
            {"FreeDashChargesOnKillChance","Defeating enemies grants a 20% chance to temporarily add unlimited charges to your movement arcana! Lasts 4.5 seconds." }, //Boots of Frenzy
            {"BuyWithHP","If you can't afford a item, Pay the remaining cost with your HP! If you buy a potion and you die from buying it, it does not revive you." }, //Wallet of Vigor
            {"FrostFlameStanza","Increase <color=#c60d0d>Fire</color> and <color=#1b78a0>Ice</color> damage by 20%. ALL <color=#c60d0d>Fire</color> and <color=#1b78a0>Ice</color> Arcana can <color=#c60d0d>Burn</color> or <color=#3aa1fc>Freeze</color>. These have an additional 35% Extra chance to do so and last 50% Longer." }, //Poem of Fiery Rime
            {"DoctorPrescription","Increases healing received from all sources by 40%!" }, //Messy Prescription
            {"DoctorPlacebo","Increases <color=#e8d925>Critical Chance</color> by 12%" }, //Critical Placebos
            {"DoctorDiscount","Receive a 50% discount whenever you purchase potions from the merchant!" }, //Health Care Card
            {"DoctorVial","Regain a <color=#f90233>10% Health!</color> every time you teleport to the next area!" }, //Renewing Potion Vial
            {"WanderersSet","<color=#0060f0>Signature</color> damage Increased by 25%, charge rate, and decay rate are Doubled!" }, //Perfect Time Crystal
            {"ResurrectRing","Revive with <color=#f90233>20% Health!</color> when defeated but consumes half of current health when picked up! This relic is destroyed on use." }, //Horned Halo
            {"Vampiricism","Defeating an enemy heals <color=#f90233>1% Health!</color> but reduces <color=#f90233>Max Health 40%</color>!" }, //Vampire's Fangs
            {"GlassCannon","Damage increased by 20%, but <color=#f90233>Max Health</color> is reduced by 30%!" }, //Glass Cannon
            {"DoubleToil","Reduces cooldowns by half but receive double damage!" }, //Double Toil
            {"DoubleTrouble","Deal double damage but receive double damage! Also cuts <color=#0060f0>Signature</color> gauge gain in half!" }, //Double Trouble
            {"IgnoreDashSlide","Allows you to triple dash but lowers <color=#13a835>Movement Speed</color> by 25%!" }, //Flashy Boots
            {"ArmorUpLoseGoldOnHit","Reduces damage taken by 16%! Lose gold every time you are hit." }, //Armor of Greed
            {"CursedSeal","Increases Damage by 5% and <color=#ffff00ff>Stuns last 25% longer</color>, but recieve <color=#13a835>5% reduced Movement Speed</color> for each cursed relic owned!" }, //Anchor of Burden
            {"DamageUpVsBoss","Your attacks on all Bosses Deal 20% more damage! Recieve 20% damage." }, //Paronomasicon
            {"BasicDamageUpOtherDamageDown","Increase basic arcana damage by 40%! All other arcana damage is decreased by 20%." }, //Double Edged Cestus
            {"ArmorUpStorePriceUp","Decreases Damage taken by 16%, Store prices are increased by 50%!" }, //Golden Armor of Envy
            {"DamageUpStorePriceUp","Increases damage by 20%!, Store prices are increased by 50%!" }, //Golden Saber of Envy
            {"EnemyHealthDownEnemyDamageUp","Enemies have 20% less health, but enemy damage is increased by 30%!" }, //Abhorrent Cologne
            {"PlayerEnemyCritChanceUp","<color=#e8d925>Critical hit chance</color> is increased for you and all enemies by 15%!" }, //Crimson Clover
            {"ArmorUpDamageDown","Decreases damage taken by 12%, Decreases damage dealt by 16%!" }, //Tortoise Shield
            {"EnemyHealthDownPlayerHealthDown","Enemy <color=#f90233>Max Health</color> is lowered by 20%, but your <color=#f90233>Max Health</color> is lowered by 20% as well!" }, //Broken Plague Flask
            {"HalfGlass","Reduces <color=#f90233>Max Health by 50%</color> but increases <color=#f90233>Max Health by 1%</color> for each enemy defeated!" }, //Tiny Crocodile Heart
            {"FullHPOnStartNoHeals","Recover <color=#f90233>FULL Health</color> every time an exit portal is used! <color=#f90233>Max Health</color> is reduced by 40% and all healing has no effect." }, //Large Red Button
            {"HealOnStartZeroGold","Expend all gold at the start of each stage and heal for a fraction of the expended amount!" }, //Overpriced Insurance
            {"WanderersDoll","<color=#0060f0>Signature</color> charge rate and decay are Trippled! but charged <color=#0060f0>Signature damage</color> is reduced by 40%! Shell of the [<color=#FFCC00>Perfect Time Crystal</color>]." }, //Volatile Gemstone
            {"DamageUpNoOverdrive","Increases ALL damage dealt by 20%, <color=#0060f0>Signature</color> can no longer be charged!" }, //Sharpened Stylus
            {"FireStanza","All <color=#c60d0d>Fire</color> arcana cause <color=#c60d0d>Burn</color> but receive double damage from <color=#3c57e0>Water</color> spells! Second half of the [<color=#FFCC00>Poem of Fiery Rime</color>]." }, //Stanza of Flames
            {"AttackSpeedItem","Increases arcana activation speed by 40%, but also <color=#13a835>Slows Movement Speed by 40%</color>! Best used with Melee Arcana." }, //Silver Spinning Top
            {"EvadeUpEnemyDamageUp","Adds <color=#b5e61d>45% Evade Chance</color> but receive <color=#f93d09>Double Damage</color>!" }, //Nog's Heavenly Boots
            {"BankLoan","Gain <color=#c9a110>250 Gold</color>. Starting NEXT stage, gain <color=#c9a110>No Gold</color> untill the debt is paid." }, //Ominous Loan Note
            {"NoPlatinumAllGold","All chaos gems gained during the Chaos Trials are transmuted into gold! 1 Gem is worth 3 Gold" }, //Alchemist's Stone
            {"MaxHPDownRegenAtLowHP","Lose 40% of <color=#f90233>Max Health</color>. When under <color=#f90233>30% Health</color>, Regen 2HP per second untill at 30%. Works well with Tears of Midas." }, //Spiked Emergency Kit
            {"PlayerStart","Mod created by Dawnbomb! <color=#8c0497>Twitch.tv/Dawnbomb</color> Clarity Mod version 0.6 BETA. Coding help by Zandra and Judgy." }, //Museum Ticket
            {"PlayerWin","Enables Hard Mode. Must be carried into the trials to play in Hard Mode, but can be sold mid run." }, //Insignia of Legend
            //Items 2
            {"OnBasicFireArcItem","Adds a 45% chance to fire off a pair of <color=#c60d0d>Dragon Arcs</color> when using basic arcana!" }, //Cabi's Cinnamon Hots
            {"OnBasicAirTwisterItem","Adds a 45% chance to release a <color=#749ecc>Breaking Twister</color> when using basic arcana!" }, //Cabi's Cotton Candy
            {"OnBasicEarthDrillItem","Adds a 45% chance to release an <color=#844205>Earth Drill</color> when using basic arcana!" }, //Cabi's Raw Chocolate
            {"OnBasicLightningStrikeItem","Adds a 45% chance to summon a <color=#ffff00ff>Lightning Strike</color> when using basic arcana!" }, //Cabi's Citrus Sweets
            {"OnBasicWaterShotItem","Adds a 45% chance to shoot a <color=#3c57e0>Bouncing Bubble</color> when using basic arcana!" }, //Cabi's Bubble Gum
            {"ProjectileDamageUp","Increases damage for all <color=#214051>Projectile arcana by 10%</color>!" }, //Gloves of Gambit
            {"MagicianHat","Normal arcana have a 18% chance to cast as enhanced! Additionally, all arcana have a 18% chance on cast to lower their <color=#5f7dad>Cooldown by 50%</color>! Required for the [<color=#FFCC00>Magician's Outfit</color>]." }, //Magician's Top Hat
            {"BaseJumpSkillDamageUp","Increases the base damage of all Jump arcana by 15%! Additionally, Each extra Jump arcana used within 1.5 Seconds gains another 10% Damage Bonus! This bonus does not cap, and you do not need to hit a enemy to keep it going." }, //Vicious Bunny Ears

            {"SlowImmune","Prevents <color=#4d67a0>Slow</color> status effect!" }, //Flashy Windbreaker
            {"AirResistanceUp","Reduces damage taken from <color=#749ecc>Air by 25%</color>." }, //Starchy Parachute
            {"LimitedGuard","Ignore the next 5 Hits of damage. Then, the relic breaks." }, //Ella's Glass Kite
            {"EvadeUpOnHurt","After taking damage, become invincible for 1 Second. 20 Second cooldown." }, //Cassim's Airy Cloak
            {"ElementalResistanceUpOnHurt","When hit by elemental damage, increase your resistance to that element PERMENANTLY by 0.05%! Caps at 25% Resistance per element." }, //Prismatic Vambrace
            {"ArmorHPUpWithCursedRelics","Each cursed relic increases your <color=#9d8766>Armor by 1%</color> and <color=#f90233>Max Health by 5%</color>!" }, //Curse Eater's Vest
            {"JustFrameDash","Dashing consecutively with perfect timing <color=#5f7dad>Lowers your Cooldowns</color> and lets you <color=#13a835>zip around faster</color>!" }, //Lonely Soul's Boots

            {"OverdriveToResetCDs","When you would cast your <color=#0060f0>Fully charged Signature</color>, Instead you <color=#5f7dad>Resets your Cooldowns</color>!" }, //Mobius Quill
            {"SpeedBoostOnSurvivalClear","After clearing a locked room, for the next 5 Seconds gain <color=#13a835>35% Movement Speed</color> and <color=#5f7dad>75% Cooldown reduction</color>!" }, //Jumper Cables
            {"LaserSightItem","Gain the ability to forsee the trajectory of your arcana!" }, //Chaos Visor
            {"GradCap","Increase chaos gem gain for all wizards! (Dropping a piggy bank at the end of a run gets you more crystals tho...) Required for the [<color=#FFCC00>Prestigious Diploma</color>]." }, //Graduation Cap
            {"ReduceCDWithCursedRelics","Each cursed relic lowers your <color=#5f7dad>Cooldowns by 4%</color>" }, //Curse Eater's Watch
            {"TokenCursed","Tempts Nox the Unfortunate to emerge in the Chaos Trials! Only one token can be held and in effect at a time." }, //Token of Doom
            {"TokenShuffler","Entices Nocturne the Cardist to attend the Chaos Trials! Only one token can be held and in effect at a time." }, //Token of Luck
            {"TokenDoctor","Pages Doctor Song to the Chaos Trials! Only one token can be held and in effect at a time." }, //Token of Health
            {"TokenTailor","Books Savile the Tailor to visit the Chaos Trials! Only one token can be held and in effect at a time." }, //Token of Elegance
            {"TokenCollector","Calls Cremire the Collector to case the Chaos Trials! Only one token can be held and in effect at a time." }, //Token of Commerce
            {"TokenBanker","Plies Doki the Banker to appear in the Chaos Trials! Only one token can be held and in effect at a time." }, //Token of Wealth
            {"TokenPinata","Summons Taffy the Pinata to the Chaos Trials! Only one token can be held and in effect at a time." }, //Token of Triumph

            {"CritHealChanceUp","Get a <color=#f90233>15% Chance</color> of receiving a <color=#f90233>Critical Heal</color>!" }, //Catalystic Tonic
            {"GradSet","Doubles Gem drop rate. Doubles Relic drop rate. Increases <color=#c9a110>Gold Gain by 20%</color>. All effects apply to all wizards, but does not stack with similuar effects." }, //Prestigious Diploma
            {"MagicianSet","Normal arcana have a 30% chance to cast as enhanced! Additionally, all arcana have a 30% chance on cast to lower their <color=#5f7dad>Cooldown by 50%</color>! Increases <color=#f93d09>ALL Damage by 20%</color>!" }, //Magician's Outfit
            {"ArcanaBalancer","Unenhanced arcana gain <color=#f93d09>20% Damage</color> and <color=#5f7dad>25% Cooldown Reduction</color>! Enhanced arcana LOSE <color=#f93d09>20% Damage</color> and <color=#5f7dad>25% Cooldown Reduction</color>!" }, //Titan's Equilibrium
            {"PermaOverdrive","Your signature arcana is permanently activated, but deals <color=#f93d09>20% Less Damage</color> and has a <color=#5f7dad>50% Longer Cooldown</color>, and gets EVEN LONGER by <color=#5f7dad>15% per Cast</color>!" }, //Anemic Marker
            {"GradBouquet","All Wizards Gain <color=#c9a110>DOUBLE GOLD</color> but halfs your relic drop chance. Does not stack with similuar effects. Required for the [<color=#FFCC00>Prestigious Diploma</color>]." }, //Graduation Bouquet
            {"MagicianWand","Increases <color=#f93d09>ALL Damage by 20%</color> but lowers the activation speed of all arcana by 30%! Required for the [<color=#FFCC00>Magician's Outfit</color>]." }, //Magician's Wand
            {"ReduceCDReduceDamage","Reduces <color=#5f7dad>Cooldown by 50%</color> but deal <color=#f93d09>30% Less Damage</color>!" }, //Cruel Chess Clock
            {"UnicornMask","Gain <color=#13a835>10% Movement Speed</color> and achieve max speed <color=#13a835>75% Faster</color>!" }, //Unicorn Disguise
            {"FlatDamage","Makes <color=#f93d09>ALL DAMAGE dealt and taken deal exactly 99 damage</color>!" }, //Rules of Contingency
            {"NoRandomPits","Pits no longer appear in hallways, but you take <color=#f93d09>10% more Damage</color> from pitfalls! (They still appear in battle rooms)." }, //Makeshift Bridge
            {"RandomSkillAfterUse","Shops have a <color=#c9a110>75% Discount</color>! However... Spells <color=#94DE40>BREAK</color> when their <color=#94DE40>Uses hit Zero</color>! <color=#94DE40>Broken Spells</color> are randomly replaced with a new ones! Basic Uses: 15. Dash Uses: 12. Spell Uses: 5. HUB only." }, //Aberrant Deck
            {"OneHPOneDamage","Turns on <color=#EF1414>INSTANT DEATH MODE</color> where all players and enemies are defeated in one punch! HUB Only." }, //Chaotic Comedy
            {"RandomExplosiveBarrels","Spawns explosive barrels randomly throughout the trials! HUB only." }, //Conga Drum Set
            {"LoseArcanaRelicOnHurtChance","All shops have a <color=#c9a110>75% OFF SALE</color>!. Enemies have a <color=#6CCE09>25% Chance to Steal</color> your spells or relics when hit!. HUB Only." }, //Rulebook of Thieves
            {"ReduceCDPlayerEnemy","All Wizards and Enemies have <color=#5f7dad>75% Lower Cooldowns</color>. Enemies gain <color=#13a835>25% Movement Speed</color>, Their Spells deal <color=#f93d09>25% More Damage</color> and are <color=#0060f0>Cast 25% Faster</color>! HUB Only." }, //Chaotic Pendulum
            {"EnemyPlayerMoveSpeedUp","You gain <color=#13a835>40%Movement Speed</color>. ALL Enemies gain <color=#13a835>50%Movement Speed</color> and their spells <color=#0060f0>Cast 25% Faster</color>! HUB Only." }, //Quantum Capacitor
            {"RandomElementalEnemies","Gain 25% resistance to all the elements! Any enemy can appear on any stage. HUB Only." }, //Melting Pot
            {"OverdriveFromDestructibles","Gain <color=#0060f0>Signature Charge</color> from attacking destructibles!" }, //Broken Stress Ball
        };

        public static Dictionary<string, Tuple<string, string>> CustomSkillsDescriptions = new Dictionary<string, Tuple<string, string>>()
        {
             { "FlameStrikeBasic", new Tuple<string, string>( "Blast enemies away with plumes of flames! [<color=#c60d0d>Fire</color>] [<color=#512323>Melee</color>]", null ) },
             { "FireBounceBasic", new Tuple<string, string>( "Fling bouncing balls of fire! [<color=#c60d0d>Fire</color>] [<color=#214051>Projectile</color>]", null ) },
             { "AirSpinBasic", new Tuple<string, string>( "Throw a disc of air into orbit around you! [<color=#4d67a0>Air</color>] [<color=#214051>Projectile</color>]", null ) },
             { "WindSlashBasic"  , new Tuple<string, string>( "Summon arcs of slashing wind! [<color=#4d67a0>Air</color>] [<color=#512323>Melee</color>]", null ) },
             { "EarthSpikeBasic", new Tuple<string, string>( "Pummel foes with gigantic earth fists! [<color=#844205>Earth</color>] [<color=#512323>Melee</color>]", null ) },
             { "UseRockThrowBasic", new Tuple<string, string>( "Rapidly fire stones! [<color=#844205>Earth</color>] [<color=#214051>Projectile</color>]", null ) },
             { "ShockTouchBasic", new Tuple<string, string>( "Jolt foes with a burst of electricity! [<color=#ffff00ff>Lightning</color>] [<color=#512323>Melee</color>]", null ) },
             { "BoltRailBasic", new Tuple<string, string>( "Direct a short stream of lightning! [<color=#ffff00ff>Lightning</color>] [<color=#3b4c35>No Type</color>]", null ) },
             { "IceDaggerBasic", new Tuple<string, string>( "Pierce foes with conjured ice daggers! [<color=#1b78a0>Ice</color>] [<color=#512323>Melee</color>]", null ) },
             { "WaterArcBasic", new Tuple<string, string>( "Direct an arcing stream of water! [<color=#3c57e0>Water</color>] [<color=#214051>Projectile</color>]", null ) },
             { "UseChaosBasic", new Tuple<string, string>( "Create a chaotic rift to strike foes before compressing it and firing it forward! [<color=#512323>Melee</color>] [<color=#214051>Projectile</color>]", null ) },
             //Basic2
             { "FlameCrossBasic", new Tuple<string, string>( "Fire off waves of criss crossing flames! [<color=#c60d0d>Fire</color>] [<color=#3b4c35>No Type</color>]", null ) },
             { "AirDartBasic", new Tuple<string, string>( "Release piercing jet streams of air! [<color=#4d67a0>Air</color>] [<color=#214051>Projectile</color>]", null ) },
             { "VineWhipBasic", new Tuple<string, string>( "Repeatedly whip a bladed vine to slash foes! [<color=#844205>Earth</color>] [<color=#512323>Melee</color>]", null ) },
             { "ThunderOrbBasic", new Tuple<string, string>( "Discharge an electric disc that bursts before dissipating! [<color=#ffff00ff>Lightning</color>] [<color=#214051>Projectile</color>]", null ) },
             { "AquaCrestBasic", new Tuple<string, string>( "Unleashes a trio of water ripples! [<color=#3c57e0>Water</color>] [<color=#214051>Projectile</color>]", null ) },
             
             //Next is dashes
             { "Dash", new Tuple<string, string>( "Dash forward on a burst of air! [<color=#4d67a0>Air</color>]", null ) },
             { "AirChannelDash", new Tuple<string, string>( "Dash forward and pull enemies into your wake! [<color=#4d67a0>Air</color>]", null ) },
             { "FireDash", new Tuple<string, string>( "Rush forward and leave flames in your wake! [<color=#c60d0d>Fire</color>] [<color=#c60d0d>Lv2 Burn</color>]", null ) },
             { "FireMineDash", new Tuple<string, string>( "Rush forward and conjure a volley of accompanying flares! [<color=#c60d0d>Fire</color>] [<color=#c60d0d>Lv1 Burn</color>]", null ) },
             { "EarthDash", new Tuple<string, string>( "Tear forward leaveing a trail of earthen spikes! [<color=#844205>Earth</color>]", null ) },
             { "PoisonDash", new Tuple<string, string>( "Tear forward, dropping a toxic bomb that poisons foes! [<color=#8c0497>Lv1 Poison</color>]", null ) },
             { "IceDash", new Tuple<string, string>( "Race forward while leaving behind a frozen decoy! [<color=#1b78a0>Ice</color>]", null ) },
             { "WaterDash", new Tuple<string, string>( "Race forward while enveloped in a watery globe that briefly shields you! [<color=#3c57e0>Water</color>]", null ) },
             { "LightningDash", new Tuple<string, string>( "Jolt ahead and leave behind a line of shocking current! [<color=#ffff00ff>Lightning</color>]", null ) },
             { "ChainedDash", new Tuple<string, string>( "Jolt ahead, leaving electric orbs that chain together after a short delay! [<color=#ffff00ff>Lightning</color>] [<color=#ffff00ff>Lv1 Shock</color>]", null ) },
             { "ChaosDash", new Tuple<string, string>( "Dash through a rift torn open by primal chaos!", null ) },
             // Dashes2
             { "FlameShieldDash", new Tuple<string, string>( "Rush forward and ignite a fiery aura around you that burns foes you come into contact with! [<color=#c60d0d>Fire</color>] [<color=#c60d0d>Lv1 Burn</color>]", null ) },
             { "SlicingFieldDash", new Tuple<string, string>( "Dash forward and create a vortex of cutting winds! [<color=#4d67a0>Air</color>]", null ) },
             { "VineDash", new Tuple<string, string>( "Tear forward as a trio of vines entangle enemies in your wake! [<color=#844205>Earth</color>]", null ) },
             { "ThunderDash", new Tuple<string, string>( "Jolt ahead as a burst of lightning shocks foes in the area! [<color=#ffff00ff>Lightning</color>] [<color=#ffff00ff>Lv1 Shock</color>]", null ) },
             { "FrostWingDash", new Tuple<string, string>( "Race forward on frozen wings that unleash a cone of freezing feathers behind you! [<color=#1b78a0>Ice</color>] [<color=#3aa1fc>Freeze</color>]", null ) },
             //Below is now standard spells

             //First is Earth
             { "SummonBoulderShield", new Tuple<string, string>( "Form a protective ring of stone shields! [<color=#844205>Earth</color>] [<color=#3b4c35>No Type</color>]", null ) },
             { "SummonChurningEarth", new Tuple<string, string>( "Rupture the earth with tunneling vines to continually damage enemies in a line! [<color=#844205>Earth</color>] [<color=#3b4c35>No Type</color>]", null ) }, //wave
             { "StalwartDefenders", new Tuple<string, string>( "Form a quartet of chess knights that leap to your defense! [<color=#844205>Earth</color>]", null ) },
             { "UseDragonStomp", new Tuple<string, string>( "Summon a tunneling dragon that drives away foes! [<color=#844205>Earth</color>] [<color=#3b4c35>No Type</color>]", null ) }, //wave
             { "UseShatterStone", new Tuple<string, string>( "Strike a boulder to shatter it into a ring of shrapnel! [<color=#844205>Earth</color>] [<color=#3b4c35>No Type</color>]", null ) },
             { "UseSpikeRing", new Tuple<string, string>( "Pulverize the ground to summon cascading rings of earth spikes! [<color=#844205>Earth</color>] [<color=#3b4c35>No Type</color>]", null ) }, //wave
             { "UseEarthJump", new Tuple<string, string>( "Launch into the air and crash down on all nearby foes to stun them! [<color=#844205>Earth</color>] [<color=#512323>Melee</color>]", null ) },
             { "UseThrowBoulder", new Tuple<string, string>( "Hurl a boulder that stuns foes! [<color=#844205>Earth</color>] [<color=#214051>Projectile</color>]", null ) },
             { "UseVineWave", new Tuple<string, string>( "Cultivate a row of entwining vines that roots foes! [<color=#844205>Earth</color>] [<color=#8c0497>Lv1 Poison when enhanced</color>] [<color=#3b4c35>No Type</color>]", null ) }, //wave
             { "UsePoisonBolas", new Tuple<string, string>( "Hurl bolas made of toxic vines to entangle and poison foes! [<color=#844205>Earth</color>] [<color=#8c0497>Lv1 Poison</color>] [<color=#214051>Projectile</color>]", null ) },
             { "UseGraspingEarth", new Tuple<string, string>( "Grasp all foes in the area with giant stone fists! [<color=#844205>Earth</color>] [<color=#3b4c35>No Type</color>]", null ) },
             { "UseEarthCascade", new Tuple<string, string>( "Unleash a cascade of obsidian daggers! [<color=#844205>Earth</color>] [<color=#214051>Projectile</color>]", null ) },
             { "UseEarthMinion", new Tuple<string, string>( "Conjure an agent of earth that crushes foes! [<color=#844205>Earth</color>] [<color=#512323>Melee</color>] [Summon]", null ) },
             { "UseEarthWard", new Tuple<string, string>( "Form a ward of earth that periodically stuns foes and increases all earth damage done in the area! [<color=#844205>Earth</color>] [<color=#3b4c35>No Type</color>]", null ) },
             { "UseEarthDrill", new Tuple<string, string>( "Drive forward and rock enemies with a giant drill made of stone! [<color=#844205>Earth</color>] [<color=#512323>Melee</color>]", null ) },
             { "UseEarthAxe", new Tuple<string, string>( "Hurl a giant stone axe into the air that crashes down on enemies! [<color=#844205>Earth</color>] [<color=#3b4c35>No Type</color>]", null ) },
             { "UseShieldCharge", new Tuple<string, string>( "Shield incoming attacks as you charge forward, stunning enemies in your path! [<color=#844205>Earth</color>] [<color=#512323>Melee</color>]", null ) },
             { "UseEarthRebound", new Tuple<string, string>( "Mark foes with a series of rocky strikes that explode as you leap back! [<color=#844205>Earth</color>] [<color=#512323>Melee</color>]", null ) },
             { "UseEarthEnhance", new Tuple<string, string>( "Generates a magnetic aura that causes basic arcana to hurl stone projectiles! [<color=#844205>Earth</color>] [<color=#214051>Projectile</color>] [Buff]", null ) },
             { "UseEarthWheel", new Tuple<string, string>( "Summon a pair of rocky buzzsaws that tear forward! [<color=#844205>Earth</color>] [<color=#3b4c35>No Type</color>]", null ) },
             //Earth2
             { "UseWhirlingAxe", new Tuple<string, string>( "Summon and hurl a stone tomahawk at your foes! Can be used repeatedly for a short duration. [<color=#844205>Earth</color>] [<color=#3b4c35>No Type</color>]", null ) },
             { "UseHammerThrow", new Tuple<string, string>( "Solidify a giant hammer of stone and whirl it about before hurling it forward! [<color=#844205>Earth</color>] [<color=#512323>Melee</color>] [<color=#214051>Projectile</color>]", null ) },
             
             
             //Next is FIRE spells
             { "ShootFireball", new Tuple<string, string>( "Fire a massive fireball that explodes on impact! [<color=#c60d0d>Fire</color>] [<color=#214051>Projectile</color>]", null ) },
             { "ShootFireArc", new Tuple<string, string>( "Summon a torrent of fiery dragons! [<color=#c60d0d>Fire</color>] [<color=#214051>Projectile</color>]", null ) },
             { "Berserk", new Tuple<string, string>( "Enter a burning rage that speeds up basic arcana! [<color=#c60d0d>Fire</color>] [Buff]", null ) },
             { "UseFireBlast", new Tuple<string, string>( "Blast away enemies with a breath of fire! [<color=#c60d0d>Fire</color>] [<color=#c60d0d>Lv1 Burn</color>] [<color=#3b4c35>No Type</color>]", null ) },
             { "SummonFireWall", new Tuple<string, string>( "Ignite the ground to create a blazing trail of flames! [<color=#c60d0d>Fire</color>] [<color=#c60d0d>Lv1 Burn</color>] [<color=#3b4c35>No Type</color>]", null ) },
             { "SummonHomingMissiles", new Tuple<string, string>( "Ignite a halo of flares that home in on the nearest foe! [<color=#c60d0d>Fire</color>] [<color=#214051>Projectile</color>]", null ) },
             { "SummonMeteorStrike", new Tuple<string, string>( "Call down a meteor from the skies! [<color=#c60d0d>Fire</color>] [<color=#3b4c35>No Type</color>]", null ) },
             { "UseBlazingBlitz", new Tuple<string, string>( "Dash forward and pummel a foe with barrage of fiery punches! [<color=#c60d0d>Fire</color>] [<color=#512323>Melee</color>]", null ) },
             { "UseBlazingCombo", new Tuple<string, string>( "Dash forward, grabbing all enemies in your path before unleashing an onslaught of fireballs! [<color=#c60d0d>Fire</color>] [<color=#214051>Projectile</color>]", null ) },
             { "UseVacuumFlame", new Tuple<string, string>( "Hold to charge an expanding ball of fire that explodes at maximum charge! [<color=#c60d0d>Fire</color>] [<color=#c60d0d>Lv1 Burn</color>] [<color=#214051>Projectile</color>]", null ) },
             { "UseCombustionWave", new Tuple<string, string>( "Blast enemies away with a chain of explosions! [<color=#c60d0d>Fire</color>] [<color=#c60d0d>Lv1 Burn</color>] [<color=#3b4c35>No Type</color>]", null ) },
             { "UseFlameSwirl", new Tuple<string, string>( "Spin forward while swinging a blazing whip of flames! [<color=#c60d0d>Fire</color>] [<color=#c60d0d>Lv1 Burn</color>] [<color=#512323>Melee</color>]", null ) },
             { "UseFireMinion", new Tuple<string, string>( "Conjure an agent of fire that engages foes with a rapid barrage of fireballs! [<color=#c60d0d>Fire</color>] [Summon]", null ) },
             { "UseFireWard", new Tuple<string, string>( "Form a ward of fire that damages enemies and increases all fire damage done in the area! [<color=#c60d0d>Fire</color>]", null ) },
             { "UseCircleBurner", new Tuple<string, string>( "Blast away enemies around you with a burning halo of flames! [<color=#c60d0d>Fire</color>] [<color=#c60d0d>Lv1 Burn</color>] [<color=#3b4c35>No Type</color>]", null ) },
             { "UseFlameReturner", new Tuple<string, string>( "Fire off a volley of arcing tracers that return to you! [<color=#c60d0d>Fire</color>] [<color=#214051>Projectile</color>]", null ) },
             { "UseFlameLeap", new Tuple<string, string>( "Blast into the air and come crashing down into a fiery explosion! [<color=#c60d0d>Fire</color>] [<color=#c60d0d>Lv2 Burn</color>] [<color=#512323>Melee</color>]", null ) },
             { "UseFlameBuster", new Tuple<string, string>( "Fire off a fireball, followed by a flaming arrow that can be fused to release a burst of arrows! [<color=#c60d0d>Fire</color>] [<color=#c60d0d>Lv1~2 Burn</color>] [<color=#214051>Projectile</color>]", null ) },
             { "UseFlameSlash", new Tuple<string, string>( "Carve out a wave of flames that ignites all enemies in its path! [<color=#c60d0d>Fire</color>] [<color=#c60d0d>Lv1 Burn</color>] [<color=#214051>Projectile</color>]", null ) },
             { "UseExplosiveCharge", new Tuple<string, string>( "Charge through enemies, marking them with a timed explosive! [<color=#c60d0d>Fire</color>] [<color=#c60d0d>Lv1 Burn when Enhanced</color>] [<color=#512323>Melee</color>]", null ) },
             //Fire2
             { "UseFlameTrap", new Tuple<string, string>( "Strike the ground, creating a burning fissure that ensnares nearby enemies! [<color=#c60d0d>Fire</color>] [<color=#3b4c35>No Type</color>]", null ) },
             { "UseRisingDragon", new Tuple<string, string>( "Spiral into the sky while a flaming vortex draws in nearby foes! [<color=#c60d0d>Fire</color>] [<color=#512323>Melee</color>]", null ) },
             
             // next is WIND SPELLS
             { "MultiShot", new Tuple<string, string>( "Release a volley of piercing winds in a cone! [<color=#4d67a0>Air</color>] [<color=#214051>Projectile</color>]", null ) },
             { "ShootArrow", new Tuple<string, string>( "Unleash a quick salvo of cutting winds! [<color=#4d67a0>Air</color>] [<color=#214051>Projectile</color>]", null ) },
             { "ShootVacuumShot", new Tuple<string, string>( "Release a burst of air that drafts foes and stuns them against walls! [<color=#4d67a0>Air</color>] [<color=#214051>Projectile</color>]", null ) },
             { "SummonTornado", new Tuple<string, string>( "Unleash a protective vortex of fierce winds! [<color=#4d67a0>Air</color>] [<color=#3b4c35>No Type</color>]", null ) },
             { "UseTornadoKick", new Tuple<string, string>( "Fly forward with a ring of tumbling winds that locks onto the first enemy struck! [<color=#4d67a0>Air</color>] [<color=#512323>Melee</color>]", null ) },
             { "SwordThrow", new Tuple<string, string>( "Hurl a rapidly revolving air current that returns to you! [<color=#4d67a0>Air</color>] [<color=#214051>Projectile</color>]", null ) },
             { "UseHeroicLeap", new Tuple<string, string>( "Seize a foe and leap high into the air before crashing down! [<color=#4d67a0>Air</color>] [<color=#512323>Melee</color>]", null ) },
             { "SummonTumbleFist", new Tuple<string, string>( "Draw in foes with a ring of tumbling winds before striking them away! [<color=#4d67a0>Air</color>] [<color=#512323>Melee</color>]", null ) },
             { "UseWindBall", new Tuple<string, string>( "Hold to charge up and release a stormy ball of wind that implodes at maximum charge! [<color=#4d67a0>Air</color>] [<color=#214051>Projectile</color>]", null ) },
             { "UseBabylonScales", new Tuple<string, string>( "Lower active cooldowns and steal speed from enemies by blasting them with a burst of air! [<color=#4d67a0>Air</color>] [<color=#4d67a0>Lv? Slow</color>] [Buff]", null ) },
             { "UseWindMinion", new Tuple<string, string>( "Conjure an agent of wind that buffets foes with whirling winds! [<color=#4d67a0>Air</color>] [<color=#512323>Melee</color>] [Summon]", null ) },
             { "UseWindWard", new Tuple<string, string>( "Summon a ward of air that slows enemies and increases all air damage done in the area! [<color=#4d67a0>Air</color>] [<color=#4d67a0>Lv? Slow</color>] [<color=#3b4c35>No Type</color>]", null ) },
             { "UseAirWave", new Tuple<string, string>( "Fire off a series of sound waves that stun any enemies they strike! [<color=#4d67a0>Air</color>] [<color=#3b4c35>No Type</color>]", null ) },
             { "UseSlicingBarrage", new Tuple<string, string>( "Rush forward while unleashing a flurry of wind slashes! [<color=#4d67a0>Air</color>] [<color=#512323>Melee</color>]", null ) },
             { "UseMindControlCloud", new Tuple<string, string>( "Charm foes around you, causing them to attack each other! [<color=#4d67a0>Air</color>]", "Enhanced: Increases Charm duration" ) },
             { "UseDragonBreath", new Tuple<string, string>( "Summon a dragon formed of wind to draw in enemies before blasting them away! [<color=#4d67a0>Air</color>] [<color=#4d67a0>Lv? Slow</color>] [<color=#3b4c35>No Type</color>]", null ) },
             { "UseWhirlwind", new Tuple<string, string>( "Rapidly spin forward and pull nearby enemies into your wake! [<color=#4d67a0>Air</color>] [<color=#512323>Melee</color>]", null ) },
             { "UseTwister", new Tuple<string, string>( "Unleash a slow moving burst of air that slows and damages all nearby enemies! [<color=#4d67a0>Air</color>] [<color=#4d67a0>Lv? Slow</color>] [<color=#214051>Projectile</color>]", null ) },
             { "UseWindDefense", new Tuple<string, string>( "Summon a current of air that causes all incoming attacks to miss while you are moving or using basic arcana! [<color=#4d67a0>Air</color>] [<color=#3b4c35>No Type</color>] [Buff]", null ) },
             { "UseWindSlam", new Tuple<string, string>( "Rocket into the sky on an air jet, then slam into the ground to create a violent burst of wind! [<color=#4d67a0>Air</color>] [<color=#4d67a0>Lv? Slow</color>] [<color=#512323>Melee</color>]", null ) },
            //Wind2
             { "UseVortexWave", new Tuple<string, string>( "Unleash a flowing burst of wind that forcefully lines up foes it strikes! [<color=#4d67a0>Air</color>] [<color=#3b4c35>No Type</color>]", null ) },
             { "UseWindFalcon", new Tuple<string, string>( "Hurl a falcon of wind that repeatedly assails the first enemy it strikes! [<color=#4d67a0>Air</color>] [<color=#214051>Projectile</color>]", null ) },
             
             
             //Next is WATER & ICE SPELLS
             { "CircleStrike", new Tuple<string, string>( "Summon swirling currents of water that envelop you! [<color=#3c57e0>Water</color>] [<color=#3b4c35>No Type</color>]", null ) },
             { "ThrowIceLance", new Tuple<string, string>( "Conjure a spear of ice that impales foes and freezes them against walls! [<color=#1b78a0>Ice</color>] [<color=#1b78a0>Freeze on Walls</color>] [<color=#214051>Projectile</color>]", null ) },
             { "FreezingLunge", new Tuple<string, string>( "Dash forward and conjure a colossal frozen fist to strike foes! [<color=#1b78a0>Ice</color>] [<color=#1b78a0>Freeze</color>] [<color=#512323>Melee</color>]", null ) },
             { "SummonBlizzard", new Tuple<string, string>( "Summon a storm of hail and ice shards that rains down on foes! [<color=#1b78a0>Ice</color>] [<color=#1b78a0>Chance to Freeze</color>] [<color=#3b4c35>No Type</color>]", null ) },
             { "SummonIceWave", new Tuple<string, string>( "Conjure a series of icicles that cascade out and return to you! [<color=#1b78a0>Ice</color>] [<color=#3b4c35>No Type</color>]", null ) },
             { "SummonSwordStorm", new Tuple<string, string>( "Conjure a rapidly revolving ring of icy blades! [<color=#1b78a0>Ice</color>] [<color=#3b4c35>No Type</color>]", null ) },
             { "SummonBubbleBarrage", new Tuple<string, string>( "Rapidly fire a stream of explosive bubbles! [<color=#3c57e0>Water</color>] [<color=#214051>Projectile</color>]", null ) },
             { "UseWaterBall", new Tuple<string, string>( "Hold to charge up and release a rapidly spinning ball of water that creates an explosive deluge at maximum charge! [<color=#3c57e0>Water</color>] [<color=#214051>Projectile</color>]", null ) },
             { "SummonShark", new Tuple<string, string>( "Lob a frozen decoy that lures in foes and giant sharks alike! [<color=#1b78a0>Ice</color>] [<color=#1b78a0>Freeze</color>] [<color=#3b4c35>No Type</color>]", null ) },
             { "UseIceChakram", new Tuple<string, string>( "Conjure snowflake chakrams that orbit outwards! [<color=#1b78a0>Ice</color>] [<color=#3b4c35>No Type</color>]", null ) },
             { "UseAquaBeam", new Tuple<string, string>( "Unleash a torrent of water to blast away enemies! [<color=#3c57e0>Water</color>] [<color=#214051>Projectile</color>]", null ) },
             { "UseAquaPrison", new Tuple<string, string>( "Fire off a prison of water that envelops the first enemy it strikes! [<color=#3c57e0>Water</color>] [<color=#214051>Projectile</color>]", null ) },
             { "UseWaterSlam", new Tuple<string, string>( "Throw a burst of water, drawing in foes before blasting into the sky and splashing down! [<color=#3c57e0>Water</color>] [<color=#512323>Melee</color>]", null ) },
             { "SummonIceSeekers", new Tuple<string, string>( "Summon a trio of seekers that home in on and freeze nearby enemies! [<color=#1b78a0>Ice</color>] [<color=#1b78a0>Freeze</color>] [<color=#3b4c35>No Type</color>]", null ) },
             { "UseWaterBounce", new Tuple<string, string>( "Throw out a bubble of water that bounces towards nearby enemies! [<color=#3c57e0>Water</color>] [<color=#214051>Projectile</color>]", null ) },
             { "UseFrostFan", new Tuple<string, string>( "Throw out a fan of frost daggers that freeze enemies! [<color=#1b78a0>Ice</color>] [<color=#1b78a0>Freeze</color>] [<color=#214051>Projectile</color>]", null ) },
             { "UseWaterWard", new Tuple<string, string>( "Form a ward of frost that freezes foes and increases all water damage done in the area! [<color=#3c57e0>Water</color>] [<color=#1b78a0>Freeze</color>] [<color=#3b4c35>No Type</color>]", null ) },
             { "UseWaterMinion", new Tuple<string, string>( "Conjure an agent of water that douses foes with a spread of water blobs! [<color=#3c57e0>Water</color>] [Summon]", null ) }, //Is melee???
             { "UseCreepingTendril", new Tuple<string, string>( "Creates a tendril of water that binds and travels to any foes it hits! [<color=#3c57e0>Water</color>] [<color=#512323>Melee</color>]", null ) },
             { "UseWaterBlast", new Tuple<string, string>( "Pull in enemies with a series of water balls, then blast them away with a massive deluge! [<color=#3c57e0>Water</color>] [<color=#3b4c35>No Type</color>]", null ) },
             //Water2
             { "UseAquaBlitz", new Tuple<string, string>( "Blasts out a chain of water blobs before using them to pummel foes with a series of blows! [<color=#3c57e0>Water</color>] [<color=#214051>Projectile</color>]", null ) },
             { "UseAquaDragon", new Tuple<string, string>( "Unleash a pair of watery dragons that track and assault your foes! [<color=#3c57e0>Water</color>] [<color=#3b4c35>No Type</color>]", null ) },
                         
             //next is LIGHTNING SPELLS
             { "ShootBallLightning", new Tuple<string, string>( "Hold to charge up and release a devastating ball of lightning that shocks at maximum charge! [<color=#ffff00ff>Lightning</color>] [<color=#ffff00ff>Lv3 Shock When Charged</color>] [<color=#214051>Projectile</color>]", null ) },
             { "ShootBoltClaymore", new Tuple<string, string>( "Release a charged orb that can be detonated to strike all nearby foes with lightning! [<color=#ffff00ff>Lightning</color>] [<color=#ffff00ff>Lv? ?% Chance to Shock</color>] [<color=#214051>Projectile</color>]", null ) },
             { "SummonThunderWave", new Tuple<string, string>( "Generate a series of lightning strikes that shock foes! [<color=#ffff00ff>Lightning</color>] [<color=#ffff00ff>Lv1 Shock</color>] [<color=#3b4c35>No Type</color>]", null ) },
             { "TeleStrike", new Tuple<string, string>( "Ride a bolt of lightning to shock foes in the area! [<color=#ffff00ff>Lightning</color>] [<color=#ffff00ff>Lv? Shock</color>] [<color=#512323>Melee</color>]", null ) },
             { "UseIonSpike", new Tuple<string, string>( "Generate a orb that syncs up with nearby orbs before releasing a stream of lightning! [<color=#ffff00ff>Lightning</color>] [<color=#ffff00ff>Lv1 Shock</color>] [<color=#3b4c35>No Type</color>]", null ) },
             { "UseVoltSplinter", new Tuple<string, string>( "Generate a protective ring of charged orbs that ricochet from foe to foe! [<color=#ffff00ff>Lightning</color>] [<color=#214051>Projectile</color>]", null ) },
             { "UseElectricAura", new Tuple<string, string>( "Generate a field of electricity that shocks enemies that you come into contact with! [<color=#ffff00ff>Lightning</color>] [<color=#ffff00ff>Lv3 Shock</color>] [<color=#3b4c35>No Type</color>] [Buff]", null ) },
             { "UseShockNova", new Tuple<string, string>( "Charge up and release an explosion of electricity that shocks all foes at maximum charge! [<color=#ffff00ff>Lightning</color>] [<color=#ffff00ff>Lv1~2 Shock</color>] [<color=#3b4c35>No Type</color>]", null ) },
             { "UseLightningMinion", new Tuple<string, string>( "Conjure an agent of lightning that assaults foes with a stream of lightning! [<color=#ffff00ff>Lightning</color>] [<color=#3b4c35>No Type</color>] [Summon]", null ) },
             { "UseLightningWard", new Tuple<string, string>( "Summon a lightning ward that shocks foes and increases all lightning damage done in the area! [<color=#ffff00ff>Lightning</color>] [<color=#ffff00ff>Lv3 Shock</color>] [<color=#3b4c35>No Type</color>]", null ) },
             { "UseShockLine", new Tuple<string, string>( "A wire of current that shocks any enemies that it comes into contact with! [<color=#ffff00ff>Lightning</color>] [<color=#ffff00ff>Lv1 Shock</color>] [<color=#214051>Projectile</color>]", null ) },
             { "UseCurrentBurst", new Tuple<string, string>( "An electric orb that fires a continuous lightning stream at its target! [<color=#ffff00ff>Lightning</color>] [<color=#214051>Projectile</color>]", null ) },
             { "UseLightningShuriken", new Tuple<string, string>( "Hurl an electrified star that shocks any enemy it strikes! [<color=#ffff00ff>Lightning</color>] [<color=#ffff00ff>Lv2 Shock</color>] [<color=#214051>Projectile</color>] ", null ) },
             { "UseShockSpear", new Tuple<string, string>( "Conjure a spear of lightning that bounces between enemies! [<color=#ffff00ff>Lightning</color>] [<color=#ffff00ff>Lv? Shock</color>] [<color=#214051>Projectile</color>]", null ) },
             { "UseMagSphere", new Tuple<string, string>( "Pull all nearby projectiles into an orbit around you! [<color=#ffff00ff>Lightning</color>] [<color=#3b4c35>No Type</color>] [Buff]", null ) },
             { "UseThunderDrop", new Tuple<string, string>( "Volt into the sky, then crash down on a bolt of lightning to shock all foes! [<color=#ffff00ff>Lightning</color>] [<color=#ffff00ff>Lv2 Shock</color>] [<color=#512323>Melee</color>]", null ) },
             { "UsePiercingShock", new Tuple<string, string>( "Dart forward pinning foes in your path with a piercing lightning needle! [<color=#ffff00ff>Lightning</color>] [<color=#512323>Melee</color>]", null ) },
             { "UseThunderFan", new Tuple<string, string>( "Form a double weave of shocking sparks that slowly move outward! [<color=#ffff00ff>Lightning</color>] [<color=#ffff00ff>Lv3 Shock</color>] [<color=#214051>Projectile</color>]", null ) },
             { "UseThunderAnchor", new Tuple<string, string>( "Throws out an anchor that generates a surge of lightning between you! [<color=#ffff00ff>Lightning</color>] [<color=#3b4c35>No Type</color>]", null ) },
             { "UseLightningFist", new Tuple<string, string>( "Generate a stationary sphere of arcing current! [<color=#ffff00ff>Lightning</color>] [<color=#ffff00ff>Lv1 Shock</color>] [<color=#512323>Melee</color>]", null ) },
             //Lightning2
             { "UseShockLace", new Tuple<string, string>( "Summon a ring of lightning pins with a shocking current that laces them together! [<color=#ffff00ff>Lightning</color>] [<color=#3b4c35>No Type</color>]", null ) },
             { "UseLightningXStrike", new Tuple<string, string>( "Fire off a pair of magnetized orbs that fly forward while bouncing off of each other! [<color=#ffff00ff>Lightning</color>] [<color=#214051>Projectile</color>]", null ) },
             
             
             //chaos
             { "UseVoidShift", new Tuple<string, string>( "Teleport all nearby enemies into a chaos storm! [<color=#214051>Projectile</color>]", null ) },
             { "UseChaosCluster", new Tuple<string, string>( "Throw out a cluster of chaos orbs that explode on landing! [<color=#3b4c35>No Type</color>]", null ) },
             { "UseChaosBeam", new Tuple<string, string>( "Summon a beam of chaos energy! [<color=#3b4c35>No Type</color>]", null ) },
             { "UseChaosEraser", new Tuple<string, string>( "Throws out an orb of primal chaos that erases the first enemy hit from existence! Enemies that cannot be erased will take heavy damage instead. [<color=#214051>Projectile</color>]", null ) },
             { "UsePhantomKnights", new Tuple<string, string>( "Summon a legion of Chaos Knights that rush forth and attack enemies! [<color=#512323>Melee</color>] [Summon]", null ) },
             //Chaos 2
             { "UseChaosBall", new Tuple<string, string>( "Hold to charge up and release an unstable cluster of chaos orbs that unleashes a barrage of strikes on any enemy hit! [<color=#214051>Projectile</color>]", null ) },
             { "UseChaosSwordStorm", new Tuple<string, string>( "Summon a field of void blades that rain down on foes! [<color=#3b4c35>No Type</color>]", null ) },

        };

        public static Dictionary<string, string> CustomUIText = new Dictionary<string, string>()
        {
            { "Vigor_desc" , "<color=#f90233>10% Health!</color>                                     <color=#7a7a7a>1 Defense</color>"},
            { "Grit_desc" , "<color=#9d8766>8% Armor</color>                                    <color=#7a7a7a>1 Defense</color>"},
            { "Tempo_desc" , "<color=#5f7dad>12% Cooldown</color>                               <color=#13a835>8% Movement Speed</color>"},
            { "Pace_desc" , "<color=#13a835>16% Movement Speed</color>                       <color=#b5e61d>5% Evade</color>"},
            { "Switch_desc" , "<color=#b5e61d>10% Evade</color>                                    <color=#e8d925>6% Critical hit chance</color>"},
            { "Awe_desc" , "<color=#e8d925>12% Critical hit chance</color>                 <color=#e8d925>20% Critical Damage</color>"},
            { "Rule_desc" , "<color=#c60d0d>10% Damage</color>                                 <color=#9d8766>4% Armor</color>"},
            { "Venture_desc" , "<color=#c60d0d>10% Damage</color>                               <color=#13a835>16% Movement Speed</color>                            <color=#5f7dad>12% Cooldowns</color>                                 <color=#f90233>-40% Health!</color>"},
            { "Pride_desc" , "<color=#f90233>Reduces Max Health to 1</color>                   <color=#0060f0>Shields Disabled</color>"},
            { "Hope_desc" , "<color=#f90233>5% Health!</color>                                <color=#13a835>8% Movement Speed</color>                                        <color=#c60d0d>5% Damage</color>                                    <color=#e8d925>6% Critical hit chance</color>"},
            { "Patience_desc" , "<color=#f90233>5% Health!</color>                                  <color=#13a835>8% Movement Speed</color>                                       <color=#9d8766>4% Armor</color>                                      <color=#b5e61d>5% Evade</color>"},
        };

        // new code starts here

        public static void ChangeSpellDescriptions() {

            try {
                Type SkillInfoType = typeof( TextManager ).GetNestedType( "SkillInfo", BindingFlags.NonPublic );
                FieldInfo SkillDictField = typeof( TextManager ).GetField( "skillInfoDict", BindingFlags.NonPublic | BindingFlags.Static );

                PropertyInfo SkillDictItemProperty = SkillDictField.FieldType.GetProperty( "Item" );
                IDictionary SkillDictValue = (IDictionary)SkillDictField.GetValue( null );

                Dictionary<string, object> NewSkillInfoDict = new Dictionary<string, object>();

                //Retrieve new spell descriptions and store them
                foreach( object SkillId in SkillDictValue.Keys ) {
                    Tuple<string, string> newDescription = GetNewSpellDescription( SkillId as string );

                    if( newDescription != null ) {
                        object CurSkillInfo = SkillDictItemProperty.GetValue( SkillDictValue, new[] { SkillId as string } );

                        if( newDescription.First != null )
                            SkillInfoType.GetField( "description" ).SetValue( CurSkillInfo, newDescription.First );

                        if( newDescription.Second != null )
                            SkillInfoType.GetField( "empowered" ).SetValue( CurSkillInfo, newDescription.Second );

                        NewSkillInfoDict.Add( SkillId as string, CurSkillInfo );
                    }
                }

                //Apply stored descriptions
                foreach( KeyValuePair<string, object> kvp in NewSkillInfoDict )
                    SkillDictItemProperty.SetValue( SkillDictValue, kvp.Value, new[] { kvp.Key as string } );

            } catch( Exception e ) {
                Debug.Log( "Something failed while changing spells descriptions :( See Error Below." );
                Debug.Log( e.Message );
            }

        }


        public static Tuple<string, string> GetNewSpellDescription(string skillID) {
            if( CustomSkillsDescriptions.ContainsKey( skillID ) )
                return CustomSkillsDescriptions[skillID];

            return null;
        }



        //end here



        public override void Init() {
            base.Init();
            this.ModID = "Clarity";
        }

        private static Type objRefType;
        private static FieldInfo wrRefFieldInfo;
        private static FieldInfo textField;
        private static MethodInfo empDescInfo;

        public override void OnLoad() {
            base.OnLoad();
            On.GameDataManager.LoadInitial += HookLoadInitial;

            objRefType = typeof( WardrobeUI ).GetNestedType( "WardrobeObjRef", BindingFlags.NonPublic );
            wrRefFieldInfo = typeof( WardrobeUI ).GetField( "wrRef", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance );

            textField = objRefType.GetField( "infoDescText" );
            empDescInfo = typeof( TextManager ).GetMethod( "GetEmpoweredDescription", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static );

            Debug.Log( objRefType + "|" + wrRefFieldInfo + "|" + textField );


        }


        public static void HookLoadInitial(On.GameDataManager.orig_LoadInitial original, bool init) {
            original( init );
            if( init ) {
                System.IO.File.WriteAllText( Application.dataPath + "/SpellStats.json", ChaosBundle.Get<TextAsset>( "Assets/Data/UI/SkillsInfo_eng.json" ).text );
                System.IO.File.WriteAllText( Application.dataPath + "/ItemStats.json", ChaosBundle.Get<TextAsset>( "Assets/Data/UI/ItemsInfo_eng.json" ).text );
                System.IO.File.WriteAllText( Application.dataPath + "/OutfitStats.json", ChaosBundle.Get<TextAsset>( "Assets/Data/UI/OutfitsInfo_eng.json" ).text );
                System.IO.File.WriteAllText( Application.dataPath + "/UIText.json", ChaosBundle.Get<TextAsset>( "Assets/Data/UI/UITexts_eng.json" ).text );
                try {
                    On.TextManager.GetItemDescription += HookItemDescription;
                    On.TextManager.GetSkillDescription += HookSpellDescription;
                    On.TextManager.GetUIText += HookUIText;
                    On.WardrobeUI.LoadInfo += HookOutfitLoad;


                    //spell stuff starts here
                    //ChangeSpellDescriptions();



                } catch( System.Exception e ) {
                    UnityEngine.Debug.LogError( e );
                }
            }
        }


        public static string HookItemDescription(On.TextManager.orig_GetItemDescription original, string itemid, int playerID) {
            string orig_itemdescription = original( itemid, playerID );
            if( CustomItemDescriptions.ContainsKey( itemid ) ) {
                orig_itemdescription = CustomItemDescriptions[itemid];
            }
            return orig_itemdescription;
        }

        public static string HookSpellDescription(On.TextManager.orig_GetSkillDescription original, string givenID, bool empowered = false, bool isChaos = false) {

            string orig_skilldescription = original( givenID, empowered, isChaos );
            if( CustomSkillsDescriptions.ContainsKey( givenID ) ) {
                Tuple<string, string> text = CustomSkillsDescriptions[givenID];


                orig_skilldescription = text.First;

                if( empowered && text.Second != null )
                    orig_skilldescription += text.Second;
                else
                    orig_skilldescription += empDescInfo.Invoke( null, new object[] { givenID, empowered, isChaos } ) as String;

            }

            return orig_skilldescription;
        }

        public static string HookUIText(On.TextManager.orig_GetUIText original, string textID) {

            try {
                string orig_UIText = original( textID );

                if( CustomUIText.ContainsKey( textID ) )
                    orig_UIText = CustomUIText[textID];

                return orig_UIText;
            } catch( System.Exception e ) {
                Debug.LogError( e );
                return original( textID );
            }

        }

        public static void HookOutfitLoad(On.WardrobeUI.orig_LoadInfo orig, WardrobeUI instance, Outfit outfit) {
            orig( instance, outfit );

            if( outfit.unlocked ) {
                object wrRef = wrRefFieldInfo.GetValue( instance );

                Text t = textField.GetValue( wrRef ) as Text;

                if( CustomUIText.ContainsKey( outfit.outfitID + "_desc" ) ) {
                    t.text = CustomUIText[outfit.outfitID + "_desc"];
                }
            }
        }




    }
}
