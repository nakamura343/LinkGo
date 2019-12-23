using System.Collections.Generic;

namespace MessagePack.Formatters
{
    public abstract class DictionaryFormatterBase<TKey, TValue, TIntermediate, TDictionary> : IMessagePackFormatter<TDictionary>
        where TDictionary : IDictionary<TKey, TValue>
    {
        public int Serialize(ref byte[] bytes, int offset, TDictionary value, IFormatterResolver formatterResolver)
        {
            if (value == null)
            {
                return MessagePackBinary.WriteNil(ref bytes, offset);
            }
            else
            {
                var startOffset = offset;
                var keyFormatter = formatterResolver.GetFormatterWithVerify<TKey>();
                var valueFormatter = formatterResolver.GetFormatterWithVerify<TValue>();

                var count = value.Count;

                offset += MessagePackBinary.WriteMapHeader(ref bytes, offset, count);

                var e = value.GetEnumerator();
                try
                {
                    while (e.MoveNext())
                    {
                        var item = e.Current;
                        offset += keyFormatter.Serialize(ref bytes, offset, item.Key, formatterResolver);
                        offset += valueFormatter.Serialize(ref bytes, offset, item.Value, formatterResolver);
                    }
                }
                finally
                {
                    e.Dispose();
                }

                return offset - startOffset;
            }
        }

        public TDictionary Deserialize(byte[] bytes, int offset, IFormatterResolver formatterResolver, out int readSize)
        {
            if (MessagePackBinary.IsNil(bytes, offset))
            {
                readSize = 1;
                return default(TDictionary);
            }
            else
            {
                var startOffset = offset;
                var keyFormatter = formatterResolver.GetFormatterWithVerify<TKey>();
                var valueFormatter = formatterResolver.GetFormatterWithVerify<TValue>();

                var len = MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
                offset += readSize;

                var dict = Create(len);
                for (int i = 0; i < len; i++)
                {
                    var key = keyFormatter.Deserialize(bytes, offset, formatterResolver, out readSize);
                    offset += readSize;

                    var value = valueFormatter.Deserialize(bytes, offset, formatterResolver, out readSize);
                    offset += readSize;

                    Add(dict, i, key, value);
                }
                readSize = offset - startOffset;

                return Complete(dict);
            }
        }

        // abstraction for deserialize
        protected abstract TIntermediate Create(int count);
        protected abstract void Add(TIntermediate collection, int index, TKey key, TValue value);
        protected abstract TDictionary Complete(TIntermediate intermediateCollection);
    }

    public sealed class DictionaryFormatter<TKey, TValue> : DictionaryFormatterBase<TKey, TValue, Dictionary<TKey, TValue>, Dictionary<TKey, TValue>>
    {
        protected override void Add(Dictionary<TKey, TValue> collection, int index, TKey key, TValue value)
        {
            collection.Add(key, value);
        }

        protected override Dictionary<TKey, TValue> Complete(Dictionary<TKey, TValue> intermediateCollection)
        {
            return intermediateCollection;
        }

        protected override Dictionary<TKey, TValue> Create(int count)
        {
            return new Dictionary<TKey, TValue>(count);
        }
    }

    public sealed class GenericDictionaryFormatter<TKey, TValue, TDictionary> : DictionaryFormatterBase<TKey, TValue, TDictionary, TDictionary>
        where TDictionary : IDictionary<TKey, TValue>, new()
    {
        protected override void Add(TDictionary collection, int index, TKey key, TValue value)
        {
            collection.Add(key, value);
        }

        protected override TDictionary Complete(TDictionary intermediateCollection)
        {
            return intermediateCollection;
        }

        protected override TDictionary Create(int count)
        {
            return new TDictionary();
        }
    }

    public sealed class InterfaceDictionaryFormatter<TKey, TValue> : DictionaryFormatterBase<TKey, TValue, Dictionary<TKey, TValue>, IDictionary<TKey, TValue>>
    {
        protected override void Add(Dictionary<TKey, TValue> collection, int index, TKey key, TValue value)
        {
            collection.Add(key, value);
        }

        protected override Dictionary<TKey, TValue> Create(int count)
        {
            return new Dictionary<TKey, TValue>(count);
        }

        protected override IDictionary<TKey, TValue> Complete(Dictionary<TKey, TValue> intermediateCollection)
        {
            return intermediateCollection;
        }
    }

    public sealed class SortedListFormatter<TKey, TValue> : DictionaryFormatterBase<TKey, TValue, SortedList<TKey, TValue>,  SortedList<TKey, TValue>>
    {
        protected override void Add(SortedList<TKey, TValue> collection, int index, TKey key, TValue value)
        {
            collection.Add(key, value);
        }

        protected override SortedList<TKey, TValue> Complete(SortedList<TKey, TValue> intermediateCollection)
        {
            return intermediateCollection;
        }

        protected override SortedList<TKey, TValue> Create(int count)
        {
            return new SortedList<TKey, TValue>(count);
        }
    }

    public sealed class SortedDictionaryFormatter<TKey, TValue> : DictionaryFormatterBase<TKey, TValue, SortedDictionary<TKey, TValue>, SortedDictionary<TKey, TValue>>
    {
        protected override void Add(SortedDictionary<TKey, TValue> collection, int index, TKey key, TValue value)
        {
            collection.Add(key, value);
        }

        protected override SortedDictionary<TKey, TValue> Complete(SortedDictionary<TKey, TValue> intermediateCollection)
        {
            return intermediateCollection;
        }

        protected override SortedDictionary<TKey, TValue> Create(int count)
        {
            return new SortedDictionary<TKey, TValue>();
        }
    }
}