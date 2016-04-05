namespace ConsoleApplication1
{
    public interface IProcessor
    {
        IProcessor<TEngine> Create<TEngine>();
    }

    public interface IProcessor<TEngine, TEntity>
    {
        Processor<TEngine, TEntity, TLogger> With<TLogger>();
    }

    public interface IProcessor<TEngine>
    {
        IProcessor<TEngine, TEntity> For<TEntity>();
    }

    public class Processor : IProcessor
    {
        public IProcessor<TEngine> Create<TEngine>()
        {
            return new Processor<TEngine>();
        }

        public static IProcessor<TEngine> CreateEngine<TEngine>()
        {
            return new Processor().Create<TEngine>();
        }
    }

    public class Processor<TEngine> : IProcessor<TEngine>
    {
        public IProcessor<TEngine, TEntity> For<TEntity>()
        {
            return new Processor<TEngine, TEntity>();
        }
    }

    public class Processor<TEngine, TEntity> : IProcessor<TEngine, TEntity>
    {
        public Processor<TEngine, TEntity, TLogger> With<TLogger>()
        {
            return new Processor<TEngine, TEntity, TLogger>();
        }
    }

    public class Processor<TEngine, TEntity, TLogger> { }
}