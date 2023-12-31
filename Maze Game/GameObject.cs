﻿using Maze_Game.GameWorlds;
using Maze_Game.Math;

namespace Maze_Game
{
    public class GameObject
    {
        private GameWorld _gameWorld;
        private IntVector2 _position;
        private List<Component> _components;

        public IntVector2 Position => _position;

        public GameObject(GameWorld gameWorld, IntVector2 position)
        {
            _gameWorld = gameWorld;
            _components = new List<Component>();
            _position = position;
        }

        public T? GetComponent<T>() where T : Component
        {
            foreach (var component in _components)
            {
                if (component is T)
                {
                    return (T)component;
                }
            }

            return default(T);
        }

        public bool TryGetComponent<T>(out T? component) where T : Component
        {
            foreach (var componentToCompare in _components)
            {
                if (componentToCompare is T)
                {
                    component = (T)componentToCompare;
                    return true;
                }
            }

            component = null;
            return false;
        }

        public T AddComponent<T>(T component) where T : Component
        {
            _components.Add(component);
            return component;
        }

        public void Move(IntVector2 deltaMovement)
        {
            IntVector2 newPosition = _position + deltaMovement;
            SetPosition(newPosition);
        }

        public void SetPosition(IntVector2 position)
        {
            _position = position;
        }

        public void Create()
        {
            _gameWorld.Create(this);
        }

        public void Destroy()
        {
            _gameWorld.Destroy(this);
        }

        public void Start()
        {
            foreach (var component in _components)
            {
                component.Start();
            }
        }

        public void Update()
        {
            foreach (var component in _components)
            {
                component.Update();
            }
        }

        public void PhysicsUpdate()
        {
            foreach (var component in _components)
            {
                component.PhysicsUpdate();
            }
        }
    }
}