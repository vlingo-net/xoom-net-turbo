// Copyright © 2012-2021 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System;
using Vlingo.Xoom.Turbo.Annotation;

namespace Vlingo.Xoom.Annotation.AutoDispatch
{
    public class HandlerInvocation
    {
        private static readonly string _defaultHandlerInvocationPattern = "{0}.Handler.Handle";
        private static readonly string parametrizedHandlerInvocationPattern = _defaultHandlerInvocationPattern + "({0})";

        public readonly int index;
        public readonly string invocation;
        private readonly TypeReader _handlersConfigReader;

        public HandlerInvocation(TypeReader handlersConfigReader, Type handlerEntry)
        {
            _handlersConfigReader = handlersConfigReader;
            this.index = FindIndex(handlerEntry);
            this.invocation = ResolveInvocation(handlerEntry);
        }

        private int FindIndex(Type handlerEntry)
        {
            string handlerEntryValue = _handlersConfigReader.FindMemberValue(handlerEntry);

            string handlerEntryIndex = handlerEntryValue.Substring(handlerEntryValue.IndexOf("(") + 1, handlerEntryValue.IndexOf(","));
            try
            {
                return int.Parse(handlerEntryIndex);
            }
            catch (FormatException)
            {
                return int.Parse(_handlersConfigReader.FindMemberValue(handlerEntryIndex));
            }
        }

        private string ResolveInvocation(Type handlerEntry)
        {
            string handlerEntryValue = _handlersConfigReader.FindMemberValue(handlerEntry);

            if (handlerEntryValue.Contains("->"))
            {
                string handlerInvocationArguments = ExtractHandlerArguments(handlerEntryValue);
                return string.Format(parametrizedHandlerInvocationPattern,
                        handlerEntry.FullName, handlerInvocationArguments);
            }

            return string.Format(_defaultHandlerInvocationPattern, handlerEntry.FullName);
        }

        private string ExtractHandlerArguments(string handlerEntryValue) => handlerEntryValue.Substring(handlerEntryValue.IndexOf(",") + 1, handlerEntryValue.IndexOf("->")).Replace("\\(", "").Replace("\\)", "").Trim();

        public bool HasCustomParamNames() => invocation.Contains("(") && invocation.Contains(")");
    }
}
