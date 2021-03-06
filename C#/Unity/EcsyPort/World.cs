﻿using System;
using System.Collections.Generic;

namespace EcsyPort
{
    public class World
    {
        private ComponentManager componentManager;
        private EntityManager entityManager;
        private SystemManager systemManager;

        public World()
        {
            componentManager = new ComponentManager();
            entityManager = new EntityManager();
            systemManager = new SystemManager();
        }

        public void registerComponent(Component component)
        {
            componentManager.registerComponent(component);
        }

        public bool hasRegisteredComponent(Component component)
        {
            return componentManager.hasComponent(component);
        }

        public void registerSystem(System system)
        {
            systemManager.registerSystem(system);
        }

        public void unregisterSystem(System system)
        {
            systemManager.unregisterSystem(system);
        }

        public System getSystem(Type systemType)
        {
            return systemManager.getSystem(systemType);
        }

        public Dictionary<Type, System> getSystems()
        {
            return systemManager.getSystems();
        }

        public T requestEntity<T>() where T : Entity, new()
        {
            return entityManager.requestEntity<T>();
        }

        public void unregisterEntity(Entity entity)
        {
            entityManager.unregisterEntity(entity);
        }

        public Entity getEntity<T>(int id) where T : Entity
        {
            return entityManager.getEntity<T>(id);
        }

        public void execute(float deltaTime)
        {
            Dictionary<Type, System> systems = systemManager.getSystems();
            Dictionary<Type, EntityPool> entities = entityManager.getEntities();
            foreach (Type systemKey in systems.Keys)
            {
                foreach (Type entityKey in entities.Keys)
                {
                    foreach (Entity entity in entities[entityKey].getActiveEntities())
                    {
                        systems[systemKey].execute(deltaTime, entity);
                    }
                }
            }
        }

        public string stats()
        {
            return string.Format("Stats:{\n{0},\n{1}\n}", entityManager.stats(), systemManager.stats());
        }
    }

}
