// Copyright © 2012-2020 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vlingo.Xoom.Symbio;

namespace Vlingo.Xoom.Scooter.Model.Sourced
{
    public abstract class SourcedEntity<T> : Entity<object, T> where T : class
    {
        private static readonly ConcurrentDictionary<Type, IDictionary<Type, Action<SourcedEntity<Type>, Source<Type>>>> _registeredConsumers =
                new ConcurrentDictionary<Type, IDictionary<Type, Action<SourcedEntity<Type>, Source<Type>>>>();

        private readonly int _currentVersion;

        /// <summary>
        /// Register the means to apply <see cref="sourceType"/> instances for state transition
        /// of <see cref="sourcedType"/> by means of a given <see cref="consumer"/>.
        /// </summary>
        /// <param name="sourcedType"> the concrete <see cref="Class<SOURCED>"/> type to which sourceType instances are applied</param>
        /// <param name="sourceType"> the concrete <see cref="Class<SOURCED>"/> type to apply</param>
        /// <param name="consumer"> the <see cref="Action<SOURCED, SOURCE>"/> used to perform the application of sourceType</param>
        /// <param name="SOURCED"> the type <see cref="Type"/> of the sourced entity to apply to</param>
        /// <param name="SOURCE"> the type <see cref="Type"/> of the sourced entity to apply to</param>
        /// <returns><see cref="World"/></returns>
        public static void RegisterConsumer<SOURCED, SOURCE>(Type sourcedType, Type sourceType, Action<SOURCED, SOURCE> consumer) where SOURCED : SourcedEntity<Type> where SOURCE : Source<Type>
        {
            IDictionary<Type, Action<SourcedEntity<Type>, Source<Type>>> sourcedTypeMap;
            if (_registeredConsumers.TryGetValue(sourcedType, out sourcedTypeMap))
            {
                sourcedTypeMap = new Dictionary<Type, Action<SourcedEntity<Type>, Source<Type>>>();
                _registeredConsumers.TryAdd(sourcedType, sourcedTypeMap);
            }

            sourcedTypeMap.Add(sourceType, (Action<SourcedEntity<Type>, Source<Type>>)consumer);
        }

        public int CurrentVersion() => _currentVersion;

        /// <summary>
        /// Answer my type name.
        /// </summary>
        /// <returns><see cref="string<S,C>"/></returns>
        public string Type() => GetType().Name;

        /// <summary>
        /// Construct my default state.
        /// </summary>
        protected SourcedEntity() => _currentVersion = 0;

        /// <summary>
        /// Construct my default state.
        /// </summary>
        /// <param name="stream"> the <see cref="List<Source<T>>"/> with which to initialize my state</param>
        /// <param name="currentVersion"> the int to set as my currentVersion</param>
        protected SourcedEntity(List<Source<T>> stream, int currentVersion)
        {
            _currentVersion = currentVersion;
            TransitionWith(stream);
        }

        /// <summary>
        /// Apply all of the given <see cref="sources"/> to myself, which includes appending
        /// them to my journal and reflecting the representative changes to my state.
        /// </summary>
        /// <param name="sources"> the <see cref="List<Source<T>>"/> to apply</param>
        protected void Apply(List<Source<T>> sources) => Apply(sources, Metadata());

        /// <summary>
        /// Apply all of the given <see cref="sources"/> to myself along with <see cref="metadata"/>
        /// </summary>
        /// <param name="sources"> the <see cref="List<Source<T>>"/> to apply</param>
        /// <param name="metadata"> the Metadata to apply along with source</param>
        protected void Apply(List<Source<T>> sources, Metadata metadata) => ApplyWithTransition(new Applied<object, T>(Snapshot<T>(), NextVersion(), sources, metadata));

        /// <summary>
        /// Apply the given  <see cref="source"/> to myself.
        /// </summary>
        /// <param name="source"> the <see cref="Source<T>"/> to apply</param>
        protected void Apply(Source<T> source) => Apply(source, Metadata());

