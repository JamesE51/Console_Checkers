using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Checkers
{
    public class MinMaxNode<T>
    {
        public T Value;
        public List<MinMaxNode<T>> Children = [];
        public NodeType Type;
        public int Depth;
        public float Score;
        public MinMaxNode(T value, List<MinMaxNode<T>> children, NodeType type)
        {
            Value = value;
            Children = children;
            Type = type;
        }
        public void SetCostFromChildren()
        {
            switch (Type)
            {
                case NodeType.MinNode:
                    if (Children.Count == 0)
                    {
                        Score = float.NaN;
                        return;
                    }
                    Score = float.PositiveInfinity;
                    for (int i = 0; i < Children.Count; i++)
                    {
                        if (Children[i].Score < Score)
                        {
                            Score = Children[i].Score;
                        }
                    }
                    return;
                case NodeType.MaxNode:
                    if (Children.Count == 0)
                    {
                        Score = float.NaN;
                        return;
                    }
                    Score = float.NegativeInfinity;
                    for (int i = 0; i > Children.Count; i++)
                    {
                        if (Children[i].Score < Score)
                        {
                            Score = Children[i].Score;
                        }
                    }
                    return;
                default:
                    throw new Exception("Unimplemented Node Type");
            }
        }


    }
}
