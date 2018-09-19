﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace KlarupHalBooking.Entities
{
    public static class Serializer<T>
    {
        /// <summary>Serializes an object to a binary byte buffer.</summary>
        /// <param name="obj">The object to serialize. Must be not null.</param>
        /// <returns>A byte buffer containing the binary representation of the serialized object.</returns>
        /// <exception cref="SerializerException">Thrown when an error occurs during serialization.</exception>
        /// <exception cref="ArgumentNullException"/>
        public static byte[] Serialize(T obj)
        {
            byte[] result;
            BinaryFormatter serializer = new BinaryFormatter();
            try
            {
                using (MemoryStream stream = new MemoryStream())
                {
                    serializer.Serialize(stream, obj);
                    result = stream.GetBuffer();
                }
            }
            catch (System.Runtime.Serialization.SerializationException serializationException)
            {
                throw new SerializerException("Attempt to serialize failed. See inner exception for details.", serializationException);
            }
            catch (UnauthorizedAccessException accessException)
            {
                throw new SerializerException("Access denied error. See inner exception for details.", accessException);
            }
            catch (System.Security.SecurityException securityException)
            {
                throw new SerializerException("Security error. See inner exception for details.", securityException);
            }
            catch (ArgumentNullException nullException)
            {
                throw new SerializerException("Security error. See inner exception for details.", nullException);
            }
            catch (Exception e)
            {
                throw new SerializerException("Unexpected error. See inner exception for details.", e);
            }
            return result;
        }

        /// <summary>Deserializes a byte buffer into an object of type <see cref="T"/>.</summary>
        /// <param name="buffer">The byte buffer to deserialize.</param>
        /// <returns>An object of type <see cref="T"/>.</returns>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ArgumentException">Thrown when the length of the buffer is zero (nothing to deserialize).</exception>
        /// <exception cref="SerializerException">Thrown when an error occurs during deserialization.</exception>
        public static T Deserialize(byte[] buffer)
        {
            if (buffer is null)
                throw new ArgumentNullException(nameof(buffer));
            else if (buffer.Length == 0)
                throw new ArgumentException("Input has zero length");
            T result;
            BinaryFormatter deserializer = new BinaryFormatter();
            try
            {
                using (MemoryStream stream = new MemoryStream(buffer))
                {
                    result = (T)deserializer.Deserialize(stream);
                }
            }
            catch (System.Runtime.Serialization.SerializationException serializationException)
            {
                throw new SerializerException("Attempt to serialize failed. See inner exception for details.", serializationException);
            }
            catch (InvalidCastException castException)
            {
                throw new SerializerException("Could not cast byte buffer to an object of type T. See inner exception for details.", castException);
            }
            catch (System.Security.SecurityException securityException)
            {
                throw new SerializerException("Security error. See inner exception for details.", securityException);
            }
            catch (Exception e)
            {
                throw new SerializerException("Unexpected error. See inner exception for details.", e);
            }
            return result;
        }
    }
}
