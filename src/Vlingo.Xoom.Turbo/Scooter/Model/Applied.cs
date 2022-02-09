// Copyright © 2012-2022 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System;
using System.Collections.Generic;
using System.Linq;
using Vlingo.Xoom.Symbio;

namespace Vlingo.Xoom.Turbo.Scooter.Model
{
	public class Applied<TS, TC> where TS : class where TC : class
	{
		public readonly Metadata Metadata;
		public readonly TS State;
		public readonly int StateVersion;

		private List<Source<TC>> _sources;

		/// <summary>
		/// Construct my state.
		/// </summary>
		/// <param name="state"> the S state of the entity</param>
		/// <param name="stateVersion"> the int version of the entity state</param>
		/// <param name="sources"> sources the <see cref="List<Source<C>>"/> of DomainEvent or Command instances</param>
		/// <param name="metadata"> the Metadata associated with this state and stateVersion</param>
		public Applied(TS state, int stateVersion, List<Source<TC>> sources, Metadata metadata)
		{
			_sources = sources;
			Metadata = metadata;
			State = state;
			StateVersion = stateVersion;
		}

		public Applied(TS state, List<Source<TC>> sources, Metadata metadata) : this(state, 1, sources, metadata)
		{
		}

		public Applied(int stateVersion, List<Source<TC>> sources, Metadata metadata) : this(null!, stateVersion, sources, metadata)
		{
		}

		public Applied(List<Source<TC>> sources, Metadata metadata) : this(null!, 1, sources, metadata)
		{
		}

		public Applied(int stateVersion, List<Source<TC>> sources) : this(null!, stateVersion, sources, Metadata.NullMetadata())
		{
		}

		public Applied(List<Source<TC>> sources) : this(null!, 1, sources, Metadata.NullMetadata())
		{
		}

		public Applied() : this(null!, 0, new List<Source<TC>>(), Metadata.NullMetadata())
		{
		}

		public Applied<TS, TC> AlongWith(List<Source<TC>> sources) => AlongWith(State, sources, Metadata);

		public Applied<TS, TC> AlongWith(int stateVersion, List<Source<TC>> sources) =>
			AlongWith(State, stateVersion, sources, Metadata);

		public Applied<TS, TC> AlongWith(TS state, int stateVersion, List<Source<TC>> sources) =>
			AlongWith(state, stateVersion, sources, Metadata);

		public Applied<TS, TC> AlongWith(TS state, List<Source<TC>> sources) => AlongWith(state, sources, Metadata);

		public Applied<TS, TC> AlongWith(TS state, List<Source<TC>> sources, Metadata metadata)
		{
			var all = _sources.ToList();
			all.AddRange(sources);
			return new Applied<TS, TC>(state, StateVersion, all, metadata);
		}

		public Applied<TS, TC> AlongWith(TS state, int stateVersion, List<Source<TC>> sources, Metadata metadata)
		{
			var all = _sources.ToList();
			all.AddRange(sources);
			return new Applied<TS, TC>(state, stateVersion, all, metadata);
		}

		public int Size() => _sources.Count;

		public List<Source<TC>> Sources() => _sources;

		public List<Source<TC>> SourcesForTest()
		{
			_sources = new List<Source<TC>>();
			return _sources;
		}

		public Source<TC> SourceAt(int index) => _sources[index];

		public Type SourceTypeAt(int index) => _sources[index].GetType();

		public Type StateType() => State.GetType();

		public string StateTypeName() => State.GetType().FullName!;

		public string StateTypeSimpleName() => State.GetType().Name;
	}
}