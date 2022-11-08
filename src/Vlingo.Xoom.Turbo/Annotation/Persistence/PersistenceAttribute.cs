// Copyright © 2012-2022 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System;
using Vlingo.Xoom.Actors;
using Vlingo.Xoom.Symbio;
using Vlingo.Xoom.Symbio.Store.Journal;
using Vlingo.Xoom.Symbio.Store.Object;
using Vlingo.Xoom.Symbio.Store.State;

namespace Vlingo.Xoom.Turbo.Annotation.Persistence;

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

public enum StorageType
{
	None, StateStore, ObjectStore, Journal
}

public static class StorageTypeExtensions
{
	public static bool IsJournal(this StorageType storageType) => storageType.Equals(StorageType.Journal);
	public static bool IsStateStore(this StorageType storageType) => storageType.Equals(StorageType.StateStore);
	public static bool IsObjectStore(this StorageType storageType) => storageType.Equals(StorageType.ObjectStore);

	public static T? ResolveNoOpStore<T>(this StorageType storageType, Stage stage) where T : class
	{
		var local = stage.World.Stage;
		switch (storageType)
		{
			case StorageType.StateStore:
				return local.ActorFor<IStateStore>(typeof(NoOpStateStoreActor<IState>)) as T;
			case StorageType.ObjectStore:
				return local.ActorFor<IObjectStore>(typeof(NoOpObjectStoreActor<IState>)) as T;
			case StorageType.Journal:
				return local.ActorFor<IJournal>(typeof(NoOpJournalActor<IState>)) as T;
			default:
				throw new InvalidOperationException("Unable to resolve no operation store for " + storageType);
		}
	}
}