using HarmonyLib;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;


namespace TroopUpgradeTest
{
    public class SubModule : MBSubModuleBase
    {
        protected override void OnSubModuleLoad()
        {
            base.OnSubModuleLoad();

        }

        protected override void OnSubModuleUnloaded()
        {
            base.OnSubModuleUnloaded();

        }

        protected override void OnBeforeInitialModuleScreenSetAsRoot()
        {
            base.OnBeforeInitialModuleScreenSetAsRoot();            
            Harmony harmony = new Harmony("upgrade_troops_smthg");
            harmony.PatchAll();
            InformationManager.DisplayMessage(new InformationMessage("upgrade test loaded", Colors.Green));
        }
        /*
        protected override void OnGameStart(Game game, IGameStarter gameStarterObject)
        {
            if (Game.Current.GameType is Campaign && gameStarterObject is CampaignGameStarter)
            {
                var existingModel = GetGameModel<VolunteerModel>(gameStarterObject);
                //GetGameModel will return the default model if it doesn't find any others?
                gameStarterObject.AddModel(new MaximumIndexHeroCanRecruitFromHeroOverRideModel(existingModel));
            }
        }

        private T? GetGameModel<T>(IGameStarter gameStarterObject) where T : GameModel
        {
            var models = gameStarterObject.Models.ToArray();

            for (int index = models.Length - 1; index >= 0; --index)
            {
                if (models[index] is T gameModel1)
                    return gameModel1;
            }
            return default;
        }
        */
    }
}