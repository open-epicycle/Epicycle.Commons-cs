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

using Moq;
using NUnit.Framework;
using System.Threading;

namespace Epicycle.Commons.Reporting
{
    [TestFixture]
    public class ReportingStopwatchTest
    {
        private ReportingStopwatch _reportingStopwatch;

        private Mock<INumericReport> _reportMock;
        private string _name;

        [SetUp]
        public void SetUp()
        {
            _name = "foo";

            _reportMock = new Mock<INumericReport>(MockBehavior.Strict);
            _reportMock.Setup(m => m.Report(_name, It.IsAny<double>())).Verifiable();
        }

        [Test]
        public void test_timing()
        {
            _reportingStopwatch = new ReportingStopwatch(_reportMock.Object, _name);

            Thread.Sleep(100);

            _reportingStopwatch.Dispose();

            _reportMock.Verify(m => m.Report(_name, It.Is<double>(time => (time > 0.09 && time < 1.0))));
        }
    }
}
