// Copyright © 2012-2021 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System.Collections.Generic;
using Vlingo.Xoom.Lattice.Model;
using Vlingo.Xoom.Symbio;

namespace Vlingo.Xoom.Turbo.Tests.Scooter.Model.Sourced
{
	public class Product : ProductParent
	{
		private static string _name;
		private static string _description;
		private static long _price;

		public string Name
		{
			get => _name;
			set => _name = value;
		}

		public string Description
		{
			get => _description;
			set => _description = value;
		}

		public long Price
		{
			get => _price;
			set => _price = value;
		}

		public Product(string type, string category, string name, string description, long price) : base(type, category)
		{
			Name = name;
			Description = description;
			Price = price;

			Apply(new ProductDefined(name, description, price));
		}

		public Product(List<Source<DomainEvent>> eventStream, int streamVersion) : base(eventStream, streamVersion)
		{
		}

		public void ChangeName(string newName) => Apply(new ProductNameChanged(newName));

		public void ChangeDescription(string newDescription) => Apply(new ProductDescriptionChanged(newDescription));

		public void ChangePrice(int newPrice) => Apply(new ProductPriceChanged(newPrice));

		public override string Id() => StreamName();

		protected override string StreamName() => null;

		static Product()
		{
			RegisterConsumer<Product, ProductDefined>(delegate(Source<DomainEvent> source)
			{
				WhenProductDefined(source as ProductDefined);
			});
			RegisterConsumer<Product, ProductNameChanged>(delegate(Source<DomainEvent> source)
			{
				WhenProductNameChanged(source as ProductNameChanged);
			});
			RegisterConsumer<Product, ProductDescriptionChanged>(delegate(Source<DomainEvent> source)
			{
				WhenProductDescriptionChanged(source as ProductDescriptionChanged);
			});
			RegisterConsumer<Product, ProductPriceChanged>(delegate(Source<DomainEvent> source)
			{
				WhenProductPriceChanged(source as ProductPriceChanged);
			});
		}

		static void WhenProductDefined(ProductDefined @event)
		{
		}

		static void WhenProductNameChanged(ProductNameChanged @event) => _name = @event.Name;

		static void WhenProductDescriptionChanged(ProductDescriptionChanged @event) => _description = @event.Description;

		static void WhenProductPriceChanged(ProductPriceChanged @event) => _price = @event.Price;

		public override bool Equals(object obj)
		{
			if (obj == null || obj.GetType() != typeof(Product))
			{
				return false;
			}

			var otherProduct = (Product)obj;

			return Name == otherProduct.Name &&
			       Description == otherProduct.Description &&
			       Price == otherProduct.Price;
		}

		protected bool Equals(Product other) => Equals((object)other);

		public override int GetHashCode()
		{
			throw new System.NotImplementedException();
		}
	}
}