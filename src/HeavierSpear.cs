using BepInEx;
using System.Security.Permissions;
using UnityEngine;

[assembly: SecurityPermission(SecurityAction.RequestMinimum, SkipVerification = true)]
namespace HeavierSpear
{
    [BepInPlugin("qu.heavier_spear", "HeavierSpears", "1.0.0")]
    public class HeavierSpear : BaseUnityPlugin
    {

        public const string ModID = "qu.heavier_spear";
        
        private readonly HeavierSpearOptions _options;

        public HeavierSpear()
        {
            _options = new HeavierSpearOptions(); 
        }
        
        private void OnEnable()
        {
            On.Lizard.Violence += LizardViolenceHook;
            On.RainWorld.OnModsInit += RainWorldOnOnModsInit;
        }

        private void LizardViolenceHook(
            On.Lizard.orig_Violence orig, 
            Lizard self, 
            BodyChunk source,
            Vector2? directionAndMomentum,
            BodyChunk hitChunk,
            PhysicalObject.Appendage.Pos onAppendagePos,
            Creature.DamageType type,
            float damage,
            float stunBonus)
        {
            var bl = false;

            if (directionAndMomentum.HasValue && self.HitHeadShield(directionAndMomentum.Value) &&
                source?.owner is Spear && self.Template.type != CreatureTemplate.Type.RedLizard)
            {
                if (_options.modifyStun.Value)
                {
                    stunBonus = 45;
                    if (ModManager.MSC && source.owner != null &&
                        source.owner.room.game.IsArenaSession && source.owner.room.game.GetArenaGameSession.chMeta != null)
                        stunBonus = 90;
                }

                bl = true;
            }

            // Calls original method
            orig(self, source, directionAndMomentum, hitChunk, onAppendagePos, type, damage, stunBonus);

            if (!bl || !_options.flipLizard.Value) return;
            
            self.turnedByRockDirection = (int) Mathf.Sign(source.pos.x - source.lastPos.x);
            self.turnedByRockCounter = 20;
            
        }

        private void RainWorldOnOnModsInit(On.RainWorld.orig_OnModsInit orig, RainWorld self)
        {
            // Calls original method
            orig(self); 
            // Registers the options menu
            MachineConnector.SetRegisteredOI(ModID, _options);
        }
    }
}