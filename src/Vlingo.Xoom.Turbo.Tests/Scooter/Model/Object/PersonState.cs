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
  public class PersonState : StateObject, IComparable<PersonState>
  {
    private static readonly AtomicLong _identityGenerator = new AtomicLong(0);

    public readonly string Name;
    public readonly int Age;

    public PersonState(long id, string name, int age) : base(id)
    {
      Name = name;
      Age = age;
    }

    public PersonState()
    {
      Name = "";
      Age = 0;
    }

    public PersonState(string name, int age) : base(_identityGenerator.IncrementAndGet())
    {
      Name = name;
      Age = age;
    }

    public PersonState With(string name) => new PersonState(PersistenceId, name, Age);

    public PersonState With(int age) => new PersonState(PersistenceId, Name, age);
    public int GetHashCode(PersonState obj) => 31 * Name.GetHashCode() * Age;

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

      var otherPerson = (PersonState) other;

      return PersistenceId == otherPerson.PersistenceId;
    }

    public int CompareTo(PersonState otherPerson) => PersistenceId.CompareTo(otherPerson.PersistenceId);
  }
}