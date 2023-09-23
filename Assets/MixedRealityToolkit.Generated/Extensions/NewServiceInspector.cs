#if UNITY_EDITOR
using System;
using Microsoft.MixedReality.Toolkit.Editor;
using Microsoft.MixedReality.Toolkit;
using UnityEngine;
using UnityEditor;

namespace Microsoft.MixedReality.Toolkit.Extensions.Editor
{	
	[MixedRealityServiceInspector(typeof(INewService))]
	public class NewServiceInspector : BaseMixedRealityServiceInspector
	{
		public override void DrawInspectorGUI(object target)
		{
			NewService service = (NewService)target;
			
			// Draw inspector here
		}
	}
}

#endif