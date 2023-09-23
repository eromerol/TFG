using Microsoft.MixedReality.Toolkit.Utilities;
using Microsoft.MixedReality.Toolkit;

namespace Microsoft.MixedReality.Toolkit.Extensions
{
	[MixedRealityExtensionService(SupportedPlatforms.WindowsStandalone|SupportedPlatforms.MacStandalone|SupportedPlatforms.LinuxStandalone|SupportedPlatforms.WindowsUniversal|SupportedPlatforms.WindowsEditor)]
	public class NewService : BaseExtensionService, INewService, IMixedRealityExtensionService
	{
		private NewServiceProfile newServiceProfile;

		public NewService(string name,  uint priority,  BaseMixedRealityProfile profile) : base(name, priority, profile) 
		{
			newServiceProfile = (NewServiceProfile)profile;
		}


		public override void Initialize()
		{
			base.Initialize();

			// Do service initialization here.
		}

		public override void Update()
		{
			base.Update();

			// Do service updates here.
		}
	}
}
