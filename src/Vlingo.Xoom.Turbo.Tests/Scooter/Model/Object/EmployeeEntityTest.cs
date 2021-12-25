// Copyright © 2012-2021 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using Xunit;

namespace Vlingo.Xoom.Turbo.Tests.Scooter.Model.Object
{
    public class EmployeeEntityTest
    {
        [Fact]
        public void TestThatEmployeeIdentifiesModifiesRecovers()
        {
            var employee = new EmployeeEntity();

            employee.Hire("12345", 50000);

            var state1 = employee.Applied().state;
            Assert.True(state1.PersistenceId > 0);
            Assert.Equal("12345", state1.Number);
            Assert.Equal(50000, state1.Salary);

            employee.Assign("67890");

            var state2 = employee.Applied().state;
            Assert.Equal(state1.PersistenceId, state2.PersistenceId);
            Assert.Equal("67890", state2.Number);
            Assert.Equal(50000, state2.Salary);

            employee.Adjust(55000);

            var state3 = employee.Applied().state;
            Assert.Equal(state1.PersistenceId, state3.PersistenceId);
            Assert.Equal("67890", state3.Number);
            Assert.Equal(55000, state3.Salary);
        }
    }
}