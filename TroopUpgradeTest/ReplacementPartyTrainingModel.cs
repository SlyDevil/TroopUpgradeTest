using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.ComponentInterfaces;
using TaleWorlds.CampaignSystem.GameComponents;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Roster;
using TaleWorlds.Core;
using TOR_Core.CharacterDevelopment;
using TOR_Core.Extensions;
using TOR_Core.Utilities;

namespace TroopUpgradeTest
{
    internal class ReplacementPartyTrainingModel : PartyTrainingModel
    {
        PartyTrainingModel _previousModel;
        PartyTrainingModel _defaultModel;

        public ReplacementPartyTrainingModel(PartyTrainingModel previousModel)
        {
          _previousModel = previousModel;
          _previousModel ??= new DefaultPartyTrainingModel();
          _defaultModel = new DefaultPartyTrainingModel(); //not sure how to call the first inheritance of the base as the inheritance stack would currently be Interface, DefaultModel, TorModel, ThisModel
            //I could have inherited from DefaultModel in the first place, but we're making this complex just cause
        }

        public override int CalculateXpGainFromBattles(FlattenedTroopRosterElement troopRosterElement, PartyBase party)
        {
            return _previousModel.CalculateXpGainFromBattles(troopRosterElement, party);
        }

        public override int GenerateSharedXp(CharacterObject troop, int xp, MobileParty mobileParty)
        {
            return _previousModel.GenerateSharedXp(troop, xp, mobileParty);
        }

        public override ExplainedNumber GetEffectiveDailyExperience(MobileParty mobileParty, TroopRosterElement troop)
        {
            ExplainedNumber result = default(ExplainedNumber);
            
            if (troop.Character.IsHero) {return result;} //this method doesn't apply to heroes; return default and save calculations

            result = _defaultModel.GetEffectiveDailyExperience(mobileParty, troop);
            
            if (mobileParty.IsLordParty && mobileParty != MobileParty.MainParty)
            {
                result.Add((float)troop.Character.Tier * 10f);//base adds 10+2*Tier, or 15+3*Tier if clan leader
            }

            if(mobileParty.HasPerk(TORPerks.GunPowder.FiringDrills, true) && troop.Character.Equipment.HasWeaponOfClass(WeaponClass.Cartridge))
            {
                result.Add(TORPerks.GunPowder.FiringDrills.SecondaryBonus);
            }
            if (mobileParty.HasPerk(TORPerks.Faith.Blessed, true) && troop.Character.IsReligiousUnit() && mobileParty.HasAnyActiveBlessing())
            {
                result.Add(TORPerks.Faith.Blessed.SecondaryBonus);
            }

            return result;
        }

        public override int GetXpReward(CharacterObject character)
        {
            return _previousModel.GetXpReward(character);
        }
    }
}