// Copyright © 2012-2021 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using Vlingo.Xoom.Lattice.Model;
using Vlingo.Xoom.Turbo.Scooter.Model.Object;

namespace Vlingo.Xoom.Turbo.Tests.Scooter.Model.Object
{
  public class EmployeeEntity : ObjectEntity<EmployeeState, DomainEvent>, IEmployee
  {
    private EmployeeState _employee;

    public EmployeeEntity()
    {
      _employee = new EmployeeState();
    }

    public EmployeeEntity(long id)
    {
      _employee = new EmployeeState(id, "", 0);
    }

    public void Assign(string number)
    {
      Apply(_employee.With(number), new EmployeeNumberAssigned());
    }

    public void Adjust(int salary)
    {
      Apply(_employee.With(salary), new EmployeeSalaryAdjusted());
    }

    public void Hire(string number, int salary)
    {
      Apply(_employee.With(number).With(salary), new EmployeeHired());
    }

    public override string Id()
    {
      throw new System.NotImplementedException();
    }

    protected override void StateObject(EmployeeState stateObject)
    {
      _employee = stateObject;
    }
  }
}