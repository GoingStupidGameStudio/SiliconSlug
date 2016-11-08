using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace RxPingPong
{
    /// <summary>Simple Ping Pong Actor model using Rx </summary>
    /// <remarks>
    /// You'll need to install the Reactive Extensions (Rx) for this to work.
    /// You can get the installer from <see href="http://msdn.microsoft.com/en-us/devlabs/ee794896.aspx"/>
    /// </remarks>
    class PingPong
    {
        PingPong()
        {
            var ping = new Ping();
            var pong = new Pong();

            Console.WriteLine("Press any key to stop ...");

            var pongSubscription = ping.Subscribe(pong);
            var pingSubscription = pong.Subscribe(ping);

            Console.ReadKey();

            pongSubscription.Dispose();
            pingSubscription.Dispose();

            Console.WriteLine("Ping Pong has completed.");
        }
    }

    class Ping : ISubject<Pong, Ping>
    {
        #region Implementation of IObserver<Pong>

        /// <summary>
        /// Notifies the observer of a new value in the sequence.
        /// </summary>
        public void OnNext(Pong value)
        {
            Console.WriteLine("Ping received Pong.");
        }

        /// <summary>
        /// Notifies the observer that an exception has occurred.
        /// </summary>
        public void OnError(Exception exception)
        {
            Console.WriteLine("Ping experienced an exception and had to quit playing.");
        }

        /// <summary>
        /// Notifies the observer of the end of the sequence.
        /// </summary>
        public void OnCompleted()
        {
            Console.WriteLine("Ping finished.");
        }

        #endregion

        #region Implementation of IObservable<Ping>

        /// <summary>
        /// Subscribes an observer to the observable sequence.
        /// </summary>
        public IDisposable Subscribe(IObserver<Ping> observer)
        {
            return Observable.Interval(TimeSpan.FromSeconds(2))
                .Where(n => n < 10)
                .Select(n => this)
                .Subscribe(observer);
        }

        #endregion

        #region Implementation of IDisposable

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <filterpriority>2</filterpriority>
        public void Dispose()
        {
            OnCompleted();
        }

        #endregion
    }

    class Pong : ISubject<Ping, Pong>
    {
        #region Implementation of IObserver<Ping>

        /// <summary>
        /// Notifies the observer of a new value in the sequence.
        /// </summary>
        public void OnNext(Ping value)
        {
            Console.WriteLine("Pong received Ping.");
        }

        /// <summary>
        /// Notifies the observer that an exception has occurred.
        /// </summary>
        public void OnError(Exception exception)
        {
            Console.WriteLine("Pong experienced an exception and had to quit playing.");
        }

        /// <summary>
        /// Notifies the observer of the end of the sequence.
        /// </summary>
        public void OnCompleted()
        {
            Console.WriteLine("Pong finished.");
        }

        #endregion

        #region Implementation of IObservable<Pong>

        /// <summary>
        /// Subscribes an observer to the observable sequence.
        /// </summary>
        public IDisposable Subscribe(IObserver<Pong> observer)
        {
            return Observable.Interval(TimeSpan.FromSeconds(1.5))
                .Where(n => n < 10)
                .Select(n => this)
                .Subscribe(observer);
        }

        #endregion

        #region Implementation of IDisposable

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <filterpriority>2</filterpriority>
        public void Dispose()
        {
            OnCompleted();
        }

        #endregion
    }
}