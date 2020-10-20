using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace EcsyPort
{
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
        private Dictionary<Type, EntityPool> _entities;

        public EntityManager()
        {
            _entities = new Dictionary<Type, EntityPool>();
        }

        public T requestEntity<T>() where T : Entity, new()
        {
            Type entityType = typeof(T);
            if (!_entities.ContainsKey(entityType))
            {
                _entities.Add(entityType, new EntityPool());
            }
            return _entities[entityType].newEntity<T>();
        }

        public void unregisterEntity<T>(T entity) where T : Entity
        {
            Type entityType = typeof(T);
            if (_entities.ContainsKey(entityType))
            {
                _entities[entityType].removeEntity(entity);
            }
        }

        public void unregisterEntity<T>(int entityId) where T : Entity
        {
            Type entityType = typeof(T);
            if (_entities.ContainsKey(entityType))
            {
                _entities[entityType].removeEntity<T>(entityId);
            }
        }

        public T getEntity<T>(int entityId) where T : Entity
        {
            bool inPool = _entities.ContainsKey(typeof(T));
            if (!inPool)
            {
                return null;
            }
            return _entities[typeof(T)].getEntity<T>(entityId);
        }

        public Dictionary<Type, EntityPool> getEntities()
        {
            return _entities;
        }

        public string stats()
        {
            return "";
        }
    }

    public class EntityPool
    {
        private int entityCount;

        private Dictionary<int, Entity> entitiesInUse;
        private Dictionary<int, Entity> reserved;

        public EntityPool()
        {
            entityCount = 0;
            entitiesInUse = new Dictionary<int, Entity>();
            reserved = new Dictionary<int, Entity>();
        }

        public T newEntity<T>() where T : Entity, new()
        {
            bool inReserve = reserved.Count > 0;
            if (inReserve)
            {
                int key = reserved.Keys.First();
                T e = (T)(reserved[key]);
                entitiesInUse.Add(key, e);
                return e;
            }
            var newEntity = new T();
            newEntity.ID = entityCount;
            entitiesInUse.Add(newEntity.ID, newEntity);
            entityCount++;
            return newEntity;
        }

        public T getEntity<T>(int entityId) where T : Entity
        {
            return (T)(entitiesInUse[entityId]);
        }

        public void removeEntity(Entity entity)
        {
            if (!entitiesInUse.ContainsKey(entity.ID))
            {
                return;
            }
            entitiesInUse.Remove(entity.ID);
            reserved.Add(entity.ID, entity);
        }

        public void removeEntity<T>(int entityId) where T : Entity
        {
            if (!entitiesInUse.ContainsKey(entityId))
            {
                return;
            }
            T entity = (T)(entitiesInUse[entityId]);
            entitiesInUse.Remove(entityId);
            reserved.Add(entity.ID, entity);
        }

        public List<Entity> getActiveEntities()
        {
            return entitiesInUse.Values.ToList<Entity>();
        }

        public List<Entity> getAllEntities()
        {
            var inUse = entitiesInUse.Values.ToList<Entity>();
            var res = reserved.Values.ToList<Entity>();
            inUse.AddRange(res);
            return inUse;
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
}