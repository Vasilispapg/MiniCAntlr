
namespace MiniC
{
    public abstract class CNodeType<T>
    {
        private T m_nodeType;

        public T MNodeType => m_nodeType;

        protected CNodeType(T nodeType)
        {
            m_nodeType = nodeType;
        }

        public abstract T Map(int type);
        public abstract T Map(T type);
        public abstract T NA();
        public abstract T Default();

    }
}
