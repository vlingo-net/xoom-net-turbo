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
  public class PersonEntity : ObjectEntity<PersonState, DomainEvent>, IPerson
  {
    private PersonState _person;

    public PersonEntity()
    {
      _person = new PersonState();
    }

    public PersonEntity(int id)
    {
      _person = new PersonState(id, "", 0);
    }

    public void Identify(string name, int age)
    {
      Apply(new PersonState(name, age));
    }

    public void Change(string name)
    {
      Apply(_person.With(name));
    }

    public void IncreaseAge()
    {
      Apply(_person.With(_person.Age + 1));
    }

    public override string Id() => _person.PersistenceId.ToString();

    protected override void StateObject(PersonState stateObject)
    {
      _person = stateObject;
    }
  }
}