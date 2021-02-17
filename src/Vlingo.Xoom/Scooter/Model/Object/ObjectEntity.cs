// Copyright © 2012-2020 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vlingo.Symbio;

namespace Vlingo.Xoom.Scooter.Model.Object
{
    public abstract class ObjectEntity<S, C> : Entity<S, C> where S : class where C : class
    {
        /// <summary>
        /// Answer my unique identity, which much be provided by
        /// my concrete extender by overriding.
        /// </summary>
        /// <returns><see cref="string"/></returns>
        public override abstract string Id();

        /// <summary>
        /// Construct my default state.
        /// </summary>
        protected ObjectEntity() { }

        /// <summary>
        /// Apply <see cref="state"/> and <see cref="sources"/>to myself.
        /// </summary>
        /// <param name="state"> the Object state to preserve</param>
        /// <param name="source"> the <see cref="Source<C>"/> to apply</param>
        protected void Apply(S state, List<Source<C>> sources, Metadata metadata) => Apply(new Applied<S, C>(state, sources, metadata));

        /// <summary>
        /// Apply <see cref="state"/>, <see cref="sources"/>, with <see cref="metadata"/> to myself.
        /// </summary>
        /// <param name="state"> the Object state to preserve</param>
        /// <param name="source"> the <see cref="Source<C>"/> to apply</param>
        /// <param name="metadata"> the Metadata to apply along with source</param>
        protected void Apply(S state, Source<C> source, Metadata metadata) => Apply(new Applied<S, C>(state, new List<Source<C>>() { source }, metadata));

        /// <summary>
        /// Apply <see cref="state"/> to myself.
        /// </summary>
        /// <param name="state"> the Object state to preserve</param>
        protected void Apply(S state) => Apply(new Applied<S, C>(state, new List<Source<C>>(), Metadata()));

        /// <summary>
        /// Apply <see cref="state"/> and <see cref="sources"/> to myself.
        /// </summary>
        /// <param name="state"> the S state to preserve</param>
        /// <param name="sources"> the <see cref="List<Source<C>>"/> to apply</param>
        protected void Apply(S state, List<Source<C>> sources) => Apply(new Applied<S, C>(state, sources, Metadata()));

        /// <summary>
        /// Apply <see cref="state"/> and <see cref="sources"/> to myself.
        /// </summary>
        /// <param name="state"> the T typed state to apply</param>
        /// <param name="source"> source the <see cref="Source<C>"/> instances to apply</param>
        protected void Apply(S state, Source<C> source) => Apply(new Applied<S, C>(state, new List<Source<C>>() { source }, Metadata()));

        /// <summary>
        /// Answer a <see cref="List<Source<C>>"/> from the varargs <see cref="sources"/>.
        /// </summary>
        /// <param name="sources"> the varargs <see cref=" Source<C>"/> of sources to answer as a <see cref="List<Source<C>>"/></param>
        /// // <returns><see cref="List<Source<C>>"/></returns>
        protected List<Source<C>> AsList(params Source<C>[] sources) => sources.ToList();

        /// <summary>
        /// Answer a representation of a number of segments as a
        /// composite id. The implementor of <see cref="Id()"/> would use
        /// this method if the its id is built from segments.
        /// </summary>
        /// <param name="separator"> the String separator the insert between segments</param>
        /// <param name="idSegments"> the varargs String of one or more segments</param>
        /// <returns><see cref="string"/></returns>
        protected string IdFrom(string separator, params string[] idSegments)
        {
            var builder = new StringBuilder();
            builder.Append(idSegments[0]);
            for (int idx = 1; idx < idSegments.Length; ++idx)
            {
                builder.Append(separator).Append(idSegments[idx]);
            }
            return builder.ToString();
        }

        /// <summary>
        /// Answer my <see cref="Metadata"/>. Must override if <see cref="Metadata"/> is to be supported.
        /// </summary>
        /// // <returns><see cref="Metadata"/></returns>
        protected Metadata Metadata() => Symbio.Metadata.NullMetadata();

        /// <summary>
        /// Received by my extender when my state object has been preserved and restored.
        /// Must be overridden by my extender.
        /// </summary>
        /// <param name="stateObject"> the T typed state object</param>
        protected abstract void StateObject(S stateObject);

        /// <summary>
        /// Apply by setting <see cref="Applied()"/> and setting state.
        /// </summary>
        /// <param name="applied"> the <see cref="Applied<S,C>"/> to apply</param>
        private void Apply(Applied<S, C> applied)
        {
            Applied(applied);
            StateObject(applied.state);
        }
    }
}
