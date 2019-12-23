using MessagePack.Internal;
using System;
using System.IO;

namespace MessagePack
{
    /// <summary>
    /// High-Level API of MessagePack for C#.
    /// </summary>
    public static partial class MessagePackSerializer
    {
        static IFormatterResolver defaultResolver;

        /// <summary>
        /// Set default resolver of MessagePackSerializer APIs.
        /// </summary>
        /// <param name="resolver"></param>
        public static void SetDefaultResolver(IFormatterResolver resolver)
        {
            defaultResolver = resolver;
        }

        /// <summary>
        /// Serialize to binary with default resolver.
        /// </summary>
        public static byte[] Serialize<T>(T obj)
        {
            return Serialize(obj, defaultResolver);
        }

        /// <summary>
        /// Serialize to binary with specified resolver.
        /// </summary>
        public static byte[] Serialize<T>(T obj, IFormatterResolver resolver)
        {
            if (resolver == null) resolver = defaultResolver;
            var formatter = resolver.GetFormatterWithVerify<T>();

            var buffer = InternalMemoryPool.GetBuffer();

            var len = formatter.Serialize(ref buffer, 0, obj, resolver);

            // do not return MemoryPool.Buffer.
            return MessagePackBinary.FastCloneWithResize(buffer, len);
        }

        /// <summary>
        /// Serialize to binary. Get the raw memory pool byte[]. The result can not share across thread and can not hold, so use quickly.
        /// </summary>
        public static ArraySegment<byte> SerializeUnsafe<T>(T obj)
        {
            return SerializeUnsafe(obj, defaultResolver);
        }

        /// <summary>
        /// Serialize to binary with specified resolver. Get the raw memory pool byte[]. The result can not share across thread and can not hold, so use quickly.
        /// </summary>
        public static ArraySegment<byte> SerializeUnsafe<T>(T obj, IFormatterResolver resolver)
        {
            if (resolver == null) resolver = defaultResolver;
            var formatter = resolver.GetFormatterWithVerify<T>();

            var buffer = InternalMemoryPool.GetBuffer();

            var len = formatter.Serialize(ref buffer, 0, obj, resolver);

            // return raw memory pool, unsafe!
            return new ArraySegment<byte>(buffer, 0, len);
        }

        /// <summary>
        /// Serialize to stream.
        /// </summary>
        public static void Serialize<T>(Stream stream, T obj)
        {
            Serialize(stream, obj, defaultResolver);
        }

        /// <summary>
        /// Serialize to stream with specified resolver.
        /// </summary>
        public static void Serialize<T>(Stream stream, T obj, IFormatterResolver resolver)
        {
            if (resolver == null) resolver = defaultResolver;
            var formatter = resolver.GetFormatterWithVerify<T>();

            var buffer = InternalMemoryPool.GetBuffer();

            var len = formatter.Serialize(ref buffer, 0, obj, resolver);

            // do not need resize.
            stream.Write(buffer, 0, len);
        }

        /// <summary>
        /// Reflect of resolver.GetFormatterWithVerify[T].Serialize.
        /// </summary>
        public static int Serialize<T>(ref byte[] bytes, int offset, T value, IFormatterResolver resolver)
        {
            return resolver.GetFormatterWithVerify<T>().Serialize(ref bytes, offset, value, resolver);
        }

        public static T Deserialize<T>(byte[] bytes)
        {
            return Deserialize<T>(bytes, defaultResolver);
        }

        public static T Deserialize<T>(byte[] bytes, IFormatterResolver resolver)
        {
            if (resolver == null) resolver = defaultResolver;
            var formatter = resolver.GetFormatterWithVerify<T>();

            int readSize;
            return formatter.Deserialize(bytes, 0, resolver, out readSize);
        }

        public static T Deserialize<T>(ArraySegment<byte> bytes)
        {
            return Deserialize<T>(bytes, defaultResolver);
        }

        public static T Deserialize<T>(ArraySegment<byte> bytes, IFormatterResolver resolver)
        {
            if (resolver == null) resolver = defaultResolver;
            var formatter = resolver.GetFormatterWithVerify<T>();

            int readSize;
            return formatter.Deserialize(bytes.Array, bytes.Offset, resolver, out readSize);
        }

        public static T Deserialize<T>(Stream stream)
        {
            return Deserialize<T>(stream, defaultResolver);
        }

        public static T Deserialize<T>(Stream stream, IFormatterResolver resolver)
        {
            return Deserialize<T>(stream, resolver, false);
        }

        public static T Deserialize<T>(Stream stream, bool readStrict)
        {
            return Deserialize<T>(stream, defaultResolver, readStrict);
        }

        public static T Deserialize<T>(Stream stream, IFormatterResolver resolver, bool readStrict)
        {
            if (resolver == null) resolver = defaultResolver;
            var formatter = resolver.GetFormatterWithVerify<T>();

            if (!readStrict)
            {
                var buffer = InternalMemoryPool.GetBuffer();

                FillFromStream(stream, ref buffer);

                int readSize;
                return formatter.Deserialize(buffer, 0, resolver, out readSize);
            }
            else
            {
                int _;
                var bytes = MessagePackBinary.ReadMessageBlockFromStreamUnsafe(stream, false, out _);
                int readSize;
                return formatter.Deserialize(bytes, 0, resolver, out readSize);
            }
        }

        /// <summary>
        /// Reflect of resolver.GetFormatterWithVerify[T].Deserialize.
        /// </summary>
        public static T Deserialize<T>(byte[] bytes, int offset, IFormatterResolver resolver, out int readSize)
        {
            return resolver.GetFormatterWithVerify<T>().Deserialize(bytes, offset, resolver, out readSize);
        }

        static int FillFromStream(Stream input, ref byte[] buffer)
        {
            int length = 0;
            int read;
            while ((read = input.Read(buffer, length, buffer.Length - length)) > 0)
            {
                length += read;
                if (length == buffer.Length)
                {
                    MessagePackBinary.FastResize(ref buffer, length * 2);
                }
            }

            return length;
        }
    }
}

namespace MessagePack.Internal
{
    internal static class InternalMemoryPool
    {
        [ThreadStatic]
        static byte[] buffer = null;

        public static byte[] GetBuffer()
        {
            if (buffer == null)
            {
                buffer = new byte[65536];
            }
            return buffer;
        }
    }
}