using System;
using UnityEngine;
using Microsoft.MixedReality.Toolkit;

namespace Microsoft.MixedReality.Toolkit.Extensions
{
	[MixedRealityServiceProfile(typeof(INewService))]
	[CreateAssetMenu(fileName = "NewServiceProfile", menuName = "MixedRealityToolkit/NewService Configuration Profile")]
	public class NewServiceProfile : BaseMixedRealityProfile
	{
		// Store config data in serialized fields
	}
}