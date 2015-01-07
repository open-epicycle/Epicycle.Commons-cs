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
using Epicycle.Commons.TestUtils.FileSystem;
using Moq;
using NUnit.Framework;

namespace Epicycle.Commons.FileSystemBasedObjects
{
    [TestFixture]
    public class DirectoryBasedObjectWithConfigurationTest
    {
        private Mock<IFileSystem> _mockFileSystem;
        private string _configurationName;
        private FileSystemPath _path;
        private FileSystemPath _configurationPath;

        [SetUp]
        public void SetUp()
        {
            _mockFileSystem = IFileSystemTestUtils.CreateMock();
            _configurationName = "config.yaml";
            _path = new FileSystemPath(@"foo\bar");
            _configurationPath = _path.Join(_configurationName);
        }

        #region IsPathToSuchObject

        [Test]
        public void IsPathToSuchObject_not_existing_path_returns_false()
        {
            SetupExistance(IFileSystemTestUtils.PathExistance.Directory);
            AssertIsPathToSuchObject(false);
        }

        [Test]
        public void IsPathToSuchObject_path_to_file_returns_false()
        {
            SetupExistance(IFileSystemTestUtils.PathExistance.File);
            AssertIsPathToSuchObject(false);
        }

        [Test]
        public void IsPathToSuchObject_path_to_directory_config_path_doesnt_exist_returns_false()
        {
            SetupExistance(IFileSystemTestUtils.PathExistance.Directory);
            AssertIsPathToSuchObject(false);
        }

        [Test]
        public void IsPathToSuchObject_path_to_directory_config_path_to_file_returns_true()
        {
            SetupExistance(IFileSystemTestUtils.PathExistance.Directory, IFileSystemTestUtils.PathExistance.File);
            AssertIsPathToSuchObject(true);
        }

        [Test]
        public void IsPathToSuchObject_path_to_directory_config_path_to_directory_returns_false()
        {
            SetupExistance(IFileSystemTestUtils.PathExistance.Directory, IFileSystemTestUtils.PathExistance.Directory);
            AssertIsPathToSuchObject(false);
        }

        private void AssertIsPathToSuchObject(bool expect)
        {
            Assert.AreEqual(expect, TestDirectoryBasedObjectWithConfiguration.PublicIsPathToSuchObject(_mockFileSystem.Object, _path, _configurationName));
        }

        #endregion

        #region Construction (no autoinit)

        [Test]
        [ExpectedException(typeof(FileSystemPathDoesNotExistException))]
        public void Ctor_no_autoinit_path_doesnt_exist_throws_FileSystemPathDoesNotExistException()
        {
            SetupExistance(IFileSystemTestUtils.PathExistance.DoesntExist);
            CreatTestObject(false);
        }
        
        [Test]
        [ExpectedException(typeof(DirectoryExpectedException))]
        public void Ctor_no_autoinit_path_to_file_throws_DirectoryExpectedException()
        {
            SetupExistance(IFileSystemTestUtils.PathExistance.File);
            CreatTestObject(false);
        }

        [Test]
        [ExpectedException(typeof(FileSystemPathDoesNotExistException))]
        public void Ctor_no_autoinit_path_to_directory_config_path_doesnt_exist_throws_FileSystemPathDoesNotExistException()
        {
            SetupExistance(IFileSystemTestUtils.PathExistance.Directory);
            CreatTestObject(false);
        }

        [Test]
        public void Ctor_no_autoinit_path_to_directory_config_path_points_to_file_with_configuration_reads_configuration()
        {
            SetupConfig("{Foo: Bar}");
            Assert.That(CreatTestObject(false).GetConfiguration().Foo, Is.EqualTo("Bar"));
        }

        [Test]
        [ExpectedException(typeof(FileExpectedException))]
        public void Ctor_no_autoinit_path_to_directory_config_path_points_to_directory_throws_FileExpectedException()
        {
            SetupExistance(IFileSystemTestUtils.PathExistance.Directory, IFileSystemTestUtils.PathExistance.Directory);
            CreatTestObject(false);
        }

        #endregion

        #region #region Construction (autoinit)

        [Test]
        public void Ctor_autoinit_path_doesnt_exist_inits_the_object()
        {
            TestAutoinit(false);
        }

        [Test]
        [ExpectedException(typeof(DirectoryExpectedException))]
        public void Ctor_autoinit_path_to_file_throws_DirectoryExpectedException()
        {
            SetupExistance(IFileSystemTestUtils.PathExistance.File);
            CreatTestObject(true);
        }

        [Test]
        public void Ctor_autoinit_path_to_directory_config_path_doesnt_exist_inits_the_object()
        {
            TestAutoinit(true);
        }

        [Test]
        public void Ctor_autoinit_path_to_directory_config_path_points_to_file_with_configuration_reads_configuration()
        {
            SetupConfig("{Foo: Bar}");
            Assert.That(CreatTestObject(false).GetConfiguration().Foo, Is.EqualTo("Bar"));
        }

