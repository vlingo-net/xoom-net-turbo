// Copyright © 2012-2022 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System.Collections.Immutable;

namespace Vlingo.Xoom.Turbo.Annotation;

using System.IO;
using System.Linq;

public class PackageCollector
{
	private readonly string? _projectPath;
	private readonly string _basePackage;

	private PackageCollector(string? projectPath, string basePackage)
	{
		_projectPath = projectPath;
		_basePackage = basePackage;
	}

	public static PackageCollector From(string? projectPath, string basePackage) =>
		new PackageCollector(projectPath, basePackage);

	public ImmutableHashSet<string> CollectAll()
	{
		var basePackageDirectory = ResolvePackagePath(_basePackage);
		return Directory
			.GetDirectories(basePackageDirectory, "*", SearchOption.AllDirectories)
			.Where(path => !path.Contains($@"{Path.DirectorySeparatorChar}bin"))
			.Where(path => !path.Contains($@"{Path.DirectorySeparatorChar}bin{Path.DirectorySeparatorChar}"))
			.Where(path => !path.Contains($@"{Path.DirectorySeparatorChar}obj"))
			.Where(path => !path.Contains($@"{Path.DirectorySeparatorChar}obj{Path.DirectorySeparatorChar}"))
			.ToImmutableHashSet();
	}

	private string ResolvePackagePath(string packageName)
	{
		var sourceFolderPath = ResolveSourceFolderPath();
		return Path.GetFullPath(sourceFolderPath);
	}

	private string ResolveSourceFolderPath() => Path.GetFullPath(_projectPath!);
}