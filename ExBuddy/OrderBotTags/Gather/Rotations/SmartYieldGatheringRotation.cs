namespace ExBuddy.OrderBotTags.Gather.Rotations
{
    using ExBuddy.Attributes;
    using ExBuddy.Enumerations;
    using ExBuddy.Interfaces;
    using ff14bot;
    using ff14bot.Managers;
    using System.Threading.Tasks;

    //Name, RequiredTime, RequiredGpBreakpoints
    [GatheringRotation("SmartYield", 25, 900, 850, 800, 750, 700, 650, 600, 550, 500, 450, 400, 350, 250, 0)]
    public class SmartYieldGatheringRotation : SmartGatheringRotation, IGetOverridePriority
    {
        #region IGetOverridePriority Members

        int IGetOverridePriority.GetOverridePriority(ExGatherTag tag)
        {
            if (tag.CollectableItem != null)
            {
                return -1;
            }

            if (tag.GatherIncrease == GatherIncrease.Yield
                || (tag.GatherIncrease == GatherIncrease.Auto && Core.Player.ClassLevel >= 40))
            {
                return 9001;
            }

            return -1;
        }

        #endregion IGetOverridePriority Members

        public override async Task<bool> ExecuteRotation(ExGatherTag tag)
        {
            if (GatheringManager.SwingsRemaining > 4 || ShouldForceUseRotation(tag, Core.Player.ClassLevel))
            {
                await IncreaseYield(tag);
                return await base.ExecuteRotation(tag);
            }

            return true;
        }
    }
}