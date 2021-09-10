// Copyright © 2012-2021 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System;
using Vlingo.Xoom.Lattice.Model;
using Vlingo.Xoom.Turbo.Scooter.Model.Sourced;
using Xunit;

namespace Vlingo.Xoom.Turbo.Tests.Scooter.Model.Sourced
{
  public class EventSourcedEntityTest
  {
    [Fact(Skip = "WIP")]
    public void TestProductDefinedEventKept()
    {
      var product = new Product("dice", "fuz", "dice-fuz-1", "Fuzzy dice", 999);

      Assert.Equal(3, product.Applied().Size());
    }

    public class Product : ProductParent
    {
      public string Name { get; set; }
      public string Description { get; set; }
      public long Price { get; set; }

      public Product(string type, string category, string name, string description, long price) : base(type, category)
      {
        Name = name;
        Description = description;
        Price = price;

        Apply(new ProductDefined(name, description, price));
      }

      public class ProductDefined : DomainEvent
      {
        public string Name { get; }
        public string Description { get; }
        public long Price { get; }
        public DateTime OccurredOn { get; }
        public int Version { get; }

        public ProductDefined(string name, string description, long price)
        {
          Name = name;
          Description = description;
          Price = price;
          OccurredOn = DateTime.Now;
          Version = 1;
        }
      }

      public override string Id()
      {
        throw new System.NotImplementedException();
      }

      protected override string StreamName()
      {
        throw new System.NotImplementedException();
      }
    }

    public abstract class ProductParent : ProductGrandParent
    {
      public string Category { get; set; }

      protected ProductParent(string type, string category) : base(type)
      {
        Category = category;

        Apply(new ProductParentCategorized(category));
      }

      public class ProductParentCategorized : DomainEvent
      {
        public string Category { get; }
        public DateTime OccurredOn { get; }
        public int Version { get; }

        public ProductParentCategorized(string category)
        {
          Category = category;
          OccurredOn = DateTime.Now;
          Version = 1;
        }
      }
    }

    public abstract class ProductGrandParent : EventSourcedEntity
    {
      public string Type { get; set; }

      protected ProductGrandParent(string type)
      {
        Type = type;

        Apply(new ProductGrandParentTyped(type));
      }

      public class ProductGrandParentTyped : DomainEvent
      {
        public string Type { get; }
        public DateTime OccurredOn { get; }
        public int Version { get; }

        public ProductGrandParentTyped(string type)
        {
          Type = type;
          OccurredOn = DateTime.Now;
          Version = 1;
        }
      }
    }
  }
}