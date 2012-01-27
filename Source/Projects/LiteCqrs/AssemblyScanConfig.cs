using System;
using System.Reflection;
using EnsureThat;

namespace LiteCqrs
{
	public class AssemblyScanConfig
	{
		public Assembly Assembly { get; private set; }
		public Func<string, bool> NamespaceFilter { get; set; }

		public AssemblyScanConfig(Assembly assembly)
		{
			Ensure.That(assembly, "assembly").IsNotNull();

			Assembly = assembly;
		}
	}
}