
using UnityEditor;

namespace GenshinImpactMovementSystem
{
    public class PlayerCombatStateMachine : StateMachine
    {

        public Player Player { get; }
       
        public PlayerCombat PlayerCombat { get; }

        public PlayerMovementStateMachine movementStateMachine { get; }

        public PlayerCombatState CombatState { get; }

        public PlayerToggledState ToggledState { get; }

        public PlayerTeleportState TeleportState { get; }

        public PlayerInteractiveAnimationTrigger InteractiveAnimation { get; }

       

        //Data
        public PlayerCombatStateMachine(Player player)
        {
            Player = player;

            movementStateMachine = Player.movementStateMachine;

            CombatState = new PlayerCombatState(this);         
            ToggledState = new PlayerToggledState(this);
            TeleportState = new PlayerTeleportState(this);  

            InteractiveAnimation = new PlayerInteractiveAnimationTrigger(this);
           
        }
    }
}
