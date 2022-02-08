// Copyright © 2012-2021 VLINGO LABS. All rights reserved.
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
	public class Applied<S, C> where S : class where C : class
	{
		public readonly Metadata metadata;
		public readonly S state;
		public readonly int stateVersion;

		private List<Source<C>> _sources;

		/// <summary>
		/// Construct my state.
		/// </summary>
		/// <param name="state"> the S state of the entity</param>
		/// <param name="stateVersion"> the int version of the entity state</param>
		/// <param name="sources"> sources the <see cref="List<Source<C>>"/> of DomainEvent or Command instances</param>
		/// <param name="metadata"> the Metadata associated with this state and stateVersion</param>
		public Applied(S state, int stateVersion, List<Source<C>> sources, Metadata metadata)
		{
			_sources = sources;
			this.metadata = metadata;
			this.state = state;
			this.stateVersion = stateVersion;
		}

		public Applied(S state, List<Source<C>> sources, Metadata metadata) : this(state, 1, sources, metadata)
		{
		}

		public Applied(int stateVersion, List<Source<C>> sources, Metadata metadata) : this(null!, stateVersion, sources,
			metadata)
		{
		}

		public Applied(List<Source<C>> sources, Metadata metadata) : this(null!, 1, sources, metadata)
		{
		}

		public Applied(int stateVersion, List<Source<C>> sources) : this(null!, stateVersion, sources,
			Metadata.NullMetadata())
		{
		}

		public Applied(List<Source<C>> sources) : this(null!, 1, sources, Metadata.NullMetadata())
		{
		}

		public Applied() : this(null!, 0, new List<Source<C>>(), Metadata.NullMetadata())
		{
		}

		public Applied<S, C> AlongWith(List<Source<C>> sources) => AlongWith(state, sources, metadata);

		public Applied<S, C> AlongWith(int stateVersion, List<Source<C>> sources) =>
			AlongWith(state, stateVersion, sources, metadata);

		public Applied<S, C> AlongWith(S state, int stateVersion, List<Source<C>> sources) =>
			AlongWith(state, stateVersion, sources, metadata);

		public Applied<S, C> AlongWith(S state, List<Source<C>> sources) => AlongWith(state, sources, metadata);

		public Applied<S, C> AlongWith(S state, List<Source<C>> sources, Metadata metadata)
		{
			var all = _sources.ToList();
			all.AddRange(sources);
			return new Applied<S, C>(state, stateVersion, all, metadata);
		}

		public Applied<S, C> AlongWith(S state, int stateVersion, List<Source<C>> sources, Metadata metadata)
		{
			var all = _sources.ToList();
			all.AddRange(sources);
			return new Applied<S, C>(state, stateVersion, all, metadata);
		}

		public int Size() => _sources.Count;

		public List<Source<C>> Sources() => _sources;

		public List<Source<C>> SourcesForTest()
		{
			_sources = new List<Source<C>>();
			return _sources;
		}

		public Source<C> SourceAt(int index) => _sources[index];

		public Type SourceTypeAt(int index) => _sources[index].GetType();

		public Type StateType() => state.GetType();

		public string StateTypeName() => state.GetType().FullName!;

		public string StateTypeSimpleName() => state.GetType().Name;
	}
}