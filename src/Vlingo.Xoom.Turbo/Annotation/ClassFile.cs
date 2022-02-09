// Copyright © 2012-2022 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System;
using System.IO;

namespace Vlingo.Xoom.Turbo.Annotation
{
	public class ClassFile
	{
		private readonly FileStream _file;

		public static ClassFile From(string path, Type typeElement) => new ClassFile(path, typeElement);

		private ClassFile(string path, Type typeElement)
		{
			var className = $"{typeElement.Name}.cs";

			var packageName = typeElement.Namespace;

			_file = FindFile(path, packageName, className);
		}

		private FileStream FindFile(string path, string? packageName, string className)
		{
			var sourceFolder = Context.LocateSourceFolder(path);
			var packagePath = packageName?.Replace("\\.", "/") ?? string.Empty;
			return new FileStream(Path.Combine(sourceFolder, packagePath, className), FileMode.Create);
		}

		public FileStream OpenInputStream() => _file;
	}
}