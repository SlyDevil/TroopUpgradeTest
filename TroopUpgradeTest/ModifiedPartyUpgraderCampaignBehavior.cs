using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.CharacterDevelopment;
using TaleWorlds.CampaignSystem.ComponentInterfaces;
using TaleWorlds.CampaignSystem.GameComponents;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.Library;
using TOR_Core.CampaignMechanics;
using TOR_Core.Extensions;
using TOR_Core.Models;

namespace TroopUpgradeTest
{
    
    /*HandleRemoveParty(MobileParty party)
        * if (party.GetMemberHeroes().Any((Hero x) => x.IsAICompanion()))
		{
			foreach (Hero companion in from x in party.GetMemberHeroes()
				where x.IsAICompanion()
				select x)
			{
				EndCaptivityAction.ApplyByEscape(companion, null);
				if (party.MemberRoster.Contains(companion.CharacterObject))
				{
					party.MemberRoster.AddToCounts(companion.CharacterObject, -1, false, 0, 0, true, -1);
				}
				this.MoveHeroToHomeTown(companion);
			}
		}
    */
    public class ModifiedPartyUpgraderCampaignBehavior  : TORPartyUpgraderCampaignBehavior
    {
        /* I can inherit its methods, but I can't override because it's not set up as an overridable class inheriting from a base like I did with the model
        public override void UpgradeReadyTroops(PartyBase party)
        {

        }
        /*PartyTroopUpgradeModel _previousModel;

        public ModifiedPartyUpgraderCampaignBehavior (PartyTroopUpgradeModel previousModel)
        {
            _previousModel = previousModel;
            _previousModel ??= new DefaultPartyTroopUpgradeModel();
        }

        public override bool CanPartyUpgradeTroopToTarget(PartyBase party, CharacterObject character, CharacterObject target)
        {
            return _previousModel.CanPartyUpgradeTroopToTarget(party, character, target);
        }

        public override bool DoesPartyHaveRequiredItemsForUpgrade(PartyBase party, CharacterObject upgradeTarget)
        {
            return _previousModel.DoesPartyHaveRequiredItemsForUpgrade(party, upgradeTarget);
        }

        public override bool DoesPartyHaveRequiredPerksForUpgrade(PartyBase party, CharacterObject character, CharacterObject upgradeTarget, out PerkObject requiredPerk)
        {
            return _previousModel.DoesPartyHaveRequiredPerksForUpgrade(party, character, upgradeTarget, out requiredPerk);
        }

        public override int GetGoldCostForUpgrade(PartyBase party, CharacterObject characterObject, CharacterObject upgradeTarget)
        {
            if (party.LeaderHero.Clan == Clan.PlayerClan)
            {
                InformationManager.DisplayMessage(new InformationMessage(party.LeaderHero.Gold.ToString() + " gold", Colors.Magenta));
            }
            return _previousModel.GetGoldCostForUpgrade(party, characterObject, upgradeTarget);
        }

        public override int GetSkillXpFromUpgradingTroops(PartyBase party, CharacterObject troop, int numberOfTroops)
        {
            return _previousModel.GetSkillXpFromUpgradingTroops(party, troop, numberOfTroops);
        }

        public override float GetUpgradeChanceForTroopUpgrade(PartyBase party, CharacterObject troop, int upgradeTargetIndex)
        {
            return 9999f;
        }

        public override int GetXpCostForUpgrade(PartyBase party, CharacterObject characterObject, CharacterObject upgradeTarget)
        {
            return _previousModel.GetXpCostForUpgrade(party, characterObject, upgradeTarget);
        }

        public override bool IsTroopUpgradeable(PartyBase party, CharacterObject character)
        {
            return _previousModel.IsTroopUpgradeable(party, character);
        }
        */
    }
}
