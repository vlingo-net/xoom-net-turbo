// Copyright © 2012-2021 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Vlingo.Xoom.Actors;
using Vlingo.Xoom.Common;

namespace Vlingo.Xoom.Turbo.Scooter.Plugin.Mailbox.Blocking
{
	public class BlockingMailbox : IMailbox
	{
		private readonly AtomicBoolean _closed;
		private readonly AtomicBoolean _delivering;
		private readonly ConcurrentQueue<IMessage> _queue;
		private readonly AtomicReference<Stack<List<Type>>> _suspendedOverrides;

		public BlockingMailbox()
		{
			TaskScheduler = null!;
			_closed = new AtomicBoolean(false);
			_delivering = new AtomicBoolean(false);
			_queue = new ConcurrentQueue<IMessage>();
			_suspendedOverrides = new AtomicReference<Stack<List<Type>>>();
		}

		public void Run()
		{
			throw new InvalidOperationException("BlockingMailbox does not support this operation.");
		}

		public void Close() => _closed.Set(true);

		public int ConcurrencyCapacity { get; }

		public void Resume(string name)
		{
			if (_suspendedOverrides.Get()!.Any())
			{
				_suspendedOverrides.Get()!.Pop();
			}

			DeliverAll();
		}

		private bool DeliverAll()
		{
			var any = false;

			while (_queue.Any())
			{
				_queue.TryDequeue(out var queued);
				if (queued != null)
				{
					var actor = queued.Actor;
					if (actor != null)
					{
						any = true;
						actor.ViewTestStateInitialization(null);
						queued.Deliver();
					}
				}
			}

			return any;
		}

		public void Send(IMessage message)
		{
			if (IsClosed) return;

			_queue.Enqueue(message);

			if (IsSuspended)
			{
				return;
			}

			try
			{
				var deliver = true;

				while (deliver)
				{
					if (_delivering.CompareAndSet(false, true))
					{
						while (DeliverAll())
							;
						_delivering.Set(false);
					}

					deliver = false;
				}
			}
			catch (Exception t)
			{
				// should never happen because message
				// delivery is protected by supervision,
				// although it could be a mailbox problem
				if (_delivering.Get())
				{
					_delivering.Set(false);
				}

				throw new Exception(t.Message, t);
			}
		}

		public void SuspendExceptFor(string name, params Type[] overrides) => _suspendedOverrides.Get()!.Push(overrides.ToList());

		public bool IsSuspendedFor(string name)
		{
			throw new NotImplementedException();
		}

		public IMessage Receive()
		{
			throw new InvalidOperationException("BlockingMailbox does not support this operation.");
		}

		public void Send<T>(Actor actor, Action<T> consumer, ICompletes? completes, string representation)
		{
			throw new NotImplementedException();
		}

		public void Send(Actor actor, Type protocol, LambdaExpression consumer, ICompletes? completes, string representation)
		{
			throw new NotImplementedException();
		}

		public bool IsClosed => _closed.Get();
		public bool IsDelivering => _delivering.Get();
		public bool IsSuspended => _suspendedOverrides.Get()!.Any();
		public int PendingMessages => _queue.Count;
		public bool IsPreallocated { get; }
		public TaskScheduler TaskScheduler { get; }
	}
}