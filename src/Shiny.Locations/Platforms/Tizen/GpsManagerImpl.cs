﻿using System;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Tizen.Location;


namespace Shiny.Locations
{
    public class GpsManagerImpl : IGpsManager
    {
        readonly Locator locator;
        static Location lastKnownLocation = new Location();


        public GpsManagerImpl()
        {
            var locatorType = LocationType.Gps;
            var gps = Platform.Get<bool>("location.gps");
            var wps = Platform.Get<bool>("location.wps");
            if (gps)
            {
                locatorType = wps ? LocationType.Hybrid : LocationType.Gps;
            }
            else
            {
                locatorType = wps ? LocationType.Wps : LocationType.Passive;
            }
            this.locator = new Locator(locatorType);
        }

        public bool IsListening => false;

        public AccessState GetCurrentStatus(bool background)
        {
            //this.locator.DistanceBasedLocationChanged
            throw new NotImplementedException();
        }

        public IObservable<IGpsReading> GetLastReading()
            => Observable.Return(lastKnownLocation);

        public Task<AccessState> RequestAccess(bool backgroundMode)
        {
            throw new NotImplementedException();
        }

        public Task StartListener(GpsRequest request)
        {
            this.locator.Start();
            return Task.CompletedTask;
        }


        public Task StopListener()
        {
            this.locator.Stop();
            return Task.CompletedTask;
        }


        public IObservable<AccessState> WhenAccessStatusChanged(bool forBackground)
        {
            throw new NotImplementedException();
        }


        public IObservable<IGpsReading> WhenReading() => Observable.Create<IGpsReading>(ob =>
        {
            var handler = new EventHandler<LocationChangedEventArgs>((sender, args) =>
            {

            });
            this.locator.LocationChanged += handler;
            return () => this.locator.LocationChanged -= handler;
        });
    }
}




//            double KmToMetersPerSecond(double km) => km * 0.277778;
//            service.LocationChanged += (s, e) =>
//            {
//                if (e.Location != null)
//                {
//                    lastKnownLocation.Accuracy = e.Location.Accuracy;
//                    lastKnownLocation.Altitude = e.Location.Altitude;
//                    lastKnownLocation.Course = e.Location.Direction;
//                    lastKnownLocation.Latitude = e.Location.Latitude;
//                    lastKnownLocation.Longitude = e.Location.Longitude;
//                    lastKnownLocation.Speed = KmToMetersPerSecond(e.Location.Speed);
//                    lastKnownLocation.Timestamp = e.Location.Timestamp;
//                }
