using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flame.Geometry;
namespace Flame.Sprites.Modules
{
    public class Path: Module
    {
        private Queue<Vector> _nodes = new Queue<Vector>();
        private Vector _targetNode;
        private bool _targetSet = false;
        //Algorithms.PathFinderFast _pathFinder;
        public Path(Sprite sprite) : base(sprite)
        {
            ArriveDistance = 5;
            Enabled = true;
            MoveSpeed = 100;
            //_pathFinder = new Algorithms.PathFinderFast(new byte[,] { });
        }
        public double ArriveDistance { get; set; }
        public double MoveSpeed { get; set; }
        public bool Enabled { get; set; }

        public void Update()
        {
            if (Enabled && _targetNode != null)
            {
                if (!_targetSet )
                {
                    Sprite.Body.Velocity = (Sprite.Position - _targetNode).Normalize();
                    Sprite.Body.Velocity *= MoveSpeed;
                    _targetSet = true;
                }

                if (Sprite.Position.DistanceTo(_targetNode) <= ArriveDistance)
                {
                    _targetSet = false;
                    if (_nodes.Count <= 0)
                    {
                        Sprite.Emit("ArrivedAtDestination", new Message(Sprite));
                        Sprite.Body.Velocity.Set(0, 0);
                    }
                    else
                    {
                        Sprite.Emit("ArrivedAtNode", new Message(Sprite));
                        _targetNode = _nodes.Dequeue();
                    }
                }
            }
        }

        public void SetPath(Vector[] nodes)
        {
            _nodes = new Queue<Vector>(nodes);
            _targetNode = _nodes.Dequeue();

            Debug.DebugConsole.Output("Flame", String.Format("Path for sprite {0} set.", Sprite.Uid));
        }

        public void SetGrid(byte[,] grid)
        {
            //_pathFinder = new Algorithms.PathFinderFast(grid);
        }

    }
}