        /// <summary>
        /// Apply the given <see cref="source"/> to myself with <see cref="metadata"/>.
        /// </summary>
        /// <param name="source"> the <see cref="Source<T>"/> to apply</param>
        /// <param name="metadata"> the Metadata to apply along with source</param>
        protected void Apply(Source<T> source, Metadata metadata) => ApplyWithTransition(new Applied<object, T>(Snapshot<T>(), NextVersion(), Wrap(source), metadata));

        /// <summary>
        /// Answer a <see cref="List<Source<T>>"/> from the varargs <see cref="sources"/>.
        /// </summary>
        /// <param name="sources"> the varargs <see cref="Source<T>"/> of sources to answer as a <see cref="List<Source<T>>"/></param>
        /// <returns><see cref="List{Source{T}}"/></returns>
        protected List<Source<T>> AsList(params Source<T>[] sources) => sources.ToList();

        /// <summary>
        /// Answer my <see cref="Metadata"/>.
        /// Must override if <see cref="Metadata"/> is to be supported.
        /// </summary>
        /// <returns><see cref="Metadata"/></returns>
        protected Metadata Metadata() => Symbio.Metadata.NullMetadata();

        /// <summary>
        /// Answer a valid <see cref="SNAPSHOT"/> state instance if a snapshot should
        /// be taken and persisted along with applied <see cref="Source{T}"/> instance(s).
        /// Must override if snapshots are to be supported.
        /// </summary>
        /// <param name="<SNAPSHOT>">the type of the snapshot</param>
        /// <returns><see cref="SNAPSHOT"/></returns>
        protected SNAPSHOT Snapshot<SNAPSHOT>() where SNAPSHOT : class => null;

        /// <summary>
        /// Answer my stream name. Must override.
        /// </summary>
        /// <returns><see cref="string"/></returns>
        protected abstract string StreamName();

        /// <summary>
        /// Answer a representation of a number of segments as a
        /// composite stream name. The implementor of <see cref="StreamName()"/>
        /// would use this method if the its stream name is built from segments.
        /// </summary>
        /// <param name="separator">the String separator the insert between segments</param>
        /// <param name="streamNameSegments">the varargs String of one or more segments</param>
        /// <returns><see cref="string"/></returns>
        protected string StreamNameFrom(string separator, params string[] streamNameSegments)
        {
            var builder = new StringBuilder();
            builder.Append(streamNameSegments[0]);
            for (int idx = 1; idx < streamNameSegments.Length; ++idx)
            {
                builder.Append(separator).Append(streamNameSegments[idx]);
            }
            return builder.ToString();
        }

        private void ApplyWithTransition(Applied<object, T> applied)
        {
            Applied(applied);
            TransitionWith(applied.Sources());
        }

        private void TransitionWith(List<Source<T>> stream)
        {
            foreach (var source in stream)
            {
                Type type = GetType();
                Action<SourcedEntity<Type>, Source<Type>> consumer = null;
                while (type != typeof(SourcedEntity<Type>))
                {
                    var sourcedTypeMap = _registeredConsumers[type];
                    if (sourcedTypeMap != null)
                    {
                        consumer = sourcedTypeMap[source.GetType()];
                        if (consumer != null)
                        {
                            //consumer(this, source);
                            break;
                        }
                    }
                    type = type.BaseType;
                }
                if (consumer == null)
                {
                    throw new InvalidOperationException("No such Sourced type.");
                }
            }
        }

        /// <summary>
        /// Answer <see cref="source"/> wrapped in a <see cref="Source{T}"/>.
        /// </summary>
        /// <param name="source">the <see cref="Source{T}"/> to wrap</param>
        /// <returns><see cref="List{Source{T}}"/></returns>
        private List<Source<T>> Wrap<T>(Source<T> source) => new List<Source<T>>() { source };

        /// <summary>
        /// Answer <see cref="sources"/> wrapped in a <see cref="List{Source{T}}"/>.
        /// </summary>
        /// <param name="sources">the <see cref="Source{T}[]"/> to wrap</param>
        /// <returns><see cref="List{Source{T}}"/></returns>
        private List<Source<T>> Wrap<T>(Source<T>[] sources) => sources.ToList();
    }
}
