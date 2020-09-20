using System;
using System.Collections;
using System.Collections.Generic;
using RyanPort;

namespace TestPort
{
    public class MovementSystem : RyanPort.System
    {
        protected override bool componentCheck(Entity entity)
        {
            return entity.hasComponent(typeof(PositionComponent));
        }

        public override void execute(float deltaTime, Entity entity)
        {
            if (componentCheck(entity))
            {
                PositionComponent position = (PositionComponent) entity.getComponent(typeof(PositionComponent));
                position.x += deltaTime;
            }
        }
    }
    public class RotationSystem : RyanPort.System
    {
        protected override bool componentCheck(Entity entity)
        {
            return entity.hasComponent(typeof(RotationComponent));
        }

        public override void execute(float deltaTime, Entity entity)
        {
            if (componentCheck(entity))
            {
                RotationComponent rotation = (RotationComponent) entity.getComponent(typeof(RotationComponent));
                rotation.x += 10 * deltaTime;
                rotation.y += 5 * deltaTime;
            }
        }
    }

    public class CubeEntity : RyanPort.Entity
    {
        public CubeEntity(){
            components = new Dictionary<Type, Component>();
            components.Add(typeof(PositionComponent), new PositionComponent());
            components.Add(typeof(RotationComponent), new RotationComponent());
        }

        public PositionComponent Position{
            get{return (PositionComponent) components[typeof(PositionComponent)];}
        }

        public RotationComponent Rotation{
            get{return (RotationComponent) components[typeof(RotationComponent)];}
        }
    }

    public class PositionComponent : RyanPort.Component
    {
        public float x;
        public float y;
        public float z;

        public PositionComponent(float x = 0, float y = 0, float z = 0)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
    }

    public class RotationComponent : RyanPort.Component
    {
        public float x;
        public float y;
        public float z;

        public RotationComponent(float x = 0, float y = 0, float z = 0)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
    }
}
