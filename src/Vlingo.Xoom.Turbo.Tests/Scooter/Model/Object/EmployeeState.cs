// Copyright © 2012-2021 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System.Collections.Generic;
using Vlingo.Xoom.Common;
using Vlingo.Xoom.Symbio.Store.Object;

namespace Vlingo.Xoom.Turbo.Tests.Scooter.Model.Object
{
  public class EmployeeState : StateObject, IEqualityComparer<EmployeeState>
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

    public bool Equals(EmployeeState x, EmployeeState y)
    {
      throw new System.NotImplementedException();
    }

    public int GetHashCode(EmployeeState obj)
    {
      throw new System.NotImplementedException();
    }

    public EmployeeState With(string number) => new EmployeeState(PersistenceId, number, Salary);

    public EmployeeState With(int salary) => new EmployeeState(PersistenceId, Number, salary);
  }
}