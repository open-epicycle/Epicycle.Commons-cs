using Epicycle.Commons.FileSystem;
using System.IO;
using YamlDotNet.Serialization;

// Authors: untrots

namespace Epicycle.Commons
{
    /// <summary>
    /// This class contains various utilities for serializing and deserializing YAML data. All the YAML functionality is based on the YamlDotNet lib.
    /// </summary>
    public static class YamlUtils
    {
        /// <summary>
        /// Serializes an object into a YAML string. For more details see the YamlDotNet lib.
        /// </summary>
        /// <typeparam name="Type">The type of the serialized object</typeparam>
        /// <param name="objectToSerialize">The object to serialize</param>
        /// <returns>The serialized object. Contains Windows-style line endidng.</returns>
        public static string Serialize<Type>(Type objectToSerialize)
        {
            ArgAssert.NotNull(objectToSerialize, "objectToSerialize");

            var stringWriter = new StringWriter();
            var serializer = new Serializer();
            serializer.Serialize(stringWriter, objectToSerialize);

            return stringWriter.ToString();
        }

        /// <summary>
        /// Deserializes an object from a YAML string. For more details see the YamlDotNet lib.
        /// </summary>
        /// <typeparam name="Type">The type of the serialized object</typeparam>
        /// <param name="yamlData">The YAML data to deserialize</param>
        /// <returns>The deserialized object</returns>
        public static Type Deserialize<Type>(string yamlData)
        {
            ArgAssert.NotNull(yamlData, "yamlData");

            var stringReader = new StringReader(yamlData);

            var deserializer = new Deserializer();
            return deserializer.Deserialize<Type>(stringReader);
        }

        /// <summary>
        /// An extension to IFileSystem types that reads and deserializes YAML data from a file. The default encoding (UTF-8) is used.
        /// </summary>
        /// <typeparam name="Type">The type of the serialized object</typeparam>
        /// <param name="path">The path to the YAML file. The file must exist.</param>
        /// <returns>The deserialized object</returns>
        /// <exception cref="FileSystemPathDoesNotExistException">Thrown if the path does not exists.</exception>
        /// <exception cref="FileExpectedException">Thrown if the path does not point to a file.</exception>
        public static Type ReadYaml<Type>(this IFileSystem fileSystem, FileSystemPath path)
        {
            return Deserialize<Type>(fileSystem.ReadTextFile(path));
        }

        /// <summary>
        /// An extension to IFileSystem types that reads and deserializes YAML data from a file. The default encoding (UTF-8) is used.
        /// If the file does not exist or does not contain the object then the object is constructed using the default constructor.
        /// </summary>
        /// <typeparam name="Type">The type of the serialized object</typeparam>
        /// <param name="path">The path to the YAML file. The file may not exist.</param>
        /// <returns>The deserialized object or the constructed object (in case of no data). Never null.</returns>
        /// <exception cref="FileExpectedException">Thrown if the path does not point to a file.</exception>
        public static Type ReadYamlOrDefault<Type>(this IFileSystem fileSystem, FileSystemPath path) where Type : new()
        {
            if (!fileSystem.Exists(path))
            {
                return new Type();
            }

            var result = fileSystem.ReadYaml<Type>(path);

            return (result != null) ? result : new Type();
        }

        /// <summary>
        /// An extension to IFileSystem types that serializes an object and writes it to a file. The default encoding (UTF-8) is used.
        /// </summary>
        /// <param name="path">The path to the file to write. If exists, it must point to a file.</param>
        /// <param name="objectToSerialize">The object to serialize</param>
        /// <exception cref="FileExpectedException">Thrown if the path does not point to a file.</exception>
        public static void WriteYaml<Type>(this IFileSystem fileSystem, FileSystemPath path, Type objectToSerialize)
        {
            fileSystem.WriteTextFile(path, Serialize(objectToSerialize));
        }
    }
}
