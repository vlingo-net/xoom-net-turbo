// Copyright © 2012-2021 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

namespace Vlingo.Xoom.Turbo.Codegen
{
	public class TextExpectation
	{
		private readonly Dialect.Dialect _dialect;

		private TextExpectation(Dialect.Dialect dialect) => _dialect = dialect;

		public static TextExpectation OnCSharp => new TextExpectation(Dialect.Dialect.CSharp);

		public string Read(string textFileName)
		{
			var path = $"/resources/TextExpectation/Csharp/{textFileName}.txt";
			return System.IO.File.ReadAllText(path);
		}
	}
}