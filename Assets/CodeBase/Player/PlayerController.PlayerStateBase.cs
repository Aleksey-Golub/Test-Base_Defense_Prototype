using Assets.CodeBase.Logic.CharacterComponents;

namespace Assets.CodeBase.Player
{
    public partial class PlayerController
    {
        private abstract class PlayerStateBase : StateBase<PlayerController>
        {
            protected PlayerStateBase(PlayerController player)
            {
                Controller = player;
            }
        }
    }
}