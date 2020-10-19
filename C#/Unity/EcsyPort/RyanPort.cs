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
        private Dictionary<Type, EntityPool<Entity>> _entities;

        public EntityManager()
        {
            _entities = new Dictionary<Type, EntityPool<Entity>>();
        }

        public void registerEntity<T>(T entity) where T : Entity
        {
            Type entityType = typeof(T);
            if (!_entities.ContainsKey(entityType))
            {
                // TODO: Figure out how to create Entity Pool of type T instead
                _entities.Add(entityType, new EntityPool<Entity>());
            }
            _entities[entityType].newEntity(entity);
        }

        public void requestEntity<T>(T entity) where T : Entity, new()
        {
            Type entityType = typeof(T);
            if (!_entities.ContainsKey(entityType))
            {
                // TODO: Figure out how to create Entity Pool of type T instead
                _entities.Add(entityType, new EntityPool<Entity>());
            }
            _entities[entityType].newEntity(entity);
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
                _entities[entityType].removeEntity(entityId);
            }
        }

        // TODO: Wrong use of entityId, find the id in the list of entities and return that entity
        public Entity getEntity(Type entityType, int entityId)
        {
            return _entities[entityType].getEntity(entityId);
        }

        public T getEntity<T>(int entityId) where T : Entity
        {
            bool inPool = _entities.ContainsKey(typeof(T));
            if (!inPool)
            {
                return null;
            }
            return (T) (_entities[typeof(T)].getEntity(entityId));
        }

        public Dictionary<Type, EntityPool<Entity>> getEntities()
        {
            return _entities;
        }

        public string stats()
        {
            return "";
        }
    }

    public class EntityPool<T> where T : Entity, new()
    {
        private int entityCount;

        private Dictionary<int, T> entitiesInUse;
        private Dictionary<int, T> reserved;

        public EntityPool()
        {
            entityCount = 0;
            entitiesInUse = new Dictionary<int, T>();
            reserved = new Dictionary<int, T>();
        }

        public T newEntity()
        {
            bool inReserve = reserved.Count > 0;
            if (inReserve)
            {
                int key = reserved.Keys.First();
                T e = reserved[key];
                entitiesInUse.Add(key, e);
                return e;
            }
            var newEntity = new T();
            newEntity.ID = entityCount;
            entityCount++;
            return newEntity;
        }

        public T newEntity(T entity)
        {
            bool inUse = entitiesInUse.ContainsValue(entity);
            bool inReserve = reserved.ContainsValue(entity);
            if (inUse || inReserve)
            {
                return entity;
            }
            entity.ID = entityCount;
            entityCount++;
            return entity;
        }

        public T getEntity(int entityId)
        {
            return entitiesInUse[entityId];
        }

        public void removeEntity(T entity)
        {
            if (!entitiesInUse.ContainsKey(entity.ID))
            {
                return;
            }
            entitiesInUse.Remove(entity.ID);
            reserved.Add(entity.ID, entity);
        }

        public void removeEntity(int entityId)
        {
            if (!entitiesInUse.ContainsKey(entityId))
            {
                return;
            }
            T entity = entitiesInUse[entityId];
            entitiesInUse.Remove(entityId);
            reserved.Add(entity.ID, entity);
        }

        public List<T> getActiveEntities()
        {
            return entitiesInUse.Values.ToList<T>();
        }

        public List<T> getAllEntities()
        {
            var inUse = entitiesInUse.Values.ToList<T>();
            var res = reserved.Values.ToList<T>();
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