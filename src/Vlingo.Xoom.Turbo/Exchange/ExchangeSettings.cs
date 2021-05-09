//// Copyright © 2012-2021 VLINGO LABS. All rights reserved.
////
//// This Source Code Form is subject to the terms of the
//// Mozilla Public License, v. 2.0. If a copy of the MPL
//// was not distributed with this file, You can obtain
//// one at https://mozilla.org/MPL/2.0/.

using System;
using System.Collections.Generic;
using System.Linq;

namespace Vlingo.Xoom.Turbo.Exchange
{
    public class ExchangeSettings
    {
        private static readonly string _exchangeNames = "exchange.names";
        private static readonly List<ExchangeSettings> _allExchangeParameters = new List<ExchangeSettings>();
        private static readonly List<string> _propertiesKeys = new List<string>()
        {
            "exchange.%s.hostname",
            "exchange.%s.username",
            "exchange.%s.password",
            "exchange.%s.port",
            "exchange.%s.virtual.host"
        };
        private readonly string _exchangeName;
        private readonly List<string> _keys;
        private readonly List<ExchangeSettingsItem> _parameters;

        public static List<ExchangeSettings> All() => _allExchangeParameters;

        public static ExchangeSettings Of(string exchangeName) => _allExchangeParameters.FirstOrDefault(param => param.HasName(exchangeName)) ?? throw new ArgumentException(string.Concat("Exchange with name ", exchangeName, " not found"));

        public List<ExchangeSettings> Load(IReadOnlyDictionary<string,string> properties)
        {
            if (_allExchangeParameters != null && _allExchangeParameters.Count == 0)
            {
                var exchangeParameters = ApplicationProperty.ReadMultipleValues(_exchangeNames, ";", properties).Select(x => new ExchangeSettings(_exchangeName, properties));
                _allExchangeParameters.AddRange(exchangeParameters);
            }
            return _allExchangeParameters ?? new List<ExchangeSettings>();
        }

        private ExchangeSettings(string exchangeName, IReadOnlyDictionary<string, string> properties)
        {
            _exchangeName = exchangeName;
            _keys = PrepareKeys(exchangeName);
            _parameters = RetrieveParameters(properties);
            Validate();
        }

        private List<string> PrepareKeys(string exchangeName) => _propertiesKeys.Select(key => string.Format(key, exchangeName)).ToList();

        private void Validate()
        {
            var parametersNotFound = _parameters.Where(param => param.value == null).Select(param => param.key).ToList();
            if (parametersNotFound != null && parametersNotFound.Count != 0)
            {
                throw new ExchangeSettingsNotFoundException(parametersNotFound);
            }
        }

        private List<ExchangeSettingsItem> RetrieveParameters(IReadOnlyDictionary<string, string> properties) => _keys.Select(key => new ExchangeSettingsItem(key, ApplicationProperty.ReadValue(key, properties) ?? string.Empty)).ToList();

        private bool HasName(string exchangeName) => _exchangeName == exchangeName;

        public List<string> MapToConnection()
        {
            return new List<string>()
            {
                RetrieveParameterValue("hostname"),
                RetrieveParameterValue("port"),
                RetrieveParameterValue("virtual.host"),
                RetrieveParameterValue("username"),
                RetrieveParameterValue("password"),

            };
        }

        private string RetrieveParameterValue(string key) => RetrieveParameterValue(key, value => value);

        private T RetrieveParameterValue<T>(string keySuffix, Func<string, T> converter)
        {
            var key = ResolveKey(keySuffix);
            var value = _parameters.Where(param => param.HasKey(key)).Select(param => param.value).First();
            return converter(value);
        }

        private string ResolveKey(string keySuffix) => string.Format("exchange.%s.%s", _exchangeName, keySuffix);
    }
}
