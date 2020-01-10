using System.Collections.Generic;

namespace LinkGo.Base.Pool
{
	/// <summary>
	/// This class allows you to pool normal C# classes, 
	/// for example:
	///		ObjectPool<Foo> objPool = new ObjectPool<Foo>(4);
	///		var foo = objPool.Spawn();
	///		objPool.Despawn(foo);
	/// </summary>
	public sealed class ObjectPool<T> where T : class
	{
		private Stack<T> m_objectStack;

		public ObjectPool(int capacity)
		{
			m_objectStack = new Stack<T>(capacity);
		}

		// This will either return a pooled class instance.
		public T New()
		{
			if (m_objectStack.Count > 0)
			{
				T t = m_objectStack.Pop();
				return t;
			}
			else
			{
				return System.Activator.CreateInstance<T>();
			}
		}

		/// <summary>
		/// This will either return a pooled class instance. If an instance it found, onSpawn will be called with it.
		/// </summary>
		public T New(System.Action<T> onSpawn)
		{
			var instance = New();

			onSpawn?.Invoke(instance);

			return instance;
		}

		/// <summary>
		/// This will pool the passed class instance.
		/// </summary>
		public void Store(T instance)
		{
			if (instance != null)
			{
				m_objectStack.Push(instance);
			}
		}

		/// <summary>This will pool the passed class instance.
		/// If you need to perform despawning code then you can do that via onDespawn.</summary>
		public void Store(T instance, System.Action<T> onDespawn)
		{
			if (instance != null)
			{
				onDespawn(instance);

				Store(instance);
			}
		}
	}
}