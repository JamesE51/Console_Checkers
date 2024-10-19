using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Checkers
{/*
    internal class MinMaxTree<T>
    {
        MinMaxNode<T> Head;
        public void GenerateLimitedTree(MinMaxNode<T> Curr, int MaxDepth, Func<T, float> EvaluateScore, Func<MinMaxNode<T>,List<MinMaxNode<T>>> MakeChildren, Func<T, bool> TerminalStateCheck)
        {
            Curr.Depth = MaxDepth;
            RecurGenLimitedTree(Curr, EvaluateScore, MakeChildren, TerminalStateCheck);
        }
        private void RecurGenLimitedTree(MinMaxNode<T> Curr, Func<T, float> EvaluateScore, Func<MinMaxNode<T>, List<MinMaxNode<T>>> MakeChildren, Func<T, bool> TerminalStateCheck)
        {
            Curr.Depth --;
            if (Curr.Depth != 0)
            {
                Curr.Children = MakeChildren(Curr);
                for (int i = 0; i < Curr.Children.Count; i++)
                {
                    RecurGenLimitedTree(Curr.Children[i], EvaluateScore, MakeChildren, TerminalStateCheck);
                }
            }
            else
            {
                Curr.Score = EvaluateScore(Curr.Value);
                return;
            }
            if(TerminalStateCheck(Curr.Value))
            {
                Curr.Score = EvaluateScore(Curr.Value);
            }
            else
            {
                Curr.SetCostFromChildren();
            }
        }



    }*/
}
