// Copyright © 2012-2021 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System;
using System.Collections.Generic;
using System.Linq;
using Vlingo.Xoom.Annotation.AutoDispatch;

namespace Vlingo.Xoom.Turbo.Annotation.AutoDispatch
{
	public class HandlerResolver
	{
		private static string _handlerEntryClassName = nameof(HandlerEntry);

		private readonly TypeReader _handlersConfigReader;
		private readonly List<HandlerInvocation> _handlerInvocations = new List<HandlerInvocation>();

		public static HandlerResolver With(Type handlersConfig, ProcessingEnvironment environment) =>
			new HandlerResolver(handlersConfig, environment);

		private HandlerResolver(Type handlersConfig, ProcessingEnvironment environment)
		{
			_handlersConfigReader = TypeReader.From(environment, handlersConfig);
			_handlerInvocations.AddRange(ResolveInvocations());
		}

		public HandlerInvocation Find(int index) =>
			_handlerInvocations.FirstOrDefault(invocation => invocation.index == index) ??
			throw new ArgumentException(string.Concat("Handler Invocation with index ", index, " not found"));

		private List<HandlerInvocation> ResolveInvocations() => _handlersConfigReader.FindMembers()
			.Where(element => element.DeclaringType.Name.StartsWith(_handlerEntryClassName))
			.Select(handlerEntry => new HandlerInvocation(_handlersConfigReader, handlerEntry as Type))
			.ToList();
	}
}