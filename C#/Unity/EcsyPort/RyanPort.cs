using System;
using System.Collections;
using System.Collections.Generic;

namespace RyanPort
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

        public void registerEntity(Entity entity)
        {
            entityManager.registerEntity(entity);
        }

        public void unregisterEntity(Entity entity)
        {
            entityManager.unregisterEntity(entity);
        }

        public Entity getEntity(Type entityType, int id){
            return entityManager.getEntity(entityType, id);
        }

        public void execute(float deltaTime)
        {
            Dictionary<Type, System> systems = systemManager.getSystems();
            Dictionary<Type, List<Entity>> entities = entityManager.getEntities();
            foreach (Type systemKey in systems.Keys)
            {
                foreach (Type entityKey in entities.Keys)
                {
                    foreach (Entity entity in entities[entityKey])
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

    public class ComponentManager
    {
        private Dictionary<Type, List<Component>> _components;

        public ComponentManager()
        {
            _components = new Dictionary<Type, List<Component>>();
        }

        public void registerComponent(Component component)
        {
            Type componentType = component.GetType();
            if (!_components.ContainsKey(componentType))
            {
                _components.Add(componentType, new List<Component>());
            }

            if (!_components[componentType].Contains(component))
            {
                _components[componentType].Add(component);
            }
        }

        public bool hasComponent(Component component)
        {
            Type componentType = component.GetType();
            if (!_components.ContainsKey(componentType))
            {
                return false;
            }
            return _components[componentType].Count <= 0;
        }

        public string stats()
        {
            return "";
        }
    }

    public class EntityManager
    {
        private Dictionary<Type, List<Entity>> _entities;

        public EntityManager()
        {
            _entities = new Dictionary<Type, List<Entity>>();
        }

        public void registerEntity(Entity entity)
        {
            Type entityType = entity.GetType();
            if (!_entities.ContainsKey(entityType))
            {
                _entities.Add(entityType, new List<Entity>());
            }
            _entities[entityType].Add(entity);
        }

        public void unregisterEntity(Entity entity)
        {
            Type entityType = entity.GetType();
            if (_entities.ContainsKey(entityType))
            {
                _entities[entityType].Remove(entity);
            }
        }

        // TODO: Wrong use of entityId, find the id in the list of entities and return that entity
        public Entity getEntity(Type entityType, int entityId)
        {
            return _entities[entityType][entityId];
        }

        public Dictionary<Type, List<Entity>> getEntities()
        {
            return _entities;
        }

        public string stats()
        {
            return "";
        }
    }

    public class SystemManager
    {
        private Dictionary<Type, System> _systems;

        public SystemManager()
        {
            _systems = new Dictionary<Type, System>();
        }

        public void registerSystem(System system)
        {
            _systems.Add(system.GetType(), system);
        }

        public void unregisterSystem(System system)
        {
            _systems.Remove(system.GetType());
        }

        public void sortSystems()
        {

        }

        public System getSystem(Type systemClass)
        {
            if (!systemClass.IsSubclassOf(typeof(System)))
            {
                return null;
            }
            if (!_systems.ContainsKey(systemClass))
            {
                return null;
            }
            return _systems[systemClass];
        }

        public Dictionary<Type, System> getSystems()
        {
            return _systems;
        }

        public void execute(float deltaTime, Entity entity, Type systemType = null)
        {
            if (systemType == null)
            {
                foreach (Type key in _systems.Keys)
                {
                    _systems[key].execute(deltaTime, entity);
                }
                return;
            }
            _systems[systemType].execute(deltaTime, entity);
        }

        public string stats()
        {
            return "";
        }
    }

    public abstract class Component
    {
        public int id;
    }

    public abstract class Entity
    {
        public int id;

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

        public Component getComponent(Type componentType){
            return components[componentType];
        }
    }

    public abstract class System
    {
        protected abstract bool componentCheck(Entity entity);
        public abstract void execute(float deltaTime, Entity entity);
    }
}
