using System;
using GamePush;

namespace TowerMergeTD.API
{
    public class GamePushDeviceProvider : IDeviceProvider
    {
        public DeviceType GetCurrentDevice()
        {
            //TODO: config GP_PlatformSetting does not work
            return DeviceType.Desktop;
            
            if (GP_Device.IsDesktop())
                return DeviceType.Desktop;
            
            if(GP_Device.IsMobile())
                return DeviceType.Mobile;
            
            throw new Exception($"Device does not support");
        }
    }
}