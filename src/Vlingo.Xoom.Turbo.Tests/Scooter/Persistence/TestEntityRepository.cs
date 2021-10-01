// Copyright © 2012-2021 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System.Linq;
using Vlingo.Xoom.Lattice.Model;
using Vlingo.Xoom.Symbio;
using Vlingo.Xoom.Symbio.Store.Journal;
using Vlingo.Xoom.Turbo.Scooter.Persistence;

namespace Vlingo.Xoom.Turbo.Tests.Scooter.Persistence
{
	public class TestEntityRepository : JournalRepository
	{
		private readonly EntryAdapterProvider _adapterProvider;
		private readonly IJournal<string> _journal;
		private readonly IStreamReader? _reader;

		public TestEntityRepository(IJournal<string> journal, EntryAdapterProvider adapterProvider)
		{
			_journal = journal;
			_reader = journal.StreamReader("TestRepository").Await();
			_adapterProvider = adapterProvider;
		}

		public TestEntity TestOf(string id)
		{
			var stream = _reader.StreamFor(id).Await();
			var sources = _adapterProvider.AsSources<Source<DomainEvent>, IEntry>(stream.Entries.OfType<IEntry>());
			return new TestEntity(sources, stream.StreamVersion);
		}

		public void Save(TestEntity test)
		{
			var interest = CreateAppendInterest();
			_journal.AppendAll<ISource>(test.Id(), test.NextVersion(), test.Applied().Sources(), interest, null);
			Await(interest);
		}
	}
}