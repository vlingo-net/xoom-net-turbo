// Copyright © 2012-2021 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using Vlingo.Xoom.Actors;
using Vlingo.Xoom.Actors.Plugin;

namespace Vlingo.Xoom.Turbo.Scooter.Plugin.Mailbox.Blocking
{
	public class BlockingMailboxPlugin : AbstractPlugin, IPlugin, IMailboxProvider
	{
		private readonly BlockingMailboxPluginConfiguration _configuration;

		public BlockingMailboxPlugin()
		{
			_configuration = new BlockingMailboxPluginConfiguration();
		}

		public override void Close()
		{
		}

		public IMailbox ProvideMailboxFor(int? hashCode) => new BlockingMailbox();

		public IMailbox ProvideMailboxFor(int? hashCode, IDispatcher dispatcher) => new BlockingMailbox();

		public override void Start(IRegistrar registrar)
		{
			registrar.Register(Name, false, this);
		}

		public override IPlugin With(IPluginConfiguration? overrideConfiguration) => this;

		public override string Name => ((IPluginConfiguration)_configuration).Name;
		public override int Pass => 1;
		public override IPluginConfiguration Configuration => _configuration;
	}

	public class BlockingMailboxPluginConfiguration : IPluginConfiguration
	{
		private string _name;

		public BlockingMailboxPluginConfiguration()
		{
		}

		public void Build(Configuration configuration)
		{
		}

		public void BuildWith(Configuration configuration, PluginProperties properties)
		{
		}

		string IPluginConfiguration.Name => _name;
	}
}