using System;
using UnityEditor;

namespace UnityBuilder {
	public class BuilderTarget
	{
		public BuildTarget target;
		public string folder;
		public string extension;

		public BuilderTarget( BuildTarget target, string folder, string extension = "" ){
			this.target = target;
			this.folder = folder;
			this.extension = extension;
		}
	}
}

