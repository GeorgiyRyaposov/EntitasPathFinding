﻿using Grids;
using Inputs;
using Paths;

namespace Game
{
    public sealed class GameSystems : Feature
    {
        public GameSystems(Contexts contexts)
        {
            // Input
            Add(new InputSystem(contexts));
            Add(new CameraInputSystem(contexts));
            
            // Events (Generated)
            Add(new GameEventSystems(contexts));
            
            Add(new FillGridSystem(contexts));
        }
    }
}