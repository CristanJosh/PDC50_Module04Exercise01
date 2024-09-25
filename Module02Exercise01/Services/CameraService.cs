using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module02Exercise01.Services
{
    public static class CameraService
    {
        public static async Task<string> CapturePhotoAsync()
        {
            try
            {
                var photo = await MediaPicker.CapturePhotoAsync();
                if (photo != null)
                {
                    // Save photo to file
                    var stream = await photo.OpenReadAsync();
                    return photo.FullPath;
                }
            }
            catch (FeatureNotSupportedException)
            {
                // Feature is not supported on the device
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
