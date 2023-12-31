﻿using Maze_Game.GameWorlds;
using Maze_Game.Math;
using System.Text;

namespace Maze_Game.Rendering
{
    public class GameRenderer
    {
        private GameWorld _gameWorld;
        IntVector2 _viewportSize;

        public GameRenderer(GameWorld gameWorld, IntVector2 viewportSize)
        {
            _gameWorld = gameWorld;
            _viewportSize = viewportSize;
        }

        public string RenderFrame()
        {
            StringBuilder frameSb = new StringBuilder(_viewportSize.X * _viewportSize.Y);
            StringBuilder rowSb = new StringBuilder(_viewportSize.X);
            List<Renderer> renderers = _gameWorld.FindAllComponentsInWorld<Renderer>();
            char hitChar = ' ';

            for (int y = 0;  y <= _viewportSize.Y; y++)
            {
                rowSb.Clear();
                for (int x = 0;  x < _viewportSize.X; x++)
                {
                    bool didHitRenderer = false;

                    foreach (Renderer renderer in renderers)
                    {
                        if (renderer.TryGetHit(new IntVector2(x, y), out hitChar))
                        {
                            didHitRenderer = true;
                            rowSb.Append(hitChar);
                            break;
                        }
                        
                    }

                    if (!didHitRenderer)
                    {
                        rowSb.Append(' ');
                    }
                }

                if (y == _viewportSize.Y)
                {
                    frameSb.Append(rowSb.ToString());
                    break;
                }
                frameSb.AppendLine(rowSb.ToString());
            }

            return frameSb.ToString();
        }
    }
}