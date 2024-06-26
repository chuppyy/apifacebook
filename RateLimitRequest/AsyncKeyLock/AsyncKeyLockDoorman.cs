﻿#region

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

#endregion

namespace RateLimitRequest.AsyncKeyLock;

/// <summary>
///     An asynchronous locker that provides read and write locking policies.
/// </summary>
internal sealed class AsyncKeyLockDoorman
{
#region Constructors

    /// <summary>
    ///     Initializes a new instance of the <see cref="AsyncKeyLockDoorman" /> class.
    /// </summary>
    /// <param name="reset">The reset action.</param>
    public AsyncKeyLockDoorman(Action<AsyncKeyLockDoorman> reset)
    {
        _waitingWriters = new Queue<TaskCompletionSource<Releaser>>();
        _waitingReader  = new TaskCompletionSource<Releaser>();
        _status         = 0;

        _readerReleaser = Task.FromResult(new Releaser(this, false));
        _writerReleaser = Task.FromResult(new Releaser(this, true));
        _reset          = reset;
    }

#endregion

#region Nested type: Releaser

    public readonly struct Releaser : IDisposable
    {
        private readonly AsyncKeyLockDoorman toRelease;
        private readonly bool                writer;

        internal Releaser(AsyncKeyLockDoorman toRelease, bool writer)
        {
            this.toRelease = toRelease;
            this.writer    = writer;
        }

        public void Dispose()
        {
            if (toRelease != null)
            {
                if (writer)
                    toRelease.WriterRelease();
                else
                    toRelease.ReaderRelease();
            }
        }
    }

#endregion

#region Fields

    private readonly Task<Releaser>                        _readerReleaser;
    private readonly Action<AsyncKeyLockDoorman>           _reset;
    private readonly Queue<TaskCompletionSource<Releaser>> _waitingWriters;
    private readonly Task<Releaser>                        _writerReleaser;
    private          int                                   _readersWaiting;
    private          int                                   _status;
    private          TaskCompletionSource<Releaser>        _waitingReader;

#endregion

#region Properties

    /// <summary>
    ///     Gets or sets the key that this doorman is mapped to.
    /// </summary>
    public string Key { get; set; }

    /// <summary>
    ///     Gets or sets the current reference count on this doorman.
    /// </summary>
    public int RefCount { get; set; }

#endregion

#region Methods

    /// <summary>
    ///     Locks the current thread in read mode asynchronously.
    /// </summary>
    /// <returns>The <see cref="Task{Releaser}" />.</returns>
    public Task<Releaser> ReaderLockAsync()
    {
        lock (_waitingWriters)
        {
            if (_status >= 0 && _waitingWriters.Count == 0)
            {
                ++_status;
                return _readerReleaser;
            }

            ++_readersWaiting;
            return _waitingReader.Task.ContinueWith(t => t.Result);
        }
    }

    /// <summary>
    ///     Locks the current thread in write mode asynchronously.
    /// </summary>
    /// <returns>The <see cref="Task{Releaser}" />.</returns>
    public Task<Releaser> WriterLockAsync()
    {
        lock (_waitingWriters)
        {
            if (_status == 0)
            {
                _status = -1;
                return _writerReleaser;
            }

            var waiter = new TaskCompletionSource<Releaser>();
            _waitingWriters.Enqueue(waiter);
            return waiter.Task;
        }
    }

    private void ReaderRelease()
    {
        TaskCompletionSource<Releaser> toWake = null;

        lock (_waitingWriters)
        {
            --_status;

            if (_status == 0)
                if (_waitingWriters.Count > 0)
                {
                    _status = -1;
                    toWake  = _waitingWriters.Dequeue();
                }
        }

        _reset(this);

        toWake?.SetResult(new Releaser(this, true));
    }

    private void WriterRelease()
    {
        TaskCompletionSource<Releaser> toWake         = null;
        var                            toWakeIsWriter = false;

        lock (_waitingWriters)
        {
            if (_waitingWriters.Count > 0)
            {
                toWake         = _waitingWriters.Dequeue();
                toWakeIsWriter = true;
            }
            else if (_readersWaiting > 0)
            {
                toWake          = _waitingReader;
                _status         = _readersWaiting;
                _readersWaiting = 0;
                _waitingReader  = new TaskCompletionSource<Releaser>();
            }
            else
            {
                _status = 0;
            }
        }

        _reset(this);

        toWake?.SetResult(new Releaser(this, toWakeIsWriter));
    }

#endregion
}