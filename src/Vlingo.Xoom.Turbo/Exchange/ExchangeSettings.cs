// Copyright © 2012-2022 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System;
using System.Collections.Generic;
using System.Linq;
using Vlingo.Xoom.Lattice.Exchange;

namespace Vlingo.Xoom.Turbo.Exchange;

public class ExchangeSettings
{
    private static readonly string ExchangeNames = "exchange.names";
    private static readonly List<ExchangeSettings> AllExchangeParameters = new List<ExchangeSettings>();

    private static readonly List<string> PropertiesKeys = new List<string>()
    {
        "exchange.{0}.hostname",
        "exchange.{0}.username",
        "exchange.{0}.password",
        "exchange.{0}.port",
        "exchange.{0}.virtual.host"
    };

    private readonly string _exchangeName;
    private readonly List<string> _keys;
    private readonly List<ExchangeSettingsItem> _parameters;

    public static List<ExchangeSettings> All() => AllExchangeParameters;

    public static ExchangeSettings Of(string exchangeName) =>
        AllExchangeParameters.FirstOrDefault(param => param.HasName(exchangeName)) ??
        throw new ArgumentException(string.Concat("Exchange with name ", exchangeName, " not found"));

    public static List<ExchangeSettings> Load(IReadOnlyDictionary<string, string> properties)
    {
        if (!AllExchangeParameters.Any())
        {
            Func<string, ExchangeSettings> mapper = exchangeNames =>
                new ExchangeSettings(exchangeNames, properties);

            var exchangeParameters = ApplicationProperty
                .ReadMultipleValues(ExchangeNames, ";", properties).Select(mapper);
            AllExchangeParameters.AddRange(exchangeParameters);
        }

        return AllExchangeParameters ?? new List<ExchangeSettings>();
    }

    private ExchangeSettings(string exchangeName, IReadOnlyDictionary<string, string> properties)
    {
        _exchangeName = exchangeName;
        _keys = PrepareKeys(exchangeName);
        _parameters = RetrieveParameters(properties);

        Validate();
    }

    private List<string> PrepareKeys(string exchangeName) =>
        PropertiesKeys.Select(key => string.Format(key, exchangeName)).ToList();

    private void Validate()
    {
        var parametersNotFound = _parameters
            .Where(param => param.Value == null)
            .Select(param => param.Key)
            .ToList();
        if (parametersNotFound != null && parametersNotFound.Any())
        {
            throw new ExchangeSettingsNotFoundException(parametersNotFound);
        }
    }

    private List<ExchangeSettingsItem> RetrieveParameters(IReadOnlyDictionary<string, string> properties)
        => _keys
            .Select(key =>
                new ExchangeSettingsItem(key, ApplicationProperty.ReadValue(key, properties) ?? string.Empty))
            .ToList();

    private bool HasName(string exchangeName) => _exchangeName == exchangeName;

    public ConnectionSettings MapToConnection() => ConnectionSettings.Instance(RetrieveParameterValue("hostname"),
        RetrieveParameterValue("port", int.Parse), RetrieveParameterValue("virtual.host"),
        RetrieveParameterValue("username"), RetrieveParameterValue("password"));

    private string RetrieveParameterValue(string key) => RetrieveParameterValue(key, value => value);

    private T RetrieveParameterValue<T>(string keySuffix, Func<string, T> converter)
    {
        var key = ResolveKey(keySuffix);
        var value = _parameters
            .Where(param => param.HasKey(key))
            .Select(param => param.Value)
            .First();
        return converter(value);
    }

    private string ResolveKey(string keySuffix) => $"exchange.{_exchangeName}.{keySuffix}";
}