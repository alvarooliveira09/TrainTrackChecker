using Microsoft.JSInterop;
using Microsoft.Maui.Devices.Sensors;
using System.Text.Json;

namespace TrainTrackChecker.Models {
    public static class ExtensionMethods
    {
        public static T DeepCopy<T>(this T self)
        {
            var serialized = JsonSerializer.Serialize(self);
            var result = JsonSerializer.Deserialize<T>(serialized) ?? default!;
            return result;
        }
    }
    public static class Helpers {

        public static class GPSLocation {

            //https://learn.microsoft.com/en-us/dotnet/maui/platform-integration/device/geolocation?view=net-maui-9.0&tabs=android

            private static CancellationTokenSource? _cancelTokenSource;
            private static bool _isCheckingLocation;

            public class LatLng {
                public double Latitude { get; set; } = 0;
                public double Longitude { get; set; } = 0;
            }

            public static async Task<LatLng?> GetCurrentLocation() {
                try {
                    _isCheckingLocation = true;

                    GeolocationRequest request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));

                    _cancelTokenSource = new CancellationTokenSource();

                    var location = await Geolocation.Default.GetLocationAsync(request, _cancelTokenSource.Token);

                    if (location != null)
                        return new LatLng() { Latitude = location.Latitude, Longitude = location.Longitude };

                }

                catch (Exception ex) {
                    // Unable to get location
                } finally {
                    _isCheckingLocation = false;
                }
                return null;
            }

            public static void CancelRequest() {
                if (_isCheckingLocation && _cancelTokenSource != null && _cancelTokenSource.IsCancellationRequested == false)
                    _cancelTokenSource.Cancel();
            }




            public static async Task<LatLng?> GetCachedLocation() {
                try {
                    var location = await Geolocation.Default.GetLastKnownLocationAsync();

                    if (location != null)
                        return new LatLng() { Latitude = location.Latitude, Longitude = location.Longitude };

                } catch (FeatureNotSupportedException fnsEx) {
                    // Handle not supported on device exception
                } catch (FeatureNotEnabledException fneEx) {
                    // Handle not enabled on device exception
                } catch (PermissionException pEx) {
                    // Handle permission exception
                } catch (Exception ex) {
                    // Unable to get location
                }

                return null;
            }


            public static double CalculateDistanceee(Location locationStart, Location locationEnd) {
                return Location.CalculateDistance(locationStart, locationEnd, DistanceUnits.Kilometers);
            }

        }

        public static DateTime? JoinDateTime(DateTime? date, DateTime? time) {
            if (time == null) { return null; }
            if (date == null) { date = DateTime.Now; }
            return new DateTime(
                year: date.Value.Year,
                month: date.Value.Month,
                day: date.Value.Day,
                hour: time.Value.Hour,
                minute: time.Value.Minute,
                second: time.Value.Second
            );
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value">EnumValue.ToString()</param>
        /// <returns></returns>
        public static string EnumToFormattedText(string enumValue) {
            var sb = new System.Text.StringBuilder();
            sb.Append(enumValue[0]);
            for (var i = 1; i < enumValue.Length; i++) {
                if (char.IsUpper(enumValue[i])) {
                    sb.Append(" ");
                }
                sb.Append(char.ToLower(enumValue[i]));
            }

            return sb.ToString().Trim();
        }


        /// <summary>
        /// return (datetime == null) ? "" : datetime.Value.ToString("dd/MM/yyyy HH:mmh");
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string DateTimeToLongString(DateTime? datetime) {
            return (datetime == null) ? "" : datetime.Value.ToString("dd/MM/yyyy HH:mm'h'");
        }

    }

}
