using System;
using System.Collections.Generic;

namespace EcsyPort
{
    public abstract class Entity
    {
        private int id;

        protected Dictionary<Type, Component> components;

        public bool hasComponent(Type componentType)
        {
            return components.ContainsKey(componentType);
        }

        public bool hasComponents(params Type[] componentTypes)
        {
            foreach (Type componentType in componentTypes)
            {
                if (!components.ContainsKey(componentType))
                {
                    return false;
                }
            }
            return true;
        }

        public Component getComponent(Type componentType)
        {
            return components[componentType];
        }

        public int ID
        {
            get { return id; }
            set { id = value; }
        }
    }
}