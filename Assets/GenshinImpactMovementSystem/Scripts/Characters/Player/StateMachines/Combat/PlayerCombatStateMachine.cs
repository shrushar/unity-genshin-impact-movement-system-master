
using UnityEditor;

namespace GenshinImpactMovementSystem
{
    public class PlayerCombatStateMachine : StateMachine
    {

        public Player Player { get; }
        
        public PlayerToggleStateMachine ToggleStateMachine { get; }

        public PlayerMovementStateMachine PlayerMovementStateMachine { get; }

        public PlayerCombatState CombatState { get; }

        public PlayerToggledState ToggledState { get; }
        public PlayerCombatStateMachine(Player player)
        {
            Player = player;

            ToggleStateMachine = new PlayerToggleStateMachine(this);

            CombatState = new PlayerCombatState(this);         
            ToggledState = new PlayerToggledState(this);    
        }
    }
}
