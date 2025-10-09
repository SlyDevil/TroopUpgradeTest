using HarmonyLib;
using System.Linq;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.ComponentInterfaces;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;
using TOR_Core.CampaignMechanics;


namespace TroopUpgradeTest
{
    public class SubModule : MBSubModuleBase
    {
        protected override void OnSubModuleLoad()
        {
            base.OnSubModuleLoad();
            //Harmony harmony = new Harmony("upgrade_troops_smthg");
            //harmony.PatchAll();
        }

        protected override void OnSubModuleUnloaded()
        {
            base.OnSubModuleUnloaded();

        }

        protected override void InitializeGameStarter(Game game, IGameStarter starterObject)
        {
            base.InitializeGameStarter(game, starterObject);
            if (Game.Current.GameType is Campaign && starterObject is CampaignGameStarter)
            {
                var starter = starterObject as CampaignGameStarter;
                if (starter != null)
                {
                    starter.RemoveBehaviors<TORPartyUpgraderCampaignBehavior>();
                    starter.AddBehavior(new FancierPartyUpgraderCampaignBehavior());
                }
                }
        }

        public override void OnGameInitializationFinished(Game game)
        {
            base.OnGameInitializationFinished(game);
            /*if (_lateHarmonyPatchApplied) {return;}
            Harmony harmony = new Harmony("upgrade_troops_smthg");
            harmony.PatchAll();
            _lateHarmonyPatchApplied = true;
            */
        }

        protected override void OnBeforeInitialModuleScreenSetAsRoot()
        {
            base.OnBeforeInitialModuleScreenSetAsRoot();
            InformationManager.DisplayMessage(new InformationMessage("troop upgrade test present", Colors.Green));
        }

        //private static bool _lateHarmonyPatchApplied = false;
        
        protected override void OnGameStart(Game game, IGameStarter gameStarterObject)
        {
            if (Game.Current.GameType is Campaign && gameStarterObject is CampaignGameStarter)
            {
                var existingModel = GetGameModel<PartyTrainingModel>(gameStarterObject);
                //GetGameModel will return the default model if it doesn't find any others?
                if (existingModel is null) 
                    Debug.Print("Existing model is null");

                gameStarterObject.AddModel(new ReplacementPartyTrainingModel(existingModel));
                Debug.Print("Existing model : " + existingModel.ToString());
            }
        }

        private T? GetGameModel<T>(IGameStarter gameStarterObject) where T : GameModel
        {
            var models = gameStarterObject.Models.ToArray();

            for (int index = models.Length - 1; index >= 0; --index)
            {
                if (models[index] is T gameModel1)
                    return gameModel1; //models are added in order of mods loading; start from last model and work backwards to find the most recent model of a given type added
            }
            return default;
        }
    }
}