namespace EcsyPort
{
    public abstract class System
    {
        protected abstract bool componentCheck(Entity entity);
        public abstract void execute(float deltaTime, Entity entity);
    }
}
