using Epicycle.Commons.FileSystem;
using Moq;
using NUnit.Framework;

namespace Epicycle.Commons
{
    [TestFixture]
    public class YamlUtilsTest
    {
        private FileSystemPath _testPath;
        private string _testYaml;
        private TestObject _testObject;

        [SetUp]
        public void SetUp()
        {
            _testPath = new FileSystemPath(@"foo\bar");
            _testYaml = "Foo: Booga\r\nBar: 42\r\n";
            _testObject = new TestObject();
            _testObject.Foo = "Booga";
            _testObject.Bar = 42;
        }

        [Test]
        public void SerializeYaml_test()
        {
            Assert.AreEqual(_testYaml, YamlUtils.Serialize(_testObject));
        }

        [Test]
        public void DeserializeYaml_test()
        {
            AssertTestObject(_testObject, YamlUtils.Deserialize<TestObject>(_testYaml));
        }

        [Test]
        public void ReadYaml()
        {
            var fileSystemMock = SetupFileSystemMock();
            fileSystemMock.Setup(m => m.ReadTextFile(_testPath, null)).Returns(_testYaml);

            AssertTestObject(_testObject, fileSystemMock.Object.ReadYaml<TestObject>(_testPath));
        }

        [Test]
        public void ReadYamlOrDefault_no_file_returns_default()
        {
            var fileSystemMock = SetupFileSystemMock();
            IFileSystemTestUtils.SetupExistance(fileSystemMock, _testPath, IFileSystemTestUtils.PathExistance.DoesntExist);

            AssertDefaultTestObject(fileSystemMock.Object.ReadYamlOrDefault<TestObject>(_testPath));
        }

        [Test]
        public void ReadYamlOrDefault_file_with_no_data_returns_default()
        {
            var fileSystemMock = SetupFileSystemMock();
            IFileSystemTestUtils.SetupExistance(fileSystemMock, _testPath, IFileSystemTestUtils.PathExistance.File);
            fileSystemMock.Setup(m => m.ReadTextFile(_testPath, null)).Returns("");

            AssertDefaultTestObject(fileSystemMock.Object.ReadYamlOrDefault<TestObject>(_testPath));
        }

        [Test]
        public void ReadYaml_file_with_data_is_deserialized()
        {
            var fileSystemMock = SetupFileSystemMock();
            fileSystemMock.Setup(m => m.ReadTextFile(_testPath, null)).Returns(_testYaml);

            AssertTestObject(_testObject, fileSystemMock.Object.ReadYaml<TestObject>(_testPath));
        }

        private void AssertTestObject(TestObject expected, TestObject testObject)
        {
            Assert.That(expected.Foo, Is.EqualTo(testObject.Foo));
            Assert.That(expected.Bar, Is.EqualTo(testObject.Bar));
        }

        private void AssertDefaultTestObject(TestObject testObject)
        {
            AssertTestObject(new TestObject(), testObject);
        }

        [Test]
        public void WriteYaml()
        {
            var fileSystemMock = SetupFileSystemMock();

            fileSystemMock.Setup(m => m.WriteTextFile(_testPath, _testYaml, null, false)).Verifiable();

            fileSystemMock.Object.WriteYaml(_testPath, _testObject);

            fileSystemMock.Verify(m => m.WriteTextFile(_testPath, _testYaml, null, false));
        }

        private Mock<IFileSystem> SetupFileSystemMock()
        {
            var fileSystemMock = IFileSystemTestUtils.CreateMock();

            IFileSystemTestUtils.SetupExistance(fileSystemMock, _testPath, IFileSystemTestUtils.PathExistance.File);

            return fileSystemMock;
        }

        public class TestObject
        {
            public TestObject()
            {
                Foo = "default";
                Bar = 123;
            }

            public string Foo { get; set; }
            public int Bar { get; set; }
        }
    }
}
