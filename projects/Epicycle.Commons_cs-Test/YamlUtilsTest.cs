// [[[[INFO>
// Copyright 2015 Epicycle (http://epicycle.org, https://github.com/open-epicycle)
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// 
// For more information check https://github.com/open-epicycle/Epicycle.Commons-cs
// ]]]]

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
