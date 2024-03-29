﻿// Copyright © 2012-2023 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

namespace Vlingo.Xoom.Turbo.Scooter.Model;

public abstract class Entity<TS, TC> where TS : class where TC : class
{
    private Applied<TS, TC>? _applied;
        
    public Applied<TS, TC>? Applied() => _applied;

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

    protected void Applied(Applied<TS, TC> applied)
    {
        if (_applied == null)
        {
            _applied = applied;
        }
        else if (applied.State != null)
        {
            if (applied.Metadata.IsEmpty)
            {
                _applied = _applied.AlongWith(applied.State, applied.Sources(), _applied.Metadata);
            }
            else
            {
                _applied = _applied.AlongWith(applied.State, applied.Sources(), applied.Metadata);
            }
        }
        else if (applied.State == null)
        {
            if (applied.Metadata.IsEmpty)
            {
                _applied = _applied.AlongWith(applied.State!, applied.Sources(), _applied.Metadata);
            }
            else
            {
                _applied = _applied.AlongWith(applied.State!, applied.Sources(), applied.Metadata);
            }
        }
    }
}