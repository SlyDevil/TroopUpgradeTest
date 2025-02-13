using HarmonyLib;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using TOR_Core.CampaignMechanics;

namespace TroopUpgradeTest
{
    [HarmonyPatch(typeof(TORPartyUpgraderCampaignBehavior), "UpgradeReadyTroops")]
    public class UpgradeReadyTroopsTranspile
    {
        static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            var codes = new List<CodeInstruction>(instructions);
            var index = -1;
            for (var i = 0; i < codes.Count; i++)
            {
                //if ((float)memberRoster.GetElementCopyAtIndex(i).Number >= num3)
                //blt.un.s is only present at the if statement to jump to the UpgradeTroop  call block
                //it is immediately follow by the instruction that I want to change
                //however, why don't I just change it to an unconditional branch to the same intruction/label and render the if statement useless; in this case I wouldn't need to care about changing the label or anything, i'd just swap the instruction?
                //I can just NoOp the br.s and it will exit the if statement after having done nothing, then continuing to UpgradeTroops
                //no, the opcodes will continue going in order if I don't jump elsewhere; but I can just change the br.s and it's label to match the preceding blt.un.s and the if statement will always jump to the correct place regardless of how it evaluates
                if (codes[i].opcode == OpCodes.Blt_Un_S)
                {
                    //because the instruction is unique this should have no false positives 
                    index = i;
                    break;
                }
            }
            if (index != -1)
            {
                //ILGenerator.DefineLabel();
                //codes[index+1].operand = 
                //codes[index].opcode = OpCodes.Beq_S;
                //codes.RemoveRange(index - 2, 3);
                var priorOperand = codes[index].operand;
                var priorLabel = codes[index].labels;
                codes[index + 1].operand = priorOperand;
                codes[index + 1].labels = priorLabel;
            }

            return codes.AsEnumerable();
        }
        /*
        [HarmonyPatch(typeof(TORPartyUpgraderCampaignBehavior), "UpgradeReadyTroops")]
        public class UpgradeReadyTroops
        {
            public static bool Prefix(PartyBase party)
            {
                if (party.LeaderHero.Clan == Clan.PlayerClan)
                {
                    InformationManager.DisplayMessage(new InformationMessage(party.LeaderHero.Gold.ToString() + " gold", Colors.Magenta));
                }

                if (party != PartyBase.MainParty && party.IsActive)
                {
                    TroopRoster memberRoster = party.MemberRoster;
                    PartyTroopUpgradeModel partyTroopUpgradeModel = Campaign.Current.Models.PartyTroopUpgradeModel;
                    for (int i = 0; i < memberRoster.Count; i++)
                    {
                        TroopRosterElement elementCopyAtIndex = memberRoster.GetElementCopyAtIndex(i);
                        if (partyTroopUpgradeModel.IsTroopUpgradeable(party, elementCopyAtIndex.Character))
                        {
                            List<TORPartyUpgraderCampaignBehavior.TORTroopUpgradeArgs> possibleUpgradeTargets = TORPartyUpgraderCampaignBehavior.GetPossibleUpgradeTargets(party, elementCopyAtIndex);
                            if (possibleUpgradeTargets.Count > 0)
                            {
                                TORPartyUpgraderCampaignBehavior.TORTroopUpgradeArgs upgradeArgs = this.SelectPossibleUpgrade(possibleUpgradeTargets);
                                if (party.IsMobile && party.MobileParty.IsLordParty && memberRoster.Contains(upgradeArgs.UpgradeTarget))
                                {
                                    PartyTemplateObject defaultPartyTemplate = party.LeaderHero.Clan.DefaultPartyTemplate;
                                    float num = (float)memberRoster.TotalManCount;
                                    if (defaultPartyTemplate.Stacks.Any((PartyTemplateStack x) => x.Character == upgradeArgs.UpgradeTarget))
                                    {
                                        float num2 = 0f;
                                        PartyTemplateStack partyTemplateStack = default(PartyTemplateStack);
                                        foreach (PartyTemplateStack partyTemplateStack2 in defaultPartyTemplate.Stacks)
                                        {
                                            if (partyTemplateStack2.Character == upgradeArgs.UpgradeTarget)
                                            {
                                                partyTemplateStack = partyTemplateStack2;
                                            }
                                            num2 += (float)partyTemplateStack2.MaxValue;
                                        }
                                        if (num2 == 0f)
                                        {
                                            goto IL_01AD;
                                        }
                                        float num3 = (float)partyTemplateStack.MaxValue / num2 * num;
                                        if ((float)memberRoster.GetElementCopyAtIndex(i).Number >= num3)
                                        {
                                            goto IL_01AD;
                                        }
                                    }
                                    else
                                    {
                                        float num5 = (float)memberRoster.GetTroopCount(upgradeArgs.UpgradeTarget);
                                        float num4 = 0.1f;
                                        if (num5 > num4 * num)
                                        {
                                            goto IL_01AD;
                                        }
                                    }
                                }
                                this.UpgradeTroop(party, i, upgradeArgs);
                            }
                        }
                        IL_01AD:;
                    }
                }
                return false;
            }
        }
        */
    }
}
