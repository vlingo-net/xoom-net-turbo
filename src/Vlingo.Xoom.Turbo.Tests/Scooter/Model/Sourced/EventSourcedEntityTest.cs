// Copyright © 2012-2021 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System.Collections.Generic;
using Vlingo.Xoom.Lattice.Model;
using Vlingo.Xoom.Symbio;
using Xunit;

namespace Vlingo.Xoom.Turbo.Tests.Scooter.Model.Sourced
{
	public class EventSourcedEntityTest
	{
		[Fact]
		public void TestProductDefinedEventKept()
		{
			var product = new Product("dice", "fuz", "dice-fuz-1", "Fuzzy dice.", 999);

			Assert.Equal(3, product.Applied().Size());
			Assert.Equal("dice-fuz-1", product.Name);
			Assert.Equal("Fuzzy dice.", product.Description);
			Assert.Equal(999, product.Price);
			Assert.Equal(new ProductDefined("dice-fuz-1", "Fuzzy dice.", 999), product.Applied().SourceAt(2));
		}

		[Fact]
		public void TestProductNameChangedEventKept()
		{
			var product = new Product("dice", "fuz", "dice-fuz-1", "Fuzzy dice.", 999);

			product.ChangeName("dice-fuzzy-1");

			Assert.Equal(4, product.Applied().Size());
			Assert.Equal("dice-fuzzy-1", product.Name);
			Assert.Equal(new ProductNameChanged("dice-fuzzy-1"), product.Applied().SourceAt(3));
		}

		[Fact]
		public void TestProductDescriptionChangedEventsKept()
		{
			var product = new Product("dice", "fuz", "dice-fuz-1", "Fuzzy dice.", 999);

			product.ChangeDescription("Fuzzy dice, and all.");

			Assert.Equal(4, product.Applied().Size());
			Assert.Equal("Fuzzy dice, and all.", product.Description);
			Assert.Equal(new ProductDescriptionChanged("Fuzzy dice, and all."), product.Applied().SourceAt(3));
		}

		[Fact]
		public void TestProductPriceChangedEventKept()
		{
			var product = new Product("dice", "fuz", "dice-fuz-1", "Fuzzy dice.", 999);

			product.ChangePrice(995);

			Assert.Equal(4, product.Applied().Size());
			Assert.Equal(995, product.Price);
			Assert.Equal(new ProductPriceChanged(995), product.Applied().SourceAt(3));
		}

		[Fact]
		public void TestReconstitution()
		{
			var sources = new List<Source<DomainEvent>>();

			var product = new Product("dice", "fuz", "dice-fuz-1", "Fuzzy dice.", 999);
			sources.AddRange(product.Applied().Sources());
			product.ChangeName("dice-fuzzy-1");
			sources.AddRange(product.Applied().Sources());
			product.ChangeDescription("Fuzzy dice, and all.");
			sources.AddRange(product.Applied().Sources());
			product.ChangePrice(995);
			sources.AddRange(product.Applied().Sources());

			var productAgain = new Product(sources, sources.Count);
			Assert.Equal(product, productAgain);
		}
		[Fact]
		public void TestBaseClassBehavior() {
			var product = new Product("dice", "fuz", "dice-fuz-1", "Fuzzy dice.", 999);
			
			Assert.Equal(new ProductGrandParentTyped("dice"), product.Applied().SourceAt(0));
			Assert.Equal(new ProductParentCategorized("fuz"), product.Applied().SourceAt(1));
			Assert.Equal(new ProductDefined("dice-fuz-1", "Fuzzy dice.", 999), product.Applied().SourceAt(2));
		}
	}
}