        [Test]
        [ExpectedException(typeof(FileExpectedException))]
        public void Ctor_autoinit_path_to_directory_config_path_points_to_directory_throws_FileExpectedException()
        {
            SetupExistance(IFileSystemTestUtils.PathExistance.Directory, IFileSystemTestUtils.PathExistance.Directory);
            CreatTestObject(true);
        }

        private void TestAutoinit(bool directoryExists)
        {
            var expectedData = "Foo: Moo\r\n";

            SetupExistance(directoryExists ? IFileSystemTestUtils.PathExistance.Directory : IFileSystemTestUtils.PathExistance.DoesntExist);
            
            if (!directoryExists)
            {
                _mockFileSystem.Setup(m => m.CreateDirectoryRecursively(_path)).Verifiable();
            }

            IFileSystemTestUtils.SetupWritableFile(_mockFileSystem, _configurationPath, expectedData);

            CreatTestObject(true);

            if(!directoryExists)
            {
                _mockFileSystem.Verify(m => m.CreateDirectoryRecursively(_path));
            }

            IFileSystemTestUtils.AssertFileWritten(_mockFileSystem, _configurationPath, expectedData);
        }

        #endregion

        #region ConfigurationPath

        [Test]
        public void ConfigurationPath_points_to_the_right_file()
        {
            var testObject = SetupObject();

            Assert.That(testObject.ConfigurationPath.PathString, Is.EqualTo(_configurationPath.PathString));
        }

        #endregion

        #region Save

        [Test]
        public void Save_no_force_not_dirty_does_nothing()
        {
            var testObject = SetupObject();

            ValidateSave(false, false);
        }

        [Test]
        public void Save_no_force_dirty_updates_config()
        {
            ValidateSave(false, true);
        }

        [Test]
        public void Save_force_not_dirty_updates_config()
        {
            var testObject = SetupObject();

            ValidateSave(true, false);
        }

        [Test]
        public void Save_force_dirty_updates_config()
        {
            ValidateSave(true, true);
        }

        private void ValidateSave(bool forced, bool dirty)
        {
            var expectWrite = forced || dirty;
            var expectedData = "Foo: Booga\r\n";

            var testObject = SetupObject();

            if (expectWrite)
            {
                IFileSystemTestUtils.SetupWritableFile(_mockFileSystem, _configurationPath, expectedData);
            }

            testObject.GetConfiguration().Foo = "Booga";
            if (dirty)
            {
                testObject.PublicConfigurationDirty();
            }
            testObject.PublicSave(forced);

            if (expectWrite)
            {
                IFileSystemTestUtils.AssertFileWritten(_mockFileSystem, _configurationPath, expectedData);
            }
        }

        #endregion

        private void SetupExistance(IFileSystemTestUtils.PathExistance pathExistance, IFileSystemTestUtils.PathExistance configFileExistance=IFileSystemTestUtils.PathExistance.DoesntExist)
        {
            IFileSystemTestUtils.SetupExistance(_mockFileSystem, _path, pathExistance);
            IFileSystemTestUtils.SetupExistance(_mockFileSystem, _configurationPath, configFileExistance);
        }

        private void SetupConfig(string configData)
        {
            SetupExistance(IFileSystemTestUtils.PathExistance.Directory, IFileSystemTestUtils.PathExistance.File);
            IFileSystemTestUtils.SetupTextFile(_mockFileSystem, _configurationPath, configData);
        }

        private TestDirectoryBasedObjectWithConfiguration SetupObject()
        {
            SetupExistance(IFileSystemTestUtils.PathExistance.Directory);
            SetupConfig("Foo: Bar");
            return CreatTestObject(false);
        }

        private TestDirectoryBasedObjectWithConfiguration CreatTestObject(bool autoInit)
        {
            return new TestDirectoryBasedObjectWithConfiguration(_mockFileSystem.Object, _path, _configurationName, autoInit);
        }

        private class TestDirectoryBasedObjectWithConfiguration : DirectoryBasedObjectWithConfiguration<TestConfiguration>
        {
            public static bool PublicIsPathToSuchObject(IFileSystem fileSystem, FileSystemPath path, string configurationFileName)
            {
                return IsPathToSuchObject(fileSystem, path, configurationFileName);
            }

            public TestDirectoryBasedObjectWithConfiguration(IFileSystem fileSystem, FileSystemPath path, string configurationName, bool autoInit)
                : base(fileSystem, path, configurationName, autoInit) { }

            public void PublicConfigurationDirty()
            {
                ConfigurationDirty();
            }

            public void PublicSave(bool force)
            {
                Save(force);
            }

            public TestConfiguration GetConfiguration()
            {
                return Configuration;
            }
        }

        private class TestConfiguration
        {
            public TestConfiguration()
            {
                Foo = "Moo";
            }

            public string Foo { get; set; }
        }
    }
}
