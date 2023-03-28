using Menu.Remix.MixedUI;

namespace HeavierSpear
{
    public sealed class HeavierSpearOptions : OptionInterface
    {
        public readonly Configurable<bool> flipLizard;
        public readonly Configurable<bool> modifyStun;

        public HeavierSpearOptions()
        {
            flipLizard = config.Bind(nameof(flipLizard), true);
            modifyStun = config.Bind(nameof(modifyStun), true);
        }

        public override void Initialize()
        {
            base.Initialize();
            var opTab = new OpTab(this, "Options");
            Tabs = new[] { opTab };
            var titleBox = new OpLabel(16f, 560f, "Configuration", true);
            var flipLizardCheckBox = new OpCheckBox(flipLizard, 16f, 510f);
            flipLizardCheckBox.description = "Causes lizards to flip over upon being hit in the head by a thrown spear. Default is true.";
            var flipLizardLabel = new OpLabel(flipLizardCheckBox.PosX + 40f, flipLizardCheckBox.PosY, "Spears flip lizards");
            var modifyStunCheckBox = new OpCheckBox(modifyStun, 16f, 460f);
            modifyStunCheckBox.description = "Increases the stun of a spear to the same as a rock. Default is true.";
            var modifyStunLabel = new OpLabel(modifyStunCheckBox.PosX + 40f, modifyStunCheckBox.PosY, "Increased spear stun");
            opTab.AddItems(titleBox, flipLizardCheckBox, flipLizardLabel, modifyStunCheckBox, modifyStunLabel);
        }
    }
}