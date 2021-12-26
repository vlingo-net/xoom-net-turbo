// Copyright © 2012-2021 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System;
using Vlingo.Xoom.Turbo.Annotation.Codegen.Storage;

namespace Vlingo.Xoom.Turbo.Annotation.Persistence
{
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
	public class PersistenceAttribute : Attribute
	{
		private readonly StorageType _storageType;

		public PersistenceAttribute(StorageType storageType, string basePackage)
		{
			_storageType = storageType;
			BasePackage = basePackage;
		}

		public string BasePackage { get; set; }

		bool Cqrs() => false;

		public bool IsStateStore() => _storageType == StorageType.StateStore;

		public bool IsJournal() => _storageType == StorageType.Journal;

		public bool IsObjectStore() => _storageType == StorageType.ObjectStore;
	}
}