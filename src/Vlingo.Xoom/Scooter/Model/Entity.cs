// Copyright © 2012-2020 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

namespace Vlingo.Xoom.Scooter.Model
{
    public abstract class Entity<S, C> where S : class where C : class
    {
        private Applied<S, C> _applied;

        /// <summary>
        /// Answer my <see cref="applied"/>.
        /// </summary>
        /// <returns><see cref="Applied<S,C>"/></returns>
        public Applied<S, C> Applied() => _applied;

        /// <summary>
        /// Answer my <see cref="currentVersion"/>, which, if zero, indicates that the
        /// receiver is being initially constructed or reconstituted.
        /// </summary>
        /// <returns><see cref="int"/></returns>
        public virtual int CurrentVersion() => 0;

        /// <summary>
        /// Answer my <see cref="nextVersion"/>, which is <see cref="currentVersion() + 1"/>.
        /// </summary>
        /// <returns><see cref="int"/></returns>
        public int NextVersion() => CurrentVersion() + 1;

        /// <summary>
        /// Answer my unique identity, which much be provided by
        /// my concrete extender by overriding.
        /// </summary>
        /// <returns><see cref="string"/></returns>
        public abstract string Id();

        protected Entity()
        {
        }

        protected void Applied(Applied<S, C> applied)
        {
            if (_applied == null)
            {
                _applied = applied;
            }
            else if (applied.state != null)
            {
                if (applied.metadata.IsEmpty)
                {
                    _applied = _applied.AlongWith(applied.state, applied.Sources(), _applied.metadata);
                }
                else
                {
                    _applied = _applied.AlongWith(applied.state, applied.Sources(), applied.metadata);
                }
            }
            else if (applied.state == null)
            {
                if (applied.metadata.IsEmpty)
                {
                    _applied = _applied.AlongWith(applied.state, applied.Sources(), _applied.metadata);
                }
                else
                {
                    _applied = _applied.AlongWith(applied.state, applied.Sources(), applied.metadata);
                }
            }
        }
    }
}
