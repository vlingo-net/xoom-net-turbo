// Copyright © 2012-2020 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Vlingo.Xoom.Common;
using Vlingo.Xoom.Symbio;
using Vlingo.Xoom.Symbio.Store;
using Vlingo.Xoom.Symbio.Store.Journal;

namespace Vlingo.Xoom.Scooter.Model.Persistence
{
    /// <summary>
    /// A base class for all concrete <see cref="Journal"/> repositories. This implementation
    /// blocks and thus must be used only by <see cref="vlingo-scooter"/> services.
    /// </summary>
    public abstract class JournalRepository
    {
        protected JournalRepository() { }

        /// <summary>
        /// Answer an <see cref="JournalRepository.AppendInterest"/> for each new
        /// <see cref="Append()"/> and <see cref="AppendAll()"/>.
        /// </summary>
        AppendInterest CreateAppendInterest() => new AppendInterest();

        /// <summary>
        /// Await on the append to be completed. The <see cref="interest"/> must be
        /// requested upon each new <see cref="Append()"/> and <see cref="AppendAll()"/>.
        /// </summary>
        /// <param name="interest"> the AppendInterest on which the await is based.</param>
        // protected void Await(AppendInterest interest)
        // {
        //     while (!interest.IsAppended())
        //     {
        //         interest.ThrowIfException();
        //     }
        // }

        public class AppendInterest : IAppendResultInterest
        {
            // private AtomicBoolean _appended;
            // private AtomicReference<StorageException> _exception;
            //
            // public void AppendResultedIn<TSource, TSnapshotState>(IOutcome<StorageException, Result> outcome, string streamName, int streamVersion, TSource source, Optional<TSnapshotState> snapshot, object @object) where TSource : Source
            // {
            //     AppendConsidering<TSource, TSnapshotState>(outcome);
            // }
            //
            // public void AppendResultedIn<TSource, TSnapshotState>(IOutcome<StorageException, Result> outcome, string streamName, int streamVersion, TSource source, Metadata metadata, Optional<TSnapshotState> snapshot, object @object) where TSource : Source
            // {
            //     AppendConsidering<TSource, TSnapshotState>(outcome);
            // }
            //
            // public void AppendAllResultedIn<TSource, TSnapshotState>(IOutcome<StorageException, Result> outcome, string streamName, int streamVersion, IEnumerable<TSource> sources, Optional<TSnapshotState> snapshot, object @object) where TSource : Source
            // {
            //     AppendConsidering<TSource, TSnapshotState>(outcome);
            // }
            //
            // public void AppendAllResultedIn<TSource, TSnapshotState>(IOutcome<StorageException, Result> outcome, string streamName, int streamVersion, IEnumerable<TSource> sources, Metadata metadata, Optional<TSnapshotState> snapshot, object @object) where TSource : Source
            // {
            //     AppendConsidering<TSource, TSnapshotState>(outcome);
            // }

            // [MethodImpl(MethodImplOptions.Synchronized)]
            // private void Appended() => _appended.Set(true);
            //
            // [MethodImpl(MethodImplOptions.Synchronized)]
            // public bool IsAppended() => _appended.Get();
            //
            // public AppendInterest()
            // {
            //     _appended = new AtomicBoolean(false);
            //     _exception = new AtomicReference<StorageException>();
            // }

            // private void AppendConsidering<TSource, TSnapshotState>(IOutcome<StorageException, Result> outcome)
            // {
            //     outcome
            //     .AndThen(result =>
            //     {
            //         if (result == Result.Success)
            //         {
            //             Appended();
            //         }
            //         return result;
            //     })
            //     .Otherwise(ex =>
            //     {
            //         _exception.Set(ex);
            //         return outcome.GetOrNull();
            //     });
            // }

            // [MethodImpl(MethodImplOptions.Synchronized)]
            // public void ThrowIfException()
            // {
            //     var t = _exception.Get();
            //     if (t != null)
            //     {
            //         throw new InvalidOperationException(string.Concat("Append failed because: ", t.Message), t);
            //     }
            // }
            public void AppendResultedIn<TSource, TSnapshotState>(IOutcome<StorageException, Result> outcome, string streamName, int streamVersion, TSource source,
                Optional<TSnapshotState> snapshot, object @object) where TSource : ISource
            {
                throw new NotImplementedException();
            }

            public void AppendResultedIn<TSource, TSnapshotState>(IOutcome<StorageException, Result> outcome, string streamName, int streamVersion, TSource source,
                Metadata metadata, Optional<TSnapshotState> snapshot, object @object) where TSource : ISource
            {
                throw new NotImplementedException();
            }

            public void AppendAllResultedIn<TSource, TSnapshotState>(IOutcome<StorageException, Result> outcome, string streamName, int streamVersion,
                IEnumerable<TSource> sources, Optional<TSnapshotState> snapshot, object @object) where TSource : ISource
            {
                throw new NotImplementedException();
            }

            public void AppendAllResultedIn<TSource, TSnapshotState>(IOutcome<StorageException, Result> outcome, string streamName, int streamVersion,
                IEnumerable<TSource> sources, Metadata metadata, Optional<TSnapshotState> snapshot, object @object) where TSource : ISource
            {
                throw new NotImplementedException();
            }
        }
    }
}
