// Copyright © 2012-2021 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System;
using Vlingo.Xoom.Common;
using Vlingo.Xoom.Symbio.Store.Object;

namespace Vlingo.Xoom.Turbo.Tests.Scooter.Model.Object
{
  public class EmployeeState : StateObject, IComparable<EmployeeState>
  {
    private static readonly AtomicLong _identityGenerator = new AtomicLong(0);

    public string Number { get; }
    public int Salary { get; }

    public EmployeeState() : base(_identityGenerator.IncrementAndGet())
    {
      Number = "";
      Salary = 0;
    }

    public EmployeeState(long id, string number, int salary) : base(id)
    {
      Number = number;
      Salary = salary;
    }

    public EmployeeState With(string number) => new EmployeeState(PersistenceId, number, Salary);

    public EmployeeState With(int salary) => new EmployeeState(PersistenceId, Number, salary);

    public int GetHashCode(EmployeeState obj) => 31 * Number.GetHashCode() * Salary;

    public override bool Equals(object? other)
    {
      if (other == null || other.GetType() != GetType())
      {
        return false;
      }

      if (this == other)
      {
        return true;
      }

      var otherEmployee = (EmployeeState) other;

      return PersistenceId == otherEmployee.PersistenceId;
    }

    public int CompareTo(EmployeeState otherEmployee) => PersistenceId.CompareTo(otherEmployee.PersistenceId);
  }
}