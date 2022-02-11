// Copyright © 2012-2022 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System;

namespace Vlingo.Xoom.Turbo.Storage;

public class Database
{
	public static DatabaseCategory? From(string name)
	{
		if (name == null)
		{
			return null;
		}

		try
		{
			var database = (DatabaseCategory)Enum.Parse(typeof(DatabaseCategory), name);
			return database;
		}
		catch (Exception e)
		{
			throw new ArgumentException(e.Message, e);
		}
	}
}

public static class DatabaseCategoryExtensions
{
	public static bool IsInMemory(this DatabaseCategory databaseCategory) =>
		databaseCategory.Equals(DatabaseCategory.InMemory);
}

public enum DatabaseCategory
{
	InMemory,
	Postgres,
	Hsqldb,
	Mysql,
	YugaByte
}