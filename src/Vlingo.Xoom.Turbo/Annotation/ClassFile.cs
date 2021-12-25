// Copyright © 2012-2021 VLINGO LABS. All rights reserved.
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

		public static ClassFile From(FileStream filer, Type typeElement)
		{
			return new ClassFile(filer, typeElement);
		}

		private ClassFile(FileStream filer, Type typeElement)
		{
			var className = typeElement.Name + ".cs";

			var packageName = typeElement.Namespace;

			this._file = FindFile(filer, packageName, className);
		}

		private FileStream FindFile(FileStream filer, string packageName,
			string className)
		{
			var sourceFolder = Context.LocateSourceFolder(filer);
			var packagePath = packageName.Replace("\\.", "/");
			return new FileStream(Path.Combine(className, "src"), FileMode.Create);
		}

		public FileStream OpenInputStream()
		{
			return _file;
		}
	}
}