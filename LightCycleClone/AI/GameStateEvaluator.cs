using LightCycleClone.DataStructures;
using LightCycleClone.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightCycleClone.AI
{
    public class GameStateEvaluator
    {
        private readonly List<Func<GameWorld, int>> _heuristics;
        private int _recurseCount;

        public int RecurseCount
        {
            get
            {
                return _recurseCount;
            }
            set
            {
                _recurseCount = value;
            }
        }

        public GameStateEvaluator()
        {
            _recurseCount = 2;
            _heuristics = new List<Func<GameWorld, int>>();
        }

        public void AddHeuristic(Func<GameWorld, int> func)
        {
            _heuristics.Add(func);
        }

        public void ClearHeuristics()
        {
            _heuristics.Clear();
        }

        public EvalNode GetStateTree(GameWorld state, Guid playerId)
        {
            var rootNode = new EvalNode();
            PopulateStateTree(state, playerId, rootNode, _recurseCount);
            return rootNode;
        }

        public void PopulateStateTree(GameWorld state, Guid playerId, EvalNode node, int recurseCount)
        {
            if (recurseCount <= 0)
            {
                return;
            }

            var availableActions = EnumUtil.GetList<PlayerAction>();

            foreach(var action in availableActions)
            {
                var pState = state.Copy();
                var player = pState.GetPlayer(playerId);

                player.SetAction(action);

                pState.Update();

                var data = new EvalData
                {
                    Action = action,
                    Value = EvalGameState(pState)
                };

                var newNode = new EvalNode(data);
                node.AddChild(newNode);
                PopulateStateTree(pState, playerId, newNode, recurseCount-1);
            }
        } 

        private int EvalGameState(GameWorld state)
        {
            var result = 0;

            foreach(var func in _heuristics)
            {
                result += func.Invoke(state);
            }

            return result;
        }
    }
}
