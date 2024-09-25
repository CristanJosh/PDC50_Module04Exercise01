using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module02Exercise01.Services
{
    public static class GeolocationService
    {
        public static async Task<Location> GetLocationAsync()
        {
            try
            {
                var location = await Geolocation.Default.GetLocationAsync();
                if (location != null)
                {
                    return location;
                }
            }
            catch (FeatureNotSupportedException)
            {
                // Feature not supported on the device
            }
            catch (PermissionException)
            {
                // Permissions are not granted
            }
            catch (System.Exception)
            {
                // Something went wrong
            }
            return null;
        }
    }
}
