using LightCycleClone.DataStructures;
using LightCycleClone.GameObjects.Character;
using LightCycleClone.GameObjects.World;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightCycleClone.AI
{
    public class PlayerAgent
    {
        private Guid _playerId;
        private GameStateEvaluator _evaluator;

        private Node<EvalData> _root;

        public PlayerAgent(Player player)
        {
            _playerId = player.Id;
            _evaluator = new GameStateEvaluator();
        }

        private int EvalPlayerState(GameWorld state)
        {
            var player = state.GetPlayer(_playerId);
            return player.State == PlayerState.Alive ? 0 : -100;
        }

        private int EvalOpponentState(GameWorld currState, GameWorld state)
        {
            var currOpponents = currState.GetLivePlayers().Count(x => x.Id != _playerId);
            var projOpponents = currState.GetLivePlayers().Count(x => x.Id != _playerId);
            var delta = currOpponents - projOpponents;
            return delta * 5;
        }

        private void Evaluate(GameWorld world)
        {
            _evaluator.ClearHeuristics();
            _evaluator.AddHeuristic(state => EvalPlayerState(state));
            _evaluator.AddHeuristic(state => EvalOpponentState(world, state));
            var stateTree = _evaluator.GetStateTree(world, _playerId);
        }

        public void Update(GameWorld world)
        {
            Evaluate(world);
        }
    }
}
